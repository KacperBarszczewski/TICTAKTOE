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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Button_1vs1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Imie_1vs1());
        }
        private void Button_1vsKOMP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Imie_Komp());
        }
        private void Button_Lista_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page4_Lista());
        }
    }
}