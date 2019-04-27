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
            #region Work With XML
            CreateXML();
            QueryXml();
            #endregion
            #region Quere
            //AggregatingDataWithQuerySyntax();
            //AggregatingDatawithMethodSyntax();

            //JoiningDataWithQuerySyntax();
            //JoiningDataWithMethodSyntax();

            //GroupingDataWithQuerySyntax();
            //GroupingDataWithMethodSyntax();

            //UsingAGroupJoinForHierarchicalDataWithQuerySyntax();
            //UsingAGroupJoinForHierarchicalDataWithMethodsSyntax();
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
        private static void QueryXml()
        {
            GetDataFromXmlFileUsingQuereSyntax();
            GetDataFromXMLFileUsingMethodSyntax();
        }

        private static void GetDataFromXMLFileUsingMethodSyntax()
        {
            var document = XDocument.Load("fuel.xml");
            var quere2 = document.Elements("Cars").Elements("Car")
                .Where(e => e.Attribute("Manufacturer")?.Value == "BMW").Select(
                 e => e.Attribute("Name").Value
                );
            Console.WriteLine("***** Get Data from XML file Using Method Syntax *****");
            Console.WriteLine();
            foreach (var name in quere2)
            {
                Console.WriteLine(name);
            }
        }
        private static void GetDataFromXmlFileUsingQuereSyntax()
        {
            var document = XDocument.Load("fuel.xml");
            var quere = from element in document.Elements("Cars").Elements("Car")
                        where element.Attribute("Manufacturer")?.Value == "BMW"
                        select (
                        element.Attribute("Name").Value
                        );
            Console.WriteLine("***** Get Data from XML file Using Quere Syntax *****");
            Console.WriteLine();
            foreach (var name in quere)
            {
                Console.WriteLine(name);
            }

        }

        private static void AggregatingDataWithQuerySyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query =
               from car in cars
               group car by car.Manufacturer into carGroup
               select new
               {
                   Name = carGroup.Key,
                   Max = carGroup.Max(c => c.Combined),
                   Min = carGroup.Min(c => c.Combined),
                   Avg = carGroup.Average(c => c.Combined),

               } into result
               orderby result.Max descending
               select result;
            Console.WriteLine();
            Console.WriteLine("***** Aggregating Data with Query Syntax   *****");
            Console.WriteLine();
            foreach (var result in query)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max : {result.Max}");
                Console.WriteLine($"\t Min : {result.Min}");
                Console.WriteLine($"\t Avg : {result.Avg}");


            }
        }
        private static void AggregatingDatawithMethodSyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query2 =
               cars.GroupBy(c => c.Manufacturer)
                   .Select(g =>
                   {
                       var results = g.Aggregate(new CarStatistics(),
                                           (acc, c) => acc.Accumulate(c),
                                           acc => acc.Compute());
                       return new
                       {
                           Name = g.Key,
                           Avg = results.Average,
                           Min = results.Min,
                           Max = results.Max
                       };
                   })
                   .OrderByDescending(r => r.Max);

            foreach (var result in query2)
            {
                Console.WriteLine($"{result.Name}");
                Console.WriteLine($"\t Max: {result.Max}");
                Console.WriteLine($"\t Min: {result.Min}");
                Console.WriteLine($"\t Avg: {result.Avg}");
            }
        }

        private static void JoiningDataWithQuerySyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query =
                from car in cars
                join manufacturer in manufacturers
                    on new { car.Manufacturer, car.Year }
                    equals new { Manufacturer = manufacturer.Name, manufacturer.Year }
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
        }
        private static void JoiningDataWithMethodSyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
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
        }

        private static void GroupingDataWithQuerySyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query =
                from car in cars
                group car by car.Manufacturer.ToUpper()
                into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            Console.WriteLine();
            Console.WriteLine("***** Grouping Data with Query Syntax  *****");
            Console.WriteLine();
            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

            }
        }
        private static void GroupingDataWithMethodSyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
                .OrderBy(g => g.Key);
            Console.WriteLine();
            Console.WriteLine("***** Grouping Data with Method Syntax *****");
            Console.WriteLine();
            foreach (var group in query2)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

            }
        }

        private static void UsingAGroupJoinForHierarchicalDataWithQuerySyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query =
                from manufacturer in manufacturers
                join car in cars
                on manufacturer.Name equals car.Manufacturer
                into carGroup
                orderby manufacturer.Name
                select new
                {
                    manufacturer = manufacturer,
                    cars = carGroup
                };
            Console.WriteLine();
            Console.WriteLine("***** Grouping Data with Query Syntax  *****");
            Console.WriteLine();
            foreach (var group in query)
            {
                Console.WriteLine($"{group.manufacturer.Name} : {group.manufacturer.Headquarters}");
                foreach (var car in group.cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

            }
        }
        private static void UsingAGroupJoinForHierarchicalDataWithMethodsSyntax()
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");
            var query2 = manufacturers.GroupJoin(cars,
                                                m => m.Name,
                                                c => c.Manufacturer,
                                                (m, g) => new
                                                {
                                                    manufacturer = m,
                                                    cars = g
                                                }).OrderBy(m => m.manufacturer.Name);

            Console.WriteLine();
            Console.WriteLine("***** Grouping Data with Method Syntax *****");
            Console.WriteLine();
            foreach (var group in query2)
            {
                Console.WriteLine($"{group.manufacturer.Name} : {group.manufacturer.Headquarters}");
                foreach (var car in group.cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

            }
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


