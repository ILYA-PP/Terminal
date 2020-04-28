using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PrintChequeService
{
    public class Cheque
    {
        [XmlAttribute("id")]
        public int ID { get; set; }
        [XmlAttribute("tel")]
        public string Phone { get; set; }
        [XmlAttribute("email")]
        public string Email { get; set; }
        [XmlAttribute("summa")]
        public double Summa { get; set; }
        [XmlAttribute("payment")]
        public int Payment { get; set; }
        [XmlElement("good")]
        public Product[] Products { get; set; }
        //public Cheque(int id, string p, string e, double s, int pay)
        //{
        //    ID = id;
        //    Phone = p;
        //    Email = e;
        //    Summa = s;
        //    Payment = pay;
        //    Products = new List<Product>();
        //}
    }
}
