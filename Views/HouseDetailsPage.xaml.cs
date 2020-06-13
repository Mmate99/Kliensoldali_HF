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

//Egy adott ház részletes adatait megjelenítő oldal
namespace hfTeszt.Views
{
    public sealed partial class HouseDetailsPage : Page
    {
        public HouseDetailsPage()
        {
            this.InitializeComponent();
        }

        //Az oldalon GridView-ból kiválasztható karakterek eseménykezelője
        private void CharacterSelected(object sender, ItemClickEventArgs e) {
            Character selectedCharacter = (Character)e.ClickedItem;
            if(selectedCharacter.Url != null)
                ViewModel.NavigateToCharacter(selectedCharacter.Url);
        }

        //Az oldalon kiválasztható karakterek eseménykezelője
        private void CharacterSelected(object sender, RoutedEventArgs e) {
            string characterURL = ((Button)sender).Tag.ToString();
            if(characterURL != "")
                ViewModel.NavigateToCharacter(characterURL);
        }

        //Az oldalon GridView-ból kiválasztható házak eseménykezelője
        private void HouseSelected(object sender, ItemClickEventArgs e) {
            House selectedHouse = (House)e.ClickedItem;
            if(selectedHouse.Url != null)
                ViewModel.NavigateToHouse(selectedHouse.Url);
        }

        //Az oldalon kiválasztható házak eseménykezelője
        private void HouseSelected(object sender, RoutedEventArgs e) {
            string HouseURL = ((Button)sender).Tag.ToString();
            if(HouseURL != "")
                ViewModel.NavigateToHouse(HouseURL);
        }

        //A Main Page gombhoz tartozó eseménykezelő
        private void MainPageButton(object sender, RoutedEventArgs e) {
            ViewModel.BackToMainPage();
        }
    }
}
