using BOT;
using System;

namespace BOT
{
    class MainClass
    {
        static void Main(string[] args)
        {
            CountryInfoBot countryInfoBot = new CountryInfoBot("6099182646:AAGz7vX7Hksy0w6n_EF8YdIeTTXP2zNe4KI");
            countryInfoBot.Start();
            Console.ReadKey();
        }
    }
}