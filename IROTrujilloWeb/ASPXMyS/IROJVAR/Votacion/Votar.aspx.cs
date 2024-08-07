using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPXMyS_IROJVAR_Votacion_Votar : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection(csgMySql.conMySQL_IIS);
    protected void Page_Load(object sender, EventArgs e)
    {
        string vmsjFec1 = "";
        var fecIni = new DateTime(2024, 1, 4, 8, 0, 0);
        var fecFin = new DateTime(2024, 1, 4, 18, 0, 0);
        var fecActual = DateTime.Now;
        //this.Page.Response.Write("<script language='JavaScript'>fmsjRangoFec('" + fecIni + "', '" + fecFin + "', '" + fecActual + "');</script>");

        if (fecActual < fecIni || fecActual > fecFin)
        {
            if (fecActual < fecIni) { vmsjFec1 = "Espere a que el Horario de Votacion Inicie."; }
            if (fecActual > fecFin) { vmsjFec1 = "El Horario de Votacion ha Finalizado."; }
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Fechas Fuera de Rango');</script>");
            string JavaScript = "fmsjRangoFec('" + fecIni + "', '" + fecFin + "', '" + fecActual + "', '" + vmsjFec1 + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", JavaScript, true);

        }
    }

    [WebMethod]
    public static string SetBtnRegVoto(string Voto, string idElector)
    {
        MySqlConnection conMySqli = new MySqlConnection(csgMySql.conMySQL_IIS);
        string vVotoYaRegistrado = "No";
        try
        {
            string qMySql = "select * from irovotacion.electores where id_per = '" + idElector + "'; ";

            MySqlCommand cmdVR = new MySqlCommand(qMySql, conMySqli);
            cmdVR.CommandType = CommandType.Text; MySqlDataAdapter MySAdapter = new MySqlDataAdapter();
            MySAdapter.SelectCommand = cmdVR;

            conMySqli.Open();
            DataSet objdataset = new DataSet();
            MySAdapter.Fill(objdataset);
            conMySqli.Close();

            if (objdataset.Tables[0].Rows[0]["VOTO_PER"].ToString() == "1"){ vVotoYaRegistrado = "Si"; }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }

        ////////////-------///////////////------////////////////////------/////////

        string gDetHtml = "";
        string qSql = "";
        if (vVotoYaRegistrado == "No")
        {
            try
            {
                qSql = "insert into irovotacion.conteovotos (Periodo, idListaVotacion, Contador) values (@Periodo, @idListV, @Contador); " +
                    "UPDATE irovotacion.electores SET VOTO_PER = '1', VOTOFEC_PER = CURRENT_TIMESTAMP WHERE id_per = @id_per;";

                MySqlCommand cmd = new MySqlCommand(qSql, conMySqli);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                cmd.Parameters.AddWithValue("@Periodo", "202312");
                cmd.Parameters.AddWithValue("@idListV", "2");
                cmd.Parameters.AddWithValue("@Contador", Voto);
                cmd.Parameters.AddWithValue("@id_per", idElector);

                adapter.SelectCommand = cmd;

                conMySqli.Open();
                int idReg = Convert.ToInt32(cmd.ExecuteScalar());
                conMySqli.Close();

                gDetHtml += "Voto Guardado";
            }
            catch (Exception ex)
            {
                gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
            }
        }
        else
        {
            gDetHtml = "El Elector logeado, ya Realizo el Voto.";
        }

        return gDetHtml;
    }

}
