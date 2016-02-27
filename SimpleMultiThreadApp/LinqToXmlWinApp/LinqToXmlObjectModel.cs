using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;

namespace LinqToXmlWinApp {
    class LinqToXmlObjectModel {
        private static List<string> makes = new List<string>();
        public static XDocument GetXmlInventory() {
            try {
                XDocument inventoryDoc = XDocument.Load("Inventory.xml");
                return inventoryDoc;
            } catch (System.IO.FileNotFoundException ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static void InsertNewElement(string make, string color, string petName){
            // Загрузить текущий документ
            XDocument inventoryDoc = XDocument.Load("Inventory.xml");

            // Сгенерировать случайное число для идентификатора
            Random random = new Random();

            XElement newElement = new XElement("Car", new XAttribute("ID", random.Next(50000)),
                new XElement("Color", color),
                new XElement("Make", make),
                new XElement("PetName", petName));
            // Добавить к объекту XDocument в памяти
            inventoryDoc.Descendants("Inventory").First().Add(newElement);
            
            // Добавить производителя в массив производителей
            AddMakeToArray(make);

            //Сщхранить изменения на диске
            inventoryDoc.Save("Inventory.xml");
        }

        private static void AddMakeToArray(string make) {
            makes.Add(make);
            //cbxMakeToLookUp.AddRange(makes);
        }

        public static void LookUpColorsForMake(string make) {
            // Загрузить текущий документ
            XDocument inventoryDoc = XDocument.Load("Inventory.xml");

            // Найти цвета заданного изготовителя
            var makeInfo = from car in inventoryDoc.Descendants("Car")
                           where (string)car.Element("Make") == make
                           select car.Element("Color").Value;

            //Построить строку представляющую каждый цвет
            string data = string.Empty;
            foreach (var item in makeInfo.Distinct()){
		        data += string.Format("- {0}\n", item);
	        }
            // Показать цвет
            MessageBox.Show(data, string.Format("{0} colors:", make));
        }
    }
}
