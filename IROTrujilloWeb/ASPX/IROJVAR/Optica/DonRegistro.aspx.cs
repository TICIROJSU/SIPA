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

public partial class ASPX_IROJVAR_Optica_DonRegistro : System.Web.UI.Page
{
    string ProcTip = "";
    string ProcId = "";
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        ProcTip = Request["Proc"];
        ProcId = Request["id"];
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            //txtFechaEmi.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            //txtFDesde.Text = txtFechaEmi.Text;
            //txtFHasta.Text = txtFechaEmi.Text;
        }

        switch (ProcTip)
        {
            case "New":
                RegNew();
                break;
            case "Copy":
                RegCopy();
                break;
            case "Edit":
                RegEdit();
                break;
            default:
                break;
        }

    }

    public void RegNew()
    {
        ProcTip = "New";
        lblTitulo.Text = " - Nuevo Registro";
    }

    public void RegCopy()
    {
        ProcTip = "Copy";
        lblTitulo.Text = " - Nuevo Registro - Duplicado";

        string gHTML = "";
        try
        {
            string qSql = "select * from TabLibres.dbo.CMNLlenado where idCMNLlenado = @vid";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vid", ProcId);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();


            if (objdataset.Tables[0].Rows.Count > 0)
            {
                DataRow dbRow = objdataset.Tables[0].Rows[0];

                CCOSTO_NOMBRE.Text = dbRow["CCOSTO_NOMBRE"].ToString();
                B_S.Text = dbRow["B_S"].ToString();
                Fuente_Rubro.Text = dbRow["Fuente_Rubro"].ToString();
                FF_Desc.Text = dbRow["FF_Desc"].ToString();
                Meta.Text = dbRow["Meta"].ToString();
                PROGRAMA_PRESUPUESTAL.Text = dbRow["PROGRAMA_PRESUPUESTAL"].ToString();
                RJ.Text = dbRow["RJ"].ToString();
                Generica_de_Gasto.Text = dbRow["Generica_de_Gasto"].ToString();
                Clasificador_de_Gasto.Text = dbRow["Clasificador_de_Gasto"].ToString();
                FECHA_VB.Text = dbRow["FECHA_VB"].ToString();
                N_PEDIDO.Text = dbRow["N_PEDIDO"].ToString();
                CCP_SIAF.Text = dbRow["CCP_SIAF"].ToString();
                Codigo_Item_N.Text = dbRow["Codigo_Item_N"].ToString();
                Descripcion_del_Items.Text = dbRow["Descripcion_del_Items"].ToString();
                Unidad_de_Medida.Text = dbRow["Unidad_de_Medida"].ToString();
                Precio_Unitario.Text = dbRow["Precio_Unitario"].ToString();
                Cantidad_Total_CMN_ACTUAL.Text = dbRow["Cantidad_Total_CMN_ACTUAL"].ToString();
                Valor_Total_CMN_ACTUAL.Text = dbRow["Valor_Total_CMN_ACTUAL"].ToString();
                Cantidad_EXCLUSION.Text = dbRow["Cantidad_EXCLUSION"].ToString();
                Valor_EXCLUSION.Text = dbRow["Valor_EXCLUSION"].ToString();
                Cantidad_INCLUSION.Text = dbRow["Cantidad_INCLUSION"].ToString();
                Valor_INCLUSION.Text = dbRow["Valor_INCLUSION"].ToString();
                CANTIDAD_INCLUSION_A_TABLA.Text = dbRow["CANTIDAD_INCLUSION_A_TABLA"].ToString();
                IMPORTE_TABLA.Text = dbRow["IMPORTE_TABLA"].ToString();
                CANTIDAD_ENERO.Text = dbRow["CANTIDAD_ENERO"].ToString();
                TOTAL_GASTO_ENERO.Text = dbRow["TOTAL_GASTO_ENERO"].ToString();
                CANTIDAD_FEBRERO.Text = dbRow["CANTIDAD_FEBRERO"].ToString();
                TOTAL_GASTO_FEBRERO.Text = dbRow["TOTAL_GASTO_FEBRERO"].ToString();
                NroOC_OS_Feb.Text = dbRow["NroOC_OS_Feb"].ToString();
                NroEXPEDIENTE_SIAF_Feb.Text = dbRow["NroEXPEDIENTE_SIAF_Feb"].ToString();
                FECHA_DEVENGADA_Feb.Text = dbRow["FECHA_DEVENGADA_Feb"].ToString();
                CANTIDAD_MARZO.Text = dbRow["CANTIDAD_MARZO"].ToString();
                TOTAL_GASTO_MARZO.Text = dbRow["TOTAL_GASTO_MARZO"].ToString();
                NroOC_OS_Mar.Text = dbRow["NroOC_OS_Mar"].ToString();
                NroEXPEDIENTE_SIAF_Mar.Text = dbRow["NroEXPEDIENTE_SIAF_Mar"].ToString();
                FECHA_DEVENGADA_Mar.Text = dbRow["FECHA_DEVENGADA_Mar"].ToString();
                CANTIDAD_ABRIL.Text = dbRow["CANTIDAD_ABRIL"].ToString();
                TOTAL_GASTO_ABRIL.Text = dbRow["TOTAL_GASTO_ABRIL"].ToString();
                NroOC_OS_Abr.Text = dbRow["NroOC_OS_Abr"].ToString();
                NroEXPEDIENTE_SIAF_Abr.Text = dbRow["NroEXPEDIENTE_SIAF_Abr"].ToString();
                FECHA_DEVENGADA_Abr.Text = dbRow["FECHA_DEVENGADA_Abr"].ToString();
                CANTIDAD_MAYO.Text = dbRow["CANTIDAD_MAYO"].ToString();
                TOTAL_GASTO_MAYO.Text = dbRow["TOTAL_GASTO_MAYO"].ToString();
                NroOC_OS_May.Text = dbRow["NroOC_OS_May"].ToString();
                NroEXPEDIENTE_SIAF_May.Text = dbRow["NroEXPEDIENTE_SIAF_May"].ToString();
                FECHA_DEVENGADA_May.Text = dbRow["FECHA_DEVENGADA_May"].ToString();
                CANTIDAD_JUNIO.Text = dbRow["CANTIDAD_JUNIO"].ToString();
                TOTAL_GASTO_JUNIO.Text = dbRow["TOTAL_GASTO_JUNIO"].ToString();
                NroOC_OS_Jun.Text = dbRow["NroOC_OS_Jun"].ToString();
                NroEXPEDIENTE_SIAF_Jun.Text = dbRow["NroEXPEDIENTE_SIAF_Jun"].ToString();
                FECHA_DEVENGADA_Jun.Text = dbRow["FECHA_DEVENGADA_Jun"].ToString();
                CANTIDAD_JULIO.Text = dbRow["CANTIDAD_JULIO"].ToString();
                TOTAL_GASTO_JULIO.Text = dbRow["TOTAL_GASTO_JULIO"].ToString();
                NroOC_OS_Jul.Text = dbRow["NroOC_OS_Jul"].ToString();
                NroEXPEDIENTE_SIAF_Jul.Text = dbRow["NroEXPEDIENTE_SIAF_Jul"].ToString();
                FECHA_DEVENGADA_Jul.Text = dbRow["FECHA_DEVENGADA_Jul"].ToString();
                CANTIDAD_AGOSTO.Text = dbRow["CANTIDAD_AGOSTO"].ToString();
                TOTAL_GASTO_AGOSTO.Text = dbRow["TOTAL_GASTO_AGOSTO"].ToString();
                NroOC_OS_Ago.Text = dbRow["NroOC_OS_Ago"].ToString();
                NroEXPEDIENTE_SIAF_Ago.Text = dbRow["NroEXPEDIENTE_SIAF_Ago"].ToString();
                FECHA_DEVENGADA_Ago.Text = dbRow["FECHA_DEVENGADA_Ago"].ToString();
                CANTIDAD_SETIEMBRE.Text = dbRow["CANTIDAD_SETIEMBRE"].ToString();
                TOTAL_GASTO_SETIEMBRE.Text = dbRow["TOTAL_GASTO_SETIEMBRE"].ToString();
                NroOC_OS_Set.Text = dbRow["NroOC_OS_Set"].ToString();
                NroEXPEDIENTE_SIAF_Set.Text = dbRow["NroEXPEDIENTE_SIAF_Set"].ToString();
                FECHA_DEVENGADA_Set.Text = dbRow["FECHA_DEVENGADA_Set"].ToString();
                CANTIDAD_OCTUBRE.Text = dbRow["CANTIDAD_OCTUBRE"].ToString();
                TOTAL_GASTO_OCTUBRE.Text = dbRow["TOTAL_GASTO_OCTUBRE"].ToString();
                NroOC_OS_Oct.Text = dbRow["NroOC_OS_Oct"].ToString();
                NroEXPEDIENTE_SIAF_Oct.Text = dbRow["NroEXPEDIENTE_SIAF_Oct"].ToString();
                FECHA_DEVENGADA_Oct.Text = dbRow["FECHA_DEVENGADA_Oct"].ToString();
                CANTIDAD_NOVIEMBRE.Text = dbRow["CANTIDAD_NOVIEMBRE"].ToString();
                TOTAL_GASTO_NOVIEMBRE.Text = dbRow["TOTAL_GASTO_NOVIEMBRE"].ToString();
                NroOC_OS_Nov.Text = dbRow["NroOC_OS_Nov"].ToString();
                NroEXPEDIENTE_SIAF_Nov.Text = dbRow["NroEXPEDIENTE_SIAF_Nov"].ToString();
                FECHA_DEVENGADA_Nov.Text = dbRow["FECHA_DEVENGADA_Nov"].ToString();
                CANTIDAD_DICIEMBRE.Text = dbRow["CANTIDAD_DICIEMBRE"].ToString();
                TOTAL_GASTO_DICIEMBRE.Text = dbRow["TOTAL_GASTO_DICIEMBRE"].ToString();
                NroOC_OS_Dic.Text = dbRow["NroOC_OS_Dic"].ToString();
                NroEXPEDIENTE_SIAF_Dic.Text = dbRow["NroEXPEDIENTE_SIAF_Dic"].ToString();
                FECHA_DEVENGADA_Dic.Text = dbRow["FECHA_DEVENGADA_Dic"].ToString();
                CANTIDAD_ANIO.Text = dbRow["CANTIDAD_ANIO"].ToString();
                TOTAL_GASTO_ANIO.Text = dbRow["TOTAL_GASTO_ANIO"].ToString();
                TOT_GASTO_IMPORTE_VALIDA.Text = dbRow["TOT_GASTO_IMPORTE_VALIDA"].ToString();

            }

        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        //LitTABL1.Text = gHTML;
        CCOSTO_NOMBRE.Focus();

    }

    public void RegEdit()
    {
        ProcTip = "Edit";
        lblTitulo.Text = " - Modificacion de Registro";
    }


    [WebMethod]
    public static string SetProductosOptica(string CodProd, string Cantidad)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "UPDATE IROBDOptica.dbo.ProductosOptica SET StockActual = StockActual + @stock " +
                "where Codigo=@Codigo;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@stock", Cantidad);
            cmd.Parameters.AddWithValue("@Codigo", CodProd);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Registro Correcto.";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string SetBtnGuardar(string vfechadoc, string vidproveedor, string viduser, string vmotivo, string vobservacion, string vmontoc, string vtipodoc, string vnrodoc, string vidalmorig, string vidalmdst)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO IROBDOptica.dbo.MIngreso (fechadoc, fechareg, idproveedor, iduser, estado, motivo, observacion, montoc, tipodoc, nrodoc, idalmorig, idalmdst) VALUES (@fechadoc, @fechareg, @idproveedor, @iduser, @estado, @motivo, @observacion, @montoc, @tipodoc, @nrodoc, @idalmorig, @idalmdst) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@fechadoc", vfechadoc);
            cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
            cmd.Parameters.AddWithValue("@idproveedor", vidproveedor);
            cmd.Parameters.AddWithValue("@iduser", viduser);
            cmd.Parameters.AddWithValue("@estado", "1");
            cmd.Parameters.AddWithValue("@motivo", vmotivo);
            cmd.Parameters.AddWithValue("@observacion", vobservacion);
            cmd.Parameters.AddWithValue("@montoc", vmontoc);
            cmd.Parameters.AddWithValue("@tipodoc", vtipodoc);
            cmd.Parameters.AddWithValue("@nrodoc", vnrodoc);
            cmd.Parameters.AddWithValue("@idalmorig", vidalmorig);
            cmd.Parameters.AddWithValue("@idalmdst", vidalmdst);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml += idReg.ToString();
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
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
        //response.Write(LitTABL1.Text);
        response.End();
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        //form.Controls.Add(GVtable);
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


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        switch (ProcTip)
        {
            case "New":
                RegNewProc();
                break;
            case "Copy":
                RegNewProc();
                break;
            case "Edit":
                RegEditProc();
                break;
            default:
                break;
        }
    }

    public void RegNewProc()
    {
        try
        {
            //con.Open();
            string consql = "INSERT INTO TabLibres.dbo.CMNLlenado " +
                "(CCOSTO_COD, CCOSTO_NOMBRE, B_S, Fuente_Rubro, FF_Desc, Meta, " +
                "PROGRAMA_PRESUPUESTAL, RJ, Generica_de_Gasto, Clasificador_de_Gasto, " +
                "FECHA_VB, N_PEDIDO, CCP_SIAF, Codigo_Item_N, Descripcion_del_Items, " +
                "Unidad_de_Medida, Precio_Unitario, Cantidad_Total_CMN_ACTUAL, " +
                "Valor_Total_CMN_ACTUAL, Cantidad_EXCLUSION, Valor_EXCLUSION, " +
                "Cantidad_INCLUSION, Valor_INCLUSION, CANTIDAD_INCLUSION_A_TABLA, " +
                "IMPORTE_TABLA, CANTIDAD_ENERO, TOTAL_GASTO_ENERO, CANTIDAD_FEBRERO, TOTAL_GASTO_FEBRERO, NroOC_OS_Feb, NroEXPEDIENTE_SIAF_Feb, " +
                "FECHA_DEVENGADA_Feb, CANTIDAD_MARZO, TOTAL_GASTO_MARZO, " +
                "NroOC_OS_Mar, NroEXPEDIENTE_SIAF_Mar, FECHA_DEVENGADA_Mar, " +
                "CANTIDAD_ABRIL, TOTAL_GASTO_ABRIL, NroOC_OS_Abr, NroEXPEDIENTE_SIAF_Abr, FECHA_DEVENGADA_Abr, CANTIDAD_MAYO, TOTAL_GASTO_MAYO, NroOC_OS_May, NroEXPEDIENTE_SIAF_May, FECHA_DEVENGADA_May, CANTIDAD_JUNIO, " +
                "TOTAL_GASTO_JUNIO, NroOC_OS_Jun, NroEXPEDIENTE_SIAF_Jun, FECHA_DEVENGADA_Jun, CANTIDAD_JULIO, TOTAL_GASTO_JULIO, NroOC_OS_Jul, NroEXPEDIENTE_SIAF_Jul, FECHA_DEVENGADA_Jul, CANTIDAD_AGOSTO, TOTAL_GASTO_AGOSTO, " +
                "NroOC_OS_Ago, NroEXPEDIENTE_SIAF_Ago, FECHA_DEVENGADA_Ago, CANTIDAD_SETIEMBRE, TOTAL_GASTO_SETIEMBRE, NroOC_OS_Set, NroEXPEDIENTE_SIAF_Set, " +
                "FECHA_DEVENGADA_Set, CANTIDAD_OCTUBRE, TOTAL_GASTO_OCTUBRE, NroOC_OS_Oct, NroEXPEDIENTE_SIAF_Oct, FECHA_DEVENGADA_Oct, CANTIDAD_NOVIEMBRE, TOTAL_GASTO_NOVIEMBRE, NroOC_OS_Nov, NroEXPEDIENTE_SIAF_Nov, " +
                "FECHA_DEVENGADA_Nov, CANTIDAD_DICIEMBRE, TOTAL_GASTO_DICIEMBRE, NroOC_OS_Dic, NroEXPEDIENTE_SIAF_Dic, FECHA_DEVENGADA_Dic, CANTIDAD_ANIO, " +
                "TOTAL_GASTO_ANIO, TOT_GASTO_IMPORTE_VALIDA) " +
                "VALUES (@CCOSTO_COD, @CCOSTO_NOMBRE, @B_S, @Fuente_Rubro, @FF_Desc, " +
                "@Meta, @PROGRAMA_PRESUPUESTAL, @RJ, @Generica_de_Gasto, @Clasificador_de_Gasto, @FECHA_VB, @N_PEDIDO, @CCP_SIAF, @Codigo_Item_N, @Descripcion_del_Items, @Unidad_de_Medida, @Precio_Unitario, @Cantidad_Total_CMN_ACTUAL, @Valor_Total_CMN_ACTUAL, @Cantidad_EXCLUSION, @Valor_EXCLUSION, " +
                "@Cantidad_INCLUSION, @Valor_INCLUSION, @CANTIDAD_INCLUSION_A_TABLA, @IMPORTE_TABLA, @CANTIDAD_ENERO, @TOTAL_GASTO_ENERO, @CANTIDAD_FEBRERO, @TOTAL_GASTO_FEBRERO, @NroOC_OS_Feb, @NroEXPEDIENTE_SIAF_Feb, @FECHA_DEVENGADA_Feb, " +
                "@CANTIDAD_MARZO, @TOTAL_GASTO_MARZO, @NroOC_OS_Mar, @NroEXPEDIENTE_SIAF_Mar, @FECHA_DEVENGADA_Mar, @CANTIDAD_ABRIL, @TOTAL_GASTO_ABRIL, @NroOC_OS_Abr, @NroEXPEDIENTE_SIAF_Abr, @FECHA_DEVENGADA_Abr, @CANTIDAD_MAYO, " +
                "@TOTAL_GASTO_MAYO, @NroOC_OS_May, @NroEXPEDIENTE_SIAF_May, @FECHA_DEVENGADA_May, @CANTIDAD_JUNIO, @TOTAL_GASTO_JUNIO, @NroOC_OS_Jun, " +
                "@NroEXPEDIENTE_SIAF_Jun, @FECHA_DEVENGADA_Jun, @CANTIDAD_JULIO, @TOTAL_GASTO_JULIO, @NroOC_OS_Jul, @NroEXPEDIENTE_SIAF_Jul, @FECHA_DEVENGADA_Jul, " +
                "@CANTIDAD_AGOSTO, @TOTAL_GASTO_AGOSTO, @NroOC_OS_Ago, @NroEXPEDIENTE_SIAF_Ago, @FECHA_DEVENGADA_Ago, @CANTIDAD_SETIEMBRE, @TOTAL_GASTO_SETIEMBRE, " +
                "@NroOC_OS_Set, @NroEXPEDIENTE_SIAF_Set, @FECHA_DEVENGADA_Set, @CANTIDAD_OCTUBRE, @TOTAL_GASTO_OCTUBRE, @NroOC_OS_Oct, @NroEXPEDIENTE_SIAF_Oct, " +
                "@FECHA_DEVENGADA_Oct, @CANTIDAD_NOVIEMBRE, @TOTAL_GASTO_NOVIEMBRE, " +
                "@NroOC_OS_Nov, @NroEXPEDIENTE_SIAF_Nov, @FECHA_DEVENGADA_Nov, @CANTIDAD_DICIEMBRE, @TOTAL_GASTO_DICIEMBRE, @NroOC_OS_Dic, @NroEXPEDIENTE_SIAF_Dic, " +
                "@FECHA_DEVENGADA_Dic, @CANTIDAD_ANIO, @TOTAL_GASTO_ANIO, " +
                "@TOT_GASTO_IMPORTE_VALIDA) " +
                "SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@CCOSTO_COD", "");
            cmd.Parameters.AddWithValue("@CCOSTO_NOMBRE", CCOSTO_NOMBRE.Text);
            cmd.Parameters.AddWithValue("@B_S", B_S.SelectedValue);
            cmd.Parameters.AddWithValue("@Fuente_Rubro", Fuente_Rubro.Text);
            cmd.Parameters.AddWithValue("@FF_Desc", FF_Desc.Text);
            cmd.Parameters.AddWithValue("@Meta", Meta.Text);
            cmd.Parameters.AddWithValue("@PROGRAMA_PRESUPUESTAL", PROGRAMA_PRESUPUESTAL.Text);
            cmd.Parameters.AddWithValue("@RJ", RJ.Text);
            cmd.Parameters.AddWithValue("@Generica_de_Gasto", Generica_de_Gasto.Text);
            cmd.Parameters.AddWithValue("@Clasificador_de_Gasto", Clasificador_de_Gasto.Text);
            cmd.Parameters.AddWithValue("@FECHA_VB", FECHA_VB.Text);
            cmd.Parameters.AddWithValue("@N_PEDIDO", N_PEDIDO.Text);
            cmd.Parameters.AddWithValue("@CCP_SIAF", CCP_SIAF.Text);
            cmd.Parameters.AddWithValue("@Codigo_Item_N", Codigo_Item_N.Text);
            cmd.Parameters.AddWithValue("@Descripcion_del_Items", Descripcion_del_Items.Text);
            cmd.Parameters.AddWithValue("@Unidad_de_Medida", Unidad_de_Medida.Text);
            cmd.Parameters.AddWithValue("@Precio_Unitario", Precio_Unitario.Text);
            cmd.Parameters.AddWithValue("@Cantidad_Total_CMN_ACTUAL", Cantidad_Total_CMN_ACTUAL.Text);
            cmd.Parameters.AddWithValue("@Valor_Total_CMN_ACTUAL", Valor_Total_CMN_ACTUAL.Text);
            cmd.Parameters.AddWithValue("@Cantidad_EXCLUSION", Cantidad_EXCLUSION.Text);
            cmd.Parameters.AddWithValue("@Valor_EXCLUSION", Valor_EXCLUSION.Text);
            cmd.Parameters.AddWithValue("@Cantidad_INCLUSION", Cantidad_INCLUSION.Text);
            cmd.Parameters.AddWithValue("@Valor_INCLUSION", Valor_INCLUSION.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_INCLUSION_A_TABLA", CANTIDAD_INCLUSION_A_TABLA.Text);
            cmd.Parameters.AddWithValue("@IMPORTE_TABLA", IMPORTE_TABLA.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_ENERO", CANTIDAD_ENERO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_ENERO", TOTAL_GASTO_ENERO.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_FEBRERO", CANTIDAD_FEBRERO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_FEBRERO", TOTAL_GASTO_FEBRERO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Feb", NroOC_OS_Feb.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Feb", NroEXPEDIENTE_SIAF_Feb.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Feb", FECHA_DEVENGADA_Feb.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_MARZO", CANTIDAD_MARZO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_MARZO", TOTAL_GASTO_MARZO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Mar", NroOC_OS_Mar.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Mar", NroEXPEDIENTE_SIAF_Mar.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Mar", FECHA_DEVENGADA_Mar.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_ABRIL", CANTIDAD_ABRIL.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_ABRIL", TOTAL_GASTO_ABRIL.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Abr", NroOC_OS_Abr.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Abr", NroEXPEDIENTE_SIAF_Abr.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Abr", FECHA_DEVENGADA_Abr.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_MAYO", CANTIDAD_MAYO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_MAYO", TOTAL_GASTO_MAYO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_May", NroOC_OS_May.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_May", NroEXPEDIENTE_SIAF_May.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_May", FECHA_DEVENGADA_May.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_JUNIO", CANTIDAD_JUNIO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_JUNIO", TOTAL_GASTO_JUNIO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Jun", NroOC_OS_Jun.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Jun", NroEXPEDIENTE_SIAF_Jun.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Jun", FECHA_DEVENGADA_Jun.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_JULIO", CANTIDAD_JULIO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_JULIO", TOTAL_GASTO_JULIO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Jul", NroOC_OS_Jul.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Jul", NroEXPEDIENTE_SIAF_Jul.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Jul", FECHA_DEVENGADA_Jul.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_AGOSTO", CANTIDAD_AGOSTO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_AGOSTO", TOTAL_GASTO_AGOSTO.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Ago", NroOC_OS_Ago.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Ago", NroEXPEDIENTE_SIAF_Ago.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Ago", FECHA_DEVENGADA_Ago.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_SETIEMBRE", CANTIDAD_SETIEMBRE.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_SETIEMBRE", TOTAL_GASTO_SETIEMBRE.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Set", NroOC_OS_Set.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Set", NroEXPEDIENTE_SIAF_Set.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Set", FECHA_DEVENGADA_Set.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_OCTUBRE", CANTIDAD_OCTUBRE.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_OCTUBRE", TOTAL_GASTO_OCTUBRE.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Oct", NroOC_OS_Oct.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Oct", NroEXPEDIENTE_SIAF_Oct.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Oct", FECHA_DEVENGADA_Oct.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_NOVIEMBRE", CANTIDAD_NOVIEMBRE.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_NOVIEMBRE", TOTAL_GASTO_NOVIEMBRE.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Nov", NroOC_OS_Nov.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Nov", NroEXPEDIENTE_SIAF_Nov.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Nov", FECHA_DEVENGADA_Nov.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_DICIEMBRE", CANTIDAD_DICIEMBRE.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_DICIEMBRE", TOTAL_GASTO_DICIEMBRE.Text);
            cmd.Parameters.AddWithValue("@NroOC_OS_Dic", NroOC_OS_Dic.Text);
            cmd.Parameters.AddWithValue("@NroEXPEDIENTE_SIAF_Dic", NroEXPEDIENTE_SIAF_Dic.Text);
            cmd.Parameters.AddWithValue("@FECHA_DEVENGADA_Dic", FECHA_DEVENGADA_Dic.Text);
            cmd.Parameters.AddWithValue("@CANTIDAD_ANIO", CANTIDAD_ANIO.Text);
            cmd.Parameters.AddWithValue("@TOTAL_GASTO_ANIO", TOTAL_GASTO_ANIO.Text);
            cmd.Parameters.AddWithValue("@TOT_GASTO_IMPORTE_VALIDA", TOT_GASTO_IMPORTE_VALIDA.Text);

            conSAP00.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00.Close();

            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');window.location.assign('../../TabLJVAR/CMNRegistro/CMNRegistrar.aspx?Proc=New&id=0');</script>");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitErrores.Text = ex.Message.ToString();
        }

    }

    public void RegEditProc()
    {
        ProcTip = "Edit";
        lblTitulo.Text = " - Modificacion de Registro";
    }


}