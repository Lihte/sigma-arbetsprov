using System;

namespace Metaphone
{
    class Program
    {
        static void Main(string[] args)
        {
            // In while loop simply to allow for running several times in a row while debugging without having to restart the program
            var input = string.Empty;
            while(input != "exit")
            {
                var metaphone = new Metaphone();

                Console.WriteLine(metaphone.TransformWord("solitude"));
                Console.WriteLine(metaphone.TransformWord("debunker"));
                Console.WriteLine(metaphone.TransformWord("aardvark"));
                Console.WriteLine(metaphone.TransformWord("cutlass"));
                Console.WriteLine(metaphone.TransformWord("metaphone"));
                Console.WriteLine(metaphone.TransformWord("kiss"));
                Console.WriteLine(metaphone.TransformWord("roosevelt"));
                Console.WriteLine(metaphone.TransformWord("knock"));
                Console.WriteLine(metaphone.TransformWord("xanadu"));
                Console.WriteLine(metaphone.TransformWord("potatoes"));
                Console.WriteLine(metaphone.TransformWord("wroom"));
                Console.WriteLine(metaphone.TransformWord("tomb"));
                Console.WriteLine(metaphone.TransformWord("aggressive"));
                Console.WriteLine(metaphone.TransformWord("why"));
                Console.WriteLine(metaphone.TransformWord("yourself"));
                input = Console.ReadLine();
            }
        }
    }
}
