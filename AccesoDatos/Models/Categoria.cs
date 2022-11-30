using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos.Models
{
    public class Categoria
    {
        public int id;
        public string nombre;

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        private int v1;
        private string v2;

       

        public static List<Categoria> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Categoria";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria d = new Categoria
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),

                            };
                            lista.Add(d);
                        }
                    }
                }
            }
            return lista;
        }

        public static Categoria GetById(long id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Categoria WHERE id=" + id;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria e = new Categoria
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1)
                            };
                            return e;
                        }

                    }
                }
            }
            return null;
        }

        public static List<Categoria> GetByName(string name)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Categoria WHERE nombre='" + name + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria e = new Categoria
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1)
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            return lista;
        }

        public static bool Create(Categoria e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("INSERT INTO Categoria VALUES ({0}, '{1}')", e.id, e.nombre);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        public static bool Delete(int id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("DELETE FROM Categoria WHERE id={0}", id);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        public static bool Update(Categoria e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("UPDATE Categoria SET nombre='{0}' WHERE id={1}", e.nombre, e.id);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }

        }
    }
}

