using System;
using static ConjunctiveNormalForm.CnfTest;

namespace ConjunctiveNormalForm
{

    /*
     * 
     *      Given the following truth table.
     *      
     *      Input:          a, b, c
     *      Desired output: s
     *      
     *      ^ / * = konjunction
     *      v / + = disjunction
     *      - = negation
     *      
     *       a | b | c | s
     *  1.   0 | 0 | 0 | 1           
     *  2.   0 | 0 | 1 | 0      t1 = a ^ b ^ -c
     *  3.   0 | 1 | 0 | 0      t2 = a ^ -b ^ c
     *  4.   0 | 1 | 1 | 0      t3 = a ^ -b ^ -c
     *  5.   1 | 0 | 0 | 1      
     *  6.   1 | 0 | 1 | 1      
     *  7.   1 | 1 | 0 | 1      
     *  8.   1 | 1 | 1 | 1      
     *  
     *  Equivalence transformation of the DNF:
     *  
     *  s = (a + b + -c)(a + -b + c)(a + -b + -c)
     *    = a + (b + -c)(-b + c)(-b + -c) // factorice a
     *    = a + (b + -c)(-b + c-c)
     *    = a + (b + -c)-b
     *    = a + b-b + -b-c
     *    = a + -b-c
     *    
     *    It must now be proved that:
     *    
     *    (a + b + -c)(a + -b + c)(a + -b + -c) == a + -b-c
     *
     */

    static class CnfTest
    {
        // "before" refers to BEFOR equivalence transformation
        public static bool Cnf_before(bool a, bool b, bool c)
        {
            //      a + b + -c    *   a + -b + c    *   a + -b + -c              
            return (a || b || !c) && (a || !b || c) && (a || !b || !c); 
        }


        // "after" refers to AFTER equivalence transformation
        public static bool Cnf_after(bool a, bool b, bool c)
        {
            // Parantheses just for readability

            //      -b-c     * a
            return a || (!b && !c);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var truthTable = new bool[,]
           {
                { false, false, false }, 
                { false, false, true },
                { false, true, false },
                { false, true, true },
                { true, false, false },  
                { true, false, true },   
                { true, true, false },   
                { true, true, true }
           };

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                var a = truthTable[i, 0];
                var b = truthTable[i, 1];
                var c = truthTable[i, 2];

                // Proving
                if (Cnf_before(a, b, c) == Cnf_after(a, b, c))
                {
                    Console.WriteLine($"{i + 1}. Same value");
                }
                else
                {
                    Console.WriteLine($"{i + 1}. Not the same value");
                }
            }

            Console.ReadLine();
        }
    }
}
