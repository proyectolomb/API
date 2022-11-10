using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos
{
    public class Curso
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static List<Curso> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Curso> lista = new List<Curso>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Curso";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Curso d = new Curso
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

        public static Curso GetById(long id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Curso WHERE id=" + id;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Curso e = new Curso
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

        public static List<Curso> GetByName(string name)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Curso> lista = new List<Curso>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Curso WHERE nombre='" + name + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Curso e = new Curso
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

        public static bool Create(Curso e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("INSERT INTO Curso VALUES ({0}, '{1}')", e.id, e.nombre);

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
                String sql = String.Format("DELETE FROM Curso WHERE id={0}", id);

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

        public static bool Update(Curso e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("UPDATE Curso SET nombre='{0}' WHERE id={1}", e.nombre, e.id);

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

