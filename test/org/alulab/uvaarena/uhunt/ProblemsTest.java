/*
 * Copyright (c) 2016, Sudipto Chandra
 * 
 * Permission to use, copy, modify, and/or distribute this software for any
 * purpose with or without fee is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
 * WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
 * ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
 * WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
 * ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
 * OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
 */
package org.alulab.uvaarena.uhunt;

import org.json.simple.JSONArray;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Dipu
 */
public class ProblemsTest {

    public ProblemsTest() {
    }

    /**
     * Test of parse method, of class Problems.
     */
    @Test
    public void testParse() throws ParseException {
        System.out.println("----get problems----");
        String json = "[[36,100,\"The 3n + 1 problem\",72828,0,0,3,6722,0,0,106689,0,61233,262,54871,5209,253524,4698,180925,3000,1,0],[38,102,\"Ecological Bin Packing\",22348,0,0,0,1950,0,0,12155,0,4605,18,2912,72,33451,640,34621,3000,1,0],[39,103,\"Stacking Boxes\",6595,0,0,0,37,0,0,9209,0,4490,4,2920,0,11788,1360,9950,3000,2,0],[40,104,\"Arbitrage\",3816,0,0,0,307,0,0,2288,0,2258,13,3623,427,12312,590,6534,3000,2,0],[41,105,\"The Skyline Problem\",8179,0,0,0,444,0,0,4979,0,6685,6,1353,214,15746,9579,12140,3000,1,0],[44,108,\"Maximum Sum\",13802,0,0,0,393,0,0,6204,0,2771,11,7960,336,11560,1527,21933,3000,1,0],[46,110,\"Meta-Loopless Sorts\",1846,0,0,0,200,0,0,949,0,558,11,395,15,4022,1581,3139,3000,2,0],[47,111,\"History Grading\",8153,0,0,0,130,0,0,3752,0,1436,10,1789,9,8478,49,11851,3000,1,0],[48,112,\"Tree Summing\",5278,0,0,0,360,0,0,3040,0,5418,0,2121,183,12949,194,8831,3000,1,0],[49,113,\"Power of Cryptography\",15865,0,0,0,703,0,0,6301,0,1967,3,6631,59,16938,115,21342,3000,1,0],[50,114,\"Simulation Wizardry\",1665,0,0,0,90,0,0,519,0,687,3,1150,6,3083,59,2482,3000,1,0],[51,115,\"Climbing Trees\",1583,0,0,0,53,0,0,618,0,616,2,317,17,2716,153,2185,3000,1,0],[52,116,\"Unidirectional TSP\",5978,0,0,0,373,0,0,3478,0,5041,7,2977,545,22841,2056,9957,3000,1,0],[53,117,\"The Postal Worker Rings Once\",3008,0,0,0,72,0,0,876,0,595,0,456,26,2235,13,3986,3000,1,0],[54,118,\"Mutant Flatworld Explorers\",5455,0,0,0,284,0,0,1689,0,839,0,303,5,7200,298,6509,3000,1,0],[55,119,\"Greedy Gift Givers\",6258,0,0,0,438,0,0,2556,0,2950,0,569,12,11954,3461,7610,3000,1,0],[56,120,\"Stacks of Flapjacks\",8093,0,0,0,1059,0,0,3651,0,2302,42,1523,47,10320,82,12567,3000,2,0],[42,106,\"Fermat vs. Pythagoras\",4016,0,0,0,132,0,0,3081,0,4072,2,5759,537,4674,27,8989,3000,1,0],[43,107,\"The Cat in the Hat\",5385,0,0,0,271,0,0,4130,0,4339,1,11783,62,17708,110,9789,3000,1,0],[37,101,\"The Blocks Problem\",12309,0,0,0,915,0,0,12708,0,19934,15,8812,200,21578,5881,18350,3000,1,0]]";
        JSONArray jarr = (JSONArray) (new JSONParser()).parse(json);
        Problems result = Problems.create(jarr);
        System.out.println(result);
        assertNotNull(result);
        assertFalse(result.isEmpty());

        System.out.println("----first last----");
        System.out.print("first: ");
        System.out.println(result.first());
        System.out.print("last: ");
        System.out.println(result.last());

        System.out.println("----floors----");
        System.out.print("floor 103: ");
        System.out.println(result.floor(103));
        System.out.print("floor 108: ");
        System.out.println(result.floor(108));
        System.out.print("floor 109: ");
        System.out.println(result.floor(109));
        System.out.print("floor 110: ");
        System.out.println(result.floor(110));
        System.out.print("floor first: ");
        System.out.println(result.floor(result.first()));
        System.out.print("floor first - 1: ");
        System.out.println(result.floor(result.first().number() - 1));
        System.out.print("floor last: ");
        System.out.println(result.floor(result.last()));
        System.out.print("floor last + 1: ");
        System.out.println(result.floor(result.last().number() + 1));

        System.out.println("----ceilings----");
        System.out.print("ceiling 103: ");
        System.out.println(result.ceiling(103));
        System.out.print("ceiling 108: ");
        System.out.println(result.ceiling(108));
        System.out.print("ceiling 109: ");
        System.out.println(result.ceiling(109));
        System.out.print("ceiling 110: ");
        System.out.println(result.ceiling(110));
        System.out.print("ceiling first: ");
        System.out.println(result.ceiling(result.first()));
        System.out.print("ceiling first - 1: ");
        System.out.println(result.ceiling(result.first().number() - 1));
        System.out.print("ceiling last: ");
        System.out.println(result.ceiling(result.last()));
        System.out.print("ceiling last + 1: ");
        System.out.println(result.ceiling(result.last().number() + 1));

        System.out.println("----higher----");
        System.out.print("higher 108: ");
        System.out.println(result.higher(108));
        System.out.print("higher 109: ");
        System.out.println(result.higher(109));
        System.out.print("higher 115: ");
        System.out.println(result.higher(115));
        System.out.print("higher first: ");
        System.out.println(result.higher(result.first()));
        System.out.print("higher first - 1: ");
        System.out.println(result.higher(result.first().number() - 1));
        System.out.print("higher first - 10: ");
        System.out.println(result.higher(result.first().number() - 10));
        System.out.print("higher last: ");
        System.out.println(result.higher(result.last()));
        System.out.print("higher last + 1: ");
        System.out.println(result.higher(result.last().number() + 1));

        System.out.println("----lower----");
        System.out.print("lower 110: ");
        System.out.println(result.lower(110));
        System.out.print("lower 109: ");
        System.out.println(result.lower(109));
        System.out.print("lower 115: ");
        System.out.println(result.lower(115));
        System.out.print("lower first: ");
        System.out.println(result.lower(result.first()));
        System.out.print("lower first - 1: ");
        System.out.println(result.lower(result.first().number() - 1));
        System.out.print("lower last: ");
        System.out.println(result.lower(result.last()));
        System.out.print("lower last + 1: ");
        System.out.println(result.lower(result.last().number() + 1));
        System.out.print("lower last + 10: ");
        System.out.println(result.lower(result.last().number() + 10));

        System.out.println("----head set of 107----");
        System.out.println(result.headSet(107));

        System.out.println("----tail set of 113----");
        System.out.println(result.tailSet(113));

        System.out.println("----sub set of 107 to 113----");
        System.out.println(result.subSet(107, 113));

        System.out.println("----descending set----");
        System.out.println(result.descendingSet());
    }

}
