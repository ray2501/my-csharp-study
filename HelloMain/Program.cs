using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloMain
{
    class Program
    {
        static async Task Main()
        {
            await GetName();
            Console.WriteLine("Hello, it is C# async Main function");
        }
        
        static async Task GetName(){
            await Task.Delay(100);
            Console.WriteLine("Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("In antoher thread...");
        }
    }
}
