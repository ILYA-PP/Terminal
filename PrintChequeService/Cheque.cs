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
        public Cheque(int id, string p, string e, double s, int pay)
        {
            ID = id;
            Phone = p;
            Email = e;
            Summa = s;
            Payment = pay;
            Products = new List<Product>();
        }
    }
}
