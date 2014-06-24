using System;
using System.Collections.Generic;
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
            public Dictionary<string, int> stockShares { get; set; }
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
            bool success = false;
            return success;
        }

        // wirte users information into the disk
        private bool writeUserDataInFile(String FileName, List<Users> data)
        {
            bool success = false;
            return success;
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
                userInfo temp = new userInfo();
                temp.stockShares[stocKName] = shares;
                this.UserDictionary.Add(userName, temp);
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
                        UserDictionary[userName].stockShares[stocKName] += shares;
                    }else
                    {
                        UserDictionary[userName].stockShares[stocKName] -= shares;
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
