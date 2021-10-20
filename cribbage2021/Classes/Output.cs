using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class Output
    {
        public void Score(string category, int newScore)
        {
            Console.WriteLine($"{category} for {newScore}");
        }
    }
}
