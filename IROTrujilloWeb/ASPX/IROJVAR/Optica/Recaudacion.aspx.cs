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

public partial class ASPX_IROJVAR_Optica_Recaudacion : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vyear = DateTime.Now.Year;
            DDLAnio.Text = vyear.ToString();
            //int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            CargaInicial();
        }
    }

    [WebMethod]
    public static string GetDetRecaudacion(string vAnio, string vMes, string vServicio)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select YEAR(FechaEmisionComprobante) AS Anio, MONTH(FechaEmisionComprobante) as Mes, day(FechaEmisionComprobante) as Dia, SUM(Recadudado) as Recaudado, Sum(Total - Recadudado) as PorCobrar " +
                "from " +
                "( " +
                "SELECT (tipoDocumento.descripcion+' '+V.DocumentoPaciente) as NumDocPac, V.DescripcionPaciente AS Nombres, convert(date,ot.FechaRegistro) as FechaRegistroOrden, ot.numeroOrdenTrabajo,convert(date,V.FechaEmisionComprobante) as FechaEmisionComprobante, V.FechaDePago, " +
                "V.FechaRegistro, v.Estado, v.serie, v.NumeroDocumento as numeroComprobante, V.SubtotalTemp as Subtotalt, V.IgvTemp as Igvt, V.Acuenta, V.Saldo, V.Descuento, V.Total,mae.Descripcion,v.Igv,v.SubTotal, (case when (V.NumeroDocumento is null) then V.ACuenta else V.Total end) as Recadudado " +
                "FROM IRO_BD_OPTICADESK.dbo.VENTA V " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.Maestro AS tipoDocumento ON V.ValorTipoDocumentoPaciente = tipoDocumento.Identificador AND tipoDocumento.Codigo BETWEEN 17 and 22 " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.OrdenTrabajo AS ot ON v.codigoOrdenTrabajo = ot.codigo " +
                "inner join IRO_BD_OPTICADESK.dbo.Maestro mae on v.Estado = mae.Codigo " +
                "WHERE v.IsActivo = 1 AND year(v.FechaEmisionComprobante) = '" + vAnio + "' and month(v.FechaEmisionComprobante) = '" + vMes + "' AND (v.Estado = 50 OR v.Estado = 64) " +
                ")  tab " +
                "group by YEAR(FechaEmisionComprobante), MONTH(FechaEmisionComprobante), day(FechaEmisionComprobante) " +
                "order by day(FechaEmisionComprobante) ";
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
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                //gDetAtenciones += "<caption>" + vServicio + " | </caption>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>Dia</th>";
                gDetAtenciones += "<th class=''>Recaudado</th>";
                gDetAtenciones += "<th class=''>Por Cobrar</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr data-toggle='modal' data-target='#modalDetRecaudacion2' onclick=\"fDetRecaudacion2('"+vAnio+ "', '" + vMes + "', '" + dbRow["Dia"].ToString() + "', '" + dbRow["Recaudado"].ToString() + "')\">";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Dia"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["Recaudado"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + dbRow["PorCobrar"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }
                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += "<tr><td>" + ex.Message.ToString() + "<td><tr>";
        }
        return gDetAtenciones;
    }
    [WebMethod]
    public static string GetDetRecaudacion2(string vAnio, string vMes, string vDia)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select YEAR(FechaEmisionComprobante) AS Anio, MONTH(FechaEmisionComprobante) as Mes, " +
                "day(FechaEmisionComprobante) as Dia, " +
                "NumDocPac, Nombres, cast(FechaRegistro as time) as HoraReg, " +
                "SUM(Recadudado) as Recaudado, Sum(Total - Recadudado) as PorCobrar " +
                "from " +
                "( " +
                "SELECT (tipoDocumento.descripcion+' '+V.DocumentoPaciente) as NumDocPac, V.DescripcionPaciente AS Nombres, convert(date,ot.FechaRegistro) as FechaRegistroOrden, ot.numeroOrdenTrabajo,convert(date,V.FechaEmisionComprobante) as FechaEmisionComprobante, V.FechaDePago, V.FechaRegistro, v.Estado, v.serie, v.NumeroDocumento as numeroComprobante, V.SubtotalTemp as Subtotalt, V.IgvTemp as Igvt, V.Acuenta, V.Saldo, V.Descuento, V.Total,mae.Descripcion,v.Igv,v.SubTotal, (case when (V.NumeroDocumento is null) then V.ACuenta else V.Total end) as Recadudado " +
                "FROM IRO_BD_OPTICADESK.dbo.VENTA V " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.Maestro AS tipoDocumento ON V.ValorTipoDocumentoPaciente = tipoDocumento.Identificador AND tipoDocumento.Codigo BETWEEN 17 and 22 " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.OrdenTrabajo AS ot ON v.codigoOrdenTrabajo = ot.codigo " +
                "inner join IRO_BD_OPTICADESK.dbo.Maestro mae on v.Estado = mae.Codigo " +
                "WHERE v.IsActivo = 1 AND year(v.FechaEmisionComprobante) = '" + vAnio + "' and month(v.FechaEmisionComprobante) = '" + vMes + "' and day(v.FechaEmisionComprobante) = '" + vDia + "' AND (v.Estado = 50 OR v.Estado = 64) " +
                ")  tab " +
                "group by YEAR(FechaEmisionComprobante), MONTH(FechaEmisionComprobante), day(FechaEmisionComprobante), NumDocPac, Nombres, FechaRegistro " +
                "order by cast(FechaRegistro as time) ";
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
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                //gDetAtenciones += "<caption>" + vServicio + " | </caption>";
                gDetAtenciones += "<tr>";
                gDetAtenciones += "<th class=''>N° </th>";
                gDetAtenciones += "<th class=''>_Documento_</th>";
                gDetAtenciones += "<th class=''>Nombres</th>";
                gDetAtenciones += "<th class=''>Hora Registro</th>";
                gDetAtenciones += "<th class=''>Recaudado</th>";
                gDetAtenciones += "<th class=''>Por Cobrar</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["NumDocPac"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Nombres"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["HoraReg"].ToString().Substring(0,5) + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Recaudado"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["PorCobrar"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }
                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gDetAtenciones += "<tr><td>" + ex.Message.ToString() + "<td><tr>";
        }
        return gDetAtenciones;
    }

    public void CargaInicial()
    {
        string vAnio = DDLAnio.SelectedValue;
        //int vAnio = DateTime.Now.Year;
        try
        {
            //con.Open();
            string qSql = "select YEAR(FechaEmisionComprobante) AS Anio, MONTH(FechaEmisionComprobante) as Mes, SUM(Recadudado) as Recaudado, Sum(Total - Recadudado) as PorCobrar " +
                "from ( " +
                "SELECT (tipoDocumento.descripcion+' '+V.DocumentoPaciente) as NumDocPac, V.DescripcionPaciente AS Nombres, convert(date,ot.FechaRegistro) as FechaRegistroOrden, ot.numeroOrdenTrabajo,convert(date,V.FechaEmisionComprobante) as FechaEmisionComprobante, " +
                "V.FechaDePago, V.FechaRegistro, v.Estado, v.serie, v.NumeroDocumento as numeroComprobante, V.SubtotalTemp as Subtotalt, V.IgvTemp as Igvt, V.Acuenta, V.Saldo, V.Descuento, V.Total, mae.Descripcion, v.Igv, v.SubTotal, (case when (V.NumeroDocumento is null) then V.ACuenta else V.Total end) as Recadudado " +
                "FROM IRO_BD_OPTICADESK.dbo.VENTA V " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.Maestro AS tipoDocumento ON V.ValorTipoDocumentoPaciente = tipoDocumento.Identificador AND tipoDocumento.Codigo BETWEEN 17 and 22 " +
                "INNER JOIN IRO_BD_OPTICADESK.dbo.OrdenTrabajo AS ot ON v.codigoOrdenTrabajo = ot.codigo " +
                "inner join IRO_BD_OPTICADESK.dbo.Maestro mae on v.Estado = mae.Codigo " +
                "WHERE v.IsActivo = 1 AND year(v.FechaEmisionComprobante) = '" + vAnio + "' AND (v.Estado = 50 OR v.Estado = 64) " +
                ")  tab " +
                "group by YEAR(FechaEmisionComprobante), MONTH(FechaEmisionComprobante) " +
                "order by MONTH(FechaEmisionComprobante)";

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
            //CargaTabla(dtDato);
            string L01 = "", L02 = "", L03 = "", L04 = "", L05 = "", L06 = "";
            string L07 = "", L08 = "", L09 = "", L10 = "", L11 = "", L12 = "";
            double vTotMonto = 0;
            double vPorCobrar = 0;

            foreach (DataRow dbRow in dtDato.Rows)
            {
                string vMesDB = dbRow["Mes"].ToString();

                vTotMonto = vTotMonto + Convert.ToDouble(dbRow["Recaudado"]);
                vPorCobrar = vPorCobrar + Convert.ToDouble(dbRow["PorCobrar"]);

                switch (vMesDB)
                {
                    case "1":
                        L01 = "<div style='display:none'>"+qSql+"</div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "2":
                        L02 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "3":
                        L03 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "4":
                        L04 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "5":
                        L05 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "6":
                        L06 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "7":
                        L07 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "8":
                        L08 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "9":
                        L09 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "10":
                        L10 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "11":
                        L11 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "12":
                        L12 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "', 'Recaudado')\"><div class='column'>   <div class='col-md-7' > Recaudado </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Recaudado"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "', 'PorCobrar')\"><div class='column'>   <div class='col-md-7' > PorCobrar </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["PorCobrar"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    default:
                        break;
                }


            }

            if (L01 == "") { L01 = "0"; }
            if (L02 == "") { L02 = "0"; }
            if (L03 == "") { L03 = "0"; }
            if (L04 == "") { L04 = "0"; }
            if (L05 == "") { L05 = "0"; }
            if (L06 == "") { L06 = "0"; }
            if (L07 == "") { L07 = "0"; }
            if (L08 == "") { L08 = "0"; }
            if (L09 == "") { L09 = "0"; }
            if (L10 == "") { L10 = "0"; }
            if (L11 == "") { L11 = "0"; }
            if (L12 == "") { L12 = "0"; }

            txtTotMonto.Text = ClassGlobal.formatoMillarDec(vTotMonto.ToString());
            txtPorCobrar.Text = ClassGlobal.formatoMillarDec(vPorCobrar.ToString());

            Lit01.Text = L01; Lit02.Text = L02; Lit03.Text = L03; Lit04.Text = L04;
            Lit05.Text = L05; Lit06.Text = L06; Lit07.Text = L07; Lit08.Text = L08;
            Lit09.Text = L09; Lit10.Text = L10; Lit11.Text = L11; Lit12.Text = L12;

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        //CargaTablaDT();
        //CargaMontoMeses();
        CargaInicial();
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