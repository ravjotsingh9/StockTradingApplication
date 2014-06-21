using System;
//using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Yahoo_Finance
{
    class stockQuote
    {
        private string stockQuery;


        private decimal? lastTradePrice;
        private string name;


        public stockQuote(string nameOfStock)
        {
            stockQuery = nameOfStock;
        }

        public string StockQuery
        {
            get 
            { 
                return stockQuery; 
            }
            set
            {
                stockQuery = value;

            }
        }


        public string Name
        {
            get 
            { 
                return name; 
            }
            set
            {
                name = value;
               
            }
        }

        public decimal? LastTradePrice
        {
            get 
            { 
                return lastTradePrice; 
            }
            set
            {
                lastTradePrice = value;    
            }
        }




    }
}
