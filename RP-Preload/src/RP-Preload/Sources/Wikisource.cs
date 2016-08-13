using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using RP_Preload.Model;

using System.Net.Http;

using HtmlAgilityPack;

namespace RP_Preload.Sources
{
    public class Wikisource
    {
        public string WikiPage { get; set; }

        static string[] boringWords = new string[]
        {
            "to", "the", "of", "from", "how", "and", "that", "in", "for", "its", "the",
            "is", "by", "with", "on", "in", "it", "were", "be", "into", "their", "would",
            "been", "this", "including", "such", "over", "under", "or", "but", "not",
            "on", "sold", "use", "up", "some", "who", "he", "held", "its", "which",
            "billion", "other", "first", "united", "one", "will", "two", "more",
            "during", "three", "four", "five", "six", "seven", "eight", "now",
            "founded", "group", "now", "most", "us", "out", "per", "when", "led", "using",
            "while", "site", "time", "top", "buy", "there", "those", "moved", "through",
            "they", "used", "since", "between"
        };

        public Wikisource()
        {

        }

        public async Task<IEnumerable<MessageContainer>> GetSentenceContent(string page)
        {
            WikiPage = page;

            string html = await getHtmlContent();
            string text = getTextContent(html).ToLower();

            string[] sentences = text.Split('.', '!', '?');

            List<MessageContainer> ret = new List<MessageContainer>();

            foreach(string sentence in sentences)
            {
                MessageContainer c = new MessageContainer();
                c.Sentence = sentence;

                string[] splits = sentence.Split(' ');
                c.Tags = splits.Where(x => isAlpha(x)).ToArray();

                ret.Add(c);
            }

            return ret;
        }

        bool isAlpha(string s)
        {
            if(boringWords.Contains(s)) { return false; }
            if(s == "") { return false; }

            foreach (char c in s)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) { continue; }
                return false;
            }

            return true;
        }

         async Task<string> getHtmlContent()
            {
                string html = "";

                using (HttpClient client = new HttpClient())
                {
                client.BaseAddress = new Uri(@"https://en.wikipedia.org/wiki/");
                    var response = await client.GetAsync(WikiPage);
                    html = await response.Content.ReadAsStringAsync();
                }

                return html;
            }

        string getTextContent(string html)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(html);

            var element = doc.GetElementbyId("mw-content-text");
            var pElements = element.ChildNodes.Where(x => x.Name == "p");
            string text = "";

            foreach(var e in pElements)
            {
                text += e.InnerText;
            }

            return text;
        }
    }
}
