using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modell osztály a Character entitások adatainak tárolására
/// </summary>
namespace hfTeszt.Models {
    public class Character {
        public string icon { get; set; } = "\uE716";
        private string name;
        private string culture;
        private string born;
        private string died;

        public string Url { get; set; }
        public string Name { get { return name; } set { name = value.Equals("") ? "N/A" : value; } }
        public string Gender { get; set; }
        public string Culture { get { return culture; } set { culture = value.Equals("") ? "N/A" : value; } }
        public string Born { get { return born; } set { born = value.Equals("") ? "N/A" : value; } }
        public string Died { get { return died; } set { died = value.Equals("") ? "N/A" : value; } }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Spouse { get; set; }

        public string[] Titles { get; set; }
        public string[] Aliases { get; set; }
        public string[] Allegiances { get; set; }
        public string[] Books { get; set; }
        public string[] PovBooks { get; set; }
        public string[] TvSeries { get; set; }
        public string[] Playedby { get; set; }
    }
}
