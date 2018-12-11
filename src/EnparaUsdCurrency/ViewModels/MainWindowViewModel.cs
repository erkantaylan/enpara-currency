using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using EnparaUsdCurrency.Models;
using HtmlAgilityPack;
using Prism.Mvvm;
using RestSharp;

namespace EnparaUsdCurrency.ViewModels
{

    internal class MainWindowViewModel : BindableBase
    {
        private const string url = "https://www.qnbfinansbank.enpara.com/doviz-kur-bilgileri/doviz-altin-kurlari.aspx";
        private const string buyingXPath = "/html/body/div/a[3]/div/div/div[2]/div[1]/div[2]/table/tr/td/span[1]/dl/dd[2]/div/span";
        private const string sellingXPath = "/html/body/div/a[3]/div/div/div[2]/div[1]/div[2]/table/tr/td/span[1]/dl/dd[1]/div/span";
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private Currency currency;

        public MainWindowViewModel(Currency currency)
        {
            Currency = currency;
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            TimerOnTick(null, null);
        }

        public Currency Currency
        {
            get => currency;
            set => SetProperty(ref currency, value);
        }

        private async void TimerOnTick(object sender, EventArgs eventArgs)
        {
            HtmlDocument doc = new HtmlDocument();
            string html = await GetHtmlAsync();
            doc.LoadHtml(html);
            Currency.Buying = Convert.ToDouble(ClearData(ResolveXpath(doc, buyingXPath)));
            Currency.Selling = Convert.ToDouble(ClearData(ResolveXpath(doc, sellingXPath)));
            Currency.UpdateDate = DateTime.Now.ToString("T");
        }


        private static string ResolveXpath(HtmlDocument doc, string xpath)
        {
            return doc.DocumentNode.SelectSingleNode(xpath).InnerText;
        }

        private Task<string> GetHtmlAsync()
        {
            return Task.Factory.StartNew(GetHtml);
        }

        private static string GetHtml()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            return client.Execute(request).Content;
        }


        /// <summary>
        ///     Sample value is 2.123123 TL
        ///     This method removes " TL" and returnes only number
        /// </summary>
        /// <param name="buying"></param>
        /// <returns></returns>
        private static string ClearData(string buying)
        {
            char separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            return buying.Split(' ')[0].Replace(',', separator);
        }
    }

}