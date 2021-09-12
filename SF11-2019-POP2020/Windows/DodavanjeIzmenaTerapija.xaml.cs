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
    /// Interaction logic for DodavanjeIzmenaTerapija.xaml
    /// </summary>
    ///  
   
    public partial class DodavanjeIzmenaTerapija : Window
    {

        private EStatus odabranStatus;
        private Terapija odabranaTerapija;
        public DodavanjeIzmenaTerapija(Terapija terapija, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = terapija;

            odabranaTerapija = terapija;
            odabranStatus = status;


            if (status.Equals(EStatus.Izmeni) && terapija != null)
            {
                this.Title = "Izmeni Terapiju";
                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(lekar);
                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(termin);

            }
            else
            {
                this.Title = "Dodaj terapiju";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Terapija terap = new Terapija()
                {
                    Opis = txtOpis.Text,
                    Lekar = Util.Instance.lekarPoId(int.Parse(txtLekarId.Text)),
                    Aktivan = true

                };
                Util.Instance.Terapije.Add(terap);
                Util.Instance.SacuvajEntitet(terap);

                this.Close();
            }
            else if (odabranStatus.Equals(EStatus.Izmeni))
            {
                Util.Instance.UpdateEntiteta(odabranaTerapija);
            }

            this.Close();
        }
    }
}
