using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeDataFromBitcoin_Tidings
{
    class Program
    {
        static void Main(string[] args)
        {
            string html = string.Empty;

            // Create a request for the URL.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
              "https://bitcoin-tidings.com/");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            request.UserAgent = "Mozilla/5.0 (Android 4.4; Tablet; rv:41.0) Gecko/41.0 Firefox/41.0";
            // Get the response.
            var response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                html = reader.ReadToEnd();
                // Display the content.
               // Console.WriteLine(html);
            }

            // Close the response.
            response.Close();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            List<string> tags = new List<string>();

            IEnumerable<HtmlNode> anchors = doc.DocumentNode.SelectNodes("//a").Cast<HtmlNode>();
            foreach (HtmlNode cell in anchors)
            {
                tags.Add(cell.InnerText);
            }

            tags.Sort();

            Console.WriteLine("------------------SORT DATA---------------------");

            foreach (var selectTags in tags)
            {
               // tags.Add(cell.InnerText);
                Console.WriteLine(selectTags);
            }

            Console.ReadKey();
        }
    }
}
