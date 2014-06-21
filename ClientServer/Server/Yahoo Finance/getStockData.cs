using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Server.Yahoo_Finance
{
    class getStockData
    {
        private const string originURL = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        public static void getData(List<stockQuote> quotes)
        {
            string query = String.Join("%2C", quotes.Select(s => "%22" + s.StockQuery + "%22").ToArray());
            string url = string.Format(originURL, query);
            XDocument resultXmlDoc = XDocument.Load(url);
            xmlProcess(quotes, resultXmlDoc);
           
        }

        private static void xmlProcess(List<stockQuote> quotes, XDocument resultXmlDoc)
        {
            XElement results = resultXmlDoc.Root.Element("results");

            foreach (stockQuote quote in quotes)
            {
                XElement q = results.Elements("quote").First(s => s.Attribute("symbol").Value == quote.StockQuery);
                
                quote.Name = q.Element("Name").Value;
                quote.LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value);
                
            }
          
        }

        private static decimal? GetDecimal(string data)
        {
            decimal value;
            
            if (data == null)
            {
                return null;
            }

            data = data.Replace("%", "");
            bool success = Decimal.TryParse(data, out value);

            if (success)
            {
                return value;
            }
            
            return null;
        }

    }
}
