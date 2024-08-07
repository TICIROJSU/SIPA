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

public partial class ASPX_IROJVAR_CitasProg_Default : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        int vAnio = Int32.Parse(DDLAnio.SelectedValue);
        int vMes = Int32.Parse(DDLMes.SelectedValue);
        try
        {
            //con.Open();
            string qSql = "select Año, datename(month, concat(Año,'-',Mes,'-01')) as Mes, pro.Cod_Ser, ser.DES_SER, (case when PRO.Tur_Ser='M' then 'MAÑANA' WHEN PRO.Tur_Ser='T' THEN 'TARDE' ELSE '' END) AS Turno, SER.PIS_SER, " +
                "(case when pro.Cod_Per='000' then '' else pro.Cod_Per end) as CodPer, (case when pro.Cod_Per='000' then '' else per.APE_PER end) as Personal, (case when Lun_Ser=0 then null else Lun_Ser end) as Lun, (case when Mar_Ser=0 then null else Mar_Ser end) as Mar, " +
                "(case when Mie_Ser=0 then null else Mie_Ser end) as Mie, (case when Jue_Ser=0 then null else Jue_Ser end) as Jue, (case when Vie_Ser=0 then null else Vie_Ser end) as Vie, (case when Sab_Ser=0 then null else Sab_Ser  end) as Sab " +
                "from NUEVA.dbo.PROGRAMACION PRO inner join NUEVA.dbo.SERVICIO SER on PRO.Cod_Ser=SER.COD_SER INNER JOIN NUEVA.dbo.PERSONAL PER ON PRO.Cod_Per=PER.COD_PER " +
                "where Año='" + vAnio + "' and Mes='" + vMes + "' and Lun_Ser+Mar_Ser+Mie_Ser+Jue_Ser+Vie_Ser+Sab_Ser <> 0 order by SER.DES_SER, PRO.Tur_Ser;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();

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