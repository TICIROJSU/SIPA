using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_Varios_ContratoProdSISMED : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        ListarReg();
    }

    [WebMethod]
    public static string GetProductosSISMED(string vtxtcodSIGA)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select COD_SISMED, DESCRIPCION_SIGA, ESTADO from IROf.dbo.CatSISMED2 where CODIGO_SIGA = '" + vtxtcodSIGA + "'";
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
                gHTML += "<table class='table table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<th class=''>Cod. SISMED </th>";
                gHTML += "<th class=''>Descripcion SIGA</th>";
                gHTML += "<th class=''>Estado</th>";
                gHTML += "<th class=''>Sel.</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vcodsg, vdessg, vest;
                    vcodsg = dbRow["COD_SISMED"].ToString();
                    vdessg = dbRow["DESCRIPCION_SIGA"].ToString();
                    vest = dbRow["ESTADO"].ToString();
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcodsg + "</td>";
                    gHTML += "<td class='' >" + vdessg + "</td>";
                    gHTML += "<td class='' >" + vest + "</td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-warning' onclick=\"fSelProd('" + vcodsg + "', '" + vdessg + "', '" + vest + "')\" data-dismiss='modal'> Seleccionar</div>" +
                        "</td>";
                    //cod, prod, codmontura, prec, cant, idmontura
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            //con.Open();
            string consql = "INSERT INTO TabLibres.dbo.ContratoProdSISMED(CODSIGA, CODSISMED, PRODUCTO, Contrato, ContratoAnio, ContratoNro, ContratoCant, CantAtendida, CantxAtender, Estado) " +
                "VALUES (@CODSIGA, @CODSISMED, @PRODUCTO, @Contrato, @ContratoAnio, @ContratoNro, @ContratoCant, @CantAtendida, @CantxAtender, @Estado) " +
                "SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@CODSIGA", txtCodSIGA.Text);
            cmd.Parameters.AddWithValue("@CODSISMED", txtCodSISMED.Text);
            cmd.Parameters.AddWithValue("@PRODUCTO", txtProducto.Text);
            cmd.Parameters.AddWithValue("@Contrato", DDLContrato.SelectedValue);
            cmd.Parameters.AddWithValue("@ContratoAnio", txtContAnio.Text);
            cmd.Parameters.AddWithValue("@ContratoNro", txtContNro.Text);
            cmd.Parameters.AddWithValue("@ContratoCant", txtContCant.Text);
            cmd.Parameters.AddWithValue("@CantAtendida", txtCantAtend.Text);
            cmd.Parameters.AddWithValue("@CantxAtender", txtCantxAtend.Text);
            cmd.Parameters.AddWithValue("@Estado", "Activo");

            conSAP00.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00.Close();

            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "'); window.location.assign('../../IROJVAR/Varios/ContratoProdSISMED.aspx' );</script>");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitErrores.Text = ex.Message.ToString();
        }

    }

    public void ListarReg()
    {
        try
        {
            //con.Open();
            string qSql = "select top 15 CODSIGA, CODSISMED, PRODUCTO, Contrato, ContratoAnio, ContratoNro, ContratoCant, CantAtendida, CantxAtender " +
                "from TabLibres.dbo.ContratoProdSISMED " +
                "where Estado = 'Activo' " +
                "order by FechRegistro desc";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0]; 
            GVtable.DataBind();
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(GVtable);
        pageToRender.Controls.Add(form);
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        pageToRender.RenderControl(htw);
        response.Write(sw.ToString());
        response.End();
    }

}