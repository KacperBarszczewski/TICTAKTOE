using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_KB2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        Button[] buttons;
        int[,] tab;
        int counter = 0;
        bool O_PTurn = true;
        public static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KtoWygralv2.txt");
        

        public Page3()
        {
            InitializeComponent();

            buttons = new Button[9];
            tab = new int[3, 3];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = (Button)FindByName("btn" + (i + 1).ToString());
                //buttons[i].FontSize= Device.GetNamedSize(NamedSize.Large, typeof(Label));
                buttons[i].FontSize = 50;
                buttons[i].FontAttributes = FontAttributes.Bold;
                buttons[i].Clicked += btn_Clicked;
                buttons[i].BackgroundColor = Color.LightGray;
            }

            btnNewGame_Clicked(null, EventArgs.Empty);


        }
        private void clearButtons()
        {
            labInfo.TextColor = Color.Gray;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = "";
                buttons[i].BackgroundColor = Color.Default;
            }
        }

        private void disableButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsEnabled)
                {
                    buttons[i].IsEnabled = false;
                }
            }
        }

        private void enableButtons()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].IsEnabled = true;
            }
        }

        private void resetTable()
        {
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    tab[i, j] = 0;
                }
            }
        }

        private void btnNewGame_Clicked(object sender, EventArgs e)
        {
            clearButtons(); enableButtons(); resetTable();
            counter = 0;
            O_PTurn = true;
            labInfo.Text = "";

        }

        private int checkResult(int[,] tablica)
        {
            int sum;
            for (int i = 0; i < tablica.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < tablica.GetLength(1); j++)
                {
                    sum += tablica[i, j];
                    if ((sum == 3) || (sum == -3)) return sum;
                }
            }
            for (int j = 0; j < tablica.GetLength(1); j++)
            {
                sum = 0;
                for (int i = 0; i < tablica.GetLength(0); i++)
                    sum += tablica[i, j];
                if ((sum == 3) || (sum == -3)) return sum;
            }

            sum = tablica[0, 0] + tablica[1, 1] + tablica[2, 2];
            if ((sum == 3) || (sum == -3)) return sum;

            sum = tablica[2, 0] + tablica[1, 1] + tablica[0, 2];
            if ((sum == 3) || (sum == -3)) return sum;

            return 0;
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            if (counter < 9)
            {
                int j, k;
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (sender.Equals(buttons[i]))
                    {
                        if (i < 3)
                        {
                            j = 0; k = i;
                        }
                        else if (i < 6)
                        {
                            j = 1; k = i - 3;
                        }
                        else
                        {
                            j = 2; k = i - 6;
                        }

                        counter++;

                        if (O_PTurn)
                        {

                            buttons[i].Text = "O";
                            buttons[i].BackgroundColor = Color.CornflowerBlue;
                            O_PTurn = false;
                            tab[j, k] = -1;

                        }
                        else
                        {
                            buttons[i].Text = "X";
                            buttons[i].BackgroundColor = Color.Coral;
                            O_PTurn = true;
                            tab[j, k] = 1;
                        }
                        

                        buttons[i].IsEnabled = false;
                        int result = checkResult(tab);

                        if (result == 3)
                        {
                            disableButtons();
                            File.WriteAllText(fileName, File.ReadAllText(fileName) + "\n" + "Wygrał "+Gracz1.Imie);
                            labInfo.Text = Gracz1.Imie+" won";
                            labInfo.TextColor = Color.Coral;
                            return;
                        }
                        else if (result == -3)
                        {
                            disableButtons();
                            File.WriteAllText(fileName, File.ReadAllText(fileName) + "\n" + "Wygrał "+ Gracz2.Imie);
                            labInfo.Text = Gracz2.Imie+" won";
                            labInfo.TextColor = Color.CornflowerBlue;
                            return;
                        }
                        if (counter == 9)
                        {
                            File.WriteAllText(fileName, File.ReadAllText(fileName) + "\n" + "Remis");
                            labInfo.Text = "no winners";
                            return;
                        }


                    }
                }


            }

        }
    }
}