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

public partial class ASPX_IROJVAR_SISMED_FarmaICI : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{
		Server.ScriptTimeout = 3600;
		if (!Page.IsPostBack)
		{
			int vmonth = DateTime.Now.Month;
			DDLMes.SelectedIndex = vmonth;

		}

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
			string qSql = "Select TabICI.Cod_SISMED, ISNULL([DESCRIPCION SISMED], '') + '-' + ISNULL(CONCENTRACION, '') + '-' + ";
			qSql += "ISNULL(PRESENTACION, '') + '-' + ISNULL(FORMA_FARMACEUTICA, '') [DESCRIPCION SISMED], ESTADO, ";
			qSql += "Sum(SalVen) SalVen, Sum(SalSIS) SalSIS, Sum(SalOcul) SalOcul from ";
			qSql += "(     ";
			qSql += "	Select ISNULL(TabOcu.Cod_SISMED, ISNULL(TabSIS.Cod_SISMED, TabVen.Cod_SISMED)) Cod_SISMED,  ";
			qSql += "	isnull(SalOcul, '') SalOcul, isnull(SalSIS, '') SalSIS, isnull(SalVen, '') SalVen from     ";
			qSql += "		(";
			qSql += "		SELECT	KF.CodigoPF, PF.Cod_SISMED, PF.Producto, month(MP.fecha) Mes, year(MP.fecha) Anio, sum(KF.salida) SalOcul ";
			qSql += "		FROM	KproductosF KF    ";
			qSql += "				INNER JOIN MovProductos MP ON KF.NroDocumento = MP.Nrodocumento AND KF.NroOrden=MP.NroOrden               ";
			qSql += "				INNER JOIN ProductosFarmacia PF ON KF.CodigoPF = PF.CodigoPF                   ";
			qSql += "		where MP.idtipodoc in('5', '4') and YEAR(MP.fecha) = @Anio and MONTH(MP.fecha) = @Mes and PF.Producto like '%SAL%OCUL%' ";
			qSql += "		group by KF.CodigoPF, PF.Cod_SISMED, PF.Producto, month(MP.fecha), year(MP.fecha)      ";
			qSql += "		) TabOcu                  ";
			qSql += "	Full outer join               ";
			qSql += "		(";
			qSql += "		SELECT ProductosFarmacia.Cod_SISMED, sum(KproductosF.salida) as SalSIS                 ";
			qSql += "		FROM         KproductosF INNER JOIN                        ";
			qSql += "							  ProductosFarmacia ON KproductosF.CodigoPF = ProductosFarmacia.CodigoPF INNER JOIN           ";
			qSql += "							  MovProductos ON KproductosF.NroDocumento = MovProductos.Nrodocumento AND KproductosF.IdtipoDoc = MovProductos.idtipodoc AND  ";
			qSql += "							  KproductosF.NroOrden = MovProductos.NroOrden                     ";
			qSql += "		where MovProductos.ESTADO=1 AND KproductosF.NroDocumento like 's%' and MovProductos.NroOrden='500'                ";
			qSql += "		and DATEPART(mm,MovProductos.fechasis)=@Mes and year(MovProductos.fechasis)=@Anio     ";
			qSql += "		group by ProductosFarmacia.Cod_SISMED                      ";
			qSql += "		) TabSIS                  ";
			qSql += "	on TabOcu.Cod_SISMED = TabSIS.Cod_SISMED                       ";
			qSql += "	Full outer join               ";
			qSql += "		(";
			qSql += "		SELECT    ProductosFarmacia.Cod_SISMED, sum(DocumentoDetalleProd.Cantidad) as SalVen   ";
			qSql += "		FROM         ProductosFarmacia INNER JOIN                  ";
			qSql += "							  DocumentoDetalleProd ON ProductosFarmacia.IdDepartamento = DocumentoDetalleProd.IdDepartamento AND ";
			qSql += "							  ProductosFarmacia.IdTipoServicio = DocumentoDetalleProd.IdTipoServicio AND                  ";
			qSql += "							  ProductosFarmacia.CodigoPF = DocumentoDetalleProd.Codigo INNER JOIN";
			qSql += "							  Documento ON DocumentoDetalleProd.NroDocumento = Documento.NroDocumento AND DocumentoDetalleProd.Serie = Documento.serie AND ";
			qSql += "							  DocumentoDetalleProd.IdtipoDoc = Documento.IdTipoDoc AND DocumentoDetalleProd.idModulo = Documento.IdModulo                  ";
			qSql += "		where Documento.IdTipoDoc<85 and Documento.Estado=2 and year(Documento.FechaEmision) = @Anio and month(Documento.FechaEmision) = @Mes             ";
			qSql += "		group by ProductosFarmacia.Cod_SISMED                      ";
			qSql += "		) TabVen                  ";
			qSql += "	on TabOcu.Cod_SISMED = TabVen.Cod_SISMED and TabSIS.Cod_SISMED = TabVen.Cod_SISMED         ";
			qSql += ") TabICI ";
			qSql += "left join CatSISMED2 on TabICI.Cod_SISMED=CatSISMED2.COD_SISMED     ";
			qSql += "group by TabICI.Cod_SISMED, ISNULL([DESCRIPCION SISMED], '') + '-' + ISNULL(CONCENTRACION, '') + '-' +                   ";
			qSql += "ISNULL(PRESENTACION, '') + '-' + ISNULL(FORMA_FARMACEUTICA, ''), ESTADO                       ";
			qSql += "order by TabICI.Cod_SISMED       ";
			SqlCommand cmd = new SqlCommand(qSql, conSAP00);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@Anio", Convert.ToString(DDLAnio.SelectedValue));
			cmd.Parameters.AddWithValue("@Mes", Convert.ToString(DDLMes.SelectedValue));

			adapter.SelectCommand = cmd;

			conSAP00.Open();
			DataSet objdataset = new DataSet();
			adapter.Fill(objdataset);
			conSAP00.Close();

			DataTable dtDato = objdataset.Tables[0];

			GVtable.DataSource = dtDato;
			GVtable.DataBind();
			//CargaTabla(dtDato);
		}
		catch (Exception ex)
		{
			//Label1.Text = "Error";
			ex.Message.ToString();
			LitTABL1.Text = ex.Message.ToString();
		}
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