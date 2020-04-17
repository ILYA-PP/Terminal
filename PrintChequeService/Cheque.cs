using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintChequeService
{
    class Cheque
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Summa { get; set; }
        public int Payment { get; set; }
        public List<Product> Products { get; set; }

        public Cheque(string phone, string email)
        {
            Phone = phone;
            Email = email;
            Products = new List<Product>();
        }
    }
}
