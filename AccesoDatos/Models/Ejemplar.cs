using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos.Models
{
    public class Ejemplar
    {
        public string codigo { get; set; }
        public  int estanteria { get; set; }
        public  int balda { get; set; }
        public string nombreLibro { get; set; }


        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static List<Ejemplar> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Ejemplar> lista = new List<Ejemplar>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT Ejemplar.Codigo, Ejemplar.Estanteria, Ejemplar.Balda, Libro.Nombre FROM Ejemplar JOIN Libro ON Libro.ISBN = Ejemplar.ISBN WHERE Libro.ISBN = Ejemplar.ISBN;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ejemplar e = new Ejemplar()
                            {
                                codigo = reader.GetString(0),
                                estanteria = reader.GetInt32(1),
                                balda = reader.GetInt32(2),
                                nombreLibro = reader.GetString(3)
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            return lista;
        }

        public static List<Ejemplar> GetByISBN(string isbn)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran"; 
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Ejemplar> lista = new List<Ejemplar>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("SELECT Codigo, Estanteria, Balda FROM Ejemplar WHERE ISBN='{0}'", isbn);
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ejemplar e = new Ejemplar()
                            {
                                codigo = reader.GetString(0),
                                estanteria = reader.GetInt32(1),
                                balda = reader.GetInt32(2),
                            };
                            lista.Add(e);
                        }
                    }
                }
            }


            return lista;
        }
    }
}
