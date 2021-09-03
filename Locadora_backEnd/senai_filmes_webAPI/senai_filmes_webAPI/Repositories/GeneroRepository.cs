using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    /// <summary>
    /// Clasee responsável pelo repositório dos generos
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros
        /// Data Source= Nome do Servidor
        /// initial catalog = Nome do Banco de Dados
        /// user ID= sa; pwd= SenaiSamuel1 = Faz autenticação com o SQL SERVER passando o Login e Senha
        /// integrated security=true = Faz autenticação com o usuario do sistema (Windows)
        /// </summary>
        private string stringConexao = "Data source=DESKTOP-20INV7D\\SQLEXPRESS; initial catalog=CATALOGO; user Id=sa; pwd=SenaiSamuel1";
        //private string stringConexao = "Data Source=DESKTOP-U20H53U; initial catalog=catalogo_tarde; integrated security=true";
        public void AtualizarIdUrl(int idGenero, GeneroDomain generoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE GENERO SET nomeGenero = @nomeGenero WHERE idGenero = @idGenero";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", generoAtualizado.nomeGenero);
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public GeneroDomain BuscarPorId(int idGenero)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeGenero, idGenero FROM GENERO WHERE idGenero = @idGenero";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        GeneroDomain generoBuscado = new GeneroDomain
                        {
                            idGenero = Convert.ToInt32(reader["idGenero"]),

                            nomeGenero = reader["nomeGenero"].ToString()
                        };

                        return generoBuscado;
                    }

                    return null;
                }
                 
            }
        }

        public void Cadastrar(GeneroDomain novoGenero)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                // "INSERT INTO GENERO (nomeGenero) VALUES ('Joana D'Arc')"    // erro de sintaxe, com o efeito Joana D'Arc
                // "INSERT INTO GENERO (nomeGenero) VALUES ('')DROP TABLE FILME--')"   // permite SQL Injection

                // Não usar dessa forma, pois pode causar o efeito Joana D'Arc
                // Além de permitir SQL Injection
                // Por exemplo:
                // "nomeGenero" : "')DROP TABLE FILME--"
                // Ao tentar cadastrar usando o comando acima, irá deletar a tabela FILME do banco de dados
                
                string queryInsert = "INSERT INTO GENERO (nomeGenero) VALUES (@nomeGenero)";

                con.Open();

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeGenero", novoGenero.nomeGenero);

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idGenero)
        {
            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Define a query a ser executada passando o id do gênero como parâmetro
                string queryDelete = "DELETE FROM GENERO WHERE idGenero = @idGenero";

                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Define o valor do id recebido no método como o valor do parâmetro @idGenero
                    cmd.Parameters.AddWithValue("@idGenero", idGenero);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os gêneros  
        /// </summary>
        /// <returns>Uma lista de gêneros</returns>
        public List<GeneroDomain> ListarTodos()
        {
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            //Declara a SqlConnection con passando a string de conexao como Parametro.
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idGenero, nomeGenero FROM GENERO";

                //Abre a conexão com o banco de dados.
                con.Open();

                //Declarando  SqlDataReader rdr percorrer a tabela do nosso banco de dados.
                SqlDataReader rdr;

                //Declara o SqlCommand passando da query que será executada e a conexão com parametros.
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //executa a query e armeza os dados no rdr. 
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registros para serem lidos no rdr, o laço se repete.
                    while (rdr.Read())
                    {
                        //instancia um objeto genero do tipo GeneroDomain
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //atribui a propriedade idGenero o valor da primeira coluna do bando de dados.
                            idGenero = Convert.ToInt32(rdr[0]),
                            nomeGenero = rdr[1].ToString()
                        };

                        //adiciona o objeto genero criado a lista listaGeneros
                        listaGeneros.Add(genero);
                    }
                }
            }
            return listaGeneros;
        }
    }
}
