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

public partial class ASPX_IROJVAR_SISMED_DispoIRO2 : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    SqlBulkCopy sqlBC;
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            DDLMes.SelectedIndex = vmonth - 1;

            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            DDLAnio.SelectedValue = vy;

            //txtPeriodo.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');

            btnComparaSIS.Visible = false;
        }
        else
        {
            btnComparaSIS.Visible = true;
        }

    }

    [WebMethod]
    public static string GetStockVto(string vCodSisMed)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        try
        {
            string qSql = "SELECT CP.CodSismed AS 'CODMED', CP.CodDisga AS 'CODSIGA', CP.Producto AS 'MEDICAMENTO', SUM(AP.StockActualAlmacen) AS 'STOCKACTUAL', " +
                "ISNULL(AP.Lote,'') AS 'LOTE', CONVERT(VARCHAR,AP.FechVen,103) AS 'FECHAVCTO', ISNULL(AP.RegSanitario,'') AS 'REGSANITARIO' " +
                "FROM ALMACENPRODUCTOF AP " +
                "INNER JOIN CATALOGOPRODUCTOS CP ON AP.CodSismed = CP.CodSismed " +
                "WHERE AP.IdCentroCosto IN (2,3,4) AND AP.NroDocumentoMovimientoInicial LIKE 'IA%' AND AP.Anulado=0 AND AP.BAJA IS NULL and " +
                "AP.StockActualAlmacen>0 and IROf.dbo.F_DIFERENCIAFECHAS(FechVen,GETDATE()) >= 0 and " +
                "CP.CodSismed = '" + vCodSisMed + "' " +
                "GROUP BY CP.CodSismed, CP.CodDisga, CP.CodPF, CP.Producto, AP.IdCentroCosto, AP.Lote, AP.FechVen, AP.RegSanitario, AP.IdPA " +
                "order by cp.Producto";


            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>N° </th>";
                gDetH += "<th class=''>CodMed</th>";
                gDetH += "<th class=''>Stock</th>";
                //gDetH += "<th class=''>Tipo</th>";
                //gDetH += "<th class=''>CCost</th>";
                //gDetH += "<th class=''>Origen</th>";
                gDetH += "<th class=''>Lote</th>";
                gDetH += "<th class=''>Fec. Vto</th>";
                gDetH += "<th class=''>Reg. San.</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-left'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["CODMED"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["STOCKACTUAL"].ToString() + "</td>";
                    //gDetH += "<td class='text-right'>" + dbRow["TIPO"].ToString() + "</td>";
                    //gDetH += "<td class='text-right'>" + dbRow["CCosto"].ToString() + "</td>";
                    //gDetH += "<td class='text-right'>" + dbRow["ORIGEN"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["Lote"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["FECHAVCTO"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["REGSANITARIO"].ToString() + "</td>";

                    gDetH += "</tr>" + Environment.NewLine;
                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    [WebMethod]
    public static string GetSIGA1(string vCodSIGA, string vProducto, string vStock, string vConsProm, string vStkMes)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SIGA);
        string gDetH = "";
        try
        {
            string qSql = "exec SALMA_SIGA.dbo.JVAR_SIGA_PEDIDOS '101', '" + vCodSIGA + "'";

            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>Año </th>";
                gDetH += "<th class=''>Stock</th>";
                gDetH += "<th class=''>Cons Prom</th>";
                gDetH += "<th class=''>Stk Mes</th>";
                gDetH += "<th class=''>Stock Solicitado</th>";
                gDetH += "<th class=''>Stock Atendido</th>";
                gDetH += "<th class=''>Stock x Atender</th>";
                gDetH += "<th class=''>Det</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                string vdnone = " class='Primero' style='display: table-row' ";
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr " + vdnone + ">";
                    gDetH += "<td class='text-center'>" + dbRow["Anio"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + vStock + "</td>";
                    gDetH += "<td class='text-right'>" + vConsProm + "</td>";
                    gDetH += "<td class='text-right'>" + vStkMes + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec(dbRow["Cant_Solicitada"].ToString()) + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec(dbRow["Cant_Atendida"].ToString()) + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec(dbRow["CantXAtender"].ToString()) + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalSIGA2' onclick=\"fSIGA2('" + vCodSIGA + "', '" + vProducto + "')\"><i class='fa fa-fw fa-exchange'></i></button></td>";
                    gDetH += "</tr>" + Environment.NewLine;

                    if (nroitem == 1)
                    {
                        vdnone = " class='Segundo' style='display: none' ";
                        gDetH += "<tr>" +
                            "<td colspan='2' onclick='fMostrarTodo()'>Historial...</td>" +
                            "<tr>";
                    }

                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    [WebMethod]
    public static string GetSIGA2(string vCodSIGA, string vProducto)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SIGA);
        string gDetH = "";
        try
        {
            string qSql = "exec SALMA_SIGA.dbo.JVAR_SIGA_PEDIDOS '102', '" + vCodSIGA + "'";

            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>Año </th>";
                gDetH += "<th class=''>Contrato</th>";
                gDetH += "<th class=''>Cant Atendida</th>";
                gDetH += "<th class=''>Cant Adjudicada</th>";
                gDetH += "<th class=''>Cant x Atender</th>";
                gDetH += "<th class=''>Fte. Fto.</th>";
                gDetH += "<th class=''>Proveedor</th>";
                gDetH += "<th class=''>Det</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                string vdnone = " class='Primero' style='display: table-row' ";
                string vanioeje = dtDatoDetAt.Rows[0]["ANO_EJE"].ToString();
                string vpass = "1ra";
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;

                    if (vanioeje != dbRow["ANO_EJE"].ToString() && vpass == "1ra")
                    {
                        vdnone = " class='Segundo' style='display: none' ";
                        gDetH += "<tr>" +
                            "<td colspan='2' onclick='fMostrarTodo()'>Historial...</td>" +
                            "<tr>";
                        vpass = "2da";
                    }

                    gDetH += "<tr " + vdnone + ">";
                    gDetH += "<td class='text-center'>" + dbRow["ANO_EJE"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["SEC_CONTRATO"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec(dbRow["CANTIDAD"].ToString()) + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec(dbRow["CANTIDAD_ADJUDICA"].ToString()) + "</td>";
                    gDetH += "<td class='text-right'>" + ClassGlobal.formatoMillarDec((Double.Parse(dbRow["CANTIDAD"].ToString()) - Double.Parse(dbRow["CANTIDAD_ADJUDICA"].ToString())).ToString()) + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["DOC_SIAF"].ToString() + "</td>";
                    gDetH += "<td class='text-right'>" + dbRow["NOMBRE_PROV"].ToString() + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalSIGA2' onclick=\"fSIGA3('')\"><i class='fa fa-fw fa-exchange'></i></button></td>";
                    gDetH += "</tr>" + Environment.NewLine;

                    if (nroitem == 1) { vanioeje = dbRow["ANO_EJE"].ToString(); }

                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }


    [WebMethod]
    public static string GetDetCodSISMED(string vCodSisMed, string vAnio, string vMes, string consTotal, string ConsProm, string stkTotal, string stkMes, string SSO, string vProducto)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DateTime vfecha = DateTime.Parse("15/" + vMes + "/" + vAnio);
        string gDetH = "";
        try
        {
            //string qSql = "exec SISMED.dbo.JVARDispoSALMACodSISMED @FechaDisp, @CodSismed ";
            //string qSql = "exec IROf.[dbo].[SPW_CONSUMOANUAL] 602, null, @vAnio,  @vMes, @CodSismed, 12, null, null, null, null, null, 0, '12', null ";
            string qSql = "select * from IROf.dbo.ztmp_SPW_CONSUMOANUAL_602 where CODSISMED = @CodSismed ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@vAnio", vAnio);
            //cmd.Parameters.AddWithValue("@vMes", vMes);
            cmd.Parameters.AddWithValue("@CodSismed", vCodSisMed);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDato = objdataset.Tables[0];

            if (dtDato.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>N° </th>" +
                    "<th>CodMED</th>" +
                    "<th>Mes1</th><th>Mes2</th><th>Mes3</th><th>Mes4</th>" +
                    "<th>Mes5</th><th>Mes6</th><th>Mes7</th><th>Mes8</th>" +
                    "<th>Mes9</th><th>Mes10</th><th>Mes11</th><th>Mes12</th>" +
                    "<th>Mes13</th><th>Consumo</th><th>Promedio</th>" +
                    "<th>Stock</th><th>Stock Mes</th><th>SSO</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDato.Rows)
                {
                    //if (dbRow["CODSISMED"].ToString() == vCodSisMed)
                    //{
                        nroitem += 1;
                        gDetH += "<tr>";
                        gDetH += "<td class='text-left'>" + nroitem + "</td>";
                        gDetH += "<td>" + dbRow["CODSISMED"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes1"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes2"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes3"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes4"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes5"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes6"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes7"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes8"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes9"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes10"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes11"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes12"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes13"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["CONSUMO_TOTAL"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["CONSUMO_PROMEDIO"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["STOCK_TOTAL"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["STOCK_MES"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["SSO"].ToString() + "</td>";
                        gDetH += "</tr>" + Environment.NewLine;
                    //}

                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    [WebMethod]
    public static string GetDetCodSISMED2(string vCodSisMed, string vAnio, string vMes, string consTotal, string ConsProm, string stkTotal, string stkMes, string SSO, string vProducto, GridView vTable)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DateTime vfecha = DateTime.Parse("15/" + vMes + "/" + vAnio);
        string gDetH = "";
        try
        {
            //GridView vGVTable = new GridView();
            //vGVTable.DataSource = vTable;
            
            DataTable dtDato = ClassGlobal.GetDataTable(vTable);
            if (dtDato.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class=''>N° </th>" +
                    "<th>CodMED</th>" +
                    "<th>Mes1</th><th>Mes2</th><th>Mes3</th><th>Mes4</th>" +
                    "<th>Mes5</th><th>Mes6</th><th>Mes7</th><th>Mes8</th>" +
                    "<th>Mes9</th><th>Mes10</th><th>Mes11</th><th>Mes12</th>" +
                    "<th>Mes13</th><th>Consumo</th><th>Promedio</th>" +
                    "<th>Stock</th><th>Stock Mes</th><th>SSO</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDato.Rows)
                {
                    if (dbRow["CODSISMED"].ToString() == vCodSisMed)
                    {
                        nroitem += 1;
                        gDetH += "<tr>";
                        gDetH += "<td class='text-left'>" + nroitem + "</td>";
                        gDetH += "<td>" + dbRow["CODSISMED"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes1"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes2"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes3"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes4"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes5"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes6"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes7"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes8"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes9"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes10"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes11"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes12"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["Mes13"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["CONSUMO_TOTAL"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["CONSUMO_PROMEDIO"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["STOCK_TOTAL"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["STOCK_MES"].ToString() + "</td>";
                        gDetH += "<td>" + dbRow["SSO"].ToString() + "</td>";
                        gDetH += "</tr>" + Environment.NewLine;
                    }

                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }

            gDetH += vTable.ToString();
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetH += ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT("");
    }

    protected void btnPetitorio_Click(object sender, EventArgs e)
    {
        CargaTablaDT("1");
    }

    protected void btnNoPetitorio_Click(object sender, EventArgs e)
    {
        CargaTablaDT("0");
    }

    public void CargaTablaDT(string vPet)
    {
        //string vPeriodo = txtPeriodo.Text;
        DateTime vfecha = DateTime.Parse("15/" + DDLMes.SelectedValue + "/" + DDLAnio.SelectedValue);

        //DataTable dtGetDetJV = CargaTabGetDetCodSISMED(vfecha);

        string vchkCat = "", vchkGla = "", vchkRet = "", vchkOG = "", 
            vchkSQL = "", vchkOR="";

        if (chkCatarata.Checked) { vchkCat = " [DEPARTAMENTO/SERVICIO] like '%CATARATA%' "; }
        if (chkGlaucoma.Checked) { vchkGla = " [DEPARTAMENTO/SERVICIO] like '%GLAUCOMA%' "; }
        if (chkRetina.Checked) { vchkRet = " [DEPARTAMENTO/SERVICIO] like '%RETINA%' "; }
        if (chkOG.Checked) { vchkOG = " [DEPARTAMENTO/SERVICIO] like '%OFTALMOLOGIA GENERAL%' "; }

        if (vchkCat != "") { vchkSQL = vchkCat; vchkOR = "OR"; }
        if (vchkGla != "") { vchkSQL = vchkSQL + vchkOR + vchkGla; vchkOR = "OR"; }
        if (vchkRet != "") { vchkSQL = vchkSQL + vchkOR + vchkRet; vchkOR = "OR"; }
        if (vchkOG != "") { vchkSQL = vchkSQL + vchkOR + vchkOG; vchkOR = "OR"; }
        
        //vchkSQL = vchkCat + "or" + vchkGla + "or" + vchkRet + "or" + vchkOG;

        try
        {
            //string qSql = "exec SISMED.dbo.JVARDispoSALMA @vPeriodo, @vPet ";
            //string qSql = "exec IROf.[dbo].[SPW_DISPONIBILIDADANUAL] 601, NULL, @vAnio, @vMes, NULL, 12, NULL, NULL, NULL, NULL, NULL, NULL, 12, NULL ";
            string qSql = "exec IROf.[dbo].[SPW_CONSUMOANUAL] 603, null, @vAnio,  @vMes, null, 12, null, null, null, null, null, 0, '12', null ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vAnio", DDLAnio.SelectedValue);
            cmd.Parameters.AddWithValue("@vMes", DDLMes.SelectedValue);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato;

            string vchkSQLand = "";
            if (vchkSQL != "") { vchkSQLand = " and (" + vchkSQL + ")"; }

            if (vPet == "")
            {
                //dtDato = objdataset.Tables[0];
                dtDato = objdataset.Tables[0].Select(vchkSQL).CopyToDataTable();
            }
            else if (vPet == "1")
            {
                dtDato = objdataset.Tables[0].Select("PETITORIO = 1 and TIPO='M' " + vchkSQLand).CopyToDataTable();
                //dtDato = objdataset.Tables[0].Select("EN_PETITORIO = 1 and [DEPARTAMENTO/SERVICIO] like '%CATA%'").CopyToDataTable();
            }
            else //(vPet == "0")
            {
                dtDato = objdataset.Tables[0].Select("PETITORIO = 0 " + vchkSQLand).CopyToDataTable();
            }
            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();

            ddlDepto.Items.Clear();
            DataTable dtDepto = dtDato.AsEnumerable().GroupBy(r => r.Field<string>("DEPARTAMENTO/SERVICIO")).Select(g => g.First()).CopyToDataTable();
            ListItem LisTMP = new ListItem("Todos", "", true);
            ddlDepto.DataSource = dtDepto;
            ddlDepto.Items.Add(LisTMP);
            ddlDepto.DataTextField = "DEPARTAMENTO/SERVICIO";
            ddlDepto.DataValueField = "DEPARTAMENTO/SERVICIO";
            ddlDepto.DataBind();


            SqlBulk_CopiaRegistros("IROf.dbo.ztmp_SPW_CONSUMOANUAL_602", dtDato, conSAP00);

            DataTable dt602 = new DataTable();
            dt602 = GetTabGroupConsDispo();
            SqlBulk_CopiaRegistros("IROf.dbo.ztmp_SPW_CONSUMOANUAL_602", dt602, conSAP00);

            CargaTabla(dt602, vfecha);
            dtDato = dt602;
            GVtable.DataSource = dtDato;
            GVtable.DataBind();



            int vNormoStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='NORMOSTOCK'").ToString());
            int vSobStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SOBRESTOCK'").ToString());
            int vSubStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SUBSTOCK'").ToString());
            int vSRStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SIN ROTACION'").ToString());
            int vDesStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='DESABASTECIDO'").ToString());

            int vNCoStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='NO CONSIDERADO'").ToString());

            int vItems = vNormoStk + vSobStk + vSubStk + vSRStk + vDesStk;
            double intvNS = (double)vNormoStk / (double)vItems * 100.00;
            double intvSob = (double)vSobStk / (double)vItems * 100.00;
            double intvSub = (double)vSubStk / (double)vItems * 100.00;
            double intvSR = (double)vSRStk / (double)vItems * 100.00;
            double intvDes = (double)vDesStk / (double)vItems * 100.00;
            double intvNCo = (double)vNCoStk / (double)vItems * 100.00;

            double intvDispo = intvNS + intvSob + intvSR;

            lblTitulo.Text = vfecha.Year.ToString() + " - " + vfecha.Month.ToString();

            lblNS.Text = ClassGlobal.formatoMillarDec(intvNS.ToString()) + " %";
            lblSob.Text = ClassGlobal.formatoMillarDec(intvSob.ToString()) + " %";
            lblSub.Text = ClassGlobal.formatoMillarDec(intvSub.ToString()) + " %";
            lblSR.Text = ClassGlobal.formatoMillarDec(intvSR.ToString()) + " %";
            lblDes.Text = ClassGlobal.formatoMillarDec(intvDes.ToString()) + " %";
            lblDisp.Text = ClassGlobal.formatoMillarDec(intvDispo.ToString()) + " %";

            lblNCo.Text = ClassGlobal.formatoMillarDec(intvNCo.ToString()) + " %";

            lblNSC.Text = vNormoStk.ToString();
            lblSobC.Text = vSobStk.ToString();
            lblSubC.Text = vSubStk.ToString();
            lblSRC.Text = vSRStk.ToString();
            lblDesC.Text = vDesStk.ToString();
            lblDispC.Text = vItems.ToString();

            lblNCoC.Text = vNCoStk.ToString();

            //SqlBulk_CopiaRegistros("IROf.dbo.ztmp_SPW_CONSUMOANUAL_602", dtDato, conSAP00);

            //DataTable dt602 = new DataTable();
            //dt602 = GetTabGroupConsDispo();
            //SqlBulk_CopiaRegistros("IROf.dbo.ztmp_SPW_CONSUMOANUAL_602", dt602, conSAP00);

            //CargaTabla(dt602, vfecha);
            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void SqlBulk_CopiaRegistros(string vTable, DataTable dtDato, SqlConnection con)
    {
        string qSql = "DELETE FROM " + vTable + " ;";

        SqlCommand cmd = new SqlCommand(qSql, con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        con.Open(); cmd.ExecuteScalar(); con.Close();

        con.Open();
        sqlBC = new SqlBulkCopy(con);
        sqlBC.DestinationTableName = vTable;
        sqlBC.WriteToServer(dtDato);
        con.Close();
    }

    public DataTable GetTabGroupConsDispo()
    {
        DataTable dt = new DataTable();

        try
        {
            string qSql = "select [CODSISMED], Max([CODSIGA]) as [CODSIGA], Max([MEDICAMENTO/INSUMO]) as [MEDICAMENTO/INSUMO], " +
            "	Max([TIPO]) as [TIPO], Max([PETITORIO]) as [PETITORIO], " +
            //"   Max([C#C]) as [C#C], " +
            "	Max([DEPARTAMENTO/SERVICIO]) as [DEPARTAMENTO/SERVICIO], " +
            "	Sum(cast([MES1] as int)) as [MES1], Sum(cast([MES2] as int)) as [MES2], Sum(cast([MES3] as int)) as [MES3], " +
            "	Sum(cast([MES4] as int)) as [MES4], Sum(cast([MES5] as int)) as [MES5], Sum(cast([MES6] as int)) as [MES6], " +
            "	Sum(cast([MES7] as int)) as [MES7], Sum(cast([MES8] as int)) as [MES8], Sum(cast([MES9] as int)) as [MES9], " +
            "	Sum(cast([MES10] as int)) as [MES10], Sum(cast([MES11] as int)) as [MES11], Sum(cast([MES12] as int)) as [MES12], " +
            "	Sum(cast([MES13] as int)) as [MES13], Sum(cast([CONSUMO_TOTAL] as decimal(18, 2))) as [CONSUMO_TOTAL], " +
            "	Sum(cast([CONSUMO_MINIMO] as decimal(18, 2))) as [CONSUMO_MINIMO], " +
            "	Sum(cast([CONSUMO_MAXIMO] as decimal(18, 2))) as [CONSUMO_MAXIMO], " +
            "	Sum(cast([CONSUMO_PROMEDIO] as decimal(18, 2))) as [CONSUMO_PROMEDIO], " +
            "	Max(cast([N_MESES_CONSUMIDOS] as decimal(18, 2))) as [N_MESES_CONSUMIDOS], " +
            "	Sum(cast([CONSUMO_PROMEDIO_AJUSTADO] as decimal(18, 2))) as [CONSUMO_PROMEDIO_AJUSTADO], " +
            "	Sum(cast([STOCK_ALMACEN] as decimal(18, 2))) as [STOCK_ALMACEN], " +
            "	Sum(cast([STOCK_DISPENSACION] as decimal(18, 2))) as [STOCK_DISPENSACION], " +
            "	Sum(cast([STOCK_TOTAL] as decimal(18, 2))) as [STOCK_TOTAL], " +
            "	cast(isnull(Sum(cast([STOCK_TOTAL] as decimal(18, 2))) / NULLIF(Sum(cast([CONSUMO_PROMEDIO] as decimal(18, 2))), 0), 0) as decimal(18, 2)) as [STOCK_MES], " +
            "	SISMED.dbo.fnDispoSSO( " +
            "		cast(isnull(Sum(cast([STOCK_TOTAL] as decimal(18, 2))) / NULLIF(Sum(cast([CONSUMO_PROMEDIO] as decimal(18, 2))), 0), 0) as decimal(18, 2)), " +
            "		Sum(cast([STOCK_TOTAL] as decimal(18, 2))), Sum(cast([CONSUMO_PROMEDIO] as decimal(18, 2))), Sum(cast([MES12] as int)), " +
            "		Sum(cast([MES11] as int)), Sum(cast([MES10] as int)), Sum(cast([MES9] as int))) " +
            "	as [SSO], " +
            "	Sum(cast([STOCK_MES_AJUSTADO] as decimal(18, 2))) as [STOCK_MES_AJUSTADO], " +
            "	Max([SSO_AJUSTADO]) as [SSO_AJUSTADO] " +
            "from IROf.dbo.ztmp_SPW_CONSUMOANUAL_602 " +
            "group by [CODSISMED] ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            dt = objdataset.Tables[0];
        }
        catch (Exception ex)
        {
            //dt.Rows.Add = ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }

        return dt;
    }

    public void CargaTabla(DataTable dtDato, DateTime vfechaSQL)
    {
        string html = "";
        if (dtDato.Rows.Count > 0)
        {
            //html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
            html += Environment.NewLine + "<table id='tblbscrJS' class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''></th>";
            html += "<th class=''>CodMED</th>";
            html += "<th class=''>CodSIGA</th>";
            html += "<th class=''>Medicamento</th>";
            html += "<th style='text-align: center;'>Cons Prom</th>";
            html += "<th class='' style='text-align: center;'>Stock</th>";
            html += "<th class=''>Stock Mes</th>";

            html += "<th class='' style='text-align: center;'>SSO</th>";
            html += "<th class='' style='text-align: center;'>Pet</th>";
            html += "<th class='' style='text-align: center;'>Precio Venta</th>";

            html += "<th class='' style='text-align: center;'>Stock Farmacia</th>";
            html += "<th class='' style='text-align: center;'>Stock Almacen</th>";
            //html += "<th class='' style='text-align: center;'>C. Costo</th>";
            //html += "<th class='' style='text-align: center;'>Precio Costo</th>";
            html += "<th class='' style='text-align: center;'>Fecha Min Venc</th>";
            html += "<th class='text-center'>Vcmtos</th>";
            html += "<th class='text-center'>SIGA</th>";
            html += "<th class='text-center'>Det</th>";
            html += "<th class='text-center'>Depto/Serv</th>";
            html += "<th class='text-center'>Prom2</th>";
            html += "<th class='text-center'>Det</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            DataTable dtMedicam = GetDTCatalogoMedFarmacia();

            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vFech = "", strFechVen = "";
                DateTime vfechaVen = DateTime.Now;
                string vDetJV = CargaTabGetDetCodSISMED_STR(vfechaSQL, dbRow["CODSISMED"].ToString());
                    //dtGetDetJV.Compute("AVG(Promedio)", "CodSismed = " + dbRow["CODSISMED"].ToString()).ToString();

                html += "<tr " + strFechVen + ">";
                html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["CODSISMED"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["CODSIGA"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["MEDICAMENTO/INSUMO"].ToString() + "</td>";
                html += "<td class='' style='text-align: right;'>" + ClassGlobal.formatoMillarDec(dbRow["CONSUMO_PROMEDIO"].ToString()) + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["STOCK_TOTAL"].ToString() + "</td>";
                html += "<td class='' style='text-align: right;'>" + ClassGlobal.formatoMillarDec(dbRow["STOCK_MES"].ToString()) + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["SSO"].ToString() + "</td>";
                //html += "<td class='' style='text-align: center;'>" + dbRow["ESTADO"].ToString() + "</td>";
                string vPetstr = "0";
                if (dbRow["PETITORIO"].ToString()=="True") { vPetstr = "1"; }
                html += "<td class='' style='text-align: center;'>" + vPetstr + "</td>";

                DataTable dtMed1 = new DataTable();
                string vsPrecio = "";
                //dtMed1 = dtMedicam.Select("Cod_SISMEDHomologo = '10929'").CopyToDataTable();
                try
                {
                    dtMed1 = dtMedicam.Select("Cod_SISMEDHomologo = '" + dbRow["CODSISMED"].ToString().Trim() + "'").CopyToDataTable();
                    vsPrecio = dtMed1.Rows[0]["PrecioUnit"].ToString();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                html += "<td style='text-align: center;'>" + vsPrecio + "</td>";
                //html += "<td style='text-align: center;'>" + getPrecioSISMED(dbRow["CODSISMED"].ToString(), "PrecioUnit") + "</td>";
                //html += "<td style='text-align: center;'>" + dtMed1.Rows[0]["PrecioUnit"].ToString() + "</td>";

                html += "<td class='text-center'>" + dbRow["STOCK_DISPENSACION"].ToString() + "</td>";
                html += "<td class='text-center'>" + dbRow["STOCK_ALMACEN"].ToString() + "</td>";
                //html += "<td class='text-center'>" + dbRow["C.C"].ToString() + "</td>";
                //html += "<td class='text-center'>" + ClassGlobal.formatoMillarDec(dbRow["COSTO_ADQUISICION"].ToString()) + "</td>";

                html += "<td class='text-center'>" + vFech + "</td>";
                html += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalStkVto' onclick=\"fStockVto('" + dbRow["CODSISMED"].ToString() + "')\"><i class='fa fa-fw fa-exchange'></i></button></td>";
                //, , , , vStkMes
                html += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#modalSIGA1' onclick=\"fSIGA1('" + dbRow["CODSIGA"].ToString() + "', '" + dbRow["MEDICAMENTO/INSUMO"].ToString() + "', '" + dbRow["STOCK_TOTAL"].ToString() + "', '" + ClassGlobal.formatoMillarDec(dbRow["CONSUMO_PROMEDIO"].ToString()) + "', '" + ClassGlobal.formatoMillarDec(dbRow["STOCK_MES"].ToString()) + "')\"><i class='fa fa-fw fa-exchange'></i></button></td>";
                html += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#mDetMed' onclick=\"fDetProd('" + dbRow["CODSISMED"].ToString() + "', '" + DDLAnio.SelectedValue + "', '" + DDLMes.SelectedValue + "', '" + dbRow["CONSUMO_TOTAL"].ToString() + "', '" + dbRow["CONSUMO_PROMEDIO"].ToString() + "', '" + dbRow["STOCK_TOTAL"].ToString() + "', '" + dbRow["STOCK_MES"].ToString() + "', '" + dbRow["SSO"].ToString() + "', '" + dbRow["MEDICAMENTO/INSUMO"].ToString().Replace("\"", "") + "')\"><i class='fa fa-fw fa-exchange'></i></button></td>";
                html += "<td class='text-center'>" + dbRow["DEPARTAMENTO/SERVICIO"].ToString() + "</td>";
                html += "<td class='text-center'>" + ClassGlobal.formatoMillarDec(vDetJV) + "</td>";

                html += "<td class='text-center'><button type='button' class='btn bg-navy' data-toggle='modal' data-target='#mDetMed' onclick=\"fDetProd2('" + dbRow["CODSISMED"].ToString() + "', '" + DDLAnio.SelectedValue + "', '" + DDLMes.SelectedValue + "', '" + dbRow["CONSUMO_TOTAL"].ToString() + "', '" + dbRow["CONSUMO_PROMEDIO"].ToString() + "', '" + dbRow["STOCK_TOTAL"].ToString() + "', '" + dbRow["STOCK_MES"].ToString() + "', '" + dbRow["SSO"].ToString() + "', '" + dbRow["MEDICAMENTO/INSUMO"].ToString() + "')\"><i class='fa fa-fw fa-exchange'></i></button></td>";

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
        //LitDetAtenciones.Text = htmlDetAtenciones;
    }

    public DataTable CargaTabGetDetCodSISMED(DateTime vfecha)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();

        try
        {
            string qSql = "exec SISMED.dbo.JVARDispoSALMACodSISMED @FechaDisp, @CodSismed ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@FechaDisp", vfecha);
            cmd.Parameters.AddWithValue("@CodSismed", "");

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            dt = objdataset.Tables[0];
        }
        catch (Exception ex)
        {
            //dt.Rows.Add = ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }

        return dt;
    }

    public string CargaTabGetDetCodSISMED_STR(DateTime vfecha, string vCod)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();
        string vReturn = "";

        try
        {
            string qSql = "exec SISMED.dbo.JVARDispoSALMACodSISMED @FechaDisp, @CodSismed ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@FechaDisp", vfecha);
            cmd.Parameters.AddWithValue("@CodSismed", vCod);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            vReturn = objdataset.Tables[0].Rows[0]["Promedio"].ToString();
        }
        catch (Exception ex)
        {
            vReturn += "0"; //ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }

        return vReturn;
    }

    public DataTable GetDTCatalogoMedFarmacia()
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        DataTable dt = new DataTable();
        
        try
        {
            string qSql = "select Cod_SISMEDHomologo, max(Producto) Producto, sum(StockActual) StockActual, " +
                "max(PrecioUnit) PrecioUnit, max(PrecioC) PrecioC, max(PrecioArfsis) PrecioArfsis " +
                "from IROf.dbo.ProductosFarmacia group by Cod_SISMEDHomologo  ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            adapter.Fill(dt);
            conSAP00i.Close();
        }
        catch (Exception ex)
        {        }

        return dt;
    }

    public string getPrecioSISMED(string vCod, string vCampo)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string vReturn = "";
        try
        {
            string qSql = "select top 1 * from IROf.dbo.ProductosFarmacia " +
                "where Cod_SISMEDHomologo = @CodSismed order by StockActual desc ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@CodSismed", vCod);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();

            vReturn = objdataset.Tables[0].Rows[0][vCampo].ToString();
        }
        catch (Exception ex)
        {
            vReturn += "0"; //ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }

        return vReturn;
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