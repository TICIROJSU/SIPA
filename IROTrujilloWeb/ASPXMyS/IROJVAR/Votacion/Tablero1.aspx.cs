using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPXMyS_IROJVAR_Votacion_Tablero1 : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection(csgMySql.conMySQL_IIS);
    protected void Page_Load(object sender, EventArgs e)
    {

        ConteoListado();
    }

    public void ConteoListado()
    {
        try
        {
            string qMySql = "select IFNULL(SUM(case when Contador = 'SI' then 1 else 0 end), 0) as ContSI, " +
                "       IFNULL(SUM(case when Contador = 'NO' then 1 else 0 end), 0) as ContNO, " +
                "       IFNULL(SUM(case when Contador = 'Ninguno' then 1 else 0 end), 0) as ContNing " +
                "   from irovotacion.conteovotos " +
                "   where Periodo = '202312' ; " +
                "select count(*) as ContElectores, IFNULL(SUM(case when VOTO_PER = '0' then 1 else 0 end), 0) as ContElecBlanco " +
                "from irovotacion.electores " +
                "where ESTADO_PER = '1' and id_per > 0; ";

            MySqlCommand cmd = new MySqlCommand(qMySql, conMySql);
            cmd.CommandType = CommandType.Text; MySqlDataAdapter MySAdapter = new MySqlDataAdapter();

            MySAdapter.SelectCommand = cmd;

            conMySql.Open();
            DataSet objdataset = new DataSet();
            MySAdapter.Fill(objdataset);
            conMySql.Close();

            DataTable dtContVotos = objdataset.Tables[0];
            DataTable dtContElect = objdataset.Tables[1];

            lblSI.Text = dtContVotos.Rows[0]["ContSI"].ToString();
            lblNO.Text = dtContVotos.Rows[0]["ContNO"].ToString();
            lblBlank.Text = dtContVotos.Rows[0]["ContNing"].ToString();
            lblTOT.Text = (Convert.ToInt32(lblSI.Text) + Convert.ToInt32(lblNO.Text) + Convert.ToInt32(lblBlank.Text)).ToString();

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }
    }

    [WebMethod]
    public static string GetListaElectores(string vVoto)
    {
        MySqlConnection conMySqli = new MySqlConnection(csgMySql.conMySQL_IIS);
        string gHTML = "";
        try
        {
            string qMySql = "SELECT * FROM irovotacion.electores where id_per > 0 ";
            if (vVoto == "ConVoto") { qMySql += " and VOTO_PER = '1' "; }
            if (vVoto == "SinVoto") { qMySql += " and VOTO_PER = '0' "; }
            qMySql += " order by NOM_PERSONAL; ";

            MySqlCommand Mycmd = new MySqlCommand(qMySql, conMySqli);
            Mycmd.CommandType = CommandType.Text; MySqlDataAdapter MySAdapter = new MySqlDataAdapter();
            MySAdapter.SelectCommand = Mycmd;

            conMySqli.Open();
            DataSet objdataset = new DataSet();
            MySAdapter.Fill(objdataset);
            conMySqli.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += "<input type='hidden' id='txtContListElec' value='" + dtDatoDetAt.Rows.Count + "' />";
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    //vcod = dbRow["IdMIng"].ToString();
                    //DateTime vfecDT = Convert.ToDateTime(vfec);
                    string vStatAvatar = "<span class='status away'></span>";
                    if (dbRow["VOTOTIP_PER"].ToString() == "ADMIN")
                    {   vStatAvatar = "<span class='status online'></span>";    }

                    gHTML += "<tr>";
                    gHTML += "<td><a href='#' onclick=\"fSetBtnTipoUser('"+ dbRow["id_per"].ToString() + "')\">" +
                        "<span class='avatar'><i class='fa fa-user-md'></i></span>" + vStatAvatar + 
                        "</a>" + dbRow["NOM_PERSONAL"].ToString() + 
                        "</td>";
                    gHTML += "<td>" + dbRow["DNI_PER"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["FNAC_PER"].ToString().Substring(0, 10) + "</td>";
                    gHTML += "<td>" + dbRow["INGRESO_PER"].ToString().Substring(0, 10) + "</td>";
                    gHTML += "<td>" + dbRow["REGIM_PER"].ToString() + "</td>";
                    gHTML += "<td colspan='2'>" + dbRow["CONDIC_PER"].ToString() + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }
            }
            else
            {
                gHTML += "<input type='hidden' id='txtContListElec' value='0' />";
                gHTML += "Sin informacion.";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string SetBtnTipoUser(string id)
    {
        MySqlConnection conMySqli = new MySqlConnection(csgMySql.conMySQL_IIS);
        string gDetHtml = "";
        string qSql = "";

        try
        {
            qSql = "UPDATE irovotacion.electores SET VOTOTIP_PER = 'ADMIN' WHERE id_per = @id_per;";

            MySqlCommand cmd = new MySqlCommand(qSql, conMySqli);
            cmd.CommandType = CommandType.Text;
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            cmd.Parameters.AddWithValue("@id_per", id);

            adapter.SelectCommand = cmd;

            conMySqli.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conMySqli.Close();

            gDetHtml += "Elector Habilitado";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        
        return gDetHtml;
    }

}