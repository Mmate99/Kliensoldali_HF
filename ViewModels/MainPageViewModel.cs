using hfTeszt.Models;
using hfTeszt.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using hfTeszt.Views;
using Windows.UI.Core;

/// <summary>
/// MainPage-hez tartozó ViewModel
/// </summary>
namespace hfTeszt.ViewModels {
    public sealed class MainPageViewModel : ViewModelBase {
        //Collection-ök, amikben eltároljuk az összes entitást
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();

        //Függvény, ami meghívódik, ha a MainPage lesz az aktív View
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state) {
            //A visszagomb letiltása, hiszen a főoldalról nem tudunk visszább menni
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Disabled;

            //Új service indítása, colletion-ök feltöltése
            var service = new GoTService();

            var books = await service.GetBooksAsync();
            foreach (var b in books) {
                Books.Add(b);
            }

            var characters = await service.GetCharactersAsync();
            foreach (var c in characters) {
                Characters.Add(c);
            }

            var houses = await service.GetHousesAsync();
            foreach (var h in houses) {
                Houses.Add(h);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó könyv részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott könyv URL-je</param>
        public void NavigateToBook(string url) {
            NavigationService.Navigate(typeof(BookDetailsPage), url);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó karakter részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott karakter URL-je</param>
        public void NavigateToCharacter(string url) {
            NavigationService.Navigate(typeof(CharacterDetailsPage), url);
        }

        /// <summary>
        /// Függvény, mellyel a paraméterként kapott URL-hez tartozó ház részletezőablaka lesz aktív
        /// </summary>
        /// <param name="url">Kiválasztott ház URL-je</param>
        public void NavigateToHouse(string url) {
            NavigationService.Navigate(typeof(HouseDetailsPage), url);
        }
    }
}
