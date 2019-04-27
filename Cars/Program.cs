using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateXML();


            #region Quere
            #region Get Data from data source
            //Finding the Most Fuel Efficient Car
            //Note Combined=> Fuel efficiency
            //var cars = ProcessFile("fuel.csv");
            //var manufacturers = ProcessManufacturers("manufacturers.csv");
            #endregion
            #region Joining Data with Query Syntax
            //Joining Data with Query Syntax
            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on new { car.Manufacturer ,car.Year}
            //        equals new { Manufacturer= manufacturer.Name, manufacturer.Year }
            //    orderby car.Combined descending, car.Name ascending
            //    select new 
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };

            //Console.WriteLine();
            //Console.WriteLine("***** Joining Data with Query Syntax *****");
            //Console.WriteLine();
            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}
            #endregion
            #region Joining Data Using Method Syntax 
            //Joining Data Using Method Syntax
            //var query2 = cars.Join(manufacturers,
            //                         c => new { c.Manufacturer, c.Year },
            //                         m => new { Manufacturer = m.Name, m.Year },
            //                          (c, m) => new
            //                              {
            //                                  m.Headquarters,
            //                                  c.Name,
            //                                  c.Combined
            //                              })
            //                       .OrderByDescending(c => c.Combined)
            //                      .ThenBy(m => m.Name);
            //Console.WriteLine();
            //Console.WriteLine("***** Joining Data Using Method Syntax *****");
            //Console.WriteLine();
            //foreach (var car in query2.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}
            #endregion

            #region Grouping Data with Query Syntax 
            //var query =
            //    from car in cars
            //    group car by car.Manufacturer.ToUpper()
            //    into manufacturer
            //    orderby manufacturer.Key
            //    select manufacturer;

            //Console.WriteLine();
            //Console.WriteLine("***** Grouping Data with Query Syntax  *****");
            //Console.WriteLine();
            //foreach (var group in query)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var car in group.OrderByDescending(c=>c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }

            //}
            #endregion
            #region Grouping Data with Method Syntax 
            //var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
            //    .OrderBy(g => g.Key);
            //Console.WriteLine();
            //Console.WriteLine("***** Grouping Data with Method Syntax *****");
            //Console.WriteLine();
            //foreach (var group in query2)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }

            //}

            #endregion


            #region Using a GroupJoin for Hierarchical Data with Query Syntax 
            //var query =
            //    from manufacturer in manufacturers
            //    join car in cars
            //    on manufacturer.Name equals car.Manufacturer
            //    into carGroup
            //    orderby manufacturer.Name
            //    select new
            //    {
            //        manufacturer = manufacturer,
            //        cars = carGroup
            //    };



            //Console.WriteLine();
            //Console.WriteLine("***** Grouping Data with Query Syntax  *****");
            //Console.WriteLine();
            //foreach (var group in query)
            //{
            //    Console.WriteLine($"{group.manufacturer.Name} : {group.manufacturer.Headquarters}");
            //    foreach (var car in group.cars.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }

            //}
            #endregion
            #region Using a GroupJoin for Hierarchical Data with Method Syntax 
            //var query2 = manufacturers.GroupJoin(cars,
            //                                     m => m.Name,
            //                                     c => c.Manufacturer,
            //                                     (m, g) => new
            //                                     {
            //                                         manufacturer = m,
            //                                         cars = g
            //                                     }).OrderBy(m => m.manufacturer.Name);

            //Console.WriteLine();
            //Console.WriteLine("***** Grouping Data with Method Syntax *****");
            //Console.WriteLine();
            //foreach (var group in query2)
            //{
            //    Console.WriteLine($"{group.manufacturer.Name} : {group.manufacturer.Headquarters}");
            //    foreach (var car in group.cars.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }

            //}

            #endregion


            #region Aggregating Data with Query Syntax 
            //var query =
            //    from car in cars
            //    group car by car.Manufacturer into carGroup
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),
            //        Min = carGroup.Min(c => c.Combined),
            //        Avg = carGroup.Average(c => c.Combined),

            //    } into result
            //    orderby result.Max descending
            //    select result;




            //Console.WriteLine();
            //Console.WriteLine("***** Aggregating Data with Query Syntax   *****");
            //Console.WriteLine();
            //foreach (var result in query)
            //{
            //    Console.WriteLine($"{result.Name}");
            //    Console.WriteLine($"\t Max : {result.Max}");
            //    Console.WriteLine($"\t Min : {result.Min}");
            //    Console.WriteLine($"\t Avg : {result.Avg}");


            //}
            #endregion
            #region Aggregating Data with Method Syntax 
            //var query2 =
            //   cars.GroupBy(c => c.Manufacturer)
            //       .Select(g =>
            //       {
            //           var results = g.Aggregate(new CarStatistics(),
            //                               (acc, c) => acc.Accumulate(c),
            //                               acc => acc.Compute());
            //           return new
            //           {
            //               Name = g.Key,
            //               Avg = results.Average,
            //               Min = results.Min,
            //               Max = results.Max
            //           };
            //       })
            //       .OrderByDescending(r => r.Max);

            //foreach (var result in query2)
            //{
            //    Console.WriteLine($"{result.Name}");
            //    Console.WriteLine($"\t Max: {result.Max}");
            //    Console.WriteLine($"\t Min: {result.Min}");
            //    Console.WriteLine($"\t Avg: {result.Avg}");
            //}
            #endregion
            #endregion
        }

        private static void CreateXML()
        {
            #region Work with Xml 
            var records = ProcessFile("fuel.csv");
            var document = new XDocument();
            var cars = new XElement("Cars",
                from record in records
                select new XElement("Car",
                                       new XAttribute("Name", record.Name),
                                       new XAttribute("Combined", record.Combined),
                                       new XAttribute("Manufacturer", record.Manufacturer))
                                    );

            document.Add(cars);
            document.Save("fuel.xml");
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
    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }

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


