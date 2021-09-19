using SF11_2019_POP2020.Models;
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

namespace SF11_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for IzmenaLicnihPodataka.xaml
    /// </summary>
    public partial class IzmenaLicnihPodataka : Window
    {
        private Korisnik ciljaniKorisnik;
        private Adresa novaAdresa;
        private EStatus odabranStatus;

        public IzmenaLicnihPodataka(Korisnik korisnik)
        {
            InitializeComponent();

            this.DataContext = korisnik;
            ciljaniKorisnik = korisnik;
            novaAdresa = korisnik.Adresa;

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Util.Instance.updateLicnihPodataka(ciljaniKorisnik);
            this.Close();
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa, EStatus.Izmeni);
            //addAdresa.Show();
            if ((bool)addAdresa.ShowDialog())
            {

            }
        }
    }
}
