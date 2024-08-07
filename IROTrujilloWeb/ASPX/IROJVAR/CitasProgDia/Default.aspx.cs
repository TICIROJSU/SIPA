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

public partial class ASPX_IROJVAR_CitasProgDia_Default : System.Web.UI.Page
{
    string html = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (Page.IsPostBack)
        {
            
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT("M", "MAÑANA", LitTABL1);
        CargaTablaDT("T", "TARDE", LitTABLTarde);        
    }

    public void CargaTablaDT(string vt, string vturno, Literal vLiteral)
    {
        int vAnio = Int32.Parse(DDLAnio.SelectedValue);
        int vMes = Int32.Parse(DDLMes.SelectedValue);
        string vGrupo = DDLGrupo.SelectedValue;
        try
        {
            //con.Open();
            string qSql = "select Año, Mes, pro.Cod_Ser, ser.DSC_SER AS DES_SER, PRO.Tur_Ser, SER.PIS_SER, sum(Lun_Ser) as Lun_Ser, sum(Mar_Ser) as Mar_Ser, sum(Mie_Ser) as Mie_Ser, sum(Jue_Ser) as Jue_Ser, sum(Vie_Ser) as Vie_Ser, sum(Sab_Ser) as Sab_Ser " +
                "from NUEVA.dbo.PROGRAMACION PRO inner join NUEVA.dbo.SERVICIO SER on PRO.Cod_Ser = SER.COD_SER " +
                "where Año='" + vAnio + "' and Mes=(case when (select COUNT(*) from NUEVA.dbo.PROGRAMACION where Año='" + vAnio + "' and Mes='" + vMes + "')>1 then '" + vMes + "' else '0' end) and Lun_Ser+Mar_Ser+Mie_Ser+Jue_Ser+Vie_Ser+Sab_Ser <> 0 and PRO.Tur_Ser='" + vt + "' and SER.COD_GRU like '%" + vGrupo + "%' " +
                "group by Año, Mes, pro.Cod_Ser, ser.DES_SER, ser.DSC_SER, PRO.Tur_Ser, SER.PIS_SER " +
                "order by SER.PIS_SER, SER.DES_SER; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            CargaTablaxMeses(dtDato, vt, vturno, vLiteral);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public string CargaPersonal(string codServicio, string vDia, string vTurno)
    {
        int vAnio = Int32.Parse(DDLAnio.SelectedValue);
        int vMes = Int32.Parse(DDLMes.SelectedValue);
        string vCadena = "";
        try
        {
            //con.Open();
            string qSql = "select Año, Mes, pro.Cod_Ser, (case when pro.Cod_Per='000' then '' else per.APE_PER end) as Personal " +
                "from NUEVA.dbo.PROGRAMACION PRO INNER JOIN NUEVA.dbo.PERSONAL PER ON PRO.Cod_Per = PER.COD_PER " +
                "where Año='" + vAnio + "' and Mes='" + vMes + "' and Lun_Ser+Mar_Ser + Mie_Ser + Jue_Ser + Vie_Ser + Sab_Ser <> 0 and " +
                "PRO.Tur_Ser = '" + vTurno + "' and pro.Cod_Ser = '" + codServicio + "' and " + vDia + ">0; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count>0)
            {
                foreach (DataRow dbRow in dtDato.Rows)
                {
                    if (dbRow["Personal"].ToString()==""){vCadena += "";}
                    else{vCadena += "<br>-<small>" + dbRow["Personal"].ToString() + "</small>"; }
                }
            }
            else{vCadena = "";}
        }
        catch (Exception ex)
        {
            vCadena = ex.Message.ToString();
        }
        return vCadena;
    }

    public void CargaTablaxMeses(DataTable dtDato, string vt, string vturno, Literal vLiteral)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            //html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr><td colspan=8 style='text-align: center; '><h2>" + vturno + "</h2></td></tr>";
            //html += "<tr>";
            //html += "<th class='' style='text-align: center; display:none; '>Piso</th>";
            //html += "<th class='' style='visibility: hidden; '>Turno</th>";
            //html += "<th class=''>Lunes</th>";
            //html += "<th class=''>Martes</th>";
            //html += "<th class=''>Miercoles</th>";
            //html += "<th class=''>Jueves</th>";
            //html += "<th class=''>Viernes</th>";
            //html += "<th class=''>Sabado</th>";
            //html += "</tr>" + Environment.NewLine;
            int nroitem = 0, vPiso = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vsLun = "", vsMar = "", vsMie = "", vsJue = "", vsVie = "", vsSab = "";
                if (dbRow["Lun_Ser"].ToString() != "0") { vsLun = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Lun_Ser", vt); }
                if (dbRow["Mar_Ser"].ToString() != "0") { vsMar = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Mar_Ser", vt); }
                if (dbRow["Mie_Ser"].ToString() != "0") { vsMie = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Mie_Ser", vt); }
                if (dbRow["Jue_Ser"].ToString() != "0") { vsJue = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Jue_Ser", vt); }
                if (dbRow["Vie_Ser"].ToString() != "0") { vsVie = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Vie_Ser", vt); }
                if (dbRow["Sab_Ser"].ToString() != "0") { vsSab = "<b>" + dbRow["DES_SER"].ToString() + "</b>" + CargaPersonal(dbRow["Cod_Ser"].ToString(), "Sab_Ser", vt); }
                if (vPiso < Int32.Parse(dbRow["PIS_SER"].ToString()))
                {
                    vPiso = Int32.Parse(dbRow["PIS_SER"].ToString());
                    html += "<tr><td></td><td colspan=7 style='text-align: left; font-size:26px; '> Piso: " + dbRow["PIS_SER"].ToString() + "</td></tr>";
                    html += "<tr>";
                    html += "<th class='' style='text-align: center; display:none; '>Piso</th>";
                    html += "<th class='' style='visibility: hidden; '>Turno</th>";
                    html += "<th class=''>Lunes</th>";
                    html += "<th class=''>Martes</th>";
                    html += "<th class=''>Miercoles</th>";
                    html += "<th class=''>Jueves</th>";
                    html += "<th class=''>Viernes</th>";
                    html += "<th class=''>Sabado</th>";
                    html += "</tr>" + Environment.NewLine;
                }
                html += "<tr>";
                html += "<td class='' style='text-align: center; display:none; '>" + dbRow["PIS_SER"].ToString() + "</td>";
                html += "<td class='' style='text-align: left; visibility: hidden; '>" + dbRow["Tur_Ser"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsLun + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsMar + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsMie + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsJue + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsVie + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + vsSab + "</td>";
                html += "</tr>" + Environment.NewLine;
            }

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        vLiteral.Text = html;
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
        response.Write(LitTABL1.Text + LitTABLTarde.Text);
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