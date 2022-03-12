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
    /// Logic for DodavanjeIzmenaLekar.xaml
    /// </summary>
    public partial class DodavanjeIzmenaLekar : Window
    {

        private EStatus odabranStatus;
        private Korisnik odabranLekar;
        public DodavanjeIzmenaLekar(Korisnik lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = lekar;

            odabranLekar = lekar;
            odabranStatus = status;
            ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            ComboBoxPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();

            if(status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni lekara";
                txtJmbg.IsEnabled = false;
            }
            else
            {
                this.Title = "Dodaj lekara";
            }
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (!Validation.GetHasError(txtEmail) && !Validation.GetHasError(txtIme))
            {
                if (odabranStatus.Equals(EStatus.Dodaj))
                {
                    odabranLekar.Aktivan = true;
                    Lekar lekar = new Lekar
                    {
                        Id = odabranLekar.Id,
                        Termini = "nema",
                        Aktivan = true
                    };

                    Util.Instance.Korisnici.Add(odabranLekar);
                    Util.Instance.Lekari.Add(lekar);

                    int id = Util.Instance.SacuvajEntitet(odabranLekar);
                    lekar.Id = id;

                    Util.Instance.SacuvajEntitet(lekar);
                }
                this.DialogResult = true;
                this.Close();
            }
        }
            

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
