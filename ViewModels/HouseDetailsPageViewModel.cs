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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

/// <summary>
/// HouseDetailsPage-hez tartozó ViewModel
/// </summary>
namespace hfTeszt.ViewModels
{
    class HouseDetailsPageViewModel : ViewModelBase {
        public ObservableCollection<House> CadetBranches { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Character> SwornMembers { get; set; } = new ObservableCollection<Character>();

        private House _house;
        public House House {
            get { return _house; }
            set { Set(ref _house, value); }
        }

        private string _titles = "N/A";
        public string Titles {
            get { return _titles; }
            set { Set(ref _titles, value); }
        }

        private string _seats = "N/A";
        public string Seats {
            get { return _seats; }
            set { Set(ref _seats, value); }
        }

        private string _currentLord = "N/A";
        public string CurrentLord {
            get { return _currentLord; }
            set { Set(ref _currentLord, value); }
        }

        private string _heir = "N/A";
        public string Heir {
            get { return _heir; }
            set { Set(ref _heir, value); }
        }

        private string _overlord = "N/A";
        public string Overlord {
            get { return _overlord; }
            set { Set(ref _overlord, value); }
        }

        private string _founder = "N/A";
        public string Founder {
            get { return _founder; }
            set { Set(ref _founder, value); }
        }

        private string _aw = "N/A";
        public string AncestralWeapons {
            get { return _aw; }
            set { Set(ref _aw, value); }
        }

        //Függvény, ami meghívódik, ha a HouseDetailsPage lesz az aktív View
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {
            //Egy új service indítása, a paraméterben kapott ház adatainak betöltése
            string url = (string)parameter;
            GoTService service = new GoTService();
            House = await service.GetAsync<House>(new Uri(url));

            if(House.Titles[0] != "") {
                string titles = "";
                foreach(var title in House.Titles) {
                    titles += title + ", ";
                }
                Titles = DeleteLastComa(titles);
            }

            if(House.Seats[0] != "") {
                string seats = "";
                foreach(var seat in House.Seats) {
                    seats += seat + ", ";
                }
                Seats = DeleteLastComa(seats);
            }

            if(House.CurrentLord != "") {
                var lord = await service.GetAsync<Character>(new Uri(House.CurrentLord));
                CurrentLord = lord.Name;
            }

            if (House.Heir != "") {
                var heir = await service.GetAsync<Character>(new Uri(House.Heir));
                Heir = heir.Name;
            }

            if (House.Overlord != "") {
                var ol = await service.GetAsync<House>(new Uri(House.Overlord));
                Overlord = ol.Name;
            }

            if(House.Founder != "") {
                var fr = await service.GetAsync<Character>(new Uri(House.Founder));
                Founder = fr.Name;
            }

            if(House.AncestralWeapons[0] != "") {
                string aw = "";
                foreach(var weapon in House.AncestralWeapons) {
                    aw += weapon + ", ";
                }
                AncestralWeapons = DeleteLastComa(aw);
            }

            if(House.CadetBranches.Length > 0) {
                foreach(var branchURL in House.CadetBranches) {
                    var house = await service.GetAsync<House>(new Uri(branchURL));
                    CadetBranches.Add(house);
                }
            }
            else {
                CadetBranches.Add(new House { Name = "N/A" });
            }

            if (House.SwornMembers.Length > 0) {
                foreach (var charURL in House.SwornMembers) {
                    var character = await service.GetAsync<Character>(new Uri(charURL));
                    SwornMembers.Add(character);
                }
            }
            else {
                SwornMembers.Add(new Character { Name = "N/A" });
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó ház részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott ház URI-ja</param>
        public void NavigateToHouse(string url) {
            NavigationService.Navigate(typeof(HouseDetailsPage), url);
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
