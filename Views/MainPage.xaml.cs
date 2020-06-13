using hfTeszt.Models;
using hfTeszt.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//A főoldal
namespace hfTeszt.Views {

    //Az aktív navigációs elem ellenőrzését segítő enumok
    enum ActivePage {
        Books,
        Houses,
        Characters
    }

    public sealed partial class MainPage : Page {
        ActivePage ap = new ActivePage();

        public MainPage() {
            this.InitializeComponent();
        }

        //Az oldalsó navigációs sáv nyitó/csukó gombjának eseménykezelője
        private void Button_Pane_Size(object sender, RoutedEventArgs e) {
            NavBar.IsPaneOpen = !NavBar.IsPaneOpen;
        }

        //A könyveket kiválasztó gomb eseménykezelője
        private void Button_Books(object sender, RoutedEventArgs e) {
            ap = ActivePage.Books;
            GridView.ItemsSource = ViewModel.Books;
            
        }

        //A karaktereket kiválasztó gomb eseménykezelője
        private void Button_Characters(object sender, RoutedEventArgs e) {
            ap = ActivePage.Characters;
            GridView.ItemsSource = ViewModel.Characters;
        }

        //A házakat kiválasztó gomb eseménykezelője
        private void Button_Houses(object sender, RoutedEventArgs e) {
            ap = ActivePage.Houses;
            GridView.ItemsSource = ViewModel.Houses;
        }

        //GridView-ban kiválasztott elem eseménykezelője
        private void GridView_ItemClick(object sender, ItemClickEventArgs e) {

            //Az ActivePage alapján a megfelelő detailsPage-re váltás
            switch (ap) {
                case ActivePage.Books:
                    var selectedBook = ((Book)e.ClickedItem).Url;
                    ViewModel.NavigateToBook(selectedBook);
                    break;
                case ActivePage.Houses:
                    var selectedHouse = ((House)e.ClickedItem).Url;
                    ViewModel.NavigateToHouse(selectedHouse);
                    break;
                case ActivePage.Characters:
                    var selectedCharacter = ((Character)e.ClickedItem).Url;
                    ViewModel.NavigateToCharacter(selectedCharacter);
                    break;
            }
        }

        //A kereső ajánlólistájának frissítése, ha egy új karakter kerül beírásra
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) {
                //Ha kettőnél kevesebb karakter van beírva az túl általános, túl sok ajánlatot kellene feleslegesen listázni
                if(sender.Text.Length > 1) {
                    sender.ItemsSource = this.GetSuggestions(sender.Text);
                }
                else {
                    sender.ItemsSource = new string[] { "No suggestions!" };
                }
            }
        }

        //A keresőbe beírt szöveg alapján az ajánlólista frissítése
        private string[] GetSuggestions(string text) {
            
            switch (ap) {
                case ActivePage.Books:
                    ArrayList books = new ArrayList();
                    foreach (var item in ViewModel.Books) {
                        if (item.Name.ToLowerInvariant().Contains(text.ToLowerInvariant()))
                            books.Add(item.Name);
                    }
                    return (string[])books.ToArray(typeof(string));

                case ActivePage.Characters:
                    ArrayList characters = new ArrayList();
                    foreach (var item in ViewModel.Characters) {
                        if (item.Name.ToLowerInvariant().Contains(text.ToLowerInvariant()))
                            characters.Add(item.Name);
                    }
                    return (string[])characters.ToArray(typeof(string));

                case ActivePage.Houses:
                    ArrayList houses = new ArrayList();
                    foreach (var item in ViewModel.Houses) {
                        if (item.Name.ToLowerInvariant().Contains(text.ToLowerInvariant()))
                            houses.Add(item.Name);
                    }
                    return (string[])houses.ToArray(typeof(string));
            }

            return null;
        }

        //Enter vagy a nagyító ikon megnyomásának hatására szűrés indítása a keresőben lévő szöveggel
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) {
            //Az oldal tartalmát frissíti azokkal az elemekkel amelyeknek a neve tartalmazza a keresett szöveget
            switch (ap) {
                case ActivePage.Books:
                    var books = ViewModel.Books.Where(x => x.Name.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()));
                    GridView.ItemsSource = new ObservableCollection<Book>(books);
                    return;

                case ActivePage.Characters:
                    var characters = ViewModel.Characters.Where(x => x.Name.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()));
                    GridView.ItemsSource = new ObservableCollection<Character>(characters);
                    return;

                case ActivePage.Houses:
                    var houses = ViewModel.Houses.Where(x => x.Name.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()));
                    GridView.ItemsSource = new ObservableCollection<House>(houses);
                    return;
            }
        }

        //Szűrés indítása, ha a felhasználó kiválasztott egy elemet az ajánlólistából
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args) {
            if (args.SelectedItem != null) {

                //A keresőben lévő szöveg kiegészítése a kiválasztott elemmel
                SuggestBox.Text = args.SelectedItem.ToString();
                //Szűrés a szöveg alapján a megfelelő listában
                switch (ap) {
                    case ActivePage.Books:
                        var books = ViewModel.Books.Where(x => x.Name.ToLowerInvariant().Equals(args.SelectedItem.ToString().ToLowerInvariant()));
                        GridView.ItemsSource = new ObservableCollection<Book>(books);
                        return;

                    case ActivePage.Characters:
                        var characters = ViewModel.Characters.Where(x => x.Name.ToLowerInvariant().Equals(args.SelectedItem.ToString().ToLowerInvariant()));
                        GridView.ItemsSource = new ObservableCollection<Character>(characters);
                        return;

                    case ActivePage.Houses:
                        var houses = ViewModel.Houses.Where(x => x.Name.ToLowerInvariant().Equals(args.SelectedItem.ToString().ToLowerInvariant()));
                        GridView.ItemsSource = new ObservableCollection<House>(houses);
                        return;
                }
            }
        }
    }
}
