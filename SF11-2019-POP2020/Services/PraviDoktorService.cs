using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    class PraviDoktorService : IPraviDoktorService
    {
        public void deleteDoktora(string jmbg)
        {
            throw new NotImplementedException();
        }

        public void deleteUserZapravo(int id)
        {
            throw new NotImplementedException();
        }

        public void readDoktore()
        {
            Util.Instance.Doktori = new ObservableCollection<Lekar>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Lekari where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Doktori.Add(new Lekar
                    {
                        Id = reader.GetInt32(0),
                        DomZdravljaId = reader.GetInt32(1),
                        //Termini = reader.GetString(2),
                        Aktivan = reader.GetBoolean(3)
                    });
                }
                reader.Close();
            }
        }

        public int saveDoktora(object obj)
        {
            Lekar lekar = obj as Lekar;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Lekari(Id, DomZdravljaId, Termini, Aktivan)
                       output inserted.id VALUES(@Id, @LekarId, @Termini, @Aktivan)";

                command.Parameters.Add(new SqlParameter("Id", lekar.Id));
                command.Parameters.Add(new SqlParameter("DomZdravljaId", lekar.DomZdravljaId));
                command.Parameters.Add(new SqlParameter("Termini", lekar.Termini));
                command.Parameters.Add(new SqlParameter("Aktivan", lekar.Aktivan));

                return (int)command.ExecuteScalar();


            }
        }

        public void updateDoktora(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
