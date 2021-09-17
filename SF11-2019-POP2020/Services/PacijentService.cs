using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.MyExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    class PacijentService : IPacijentService
    {
        public void deletePacijenta(int id)
        {
            throw new NotImplementedException();
        }

        public void deletePacijentaZapravo(int id)
        {
            throw new NotImplementedException();
        }

        public void readPacijente()
        {
            Util.Instance.Pacijenti = new ObservableCollection<Pacijent>();
            //Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();
            //Util.Instance.Lekari = new ObservableCollection<Lekar>();


            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Pacijenti where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Pacijenti.Add(new Pacijent
                    {
                        Id = reader.GetInt32(0),
                        Korisnik = Util.Instance.korisnikPoId(reader.GetInt32(1)),
                        //ListaTerapija = new ObservableCollection<Terapija>(),
                        Termini = reader.GetString(2),
                        Aktivan = reader.GetBoolean(3),
                        ListaTerapija = new ObservableCollection<Terapija>(),
                });
                }
                reader.Close();
            }
        }

        public int savePacijenta(object obj)
        {
            throw new NotImplementedException();
        }

        public void updatePacijenta(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
