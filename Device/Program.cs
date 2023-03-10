using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Device.Program;

namespace Device
{
    public class Program
    {
        [Serializable]
        [DataContract]
        public class DEvice
        {
           [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Manufacturer { get; set; }
            [DataMember]
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

            char answer;
            FileStream stream = null;
            BinaryFormatter formatter = null;
            XmlSerializer serializer = null;
            string s = String.Empty;
            

            stream = new FileStream("list.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(List<DEvice>));
            serializer.Serialize(stream, Devices);
            stream.Close();
            Console.WriteLine("Сериализация успешно выполнена!");

            stream = new FileStream("list.xml", FileMode.Open);
            serializer = new XmlSerializer(typeof(List<DEvice>));
            Devices = (List<DEvice>)serializer.Deserialize(stream);
            foreach (DEvice j in Devices)
            {
                Console.WriteLine($"{j.Name} ({j.Manufacturer}): ${j.Price}");
            }
            //Console.WriteLine(s);
            stream.Close();

            using (stream = new FileStream("list.json", FileMode.Create))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<DEvice>));
                jsonFormatter.WriteObject(stream, Devices1);
                Console.WriteLine("Сериализация выполнена успешно!");
            }

            // Читаем сериализованный список из файла и выводим информацию о каждом устройстве
            using (stream = new FileStream("list.json", FileMode.Open))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<DEvice>));
                List<DEvice> deserializedDevices = (List<DEvice>)jsonFormatter.ReadObject(stream);
                foreach (DEvice device in deserializedDevices)
                {
                    Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
                }
                Console.WriteLine("Десериализация выполнена успешно!");
            }


            //Console.WriteLine("\t*** Разница ***");
            //var difference = Devices.Except(Devices1, new Sort());
            //foreach (var device in difference)
            //{
            //    Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            //}
            //Console.WriteLine();
            //Console.WriteLine("\t*** Обьединение ***");
            //var union = Devices.Union(Devices1, new Sort());
            //foreach (var device in union)
            //{
            //    Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            //}
            //Console.WriteLine();
            //Console.WriteLine("\t*** Пересечение ***");
            //var intersection = Devices.Intersect(Devices1, new Sort());
            //foreach (var device in intersection)
            //{
            //    Console.WriteLine($"{device.Name} ({device.Manufacturer}): ${device.Price}");
            //}
        }
    }
}
