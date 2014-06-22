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

        public class stockInfo
        {
            public string price { get; set; }
            public int shares { get; set; }
        }
        
        // Stock list and price in memory
        //Key:stock' name, value: price, shares
        private Dictionary<string, stockInfo> m_stocksDictionary = new Dictionary<string, stockInfo>();

        public Dictionary<string, stockInfo> stocksDictionary
        {
            get{
                return m_stocksDictionary;
               }
            set
            {
                m_stocksDictionary = value;
            }
        }

        // file's name for stock's information stores in the Disk
        private string fileStock;

        // file's name for users' information stores in the Disk
        private string fileusers;

        // querys to update all the stock's price
        private List<stockQuote> updatequerys { get; set; }
        
        // constructor 
        public Stock()
        {

        }

        ///You can use this function to test the stock value
        ///stockIdentifier : stock's name
        ///For example, Apple should input as AAPL
        ///You can see the right stock name from here:
        ///https://finance.yahoo.com/q?s=AAPL
        // string val = getPriceFromYahoo("AAPL");
        public string getPriceFromYahoo(string stockIdentifier)
        {
            string price = "";
            List<stockQuote> queryPrice = new List<stockQuote>();
            queryPrice.Add(new stockQuote(stockIdentifier));
            if(getStockData.getData(queryPrice))
            {
                stockQuote value =  queryPrice.ElementAt(0);
                price = value.LastTradePrice.ToString();
                return price;
            }

            return null;

        }

        // update the price for all the stocks
        // update information should first write into the disk
        // stocksDictionary should use getStockDataFromFile to update info
        public bool updateAllPrice(Dictionary<string, string> stocksDic)
        {
            bool success = false;
            List<stockQuote> updateQueries = new List<stockQuote>();
            foreach (string key in stocksDic.Keys)
            {
                updateQueries.Add(new stockQuote(key));
            }
            getStockData.getData(updateQueries);

            // write into the file
            writeAllStockData(fileStock, stocksDictionary);
            
            return success;

        }

       // add stock into the dictionary with initail shares 1000 and up-to-date price from Yahoo.
       // This function is private. Use writeNewStockData() to both add new stock in memory and file
        private bool addToTheStockList(string stockName)
        {
            stockInfo info = new stockInfo();
            info.price = getPriceFromYahoo(stockName);
            if (info.price != null)
            {
                info.shares = 1000;
                this.m_stocksDictionary.Add(stockName, info);
                return true;
            }
            return false;
        }

        // wirte stock's information into the disk
        public bool writeAllStockData(String FileName, Dictionary<string, stockInfo> dictionary){
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FileName, true))
                {
                    foreach (String key in dictionary.Keys)
                    {

                        file.WriteLine("{0} {1} {2}", key, dictionary[key].price, dictionary[key].shares);
                    }

                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        // write new data into the file when client first queries the stcok
        public bool writeNewStockData(String FileName, String stockName)
        {
            try
            {
                if (addToTheStockList(stockName))
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FileName, true))
                    {
                        file.WriteLine("{0} {1} {2}", stockName, stocksDictionary[stockName].price, stocksDictionary[stockName].shares);

                    }
                }else{
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        // get stock's information from the disk
        public bool getStockDataFromFile(String FileName)
        {
            bool success = false;
            return success;
        }




        //check if stock already in the tracing list
        //Server can call this function to check
        public bool checkInStockList(string name)
        {
            return stocksDictionary.ContainsKey(name);
        }

        //check if the user enter the correct stock name
        //UI can call this function to display error to the user
        public bool validStockName(string name)
        {

            if (getPriceFromYahoo(name) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
