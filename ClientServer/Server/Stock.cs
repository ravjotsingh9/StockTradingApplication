using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Server.Yahoo_Finance;
//
namespace Server
{
    public class Stock
    {
        public string stockname;
        public float stockprice;

        private List<stockQuote> querys { get; set; }

        public Stock()
        {

        }

        ///You can use this function to test the stock value
        ///stockIdentifier : stock's name
        ///For example, Apple should input as AAPL
        ///You can see the right stock name from here:
        ///https://finance.yahoo.com/q?s=AAPL
        // string val = getPriceFromYahoo("AAPL");
        public String getPriceFromYahoo(String stockIdentifier)
        {
            string price = "";
            List<stockQuote> queryPrice = new List<stockQuote>();
            queryPrice.Add(new stockQuote(stockIdentifier));
            getStockData.getData(queryPrice);
            stockQuote value =  queryPrice.ElementAt(0);
            price = value.LastTradePrice.ToString();
            return price;

        }

        // update the price for all the stocks
        private void updateAllPrice(List<stockQuote> updateList)
        {

        }

    }
}
