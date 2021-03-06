﻿using System;
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

        public static bool getData(List<stockQuote> quotes)
        {
          
            string query = String.Join("%2C", quotes.Select(s => "%22" + s.StockQuery + "%22").ToArray());
            string url = string.Format(originURL, query);
            XDocument resultXmlDoc = XDocument.Load(url);

            if (xmlProcess(quotes, resultXmlDoc))
            {
                return true;
            }
            else{
                return false;
            }
            
        }

        private static bool xmlProcess(List<stockQuote> quotes, XDocument resultXmlDoc)
        {
            XElement results = resultXmlDoc.Root.Element("results");
          
            foreach (stockQuote quote in quotes)
            {
                try
                {
                    XElement q = results.Elements("quote").First(s => s.Attribute("symbol").Value == quote.StockQuery);

                    quote.FullName = q.Element("Name").Value;
                    quote.LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value);
                }
                catch(Exception e)
                {
                    return false;
                }
               
                
            }
            return true;
  
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
