using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronWebScraper;
using System.IO;


    public static class UserAgentsScraper
    {
       public static void Scrape(string filePath)
        {
            var getPage = new Myscraper();
            getPage.Start();
            getPage.WritetoCSV(filePath);
               
        }
    }

    public class Myscraper : WebScraper
    {
        public List<string> UserAgents = new List<string>();

        public override void Init()
        {
            this.Request("https://techblog.willshouse.com/2012/01/03/most-common-user-agents/", Parse);
        }

        public override void Parse(Response response)
        {
            var UserAgentsNodes = response.QuerySelectorAll("#post-2229 > div.entry-content > table > tbody > * > td.useragent");
            foreach (var node in UserAgentsNodes)
            {
                UserAgents.Add(node.InnerHtml);
            }
        }

        public void WritetoCSV(string filePath)
        {
            var lastchar = filePath.LastIndexOf('\\');
            var dirPath = filePath.Remove(lastchar);

            Directory.CreateDirectory(dirPath);
        
            File.Delete(filePath);

            using (StreamWriter writer = File.AppendText(filePath))
            {                
                foreach (var useragent in UserAgents)
                {
                    writer.WriteLine(useragent);
                }
            }
            

        }

    }

 
