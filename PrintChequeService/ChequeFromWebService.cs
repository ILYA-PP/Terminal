using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace PrintChequeService
{
    class ChequeFromWebService
    {
        private static List<int> PrintedCheques = new List<int>();
        private static string GetXml()
        {
            string data = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://sert.godovalov.ru/webreport/execsp?spName=megamakler.dbo.api_bill_print&spOut=result");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if(response.StatusCode == HttpStatusCode.OK)
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
                        int id = 0, payment = 0;
                        string phone = "", email = "";
                        double summa = 0;
                        if (b.Attribute("id") != null)
                            id = int.Parse(b.Attribute("id").Value);
                        if (b.Attribute("tel") != null)
                            phone = b.Attribute("tel").Value;
                        if (b.Attribute("email") != null)
                            email = b.Attribute("email").Value;
                        if (b.Attribute("summa") != null)
                            summa = double.Parse(b.Attribute("summa").Value);
                        if (b.Attribute("payment") != null)
                            payment = int.Parse(b.Attribute("payment").Value);

                        cheque = new Cheque(id, phone, email, summa, payment);
                        foreach (var p in b.Elements())
                        {
                            string name = "";
                            double price = 0, nds = 0, ndsS = 0, rowS = 0;
                            int qnt = 0, rowT = 0;
                            if (p.Attribute("name") != null)
                                name = p.Attribute("name").Value;
                            if (p.Attribute("price") != null)
                                price = double.Parse(p.Attribute("price").Value);
                            if (p.Attribute("qnt") != null)
                                qnt = int.Parse(p.Attribute("qnt").Value);
                            if (p.Attribute("nds") != null)
                                nds = double.Parse(p.Attribute("nds").Value);
                            if (p.Attribute("nds_summ") != null)
                                ndsS = double.Parse(p.Attribute("nds_summ").Value);
                            if (p.Attribute("row_type") != null)
                                rowT = int.Parse(p.Attribute("row_type").Value);
                            if (p.Attribute("row_summ") != null)
                                rowS = double.Parse(p.Attribute("row_summ").Value);
                            cheque.Products.Add(new Product()
                            {
                                Name = name,
                                Price = price,
                                Quantity = qnt,
                                NDS = nds,
                                NDS_Summ = ndsS,
                                Row_Type = rowT,
                                Row_Summ = rowS
                            });
                        }
                        if (!PrintedCheques.Contains(cheque.ID))
                        {
                            PrintedCheques.Add(cheque.ID);
                            cheques.Add(cheque);
                        }
                    }
                }
                catch(Exception e) { Console.WriteLine(e.Message); }
            }
            return cheques;
        }
    }
}
