using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Server.Yahoo_Finance;
using System.IO;
//
namespace Server
{
    public class Stock
    {
        //public string stockname;
        //public float stockprice;

        public class stockInfo
        {
            public string price { get; set; }
            public int shares { get; set; }
        }
        
        // Stock list and price in memory
        //Key:stock' name, value: price, shares
        private Dictionary<string, stockInfo> m_stocksDictionary ;

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


        
        // constructor 
        public Stock()
        {
            stocksDictionary = new Dictionary<string, stockInfo>();
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

        // update the price for all the stocks in the memoery (next step should store in the file)
        public bool updateAllPrice()
        {
      
            foreach (string key in this.stocksDictionary.Keys)
            {
                List<stockQuote> updateQueries = new List<stockQuote>();
                updateQueries.Add(new stockQuote(key));
                getStockData.getData(updateQueries);
                if (key == updateQueries[0].Name)
                {
                    this.stocksDictionary[key].price = updateQueries[0].LastTradePrice.ToString();
                }
                else
                {
                    return false;
                }
                
            }
            
            return true;

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
        public bool writeAllStockData(String FileName){
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FileName))
                {
                    foreach (String key in this.stocksDictionary.Keys)
                    {

                        file.WriteLine("{0} {1} {2}", key, this.stocksDictionary[key].price, this.stocksDictionary[key].shares);
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
        // Need to consider synchronize issue for shared resoures --- "file" and variable"stocksDictionary"
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
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        // get stock's information from the disk
        // only used when server is shut down and restar again
        public bool getStockDataFromFile(String FileName)
        {
            
            try
            {
               
                using (StreamReader reader = new StreamReader(FileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        stockInfo info = new stockInfo();
                        info.price = words[1];
                        info.shares = Convert.ToInt32(words[2]);
                        stocksDictionary.Add(words[0], info);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }




        //check if stock already in the tracing list(stockDictionary)
        //Server can call this function to check
        public bool checkInStockDic(string name)
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

        // decrease shares number in the stocksDictionary (users' stock's share quantity should increase)
        public bool clientBuyShares(string stockName, int quantity)
        {
            if (this.stocksDictionary.ContainsKey(stockName))
            {
                
                if (this.stocksDictionary[stockName].shares < quantity)
                {
                    return false;
                }
                else
                {
                    this.stocksDictionary[stockName].shares -= quantity;
                    return true;
                }
               
            }
            else
            {
                return false;
            }
            
        }

        // increase shares number in the stocksDictionary (users' stock's share quantity should decrease)
        public bool clientSellShares(string stockName, int quantity)
        {
            if (this.stocksDictionary.ContainsKey(stockName))
            {

             
                    this.stocksDictionary[stockName].shares += quantity;
                    return true;

            }
            else
            {
                return false;
            }

        }

    }
}
