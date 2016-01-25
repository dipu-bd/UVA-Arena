package org.alulab.uvaarena.webapi;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.CookieHandler;
import java.net.CookieManager;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost; 
import org.apache.http.entity.mime.MultipartEntityBuilder;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.http.message.BasicNameValuePair;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

public class HttpCilentExample {

  private String cookies;
  private HttpClient client = HttpClientBuilder.create().build();
  private final String USER_AGENT = "Mozilla/5.0";

  public static void main(String[] args) throws Exception {

	String url = "https://accounts.google.com/ServiceLoginAuth";
	String gmail = "https://mail.google.com/mail/";

	// make sure cookies is turn on
	CookieHandler.setDefault(new CookieManager());

	HttpCilentExample http = new HttpCilentExample();

	String page = http.GetPageContent(url);
	
	List<NameValuePair> postParams = 
               http.getFormParams(page, "shiftloader@gmail.com","sd.19.93");

	http.sendPost(url, postParams);

	String result = http.GetPageContent(gmail);
	System.out.println(result);

	System.out.println("Done");
  }

  private void sendPost(String url, List<NameValuePair> postParams) 
        throws Exception {

	HttpPost post = new HttpPost(url);

	// add header
	post.setHeader("Host", "accounts.google.com");
	post.setHeader("User-Agent", USER_AGENT);
	post.setHeader("Accept", 
             "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
	post.setHeader("Accept-Language", "en-US,en;q=0.5");
	post.setHeader("Cookie", getCookies());
	post.setHeader("Connection", "keep-alive");
	post.setHeader("Referer", "https://accounts.google.com/ServiceLoginAuth");
	post.setHeader("Content-Type", "application/x-www-form-urlencoded");
                
	post.setEntity(new UrlEncodedFormEntity(postParams));

	HttpResponse response = client.execute(post);

	int responseCode = response.getStatusLine().getStatusCode();

	System.out.println("\nSending 'POST' request to URL : " + url);
	System.out.println("Post parameters : " + postParams);
	System.out.println("Response Code : " + responseCode);

	BufferedReader rd = new BufferedReader(
                new InputStreamReader(response.getEntity().getContent()));

	StringBuffer result = new StringBuffer();
	String line = "";
	while ((line = rd.readLine()) != null) {
		result.append(line);
	}

	// System.out.println(result.toString());

  }

  private String GetPageContent(String url) throws Exception {

	HttpGet request = new HttpGet(url);

	request.setHeader("User-Agent", USER_AGENT);
	request.setHeader("Accept",
		"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
	request.setHeader("Accept-Language", "en-US,en;q=0.5");

	HttpResponse response = client.execute(request);
	int responseCode = response.getStatusLine().getStatusCode();

	System.out.println("\nSending 'GET' request to URL : " + url);
	System.out.println("Response Code : " + responseCode);

	BufferedReader rd = new BufferedReader(
                new InputStreamReader(response.getEntity().getContent()));

	StringBuffer result = new StringBuffer();
	String line = "";
	while ((line = rd.readLine()) != null) {
		result.append(line);
	}

	// set cookies
	setCookies(response.getFirstHeader("Set-Cookie") == null ? "" : 
                     response.getFirstHeader("Set-Cookie").toString());

	return result.toString();

  }

  public List<NameValuePair> getFormParams(
             String html, String username, String password)
			throws UnsupportedEncodingException {

	System.out.println("Extracting form's data...");

	Document doc = Jsoup.parse(html);

	// Google form id
	Element loginform = doc.getElementById("gaia_loginform");
	Elements inputElements = loginform.getElementsByTag("input");

	List<NameValuePair> paramList = new ArrayList<NameValuePair>();

	for (Element inputElement : inputElements) {
		String key = inputElement.attr("name");
		String value = inputElement.attr("value");

		if (key.equals("Email"))
			value = username;
		else if (key.equals("Passwd"))
			value = password;

		paramList.add(new BasicNameValuePair(key, value));

	}

	return paramList;
  }

  public String getCookies() {
	return cookies;
  }

  public void setCookies(String cookies) {
	this.cookies = cookies;
  }

}