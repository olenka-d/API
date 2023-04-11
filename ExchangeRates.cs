//Використатит одну з безкоштовних апішок (використано публічний API приватбанку)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Security.Policy;

namespace exchange_rate
{
    public class ExchangeRates
    {
        public string baseCurrency { get; set; }  //Базова валюта
        public string currency { get; set; }      //Валюта угоди
        public float saleRateNB { get; set; }     //Курс продажу НБУ
        public float purchaseRateNB { get; set; } //Курс продажу НБУ
        public float saleRate { get; set; }       //Курс продажу ПриватБанку
        public float purchaseRate { get; set; }   //Курс купівлі ПриватБанку
    }
    public class ExchangeRatesMain
    {
        public IList<ExchangeRates> exchangeRate { get; set; }
        public string date { get; set; }
        public string bank { get; set; }
        public int baseCurrency { get; set; }
        public string baseCurrencyLit { get; set; }
    }
    class Program 
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "https://api.privatbank.ua/p24api/exchange_rates?json=true&date=";
        async static Task Main(string[] args)
        {
            await getExchangeRates();
            //Console.WriteLine("Hello, World!");

            async Task getExchangeRates()
            {
                DateTime currentDate = DateTime.Today;
                Console.Write("Input date 'dd.mm.yyyy'(skip - today date): ");
                string date = Console.ReadLine();
                date = (date == "") ? currentDate.ToString("dd.MM.yyyy") : date;
                url += date;

                Console.Write("Input currency (USD,EUR,PLZ,GBP)(skip - all currency): ");
                string currency_need = Console.ReadLine();
                currency_need = (currency_need == "") ? "all" : currency_need;

                var responseString = await client.GetStringAsync(url);
                //Console.WriteLine(responseString);
                Console.WriteLine("Hello");
                ExchangeRatesMain exchangeRatesMain = JsonSerializer.Deserialize<ExchangeRatesMain>(responseString);
                Console.WriteLine($"bank: {exchangeRatesMain.bank}");
                Console.WriteLine($"date: {exchangeRatesMain.date}");
                //Console.WriteLine($"baseCurrencyLit: {exchangeRatesMain.baseCurrencyLit}");
                //Console.WriteLine($"list: {exchangeRatesMain.exchangeRate.Count}");

                foreach (var currency in exchangeRatesMain.exchangeRate)
                {
                    if (currency_need == "all" || currency.currency == currency_need)
                    {
                        Console.Write($"baseCurrency: {currency.baseCurrency}\t");
                        Console.Write($"currency: {currency.currency}");
                        Console.Write($"\tsaleRateNB: {currency.saleRateNB}\t");
                        //Console.Write($"\tpurchaseRateNB: {currency?.purchaseRateNB}\t");
                        Console.Write($"\tsaleRate: {currency.saleRate}\t");
                        Console.Write($"purchaseRate: {currency.purchaseRate}");
                        Console.WriteLine();
                    }                
                }
                Console.ReadKey();
            }
        }
    }
}

