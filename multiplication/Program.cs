using System;
using System.Collections.Generic;
using System.Linq;

namespace multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> x = Enumerable.Range(1, 9);
            IEnumerable<int> y = Enumerable.Range(1, 9);
            foreach (var (nx, ny, z) in from int nx in x
                                        from int ny in y
                                        let z = nx * ny
                                        select (nx, ny, z))
            {
                Console.WriteLine("{0, 1:G} x {1, 1:G} = {2, 2:G}", nx, ny, z);
            }
        }
    }
}
