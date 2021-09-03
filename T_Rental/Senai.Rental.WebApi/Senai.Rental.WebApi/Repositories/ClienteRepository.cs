using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private string stringConexao = "Data source=DESKTOP-20INV7D\\SQLEXPRESS; initial catalog=T_Rental; user Id=sa; pwd=SenaiSamuel1";

        public void AtualizarIdUrl(int idCliente, ClienteDomain clienteAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE CLIENTE SET nomeCliente = @nomeCliente, sobrenomeCliente = @sobrenomeCliente, rgCliente = @rgCliente, cpfCliente = @cpfCliente WHERE idCliente = @idCliente";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@nomeCliente", clienteAtualizado.nomeCliente);
                    cmd.Parameters.AddWithValue("@sobrenomeCliente", clienteAtualizado.sobrenomeCliente);
                    cmd.Parameters.AddWithValue("@rgCliente", clienteAtualizado.rgCliente);
                    cmd.Parameters.AddWithValue("@cpfCliente", clienteAtualizado.cpfCliente);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ClienteDomain BuscarPorId(int idCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idCliente, nomeCliente, sobrenomeCliente, rgCliente, cpfCliente FROM CLIENTE WHERE idCliente = @idCliente";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    reader = cmd.ExecuteReader();

                    if (reader.Read()) 
                    {
                        ClienteDomain clienteBuscado = new ClienteDomain
                        {
                            idCliente = Convert.ToInt32(reader["idCliente"]),

                            nomeCliente = reader["nomeCliente"].ToString(),

                            sobrenomeCliente = reader["sobrenomeCliente"].ToString(),

                            rgCliente = reader["rgCliente"].ToString(),

                            cpfCliente = reader["cpfCliente"].ToString()
                        };

                        return clienteBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(ClienteDomain novoCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO CLIENTE (nomeCliente,sobrenomeCliente,rgCliente,cpfCliente) VALUES (@nomeCliente,@sobrenomeCliente,@rgCliente,@cpfCliente)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeCliente", novoCliente.nomeCliente);
                    cmd.Parameters.AddWithValue("@sobrenomeCliente", novoCliente.sobrenomeCliente);
                    cmd.Parameters.AddWithValue("@rgCliente", novoCliente.rgCliente);
                    cmd.Parameters.AddWithValue("@cpfCliente", novoCliente.cpfCliente);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM CLIENTE WHERE idCliente = @idCliente";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClienteDomain> ListarTodos()
        {
            List<ClienteDomain> listaClientes = new List<ClienteDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idCliente, nomeCliente, sobrenomeCliente, rgCliente, cpfCliente FROM CLIENTE";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClienteDomain cliente = new ClienteDomain()
                        {
                            idCliente = Convert.ToInt32(reader[0]),
                            nomeCliente = reader[1].ToString(),
                            sobrenomeCliente = reader[2].ToString(),
                            rgCliente = reader[3].ToString(),
                            cpfCliente = reader[4].ToString()
                        };

                        listaClientes.Add(cliente);
                    }
                }
            }
            return listaClientes;
        }
    }
}
