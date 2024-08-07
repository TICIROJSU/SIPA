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

public partial class ASPX_IROJVAR_SISMED_FarmaPreciosL : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
        }
        if (Page.IsPostBack)
        {
        }
    }

    protected void btnBuscarProd_Click(object sender, EventArgs e)
    {
        try
        {
            string qSql = "select *,(case when DateDiff(MONTH,GETDATE(),FECHAVCTO)<4 THEN 1 ELSE 0 END) AS 'CVCTO' " +
            "from ( SELECT CP.CodSismed, (case when CP.Producto=CP.ProductoAlternativo then CP.Producto else CP.ProductoAlternativo end) as 'Producto', TC.Nombre, CP.CodPF, (SELECT TOP 1 CONVERT(VARCHAR,FECHAVCTO,103) AS 'FechaVcto' " +
            "FROM IROf.dbo.DISPENSACIONPRODUCTOF WHERE CodSismed=CP.CodSismed AND Activo=1 AND IdCentroCosto<>2)  AS 'FECHAVCTO', " +
            "(CASE WHEN CP.CodSismed='JS001' THEN (select SUM(STOCKACTUAL) AS 'CANT' from IROf.dbo.ProductosFarmacia " +
            "where Producto like '%TREPANO%CORNEAL%DONANTE%') ELSE (CASE WHEN CP.CodSismed='JS002' THEN (select SUM(STOCKACTUAL) AS 'CANT' " +
            "from IROf.dbo.ProductosFarmacia " +
            "where Producto like '%TREPANO%CORNEAL%RECEPTOR%') ELSE PF.StockActual END) END) AS 'StockActual', " +
            "(CASE WHEN CP.CodSismed='JS001' THEN (select MAX(PrecioUnit) AS 'PU' " +
            "from IROf.dbo.ProductosFarmacia where Producto like '%TREPANO%CORNEAL%DONANTE%') ELSE (CASE WHEN CP.CodSismed='JS002' THEN (select MAX(PrecioUnit) AS 'PU' " +
            "from IROf.dbo.ProductosFarmacia where Producto like '%TREPANO%CORNEAL%RECEPTOR%') ELSE PF.PrecioUnit END) END" +
            ") AS 'PrecioUnit', " +
            "IROf.dbo.F_FECHALARGA(GETDATE(),1) AS 'FECHA' " +
            "FROM IROf.dbo.TIPOCATEGORIAMEDICAMENTO TCM " +
            "INNER JOIN IROf.dbo.CATALOGOPRODUCTOS CP ON TCM.IdMedicamentoCP=CP.IdCatalogMedicamentos and CP.Activo=1 and CP.EnListaPrecios=1 " +
            "INNER JOIN IROf.dbo.TIPOCATEGORIA TC ON TCM.IdTipoCategoria=TC.IdTipoCategoria " +
            "LEFT JOIN IROf.dbo.ProductosFarmacia PF ON CP.CodPF=PF.CodigoPF )CJ " +
            "order by nombre, Producto";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            objdataset.Tables[0].Columns.Remove("CodPF");
            objdataset.Tables[0].Columns.Remove("FECHA");
            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

}