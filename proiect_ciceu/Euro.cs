using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.Json;

namespace proiect_ciceu
{
    class Euro : credit
    {
        float x;
        public Euro()
        {
            _suma = 0;
            _dobanda = 0;
            _durata = 0;
        }
        public Euro(float a)
        {
            x = a;
        }

        public string Conve()
        {
            string info = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=EUR_RON&compact=ultra&apiKey=02109d5b9f21cf341d8b");
            JsonValue jsonValue = System.Json.JsonValue.Parse(info);
            float e = 0;
            if (jsonValue.ContainsKey("EUR_RON"))

            {
                e = jsonValue["EUR_RON"];
                return Convert.ToString((int)(x / e * 100) / 100.0);
            }
            else return "valuta inexistenta";

        }
        public string UsdConve()
        {
            string info = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=USD_RON&compact=ultra&apiKey=02109d5b9f21cf341d8b");
            JsonValue jsonValue = System.Json.JsonValue.Parse(info);
            float e = 0;
            if (jsonValue.ContainsKey("USD_RON"))

            {
                e = jsonValue["USD_RON"];
                return Convert.ToString((int)(x / e * 100) / 100.0);
            }
            else return "valuta inex";

        }
        public string GbpConve()
        {
            string info = new WebClient().DownloadString("https://free.currconv.com/api/v7/convert?q=GBP_RON&compact=ultra&apiKey=02109d5b9f21cf341d8b");
            JsonValue jsonValue = System.Json.JsonValue.Parse(info);
            float e = 0;
            if (jsonValue.ContainsKey("GBP_RON"))

            {
                e = jsonValue["GBP_RON"];
                return Convert.ToString((int)(x / e * 100) / 100.0);
            }
            else return "valuta inexistenta";
        }

       

    }
}
