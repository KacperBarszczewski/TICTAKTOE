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
    public partial class Page_Imie_1vs1 : ContentPage
    {
        public Page_Imie_1vs1()
        {
            InitializeComponent();
            Gracz1.Imie = "Gracz X";
            Gracz2.Imie = "Gracz O";
        }
        private void Button_1vs1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page3());
        }
        private void Wpisywanie1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Gracz1.Imie = e.NewTextValue;
        }
        private void Wpisywanie2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Gracz2.Imie = e.NewTextValue;
        }


    }
    public static class Gracz1
    {
        public static string Imie = "Gracz X";
    }
    public static class Gracz2
    {
        public static string Imie = "Gracz O";
    }
}