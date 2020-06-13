using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Modell osztály a Book entitások adatainak tárolására
/// </summary>
namespace hfTeszt.Models {
    public class Book {
        public string icon { get; set; } = "\uE736";

        public int NumberOfPages { get; set; }
        public DateTime Released { get; set; }

        public string Url { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public string MediaType { get; set; }

        public string[] Authors { get; set; }
        public string[] Characters { get; set; }
        public string[] PovCharacters { get; set; }
    }
}
