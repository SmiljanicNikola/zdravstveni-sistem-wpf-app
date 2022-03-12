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
    /// Interaction logic for DodavanjeTerapijaVerzija2.xaml
    /// </summary>
    public partial class DodavanjeTerapijaVerzija2 : Window
    {
        private EStatus odabranStatus;
        private Terapija odabranaTerapija;
        public DodavanjeTerapijaVerzija2(Terapija terapija, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = terapija;

            odabranaTerapija = terapija;
            odabranStatus = status;
            if (status.Equals(EStatus.Izmeni) && terapija != null)
            {
                this.Title = "Izmeni Terapiju";
            }
            else
            {

                this.Title = "Dodaj terapiju";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string jmbg = PacijentiLekara.jmbg;
            Lekar lekar = Util.Instance.lekarPoJmbg(jmbg);
            Pacijent pacijent = PacijentiLekara.izabraniPacijent;
            if (lekar != null && pacijent != null)
            {
                Terapija terapija = new Terapija()
                {
                    Opis = txtOpis.Text,
                    Lekar = lekar,
                    Pacijent = pacijent,
                    Aktivan = true

                };

                int id = Util.Instance.SacuvajEntitet(terapija);
                terapija.Id = id;
                Util.Instance.Terapije.Add(terapija);

                this.Close();
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
