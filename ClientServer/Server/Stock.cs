using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Server.Yahoo_Finance;
//
namespace Server
{
    class Stock
    {
        public string stockname;
        public float stockprice;

        private ObservableCollection<stockQuote> query { get; set; }


        ///You can use this function to test the stock value
        ///stockIdentifier : stock's name
        ///For example, Apple should input as AAPL
        ///You can see the right stock name from here:
        ///https://finance.yahoo.com/q?s=AAPL
        // string val = getPriceFromYahoo("AAPL");
        private String getPriceFromYahoo(String stockIdentifier)
        {
            string price = "";
            query = new ObservableCollection<stockQuote>();
            query.Add(new stockQuote(stockIdentifier));
            stockQuote rr = getStockData.getData(query);
            price = rr.LastTradePrice.ToString();
            return price;

        }
    }
}
