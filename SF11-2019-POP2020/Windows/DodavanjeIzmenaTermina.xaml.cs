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
    /// Interaction logic for DodavanjeIzmenaTermina.xaml
    /// </summary>
    public partial class DodavanjeIzmenaTermina : Window
    {
        private EStatus odabranStatus;
        private Termin odabranTermin;
        public DodavanjeIzmenaTermina(Termin termin,  EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = termin;

            odabranTermin = termin;
            odabranStatus = status;

            ComboBoxStatusTermina.ItemsSource = Enum.GetValues(typeof(EStatusTermina)).Cast<EStatusTermina>();

            if (status.Equals(EStatus.Izmeni) && termin != null)
            {
                this.Title = "Izmena termina";
            }
            else
            {
                this.Title = "Dodavanje termina";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                //ComboBoxItem item = (ComboBoxItem)ComboBoxPol.SelectedItem;
                //string value = item.Content.ToString();
                //.TryParse(value, out ETipKorisnika tip);
                //ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

                // ComboBoxItem item1 = (ComboBoxItem)ComboBoxPol.SelectedItem;
                // string value1 = item1.Content.ToString();
                // Enum.TryParse(value1, out EPol pol);
                //odabranKorisnik.Aktivan = true;
                Termin t = new Termin()
                {
                    LekarId = int.Parse(txtLekarId.Text),
                    Datum = (DateTime)dateDatum.SelectedDate,
                    StatusTermina = EStatusTermina.SLOBODAN,
                    PacijentId = int.Parse(txtPacijentId.Text),
                    Aktivan = true

                };
                Util.Instance.Termini.Add(t);
                Util.Instance.SacuvajEntitet(t);

                this.Close();
            }
            else
            {

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
