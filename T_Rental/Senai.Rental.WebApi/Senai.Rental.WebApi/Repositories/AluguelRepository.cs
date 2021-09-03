using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private string stringConexao = "Data source=DESKTOP-20INV7D\\SQLEXPRESS; initial catalog=T_Rental; user Id=sa; pwd=SenaiSamuel1";

        public void AtualizarIdUrl(int idAluguel, AluguelDomain aluguelAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE ALUGUEL SET idCliente = @idCliente, idVeiculo = @idVeiculo, dataRetirada = @dataRetirada, dataDevolucao = @dataDevolucao WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", aluguelAtualizado.idCliente);
                    cmd.Parameters.AddWithValue("@idVeiculo", aluguelAtualizado.idVeiculo);
                    cmd.Parameters.AddWithValue("@dataRetirada", aluguelAtualizado.dataRetirada);
                    cmd.Parameters.AddWithValue("@dataDevolucao", aluguelAtualizado.dataDevolucao);
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public AluguelDomain BuscarPorId(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idCliente, idVeiculo, dataRetirada, dataDevolucao, idAluguel FROM ALUGUEL WHERE idAluguel = @idAluguel";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain
                        {
                            idAluguel = Convert.ToInt32(reader["idAluguel"]),

                            idCliente = Convert.ToInt32(reader["idCliente"]),

                            idVeiculo = Convert.ToInt32(reader["idVeiculo"]),

                            dataRetirada = Convert.ToDateTime(reader["dataRetirada"]),

                            dataDevolucao = Convert.ToDateTime(reader["dataDevolucao"])
                        };

                        return aluguelBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ALUGUEL (idCliente,idVeiculo,dataRetirada,dataDevolucao) VALUES (@idCliente,@idVeiculo,@dataRetirada,@dataDevolucao)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", novoAluguel.idCliente);
                    cmd.Parameters.AddWithValue("@idVeiculo", novoAluguel.idVeiculo);
                    cmd.Parameters.AddWithValue("@dataRetirada", novoAluguel.dataRetirada);
                    cmd.Parameters.AddWithValue("@dataDevolucao", novoAluguel.dataDevolucao);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ALUGUEL WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AluguelDomain> ListarTodos()
        {
            List<AluguelDomain> listaAlugueis = new List<AluguelDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idAluguel, idCliente, idVeiculo, dataRetirada, dataDevolucao FROM ALUGUEL";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(reader[0]),
                            idCliente = Convert.ToInt32(reader[1]),
                            idVeiculo = Convert.ToInt32(reader[2]),
                            dataRetirada = Convert.ToDateTime(reader[3]),
                            dataDevolucao = Convert.ToDateTime(reader[4])
                        };

                        listaAlugueis.Add(aluguel);
                    }
                }
            }
            return listaAlugueis;
        }
    }
}
