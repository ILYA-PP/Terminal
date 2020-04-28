using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PrintChequeService
{
    class ChequeFromWebService
    {
        private static List<int> PrintedCheques = new List<int>();
        public static List<Cheque> GetXml()
        {
            bills data = null;
            try
            {
                Console.WriteLine("Отправка запроса на сервер: http://sert.godovalov.ru/webreport/execsp?spName=megamakler.dbo.api_bill_print&spOut=result");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://sert.godovalov.ru/webreport/execsp?spName=megamakler.dbo.api_bill_print&spOut=result");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    data = (bills)new XmlSerializer(typeof(bills)).Deserialize(stream);
                    List<Cheque> temp = new List<Cheque>();
                    temp.AddRange(data.Cheques);
                    foreach (Cheque c in temp)
                        if (PrintedCheques.Contains(c.ID))
                            data.Cheques.Remove(c);
                        else
                            PrintedCheques.Add(c.ID);
                    Console.WriteLine($"Ответ сервера: {response.StatusCode} - {response.StatusDescription} " +
                        $"| Чеков получено: {data.Cheques.Count}");
                }
            }
            catch (Exception e)
            { 
                Console.Write(e.Message+" ");
                data.Cheques = null;
            }
            return data.Cheques;
        }
    }
}
