using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.Windows;
using System;
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

namespace SF11_2019_POP2020
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            Util.Instance.CitanjeEntiteta("lekari.txt");
            Util.Instance.CitanjeEntiteta("adrese.txt");
            Util.Instance.CitanjeEntiteta("domovizdravlja.txt");

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SviLekari window = new SviLekari();

            this.Hide();
            window.Show();
        }

        private void btnAdministratori_Click(object sender, RoutedEventArgs e)
        {
            SviAdministratori windowAdministratori = new SviAdministratori();

            this.Hide();
            windowAdministratori.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdrese_Click(object sender, RoutedEventArgs e)
        {
            SveAdrese windowAdrese = new SveAdrese();

            this.Hide();
            windowAdrese.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SviDomoviZdravlja windowDomoviZdravlja = new SviDomoviZdravlja();
            this.Hide();
            windowDomoviZdravlja.Show();
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SviLekari window = new SviLekari();

            this.Hide();
            window.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            loginAdmin l = new loginAdmin();

            this.Hide();
            l.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            loginLekar ll = new loginLekar();

            this.Hide();
            ll.Show();
        }

        private void btnPacijenti_Click(object sender, RoutedEventArgs e)
        {
            SviPacijenti windowPacijenti = new SviPacijenti();

            this.Hide();
            windowPacijenti.Show();
        }
    }
}
