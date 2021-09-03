using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = "Data source=DESKTOP-20INV7D\\SQLEXPRESS; initial catalog=T_Rental; user Id=sa; pwd=SenaiSamuel1";

        public void AtualizarIdUrl(int idVeiculo, VeiculoDomain veiculoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE VEICULO SET idEmpresa = @idEmpresa, idModelo = @idModelo, placa = @placa WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", veiculoAtualizado.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", veiculoAtualizado.idModelo);
                    cmd.Parameters.AddWithValue("@placa", veiculoAtualizado.placa);
                    cmd.Parameters.AddWithValue("@idVeiculo", veiculoAtualizado.idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VeiculoDomain BuscarPorId(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idEmpresa, idModelo, placa, idVeiculo FROM VEICULO WHERE idVeiculo = @idVeiculo";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        VeiculoDomain veiculoBuscado = new VeiculoDomain
                        {
                            idVeiculo = Convert.ToInt32(reader["idVeiculo"]),

                            idEmpresa = Convert.ToInt32(reader["idEmpresa"]),

                            idModelo = Convert.ToInt32(reader["idModelo"]),

                            placa = reader["placa"].ToString()
                        };

                        return veiculoBuscado;
                    }

                    return null;
                }
            }
        }    

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO VEICULO (idEmpresa,idModelo,placa) VALUES (@idEmpresa,@idModelo,@placa)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", novoVeiculo.idModelo);
                    cmd.Parameters.AddWithValue("@placa", novoVeiculo.placa);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM VEICULO WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idVeiculo, idEmpresa, idModelo, placa FROM VEICULO";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(reader[0]),
                            idEmpresa = Convert.ToInt32(reader[1]),
                            idModelo = Convert.ToInt32(reader[2]),
                            placa = reader[3].ToString()
                        };

                        listaVeiculos.Add(veiculo);
                    }
                }
            }
            return listaVeiculos;
        }
    }
}
