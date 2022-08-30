using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Entidade;
using Microsoft.Identity.Client;
using System.Windows;

namespace Dados
{
    public class Alterar
    {
        public Conexao.Conexao oConexao = new Conexao.Conexao();

        public bool ExcluirCompraDeGado(int idCompraDeGado)
        {

            int retorno = 0;

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand(" DELETE FROM[CompraDeGadoItem] WHERE IdCompraGado = " + idCompraDeGado +
                                                                     " DELETE FROM [CompraDeGado] WHERE Id = " + idCompraDeGado, dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            retorno = dbCommand.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();
                    }

                    dbTrans.Commit();
                }

                dbConn.Close();
            }

            return retorno > 0 ? true : false;
        }
        public bool ExcluirCompraDeGadoItem(int idCompraDeGadoItem)
        {

            int retorno = 0;

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand(" DELETE FROM [CompraDeGadoItem] WHERE Id = " + idCompraDeGadoItem, dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            retorno = dbCommand.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();
                    }

                    dbTrans.Commit();
                }

                dbConn.Close();
            }

            return retorno > 0 ? true : false;
        }
        public bool Adicionar(List<Entidade.CompraDeGado> compraDeGado)
        {
            int iCount = 0;
            int IdRetorno = 0;
            int retorno = 0;

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in compraDeGado)
                        {
                            ++iCount;

                            if (iCount == 1)
                            {
                                using (SqlCommand dbCommand = new SqlCommand("INSERT INTO [dbo].[CompraDeGado] ([IdPecuarista], [DataEntrega])" +
                                                                             " Output Inserted.Id VALUES (@IdPecuarista, @DataEntrega)", dbConn))
                                {
                                    dbCommand.Transaction = dbTrans;

                                    dbCommand.Parameters.Add("IdPecuarista", SqlDbType.VarChar).Value = item.IdPecuarista;
                                    dbCommand.Parameters.Add("DataEntrega", SqlDbType.DateTime).Value = Convert.ToDateTime(item.DataEntrega);

                                    IdRetorno = (int)dbCommand.ExecuteScalar();
                                }
                            }

                            using (SqlCommand dbCommand = new SqlCommand("INSERT INTO [dbo].[CompraDeGadoItem] ([IdCompraGado], [IdAnimal], [Quantidade]) " +
                                                                        "VALUES (@IdCompraGado, @IdAnimal, @Quantidade)", dbConn))
                            {
                                dbCommand.Transaction = dbTrans;

                                dbCommand.Parameters.Add("IdCompraGado", SqlDbType.VarChar).Value = IdRetorno;
                                dbCommand.Parameters.Add("IdAnimal", SqlDbType.VarChar).Value = item.IdAnimal;
                                dbCommand.Parameters.Add("Quantidade", SqlDbType.VarChar).Value = item.Quantidade;

                                retorno = dbCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();
                    }

                    dbTrans.Commit();
                }

                dbConn.Close();
            }

            return retorno > 0 ? true : false;
        }

        public bool Salvar(List<Entidade.CompraDeGado> compraDeGado)
        {
            int retornoDel = 0;
            int retornoIns = 0;
            int count = 0;

            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in compraDeGado)
                        {
                            count++;

                            if (count == 1)
                            {
                                using (SqlCommand dbCommand = new SqlCommand(" DELETE FROM [CompraDeGadoItem] WHERE IdCompraGado = " + item.IdCompraDeGado, dbConn))
                                {
                                    dbCommand.Transaction = dbTrans;

                                    retornoDel = dbCommand.ExecuteNonQuery();
                                }
                            }

                            if (retornoDel > 0)
                            {
                                using (SqlCommand dbCommand = new SqlCommand(" INSERT INTO[dbo].[CompraDeGadoItem]([IdCompraGado], [IdAnimal], [Quantidade]) " +
                                                                             " VALUES (@IdCompraGado, @IdAnimal, @Quantidade)", dbConn))
                                {
                                    dbCommand.Transaction = dbTrans;

                                    dbCommand.Parameters.Add("IdCompraGado", SqlDbType.VarChar).Value = item.IdCompraDeGado;
                                    dbCommand.Parameters.Add("IdAnimal", SqlDbType.VarChar).Value = item.IdAnimal;
                                    dbCommand.Parameters.Add("Quantidade", SqlDbType.VarChar).Value = item.Quantidade;

                                    retornoIns = dbCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();
                    }

                    dbTrans.Commit();
                }

                dbConn.Close();
            }

            return retornoIns > 0 ? true : false; ;
        }

        public bool AtualizarIsPrinted(int idCompraDeGado)
        {
            int retorno = 0;
            
            using (SqlConnection dbConn = new SqlConnection(oConexao.ObterConexao()))
            {
                dbConn.Open();

                using (SqlTransaction dbTrans = dbConn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand dbCommand = new SqlCommand(" UPDATE [CompraDeGadoItem] SET [IsPrinted] = '1'  WHERE IdCompraGado = " + idCompraDeGado, dbConn))
                        {
                            dbCommand.Transaction = dbTrans;

                            retorno = dbCommand.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException)
                    {
                        dbTrans.Rollback();
                    }

                    dbTrans.Commit();
                }

                dbConn.Close();
            }

            return retorno > 0 ? true : false; ;
        }
    }
}
