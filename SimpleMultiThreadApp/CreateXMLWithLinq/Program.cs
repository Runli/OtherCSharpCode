using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreateXMLWithLinq {
    class Program {
        static void ParseAndLoadExistingXml() {
            // Строим xml из строки 
            string myElement = @"<Car ID = '3'> 
                    <Color> Yellow </Color>
                    <Make>Yugo</Make>
                    </Car>";
            XElement newElement = XElement.Parse(myElement);
            Console.WriteLine(newElement);
            Console.WriteLine();
            newElement.Save("SimpleInventory.xml");
            // Загрузить файл SimpleInventory.xml
            XDocument myDoc = XDocument.Load("SimpleInventory.xml");
            Console.WriteLine(myDoc);
        }
        static void MakeXElementFromArray() {
            // создать анонимный массив анонимных типов
            var people = new[] {
                new {FirstName = "Mandy", Age = 32},
                new {FirstName = "Andrew", Age = 40},
                new {FirstName = "Dave", Age = 41},
                new {FirstName = "Sara", Age = 33}
            };
            XElement peopleDoc = new XElement("People",
                from p in people
                select new XElement("Person", new XAttribute("Age", p.Age),
                       new XElement("FirstName", p.FirstName))
            );
            Console.WriteLine(peopleDoc);
        }
        static void BuildXMLDocWithLILNQToXML() {
            XElement doc = new XElement("Inventory",
                new XElement("Car", new XAttribute("ID", "1000"),
                    new XElement("PetName", "Jimbo"),
                    new XElement("Color", "Red"),
                    new XElement("Make", "Ford")
                    )
                //new XElement("Seller", new XAttribute("ID", "9999"),
                  //  new XElement("Name", "Akos"),
                    //new XElement("Address", "Almet"),
                    //new XElement("Phone", "89823749873")
                    //)
                );
            doc.Save("InventoryWithXMLLINQ.xml");
            
        }
        static void BuildXMLWithLILNQToXMLForCroc() {
            XElement document = new XElement("projects",
                new XElement("project", new XAttribute("name", "xml"),
                    new XElement("member", new XAttribute("role", "developer"), new XAttribute("name", "Fedya")),
                    new XElement("member", new XAttribute("role", "manager"), new XAttribute("name", "Ivan")),
                    new XElement("member", new XAttribute("role", "manager"), new XAttribute("name", "Fedya"))
                    ),
                new XElement("project", new XAttribute("name", "rpc"),
                    new XElement("member", new XAttribute("role", "developer"), new XAttribute("name", "Fedya"))
                ));
            
            document.Save("CrocMembers.xml");

        }
        static void Main(string[] args) {
            BuildXMLWithLILNQToXMLForCroc();
            //BuildXMLDocWithLILNQToXML();
            //MakeXElementFromArray();
            //ParseAndLoadExistingXml();
            //Console.ReadLine();
        }
    }
}
