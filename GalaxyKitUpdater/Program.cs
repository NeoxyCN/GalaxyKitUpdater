using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GalaxyKitUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String url = "https://api.github.com/repos/allstars-org/galaxykit/releases/latest";
            Console.WriteLine("GalaxyKitUpdater 1.0.0 Made by Neoxy");
            Console.WriteLine("Loading files...");
            try
            {
                WebClient wc = new WebClient();
                String raw = wc.DownloadString("");

            }catch(Exception error)
            {
                Console.WriteLine("Something went wrong.");
                Console.WriteLine(error.Message.ToString());
            }
        }
    }
}
