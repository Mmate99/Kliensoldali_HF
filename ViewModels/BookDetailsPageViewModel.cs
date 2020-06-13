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
/// BookDetailsPage-hez tartozó ViewModel
/// </summary>
namespace hfTeszt.ViewModels
{
    class BookDetailsPageViewModel : ViewModelBase {
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> PovCharacters { get; set; } = new ObservableCollection<Character>();

        private Book _book;
        public Book Book {
            get { return _book; }
            set { Set(ref _book, value); }
        }

        private string _authors;
        public string Authors {
            get { return _authors; }
            set { Set(ref _authors, value); }
        }

        //Függvény, ami meghívódik, ha a BookDetailsPage lesz az aktív View
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {
            //Egy új service indítása, a paraméterben kapott könyv adatainak betöltése
            string url = (string)parameter;
            GoTService service = new GoTService();
            Book = await service.GetAsync<Book>(new Uri(url));

            if (Book.Authors.Length > 0) {
                string authors = "";
                foreach (var author in Book.Authors) {
                    authors += author + ", ";
                }
                Authors = DeleteLastComa(authors);
            }

            if (Book.PovCharacters.Length > 0) {
                foreach (var characterURL in Book.PovCharacters) {
                    var ch = await service.GetAsync<Character>(new Uri(characterURL));
                    PovCharacters.Add(ch);
                }
            }
            else {
                PovCharacters.Add(new Character { Name = "N/A" });
            }

            if (Book.Characters.Length > 0) {
                foreach (var characterURL in Book.Characters) {
                    var ch = await service.GetAsync<Character>(new Uri(characterURL));
                    Characters.Add(ch);
                }
            }
            else {
                Characters.Add(new Character { Name = "N/A" });
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó karakter részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott karakter URI-ja</param>
        public void NavigateToCharacter(string url) {
            NavigationService.Navigate(typeof(CharacterDetailsPage), url);
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
