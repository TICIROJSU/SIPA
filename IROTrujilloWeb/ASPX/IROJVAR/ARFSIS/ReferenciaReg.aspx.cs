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

public partial class ASPX_IROJVAR_ARFSIS_ReferenciaReg : System.Web.UI.Page
{
    string html = "";
    string htmlAtenciones = "";
    string htmlDetAtenciones = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //html = "";
            //htmlAtenciones = "";
            //htmlDetAtenciones = "";
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth;

        }

    }

    [WebMethod]
    public static string SetRegRefG(string idNro, string Ventanilla, string citaSipa, string Observacion, string fechaobsv, string NrObs, string retirohc, string crearhc, string disaorigen, string redorigen, string mredorigen, string Estorigen)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        string qSql = "";
        try
        {
            //con.Open();
            qSql = "UPDATE Referencias SET Ventanilla = @Ventanilla, citaSipa = @citaSipa, Observacion = @Observacion, fechaobsv = @fechaobsv, NrObs = @NrObs, retirohc = @retirohc, crearhc = @crearhc, disaorigen = @disaorigen, redorigen = @redorigen, mredorigen = @mredorigen, Estorigen = @Estorigen WHERE idNro = @idNro; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@idNro", idNro);
            cmd.Parameters.AddWithValue("@Ventanilla", Ventanilla);
            cmd.Parameters.AddWithValue("@citaSipa", citaSipa);
            cmd.Parameters.AddWithValue("@Observacion", Observacion);
            cmd.Parameters.AddWithValue("@fechaobsv", fechaobsv);
            cmd.Parameters.AddWithValue("@NrObs", NrObs);
            cmd.Parameters.AddWithValue("@retirohc", retirohc);
            cmd.Parameters.AddWithValue("@crearhc", crearhc);
            cmd.Parameters.AddWithValue("@disaorigen", disaorigen);
            cmd.Parameters.AddWithValue("@redorigen", redorigen);
            cmd.Parameters.AddWithValue("@mredorigen", mredorigen);
            cmd.Parameters.AddWithValue("@Estorigen", Estorigen);

            conSAP00i.Open();
            cmd.ExecuteNonQuery();
            conSAP00i.Close();

            gHTML += idNro + " - Registro/Edicion Correcto.";
        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetRegRef(string vIdNro)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        string qSql = "";
        try
        {
            //con.Open();
            qSql = "select * from Referencias where idNro = @idNro;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@idNro", vIdNro);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            //CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                DataRow dbRow = dtDato.Rows[0];
                gHTML += Environment.NewLine;

                gHTML += "<div class='box' style='color:#000000; font-size:20px;'>" +
                    "<div class='box-body table-responsive no-padding' >";
                gHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gHTML += "<tbody>";
                gHTML += "	<tr>";
                gHTML += "		<td class='text-left'>" + dbRow["idNro"].ToString() + "</td>" +
                    "<td class='text-left'>" + dbRow["nrodoc"].ToString() + "</td>";
                gHTML += "		<td class='text-left'>" + dbRow["paciente"].ToString() + "</td>" +
                    "<td class='text-left'>" + dbRow["financiador"].ToString() + "</td>";
                gHTML += "		<td class='text-left'>" + dbRow["fechaenvio"].ToString() + "</td>" +
                    "</tr><tr>" +
                    "<td class='text-left'>" + dbRow["PersonalRefiere"].ToString() + "</td>";
                gHTML += "		<td class='text-left'>" + dbRow["ServicioDestino"].ToString() + "</td>" +
                    "<td class='text-left'>" + dbRow["Especialidad"].ToString() + "</td>";
                gHTML += "		<td class='text-left'>" + dbRow["Motivo"].ToString() + "</td>" +
                    "<td class='text-left'>" + dbRow["EstadoRef"].ToString() + "</td>";
                gHTML += "	</tr>";
                gHTML += "</tbody>";
                gHTML += "</table>";
                gHTML += "</div> </div>";
                /////////////////////////////////////
                gHTML += "<div class='row' style='display:block;' id='ConsultaContenido'>";
                gHTML += "	<div class='column'>";
                gHTML += "		<div class='col-md-11' style='text-align: center;padding:  0px 30px 0px 30px;'>";
                gHTML += "			<div class='box-body'>";
                gHTML += "			<div class='form-group box box-primary'>";
                gHTML += "				<div class='form-group box box-primary'>";
                gHTML += "					<br />";
                gHTML += "					<div class='form-group has-success'>";
                gHTML += "						<div class='col-sm-10'>";
                gHTML += "							<input id='txtVentanilla' class='form-control input-lg' value='" + dbRow["Ventanilla"].ToString() + "' />";
                gHTML += "							<code style='color:black; background-color:transparent'>Ventanilla</code>";
                gHTML += "						</div>";
                gHTML += "						<div style='display:none'>";
                gHTML += "							<input id='txtid' value='" + dbRow["idNro"].ToString() + "' />";
                gHTML += "						</div>";
                gHTML += "						<div class='col-sm-2'>";
                gHTML += "							<input id='txtcitaSipa' class='form-control input-lg' value='" + dbRow["citaSipa"].ToString() + "' />";
                gHTML += "							<code style='color:black; background-color:transparent'>Cita Sipa</code>";
                gHTML += "						</div>";
                gHTML += "						<div class='col-sm-9' style='padding-bottom: 20px; '>";
                gHTML += "							<input id='txtObservacion' class='form-control input-lg' value='" + dbRow["Observacion"].ToString() + "' />";
                gHTML += "							<code style='color:black; background-color:transparent; '>Observacion</code> ";
                gHTML += "						</div>";
                gHTML += "						<div class='col-sm-3'>";
                gHTML += "							<input id='txtfechaobsv' class='form-control input-lg' value='" + dbRow["fechaobsv"].ToString() + "' />";
                gHTML += "							<code style='color:black; background-color:transparent'>Fecha</code>";
                gHTML += "						</div>";
                gHTML += "<div class='col-sm-4' style='padding-bottom: 20px; '>" +
                    " <input id='txtNrObs' class='form-control input-lg' value='" + dbRow["NrObs"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>Nro. Observ.</code></div>";
                gHTML += "<div class='col-sm-4' style='padding-bottom: 20px; '>" +
                    " <input id='txtretirohc' class='form-control input-lg' value='" + dbRow["retirohc"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>Ret. HC</code></div>";
                gHTML += "<div class='col-sm-4' style='padding-bottom: 20px; '>" +
                    " <input id='txtcrearhc' class='form-control input-lg' value='" + dbRow["crearhc"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>Crea H.C.</code></div>";

                gHTML += "<div class='col-sm-4' style='padding-bottom: 10px; '>" +
                    " <input id='txtdisaorigen' class='form-control input-lg' value='" + dbRow["disaorigen"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>DISA Origen</code></div>";
                gHTML += "<div class='col-sm-4' style='padding-bottom: 10px; '>" +
                    " <input id='txtredorigen' class='form-control input-lg' value='" + dbRow["redorigen"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>Red Origen</code></div>";
                gHTML += "<div class='col-sm-4' style='padding-bottom: 10px; '>" +
                    " <input id='txtmredorigen' class='form-control input-lg' value='" + dbRow["mredorigen"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>MRed Origen</code></div>";

                gHTML += "<div class='col-sm-12' style='padding-bottom: 10px; '>" +
                    " <input id='txtEstorigen' class='form-control input-lg' value='" + dbRow["Estorigen"].ToString() + "' />" +
                    " <code style='color:black; background-color:transparent'>Establecimiento de Salud Origen</code></div>";

                gHTML += "						<br />";
                gHTML += "					</div>";
                gHTML += "					<p><br /></p>";
                gHTML += "<div class='btn btn-primary' id='bntGuardar' onclick='fRegRefG()' data-dismiss='modal'> <i class='fa fa-save'></i>  Guardar Contenido</div>";
                gHTML += "				</div>";
                gHTML += "			</div>";
                gHTML += "			</div>";
                gHTML += "		</div>";
                gHTML += "	</div>";
                gHTML += "</div>";
            }
        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
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
        string vCodProf = txtIdProf.Value;
        try
        {
            //con.Open();
            string qSql = "select * from Referencias where year(fechaenvio) = '" + vAnio + "' and month(fechaenvio) = '" + vMes + "'";

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

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {

            html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''>Accion</th>";
            html += "<th class=''>ID</th>";
            html += "<th class=''>tDoc</th>";
            html += "<th class=''>Nro. Doc.</th>";
            html += "<th class=''>Paciente</th>";
            html += "<th class=''>Financ</th>";
            html += "<th class=''>Fec. Envio</th>";
            html += "<th class=''>Personal Refiere</th>";
            html += "<th class=''>Cod. Serv</th>";
            html += "<th class=''>Serv. Destino</th>";
            html += "<th class=''>Especialidad</th>";
            html += "<th class=''>Motivo</th>";
            html += "<th class=''>Estado Ref.</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                html += "<tr>";
                html += "<td class='text-left' >"
                    + "<button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalRegRef' onclick=\"fRegRef('" + dbRow["idNro"].ToString() + "')\"><i class='fa fa-fw fa-edit'></i></button>"
                    + "</td>";
                html += "<td class='text-left' >" + dbRow["idNro"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["tdoc"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["nrodoc"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["paciente"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["financiador"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["fechaenvio"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["PersonalRefiere"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["codigoserv"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["ServicioDestino"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["Especialidad"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["Motivo"].ToString() + "</td>";
                html += "<td class='text-left' >" + dbRow["EstadoRef"].ToString() + "</td>";

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
        LitAtenciones.Text = htmlAtenciones;
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
