using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace TerminalApp.Models
{
    class ChequeFromWebService
    {
        private static string GetXml()
        {
            string data = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://sert.godovalov.ru/webreport/execsp?spName=megamakler.dbo.api_bill_print&spOut=result");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        data = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
                data = "";
            }
            return data;
        }
        public static List<Cheque> GetCheque()
        {
            List<Cheque> cheques = new List<Cheque>();
            Cheque cheque;
            string xml = GetXml();
            if (xml != "")
            {
                try
                {
                    var elem = XElement.Parse(xml);
                    foreach (var b in elem.Elements())
                    {
                        cheque = new Cheque(b.Attribute("tel").Value, elem.Element("bill").Attribute("email").Value);
                        cheques.Add(cheque);
                        foreach (var p in b.Elements())
                        {
                            cheque.Products.Add(new Product()
                            {
                                Name = p.Attribute("name").Value,
                                Price = double.Parse(p.Attribute("price").Value),
                                Quantity = int.Parse(p.Attribute("qnt").Value),
                                NDS = double.Parse(p.Attribute("nds").Value),
                                NDS_Summ = double.Parse(p.Attribute("nds_summ").Value),
                                Row_Type = int.Parse(p.Attribute("row_type").Value),
                                Row_Summ = double.Parse(p.Attribute("row_summ").Value)
                            });
                        }
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            return cheques;
        }
    }
}
