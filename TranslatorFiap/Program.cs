using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TranslatorFiap
{
    public class Program
    {

        private const string Key = "1d810bf223c54fa09f578d07796b9351";
        private static readonly HttpClient client = new HttpClient
        {
            DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", Key } }
        };

        public  static async Task Main()
        {
            while(true)
            {
                var text = Console.ReadLine();
                var TranslatedText = await Translate(text, "en");
                Console.WriteLine(TranslatedText);
            }
            
        }

        public static async Task<string> Translate (string text, string language)
        {
            var encodedText = WebUtility.UrlEncode(text);
            var uri = "https://api.microsofttranslator.com/V2/Http.SVC/Translate?" +
                $"to={language}&text={encodedText}";

            var result = await client.GetStringAsync(uri);

            return XElement.Parse(result).Value;
        }
    }
}
