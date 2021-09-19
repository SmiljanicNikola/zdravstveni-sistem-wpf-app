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
            Util.Instance.CitanjeEntiteta("termini.txt");
            Util.Instance.CitanjeEntiteta("terapije.txt");



        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SviLekari window = new SviLekari();

            this.Hide();
            window.Show();
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

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LoginPacijent lll = new LoginPacijent();

            this.Hide();
            lll.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SviTermini windowTermini = new SviTermini();

            this.Hide();
            windowTermini.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            SveTerapije windowTerapije = new SveTerapije();

            this.Hide();
            windowTerapije.Show();
        }

        private void pregledLekara_Click(object sender, RoutedEventArgs e)
        {
            SviDoktori windowLekari = new SviDoktori();

            this.Hide();
            windowLekari.Show();
        }

        private void pregledDomova_Click(object sender, RoutedEventArgs e)
        {
            SviDomoviZdravlja sdz = new SviDomoviZdravlja();

            this.Hide();
            sdz.Show();
        }



        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaPacijenta addPacijent = new DodavanjeIzmenaPacijenta(noviKorisnik, EStatus.Dodaj);
            addPacijent.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            //view.Refresh();
        }
    }
}
