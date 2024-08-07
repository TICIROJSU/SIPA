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

public partial class ASPX_IROJVAR_ProgCupos_ProgPerCalendar : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
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



    }

    [WebMethod]
    public static string GetDetAtenciones(string vanio, string vmes, string vdia, string turno, string vplaza, string vCodPer, string vFechaFor, string vCodSer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "<div class='row' style='display:block; background-color:#AEB6BF'> " +
                    "	<div class='column'> " +
                    "		<div class='col-md-10'> " +
                    "			<div class='row'> " +
                    "				<div class='btn btn-app hide' style='background-color:#D4E6F1' onclick='btnGuardar()'> " +
                    "					<i class='fa fa-save'></i> Guardar " +
                    "				</div> " +
                    "				<a class='btn btn-app btn-danger bg-danger' style='background-color:#E6B0AA' onclick=\"btnAnularProgPer('"+ vCodPer + "', '" + vFechaFor + "', '" + turno + "')\" > " +
                    "					<i class='fa fa-times-circle'></i> Anular " +
                    "				</a> " +
                    "			</div> " +
                    "		</div> " +
                    "	</div> " +
                    "</div> ";
        try
        {
            SqlCommand cmd = new SqlCommand("NUEVA.dbo.SP_JVAR_HISMINSA_DETATENCIONES", conSAP00i);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@vanio", SqlDbType.VarChar); cmd.Parameters.Add("@vmes", SqlDbType.VarChar);
            cmd.Parameters.Add("@vdia", SqlDbType.VarChar); cmd.Parameters.Add("@vturno", SqlDbType.VarChar);
            cmd.Parameters.Add("@vplaza", SqlDbType.VarChar);
            cmd.Parameters.Add("@vFechaFor", SqlDbType.VarChar);
            cmd.Parameters.Add("@vCodSer", SqlDbType.VarChar);

            cmd.Parameters["@vanio"].Value = vanio; cmd.Parameters["@vmes"].Value = vmes;
            cmd.Parameters["@vdia"].Value = vdia; cmd.Parameters["@vturno"].Value = turno;
            cmd.Parameters["@vplaza"].Value = vplaza;
            cmd.Parameters["@vFechaFor"].Value = vFechaFor;
            cmd.Parameters["@vCodSer"].Value = vCodSer;

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            DataTable dt1 = objdataset.Tables[1];
            

            if (dt1.Rows.Count > 0)
            {
                DataRow dr1 = dt1.Rows[0];
                gDetHTML += "<div class='row' style='display:block; background-color:#4dd2ff'> " +
                    "   <div class='column'> " +
                    "	  <div class='col-md-3' style='text-align: center;padding:  0px 30px 0px 30px;'> " +
                    "		 <div class='box-body'> " +
                    "			<div class='form-group'> " +
                    "				<h3>"+ dr1["TarjCont"].ToString() + "</h3> " +
                    "				<code style='font-size:large; background-color:transparent; '>Citas Total</code> " +
                    "			</div> " +
                    "		 </div> " +
                    "	  </div> " +
                    "   </div> " +
                    "	<div class='column'> " +
                    "	  <div class='col-md-3' style='text-align: center;padding:  0px 30px 0px 30px;'> " +
                    "		 <div class='box-body'> " +
                    "			<div class='form-group'> " +
                    "				<h3>" + dr1["Citados"].ToString() + "</h3> " +
                    "				<code style='font-size:large; background-color:transparent; '>Citados</code> " +
                    "			</div> " +
                    "		 </div> " +
                    "	  </div> " +
                    "   </div> " +
                    "	<div class='column'> " +
                    "	  <div class='col-md-3' style='text-align: center;padding:  0px 30px 0px 30px;'> " +
                    "		 <div class='box-body'> " +
                    "			<div class='form-group'> " +
                    "				<h3>" + dr1["CitaAdicional"].ToString() + "</h3> " +
                    "				<code style='font-size:large; background-color:transparent; '>Cita Adicional</code> " +
                    "			</div> " +
                    "		 </div> " +
                    "	  </div> " +
                    "   </div> " +
                    "	<div class='column'> " +
                    "	  <div class='col-md-3' style='text-align: center;padding:  0px 30px 0px 30px;'> " +
                    "		 <div class='box-body'> " +
                    "			<div class='form-group'> " +
                    "			   <h3>" + dr1["Adicionales"].ToString() + "</h3> " +
                    "				<code style='font-size:large; background-color:transparent; '>Adicionales</code> " +
                    "			</div> " +
                    "		 </div> " +
                    "	  </div> " +
                    "   </div> " +
                    " </div> ";
            }
            

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetHTML += "<tr>";
                gDetHTML += "<th class=''>N° </th>";
                gDetHTML += "<th class=''>FRT</th>";
                gDetHTML += "<th class=''>Pag</th>";
                gDetHTML += "<th class=''>Reg</th>";
                gDetHTML += "<th class=''>DNI</th>";
                gDetHTML += "<th class=''>Cliente</th>";
                gDetHTML += "<th class=''>Edad</th>";
                gDetHTML += "<th class=''>Sexo</th>";
                gDetHTML += "<th class=''>Dx</th>";
                gDetHTML += "<th class=''>Fin</th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetHTML += "<tr>";
                    gDetHTML += "<td style='text-align: left;'>" + nroitem + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["NUM_FRT"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["NUM_PAG"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["NUM_REG"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["DNI"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + dbRow["Nombres"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["EDAD"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["SEXO"].ToString() + "</td>";
                    gDetHTML += "<td style='text-align: left;'>" + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalDetDx'  onclick=\"DetDx('" + dbRow["ID_HIS"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button>" + "</td>";
                    gDetHTML += "<td style='text-align: center;'>" + dbRow["Fin1"].ToString() + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetHTML += vplaza + "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
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

                        string[] vDiamsgSplit = vDiamsg.Split('&');
                        vDiamsg = vDiamsgSplit[0] + "-" + vDiamsgSplit[1];

                        string vColActivo = "64beff"; // 81c784
                        if (vDiamsgSplit[2] == "Inactivo")
                        {
                            vColActivo = "FF9899";
                        }

                        if (vDiamsg.Length > 3)
                        {
                            vColorCelda = "style='background-color:#" + vColActivo + "'";
                            vLinkCelda = "data-toggle='modal' data-target = '#modalDetAtenciones' " +
                                "onclick=\"DetAtenciones('" + vAnio + "', '" + vMes + "', '" + vDia + "', '" + vTurno + "', '" + vplaza + "', '" + vCodPer + "', '" + vFechaFor.ToString().Substring(0, 10) + "', '" + vDiamsg.Substring(0, 2) + "')\"";
                        }

                        vFechaFor = vFechaTrab.AddDays(i);
                        i++;
                    }
                    if (i > Days + 1)
                    {
                        vDia = ""; vDiamsg = ""; vColorCelda = ""; vLinkCelda = "";
                    }
                    lhtml += "	<td " + vColorCelda + " " + vLinkCelda + "> ";
                    lhtml += "		<h3><span id='' class='pull-right'>" +  vDia + "</span></h3> ";
                    //lhtml += "  " + vDiachk;
                    lhtml += "		<span id='spDiaMsg" + vDia + "'>" + vDiamsg + "</span> ";
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
        string vTipTrab = "& &";
        try
        {
            string qSql = "SELECT PSer.*, Ser.DES_SER, Ser.DSC_SER " +
                "from TabLibres.dbo.ProgPer PSer " +
                "left join NUEVA.dbo.SERVICIO Ser on PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where Cod_Per = @Cod_Per and Tur_Ser = @Tur_Ser and PPFechaCupos = @PPFechaCupos  " +
                "order by PPFechaCupos, PPEstado ";
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

            if (dtDato.Rows.Count > 0)
            {
                vTipTrab = dtDato.Rows[0]["COD_SER"].ToString() + '&' + dtDato.Rows[0]["DES_SER"].ToString() + '&' + dtDato.Rows[0]["PPEstado"].ToString();
            }
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
        LitCalendar.Text = CargaCalendario(Request.QueryString["codPer"], Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "M");
        LitPeriodo.Text = DDLMes.SelectedItem.Text + " de " + DDLAnio.SelectedValue;
        LitPeriodo.Text = "Turno Mañana";

        LitCalendarT.Text = CargaCalendario(Request.QueryString["codPer"], Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text, "T");
        LitPeriodoT.Text = "Turno Tarde";
    }


    [WebMethod]
    public static string SetAnulaRegProg(string Cod_Per, string PPFechaCupos, string Tur_Ser)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        string qSql = "";
        try
        {
            qSql = "update TabLibres.dbo.ProgPer set PPEstado = 'Inactivo' where Cod_Per = @Cod_Per and PPFechaCupos = @PPFechaCupos and Tur_Ser = @Tur_Ser ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Per", Cod_Per);
            cmd.Parameters.AddWithValue("@PPFechaCupos", PPFechaCupos);
            cmd.Parameters.AddWithValue("@Tur_Ser", Tur_Ser);

            conSAP00i.Open();
            cmd.ExecuteNonQuery();
            conSAP00i.Close();

            gHTML = "Registro Inactivado Correctamente";
        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        return gHTML;
    }

}