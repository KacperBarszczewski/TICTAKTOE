﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe_KB2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void Button_Start_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page2());
        }
    }
}
