using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos
{
    public class Autor
    {
        public int id;
        public string nombre;
        public string pais_origen;
        public List<Categoria> categorias;  
        public Editorial editorial;
        

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static List<Autor> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Autor> lista = new List<Autor>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Autor";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autor d = new Autor
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                pais_origen = getPaisOrigen(),
                                categorias = getCategorias(),
                                editorial = getEditorial()
                                    
                            };

                            lista.Add(d);
                        }
                    }
                }
            }
            return lista;
        }
        public static Editorial getEditorial()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(0, 2);
            switch (value)
            {
                case 0:
                    return new Editorial { id = 1, nombre="Alfajara" };
                case 1:
                    return null;
            }
            return null;
        }

        public static string getPaisOrigen()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(0, 5);
            switch (value)
            {
                case 0:
                    return "Brasil";
                case 1:
                    return "España";
                case 2:
                    return "Argentina";
                case 3:
                    return "Francia";
                case 4:
                    return "Japón";
                case 5:
                    return "Inglaterra";
            }
            return "China";
        }

        public static List<Categoria> getCategorias()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(0, 5);
            List<Categoria> a = new List<Categoria>();
            switch (value)
            {
                case 0:
                    a.Add(new Categoria { id = 1, nombre = "Terror" });
                    a.Add(new Categoria{id = 2, nombre = "Acción"});
                    break; 
                case 1:
                    a.Add(new Categoria{ id = 2, nombre = "Acción" });
                    break;
                case 2:
                    a.Add(new Categoria{ id = 3, nombre = "Acción" });
                    break;
                case 3:
                    a.Add(new Categoria{ id = 1, nombre = "Terror" });
                    a.Add(new Categoria{ id = 2, nombre = "Acción" });
                    a.Add(new Categoria{ id = 3, nombre = "Aventura" });
                    break;
                case 4:
                    a.Add(new Categoria{ id = 3, nombre = "Aventura" });
                    break;
                case 5:
                    a.Add(new Categoria{ id = 3, nombre = "Aventura" });
                    a.Add(new Categoria{ id = 2, nombre = "Acción" });
                    break;
            }
            return a;
        }

        public static Autor GetById(long id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Autor WHERE id=" + id;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autor e = new Autor
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

        public static List<Autor> GetByName(string name)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Autor> lista = new List<Autor>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM Autor WHERE nombre='" + name + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autor e = new Autor
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

        public static bool Create(Autor e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("INSERT INTO Autor VALUES ({0}, '{1}')", e.id, e.nombre);

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
                String sql = String.Format("DELETE FROM Autor WHERE id={0}", id);

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

        public static bool Update(Autor e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("UPDATE Autor SET nombre='{0}' WHERE id={1}", e.nombre, e.id);

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
