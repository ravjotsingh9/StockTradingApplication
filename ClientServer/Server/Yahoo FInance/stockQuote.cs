using System;
//using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Yahoo_Finance
{
    public class stockQuote
    {
        private string stockQuery;


        private decimal? lastTradePrice;
        private string name;
        private string m_Fullname;

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
                return stockQuery; 
            }
            set
            {
                stockQuery = value;
               
            }
        }

        public string FullName
        {
            get
            {
                return m_Fullname;
            }
            set
            {
                m_Fullname = value;

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
