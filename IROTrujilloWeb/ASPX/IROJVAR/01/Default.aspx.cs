using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_01_Default : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    string tipCarga, whereSql;
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        tipCarga = Request["tip"];
        switch (tipCarga)
        {
            case "Tiempos":
                lblTitulo.Text = "Tiempos de Espera";
                whereSql = "where month(SIS.FEC_SIS)<@vMes";
                break;

            case "Diferimiento":
                lblTitulo.Text = "Diferimiento";
                whereSql = "where month(TARJETON.FRG_TAR)<@vMes";
                break;

            default:
                lblTitulo.Text = "Seleccione Reporte";
                whereSql = "where month(TARJETON.FRG_TAR)<0";
                tipCarga = "";
                break;
        }

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            int vyear = DateTime.Now.Year;
            DDLMes.SelectedIndex = vmonth - 1;
            DDLAnio.Text = vyear.ToString();
        }

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        int vAnio = Int32.Parse(DDLAnio.SelectedValue);
        int vMes = Int32.Parse(DDLMes.SelectedValue);
        try
        {
            //con.Open();
            if (vMes>1)
            {
                CrearTablaSIS_DNI(vAnio, vMes, "");
            }
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_RES_SIS_SIPA", conSAP00);
            cmd.CommandType = CommandType.StoredProcedure;
            //creamos los parametros que usaremos
            cmd.Parameters.Add("@anioi", SqlDbType.Int);
            cmd.Parameters.Add("@mesi", SqlDbType.Int);
            cmd.Parameters.Add("@det", SqlDbType.VarChar);
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters["@anioi"].Value = vAnio;
            cmd.Parameters["@mesi"].Value = vMes;
            cmd.Parameters["@det"].Value = tipCarga;
            //adapter, para asignarle el cmd, command
            //SqlCommand cmd = new SqlCommand("exec NUEVA.dbo.SP_JVAR_RES_SIS_SIPA " + vAnio + ", " + vMes + ", ''", conSAP00);
            //cmd.CommandType = CommandType.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();

            lblRuta.Text = dtDato.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CrearTablaSIS_DNI(int vAnio, int vMes, string det)
    {

        try
        {
            //con.Open();
            string sql = "IF OBJECT_ID('NUEVA.dbo.jvarTMPdniREFCON', 'U') IS NOT NULL DROP TABLE NUEVA.dbo.jvarTMPdniREFCON; ";
            /* sql += "	select SIS.DNI_SIS into NUEVA.dbo.jvarTMPdniREFCON ";
             sql += "	from  ";
             sql += "		(select NHC_SIS, APP_SIS, APM_SIS, NOM_SIS, DNI_SIS, NFA_SIS, cast(SIS.FEC_SIS as date) AS FEC_SIS, HOR_SIS, FNC_SIS  ";
             sql += "		from NUEVA.dbo.SIS  ";
             sql += "		where NFA_SIS in  ";
             sql += "			(select NFA_SIS  ";
             sql += "			FROM  ";
             sql += "				(select min(NFA_SIS) as NFA_SIS, NHC_SIS as NHC_SIS, DNI_SIS  ";
             sql += "				from NUEVA.dbo.SIS  ";
             sql += "				WHERE YEAR(FEC_SIS)=@vAnio AND MONTH(FEC_SIS)<@vMes  ";
             sql += "				group by NHC_SIS, DNI_SIS)  ";
             sql += "			TAB) ";
             sql += "		) SIS  ";
             sql += "	INNER JOIN  ";
             sql += "		(select * from NUEVA.dbo.TARJETON  ";
             sql += "		where YEAR(FEC_TAR)=@vAnio and MONTH(FEC_TAR)<@vMes and (SER_TAR = '71' OR SER_TAR = '26')  ";
             sql += "		and HIC_TAR+cast(NUR_TAR as varchar) IN  ";
             sql += "			(SELECT HIC_TAR+cast(NUR_TAR as varchar)  ";
             sql += "			FROM  ";
             sql += "				(select HIC_TAR, MIN(NUR_TAR) AS NUR_TAR  ";
             sql += "				from NUEVA.dbo.TARJETON  ";
             sql += "				where YEAR(FEC_TAR)=@vAnio and MONTH(FEC_TAR)<@vMes and CIT_TAR='X'  ";
             sql += "				GROUP BY HIC_TAR)  ";
             sql += "			TAB)) TARJETON ";
             sql += "	ON SIS.NHC_SIS = TARJETON.HIC_TAR ";*/
            sql += "	select SIS.DNI_SIS AS IDNI, @vMes as vMes into NUEVA.dbo.jvarTMPdniREFCON ";
            sql += "	from ";
            sql += "		(select NHC_SIS, DNI_SIS, NFA_SIS, cast(SIS.FEC_SIS as date) AS FEC_SIS, HOR_SIS, FNC_SIS ";
            sql += "		from NUEVA.dbo.SIS ";
            sql += "		where NFA_SIS in ";
            sql += "			(select NFA_SIS ";
            sql += "			FROM ";
            sql += "				(select min(NFA_SIS) as NFA_SIS, NHC_SIS as NHC_SIS, DNI_SIS ";
            sql += "				from NUEVA.dbo.SIS ";
            sql += "				WHERE YEAR(FEC_SIS)=@vAnio ";
            sql += "				group by NHC_SIS, DNI_SIS) ";
            sql += "			TAB) ";
            sql += "		) SIS ";
            sql += "	inner JOIN ";
            sql += "		(select * from NUEVA.dbo.TARJETON ";
            sql += "		where YEAR(FEC_TAR)=@vAnio and (SER_TAR = '71' OR SER_TAR = '26') and (USU_TAR = '150' OR USU_TAR = '171') ";
            sql += "		and HIC_TAR+cast(NUR_TAR as varchar) IN ";
            sql += "			(SELECT HIC_TAR+cast(NUR_TAR as varchar) ";
            sql += "			FROM ";
            sql += "				(select HIC_TAR, MIN(NUR_TAR) AS NUR_TAR ";
            sql += "				from NUEVA.dbo.TARJETON ";
            sql += "				where YEAR(FEC_TAR)=@vAnio and CIT_TAR='X' ";
            sql += "				GROUP BY HIC_TAR) ";
            sql += "			TAB)) TARJETON ";
            sql += "	ON SIS.NHC_SIS = TARJETON.HIC_TAR ";
            sql += whereSql;
            //sql += "	--where month(SIS.FEC_SIS)<2 ";
            //sql += "	--where month(TARJETON.FRG_TAR)<2 ";

            SqlCommand cmd = new SqlCommand(sql, conSAP00);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@vAnio",vAnio);
            cmd.Parameters.AddWithValue("@vMes", vMes);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();

        }
        catch (Exception ex)
        {
            //conSAP00.Close();
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