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

public partial class ASPX_IROJVARSIPA_Programacion_ProSerCalendar : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
        Server.ScriptTimeout = 3600;
        
        //

        if (!Page.IsPostBack)
        {
            IniServicios();
            ddlServicio.SelectedValue = Request.QueryString["codPer"];
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            int vmonth = DateTime.Now.Month - 1;
            DDLMes.SelectedIndex = vmonth;

            //CargaTablaDTIni();
            //CargaTablaDT();
            LitCalendar.Text = CargaCalendario(Request.QueryString["codPer"], Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "M");
            LitPeriodo.Text = DDLMes.SelectedItem.Text + " de " + DDLAnio.SelectedValue;
            LitPeriodo.Text = "Turno Mañana";

            LitCalendarT.Text = CargaCalendario(Request.QueryString["codPer"], Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "T");
            LitPeriodoT.Text = "Turno Tarde";

        }

        if (!Page.IsPostBack) {
            string vServ = ddlServicio.SelectedValue;
        }

    }

    public void IniServicios()
    {
        try
        {

            string qSql = "select * from NUEVA.dbo.SERVICIO order by DSC_SER ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //ListItem LisTMP = new ListItem("OFT. GEN.", "71", true);
            ddlServicio.DataSource = dtDato;
            //ddlServicio.Items.Add(LisTMP);
            ddlServicio.DataTextField = "DSC_SER";
            ddlServicio.DataValueField = "COD_SER";
            ddlServicio.DataBind();

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    [WebMethod]
    public static string GetDetAtenciones(string vanio, string vmes, string vdia, string turno, string vplaza)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_HISMINSA_DETATENCIONES", conSAP00i);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@vanio", SqlDbType.VarChar); cmd.Parameters.Add("@vmes", SqlDbType.VarChar);
            cmd.Parameters.Add("@vdia", SqlDbType.VarChar); cmd.Parameters.Add("@vturno", SqlDbType.VarChar);
            cmd.Parameters.Add("@vplaza", SqlDbType.VarChar);

            cmd.Parameters["@vanio"].Value = vanio; cmd.Parameters["@vmes"].Value = vmes;
            cmd.Parameters["@vdia"].Value = vdia; cmd.Parameters["@vturno"].Value = turno;
            cmd.Parameters["@vplaza"].Value = vplaza;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>FRT</th>";
                gDetAtenciones += "<th class=''>Pag</th>";
                gDetAtenciones += "<th class=''>Reg</th>";
                gDetAtenciones += "<th class=''>DNI</th>";
                gDetAtenciones += "<th class=''>Cliente</th>";
                gDetAtenciones += "<th class=''>Edad</th>";
                gDetAtenciones += "<th class=''>Sexo</th>";
                gDetAtenciones += "<th class=''>Dx</th>";
                gDetAtenciones += "<th class=''>Fin</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_FRT"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_PAG"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["NUM_REG"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["DNI"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Nombres"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["EDAD"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["SEXO"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";

                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["Fin1"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    public string CargaCalendario(string vCodPer, int vAnio, int vMes, string vMesNom, string vTurno)
    {
        string lhtml = "";
        int Days = DateTime.DaysInMonth(vAnio, vMes);
        //txtCantDias.Text = Days.ToString();
        //txtIdPer.Text = Session["PerID"].ToString();
        //txtDNI.Text = Session["PerDNI"].ToString();
        //txtDatosPer.Text = Session["PerDatos"].ToString();
        string vplaza = BuscaPlaza(vCodPer);
        //vplaza = "14113829506";
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
                    string vDia = "", vDiamsg = "", vColorCelda = "", vLinkCelda = "";
                    int diaSemNro = (int)vFechaFor.DayOfWeek;
                    if (diaSemNro == j)
                    {
                        vDia = i.ToString();

                        vDiamsg = CargaTipoTrabajo(vCodPer, vFechaFor, vTurno);

                        if (vDiamsg != "")
                        {
                            vColorCelda = "style='background-color:#81C784'";
                            vLinkCelda = "data-toggle='modal' data-target='#modalDet_Atenciones' onclick=\"Det_Atenciones('" + vAnio + "', '" + vMes + "', '" + vDia + "', '" + vTurno + "', '" + vplaza + "')\"";
                        }

                        vFechaFor = vFechaTrab.AddDays(i);
                        i++;
                    }
                    if (i > Days + 1)
                    {
                        vDia = ""; vDiamsg = ""; vColorCelda = ""; vLinkCelda = "";
                    }
                    lhtml += "	<td " + vColorCelda + " " + vLinkCelda + "> ";

                    //lhtml += "		<h3><span id='' class='pull-right'>" + vDia + "</span></h3> ";
                    ////lhtml += "  " + vDiachk;
                    //lhtml += "		<span id='spDiaMsg" + vDia + "'>" + vDiamsg + "</span> ";


                    lhtml += "<table style='font-size:22px;' class='table-bordered'> " +
                        "<tbody> " +
                        "	<tr> " +
                        "		<td colspan='1' rowspan='2'><div align='center'><h3>" + vDia + "</h3></div></td> " +
                        "		<td colspan='2' ><div class='btn btn-block ' align='center' style='background-color: #9BE2E3; font-weight:bold;'>" + vDiamsg + " </div></td> " +
                        "	</tr> " +
                        "	<tr> " +
                        "		<td><div class='btn btn-block ' align='center' style='background-color: #9BE2E3; font-weight:bold;'></div></td> " +
                        "		<td><div class='btn btn-block ' align='center' style='background-color: #9BC9E3; font-weight:bold;'></div></td> " +
                        "	</tr> " +
                        "	<tr> " +
                        "		<td> " +
                        "			<div class='btn btn-block bg-orange' align='center'></div> " +
                        "		</td> " +
                        "		<td> " +
                        "			<div class='btn btn-block btn-warning' align='center'></div> " +
                        "		</td> " +
                        "		<td> " +
                        "			<div class='btn btn-block bg-purple' align='center'></div> " +
                        "		</td> " +
                        "	</tr> " +
                        "</tbody> " +
                        "</table> ";


                    lhtml += "	</td> ";
                }
                lhtml += "</tr> ";
                i--;
            }

            lhtml += "	</tbody> ";
            lhtml += "</table> ";

        }
        catch (Exception ex)
        {
            lhtml += ex.Message.ToString();
        }

        return lhtml;
    }


    public string CargaTipoTrabajo(string vCodPer, DateTime vFechaFor, string vTurno)
    {
        string vTipTrab = "";
        try
        {
            string qSql = "select *, LEFT(Per.APE_PER, 15) as NomAbr " +
                "from TabLibres.dbo.ProgPer PPer " +
                "left join NUEVA.dbo.PERSONAL Per on PPer.Cod_Per COLLATE Modern_Spanish_CI_AS = Per.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "where PPer.COD_SER = @Cod_Per and (PPer.PPFechaCupos) = @PPFechaCupos and PPer.Tur_Ser = @Tur_Ser " +
                "order by PPFechaCupos  ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@Cod_Per", vCodPer);
            cmd2.Parameters.AddWithValue("@Tur_Ser", vTurno);
            cmd2.Parameters.AddWithValue("@PPFechaCupos", vFechaFor);
            //cmd2.Parameters.AddWithValue("@Mes", vMes);

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            string vcoma = "";
            foreach (DataRow dbRow in dtDato.Rows)
            {
                vTipTrab = vTipTrab + vcoma + dbRow["NomAbr"].ToString();
                vcoma = ", <br />";
            }

            //if (dtDato.Rows.Count > 0)
            //{         //}

        }
        catch (Exception ex)
        {
            vTipTrab += ex.Message.ToString();
        }

        return vTipTrab;
    }

    public string BuscaPlaza(string vCodPer)
    {
        string vTipTrab = "";
        try
        {
            string qSql = "select * from NUEVA.dbo.PERSONAL where COD_PER = @COD_PER ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@COD_PER", vCodPer);

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                vTipTrab = dtDato.Rows[0]["HIS_PER"].ToString();
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
        //ddlServicio.SelectedValue = Request.QueryString["codPer"];
        LitCalendar.Text = CargaCalendario(ddlServicio.SelectedValue, Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "M");
        LitPeriodo.Text = DDLMes.SelectedItem.Text + " de " + DDLAnio.SelectedValue;
        LitPeriodo.Text = "Turno Mañana";

        LitCalendarT.Text = CargaCalendario(ddlServicio.SelectedValue, Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "T");
        LitPeriodoT.Text = "Turno Tarde";
    }
}
