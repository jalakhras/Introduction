﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            //Note Combined=> Fuel efficiency
            var cars = ProcessFile("fuel.csv");
            var query = cars.OrderByDescending(c => c.Combined)
                .ThenBy(c=>c.Name);
            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} :{car.Combined}");
            }
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(Car.ParseFromCsv)
                    .ToList();
        }
    }
}
