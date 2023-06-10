using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT
{
    public class NativeName
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
        public NativeName nativeName { get; set; }
    }

    public class Currencies
    {
        public Currency currency { get; set; }
    }

    public class Currency
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Languages
    {
        public string lang { get; set; }
    }

    public class Translations
    {
        public Translation ces { get; set; }
        public Translation deu { get; set; }
        public Translation fin { get; set; }
        public Translation fra { get; set; }
        public Translation ita { get; set; }
        public Translation jpn { get; set; }
        public Translation kor { get; set; }
        public Translation pol { get; set; }
        public Translation spa { get; set; }
        public Translation swe { get; set; }
    }

    public class Translation
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Car
    {
        public List<string> signs { get; set; }
        public string side { get; set; }
    }

    public class PostalCode
    {
        public string format { get; set; }
        public string regex { get; set; }
    }

    public class CountryInfo
    {
        public Name name { get; set; }
        public List<string> tld { get; set; }
        public string cca2 { get; set; }
        public string ccn3 { get; set; }
        public string cca3 { get; set; }
        public string cioc { get; set; }
        public bool independent { get; set; }
        public string status { get; set; }
        public bool unMember { get; set; }
        public Currencies currencies { get; set; }
        public List<string> capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public Languages languages { get; set; }
        public Translations translations { get; set; }
        public List<double> latlng { get; set; }
        public bool landlocked { get; set; }
        public List<string> borders { get; set; }
        public double area { get; set; }
        public string flag { get; set; }
        public int population { get; set; }
        public string fifa { get; set; }
        public Car car { get; set; }
        public List<string> timezones { get; set; }
        public List<string> continents { get; set; }
        public Flags flags { get; set; }
        public string startOfWeek { get; set; }
        public CapitalInfo capitalInfo { get; set; }
        public PostalCode postalCode { get; set; }
    }

    public class Flags
    {
        public string png { get; set; }
        public string svg { get; set; }
        public string alt { get; set; }
    }

    public class CapitalInfo
    {
        public List<double> latlng { get; set; }
    }

}