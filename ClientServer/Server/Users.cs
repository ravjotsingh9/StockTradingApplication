﻿using System;
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
            public string cashBalance { get; set; }
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

        // get users' information from the disk
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
                    UserDictionary[userName] += amount;
                    return true;
                }
                else
                {
                    if (UserDictionary[userName] >= amount)
                    {
                        UserDictionary[userName] -= amount;
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
               
                UserDictionary.Add(name, 1000);
                return true;
            }
            else{
                return false;
            }
            
        }

       // add new stock and shares for a user
        public bool addStcokForUser(string userName, string stocKName, int shares)
        {
            try
            {
                var key = new Tuple<string, string>(userName, stocKName);
                UserStockDictionary.Add(key, shares);
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
            var key = new Tuple<string, string>(userName, stocKName);
            try
            {
                
                if (UserStockDictionary.ContainsKey(key))
                {
                    if (addORdecrese)
                    {
                         UserStockDictionary[key] -=
                    }
                    else
                    {
                       
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
