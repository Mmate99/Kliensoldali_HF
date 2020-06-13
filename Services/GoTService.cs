using hfTeszt.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Az An API of Ice And Fire API-val való kommunikációt megvalósító osztály
/// </summary>
namespace hfTeszt.Services {
    class GoTService {
        private readonly Uri serverUrl = new Uri("https://anapioficeandfire.com");

        //Maximum 50 elem kapható vissza az API-tól, ha ennél több van, akkor több oldalon adja vissza az elemeket
        private int pages { get; set; }

        /// <summary>
        /// Függvény az összes karakter lekérdezéséhez
        /// </summary>
        /// <returns>Az összes karaktert egy listában</returns>
        public async Task<List<Character>> GetCharactersAsync() {
            //Az első oldalnyi karakterek lekérdezése
            var Characters = await GetAsync<List<Character>>(new Uri(serverUrl, "api/characters?&pageSize=50"));

            //További oldalak lekérése, ha vannak.
            if (pages == 1)
                return Characters;
            else {
                for(int i = 2; i <= pages; i++) {
                    Characters.AddRange(await GetAsync<List<Character>>(new Uri(serverUrl, "api/characters?" + $"page={i}&pageSize=50")));
                }

                return Characters;
            }
        }

        /// <summary>
        /// Fügvény az összes könyv lekérdezéséhez
        /// </summary>
        /// <returns>Az összes könyvet egy listában</returns>
        public async Task<List<Book>> GetBooksAsync() {
            //A könyvek száma az 50-et sosem fogja túllépni, ezért itt nem volt szükség az előbbi megoldásra
            return await GetAsync<List<Book>>(new Uri(serverUrl, "api/books?&pageSize=50"));
        }

        /// <summary>
        /// Függvény az összes ház lekérdezéséhez
        /// </summary>
        /// <returns>Az összes házat egy listában</returns>
        public async Task<List<House>> GetHousesAsync() {
            //Az első oldalnyi könyvek lekérdezése
            var Houses = await GetAsync<List<House>>(new Uri(serverUrl, "api/houses?&pageSize=50"));

            //További oldalak lekérése, ha vannak.
            if (pages == 1)
                return Houses;
            else {
                for (int i = 2; i <= pages; i++) {
                    Houses.AddRange(await GetAsync<List<House>>(new Uri(serverUrl, "api/houses?" + $"page={i}&pageSize=50")));
                }

                return Houses;
            }
        }

        /// <summary>
        /// Függvény egy tetszőleges típusú elem lekérdezéséhez adott URI alapján
        /// </summary>
        /// <typeparam name="T">Az elem típusa (House/Book/Character)</typeparam>
        /// <param name="uri">A kiválasztott elem URI-ja</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(Uri uri) {
            using (var client = new HttpClient()) {
                var response = await client.GetAsync(uri);
                setPages(response);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        /// <summary>
        /// Pages változó beállítása az adott headerben kapott értékre
        /// </summary>
        /// <param name="response">Get kérésre kapott válasz</param>
        private void setPages(HttpResponseMessage response) {
            //Az összes oldal mennyiségéről szóló információ lekérése a válasz header-ből
            //Ha üres, visszatér a függvény
            IEnumerable<string> links;
            try {
                links = response.Headers.GetValues("Link");
            }
            catch {
                return;
            }

            //Az információk egy tömbben érkeznek, amelynek csupán egy eleme van
            var str = links.ElementAt(0);
            //Az így kapott string nem csak az összes oldalt tartalmazza, hanem egyéb információkat is, így előb ezt meg kell keresni a stringben
            var beginning = str.LastIndexOf("page=");
            var end = str.LastIndexOf("&");

            //A shortstr már csupán az oldalak számát tartalmazza stringként "page=NUM" formátumban
            string shortstr = str.Substring(beginning, end - beginning);

            //A tényleges oldalszám kinyerése
            string number = "";
            foreach(char c in shortstr) {
                if (Char.IsDigit(c)) {
                    number += c;
                }
            }

            if (number.Length > 0) {
                pages = int.Parse(number);
            }
        }
    }
}
