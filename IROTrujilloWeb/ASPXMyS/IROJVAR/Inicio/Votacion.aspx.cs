using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.Services;

public partial class ASPXMyS_IROJVAR_Inicio_Votacion : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection(csgMySql.conMySQL_IIS);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies.Get("IniciarSesion") != null)
        {
            LblMensaje.Text = Request.Cookies.Get("IniciarSesion").Value;
        }

        Session.Contents.RemoveAll();
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        string vDNI = txtDNI.Text;
        string vFNac = txtFNac.Text;
        string vSexo = ddlGenero.SelectedValue;

        DateTime vdFNac = Convert.ToDateTime(vFNac).Date;
        vFNac = vdFNac.Year + "-" + vdFNac.Month.ToString().PadLeft(2, '0') + "-" + vdFNac.Day.ToString().PadLeft(2, '0');

        string qMySql = "select * from irovotacion.electores " +
            "where DNI_PER = @DNI_PER and FNAC_PER2 LIKE '%" + vFNac + "%' and SEXO_PER = @SEXO_PER;";
        //"where DNI_PER = @DNI_PER and FNAC_PER = @FNAC_PER and SEXO_PER = @SEXO_PER;";
        //"where DNI_PER = @DNI_PER and FNAC_PER = '" + vFNac + "' and SEXO_PER = @SEXO_PER;";

        MySqlCommand cmd = new MySqlCommand(qMySql, conMySql);
        cmd.CommandType = CommandType.Text;
        MySqlDataAdapter MySAdapter = new MySqlDataAdapter();

        cmd.Parameters.AddWithValue("@DNI_PER", vDNI);
        //cmd.Parameters.AddWithValue("@FNAC_PER", txtFNac.Text);
        cmd.Parameters.AddWithValue("@SEXO_PER", vSexo);

        MySAdapter.SelectCommand = cmd;

        conMySql.Open();
        DataSet objdataset = new DataSet();
        MySAdapter.Fill(objdataset);
        conMySql.Close();

        DataTable dtDatos = objdataset.Tables[0];

        if (dtDatos.Rows.Count > 0)
        {
            Session["eID"] = dtDatos.Rows[0]["id_per"].ToString();
            Session["eNOM"] = dtDatos.Rows[0]["NOM_PERSONAL"].ToString();
            Session["eDNI"] = dtDatos.Rows[0]["DNI_PER"].ToString();
            Session["eSEXO"] = dtDatos.Rows[0]["SEXO_PER"].ToString();
            Session["eFNAC"] = dtDatos.Rows[0]["FNAC_PER"].ToString();
            Session["eEDAD"] = dtDatos.Rows[0]["EDAD_PER"].ToString();
            Session["eVOTO"] = dtDatos.Rows[0]["VOTO_PER"].ToString();
            Session["eEST"] = dtDatos.Rows[0]["ESTADO_PER"].ToString();
            if (dtDatos.Rows[0]["VOTOTIP_PER"].ToString() == "ADMIN")
                {   Session["varTipoUser"] = "Admin";   }
            else{   Session["varTipoUser"] = "Usuario"; }
            Session.Timeout = 20;

            //if (dtDatos.Rows[0]["VOTO_PER"].ToString() == "1")
            //{
            //    this.Page.Response.Write("<script language='JavaScript'>window.alert('Usted ya Realizo su Voto');</script>");
            //}

            if (dtDatos.Rows[0]["ESTADO_PER"].ToString() == "0")
            {
                this.Page.Response.Write("<script language='JavaScript'>window.alert('Usted no se Encuentra Habilitado para realizar la Votacion');</script>");
                LblMensaje.Text = "Usuario Inhabilitado. ";
                //Response.Redirect("../Inicio/Votacion.aspx");
            }
            else
            {
                Thread.Sleep(2000);
                Response.Redirect("../Inicio/");
            }

        }
        else
        {
            LblMensaje.Text = "Usuario Incorrecto, Vuelva a Intentar. ";
            txtDNI.Text = "";
            txtFNac.Text = "";
            txtDNI.Focus();
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Datos Incorrectos');</script>");
        }





        //this.Page.Response.Write("<script language='JavaScript'>window.alert('Fecha Insertada: " + txtFNac.Text + "');</script>");

    }

    [WebMethod]
    public static string GetBtnDNI(string DNI)
    {
        MySqlConnection conMySqli = new MySqlConnection(csgMySql.conMySQL_IIS);
        string gHtml = "";
        string qMySql = "";
        try
        {
            qMySql = "select * from irovotacion.electores where DNI_PER = @DNI_PER ";
            MySqlCommand cmd2 = new MySqlCommand(qMySql, conMySqli);
            cmd2.CommandType = CommandType.Text; MySqlDataAdapter adapter2 = new MySqlDataAdapter();

            cmd2.Parameters.AddWithValue("@DNI_PER", DNI);

            adapter2.SelectCommand = cmd2;

            conMySqli.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conMySqli.Close();

            DataTable dtDatos = objdataset.Tables[0];

            if (dtDatos.Rows.Count > 0)
            {
                gHtml = dtDatos.Rows[0]["NOM_PERSONAL"].ToString();
            }
            else
            {
                gHtml = "No Existe Elector";
            }
            
        }
        catch (Exception ex)
        {
            gHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gHtml;
    }

}