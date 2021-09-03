using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private string stringConexao = "Data source=DESKTOP-20INV7D\\SQLEXPRESS; initial catalog=CATALOGO; user Id=sa; pwd=SenaiSamuel1";

        public void AtualizarIdUrl(int idFilme, FilmeDomain filmeAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE FILME SET idGenero = @idGenero, tituloFilme = @tituloFilme WHERE idFilme = @idFilme";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", filmeAtualizado.idGenero);
                    cmd.Parameters.AddWithValue("@tituloFilme", filmeAtualizado.tituloFilme);
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FilmeDomain BuscarPorId(int idFilme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idGenero, tituloFilme, idFilme FROM FILME WHERE idFilme = @idFilme";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        FilmeDomain filmeBuscado = new FilmeDomain
                        {
                            idFilme = Convert.ToInt32(reader["idFilme"]),

                            idGenero = Convert.ToInt32(reader["idGenero"]),

                            tituloFilme = reader["tituloFilme"].ToString()
                        };

                        return filmeBuscado;
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto tituloFilme com as informações que serão cadastradas.</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //string queryInsert = "INSERT INTO FILME (idGenero,tituloFilme) VALUES ('" + novoFilme.idGenero + "','" + novoFilme.tituloFilme + "')"; // Dessa forma permite SQL Injection
                string queryInsert = "INSERT INTO FILME (idGenero,tituloFilme) VALUES (@idGenero,@tituloFilme)";

                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", novoFilme.idGenero);
                    cmd.Parameters.AddWithValue("@tituloFilme", novoFilme.tituloFilme);

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idFilme)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM FILME WHERE idFilme = @idFilme";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idFilme, idGenero, tituloFilme FROM FILME";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll,con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(rdr[0]),
                            idGenero = Convert.ToInt32(rdr[1]),
                            tituloFilme = rdr[2].ToString()
                        };

                        listaFilmes.Add(filme);
                    }
                }
            }
            return listaFilmes;
        }
    }
}
