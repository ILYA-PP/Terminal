using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PrintChequeService
{
    public class Cheque
    {
        private string phone;
        [XmlAttribute("id")]
        public int ID { get; set; }
        [XmlAttribute("tel")]
        public string Phone 
        {
            get
            {
                if (phone[0] == '8')
                    phone = "+7" + phone.Remove(0, 1);
                return phone;
            }
            set { phone = value; }
        }
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
