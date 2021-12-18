using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RentWeb2.Shared
{

   public class ConnectAPI
    {

        //public static string apiUri()
        //{
        //    //return (Environment.MachineName.ToUpper() == "SERVER") ? "http://server:8078" : "http://localhost:21475";
        //    // return "http://server:8078";
        //   //  return "http://192.168.100.12:8078";
        //    return "http://localhost:21475";
        //}

        //VPN
         //public static string apiUri = "http://server:8078";
        //LAN Krok
        // public static string apiUri = "http://192.168.100.12:8078";

        //Dev
        // public static string apiUri = "http://localhost:21475";

    
        // Server API2 Core
       
      //  public static string apiUri = "https://server:44307";

        public static string apiUri = "https://localhost:5001";

        public static string reportUri = "https://server:44309";

    }
    
}
