using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_KB2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Imie_Komp : ContentPage
    {
        public Page_Imie_Komp()
        {
            InitializeComponent();
            Gracz3.Imie = "Gracz";
        }
        private void Button_1vsKOMP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }
        private void Wpisywanie3_TextChanged(object sender, TextChangedEventArgs e)
        {
            Gracz3.Imie = e.NewTextValue;
        }

    }
    public static class Gracz3
    {
        public static string Imie = "Gracz";
    }
}