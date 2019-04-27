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
            //Finding the Most Fuel Efficient Car
            //Note Combined=> Fuel efficiency
            var cars = ProcessFile("fuel.csv");
            var query = cars.OrderByDescending(c => c.Combined)
                .ThenBy(c=>c.Name);
            
            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} :{car.Combined}");
            }

            var BMWMostFuelEfficient = cars.Where(car => car.Manufacturer == "BMW" && car.Year == 2016).OrderByDescending(c => c.Combined)
                 .ThenBy(c => c.Name).FirstOrDefault();
            Console.WriteLine("******BMW : Most Fuel Efficient ******");
            Console.WriteLine($"{BMWMostFuelEfficient.Name} :{BMWMostFuelEfficient.Combined}");

        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar()
                    .ToList();
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


