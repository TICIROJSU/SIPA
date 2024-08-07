using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_ProgCupos_ProgPerAdicionalSIPA : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        IniPersonalUsu(lblidUser2.Text);

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth - 1;
            DDLAnio.SelectedValue = DateTime.Now.Year.ToString();

            IniPersonalServ(lblPerCod.Text, DDLAnio.SelectedValue);

            CargaCalendario(Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text);
        }

    }

    public void IniPersonalUsu(string codUsu)
    {
        try
        {
            string qSql = "select * from NUEVA.dbo.PERSONAL per left join NUEVA.dbo.USUARIO usu on per.USU_PER = usu.Cod_Usu where usu.Cod_Usu=@Cod_Usu ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Cod_Usu", codUsu);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            lblPerCod.Text = dtDato.Rows[0]["COD_PER"].ToString();
            lblPerNom.Text = dtDato.Rows[0]["APE_PER"].ToString();

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void IniPersonalServ(string COMTAR, string vAnio)
    {
        try
        {
            string qSql = "select COM_TAR, SER_TAR, DES_SER, DSC_SER " +
                "from NUEVA.dbo.TARJETON Tar " +
                "left join NUEVA.dbo.SERVICIO Ser on Tar.SER_TAR = Ser.COD_SER " +
                "where year(FEC_TAR)=@Anio and COM_TAR = @COMTAR " +
                "group by COM_TAR, SER_TAR, DES_SER, DSC_SER " +
                "order by SER_TAR ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@COMTAR", COMTAR);
            cmd.Parameters.AddWithValue("@Anio", vAnio);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //ListItem LisTMP = new ListItem("Todos", "", true);
            ddlServicio.DataSource = dtDato;
            //ddlServicio.Items.Add(LisTMP);
            ddlServicio.DataTextField = "DSC_SER";
            ddlServicio.DataValueField = "SER_TAR";
            ddlServicio.DataBind();

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    [WebMethod]
    public static string GetBtnBuscarServ(string vCodPer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "select PSer.idProgPer, PSer.COD_SER, Ser.DSC_SER, PSer.Tur_Ser, PPFechaCupos " +
                "from TabLibres.dbo.ProgPer PSer " +
                "left join NUEVA.dbo.SERVICIO Ser on PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where Cod_Per = '" + vCodPer + "' and PPFechaCupos >= GETDATE() and PPEstado = 'Activo' " +
                "order by PPFechaCupos ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class='text-center'>N° </th>";
                gDetH += "<th class='text-center'>Sel </th>";
                gDetH += "<th class='text-center'>Fecha</th>";
                gDetH += "<th class='text-center'>CodSer</th>";
                gDetH += "<th class='text-center'>Servicio</th>";
                gDetH += "<th class='text-center'>Turno</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelServicio('" + dbRow["idProgPer"].ToString() + "', '" + dbRow["COD_SER"].ToString() + "', '" + dbRow["DSC_SER"].ToString() + "', '" + dbRow["Tur_Ser"].ToString() + "', '" + dbRow["PPFechaCupos"].ToString().Substring(0, 10) + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["PPFechaCupos"].ToString().Substring(0, 10) + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["COD_SER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["DSC_SER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Tur_Ser"].ToString() + "</td>";

                    gDetH += "</tr>" + Environment.NewLine;
                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetH += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    [WebMethod]
    public static string GetAdiPac(string vFecha, string vSer, string vPer, string vTur)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            //select cast((cast(cast(FEC_TAR as date) as varchar(10)) + ' ' + HOR_TAR) as smalldatetime) Cita,
            qSql = "select FORMAT(FEC_TAR, 'dd/MM/yyyy') Cita, " +
                "TUR_TAR Turno, HOR_TAR Hora, " +
                "HIC_TAR HC, IDNI as DNI, IAP + ' ' + IAM + ' ' + INO as Paciente, " +
                "Des_Pg as Programa, IOC as Ocupacion, " +
                "ISE as Sexo, FORMAT(IFN, 'dd/MM/yyyy') as FecNac, " +
                "ITC as Telefono, cast(FRG_TAR as smalldatetime) Registro, " +
                "SER_TAR IDSer, COM_TAR IDPer, PRG_TAR IDProg, TIK_TAR " +
                "from NUEVA.dbo.TARJETON " +
                "left join NUEVA.dbo.HISTORIA on TARJETON.HIC_TAR = HISTORIA.IHC " +
                "left join NUEVA.dbo.PROGRAMA on TARJETON.PRG_TAR = PROGRAMA.Cod_Pg " +
                "where cast(FEC_TAR as date) = cast('" + vFecha + "' as date) " +
                "and SER_TAR='" + vSer + "' and COM_TAR='" + vPer + "' " +
                "and CIT_TAR='X' and ADI_TAR='X' " +
                "order by 1, Turno ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class='text-center'>N° </th>";
                gDetH += "<th class='text-center'>Cita </th>";
                gDetH += "<th class='text-center'>Turno</th>";
                gDetH += "<th class='text-center'>HC</th>";
                gDetH += "<th class='text-center'>DNI</th>";
                gDetH += "<th class='text-center'>Paciente</th>";
                gDetH += "<th class='text-center'>Programa</th>";
                gDetH += "<th class='text-center'>Sexo</th>";
                //gDetH += "<th class='text-center'>FecNac</th>";
                gDetH += "<th class='text-center'>TIK_TAR</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td>" + dbRow["Hora"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Turno"].ToString() + "</td>";
                    gDetH += "<td>" + dbRow["HC"].ToString() + "</td>";
                    gDetH += "<td>" + dbRow["DNI"].ToString() + "</td>";
                    gDetH += "<td class='text-left'>" + dbRow["Paciente"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Programa"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Sexo"].ToString() + "</td>";
                    //gDetH += "<td>" + dbRow["FecNac"].ToString() + "</td>";
                    //gDetH += "<td>" + dbRow["Telefono"].ToString() + "</td>";
                    gDetH += "<td>" + dbRow["TIK_TAR"].ToString() + "</td>";
                    gDetH += "</tr>" + Environment.NewLine;
                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetH += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }


    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        int vAnio = Convert.ToInt32(DDLAnio.SelectedValue);
        int vMes = Convert.ToInt32(DDLMes.SelectedValue);
        CargaCalendario(vAnio, vMes, DDLMes.SelectedItem.Text);
    }
    
    public void CargaCalendario(int vAnio, int vMes, string vMesNom)
    {
        string lhtml = "";
        int Days = DateTime.DaysInMonth(vAnio, vMes);

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
            DataTable vMesSerPer = GetPerSerMesAdicionales(vFechaFor.Year.ToString(), vFechaFor.Month.ToString(), ddlServicio.SelectedValue, lblPerCod.Text, ddlTurno.SelectedValue);

            for (int i = 1; i <= Days; i++)
            {
                lhtml += "<tr height='70'> ";
                for (int j = 0; j <= 6; j++)
                {
                    string vDia = "", vDiaHTML = "", vDiamsg = "", vColorCelda = "", vLinkCelda = "", vDiaAdiCit = "", vDiaAdiNoCit = "", vDiaCit = "";
                    int diaSemNro = (int)vFechaFor.DayOfWeek;
                    if (diaSemNro == j)
                    {
                        vDia = i.ToString();
                        vDiaAdiCit = vMesSerPer.Compute("Sum(AdiCit)", "FecDia = " + vDia).ToString();
                        vDiaAdiNoCit = vMesSerPer.Compute("Sum(AdiNoCit)", "FecDia = " + vDia).ToString();
                        vDiaCit = vMesSerPer.Compute("Sum(Citados)", "FecDia = " + vDia).ToString();


                        vDiaHTML = "<table style='font-size:22px;' class='table-bordered'> " +
                            "<tr><td colspan='3'>" +
                            "   <div align='center'>" + vDia + "</div>" +
                            "</td></tr> " +
                            "<tr> " +
                            "   <td >" +
                            "       <div class='btn btn-block bg-orange' align='center' data-toggle='modal' data-target='#mAdiPac' onclick=\"fAdiPac('" + vFechaFor.ToShortDateString() + "')\">" + vDiaAdiCit + "</div>" +
                            "   </td> " +
                            "   <td >" +
                            "       <div class='btn btn-block btn-warning' align='center'>" + vDiaAdiNoCit + "</div>" +
                            "   </td> " +
                            "   <td >" +
                            "       <div class='btn btn-block bg-purple' align='center'>" + vDiaCit + "</div>" +
                            "   </td> " +
                            "</tr> " +
                            "</table>"; 

                        //vDiamsg = CargaTipoTrabajo(Session["PerDNI"].ToString(), vFechaFor);
                        if (vDiamsg == "PRESENCIAL")
                        { vColorCelda = "style='background-color:#81C784'"; }
                        if (vDiamsg == "REMOTO")
                        { vColorCelda = "style='background-color:#F9A825'"; }

                        //if (vColorCelda != "")
                        if (vDiamsg == "REMOTO")
                        { vLinkCelda = "data-toggle='modal' data-target='#modalTrabDescrip' onclick=\"fTrabDescr('" + vDia + "', '" + vFechaFor.ToShortDateString() + "')\""; }

                        vFechaFor = vFechaTrab.AddDays(i);
                        i++;
                    }
                    if (i > Days + 1)
                    {
                        vDia = ""; vDiamsg = ""; vColorCelda = ""; vLinkCelda = ""; vDiaHTML = "";
                    }
                    lhtml += "	<td " + vColorCelda + " " + vLinkCelda + "> ";
                    lhtml += vDiaHTML;
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


    private static DataTable GetPerSerMesAdicionales(string vAnio, string vMes, string vSer, string vPer, string vTur)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();

        try
        {
            string qSql = "select day(FEC_TAR) as FecDia, cast(FEC_TAR as date) FEC_TAR, " +
                "sum(case when (CIT_TAR='X' and ADI_TAR='X') then 1 else 0 end) AdiCit, " +
                "sum(case when (CIT_TAR<>'X' and ADI_TAR='X') then 1 else 0 end) AdiNoCit, " +
                "sum(case when (CIT_TAR='X' and ADI_TAR<>'X') then 1 else 0 end) Citados " +
                "from NUEVA.dbo.TARJETON " +
                "where year(FEC_TAR)='" + vAnio + "' and month(FEC_TAR)='" + vMes + "' " +
                "and SER_TAR='" + vSer + "' and COM_TAR='" + vPer + "' " +
                "and TUR_TAR='" + vTur + "' " +
                "group by FEC_TAR";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            dt = objdataset.Tables[0];
        }
        catch (Exception ex)
        {
            //LitErrores.Text += "-" + "-" + ex.Message.ToString();
        }

        return dt;

    }

}
