using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

public partial class ASPX_IROJVAR_zPruebas_CargaArchivos01 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	[WebMethod]
	public static string CargaArchivo01(string FileUpload1_ruta)
	{
		FileUpload FileUpload1 = new FileUpload();
		//FileUpload1.FileName = FileUpload1_ruta;
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
				String savePath = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Carga\";
				if (FileUpload1.HasFile)
				{
					String fileName = FileUpload1.FileName;
					savePath += fileName;
					FileUpload1.SaveAs(savePath);
					gDetHtml = "Tu Archivo fue guardado como " + fileName;
				}
				else
				{ gDetHtml = "No haz Cargado ningun Archivo."; }
		}
		catch (Exception ex)
		{
			gDetHtml += "ERROR Registro: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
		}
		return gDetHtml;
	}

}