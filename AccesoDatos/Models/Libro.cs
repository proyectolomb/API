using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos
{
    public class Libro
    {
        public string isbn { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public List<Autor> autores { get; set; }
        public Idioma idioma { get; set; }
        public Editorial editorial { get; set; }
        public List<Categoria> categorias { get; set; }

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        
        public static List<Libro> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Libro> lista = new List<Libro>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT Libro.isbn, Libro.nombre, Libro.Fecha_de_publicacion, Idioma.nombre, Idioma.Id, Editorial.nombre, Editorial.Id FROM Libro JOIN Idioma ON Libro.Idioma = Idioma.id JOIN Editorial ON Libro.Editorial = Editorial.id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Libro l = new Libro();
                        while (reader.Read())
                        {
                            l = new Libro
                            {
                                isbn = reader.GetString(0),
                                nombre = reader.GetString(1),
                                fecha_publicacion = reader.GetDateTime(2),
                                idioma = new Idioma
                                {
                                    id = reader.GetInt32(4),
                                    nombre = reader.GetString(3)
                                },
                                editorial = new Editorial
                                {
                                    id = reader.GetInt32(6),
                                    nombre = reader.GetString(5)
                                },
                                autores = new List<Autor>()

                            };

                            lista.Add(l);

                        }
                    }
                }

                foreach (Libro libro in lista)
                {
                    sql = String.Format("SELECT Autor.Id, Autor.nombre FROM Autor JOIN Libro_Autor ON Autor.id = Libro_Autor.Autor WHERE Libro_Autor.Libro='{0}'", libro.isbn);
                    using (SqlCommand c = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader r = c.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                libro.autores.Add(new Autor
                                {
                                    id = r.GetInt32(0),
                                    nombre = r.GetString(1)
                                });
                            }
                        }
                    }
                }
            
                foreach (Libro libro in lista)
                {
                    sql = String.Format("SELECT Categoria.Id, Categoria.Nombre FROM Categoria JOIN Libro_Categoria ON Categoria.Id = Libro_Categoria.Categoria WHERE Libro_Categoria.Libro='{0}'", libro.isbn);
                    using (SqlCommand c2 = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader r2 = c2.ExecuteReader())
                        {
                            while (r2.Read())
                            {
                                libro.categorias = new List<Categoria>();
                                libro.categorias.Add(new Categoria
                                {
                                    id = r2.GetInt32(0),
                                    nombre = r2.GetString(1)
                                });
                            }
                        }
                    }
                }
                return lista;
            }
        }

        public static bool Create(Libro libro)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                foreach (var autor in libro.autores)
                {
                    String sql1 = String.Format("INSERT INTO Libro_Autor VALUES ({0}, '{1}')", libro.isbn, autor);

                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        connection.Open();

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            return false;
                        }

                    }
                }

                foreach (var categoria in libro.categorias)
                {
                    String sql2 = String.Format("INSERT INTO Libro_Categoria VALUES ({0}, {1})", libro.isbn, categoria);
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        connection.Open();

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            return false;
                        }

                    }

                }
                String sql = String.Format("INSERT INTO Libro VALUES ({0}, {1}, {2}, {3}, {4})", libro.isbn, libro.nombre, libro.fecha_publicacion, libro.idioma.id, libro.editorial.id);
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

        public static Libro GetByISBN(string isbn)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            Libro l = new Libro();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("SELECT Libro.isbn, Libro.nombre, Libro.Fecha_de_publicacion, Idioma.nombre, Idioma.Id, Editorial.nombre, Editorial.Id FROM Libro JOIN Idioma ON Libro.Idioma = Idioma.id JOIN Editorial ON Libro.Editorial = Editorial.id WHERE Libro.ISBN='{0}'", isbn);
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l = new Libro
                            {
                                isbn = reader.GetString(0),
                                nombre = reader.GetString(1),
                                fecha_publicacion = reader.GetDateTime(2),
                                idioma = new Idioma
                                {
                                    id = reader.GetInt32(4),
                                    nombre = reader.GetString(3)
                                },
                                editorial = new Editorial
                                {
                                    id = reader.GetInt32(6),
                                    nombre = reader.GetString(5)
                                },
                                autores = new List<Autor>()

                            };

                        }


                    }
                }
                sql = String.Format("SELECT Autor.Id, Autor.nombre FROM Autor JOIN Libro_Autor ON Autor.id = Libro_Autor.Autor WHERE Libro_Autor.Libro='{0}'", isbn);
                using (SqlCommand c = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader r = c.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            l.autores.Add(new Autor
                            {
                                id = r.GetInt32(0),
                                nombre = r.GetString(1)
                            });
                        }
                    }
                }
                sql = String.Format("SELECT Categoria.Id, Categoria.Nombre FROM Categoria JOIN Libro_Categoria ON Categoria.Id = Libro_Categoria.Categoria WHERE Libro_Categoria.Libro='{0}'", isbn);
                using (SqlCommand c = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader r = c.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Categoria ca = new Categoria();
                            l.categorias = new List<Categoria>();
                            ca.id = r.GetInt32(0);
                            ca.nombre = r.GetString(1);
                            if(ca != null && l != null) l.categorias.Add(ca);
                        }
                    }
                }
            }
            return l;
        }
    }
}
