using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for loginLekar.xaml
    /// </summary>
    public partial class loginLekar : Window
    {
        public loginLekar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;
                                                    Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                                    TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                                    MultiSubnetFailover=False");
            string query = "Select * from korisnici where TipKorisnika like 'lekar' and jmbg ='" + txtJmbg.Text.Trim() + "' and lozinka ='" + txtLozinka.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlConn);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                SviLekari svilekari = new SviLekari();
                this.Hide();
                svilekari.Show();
            }
            else
            {
                MessageBox.Show("Netacni podaci za prijavu! Proveri ih ponovo!");
            }
        }
    }
}
