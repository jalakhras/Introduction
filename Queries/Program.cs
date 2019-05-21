using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10).OrderBy(n => n);
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }


            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",   Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca",        Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V",       Rating = 8.7f, Year = 1980 }
            };
            var DeferredExecutionTime = DateTime.Now; 
            var queryWithDeferredExecution = movies.Filter(x => x.Year > 2008);
            var enumerator2 = queryWithDeferredExecution.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                Console.WriteLine(enumerator2.Current.Title);
            }
            Console.WriteLine("Time spend of Deferred Execution " + (DateTime.Now - DeferredExecutionTime).TotalMilliseconds);

            var DeferredImmediatelyTime = DateTime.Now;
            var queryWithImmediately = movies.Filter(x => x.Year > 2008).OrderByDescending(x=>x.Rating).ToList();

            var enumerator = queryWithImmediately.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
            Console.WriteLine("Time spend of Immediately Execution " + (DateTime.Now - DeferredImmediatelyTime).TotalMilliseconds);


        }
    }
}
