using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GalaxyKitUpdater
{
    public static class definition
    {
        public static string gk_update = "https://api.github.com/repos/allstars-org/GalaxyKitUpdater/releases/latest";
        public static string gk_url = "https://allstars-org.github.io/galaxykit-update.json";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            Console.WriteLine("GalaxyKitUpdater 1.0.0 Made by Neoxy");
            Console.WriteLine("Loading files...");
            CheckKitUpdate();

        }

        static void CheckKitUpdate() {
            string json=LoadWeb(definition.gk_url);
            JObject jsonObject = JObject.Parse(@json);
            string tagName=(string)jsonObject["tag_name"];
            string id= (string)jsonObject["id"];
            string downloadurl=(string)jsonObject["assets"]["browser_download_url"];
            string date= (string)jsonObject["published_at"];
            Console.WriteLine(jsonObject.ToString());
            Console.WriteLine("tag_name:" + tagName+" id:" + id +" url:"+ downloadurl+" date:"+date);
            int newid=Convert.ToInt16(id);
            int nowversion = 0;
            string nowversion_s = "";
            string nowtagName_s = "";
            try
            {
                nowversion_s = File.ReadAllText(@".\galaxykit.version");
                nowtagName_s = File.ReadAllText(@".\galaxykit.tagName");

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in reading old files.");
                Console.WriteLine(e.Message.ToString());
            }

            nowversion = Convert.ToInt16(nowversion_s);
            if (nowversion < newid)
            {
                //先删除
                try
                {
                    System.IO.File.Delete(".\\plugins\\galaxykit-" + nowtagName_s);
                }catch(Exception e)
                {
                    Console.WriteLine("Something went wrong in deleting old tagName.");
                    Console.WriteLine(e.Message.ToString());
                }
                //下载
                DownFile(downloadurl, "galaxykit-"+tagName);
                using (StreamWriter sw = new StreamWriter("galaxykit.version"))
                {
                    sw.WriteLine(id);
                }
                using (StreamWriter sw = new StreamWriter("galaxykit.tagName"))
                {
                    sw.WriteLine(tagName);
                }
                Console.WriteLine("Update finished, good luck.");
            }
        }

        static string LoadWeb(string url)
        {
            string raw="";
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                WebClient wc = new WebClient();
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.81 Safari/537.36 Edg/104.0.1293.54");
                wc.Headers.Add("Accept", "*/*");
                wc.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                wc.Headers.Add("Connection", "keep-alive");
                Console.WriteLine("url:" + url);
                raw = wc.DownloadString(url);
                //json的锅 
                Console.WriteLine("raw:"+raw);

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in LoadWeb.");
                Console.WriteLine(e.Message.ToString());
            }
            return raw;
        }
        static void DownFile(string url,string name)
        {
            var save = "./plugins/"+name;
            using (var web = new WebClient())
            {
                web.DownloadFile(url, @save);
            }
        }
    }
}
