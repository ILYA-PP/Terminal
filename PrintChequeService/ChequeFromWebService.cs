using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace PrintChequeService
{
    class ChequeFromWebService
    {
        private static Dictionary<int, bool> PrintedCheques = new Dictionary<int, bool>();
        private static List<int> NotMarkedCheques = new List<int>();
        public static List<Cheque> GetCheque()
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
                        if (PrintedCheques.ContainsKey(c.ID))
                            data.Cheques.Remove(c);
                    Console.WriteLine($"Ответ сервера: {response.StatusCode} - {response.StatusDescription} " +
                        $"| Чеков получено: {data.Cheques.Count}");
                }
            }
            catch (Exception e)
            { 
                Console.Write($"Ошибка при получении данных с сервера: {e.Message} ");
                data.Cheques = null;
            }
            return data.Cheques;
        }

        public static void ChequePrinted(int id)
        {
            try
            {
                PrintedCheques.Add(id, false);//сохранение id напечатанных чеков
                foreach(int i in PrintedCheques.Keys)
                {
                    if (!PrintedCheques[i])
                    {
                        Console.WriteLine("Отправка запроса на сервер: ");
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{id}");
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Console.WriteLine($"Ответ сервера: {response.StatusCode} - {response.StatusDescription} ");
                        if (response.StatusCode == HttpStatusCode.OK)
                            PrintedCheques[i] = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при отправке данных на сервер: {e.Message}");
            }
        }
    }
}
