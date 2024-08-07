using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

public partial class SISMEDG_JVAR_Almacen_ConsultaGuia : System.Web.UI.Page
{
	SqlConnection conSISMED = new SqlConnection(ClassGlobal.conexion_EXTERNO);
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
            //CargaInicial();
            CargaRed();
        }
		if (Page.IsPostBack)
		{

		}
	}

	[WebMethod]
	public static string GetGR(string vMovNumero)
	{
		SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
		string gDetH = "";
		try
		{
			string qSql = "select movcoditip, MOVNUMERO, medcod, MEDCOD + ' - ' + MEDICAMENT + '-' + ISNULL(presentaci, '') + '-' + ISNULL(CONCENTRAC, '') + '-' + ISNULL(FORMA_FARM, '') Producto, MedRegSan, MedLote, MedFechVto, MOVCANTID, MOVPRECIO, MOVTOTAL from tmovimdet tmd left join catmedicam med on tmd.medcod = med.CODIGO_MED where MOVCODITIP = 'S' and CODDEP = '018A01' and movnumero = '" + vMovNumero + "';";
			SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSISMEDi.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSISMEDi.Close();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tblGR' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<th class=''>Nro </th>";
				gDetH += "<th class=''>Producto</th>";
				gDetH += "<th class=''>Lote</th>";
				gDetH += "<th class=''>FechVto</th>";
				gDetH += "<th class=''>Cantidad</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr>" +
						"<td>" + nroitem + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["Producto"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MedLote"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MedFechVto"].ToString() + "</td>";
					gDetH += "<td style='text-align: left;' >" + dbRow["MOVCANTID"].ToString() + "</td>";
					gDetH += "</tr>";
					gDetH += Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";
			}
		}
		catch (Exception ex)
		{
			gDetH += "-" + "-" + ex.Message.ToString();
		}
		return gDetH;
	}

    [WebMethod]
    public static string GetMRed(string vRed)
    {
        SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
        string ghtml = "";
        try
        {
            string qSql = "select Red, Microrred " +
                "from SISMED.dbo.CATEESS " +
                "where Red like '" + vRed + "%' " +
                "group by Red, Microrred; ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSISMEDi.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSISMEDi.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                ghtml += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                ghtml += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                ghtml += "<th class=''>Nro </th>";
                ghtml += "<th class=''>Micro Red</th>";
                ghtml += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    ghtml += "<tr onclick=\"txtMRedCarga('" + dbRow["Microrred"].ToString() + "')\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    ghtml += "<td style='text-align: left;' >" + dbRow["Microrred"].ToString() + "</td>" +
                        "</tr>";
                    ghtml += Environment.NewLine;
                }

                ghtml += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            ghtml += "-" + "-" + ex.Message.ToString();
        }
        return ghtml;
    }
    [WebMethod]
    public static string GetEESS(string vRed, string vMRed)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_EXTERNO);
        string ghtml = "";
        try
        {
            string qSql = "select Red, Microrred, SUBSTRING(codEESS,4,5) CodES, EESSS from SISMED.dbo.CATEESS " +
                "where Red like '" + vRed + "%' and Microrred like '" + vMRed + "%'; ";
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
                ghtml += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                ghtml += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                ghtml += "<th class=''>Nro </th>";
                ghtml += "<th class=''>CodEESS</th>";
                ghtml += "<th class=''>Establecimiento</th>";
                ghtml += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    ghtml += "<tr onclick=\"txtEESSCarga('" + dbRow["EESSS"].ToString() + "', '" + dbRow["CodES"].ToString() + "')\" data-dismiss='modal'>" +
                        "<td>" + nroitem + "</td>";
                    ghtml += "<td>" + dbRow["CodES"].ToString() + "</td>";
                    ghtml += "<td style='text-align: left;' >" + dbRow["EESSS"].ToString() + "</td>" +
                        "</tr>";
                    ghtml += Environment.NewLine;
                }

                ghtml += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            ghtml += "-" + "-" + ex.Message.ToString();
        }
        return ghtml;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
	{
		string vDato = txtDato.Text;
        string vcodES = txtEESSCodHide.Value;
        string gDetH = "";
        if (vcodES.Length < 4)
        {
            LitTABL1.Text = vcodES + " - Sin Codigo de EESS";
            return;
        }
		try
		{
            //con.Open();
            string qSql = "select MOVNUMERO, ALMCODIDST, es.EESSS, MOVNUMEDCO, MOVFECHREG, MOVTOT, MOVFFINAN, MOVREFE " +
                "from tmovim left join CATEESS es on ALMCODIDST = RIGHT(es.codEESS, 5) " +
                "where MOVCODITIP = 'S' and CODDEP = '018A01' " +
                "and ALMCODIDST like '" + vcodES + "%'" +
                //"and MOVNUMEDCO + movnumero like '%" + vDato + "%'" +
                "order by MOVFECHREG desc;";

			SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

			conSISMED.Open();
			DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
			conSISMED.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();

			DataTable dtDatoDetAt = objdataset.Tables[0];

			//ghtml = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
			if (dtDatoDetAt.Rows.Count > 0)
			{
				gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
				gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
				gDetH += "<caption>Documentos Filtrados</caption>";
				gDetH += "<tr>";
                gDetH += "<th class=''>N° </th>";
                gDetH += "<th class=''>Zip</th>";
                gDetH += "<th class=''>Ver</th>";
                gDetH += "<th class=''>Establecimiento</th>";
				gDetH += "<th class=''>Documento</th>";
				gDetH += "<th class=''>FechaReg</th>";
				gDetH += "<th class=''>Monto</th>";
				gDetH += "<th class=''>FF</th>";
				gDetH += "<th class=''>Referencia</th>";
				gDetH += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					gDetH += "<tr>";
                    //gDetH += "<tr data-toggle=\"modal\" data-target=\"#modalAtenCli\" onclick=\"DetAtenCli('" + vRed + "-" + vMRed + "-" + vcodEESS + "')\">";
                    gDetH += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    //gDetH += "<td class='' style='text-align: left;'><button type='button' class='btn bg-navy' onclick=\"fzipGuiaRem('" + dbRow["MOVNUMERO"].ToString() + "', '" + dbRow["ALMCODIDST"].ToString() + "', '" + dbRow["MOVNUMEDCO"].ToString() + "', )\"><i class='fa fa-fw fa-download'></i></button></td>";
                    gDetH += "<td class='' style='text-align: left;'><a class='btn bg-navy' href='ConsultaGuiaZip.aspx?movnum=" + dbRow["MOVNUMERO"].ToString() + "&codes=" + dbRow["ALMCODIDST"].ToString() + "&numdoc=" + dbRow["MOVNUMEDCO"].ToString() + "' target='_blank' ><i class='fa fa-fw fa-download'></i></a></td>";
                    gDetH += "<td class='' style='text-align: left;'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalGR' onclick=\"fShowGR('" + dbRow["MOVNUMERO"].ToString() + "')\"><i class='fa fa-fw fa-eye'></i></button></td>";
                    gDetH += "<td class='' style='text-align: left;'>" + dbRow["ALMCODIDST"].ToString() + " - " + dbRow["EESSS"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVNUMEDCO"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVFECHREG"].ToString().Substring(0, 10) + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVTOT"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVFFINAN"].ToString() + "</td>";
					gDetH += "<td class='' style='text-align: left;'>" + dbRow["MOVREFE"].ToString() + "</td>";

					gDetH += "</tr>" + Environment.NewLine;
				}

				gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

			}
			//ghtml - FINAL
			LitTABL1.Text = gDetH;

		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
		}
	}

    //public static string GetzipGuiaRem(string vMovNumero, string vCodEESS, string vNumDco)

    [WebMethod]
    public static string GetzipGuiaRem(string vMovNumero, string vCodEESS, string vNumDco)
    {
        SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
        string gDetH = "";
        try
        {
            string qSql = "SELECT TM.MOVCODITIP, TM.MOVNUMERO, TM.ALMCODIORG, " +
                "TM.ALMCODIDST + 'F01' ALMCODIDST, " +
                "TM.MOVTIPODCI, TM.MOVNUMEDCI, TM.MOVTIPODCO, TM.MOVNUMEDCO, TM.MOVFECHREC, " +
                "TM.MOVFECHEMI, TM.CCTCODIGO, TM.MOVTOT, TM.PRVNUMERUC, TM.PRVDESCRIP, " +
                "TM.MOVREFE, TM.USRCODIGO, '' APPVERSION, TM.MOVFECHREG, TM.MOVFECHULT, " +
                "TM.MOVSITUA, TMD.MOVNUMEITE, TMD.MEDCOD, TMD.MEDCODIFAB, TMD.MEDREGSAN, " +
                "TMD.MEDLOTE, TMD.MEDFECHVTO, TMD.MOVCANTID, TMD.MOVPRECIO, " +
                "TM.ALMORGVIR, TM.ALMCODIDST + 'F0101' ALMDSTVIR, TM.MOVINDPRC, TM.MOVFFINAN " +
                "from tmovim tm " +
                "inner join tmovimdet tmd on tm.coddep = tmd.coddep and tm.movcoditip = tmd.movcoditip and tm.movnumero = tmd.movnumero " +
                "where tm.coddep = '018A01' and tm.movcoditip = 'S' and tm.movnumero = '" + vMovNumero + "';";
            SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSISMEDi.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSISMEDi.Close();

            
            //S + CodEESS + 01 + _ + MOVNUMERO(6 ULTIMOS DIGITOS) + S + MOVNUMEDCO
            string nombrearchivo = "S0531201_024494_S0253646";
            nombrearchivo = "S" + vCodEESS + "01_" + vMovNumero.Substring(3,6) + "_S" + vNumDco;
            //210 024494
            string nomArchivo = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\" + nombrearchivo;
            try
            {
                File.Delete(nomArchivo + ".zip");
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.Directory.Delete(nomArchivo);
            }
            catch (Exception)
            {
            }
            Directory.CreateDirectory(nomArchivo);
            objdataset.WriteXml(nomArchivo + "\\" + nombrearchivo + ".xls"); //Ruta de Guardado del Archivo dentro del Servidor
            ZipFile.CreateFromDirectory(nomArchivo, nomArchivo + ".zip");
            /*
            HttpContext context = HttpContext.Current;

            context.Response.Clear();
            context.Response.ContentType = "application/zip";
            //context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.AppendHeader("Content-Disposition", "inline; filename=" + nombrearchivo + ".zip");
            //Response.AppendHeader("Content-Disposition", "inline; filename=" + nombrearchivo + ".xls");
            //Response.TransmitFile(Server.MapPath("~" + RutaDescarga + nombrearchivo + ".xls"));
            context.Response.TransmitFile(context.Server.MapPath(@"~\ASPX\Descargas\" + nombrearchivo + ".zip"));
            //context.Response.TransmitFile(@"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\" + nombrearchivo + ".zip");
            //context.Response.WriteFile(@"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\S0531201_024494_S0253646bl.zip");
            */
            gDetH += "Descarga Correcta";
        }
        catch (Exception ex)
        {
            gDetH += "-" + "-" + ex.Message.ToString();
        }
        return gDetH;
    }

    protected void Descarga01_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/zip";
        //context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "inline; filename=S0531201_024494_S0253646.zip");
        Response.TransmitFile(Server.MapPath(@"~\ASPX\Descargas\S0531201_024494_S0253646.zip"));

        Response.End();
    }


    protected void ExportarExcelDataSet(string varSql, string nombrearchivo)
    {
        /*
        SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
        string gDetH = "";
        try
        {
            string qSql = "SELECT TM.MOVCODITIP, TM.MOVNUMERO, TM.ALMCODIORG, " +
                "TM.ALMCODIDST + 'F01' ALMCODIDST, " +
                "TM.MOVTIPODCI, TM.MOVNUMEDCI, TM.MOVTIPODCO, TM.MOVNUMEDCO, TM.MOVFECHREC, " +
                "TM.MOVFECHEMI, TM.CCTCODIGO, TM.MOVTOT, TM.PRVNUMERUC, TM.PRVDESCRIP, " +
                "TM.MOVREFE, TM.USRCODIGO, '' APPVERSION, TM.MOVFECHREG, TM.MOVFECHULT, " +
                "TM.MOVSITUA, TMD.MOVNUMEITE, TMD.MEDCOD, TMD.MEDCODIFAB, TMD.MEDREGSAN, " +
                "TMD.MEDLOTE, TMD.MEDFECHVTO, TMD.MOVCANTID, TMD.MOVPRECIO, " +
                "TM.ALMORGVIR, TM.ALMCODIDST + 'F0101' ALMDSTVIR, TM.MOVINDPRC, TM.MOVFFINAN " +
                "from tmovim tm " +
                "inner join tmovimdet tmd on tm.coddep = tmd.coddep and tm.movcoditip = tmd.movcoditip and tm.movnumero = tmd.movnumero " +
                "where tm.coddep = '018A01' and tm.movcoditip = 'S' and tm.movnumero = '" + vMovNumero + "';";
            SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSISMEDi.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSISMEDi.Close();

            //S + CodEESS + 01 + _ + MOVNUMERO(6 ULTIMOS DIGITOS) + S + MOVNUMEDCO
            string nombrearchivo = "S0531201_024494_S0253646";
            string nomArchivo = @"~\ASPX\Descargas\" + nombrearchivo;
            try
            {
                File.Delete(nomArchivo + ".zip");
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.Directory.Delete(nomArchivo);
            }
            catch (Exception)
            {
            }
            Directory.CreateDirectory(nomArchivo);
            objdataset.WriteXml(nomArchivo + "\\" + nombrearchivo + ".xls"); //Ruta de Guardado del Archivo dentro del Servidor
            ZipFile.CreateFromDirectory(nomArchivo, nomArchivo + ".zip");

            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombrearchivo + ".zip");
            //Response.AppendHeader("Content-Disposition", "inline; filename=" + nombrearchivo + ".xls");
            //Response.TransmitFile(Server.MapPath("~" + RutaDescarga + nombrearchivo + ".xls"));
            Response.TransmitFile(Server.MapPath(@"~\ASPX\Descargas\" + nombrearchivo + ".zip"));


            gDetH += "Descarga Correcta";
        }
        catch (Exception ex)
        {
            gDetH += "-" + "-" + ex.Message.ToString();
        }
        */
    }

    public void CargaRed()
    {
        try
        {
            string qSql = "select Red from SISMED.dbo.CATEESS group by Red ;";

            SqlCommand cmd = new SqlCommand(qSql, conSISMED); cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(); adapter.SelectCommand = cmd;

            conSISMED.Open();
            DataSet objdataset = new DataSet(); adapter.Fill(objdataset);
            conSISMED.Close();

            DataTable dt = objdataset.Tables[0];
            string ghtml = "";

            if (dt.Rows.Count > 0)
            {
                ghtml += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                ghtml += "<table class='table table-hover' id='tblRed' style='text-align: right; font-size: 14px; '>";
                ghtml += "<th class=''>Nro </th>";
                ghtml += "<th class=''>Red </th>";

                int nroitem = 0;
                ghtml += Environment.NewLine;

                foreach (DataRow dbRow in dt.Rows)
                {
                    nroitem += 1;
                    ghtml += "<tr onclick=\"txtRedCarga('" + dbRow["Red"].ToString() + "')\" data-dismiss='modal'>";
                    ghtml += "<td>" + nroitem + "</td>";
                    ghtml += "<td style='text-align: left;'>" + dbRow["Red"].ToString() + "</td>";
                    ghtml += "</tr>";
                    ghtml += Environment.NewLine;
                }

                ghtml += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
            else
            {
                ghtml = "<table></table><hr style='border-top: 1px solid blue'>";
            }
            LitRed.Text = ghtml;
        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
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
		response.Write(LitTABL1.Text);
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