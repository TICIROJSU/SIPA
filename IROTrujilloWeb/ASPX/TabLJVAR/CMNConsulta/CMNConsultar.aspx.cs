using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_TabLJVAR_CMNConsulta_CMNConsultar : System.Web.UI.Page
{
    string html = "";
    string htmlAtenciones = "";
    string htmlDetAtenciones = "";
    string CantConsulta = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        CantConsulta = Request["Cant"];
        CargaColumns();
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            int vyear = DateTime.Now.Year;
           // DDLAnio.Text = vyear.ToString();
            CargaTablaDTIni();
        }

        if (Page.IsPostBack)
        {
            GVtable.DataSource = null;
            GVtable.DataBind();
        }

    }

    public void CargaColumns()
    {
        try
        {
            //con.Open();
            string qSql = "SELECT COLUMN_NAME, ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE " +
                "FROM TabLibres.INFORMATION_SCHEMA.COLUMNS " +
                "WHERE TABLE_NAME = N'CMMLlenaView' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtCol = objdataset.Tables[0];
            foreach (DataRow dbR in dtCol.Rows)
            {
                ListItem i;
                i = new ListItem(dbR["COLUMN_NAME"].ToString());
                DdlColumn.Items.Add(i);
            }

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();

    }

    public void CargaTablaDT()
    {
        
    }

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; ' id='tblbscrJS'>";
            html += "<tr>";
            html += "<th>Edit</th> <th>Copy</th> <th>CCOSTO_NOMBRE</th> " +
                "<th>B_S</th> <th>Fuente_Rubro</th> <th>FF_Desc</th> <th>Meta</th> " +
                "<th>PROGRAMA_PRESUPUESTAL</th> <th>RJ</th> <th>Generica_de_Gasto</th> " +
                "<th>Clasificador_de_Gasto</th> <th>FECHA_VB</th> <th>N_PEDIDO</th> " +
                "<th>CCP_SIAF</th> <th>Codigo_Item_N</th> <th>Descripcion_del_Items</th> " +
                "<th>Unidad_de_Medida</th> <th>Precio_Unitario</th> " +
                "<th>Cantidad_Total_CMN_ACTUAL</th> <th>Valor_Total_CMN_ACTUAL</th> " +
                "<th>Cantidad_EXCLUSION</th> <th>Valor_EXCLUSION</th> " +
                "<th>Cantidad_INCLUSION</th> <th>Valor_INCLUSION</th> " +
                "<th>CANTIDAD_INCLUSION_A_TABLA</th> <th>IMPORTE_TABLA</th> " +
                "<th>CANTIDAD_ENERO</th> <th>TOTAL_GASTO_ENERO</th> " +
                "<th>CANTIDAD_FEBRERO</th> <th>TOTAL_GASTO_FEBRERO</th> " +
                "<th>NroOC_OS_Feb</th> <th>NroEXPEDIENTE_SIAF_Feb</th> " +
                "<th>FECHA_DEVENGADA_Feb</th> <th>CANTIDAD_MARZO</th> " +
                "<th>TOTAL_GASTO_MARZO</th> <th>NroOC_OS_Mar</th> " +
                "<th>NroEXPEDIENTE_SIAF_Mar</th> <th>FECHA_DEVENGADA_Mar</th> " +
                "<th>CANTIDAD_ABRIL</th> <th>TOTAL_GASTO_ABRIL</th> " +
                "<th>NroOC_OS_Abr</th> <th>NroEXPEDIENTE_SIAF_Abr</th> " +
                "<th>FECHA_DEVENGADA_Abr</th> <th>CANTIDAD_MAYO</th> " +
                "<th>TOTAL_GASTO_MAYO</th> <th>NroOC_OS_May</th> " +
                "<th>NroEXPEDIENTE_SIAF_May</th> <th>FECHA_DEVENGADA_May</th> " +
                "<th>CANTIDAD_JUNIO</th> <th>TOTAL_GASTO_JUNIO</th> " +
                "<th>NroOC_OS_Jun</th> <th>NroEXPEDIENTE_SIAF_Jun</th> " +
                "<th>FECHA_DEVENGADA_Jun</th> <th>CANTIDAD_JULIO</th> " +
                "<th>TOTAL_GASTO_JULIO</th> <th>NroOC_OS_Jul</th> " +
                "<th>NroEXPEDIENTE_SIAF_Jul</th> <th>FECHA_DEVENGADA_Jul</th> " +
                "<th>CANTIDAD_AGOSTO</th> <th>TOTAL_GASTO_AGOSTO</th> " +
                "<th>NroOC_OS_Ago</th> <th>NroEXPEDIENTE_SIAF_Ago</th> " +
                "<th>FECHA_DEVENGADA_Ago</th> <th>CANTIDAD_SETIEMBRE</th> " +
                "<th>TOTAL_GASTO_SETIEMBRE</th> <th>NroOC_OS_Set</th> " +
                "<th>NroEXPEDIENTE_SIAF_Set</th> <th>FECHA_DEVENGADA_Set</th> " +
                "<th>CANTIDAD_OCTUBRE</th> <th>TOTAL_GASTO_OCTUBRE</th> " +
                "<th>NroOC_OS_Oct</th> <th>NroEXPEDIENTE_SIAF_Oct</th> " +
                "<th>FECHA_DEVENGADA_Oct</th> <th>CANTIDAD_NOVIEMBRE</th> " +
                "<th>TOTAL_GASTO_NOVIEMBRE</th> <th>NroOC_OS_Nov</th> " +
                "<th>NroEXPEDIENTE_SIAF_Nov</th> <th>FECHA_DEVENGADA_Nov</th> " +
                "<th>CANTIDAD_DICIEMBRE</th> <th>TOTAL_GASTO_DICIEMBRE</th> " +
                "<th>NroOC_OS_Dic</th> <th>NroEXPEDIENTE_SIAF_Dic</th> " +
                "<th>FECHA_DEVENGADA_Dic</th> <th>CANTIDAD_ANIO</th> " +
                "<th>TOTAL_GASTO_ANIO</th> <th>TOT_GASTO_IMPORTE_VALIDA</th> " +
                "<th>ARTIFICIO_COMP_MEN1</th> <th>ARTIFICIO_COMP_MEN2</th> " +
                "<th>ARTIFICIO_DEV1</th> <th>ARTIFICIO_DEV2</th> <th>CON_PEDIDO</th> " +
                "<th>PIM</th> <th>CERTIFICADO</th> <th>COMPROMISO_MENSUAL</th> " +
                "<th>DEVENGADO</th> <th>SALDO_X_CERT</th> <th>SALDO_X_COMP</th> " +
                "<th>SALDO_X_DEV</th> <th>OJO_LOG</th> <th>OJO_ECO</th> " +
                "<th>%CERT</th> <th>%COMP</th> <th>%DEV</th>";

            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                html += "<tr>";
                //html += "<td class='' style='text-align: center;'>" + ClassGlobal.MesNroToTexto(dbRow["MES"].ToString()) + "</td>";

                html += "<td><img src='../../../Images/iconEdit.png' title='Editar' width='32' /></td> " +
                    "<td>" +
                        "<a href='../../TabLJVAR/CMNRegistro/CMNRegistrar.aspx?Proc=Copy&id=" + dbRow["idCMNLlenado"].ToString() + "'>" +
                        "<img src='../../../Images/iconCopy.png' title='Duplicar' width='32' />" +
                        "</a>" +
                    "</td> " +
                    "<td>" + dbRow["CCOSTO_NOMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["B_S"].ToString() + "</td> " +
                    "<td>" + dbRow["Fuente_Rubro"].ToString() + "</td> " +
                    "<td>" + dbRow["FF_Desc"].ToString() + "</td> " +
                    "<td>" + dbRow["Meta"].ToString() + "</td> " +
                    "<td>" + dbRow["PROGRAMA_PRESUPUESTAL"].ToString() + "</td> " +
                    "<td>" + dbRow["RJ"].ToString() + "</td> " +
                    "<td>" + dbRow["Generica_de_Gasto"].ToString() + "</td> " +
                    "<td>" + dbRow["Clasificador_de_Gasto"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_VB"].ToString() + "</td> " +
                    "<td>" + dbRow["N_PEDIDO"].ToString() + "</td> " +
                    "<td>" + dbRow["CCP_SIAF"].ToString() + "</td> " +
                    "<td>" + dbRow["Codigo_Item_N"].ToString() + "</td> " +
                    "<td>" + dbRow["Descripcion_del_Items"].ToString() + "</td> " +
                    "<td>" + dbRow["Unidad_de_Medida"].ToString() + "</td> " +
                    "<td>" + dbRow["Precio_Unitario"].ToString() + "</td> " +
                    "<td>" + dbRow["Cantidad_Total_CMN_ACTUAL"].ToString() + "</td> " +
                    "<td>" + dbRow["Valor_Total_CMN_ACTUAL"].ToString() + "</td> " +
                    "<td>" + dbRow["Cantidad_EXCLUSION"].ToString() + "</td> " +
                    "<td>" + dbRow["Valor_EXCLUSION"].ToString() + "</td> " +
                    "<td>" + dbRow["Cantidad_INCLUSION"].ToString() + "</td> " +
                    "<td>" + dbRow["Valor_INCLUSION"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_INCLUSION_A_TABLA"].ToString() + "</td> " +
                    "<td>" + dbRow["IMPORTE_TABLA"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_ENERO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_ENERO"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_FEBRERO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_FEBRERO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Feb"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Feb"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Feb"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_MARZO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_MARZO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Mar"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Mar"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Mar"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_ABRIL"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_ABRIL"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Abr"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Abr"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Abr"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_MAYO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_MAYO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_May"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_May"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_May"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_JUNIO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_JUNIO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Jun"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Jun"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Jun"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_JULIO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_JULIO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Jul"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Jul"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Jul"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_AGOSTO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_AGOSTO"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Ago"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Ago"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Ago"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_SETIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_SETIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Set"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Set"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Set"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_OCTUBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_OCTUBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Oct"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Oct"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Oct"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_NOVIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_NOVIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Nov"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Nov"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Nov"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_DICIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_DICIEMBRE"].ToString() + "</td> " +
                    "<td>" + dbRow["NroOC_OS_Dic"].ToString() + "</td> " +
                    "<td>" + dbRow["NroEXPEDIENTE_SIAF_Dic"].ToString() + "</td> " +
                    "<td>" + dbRow["FECHA_DEVENGADA_Dic"].ToString() + "</td> " +
                    "<td>" + dbRow["CANTIDAD_ANIO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOTAL_GASTO_ANIO"].ToString() + "</td> " +
                    "<td>" + dbRow["TOT_GASTO_IMPORTE_VALIDA"].ToString() + "</td> " +
                    "<td>" + dbRow["ARTIFICIO_COMP_MEN1"].ToString() + "</td> " +
                    "<td>" + dbRow["ARTIFICIO_COMP_MEN2"].ToString() + "</td> " +
                    "<td>" + dbRow["ARTIFICIO_DEV1"].ToString() + "</td> " +
                    "<td>" + dbRow["ARTIFICIO_DEV2"].ToString() + "</td> " +
                    "<td>" + dbRow["CON_PEDIDO"].ToString() + "</td> " +
                    "<td>" + dbRow["PIM"].ToString() + "</td> " +
                    "<td>" + dbRow["CERTIFICADO"].ToString() + "</td> " +
                    "<td>" + dbRow["COMPROMISO_MENSUAL"].ToString() + "</td> " +
                    "<td>" + dbRow["DEVENGADO"].ToString() + "</td> " +
                    "<td>" + dbRow["SALDO_X_CERT"].ToString() + "</td> " +
                    "<td>" + dbRow["SALDO_X_COMP"].ToString() + "</td> " +
                    "<td>" + dbRow["SALDO_X_DEV"].ToString() + "</td> " +
                    "<td>" + dbRow["OJO_LOG"].ToString() + "</td> " +
                    "<td>" + dbRow["OJO_ECO"].ToString() + "</td> " +
                    "<td>" + dbRow["%CERT"].ToString() + "</td> " +
                    "<td>" + dbRow["%COMP"].ToString() + "</td> " +
                    "<td>" + dbRow["%DEV"].ToString() + "</td>";

                html += "</tr>" + Environment.NewLine;
            }

            html += "</table>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados0</td></tr>";
            html += "</table><hr>";
        }
        LitTABL1.Text = html;

    }

    public void CargaTablaDTIni()
    {
        if (CantConsulta == "5")
        {
            CantConsulta = " top 5 ";
        }
        else
        {
            CantConsulta = "";
        }

        try
        {
            //con.Open();
            string qSql = "select " + CantConsulta + " * from TabLibres.dbo.CMMLlenaView order by idCMNLlenado desc";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            CargaTabla(dtDato);

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        response.Write(LitTABL1.Text);
        response.End();
    }

    protected void ExportarExcel2_Click(object sender, EventArgs e)
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