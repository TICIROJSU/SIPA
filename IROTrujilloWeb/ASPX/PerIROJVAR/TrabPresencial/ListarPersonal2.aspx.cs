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

public partial class ASPX_PerIROJVAR_TrabPresencial_ListarPersonal2 : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        txtUnidad.Text = Session["PerUOAbr"].ToString();
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;

            //DDLMes.SelectedIndex = vmonth;
            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            txtDesde.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            txtHasta.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
        }

    }

    [WebMethod]
    public static string GetPerDet(string vDNI, string vAnio, string vMes)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select idTipoTrabajo ID, DNIPER DNI, FechaTrab Fecha, TipoTrab Tipo from NUEVA.dbo.JVARTipoTrab where DNIPER = '" + vDNI + "' and MONTH(FechaTrab) = " + vMes + " and YEAR(FechaTrab) = " + vAnio + " order by FechaTrab ";
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
                gHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '><tr>";
                gHTML += "<th class='bg-light-blue '>ID </th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>Tipo</th>";
                gHTML += "<th class=''><button type='button' class='btn-danger'><i class='fa fa-fw fa-close'></i></button></th>";
                gHTML += "<th class='bg-light-blue '>ID </th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>Tipo</th>";
                gHTML += "<th class=''><button type='button' class='btn-danger'><i class='fa fa-fw fa-close'></i></button></th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0, nrocol = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string dSem = "";
                    DateTime vfecFor = Convert.ToDateTime(dbRow["Fecha"].ToString().Substring(0, 10));
                    switch ((int)vfecFor.DayOfWeek)
                    {
                        case 0:
                            dSem = "Dom";
                            break;
                        case 1:
                            dSem = "Lun";
                            break;
                        case 2:
                            dSem = "Mar";
                            break;
                        case 3:
                            dSem = "Mie";
                            break;
                        case 4:
                            dSem = "Jue";
                            break;
                        case 5:
                            dSem = "Vie";
                            break;
                        case 6:
                            dSem = "Sab";
                            break;
                        default:
                            break;
                    }

                    if (nrocol == 0)
                    {
                        nrocol = 1;
                        gHTML += "<tr>";
                        gHTML += "<td class='stLeft bg-light-blue ' >" + dbRow["ID"].ToString() + "</td>";
                        gHTML += "<td class='stLeft' >" + dbRow["DNI"].ToString() + "</td>";
                        gHTML += "<td class='stLeft' >" + dSem + ". " + dbRow["Fecha"].ToString().Substring(0, 10) + "</td>";
                        gHTML += "<td class='stLeft' >" + dbRow["Tipo"].ToString() + "</td>";
                        gHTML += "<td class='text-left' >"
                            + "<button type='button' class='btn btn-danger' data-dismiss='modal' onclick=\"fDeletePerDet('" + vDNI + "', '" + dbRow["ID"].ToString() + "', '" + dbRow["Fecha"].ToString() + "')\"><i class='fa fa-fw fa-close'></i></button>"
                            + "</td>";
                    }
                    else if (nrocol == 1)
                    {
                        nrocol = 0;
                        gHTML += "<td class='stLeft bg-light-blue ' >" + dbRow["ID"].ToString() + "</td>";
                        gHTML += "<td class='stLeft' >" + dbRow["DNI"].ToString() + "</td>";
                        gHTML += "<td class='stLeft' >" + dSem + ". " + dbRow["Fecha"].ToString().Substring(0, 10) + "</td>";
                        gHTML += "<td class='stLeft' >" + dbRow["Tipo"].ToString() + "</td>";
                        gHTML += "<td class='text-left' >"
                            + "<button type='button' class='btn btn-danger' data-dismiss='modal' onclick=\"fDeletePerDet('" + vDNI + "', '" + dbRow["ID"].ToString() + "', '" + dbRow["Fecha"].ToString().Substring(0, 10) + "')\"><i class='fa fa-fw fa-close'></i></button>"
                            + "</td>";
                        gHTML += "</tr>";
                        gHTML += Environment.NewLine;
                    }

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

    [WebMethod]
    public static string GetDeletePetDet(string vDNI, string vIdT, string vFechaT)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "DELETE FROM NUEVA.dbo.JVARTipoTrab WHERE idTipoTrabajo = @idT;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@idT", vIdT);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Eliminacion Correcta. " + vFechaT;
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string SetTipoTrabajoInsert(string vIdPer, string vDNIPer, string vFechaTrab, string vTipoTrab, string vCantTurnos)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO NUEVA.dbo.JVARTipoTrab (IDPER, DNIPER, FechaTrab, TipoTrab, Observacion, CantTurnos) VALUES (@IDPER, @DNIPER, @FechaTrab, @TipoTrab, @Observacion, @CantTurnos) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@IDPER", vIdPer);
            cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
            cmd.Parameters.AddWithValue("@FechaTrab", vFechaTrab);
            cmd.Parameters.AddWithValue("@TipoTrab", vTipoTrab);
            cmd.Parameters.AddWithValue("@Observacion", "");
            cmd.Parameters.AddWithValue("@CantTurnos", vCantTurnos);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml += "Registro Correcto: " + idReg.ToString();
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR Registro: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string SetTipoTrabajoInsertVDias(string vIdPer, string vDNIPer, string vFechaI, string vFechaF, string vTipoTrab, string vCantTurnos, string vCantDias, string vSab, string vDom)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";

        qSql = "select * from NUEVA.dbo.JVARTipoTrab where DNIPER = '" + vDNIPer + "' and (FechaTrab between '" + vFechaI + "' and '" + vFechaF + "')";
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
            DateTime vfecFor = Convert.ToDateTime(vFechaI);
            vfecFor = vfecFor.AddDays(i);
            // 0=Domingo, 6=Sabado
            if ((int)vfecFor.DayOfWeek == 6 && vSab == "") { vGuardar = "No"; gDetHtml += "<br /> Sabado : " + vfecFor + " - "; }
            if ((int)vfecFor.DayOfWeek == 0 && vDom == "") { vGuardar = "No"; gDetHtml += "<br /> Domingo : " + vfecFor + " - "; }

            //int OExist = Convert.ToInt32(dtDato.Compute("Count(FechaTrab)", "FechaTrab like '%" + vfecFor + "%'").ToString());
            int OExist = Convert.ToInt32(dtDato.Compute("Count(FechaTrab)", "FechaTrab = '" + vfecFor + "'"));
            if (OExist >= 1)
            {
                vGuardar = "No"; gDetHtml += "<br /> Ya esta Registrado : " + vfecFor + " - ";
            }

            if (vGuardar == "Si")
            {
                try
                {
                    qSql = "INSERT INTO NUEVA.dbo.JVARTipoTrab (IDPER, DNIPER, FechaTrab, TipoTrab, Observacion, CantTurnos) VALUES (@IDPER, @DNIPER, @FechaTrab, @TipoTrab, @Observacion, @CantTurnos) SELECT @@Identity;";

                    SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("@IDPER", vIdPer);
                    cmd.Parameters.AddWithValue("@DNIPER", vDNIPer);
                    cmd.Parameters.AddWithValue("@FechaTrab", vfecFor);
                    cmd.Parameters.AddWithValue("@TipoTrab", vTipoTrab);
                    cmd.Parameters.AddWithValue("@Observacion", "");
                    cmd.Parameters.AddWithValue("@CantTurnos", vCantTurnos);

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
    public static string GetTipoTrabajoInsertValida(string vIdPer, string vDNIPer, string vFechaTrab)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "Conforme";
        try
        {
            string qSql = "select * from NUEVA.dbo.JVARTipoTrab where DNIPER = '" + vDNIPer + "' and FechaTrab = '" + vFechaTrab + "'";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;
            //cmd2.Parameters.AddWithValue("@Dni", vDNIPer);
            //cmd2.Parameters.AddWithValue("@Fec", vFechaTrab);

            //gHTML = qSql;

            SqlDataAdapter adapter2 = new SqlDataAdapter(); adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            if (objdataset.Tables[0].Rows.Count > 0)
            {
                gHTML = "Discorde";
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
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {
        string vAnio = DDLAnio.SelectedValue;
        string vMes = DDLMes.SelectedValue;
        ClassGlobal.varGlobalTmp = vMes;
        string vEESS = txtEESS.Text;
        try
        {
            //con.Open();
            string qSql = "Select * from (select *, (CASE WHEN SUBJEFE_PER<>'' THEN 'JEFE' ELSE 'P' END) TipoP  " +
                "from RRHH.dbo.PERSONAL " +
                "where UOABR_PER = @UOABR_PER AND TIPOUSER_PER=@TIPOUSER_PER and ESTADO_PER = '1' " +
                "union select *, 'SubJefe' " +
                "from RRHH.dbo.PERSONAL where SUBJEFE_PER = @UOABR_PER) Tab1 " +
                "order by TipoP ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@UOABR_PER", txtUnidad.Text);
            cmd.Parameters.AddWithValue("@TIPOUSER_PER", "PERSONAL");

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

    public void CargaTabla(DataTable dtDato)
    {
        string html = "";
        if (dtDato.Rows.Count > 0)
        {
            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''>Accion</th>";
            html += "<th class=''>ID</th>";
            html += "<th class=''>Personal</th>";
            html += "<th class=''>Ver</th>";
            html += "<th class=''>DNI</th>";
            html += "<th class=''>Cargo</th>";
            html += "<th class=''>Regimen</th>";
            html += "<th class=''>TipoT</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vhDisable = "";
                if (dbRow["TipoP"].ToString() == "JEFE") { vhDisable = "disabled='true'"; }

                html += "<tr >";
                html += "<td class='text-left' >"
                    + "<button " + vhDisable + " type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalPerDet' onclick=\"fPerDet('" + dbRow["DNI_PER"].ToString() + "', '" + dbRow["ID_PER"].ToString() + "', '" + dbRow["NOM_PERSONAL"].ToString() + "')\"><i class='fa fa-fw fa-edit'></i></button>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["ID_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["NOM_PERSONAL"].ToString() + "</td>";
                html += "<td class='text-left' >"
                    + "<a class='btn bg-navy' href='VerRegRemoto.aspx?DNI=" + dbRow["DNI_PER"].ToString() + "' ><i class='fa fa-fw fa-eye'></i></a>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["DNI_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["CARGO_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["COND_PER"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["TipoP"].ToString() + "</td>";
                html += "</tr>" + Environment.NewLine;

                //CargaDTAtenciones(DDLAnio.SelectedValue, DDLMes.SelectedValue, dbRow["DNI"].ToString(), dbRow["Usuario"].ToString(), codtmp_modal);
            }

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        LitTABL1.Text = html;
        //LitAtenciones.Text = htmlAtenciones;
        //LitDetAtenciones.Text = htmlDetAtenciones;
    }

    protected void ExportarExcel2_Click(object sender, EventArgs e)
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