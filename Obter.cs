using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entidade;
using Microsoft.Extensions.Configuration;

namespace Dados
{
    public class Obter
    {
        public Conexao.Conexao oConexao = new Conexao.Conexao();

        public List<Entidade.ModelCompraDeGado> ObterCompraDeGado(string idCompraDeGado, string idpecuarista, string dataIni, string dataFim)
        {
            List<ModelCompraDeGado> oModelCompraDeGadoList = new List<ModelCompraDeGado>();

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                try
                {
                    string SqlString = " SELECT A.Id, C.Nome, A.DataEntrega, SUM(D.Preco * B.Quantidade) Total" +
                                       " FROM [CompraDeGado] A, [CompraDeGadoItem] B, [Pecuarista] C, [Animal] D" +
                                       " WHERE A.Id = B.IdCompraGado AND" +
                                       " B.IdAnimal = D.Id AND" +
                                       " A.IdPecuarista = C.Id AND";

                    SqlString += idCompraDeGado != "0" ? " A.Id = " + idCompraDeGado + " AND" : String.Empty;

                    SqlString += idpecuarista != "0" ? " A.IdPecuarista = " + idpecuarista + " AND" : String.Empty;

                    SqlString += !string.IsNullOrEmpty(dataIni) ? " A.DataEntrega >= '" + dataIni + "' AND" + " A.DataEntrega <= '" + dataFim + "' AND" : String.Empty;

                    SqlString = SqlString.Substring(0, SqlString.Length - 4);

                    SqlString += " GROUP BY A.Id, A.IdPecuarista, C.Nome, A.DataEntrega";

                    using (SqlCommand dbCommand = new SqlCommand(SqlString, dbConn))
                    {
                        using (SqlDataReader retorno = dbCommand.ExecuteReader())
                        {
                            while (retorno.Read())
                            {
                                ModelCompraDeGado oCompraDeGado = new ModelCompraDeGado();

                                oCompraDeGado.Id = retorno.GetInt32(0);
                                oCompraDeGado.Pecuarista = retorno.GetString(1);
                                oCompraDeGado.DataEntrega = retorno.GetDateTime(2).ToString("dd/MM/yyyy");
                                oCompraDeGado.ValorTotal = retorno.GetDecimal(3).ToString("n2");

                                oModelCompraDeGadoList.Add(oCompraDeGado);


                            }
                        }
                    }
                }
                catch (SqlException e)
                {

                }

                dbConn.Close();
            }

            return oModelCompraDeGadoList;
        }
        public List<Entidade.ModelCompraDeGadoItem> ObterCompraDeGadoItem(int idCompraDeGado)
        {
            List<ModelCompraDeGadoItem> oModelCompraDeGadoItemList = new List<ModelCompraDeGadoItem>();

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                try
                {
                    string SqlString = " SELECT D.Descricao, B.Quantidade, D.Preco, " +
                                       " (B.Quantidade * D.Preco) ValorTotal, A.Id IdCompraDeGado," +
                                       " B.Id IdCompraDeGadoItem, C.Id IdPecuarista, D.Id IdAnimal, A.DataEntrega, B.IsPrinted FROM " +
                                       " [CompraDeGado] A, [CompraDeGadoItem] B, [Pecuarista] C, [Animal] D " +
                                       " WHERE A.Id = B.IdCompraGado AND " +
                                       " B.IdAnimal = D.Id AND " +
                                       " A.IdPecuarista = C.Id AND " +
                                       " A.Id = " + idCompraDeGado;

                    using (SqlCommand dbCommand = new SqlCommand(SqlString, dbConn))
                    {
                        using (SqlDataReader retorno = dbCommand.ExecuteReader())
                        {
                            while (retorno.Read())
                            {
                                ModelCompraDeGadoItem oCompraDeGadoItem = new ModelCompraDeGadoItem();

                                oCompraDeGadoItem.Descricao = retorno.GetString(0);
                                oCompraDeGadoItem.Quantidade = retorno.GetInt32(1);
                                oCompraDeGadoItem.Preco  = retorno.GetDecimal(2).ToString("n2");
                                oCompraDeGadoItem.ValorTotal = retorno.GetDecimal(3).ToString("n2");
                                oCompraDeGadoItem.IdCompraDeGado = retorno.GetInt32(4);
                                oCompraDeGadoItem.IdCompraDeGadoItem = retorno.GetInt32(5);
                                oCompraDeGadoItem.IdPecuarista = retorno.GetInt32(6);
                                oCompraDeGadoItem.IdAnimal = retorno.GetInt32(7);
                                oCompraDeGadoItem.DataEntrega = retorno.GetDateTime(8).ToString("dd/MM/yyyy");
                                oCompraDeGadoItem.IsPrinted = retorno.GetString(9);

                                oModelCompraDeGadoItemList.Add(oCompraDeGadoItem);
                            }
                        }
                    }
                }
                catch (SqlException)
                {

                }

                dbConn.Close();
            }

            return oModelCompraDeGadoItemList;
        }
        public List<Entidade.ModelPecuarista> ObterPecuarista()
        {
            List<Entidade.ModelPecuarista> oPecuaristaList = new List<Entidade.ModelPecuarista>();

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("SELECT Id, Nome FROM [Pecuarista] ORDER BY NOME", dbConn))
                    {
                        using (SqlDataReader retorno = dbCommand.ExecuteReader())
                        {
                            while (retorno.Read())
                            {
                                ModelPecuarista oPecuarista = new ModelPecuarista();
                                oPecuarista.Id = retorno.GetInt32(0);
                                oPecuarista.Nome = retorno.GetString(1);

                                oPecuaristaList.Add(oPecuarista);
                            }
                        }
                    }
                }
                catch (SqlException)
                {
                       
                }

                dbConn.Close();
            }

            return oPecuaristaList;
        }
        public List<Entidade.ModelAnimal> ObterAnimal()
        {
            List<Entidade.ModelAnimal> oAnimalList = new List<Entidade.ModelAnimal>();

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                try
                {
                    using (SqlCommand dbCommand = new SqlCommand("SELECT Id, Descricao, Preco FROM [Animal] ORDER BY DESCRICAO", dbConn))
                    {
                        using (SqlDataReader retorno = dbCommand.ExecuteReader())
                        {
                            while (retorno.Read())
                            {
                                ModelAnimal oAnimal = new ModelAnimal();

                                oAnimal.Id = retorno.GetInt32(0);
                                oAnimal.Descricao = retorno.GetString(1) + " - " + retorno.GetDecimal(2).ToString("n2");
                                oAnimal.Preco = retorno.GetDecimal(2);

                                oAnimalList.Add(oAnimal);
                            }
                        }
                    }
                }
                catch (SqlException)
                {

                }

                dbConn.Close();
            }

            return oAnimalList;
        }
    }
}
