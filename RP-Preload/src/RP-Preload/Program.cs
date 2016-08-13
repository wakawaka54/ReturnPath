using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

using Microsoft.Extensions.Configuration;

using System.Net.Http;

using RP_Preload.Sources;
using RP_Preload.Model;

using MongoDB.Bson;
using MongoDB.Driver;

namespace RP_Preload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length == 0) { return; }

            Console.WriteLine("MongoDB-Url: " + args[0]);
            var client = new MongoClient(args[0]);
            var db = client.GetDatabase("RPDB");

            Console.WriteLine("Connected to DB");

            Wikisource source = new Wikisource();

            string[] pages = new string[]
            {
                "Exxon",
                "ExxonMobil",
                "Philips",
                "ConocoPhillips",
                "Chevron_Phillips_Chemical",
                "Dow_Chemical_Company",
                "BASF",
                "Sinopec",
                "SABIC",
                "Formosa_Plastics_Corp",
                "LyondellBasell",
                "DuPont",
                "Ineos",
                "Bayer",
                "Mitsubishi_Chemical_Holdings",
                "Royal_Dutch_Shell",
                "Saudi_Aramco",
                "China_National_Petroleum_Corporation",
                "PetroChina",
                "Kuwait_Petroleum_Corporation",
                "BP",
                "Total_S.A.",
                "Lukoil",
                "Eni",
                "Valero_Energy",
                "Petrobras",
                "Chevron_Corporation",
                "PDVSA",
                "Pemex",
                "National_Iranian_Oil_Company",
                "Gazprom",
                "21st_Century_Fox",
                "Gazprom",
                "Activision_Blizzard",
                "Adobe_Systems",
                "Akamai_Technologies",
                "Alexion_Pharmaceuticals",
                "Alphabet_Inc.",
                "Amazon.com",
                "American_Airlines_Group",
                "Amgen"
            };

            var res = db.GetCollection<MessageContainer>("Sentences");

            foreach (string page in pages)
            {
                Console.WriteLine("Reading wiki page: " + page);

                Task<IEnumerable<MessageContainer>> t = source.GetSentenceContent(page);
                t.Wait();

                res.InsertMany(t.Result);
            }

            Console.WriteLine("COMPLETED");
        }
    }
}
