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
    class AdresaService : IAdresaService
    {
        public void deleteAdresa(int id)
        {
            Adresa adr = Util.Instance.Adrese.ToList().Find(adresa => adresa.Id.Equals(id));

            if (adr == null)
                throw new UserNotFoundException($"Ne postoji adresa sa id-om {id}");
            adr.Aktivan = false;

            // updateUser(k);
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Adrese
                                       
                                        SET Aktivan = @Aktivan
                                        where id = @Id";

                command.Parameters.Add(new SqlParameter("Aktivan", adr.Aktivan));
                command.Parameters.Add(new SqlParameter("Id", adr.Id));

                command.ExecuteNonQuery();

            }
        }

        public void readAdrese()
        {

            Util.Instance.Adrese = new ObservableCollection<Adresa>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from adrese where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Adrese.Add(new Adresa
                    {
                        Id = reader.GetInt32(0),
                        Ulica = reader.GetString(1),
                        Broj = reader.GetString(2),
                        Grad = reader.GetString(3),
                        Drzava = reader.GetString(4),
                        Aktivan = reader.GetBoolean(5)
                    });
                }
                reader.Close();

            }
        }

        public int saveAdresa(object obj)
        {
            Adresa adresa = obj as Adresa;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Adrese(ulica,broj,grad,drzava,aktivan)
                       output inserted.id VALUES(@ulica, @broj, @grad, @drzava, @aktivan)";

                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Aktivan", adresa.Aktivan));

                return (int)command.ExecuteScalar();


            }
        
        }

        public void updateAdresa(object obj)
        {

            Adresa adresa = obj as Adresa;
            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"update dbo.Adrese
                                        SET Ulica = @Ulica
                                        SET Broj = @Broj
                                        SET Drzava = @Drzava
                                        SET Grad = @Grad                            
                                        where id = @Id";

                command.Parameters.Add(new SqlParameter("Ulica", adresa.Ulica));
                command.Parameters.Add(new SqlParameter("Broj", adresa.Broj));
                command.Parameters.Add(new SqlParameter("Drzava", adresa.Drzava));
                command.Parameters.Add(new SqlParameter("Grad", adresa.Grad));
                command.Parameters.Add(new SqlParameter("Id", adresa.Id));
                command.Parameters.Add(new SqlParameter("Aktivan", adresa.Aktivan));



                command.ExecuteNonQuery();

            }
        }
    }
}
