using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_SInfantil_Registros_RegEquipoSI : System.Web.UI.Page
{
    SqlConnection conExtSI = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
        }
    }

    [WebMethod]
    public static string GetDatoFiltro(string vtxtCarga, string vInstitucion, string vRed, string vMRed, string vProvincia, string vDistrito, string vEESS, string vSel)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
        string gHTML = "";
        try
        {
            string qSql = "";

            if (vSel == "Institucion")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Sector', '', '', '', '', ''; ";
            }
            if (vSel == "Red")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Red', '" + vInstitucion + "', '', '', '', '';";
            }
            if (vSel == "MRed")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'MRed', '" + vInstitucion + "', '" + vRed + "', '', '', '';";
            }
            if (vSel == "Provincia")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Provincia', '" + vInstitucion + "', '', '', '', '';";
            }
            if (vSel == "Distrito")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'Distrito', '" + vInstitucion + "', '', '', '" + vProvincia + "', '';";
            }
            if (vSel == "EESS")
            {
                qSql = "exec [BD_GENERAL].[dbo].[sp_listar_ESRedMRedProvDist] 'EESS', '" + vInstitucion + "', '" + vRed + "', '" + vMRed + "', '" + vProvincia + "', '" + vDistrito + "';";
            }


            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblTablaModal' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gHTML += "<th class=''>Nro </th>";
                gHTML += "<th class=''>Seleccionar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vdato = "";
                    for (int i = 0; i < dbRow["CampoDato"].ToString().Length; i++)
                    {
                        if (dbRow["CampoDato"].ToString().Substring(i, 1) == "\"") { break; }
                        vdato += dbRow["CampoDato"].ToString().Substring(i, 1);
                    }
                    gHTML += "<tr onclick=\"document.getElementById('" + vtxtCarga + "').value = '" + vdato + "';\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gHTML += "<td style='text-align: left;' >" + dbRow["CampoDato"].ToString() + "</td>" +
                        "</tr>";
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

    [WebMethod]
    public static string GetDatoFiltroES(string vtxtCarga, string vInstitucion, string vRed, string vMRed, string vProvincia, string vDistrito, string vEESS, string vSel, string vtxtESHide)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
        string gHTML = "";
        try
        {
            string qSql = "select Nombre_Establecimiento as CampoDato, Codigo_Unico " +
                "from [BDHIS_MINSA].[dbo].[MAESTRO_HIS_ESTABLECIMIENTO] " +
                "where Departamento = 'LA LIBERTAD' and Descripcion_Sector = '" + vInstitucion + "' and Red like '" + vRed + "' + '%' " +
                "and MicroRed like '" + vMRed + "' + '%' and Provincia like '" + vProvincia + "' + '%' " +
                "and Distrito like '" + vDistrito + "' + '%' " +
                "group by Nombre_Establecimiento, Codigo_Unico " +
                "order by 1 ";

            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblTablaModal' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gHTML += "<th class=''>Nro </th>";
                gHTML += "<th class=''>Seleccionar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vdato = "";
                    for (int i = 0; i < dbRow["CampoDato"].ToString().Length; i++)
                    {
                        if (dbRow["CampoDato"].ToString().Substring(i, 1) == "\"") { break; }
                        vdato += dbRow["CampoDato"].ToString().Substring(i, 1);
                    }
                    gHTML += "<tr onclick=\"document.getElementById('" + vtxtCarga + "').value = '" + vdato + "';document.getElementById('" + vtxtESHide + "').value = '" + dbRow["Codigo_Unico"].ToString() + "';\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    gHTML += "<td style='text-align: left;' >" + dbRow["CampoDato"].ToString() + "</td>" +
                        "</tr>";
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }


    public void CargaTablaDT()
    {
        string vInstitucion = txtInstitucion.Text;
        string vRed = txtRed.Text;
        string vMRed = txtMicroRed.Text;
        string vProvincia = txtProvincia.Text;
        string vDistrito = txtDistrito.Text;
        string vEESS = txtEESS.Text;
        string vcEESS = txtEESSCodHide.Value;

        string gHTML = "";
        try
        {

            string qSql = "select * from BD_COMPROMISOSGESTION.dbo.SIEquipo where Codigo_Unico_HIS_ES = '" + vcEESS + "'";
            SqlCommand cmd = new SqlCommand(qSql, conExtSI);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conExtSI.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conExtSI.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    if (dbRow["EquipamientoSI"].ToString() == "Hemoglobinometro")
                    {
                        ddlHemogExis.Value = dbRow["Existe"].ToString();
                        ddlHemogEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Balanza Pediatrica")
                    {
                        ddlBalPExis.Value = dbRow["Existe"].ToString();
                        ddlBalPEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Tallimetro")
                    {
                        ddlTallPExis.Value = dbRow["Existe"].ToString();
                        ddlTallPEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Infantometro")
                    {
                        ddlInfanExis.Value = dbRow["Existe"].ToString();
                        ddlInfanPEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Refrigeradora")
                    {
                        ddlRefriExis.Value = dbRow["Existe"].ToString();
                        ddlRefriEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Congeladora")
                    {
                        ddlCongeExis.Value = dbRow["Existe"].ToString();
                        ddlCongePEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "Thermo")
                    {
                        ddlTherExis.Value = dbRow["Existe"].ToString();
                        ddlTherEst.Value = dbRow["Estado"].ToString();
                    }
                    if (dbRow["EquipamientoSI"].ToString() == "DataLogger")
                    {
                        ddlDLogExis.Value = dbRow["Existe"].ToString();
                        ddlDLogEst.Value = dbRow["Estado"].ToString();
                    }

                }

                gHTML += "";
            }

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            gHTML += ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }

        LitTABL1.Text = gHTML;
    }


    [WebMethod]
    public static string SetRegRefG(string vcodES, string vEquipo, string vExiste, string vEstado )
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO_SI);
        string gHTML = "";
        string qSql = "";
        try
        {
            //con.Open();
            qSql = "UPDATE BD_COMPROMISOSGESTION.dbo.SIEquipo " +
                "SET Existe = @Existe, Estado = @Estado " +
                "WHERE Codigo_Unico_HIS_ES = @Codigo_Unico_HIS_ES " +
                "       and EquipamientoSI = @EquipamientoSI; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Existe", vExiste);
            cmd.Parameters.AddWithValue("@Estado", vEstado);
            cmd.Parameters.AddWithValue("@Codigo_Unico_HIS_ES", vcodES);
            cmd.Parameters.AddWithValue("@EquipamientoSI", vEquipo);

            conSAP00i.Open();
            cmd.ExecuteNonQuery();
            conSAP00i.Close();

            gHTML += vEquipo + " - Registro/Edicion Correcto.";
        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        return gHTML;
    }

}