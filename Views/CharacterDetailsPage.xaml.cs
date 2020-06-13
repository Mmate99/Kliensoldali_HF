using hfTeszt.Models;
using System;
using System.Collections.Generic;
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

//Egy adott karakter részletes adatait megjelenítő oldal
namespace hfTeszt.Views {

    public sealed partial class CharacterDetailsPage : Page {
        public Character ch;

        public CharacterDetailsPage() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Az oldalon kiválasztható könyvek eseménykezelője
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSelected(object sender, ItemClickEventArgs e) {
            Book selectedBook = (Book)e.ClickedItem;
            if(selectedBook.Url != null)
                ViewModel.NavigateToBook(selectedBook.Url);
        }

        //Az oldalon kiválasztható karakterek eseménykezelője
        private void CharacterSelected(object sender, RoutedEventArgs e) {
            string characterURL = ((Button)sender).Tag.ToString();
            if(characterURL != "")
                ViewModel.NavigateToCharacter(characterURL);
        }

        //Az oldalon kiválasztható házak eseménykezelője
        private void HouseSelected(object sender, ItemClickEventArgs e) {
            House selectedHouse = (House)e.ClickedItem;
            if(selectedHouse.Url != null)
                ViewModel.NavigateToHouse(selectedHouse.Url);
        }

        //A Main Page gombhoz tartozó eseménykezelő
        private void MainPageButton(object sender, RoutedEventArgs e) {
            ViewModel.BackToMainPage();
        }
    }
}
