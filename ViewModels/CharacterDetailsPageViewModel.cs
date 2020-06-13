using hfTeszt.Models;
using hfTeszt.Services;
using hfTeszt.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

/// <summary>
/// CharacterDetailsPage-hez tartozó ViewModel
/// </summary>
namespace hfTeszt.ViewModels {
    public class CharacterDetailsPageViewModel : ViewModelBase {
        public ObservableCollection<Book> BookArray { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> PovBookArray { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<House> Allegiances { get; set; } = new ObservableCollection<House>();

        private Character _character;
        public Character Character {
            get { return _character; }
            set { Set(ref _character, value); }
        }

        private string _father = "N/A";
        public string Father {
            get { return _father; }
            set { Set(ref _father, value); }
        }

        private string _mother = "N/A";
        public string Mother {
            get { return _mother; }
            set { Set(ref _mother, value); }
        }

        private string _aliases = "N/A";
        public string Aliases {
            get { return _aliases; }
            set { Set(ref _aliases, value); }
        }

        private string _spouse = "N/A";
        public string Spouse {
            get { return _spouse; }
            set { Set(ref _spouse, value); }
        }

        private string _titles = "N/A";
        public string Titles {
            get { return _titles; }
            set { Set(ref _titles, value); }
        }

        private string _tvSeries = "N/A";
        public string TvSeries {
            get { return _tvSeries; }
            set { Set(ref _tvSeries, value); }
        }

        private string _playedBy = "N/A";
        public string PlayedBy {
            get { return _playedBy; }
            set { Set(ref _playedBy, value); }
        }

        //Függvény, ami meghívódik, ha a CharacterDetailsPage lesz az aktív View
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {
            //Egy új service indítása, a paraméterben kapott karakter adatainak betöltése
            string url = (string)parameter;
            GoTService service = new GoTService();
            Character = await service.GetAsync<Character>(new Uri(url));

            if (!Character.Father.Equals("")) {
                var father = await service.GetAsync<Character>(new Uri(Character.Father));
                Father = father.Name;
            }

            if (!Character.Mother.Equals("")) {
                var mother = await service.GetAsync<Character>(new Uri(Character.Mother));
                Mother = mother.Name;
            }

            if (Character.Spouse.Length > 0) {
                var spouse = await service.GetAsync<Character>(new Uri(Character.Spouse));
                Spouse = spouse.Name;
            }

            if (!Character.Titles[0].Equals("")) {
                string titles = "";
                foreach (string title in Character.Titles) {
                    titles += title + ", ";
                }
                Titles = DeleteLastComa(titles);
            }

            if (!Character.Aliases[0].Equals("")) {
                string aliases = "";
                foreach (string alias in Character.Aliases) {
                    aliases += alias + ", ";
                }
                Aliases = DeleteLastComa(aliases);
            }

            if (Character.Books.Length > 0) {
                foreach (var bookURL in Character.Books) {
                    var book = await service.GetAsync<Book>(new Uri(bookURL));
                    this.BookArray.Add(book);
                }
            }
            else {
                BookArray.Add(new Book { Name = "N/A" });
            }

            if (Character.PovBooks.Length > 0) {
                foreach (var povBookURL in Character.PovBooks) {
                    var povBook = await service.GetAsync<Book>(new Uri(povBookURL));
                    this.PovBookArray.Add(povBook);
                }
            }
            else {
                PovBookArray.Add(new Book { Name = "N/A" });
            }

            if(Character.Allegiances.Length > 0) {
                foreach(var houseURL in Character.Allegiances) {
                    var house = await service.GetAsync<House>(new Uri(houseURL));
                    this.Allegiances.Add(house);
                }
            }
            else {
                Allegiances.Add(new House { Name = "N/A" });
            }

            if (!Character.TvSeries[0].Equals("")) {
                string series = "";
                foreach (var season in Character.TvSeries) {
                    series += season + "\n";
                }
                TvSeries = series;
            }

            if (!Character.Playedby[0].Equals("")) {
                string playedBy = "";
                foreach (var actor in Character.Playedby) {
                    playedBy += actor + ", ";
                }
                PlayedBy = DeleteLastComa(playedBy);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó könyv részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott könyv URI-ja</param>
        public void NavigateToBook(string url) {
            NavigationService.Navigate(typeof(BookDetailsPage), url);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó karakter részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott karakter URI-ja</param>
        public void NavigateToCharacter(string url) {
            NavigationService.Navigate(typeof(CharacterDetailsPage), url);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó ház részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott ház URI-ja</param>
        public void NavigateToHouse(string url) {
            NavigationService.Navigate(typeof(HouseDetailsPage), url);
        }

        /// <summary>
        /// A Main Page gomb lenyomásához tartozó függvény, aminek hatására a nézet visszavált a főoldalra
        /// </summary>
        public void BackToMainPage() {
            NavigationService.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Függvény, ami a paraméterként kapott stringből eltávolítja az utolsó helyen lévő felesleges vessző karaktert
        /// </summary>
        /// <param name="txt">Szöveg felesleges vesszővel</param>
        /// <returns>Szöveg vessző nélkül</returns>
        private string DeleteLastComa(string txt) {
            if (txt[txt.Length - 2].ToString() == ",") {
                StringBuilder sb = new StringBuilder(txt);
                sb.Remove(txt.Length - 2, 1);
                return sb.ToString();
            }
            return txt;
        }
    }
}
