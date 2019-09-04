using System;

namespace consoledi
{
     public class TestService:  ITestService  
    {
        private readonly string myString;

        public TestService(string mystring)
        {
            myString = mystring;
        }

        public void Print(string result)
        {
            Console.WriteLine($"{myString} {result}");
        }
    }
}
