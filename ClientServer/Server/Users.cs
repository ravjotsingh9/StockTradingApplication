using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Users
    {
        //Dictionary<string, int> stockShares stores stock's name and shares
        public class userInfo{
            public string cashBalance;
            public Dictionary<string, int> stockShares;
        }

        private Dictionary<string, userInfo> m_userDictionary = new Dictionary<string, userInfo>();


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
