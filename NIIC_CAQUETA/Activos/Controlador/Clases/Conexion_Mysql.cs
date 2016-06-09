using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Activos.Clases
{
    class Conexion_Mysql
    {

        public String Tiempo = "";
        //MySqlConnection Conexion = null;

        public string cad_Conexion()
        {
            string retValue = string.Empty;
            retValue = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["conexion_ActivoFijo"].ConnectionString;
            return retValue + Tiempo;
        }

        public MySqlConnection ConectarMysql() //Metoto para conectar a C# a MySQL
        {
            string CadenaConexion;
            CadenaConexion = cad_Conexion();
            MySqlConnection Conexion = new MySqlConnection(CadenaConexion);
            try
            {
                Conexion.Open();
            }
            catch (Exception)
            {
                //   MessageBox.Show("Error de configuración del sistema " + error.Message, "Aplicación : ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //   Application.Exit();
            }
            return Conexion;
        }

        public System.Data.DataSet EjecutarSelectMysql(String Select)//Metodo para ejecutar Select 
        {
            //MySqlDataReader
            try
            {
                System.Data.DataSet dt = new System.Data.DataSet();

                MySqlConnection Conn = ConectarMysql();
                MySqlCommand ComandoSelect = new MySqlCommand(Select);
                ComandoSelect.Connection = Conn;

                // MySqlDataReader Resultado;
                MySqlDataAdapter da = new MySqlDataAdapter(Select, Conn);
                //da = ComandoSelect.ExecuteReader();
                da.Fill(dt);
                Conn.Close();
                return dt;
            }
            catch (Exception) { }
            return null;
        }
        public Boolean Grabar_mysql(string cadena)
        {
            MySqlConnection con = ConectarMysql();
            MySqlCommand cmd = new MySqlCommand(cadena, con);
            if (cmd.ExecuteNonQuery() > 0) //Ejecuta una instrucción SQL en Connection y devuelve el número de filas afectadas.
            {

                //myTrans.Commit();//Confirma la transacción de base de datos.
                con.Close();
                return true;//Retorna un  mensaje de exito 
            }
            else
            {
                con.Close();
                return false;
            }
        }
        public String Sql(List<String> cadena)//Metodo para ejecutar Select 
        {
            MySqlConnection con = ConectarMysql();
            MySqlTransaction transa = con.BeginTransaction();
            MySqlCommand cmd;

            try
            {

                for (int i = 0; i < cadena.Count; i++)
                {
                    if (cadena[i].Length > 0)
                    {
                        cmd = new MySqlCommand(cadena[i], con);
                        cmd.Transaction = transa;
                        cmd.ExecuteNonQuery();
                    }

                }
                transa.Commit();
                return "OPERACION EXITOSA";
            }
            catch (Exception e)
            {
                transa.Rollback();
                return "OPERACION NO EXITOSA." + e.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        public String Sql(string[] cadena)//Metodo para ejecutar Select 
        {
            MySqlConnection con = ConectarMysql();
            MySqlTransaction transa = con.BeginTransaction();
            MySqlCommand cmd;

            try
            {

                for (int i = 0; i < cadena.Length; i++)
                {
                    if (cadena[i].Length > 0)
                    {
                        cmd = new MySqlCommand(cadena[i], con);
                        cmd.Transaction = transa;
                        cmd.ExecuteNonQuery();
                    }

                }
                transa.Commit();
                return "OPERACION EXITOSA";
            }
            catch
            {
                transa.Rollback();
                return "OPERACION NO EXITOSA.";
            }
            finally
            {
                con.Close();
            }
        }



        public bool EjecutarOperacion(string Sentencia)
        {
            MySqlConnection mySqlConnection = ConectarMysql();
            MySqlCommand mySqlCommand = new MySqlCommand(Sentencia, mySqlConnection);

            if (mySqlCommand.ExecuteNonQuery() > 0)
            {
                mySqlConnection.Close();
                return true;
            }
            mySqlConnection.Close();
            throw new Exception("No se realizó ninguna operación en la Base de Datos.");
        }
        public bool EjecutarTransaccion(List<string> Sentencia)
        {
            MySqlConnection mySqlConnection = ConectarMysql();
            MySqlTransaction transa = mySqlConnection.BeginTransaction();
            MySqlCommand mySqlCommand;

            try
            {
                for (int i = 0; i < Sentencia.Count; i++)
                {
                    if (Sentencia[i].Length > 0)
                    {
                        mySqlCommand = new MySqlCommand(Sentencia[i], mySqlConnection);
                        mySqlCommand.Transaction = transa;
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
                transa.Commit();
                Console.Write("finalizado......");
                return true;
            }
            catch (Exception exc)
            {
                transa.Rollback();
              
                throw new Exception("No se realizó ninguna operación en la Base de Datos." + exc);
                return false;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public void EjecutarTransaccion1(List<string> Sentencia,System.Web.SessionState.HttpSessionState Session)
        {
            MySqlConnection mySqlConnection = ConectarMysql();
            MySqlTransaction transa = mySqlConnection.BeginTransaction();
            MySqlCommand mySqlCommand;

            try
            {
                for (int i = 0; i < Sentencia.Count; i++)
                {
                    if (Sentencia[i].Length > 0)
                    {
                        mySqlCommand = new MySqlCommand(Sentencia[i], mySqlConnection);
                        Session["LongActionProgress"] = i;
                        mySqlCommand.Transaction = transa;
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
                transa.Commit();
                Console.Write("finalizado......");

            }
            catch (Exception exc)
            {
                transa.Rollback();

                throw new Exception("No se realizó ninguna operación en la Base de Datos." + exc);

            }
            finally
            {
                mySqlConnection.Close();
            }
        }
    }
}