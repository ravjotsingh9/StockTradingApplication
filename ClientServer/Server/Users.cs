using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Server
{
    public class Users
    {

        
        
        //Dictionary<string, int> stockShares stores stock's name and shares
        public class userInfo
        {
            public double cashBalance { get; set; }
            private Dictionary<string, int> m_stockShares = new Dictionary<string, int>();
            public Dictionary<string, int> StockShares
            {
                get
                {
                    return m_stockShares;
                }
                set
                {
                    m_stockShares = value;
                }
            }
        }

        private Dictionary<string, userInfo> m_userDictionary;
        public Dictionary<string, userInfo> UserDictionary

        {
            get{
                return m_userDictionary;
            }
            set{
                m_userDictionary = value;
            }
        }

        public Users()
        {
        

            UserDictionary = new Dictionary<string, userInfo>();
        }


        


        // file's name for users' information stores in the Disk
        private string fileusers;

        // get users' information from the disk when server restart
        public bool getUserDataFromFile(String FileName)
        {
            try
            {

                using (StreamReader reader = new StreamReader(FileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        string key = words[0];
                        double balance = Convert.ToDouble(words[1]);
                        userInfo info = new userInfo();
                        info.cashBalance = balance;
                        string line2;
                        while((line2 = reader.ReadLine()) != "-----"){
                            string[] stocksList = line2.Split(' ');
                            
                            info.StockShares.Add(stocksList[0], Convert.ToInt32(stocksList[1]));
                          
                        }
                       
                        this.UserDictionary.Add(key, info);
                      
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

        // wirte all users information into the disk
        public bool writeAllUserData(String FileName)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FileName))
                {
                    foreach (string key in this.UserDictionary.Keys)
                    {

                        file.WriteLine("{0} {1}", key, this.UserDictionary[key].cashBalance);
                        foreach (string key2 in this.UserDictionary[key].StockShares.Keys)
                        {
                            file.WriteLine("{0} {1}", key2, this.UserDictionary[key].StockShares[key2]);
                        }
                        
                        file.WriteLine("-----");
                    }

                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        // append single user's information into the disk
        public bool writeSingleUserData(string FileName, string userName)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@FileName, true))
                {


                    file.WriteLine("{0} {1}", userName, this.UserDictionary[userName].cashBalance);
                    foreach (string key2 in this.UserDictionary[userName].StockShares.Keys)
                        {
                            file.WriteLine("{0} {1}", key2, this.UserDictionary[userName].StockShares[key2]);
                        }

                        file.WriteLine("-----");
    

                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        // Modify user's cash balance, addORminus: false:decrease balance, true:increase balance
        public bool modifyCash(string userName, double amount, bool addORminus)
        {
            if (UserDictionary.ContainsKey(userName))
            {
                if (addORminus)
                {
                    UserDictionary[userName].cashBalance += amount;
                    return true;
                }
                else
                {
                    if (UserDictionary[userName].cashBalance >= amount)
                    {
                        UserDictionary[userName].cashBalance -= amount;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
  
                }

                
            }
            else
            {
                return false;
            }

            
        }


        // add user to the UserDictionary
        public bool addUser(string name)
        {
            if (!UserDictionary.ContainsKey(name))
            {
                userInfo tempInfo = new userInfo();
                tempInfo.cashBalance = 1000;
                UserDictionary.Add(name, tempInfo);
                return true;
            }
            else{
                return false;
            }
            
        }

        public bool ifUserExist(string name)
        {
            if (UserDictionary.ContainsKey(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       // add new stock and shares for a user
        public bool addStcokForUser(string userName, string stocKName, int shares)
        {
            try
            {
                userInfo temp = this.UserDictionary[userName];
                temp.StockShares.Add(stocKName, shares);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Modify user's exist stock's shares, addORminus: false:decrease , true:increase
        public bool modifyShares(string userName, string stocKName, int shares, bool addORdecrese)
        {
            
            try
            {
                
                if (this.UserDictionary.ContainsKey(userName))
                {
                    if (addORdecrese)
                    {
                        UserDictionary[userName].StockShares[stocKName] += shares;
                    }else
                    {
                        UserDictionary[userName].StockShares[stocKName] -= shares;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
              
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
    }
}
