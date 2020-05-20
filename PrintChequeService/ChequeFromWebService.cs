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
                    IsValide(data.Cheques); //проверка чеков на валидность
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

        public static void ChequePrinted(object id)
        {
            PrintedCheques.Add((int)id, false);//сохранение id напечатанных чеков
            foreach(int i in PrintedCheques.Keys)
            {
                if (!PrintedCheques[i])
                {
                    try
                    {
                        Console.WriteLine("Отправка запроса на сервер: ");
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{id}");
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Console.WriteLine($"Ответ сервера: {response.StatusCode} - {response.StatusDescription}");
                        if (response.StatusCode == HttpStatusCode.OK)
                            PrintedCheques[i] = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка при отправке данных на сервер: {e.Message}");
                    }
                }
            }
        }

        private static void IsValide(List<Cheque> cheques)
        {
            double result1 = 0, result2 = 0, ndsSumm = 0;
            var temp = new List<Cheque>();
            temp.AddRange(cheques);
            foreach (Cheque c in temp)
            {
                if (c.Email == "" && c.Phone == "")
                {
                    Console.WriteLine($"В чеке {c.ID} отсутствуют телефон и электронная почта!");
                    cheques.Remove(c);
                    continue;
                }
                if(c.Summa == 0)
                {
                    Console.WriteLine($"Итог по чеку {c.ID} равен 0!");
                    cheques.Remove(c);
                    continue;
                }
                if (c.Payment == 0)
                {
                    Console.WriteLine($"В чеке {c.ID} не указан способ оплаты!");
                    cheques.Remove(c);
                    continue;
                }
                foreach (Product p in c.Products)
                {
                    result1 += p.Row_Summ;
                    result2 = p.Price * p.Quantity;
                    if (p.Name == "" || p.Price == 0 || p.Quantity == 0 || p.Row_Type == 0)
                    {
                        Console.WriteLine($"В чеке {c.ID} отсутствует один из атрибутов в позиции с товаром {p.Name}!");
                        cheques.Remove(c);
                        continue;
                    }
                    if (result2 != p.Row_Summ)
                    {
                        Console.WriteLine($"В чеке {c.ID} неверна сумма по позиции с товаром {p.Name}!");
                        cheques.Remove(c);
                        continue;
                    }
                    ndsSumm = Math.Round(p.Price * p.Quantity * p.NDS / (100 + p.NDS), 2);
                    if (ndsSumm != p.NDS_Summ)
                    {
                        Console.WriteLine($"В чеке {c.ID} неверна сумма НДС по позиции с товаром {p.Name}!  {ndsSumm}|{p.NDS_Summ}");
                        cheques.Remove(c);
                        continue;
                    }
                }
                if(result1 != c.Summa)
                {
                    Console.WriteLine($"В чеке {c.ID} неверно указан итог!");
                    cheques.Remove(c);
                    continue;
                }
                result1 = 0;
            }
        }
    }
}
