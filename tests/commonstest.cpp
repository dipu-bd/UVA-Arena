#include <QApplication>
#include <iostream>
#include <QDateTime>

#include "uhunt/submission.h"
#include "commons/conversion.h"

using namespace std;
using namespace uva;

typedef Submission::Language Language;
typedef Submission::Verdict Verdict;

int main(int argc, char* argv[])
{
    cout << "Get Language Name: " << endl;
    cout << Conversion::getLanguageName(Language::C).toStdString() << endl;
    cout << Conversion::getLanguageName(Language::CPP).toStdString() << endl;
    cout << Conversion::getLanguageName(Language::CPP11).toStdString() << endl;
    cout << Conversion::getLanguageName(Language::JAVA).toStdString() << endl;
    cout << Conversion::getLanguageName(Language::PASCAL).toStdString() << endl;
    cout << Conversion::getLanguageName(Language::OTHER).toStdString() << endl;
    cout << Conversion::getLanguageName(static_cast<Language>(342)).toStdString() << endl;
    cout << endl;

    cout << "Get Verdict Name: " << endl;
    cout << Conversion::getVerdictName(Verdict::ACCEPTED).toStdString() << endl;
    cout << Conversion::getVerdictName(Verdict::WRONG_ANSWER).toStdString() << endl;
    cout << Conversion::getVerdictName(Verdict::PRESENTATION_ERROR).toStdString() << endl;
    cout << Conversion::getVerdictName(Verdict::TIME_LIMIT).toStdString() << endl;
    cout << Conversion::getVerdictName(Verdict::OUTPUT_LIMIT).toStdString() << endl;
    cout << Conversion::getVerdictName(Verdict::IN_QUEUE).toStdString() << endl;
    cout << Conversion::getVerdictName(static_cast<Verdict>(342)).toStdString() << endl;
    cout << endl;

    cout << "Get runtime: " << endl;
    cout << Conversion::getRuntime(0).toStdString() << endl;
    cout << Conversion::getRuntime(210).toStdString() << endl;
    cout << Conversion::getRuntime(12132).toStdString() << endl;
    cout << Conversion::getRuntime(121322).toStdString() << endl;
    cout << endl;

    cout << "Get memory: " << endl;
    cout << Conversion::getMemory(0).toStdString() << endl;
    cout << Conversion::getMemory(129).toStdString() << endl;
    cout << Conversion::getMemory(12239).toStdString() << endl;
    cout << Conversion::getMemory(121123239).toStdString() << endl;
    cout << endl;

    cout << "Get time span:  " << endl;
    QDateTime cur = QDateTime::currentDateTime();
    cout << Conversion::getTimeSpan(cur, cur.addSecs(-10)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addSecs(10)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addSecs(75)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addSecs(3728)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addSecs(37020)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addDays(5)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addDays(50)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addMonths(2).addDays(8)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addMonths(23)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addYears(22)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addYears(2).addMonths(11)).toStdString() << endl;
    cout << Conversion::getTimeSpan(cur, cur.addYears(5).addMonths(10).addDays(20)).toStdString() << endl;
    cout << endl;

    cout << "Convert Unix Time: " << endl;
    cout << Conversion::getSubmissionTime(1374730211).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1408048132).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1362214471).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1368964000).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1362927261).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1408050064).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436958191).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436958191).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436959128).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436958206).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436959144).toStdString() << endl;
    cout << Conversion::getSubmissionTime(1436959249).toStdString() << endl;

    cout << "End of test." << endl << endl;

    QApplication::exit();
    return 0;
}
