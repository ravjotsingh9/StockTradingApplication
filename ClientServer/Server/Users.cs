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
            public string cashBalance { get; set; }
            public Dictionary<string, int> stockShares { get; set; }
            public userInfo()
            {
                stockShares = new Dictionary<string, int>();
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

    }
}
