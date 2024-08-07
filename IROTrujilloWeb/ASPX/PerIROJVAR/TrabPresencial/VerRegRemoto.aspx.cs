using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.Services;
using System.Web.UI;

public partial class ASPX_PerIROJVAR_TrabPresencial_VerRegRemoto : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth - 1;
        }

        int vd, vm, vy;
        vm = DateTime.Now.Month;
        vy = DateTime.Now.Year;

        //txtDNI.Text = Request.QueryString["DNI"];

        CargaCalendario(vy, vm, DDLMes.SelectedItem.Text);

    }

    [WebMethod]
    public static string GetTipoTrabDescrip(string vIdPer, string vDNIPer, string vFechaTrab)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string vTipTrab = "";
        try
        {
            string qSql = "select * from NUEVA.dbo.JVARTipoTrabDescrip " +
                "where DNIPER = @Dni and FechaTrab = @Fec;";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@Dni", vDNIPer);
            cmd2.Parameters.AddWithValue("@Fec", vFechaTrab);

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                vTipTrab += dtDato.Rows[0]["Actividad"].ToString();
                vTipTrab += "||sep||";
                vTipTrab += dtDato.Rows[0]["Ruta"].ToString();
            }
        }
        catch (Exception ex)
        {
            vTipTrab += ex.Message.ToString();
        }

        return vTipTrab;
    }

    public void CargaCalendario(int vAnio, int vMes, string vMesNom)
    {
        string lhtml = "";
        //vMes = 6;
        int Days = DateTime.DaysInMonth(vAnio, vMes);
        txtCantDias.Text = Days.ToString();
        txtIdPer.Text = Session["PerID"].ToString();
        txtDNI.Text = Request.QueryString["DNI"];
        txtDatosPer.Text = Session["PerDatos"].ToString();

        try
        {

            lhtml += "<table> ";
            lhtml += "	<thead class='fc-head'> ";
            lhtml += "		<tr> ";
            lhtml += "			<th>Dom</th> ";
            lhtml += "			<th>Lun</th><th>Mar</th><th>Mie</th> ";
            lhtml += "			<th>Jue</th><th>Vie</th><th>Sab</th> ";
            lhtml += "		</tr> ";
            lhtml += "	</thead> ";
            lhtml += "	<tbody class='fc-body'> ";

            DateTime vFechaTrab = Convert.ToDateTime("01-" + vMes + "-" + vAnio);
            DateTime vFechaFor = vFechaTrab;

            for (int i = 1; i <= Days; i++)
            {
                lhtml += "<tr height='70'> ";
                for (int j = 0; j <= 6; j++)
                {
                    string vDia = "", vDiachk = "", vDiamsg = "", vColorCelda = "", vLinkCelda = "", vFechaForStr = "";
                    int diaSemNro = (int)vFechaFor.DayOfWeek;
                    if (diaSemNro == j)
                    {
                        vFechaForStr = vFechaFor.ToShortDateString();
                        vDia = i.ToString();
                        vDiachk = "<span><input type='checkbox' id='chkDia" + vDia + "' onclick = 'fcal1clic()' /></span>" +
                            "<input type='hidden' id='hFecDia" + vDia + "' value='" + vFechaForStr + "' /> ";
                        vDiamsg = CargaTipoTrabajo(Request.QueryString["DNI"], vFechaFor);
                        if (vDiamsg == "PRESENCIAL")
                        { vColorCelda = "style='background-color:#81C784'"; }
                        if (vDiamsg == "REMOTO")
                        { vColorCelda = "style='background-color:#F9A825'"; }

                        //if (vColorCelda != "")
                        if (vDiamsg == "REMOTO")
                        { vLinkCelda = "data-toggle='modal' data-target='#modalTrabDescrip' onclick=\"fTrabDescr('" + vDia + "', '" + vFechaForStr + "')\""; }

                        vFechaFor = vFechaTrab.AddDays(i);
                        i++;
                    }
                    if (i > Days + 1)
                    {
                        vDia = ""; vDiachk = ""; vDiamsg = ""; vColorCelda = ""; vLinkCelda = "";
                    }
                    lhtml += "	<td id='"+i.ToString() + j.ToString()+"' " + vColorCelda + " ondblclick = \"fcal2clic('DNI', 'Fecha', 'TipoTrab', 'tdId')\" " + /*vLinkCelda +*/ "> ";
                    lhtml += "		<h3><span id='' class='pull-right'>" + vDiachk + vDia + "</span></h3> ";
                    //lhtml += "  " + vDiachk;
                    lhtml += "		<span id='spDiaMsg" + vDia + "' " + vLinkCelda + ">" + vDiamsg + "</span> ";
                    lhtml += "	</td> ";
                }
                lhtml += "</tr> ";
                i--;
            }

            lhtml += "	</tbody> ";
            lhtml += "</table> ";

            LitCalendar.Text = lhtml;
        }
        catch (Exception ex)
        {
            LitCalendar.Text += ex.Message.ToString();
        }

        LitPeriodo.Text = vMesNom + " de " + vAnio;

    }

    public string CargaTipoTrabajo(string vDNIPer, DateTime vFechaTrab)
    {
        string vTipTrab = "";
        try
        {
            string qSql = "select * from NUEVA.dbo.JVARTipoTrab where DNIPER = @Dni and FechaTrab = @Fec";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@Dni", vDNIPer);
            cmd2.Parameters.AddWithValue("@Fec", vFechaTrab);

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                vTipTrab = dtDato.Rows[0]["TipoTrab"].ToString();
            }
        }
        catch (Exception ex)
        {
            vTipTrab += ex.Message.ToString();
        }

        return vTipTrab;
    }


    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        int vAnio = Convert.ToInt32(DDLAnio.SelectedValue);
        int vMes = Convert.ToInt32(DDLMes.SelectedValue);
        CargaCalendario(vAnio, vMes, DDLMes.SelectedItem.Text);
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        String savePath = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Carga\";
        String fileName = "";
        if (FileUpload1.HasFile)
        {
            fileName = FileUpload1.FileName;
            savePath += fileName;
            FileUpload1.SaveAs(savePath);
            SetGuardaFile(txtIdPer.Text, txtDNI.Text, txtDiaSel.Text, savePath);
            UploadStatusLabel.Text = "Tu Archivo fue guardado como " + fileName;
        }
        else
        { UploadStatusLabel.Text = "No haz Cargado ningun Archivo."; }

        UploadStatusLabel.Text += " | Dia: " + txtDiaSel.Text;

    }

    public void SetGuardaFile(string vIdPer, string vDNIPer, string vFechaTrab, string vRuta)
    {
        string gDetHtml = "";
        try
        {
            string qSql = "UPDATE NUEVA.dbo.JVARTipoTrabDescrip SET Ruta = @Ruta WHERE DNIPER = @DNIPER and FechaTrab = @FechaTrab;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
            cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
            cmd.Parameters.AddWithValue("@Ruta", vRuta);
            adapter.SelectCommand = cmd;

            conSAP00.Open(); cmd.ExecuteScalar(); conSAP00.Close();
            gDetHtml += "Tu Archivo fue guardado";
        }
        catch (Exception ex)
        {
            gDetHtml += "No haz Cargado ningun Archivo o No haz guardado tu Actividad. ERROR: " + Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }

        UploadStatusLabel.Text = gDetHtml;
    }


    protected void btnDescarga_Click(object sender, EventArgs e)
    {
        string vruta = @txtDiaSelRuta.Text;
        string filename;
        filename = Path.GetFileName(vruta);

        //Response.ContentType = "application/" + Path.GetExtension(vruta).Substring(1);
        Response.ContentType = "text/xml";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        Response.TransmitFile(Server.MapPath(@"~\ASPX\Carga\" + filename + ""));

    }
}
