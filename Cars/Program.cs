using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Get Data from data source
            //Finding the Most Fuel Efficient Car
            //Note Combined=> Fuel efficiency
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            #endregion
            #region Joining Data with Query Syntax
            //Joining Data with Query Syntax
            var query =
                from car in cars
                join manufacturer in manufacturers
                    on new { car.Manufacturer ,car.Year}
                    equals new { Manufacturer= manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name ascending
                select new 
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined
                };

            Console.WriteLine();
            Console.WriteLine("***** Joining Data with Query Syntax *****");
            Console.WriteLine();
            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
            #endregion
            #region Joining Data Using Method Syntax 
            //Joining Data Using Method Syntax
            var query2 = cars.Join(manufacturers,
                                     c => new { c.Manufacturer, c.Year },
                                     m => new { Manufacturer = m.Name, m.Year },
                                      (c, m) => new
                                          {
                                              m.Headquarters,
                                              c.Name,
                                              c.Combined
                                          })
                                   .OrderByDescending(c => c.Combined)
                                  .ThenBy(m => m.Name);
            Console.WriteLine();
            Console.WriteLine("***** Joining Data Using Method Syntax *****");
            Console.WriteLine();
            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }
            #endregion
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar()
                    .ToList();
        }
        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                   File.ReadAllLines(path)
                       .Where(l => l.Length > 1)
                       .Select(l =>
                       {
                           var columns = l.Split(',');
                           return new Manufacturer
                           {
                               Name = columns[0],
                               Headquarters = columns[1],
                               Year = int.Parse(columns[2])
                           };
                       });
            return query.ToList();
        }

    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}


