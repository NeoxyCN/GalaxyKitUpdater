using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebClient wc = new WebClient();
            string raw = wc.DownloadString("https://allstars-org.github.io/galaxykit-update.json");
            Console.WriteLine("raw:" + raw);
            Console.ReadLine();
        }
    }
}
