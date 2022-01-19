using System;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using System.Data;

namespace Crud_Firebird
{
    public class AcessoFB
    {
        private static readonly AcessoFB instanciaFirebird = new AcessoFB();
        private AcessoFB()
        {

        }

        public static AcessoFB getInstancia()
        {
            return instanciaFirebird;
        }

        public FbConnection getConexao()
        {
            string conn = ConfigurationManager.ConnectionStrings["FirebirdConnectionString"].ToString();
            return new FbConnection(conn);
        }

        public static DataTable fbGetDados()
        {
            using (FbConnection conexaoFirebird = AcessoFB.getInstancia().getConexao())
            {
                try
                {
                    conexaoFirebird.Open();
                    string mSQL = "select * from clientes order by id";
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebird);
                    FbDataAdapter da = new FbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (FbException fbex)
                {
                    throw fbex;
                }
                finally
                {
                    conexaoFirebird.Close();
                }
            }
        }

        public static void fb_InserirDados(Cliente cliente)
        {
            int id=0;
            using (FbConnection conexaoFirebird = AcessoFB.getInstancia().getConexao())
            {
                try
                {
                    conexaoFirebird.Open();
                    string idSQL = "select max(id) from clientes";
                    FbCommand cmdID = new FbCommand(idSQL, conexaoFirebird);
                    FbDataReader dr = cmdID.ExecuteReader();


                    while (dr.Read())
                    {
                        id = Convert.ToInt32(dr[0]) + 1;
                    }                                                    

                    string mSQL = "insert into clientes values (" + id + ",'" + cliente.Nome + "','" + cliente.Endereco + "','" + cliente.Telefone + "','" + cliente.Email + "');" ;
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebird);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    throw fbex;
                }
                finally
                {
                    conexaoFirebird.Close();
                }
            }
        }

        public static void fb_ExcluirDados(int id)
        {
            using (FbConnection conexaoFirebrid = AcessoFB.getInstancia().getConexao())
            {
                try
                {
                    conexaoFirebrid.Open();
                    string mSQL = "delete from clientes where id = " + id;
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebrid);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    throw fbex;
                }
                finally
                {
                    conexaoFirebrid.Close();
                }
            }
        }

        public static Cliente fb_ProcurarDados(int id)
        {
            using (FbConnection conexaoFirebird = AcessoFB.getInstancia().getConexao())
            {
                try
                {
                    conexaoFirebird.Open();
                    string mSQL = "select * from clientes where id = " + id;
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebird);
                    FbDataReader dr = cmd.ExecuteReader();
                    Cliente cliente = new Cliente();

                    while (dr.Read())
                    {
                        cliente.ID = Convert.ToInt32(dr[0]);
                        cliente.Nome = dr[1].ToString();
                        cliente.Endereco = dr[2].ToString();
                        cliente.Telefone = dr[3].ToString();
                        cliente.Email = dr[4].ToString();
                    }
                    return cliente;
                }
                catch (FbException fbex)
                {
                    throw fbex;
                }
                finally
                {
                    conexaoFirebird.Close();
                }
            }
        }

        public static void fb_AlterarDados(Cliente cliente)
        {
            using (FbConnection conexaoFirebird = AcessoFB.getInstancia().getConexao())
            {
                try
                {
                    conexaoFirebird.Open();
                    string mSQL = "update clientes set nome = " + cliente.Nome + ", endereco = " + cliente.Endereco + ", telefone = " + cliente.Telefone + ", email = " + cliente.Email
                        + "where id = " + cliente.ID;
                    FbCommand cmd = new FbCommand(mSQL, conexaoFirebird);
                    cmd.ExecuteNonQuery();
                }
                catch (FbException fbex)
                {
                    throw fbex;
                }
                finally
                {
                    conexaoFirebird.Close();
                }
            }
        }
    }
}
