using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Programacion_ProgPersonalM : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth - 1;
            DDLAnio.SelectedValue = DateTime.Now.Year.ToString();
            //IniPersonalServ(lblPerCod.Text, DDLAnio.SelectedValue);

            CargaCalendario(Convert.ToInt32(DDLAnio.SelectedValue), Convert.ToInt32(DDLMes.SelectedValue), DDLMes.SelectedItem.Text);
        }

    }

    [WebMethod]
    public static string SetBtnGuardar(string idProgServ, string COD_SER, string Cod_Per, string PIS_SER, string Tur_Ser, string PPAnio, string PPMes, string PPFechaCupos, string PPCupos, string PPAdiLimite, string Hr_Ser, string PPObservacion, string PPEstado, string PPTipTrabajo, string PPCantTurno)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";

        qSql = "SELECT * from TabLibres.dbo.ProgPer where Cod_Per = '" + Cod_Per + "' and Tur_Ser = '" + Tur_Ser + "' and PPFechaCupos = '" + PPFechaCupos + "' and PPEstado = 'Activo' ";
        SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
        cmd2.CommandType = CommandType.Text;
        SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;
        conSAP00i.Open();
        DataSet objdataset = new DataSet();
        adapter2.Fill(objdataset);
        conSAP00i.Close();
        DataTable dtDato = objdataset.Tables[0];

        if (dtDato.Rows.Count > 0)
        {
            gDetHtml += "-|- " + PPFechaCupos + " - Existe <br />";
        }
        else
        {
            try
            {
                qSql = "INSERT INTO TabLibres.dbo.ProgPer (idProgServ, COD_SER, Cod_Per, PIS_SER, Tur_Ser, PPAnio, PPMes, PPFechaCupos, PPCupos, PPAdicional, PPAdiLimite, Hr_Ser, PPObservacion, PPEstado, PPTipTrabajo, PPCantTurno) VALUES (@idProgServ, @COD_SER, @Cod_Per, @PIS_SER, @Tur_Ser, @PPAnio, @PPMes, @PPFechaCupos, @PPCupos, @PPAdicional, @PPAdiLimite, @Hr_Ser, @PPObservacion, @PPEstado, @PPTipTrabajo, @PPCantTurno) SELECT @@Identity;";

                SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd.Parameters.AddWithValue("@idProgServ", idProgServ);
                cmd.Parameters.AddWithValue("@COD_SER", COD_SER);
                cmd.Parameters.AddWithValue("@Cod_Per", Cod_Per);
                cmd.Parameters.AddWithValue("@PIS_SER", PIS_SER);
                cmd.Parameters.AddWithValue("@Tur_Ser", Tur_Ser);
                cmd.Parameters.AddWithValue("@PPAnio", PPAnio);
                cmd.Parameters.AddWithValue("@PPMes", PPMes);
                cmd.Parameters.AddWithValue("@PPFechaCupos", PPFechaCupos);
                cmd.Parameters.AddWithValue("@PPCupos", PPCupos);
                cmd.Parameters.AddWithValue("@PPAdicional", "0");
                cmd.Parameters.AddWithValue("@PPAdiLimite", PPAdiLimite);
                cmd.Parameters.AddWithValue("@Hr_Ser", Hr_Ser);
                cmd.Parameters.AddWithValue("@PPObservacion", PPObservacion);
                cmd.Parameters.AddWithValue("@PPEstado", "Activo");
                cmd.Parameters.AddWithValue("@PPTipTrabajo", PPTipTrabajo);
                cmd.Parameters.AddWithValue("@PPCantTurno", PPCantTurno);

                adapter.SelectCommand = cmd;

                conSAP00i.Open();
                int idReg = Convert.ToInt32(cmd.ExecuteScalar());
                conSAP00i.Close();

                gDetHtml += idReg.ToString() + " - " + PPFechaCupos + " - Programacion de Personal Guardado <br />";
            }
            catch (Exception ex)
            {
                gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
            }
        }

        return gDetHtml;
    }

    [WebMethod]
    public static string SetBtnGuardarVarios(string idProgServ, string COD_SER, string Cod_Per, string PIS_SER, string Tur_Ser, string PPAnio, string PPMes, string PPFechaCupos, string PPCupos, string PPAdiLimite, string Hr_Ser, string PPObservacion, string PPEstado, string PPTipTrabajo, string vCantDias, string vFechaHasta, string vLun, string vMar, string vMie, string vJue, string vVie, string vSab)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "ok4 - " + DateTime.Now.ToLongTimeString();
        string qSql = "";

        qSql = "SELECT * from TabLibres.dbo.ProgPer where Cod_Per = '" + Cod_Per + "' and Tur_Ser = '" + Tur_Ser + "' and (PPFechaCupos between '" + PPFechaCupos + "' and '" + vFechaHasta + "')";
        SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
        cmd2.CommandType = CommandType.Text;

        SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

        conSAP00i.Open();
        DataSet objdataset = new DataSet();
        adapter2.Fill(objdataset);
        conSAP00i.Close();

        DataTable dtDato = objdataset.Tables[0];

        string vGuardar = "Si";
        for (int i = 0; i <= Convert.ToInt32(vCantDias) - 1; i++)
        {
            vGuardar = "Si";
            DateTime vfecFor = Convert.ToDateTime(PPFechaCupos);
            vfecFor = vfecFor.AddDays(i);
            // 0=Domingo, 6=Sabado
            if ((int)vfecFor.DayOfWeek == 1 && vLun == "") { vGuardar = "No"; gDetHtml += "<br /> Lunes : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 2 && vMar == "") { vGuardar = "No"; gDetHtml += "<br /> Martes : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 3 && vMie == "") { vGuardar = "No"; gDetHtml += "<br /> Miercoles : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 4 && vJue == "") { vGuardar = "No"; gDetHtml += "<br /> Jueves : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 5 && vVie == "") { vGuardar = "No"; gDetHtml += "<br /> Viernes : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 6 && vSab == "") { vGuardar = "No"; gDetHtml += "<br /> Sabado : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 0) { vGuardar = "No"; gDetHtml += "<br /> Domingo : " + vfecFor + " - "; }

            //int OExist = Convert.ToInt32(dtDato.Compute("Count(FechaTrab)", "FechaTrab like '%" + vfecFor + "%'").ToString());
            int OExist = Convert.ToInt32(dtDato.Compute("Count(PPFechaCupos)", "PPFechaCupos = '" + vfecFor + "'"));
            if (OExist >= 1)
            {
                vGuardar = "No"; gDetHtml += "<br /> Ya esta Registrado : " + vfecFor + " - ";
            }

            if (vGuardar == "Si")
            {
                try
                {
                    qSql = "INSERT INTO TabLibres.dbo.ProgPer (idProgServ, COD_SER, Cod_Per, PIS_SER, Tur_Ser, PPAnio, PPMes, PPFechaCupos, PPCupos, PPAdicional, PPAdiLimite, Hr_Ser, PPObservacion, PPEstado, PPTipTrabajo) " +
                        "VALUES (@idProgServ, @COD_SER, @Cod_Per, @PIS_SER, @Tur_Ser, @PPAnio, @PPMes, @PPFechaCupos, @PPCupos, @PPAdicional, @PPAdiLimite, @Hr_Ser, @PPObservacion, @PPEstado, @PPTipTrabajo) SELECT @@Identity;";

                    SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("@idProgServ", idProgServ);
                    cmd.Parameters.AddWithValue("@COD_SER", COD_SER);
                    cmd.Parameters.AddWithValue("@Cod_Per", Cod_Per);
                    cmd.Parameters.AddWithValue("@PIS_SER", PIS_SER);
                    cmd.Parameters.AddWithValue("@Tur_Ser", Tur_Ser);
                    cmd.Parameters.AddWithValue("@PPAnio", PPAnio);
                    cmd.Parameters.AddWithValue("@PPMes", PPMes);
                    cmd.Parameters.AddWithValue("@PPFechaCupos", vfecFor);
                    cmd.Parameters.AddWithValue("@PPCupos", PPCupos);
                    cmd.Parameters.AddWithValue("@PPAdicional", "0");
                    cmd.Parameters.AddWithValue("@PPAdiLimite", PPAdiLimite);
                    cmd.Parameters.AddWithValue("@Hr_Ser", Hr_Ser);
                    cmd.Parameters.AddWithValue("@PPObservacion", PPObservacion);
                    cmd.Parameters.AddWithValue("@PPEstado", "Activo");
                    cmd.Parameters.AddWithValue("@PPTipTrabajo", PPTipTrabajo);

                    adapter.SelectCommand = cmd;

                    conSAP00i.Open();
                    int idReg = Convert.ToInt32(cmd.ExecuteScalar());
                    conSAP00i.Close();

                    gDetHtml += "<br /> Registro Correcto: " + vfecFor + " - " + idReg.ToString() + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    gDetHtml += "ERROR Registro: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
                }
            }

        }
        return gDetHtml;
    }

    [WebMethod]
    public static string GetBtnBuscarServ(string PSEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "select * from " +
                "	(" +
                "	select '0' as idProgServ, '00' as CSer, ' Licencia' as Servicio, '5' as Piso, 'M' as Turno, " +
                "	'0' as Cupos, '0' as Limite, '07:30' as Horario, '' as Observacion, '1' as PSCantTurno " +
                "	union " +
                "	SELECT PSer.idProgServ, PSer.COD_SER CSer, Ser.DSC_SER Servicio, " +
                "		PSer.PIS_SER Piso, Tur_Ser Turno, PSCupos Cupos, PSAdiLimite Limite, Hr_Ser Horario, PSObservacion Observacion, PSer.PSCantTurno " +
                "	from TabLibres.dbo.ProgServ PSer " +
                "	left join NUEVA.dbo.SERVICIO Ser on " +
                "	PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "	) TabServ " +
                "order by Servicio, Turno ";

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
                gDetH += "<th class='text-center'>CodSer</th>";
                gDetH += "<th class='text-center'>Servicio</th>";
                gDetH += "<th class='text-center'>Piso</th>";
                gDetH += "<th class='text-center'>Turno</th>";
                gDetH += "<th class='text-center'>Cupos</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vColorFila = "#CEF6F5";
                    if (dbRow["Turno"].ToString()=="T") { vColorFila = "#F5ECCE"; }
                    gDetH += "<tr style='background-color:" + vColorFila + "'>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelServicio('" + dbRow["idProgServ"].ToString() + "', '" + dbRow["CSer"].ToString() + "', '" + dbRow["Servicio"].ToString() + "', '" + dbRow["Piso"].ToString() + "', '" + dbRow["Turno"].ToString() + "', '" + dbRow["Cupos"].ToString() + "', '" + dbRow["Limite"].ToString() + "', '" + dbRow["Horario"].ToString() + "', '" + dbRow["PSCantTurno"].ToString() + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["CSer"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Servicio"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Piso"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Turno"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Cupos"].ToString() + "</td>";

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
    public static string GetBtnBuscarPer(string PSEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "select per.*, pro.DES_PRO " +
                "from NUEVA.dbo.PERSONAL per " +
                "left join NUEVA.dbo.PROFESION pro on per.COD_PRO = pro.COD_PRO " +
                "where (pro.COD_PRO = '01' ) and per.EST_PER = '1' " +
                "order by APE_PER ";

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
                gDetH += "<table id='tblbscrJS' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class='text-center'>N° </th>";
                gDetH += "<th class='text-center'>Sel </th>";
                gDetH += "<th class='text-center'>CodPer</th>";
                gDetH += "<th class='text-center'>Personal</th>";
                gDetH += "<th class='text-center'>Profesion</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelPersonal('" + dbRow["COD_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["COD_PER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["APE_PER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["DES_PRO"].ToString() + "</td>";

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

    protected void btnListar_Click(object sender, EventArgs e)
    {
        try
        {
            string qSql = "SELECT PPER.PPFechaCupos, PPER.Cod_Per, PER.APE_PER, PPER.COD_SER, SER.DSC_SER, PPER.Tur_Ser, PPER.PIS_SER, PPER.PPAdicional, PPER.Hr_Ser " +
                "from TabLibres.dbo.ProgPer PPER " +
                "left join NUEVA.dbo.PERSONAL PER on PPER.Cod_Per COLLATE Modern_Spanish_CI_AS = PER.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "left join NUEVA.dbo.SERVICIO SER on PPER.COD_SER COLLATE Modern_Spanish_CI_AS = SER.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where PPEstado = 'Activo' ";

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
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }



    public void CargaCalendario(int vAnio, int vMes, string vMesNom)
    {
        string lhtml = "";
        int Days = DateTime.DaysInMonth(vAnio, vMes);

        try
        {

            lhtml += "<div class='box-body table-responsive no-padding' style='color:#000000; font-size:20px;'>";
            lhtml += "<input type='hidden' id='txtCantDiasMes' value='" + Days + "'>";
            lhtml += "<table border='2' class='table table-hover'> ";
            lhtml += "	<thead class='fc-head'> ";
            lhtml += "		<tr> ";
            lhtml += "			<th>Dom</th> ";
            lhtml += "		<th onclick=\"fchkDiaSemCab('1', 'chkILun')\">Lun</th>" +
                "<th onclick=\"fchkDiaSemCab('2', 'chkIMar')\">Mar</th>" +
                "<th onclick=\"fchkDiaSemCab('3', 'chkIMie')\">Mie</th> ";
            lhtml += "		<th onclick=\"fchkDiaSemCab('4', 'chkIJue')\">Jue</th>" +
                "<th onclick=\"fchkDiaSemCab('5', 'chkIVie')\">Vie</th>" +
                "<th onclick=\"fchkDiaSemCab('6', 'chkISab')\">Sab</th> ";
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
                    string vDia = "", vDiaHTML = "", vDiaAdiCit = "";
                    int diaSemNro = (int)vFechaFor.DayOfWeek;
                    if (diaSemNro == j)
                    {
                        vDia = i.ToString();

                        vDiaHTML = "<table style='font-size:22px;' class='table-bordered' onclick=\"fClickChk('" + vDia + "', '" + diaSemNro + "')\" > " +
                            "<tr><td colspan='4'>" +
                            "   <div align='center'>" + vDia + "</div>" +
                            "</td>" +
                            "   <td>" +
                            "       <div class='btn btn-block hide' align='center' style = 'background-color: #D29B9F; font-weight:bold;' data-toggle='modal' data-target='#mCitarAdic' onclick=\"fmCitarAdic('" + vFechaFor.ToShortDateString() + "', '" + vDiaAdiCit + "')\"><i class='fa fa-fw fa-calendar-plus-o'></i></div>" +
                            "       <input type='checkbox' id='chk" + vDia + "' onclick=\"fClickChk('" + vDia + "', '" + diaSemNro + "')\"> " +
                            "   </td> " +
                            "</tr> " +
                            "<tr> " +
                            "   <td>" +
                            "       <input type='hidden' id='txtDiaSem" + diaSemNro + "D" + vDia + "' value='" + vDia + "'>" +
                        "       <div class='btn-block btn-success btn-xs'></div>" +
                            "   </td> " +
                            "   <td>" +
                            "       <input type='hidden' id='txtFecCalendar" + vDia + "' value='" + vFechaFor.ToShortDateString() + "'>" +
                            "       <div class='btn-block btn-success btn-xs'></div>" +
                            "   </td> " +
                            "   <td>" +
                            "       <div class='btn-block btn-success btn-xs'></div>" +
                            "   </td> " +
                            "   <td>" +
                            "       <div class='btn-block btn-success btn-xs'></div>" +
                            "   </td> " +
                            "   <td>" +
                            "       <div class='btn-block bg-purple btn-xs'></div>" +
                            "   </td> " +
                            "</tr> " +
                            "</table>";

                        vFechaFor = vFechaTrab.AddDays(i);
                        i++;
                    }
                    if (i > Days + 1)
                    {
                        vDia = ""; vDiaHTML = "";
                    }
                    lhtml += "	<td> ";
                    lhtml += vDiaHTML;
                    lhtml += "	</td> ";
                }
                lhtml += "</tr> ";
                i--;
            }

            lhtml += "	</tbody> ";
            lhtml += "</table> ";
            lhtml += "</div>";

            LitCalendarServer.Text = lhtml;
        }
        catch (Exception ex)
        {
            LitCalendarServer.Text += ex.Message.ToString();
        }

        //LitPeriodo.Text = vMesNom + " de " + vAnio;

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        int vAnio = Convert.ToInt32(DDLAnio.SelectedValue);
        int vMes = Convert.ToInt32(DDLMes.SelectedValue);
        CargaCalendario(vAnio, vMes, DDLMes.SelectedItem.Text);
    }
}
