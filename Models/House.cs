using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modell osztály a House entitások adatainak tárolására
/// </summary>
namespace hfTeszt.Models {
    public class House {
        public string icon { get; set; } = "\uE80F";
        private string words;
        private string founded;
        private string diedOut;

        public string Url { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get { return words; } set { words = value.Equals("") ? "N/A" : value; } }
        public string CurrentLord { get; set; }
        public string Heir { get; set; }
        public string Overlord { get; set; }
        public string Founded { get { return founded; } set { founded = value.Equals("") ? "N/A" : value; } }
        public string Founder { get; set; }
        public string DiedOut { get { return diedOut; } set { diedOut = value.Equals("") ? "N/A" : value; } }

        public string[] Titles { get; set; }
        public string[] Seats { get; set; }
        public string[] AncestralWeapons { get; set; }
        public string[] CadetBranches { get; set; }
        public string[] SwornMembers { get; set; }
    }
}
