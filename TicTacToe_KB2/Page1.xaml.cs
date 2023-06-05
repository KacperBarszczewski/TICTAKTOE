﻿using System;
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
    public partial class Page1 : ContentPage
    {
        Button[] buttons;
        int[,] tab;
        int counter = 0;
        bool O_PTurn = true;
        public static string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KtoWygralv2.txt");

        public Page1()
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

            PierszyRuchkomp();
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

        private int checkResult_dla2(int[,] tablica, int dwa)
        {
            int sum = 0;

            sum = tablica[2, 0] + tablica[2, 1] + tablica[2, 2];
            if (sum == dwa) return sum;

            sum = tablica[0, 0] + tablica[0, 1] + tablica[0, 2];
            if (sum == dwa) return sum;

            sum = tablica[0, 0] + tablica[1, 0] + tablica[2, 0];
            if (sum == dwa) return sum;

            sum = tablica[0, 2] + tablica[1, 2] + tablica[2, 2];
            if (sum == dwa) return sum;

            sum = tablica[0, 0] + tablica[1, 1] + tablica[2, 2];
            if (sum == dwa) return sum;

            sum = tablica[2, 0] + tablica[1, 1] + tablica[0, 2];
            if (sum == dwa) return sum;

            sum = tablica[1, 0] + tablica[1, 1] + tablica[1, 2];
            if (sum == dwa) return sum;
            sum = tablica[0, 1] + tablica[1, 1] + tablica[2, 1];
            if (sum == dwa) return sum;

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

                        buttons[i].Text = "O";
                        buttons[i].BackgroundColor = Color.CornflowerBlue;
                        O_PTurn = false;
                        tab[j, k] = -1;

                        if (counter != 9 & (checkResult(tab) != -3))
                        {

                            if (checkResult_dla2(tab, 2) == 2)
                            {
                                komp();
                            }
                            else
                            {
                                if (counter == 2)
                                {
                                    DrugiRuchkomp();
                                }
                                else if (counter == 4)
                                {
                                    TrzciRuchkomp();
                                }
                                else
                                {
                                    komp();
                                }
                            }

                            counter++;
                        }

                        buttons[i].IsEnabled = false;
                        int result = checkResult(tab);

                        if (result == 3)
                        {
                            disableButtons();
                            File.WriteAllText(fileName, File.ReadAllText(fileName) + "\n" + "Wygrał Komputer");
                            labInfo.Text = "Computer won";
                            labInfo.TextColor = Color.Coral;
                            return;
                        }
                        else if (result == -3)
                        {
                            disableButtons();
                            File.WriteAllText(fileName, File.ReadAllText(fileName) + "\n" + "Wygrał "+Gracz3.Imie);
                            labInfo.Text = Gracz3.Imie+" won";
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


        private void komp()
        {

            int[,] komp_tab = tab;
            int jkomp, kkomp;
            bool boolkomp = true;
            Random random = new Random();


            if (checkResult_dla2(tab, 2) == 2)
            {
                do
                {

                    jkomp = random.Next(3);
                    kkomp = random.Next(3);
                    if ((komp_tab[jkomp, kkomp] == 0))
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if (checkResult(komp_tab) == 3)
                        {
                            boolkomp = false;
                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }

                } while (boolkomp);

                tab = komp_tab;
            }
            else if (komp_tab[1, 1] == -1)
            {

                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);


                    if (komp_tab[jkomp, kkomp] == 0)
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if (checkResult_dla2(komp_tab, -2) != -2)
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }


                } while (boolkomp);

                tab = komp_tab;

            }
            else if (checkResult_dla2(tab, -2) == -2)
            {
                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);


                    if (komp_tab[jkomp, kkomp] == 0)
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if (checkResult_dla2(komp_tab, -2) != -2)
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }


                } while (boolkomp);

                tab = komp_tab;

            }
            else
            {
                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);
                    if ((komp_tab[jkomp, kkomp] == 0))
                    {
                        tab[jkomp, kkomp] = 1;
                        boolkomp = false;
                    }

                } while (boolkomp);
            }

            Zmiana_Button_KOMP(jkomp, kkomp);

        }

        private void PierszyRuchkomp()
        {
            int[,] komp_tab = tab;
            int jkomp = 1, kkomp = 1;
            Random random = new Random();
            int s = random.Next(4);
            counter = 1;

            switch (s)
            {
                case 0: tab[jkomp = 0, kkomp = 0] = 1; break;
                case 1: tab[jkomp = 2, kkomp = 0] = 1; break;
                case 2: tab[jkomp = 0, kkomp = 2] = 1; break;
                case 3: tab[jkomp = 2, kkomp = 2] = 1; break;

            }

            Zmiana_Button_KOMP(jkomp, kkomp);
        }

        private void DrugiRuchkomp()
        {
            int[,] komp_tab = tab;
            int jkomp = 0, kkomp = 0;
            bool boolkomp = true;
            Random random = new Random();

            if ((tab[1, 2] == -1) | (tab[1, 0] == -1) | (tab[2, 1] == -1) | (tab[0, 1] == -1))
            {
                tab[jkomp = 1, kkomp = 1] = 1;
            }
            else
            {
                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);

                    if ((komp_tab[jkomp, kkomp] == 0))
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if ((komp_tab[1, 1] != 1)&(checkResult_dla2(komp_tab, 2) == 2) & !((komp_tab[1, 2] == 1) | (komp_tab[1, 0] == 1) | (komp_tab[2, 1] == 1) | (komp_tab[0, 1] == 1)))
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }

                } while (boolkomp);

                tab = komp_tab;
            }

            Zmiana_Button_KOMP(jkomp, kkomp);

        }

        private void TrzciRuchkomp()
        {
            int[,] komp_tab = tab;
            int jkomp = 0, kkomp = 0;
            bool boolkomp = true;
            Random random = new Random();

            if (komp_tab[1, 1] == 1)
            {
                do
                {

                    jkomp = random.Next(3);
                    kkomp = random.Next(3);


                    if (komp_tab[jkomp, kkomp] == 0)
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if ((checkResult_dla2(komp_tab, -2) != -2) & (checkResult_dla2(komp_tab, 2) == 2) & !((komp_tab[1, 2] == 1) | (komp_tab[1, 0] == 1) | (komp_tab[2, 1] == 1) | (komp_tab[0, 1] == 1)))
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }

                } while (boolkomp);

                tab = komp_tab;

            }
            else if (komp_tab[1, 1] == -1)
            {
                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);

                    if (komp_tab[jkomp, kkomp] == 0)
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if (checkResult_dla2(komp_tab, -2) != -2)
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }
                } while (boolkomp);

                tab = komp_tab;

            }
            else
            {
                do
                {
                    jkomp = random.Next(3);
                    kkomp = random.Next(3);

                    if (komp_tab[jkomp, kkomp] == 0)
                    {
                        komp_tab[jkomp, kkomp] = 1;

                        if ((checkResult_dla2(komp_tab, 2) == 2) & !((komp_tab[1, 2] == 1) | (komp_tab[1, 0] == 1) | (komp_tab[2, 1] == 1) | (komp_tab[0, 1] == 1)))
                        {

                            boolkomp = false;

                        }
                        else
                        {
                            komp_tab[jkomp, kkomp] = 0;
                        }
                    }
                } while (boolkomp);

                tab = komp_tab;
            }

            Zmiana_Button_KOMP(jkomp, kkomp);

        }
        private void Zmiana_Button_KOMP(int jkomp, int kkomp)
        {
            if (jkomp == 0)
            {
                buttons[kkomp].Text = "X";
                buttons[kkomp].IsEnabled = false;
                buttons[kkomp].BackgroundColor = Color.Coral;
            }
            else if (jkomp == 1)
            {
                buttons[kkomp + 3].Text = "X";
                buttons[kkomp + 3].IsEnabled = false;
                buttons[kkomp + 3].BackgroundColor = Color.Coral;
            }
            else
            {
                buttons[kkomp + 6].Text = "X";
                buttons[kkomp + 6].IsEnabled = false;
                buttons[kkomp + 6].BackgroundColor = Color.Coral;
            }

        }
    }
}