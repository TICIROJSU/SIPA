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

public partial class ASPX_IROJVAR_Varios_InventarioSIGA : System.Web.UI.Page
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
			//CargaTablaDTIni();
			string vd, vm, vy;
			vd = DateTime.Now.Day.ToString();
			vm = DateTime.Now.Month.ToString();
			vy = DateTime.Now.Year.ToString();

			txtFechaDoc.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
		}
		else
		{

		}

	}

	protected void bntBuscar_Click(object sender, EventArgs e)
	{
		//if (DDLMesDesde.SelectedValue == "0")
		//{
		//	CargaTablaDTIni();
		//}
		//else
		//{
		//	CargaTablaDT();
		//}
		CargaTablaDTIni();
	}

	public void CargaTablaDTIni()
	{
		//string vAnio = DDLAnio.SelectedValue;
		try
		{
			//con.Open();
			string qSql = "Select TabTC.Anio, TabTC.Mes, isnull(TabTC.TAR_CONT, 0) TAR_CONT, isnull(TabTC.CHQ_CONT_NO, 0) CHQ_CONT_NO, isnull(TabU.UPE_CONT, 0) UPE_CONT from " +
				"(	select year(Fec_TCU) Anio, month(Fec_TCU) Mes, SUM(TAR_CONT) TAR_CONT, SUM(CHQ_CONT_NO) CHQ_CONT_NO from ( " +
				"	select	(case when FecCita is null then 0 else 1 end) TAR_CONT, " +
				"			(case when FecHIS is not null then 0 else 1 end) CHQ_CONT_NO, " +
				"			(case when FecCita is not null then FecCita else (case when FecHIS is not null then FecHIS end) end) Fec_TCU, " +
				"			* " +
				"		from " +
				"		(Select cast(FEC_TAR as date) FecCita, * from NUEVA.dbo.TARJETON where year(FEC_TAR)=@Anio and (month(FEC_TAR) between @desde and @hasta) AND CIT_TAR = 'X' and TIP_TAR='C') TAR " +
				"		left JOIN " +
				"		(select CAST((cast(ANO as varchar) + '/' + cast(MES as varchar) + '/' + cast(DIA as varchar)) as date) FecHIS, * from NUEVA.dbo.CHEQ2011 where ANO=@Anio and (MES between @desde and @hasta)) HIS " +
				"		ON TAR.HIC_TAR = RIGHT('000000' + FICHAFAM, 6) and TAR.FecCita = HIS.FecHIS " +
				"	) Tab " +
				"	Group by year(Fec_TCU), month(Fec_TCU) " +
				") TabTC " +
				"left Join " +
				"	(SELECT year(FEC_UPE) Anio, month(FEC_UPE) Mes, COUNT(*) UPE_CONT " +
				"	FROM NUEVA.dbo.UPE WHERE YEAR(FEC_UPE)=@Anio AND (MONTH(FEC_UPE) between @desde and @hasta) and PRG_UPE <> '13' " +
				"	Group by year(FEC_UPE), month(FEC_UPE)) TabU " +
				"on TabTC.Anio = TabU.Anio and TabTC.Mes = TabU.Mes " +
				"order by 1, 2 ; ";

			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

			cmd2.Parameters.AddWithValue("@Anio", Convert.ToString(""));
			cmd2.Parameters.AddWithValue("@desde", Convert.ToString(""));
			cmd2.Parameters.AddWithValue("@hasta", Convert.ToString(""));

			adapter2.SelectCommand = cmd2;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			//GVtable.DataSource = dtDato;
			//GVtable.DataBind();
			//CargaTabla(dtDato);

			html = "";
			if (dtDato.Rows.Count > 0)
			{
				html += Environment.NewLine + "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
				html += "<caption class='text-center'><b>" + "" + "</b></caption>";
				html += "<tr>";
				html += "<th class='text-center'>Mes</th>";
				html += "<th class='text-center'>Citas Prg</th><th class='text-center'>Citas No Atendidas</th><th class='text-center'>Atenciones UPE</th>";
				html += "<th class='text-center'>Citas No Atendidas -UPE</th>";
				html += "</tr>" + Environment.NewLine;
				////////////////////////////////
				html += "<tr>";
				double vt_TAR_CONT = Convert.ToDouble(dtDato.Compute("sum(TAR_CONT)", String.Empty));
				double vt_CHQ_CONT_NO = Convert.ToDouble(dtDato.Compute("sum(CHQ_CONT_NO)", String.Empty));
				double vt_UPE_CONT = Convert.ToDouble(dtDato.Compute("sum(UPE_CONT)", String.Empty));
				double vt_DIF = vt_CHQ_CONT_NO - vt_UPE_CONT;
				html += "<td class='text-center'><b>Totales</b></td>";
				html += "<td class='text-center'><b>" + ClassGlobal.formatoMillar(vt_TAR_CONT.ToString()) + "</b></td>" +
					"<td class='text-center'><b>" + ClassGlobal.formatoMillar(vt_CHQ_CONT_NO.ToString()) + "</b></td>" +
					"<td class='text-center'><b>" + ClassGlobal.formatoMillar(vt_UPE_CONT.ToString()) + " </b></td>" +
					"<td class='text-center'><b>" + ClassGlobal.formatoMillar(vt_DIF.ToString()) + " </b></td>";
				html += "</tr>" + Environment.NewLine;
				int nroitem = 0;
				foreach (DataRow dbRow in dtDato.Rows)
				{
					nroitem += 1;
					html += "<tr>";
					html += "<td class='' style='text-align: center;'>" + ClassGlobal.MesNroToTexto(dbRow["Mes"].ToString()) + "</td>";
					html += "<td class='' style='text-align: center;'>" + dbRow["TAR_CONT"].ToString() + "</td>";
					html += "<td class='' style='text-align: center;'>" + dbRow["CHQ_CONT_NO"].ToString() + "</td>";
					html += "<td class='' style='text-align: center;'>" + dbRow["UPE_CONT"].ToString() + "</td>";
					html += "<td class='' style='text-align: center;'>" + (Convert.ToDouble(dbRow["CHQ_CONT_NO"].ToString()) - Convert.ToDouble(dbRow["UPE_CONT"].ToString())) + "</td>";
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
			LitTABL1.Text = html;

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