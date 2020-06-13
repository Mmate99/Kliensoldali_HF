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

//Egy adott könyv részletes adatait megjelenítő oldal
namespace hfTeszt.Views
{
    public sealed partial class BookDetailsPage : Page
    {
        public BookDetailsPage()
        {
            this.InitializeComponent();
        }

        //Az oldalon kiválasztható karakterek eseménykezelője
        private void CharacterSelected(object sender, ItemClickEventArgs e) {
            Character selectedCharacter = (Character)e.ClickedItem;
            //Ismeretlen karakternek nincs linkje
            if(selectedCharacter.Url != null)
                ViewModel.NavigateToCharacter(selectedCharacter.Url);
        }

        //A Main Page gombhoz tartozó eseménykezelő
        private void MainPageButton(object sender, RoutedEventArgs e) {
            ViewModel.BackToMainPage();
        }
    }
}
