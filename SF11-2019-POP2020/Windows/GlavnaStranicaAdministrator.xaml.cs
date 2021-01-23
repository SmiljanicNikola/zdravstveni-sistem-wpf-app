﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SF11_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for GlavnaStranicaAdministrator.xaml
    /// </summary>
    public partial class GlavnaStranicaAdministrator : Window
    {
        public GlavnaStranicaAdministrator()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemLekari_Click(object sender, RoutedEventArgs e)
        {
            SviLekari window = new SviLekari();

            this.Hide();
            window.Show();
        }

        private void MenuItemPacijenti_Click(object sender, RoutedEventArgs e)
        {
            SviPacijenti windowPacijenti = new SviPacijenti();

            this.Hide();
            windowPacijenti.Show();
        }

        private void MenuItemAdministratori_Click(object sender, RoutedEventArgs e)
        {
            SviAdministratori windowAdministratori = new SviAdministratori();

            this.Hide();
            windowAdministratori.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemLogOut_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTerapije_Click(object sender, RoutedEventArgs e)
        {
            SveTerapije windowTerapije = new SveTerapije();

            this.Hide();
            windowTerapije.Show();
        }

        private void btnDomoviZdravlja_Click(object sender, RoutedEventArgs e)
        {
            SviDomoviZdravlja windowDomoviZdravlja = new SviDomoviZdravlja();
            this.Hide();
            windowDomoviZdravlja.Show();
        }

        private void btnTermini_Click(object sender, RoutedEventArgs e)
        {
            SviTermini windowTermini = new SviTermini();

            this.Hide();
            windowTermini.Show();
        }

        private void btnAdrese_Click(object sender, RoutedEventArgs e)
        {
            SveAdrese windowAdrese = new SveAdrese();

            this.Hide();
            windowAdrese.Show();
        }
    }
}
