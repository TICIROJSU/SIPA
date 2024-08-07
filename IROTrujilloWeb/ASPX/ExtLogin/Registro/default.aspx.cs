using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_Registro_default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //string varSql = "select ren.UBIGEO + '|' + ren.Cod_DISA + '|' + ren.Cod_Red + '|' + ren.Cod_Microrred + '|' + ren.Cod_unico as codigoeess, ren.Red + ' | ' + ren.Microrred + ' | ' + RIGHT(ren.Cod_unico, 5) + ' - ' + ren.Establecimiento as EESS, ren.UBIGEO, ren.Cod_DISA, ren.Cod_Red, ren.Cod_Microrred, ren.Cod_unico, ren.Red, ren.Microrred, ren.Establecimiento  from BDHISMINSA.dbo.RENIPRESS ren where ren.Institucion='GOBIERNO REGIONAL' AND ren.Cod_DISA='18';";
            //SqlCommand cmd = new SqlCommand(varSql, con);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //cboREDreg.Items.Clear();
            //ListItem LisTMP = new ListItem("Selecciona EESS", "", true);
            //cboREDreg.Items.Add(LisTMP);
            //cboREDreg.DataSource = dt;
            //cboREDreg.DataTextField = "EESS";
            //cboREDreg.DataValueField = "codigoeess";
            //cboREDreg.DataBind();
        }
    }

    [WebMethod]
    public static string GetEESS(string vEESS)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select top 20 * from NUEVA.dbo.RENIPRESS where Codigo_Unico + Nombre_del_establecimiento like '%' + @vEESS + '%' order by DISA, Red, Microrred, Nombre_del_establecimiento;";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@vEESS", vEESS);

            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gHTML += "<th class=''>Sel</th>";
                gHTML += "<th class=''>DISA </th>";
                gHTML += "<th class=''>Provincia</th>";
                gHTML += "<th class=''>Distrito</th>";
                gHTML += "<th class=''>Codigo</th>";
                gHTML += "<th class=''>Establecimiento</th>";
                gHTML += "<th class=''>Ubigeo</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vdisa, vprov, vdist, vcod, veess, vubi;
                    vdisa = dbRow["DISA"].ToString();
                    vprov = dbRow["Provincia"].ToString();
                    vdist = dbRow["Distrito"].ToString();
                    vcod = dbRow["Codigo_Unico"].ToString();
                    veess = dbRow["Nombre_del_establecimiento"].ToString();
                    vubi = dbRow["UBIGEO"].ToString();

                    //string vd, vm, vy;
                    //DateTime vfecDT = Convert.ToDateTime(vfec);
                    //vd = vfecDT.Day.ToString();
                    //vm = vfecDT.Month.ToString();
                    //vy = vfecDT.Year.ToString();
                    //string vfec2 = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');

                    gHTML += "<tr>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-info' onclick=\"gSelEESS('" + vcod + "', '" + vdisa + "', '" + vprov + "', '" + vdist + "', '" + veess + "', '" + vubi + "')\" data-dismiss='modal'>Sel</div>" +
                        "</td>";
                    gHTML += "<td style='text-align: left;' >" + vdisa + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vprov + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vdist + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vcod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + veess + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vubi + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        string varSql = "select * from NUEVA.dbo.extUsuario where extUDNI = '" + txtDNIreg.Text + "'";
        SqlCommand cmd = new SqlCommand(varSql, con);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        //con.Open();
        DataSet objdataset = new DataSet();
        adapter.Fill(objdataset);
        //con.Close();
        DataTable dtDato = objdataset.Tables[0];
        if (dtDato.Rows.Count > 0)
        {
            this.Page.Response.Write("<script language='JavaScript'>window.alert('Usuario ya se Encuentra Registrado');</script>");
            this.Page.Response.Write("<script language='JavaScript'>location='HojaRegistro.aspx?Usuario=" + txtDNIreg.Text + "'</script>");
        }
        else
        {
            GuardarUsuario();
        }

    }

    protected void GuardarUsuario()
    {
        string varSql = "INSERT INTO NUEVA.dbo.extUsuario (extUNombres, extUApellidos, extUDNI, extClave, extUFecNac, extUemail, extUTelefono, codIPRESS, extUCargo, extUsuEstado) values (@extUNombres, @extUApellidos, @extUDNI, @extClave, @extUFecNac, @extUemail, @extUTelefono, @codIPRESS, @extUCargo, @extUsuEstado); ";
        SqlCommand cmd = new SqlCommand(varSql, con);
        cmd.Parameters.AddWithValue("@extUDNI", txtDNIreg.Text);
        cmd.Parameters.AddWithValue("@extClave", txtDNIreg.Text);
        cmd.Parameters.AddWithValue("@extUNombres", txtNOMBREreg.Text);
        cmd.Parameters.AddWithValue("@extUApellidos", txtAPELLIDOSreg.Text);
        cmd.Parameters.AddWithValue("@extUemail", txtEMAILreg.Text);
        cmd.Parameters.AddWithValue("@extUFecNac", txtFECNACreg.Text);
        cmd.Parameters.AddWithValue("@extUTelefono", txtTELEFONOreg.Text);
        cmd.Parameters.AddWithValue("@codIPRESS", txtCodEESS.Text);
        cmd.Parameters.AddWithValue("@extUCargo", ddlCargo.Text);
        cmd.Parameters.AddWithValue("@extUsuEstado", "INACTIVO");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Correcto'); location='HojaRegistro.aspx?Usuario=" + txtDNIreg.Text + "'</script>");
        //Response.Redirect("../Login/");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.Page.Response.Write("<script language='JavaScript'>location='HojaRegistro.aspx?Usuario=" + txtDNIreg.Text + "'</script>");
    }


}