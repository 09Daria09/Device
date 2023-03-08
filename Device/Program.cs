using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device
{
    internal class Program
    {
        public class DEvice
        {
            public string Name { get; set; }
            public string Manufacturer { get; set; }
            public double Price { get; set; }

            public DEvice()
            {
                Name = null;
                Manufacturer = null;
                Price = 0;
            }
            public DEvice(string name, string manufacturer, double price)
            {
                Name = name;
                Manufacturer = manufacturer;
                Price = price;
            }
        }
        public class Sort : IEqualityComparer<DEvice>
        {
            public bool Equals(DEvice x, DEvice y)
            {
                if (x.Manufacturer == y.Manufacturer)
                {
                    return true;
                }
                return false;
            }
            public int GetHashCode(DEvice obj)
            {
                return obj.Manufacturer.GetHashCode();
            }
        }
        static void Main(string[] args)
        {
            List<DEvice> Devices = new List<DEvice>{
    new DEvice("Laptop", "Dell", 1500.00),
    new DEvice("Smartphone", "Samsung", 800.00),
    new DEvice("Tablet", "Apple", 1000.00),
    new DEvice("Desktop", "HP", 1200.00),
    new DEvice("Smartwatch", "Fitbit", 200.00),
    new DEvice("Smart speaker", "Amazon", 100.00)};

            List<DEvice> Devices1 = new List<DEvice>{
    new DEvice("Smart TV", "Samsung", 1200.00),
    new DEvice("Gaming laptop", "Asus", 2000.00),
    new DEvice("Gaming console", "Sony", 500.00),
    new DEvice("E-book reader", "Kobo", 150.00),
    new DEvice("Virtual assistant", "Google", 50.00),
    new DEvice("Wireless earbuds", "Apple", 250.00)};

            Console.WriteLine("\t*** Разница ***");
            var difference = Devices.Except(Devices1, new Sort());
            foreach (var device in difference)
            {
                Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            }
            Console.WriteLine();
            Console.WriteLine("\t*** Обьединение ***");
            var union = Devices.Union(Devices1, new Sort());
            foreach (var device in union)
            {
                Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            }
            Console.WriteLine();
            Console.WriteLine("\t*** Пересечение ***");
            var intersection = Devices.Intersect(Devices1, new Sort());
            foreach (var device in intersection)
            {
                Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            }
        }
    }
}
