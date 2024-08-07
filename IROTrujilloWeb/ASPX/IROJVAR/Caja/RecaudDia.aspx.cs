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


public partial class ASPX_IROJVAR_Caja_RecaudDia : System.Web.UI.Page
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
        }
        CargaMontoMeses();
    }

	[WebMethod]
	public static string GetDetRecaudacion(string vAnio, string vMes, string vServicio)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHTML = "";
        try
        {
            string qSql = "SELECT convert(date,FechaEmision) fecha, sum(Documento.Total) total, " +
                "sum(case when cast(FechaEmision as time) < '12:00' then Documento.Total else 0 end ) Manana, " +
                "sum(case when cast(FechaEmision as time) >= '12:00' then Documento.Total else 0 end ) Tarde  " +
                "FROM IRO.dbo.Documento INNER JOIN " +
                "IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie INNER JOIN " +
                "IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo INNER JOIN " +
                "IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                "WHERE Documento.IdTipoDoc<80 and (year(Documento.FechaEmision) = '" + vAnio + "' and month(Documento.FechaEmision) = '" + vMes + "') and Tipo_Servicio.Nombre like '%" + vServicio + "%' " +
                "group by convert(date,FechaEmision) order by fecha ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];
            double vt_totProm = Convert.ToDouble(dtDatoDetAt.Compute("Avg(total)", String.Empty));
            double vt_totMon = Convert.ToDouble(dtDatoDetAt.Compute("SUM(total)", String.Empty));
            double vt_totMan = Convert.ToDouble(dtDatoDetAt.Compute("SUM(Manana)", String.Empty));
            double vt_totTar = Convert.ToDouble(dtDatoDetAt.Compute("SUM(Tarde)", String.Empty));
            //HttpContext.Current.Session["SesTotProm"] = vt_totProm.ToString();

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetHTML += "<tr>";
                gDetHTML += "<td class='' align='center'><b>Dia </b></th>";
                gDetHTML += "<td class=''><b>Monto</b></th>";
                gDetHTML += "<td class=''><b>Mañana</b></th>";
                gDetHTML += "<td class=''><b>Tarde</b></th>";
                gDetHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                string vtmpServ = "data-target='#modalDetRecaudacion2' onclick=\"fDetRecaudacion2";
                if (vServicio != "")
                { vtmpServ = "data-target='#modalDetRecaudacion22' onclick=\"fDetRecaudacion2_2"; }

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    double vt_subMon = Convert.ToDouble(dbRow["total"].ToString());
                    double vt_subMan = Convert.ToDouble(dbRow["Manana"].ToString());
                    double vt_subTar = Convert.ToDouble(dbRow["Tarde"].ToString());

                    DateTime vFecha = (Convert.ToDateTime(dbRow["fecha"].ToString()));
                    gDetHTML += "<tr  data-toggle='modal' " + vtmpServ + "('" + vFecha.Day.ToString() + "', '" + vFecha.Month.ToString() + "', '" + vFecha.Year.ToString() + "', '" + vServicio + "', '" + dbRow["total"].ToString() + "')\">";
                    //gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetHTML += "<td class='' style='text-align: center;'>" + dbRow["fecha"].ToString().Substring(0, 2) + "</td>";
                    gDetHTML += "<td class='' >" + dbRow["total"].ToString() + "</td>";
                    gDetHTML += "<td class='' >" + ClassGlobal.MontoPorc(vt_subMon, vt_subMan) + "</td>";
                    gDetHTML += "<td class='' >" + ClassGlobal.MontoPorc(vt_subMon, vt_subTar) + "</td>";
                    gDetHTML += "</tr>" + Environment.NewLine;
                }

                gDetHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gDetHTML += "||sep||";
                gDetHTML += ClassGlobal.formatoMillarDec(vt_totProm.ToString());
                gDetHTML += "||sep||";
                gDetHTML += ClassGlobal.MontoPorc(vt_totMon, vt_totMan);
                gDetHTML += "||sep||";
                gDetHTML += ClassGlobal.MontoPorc(vt_totMon, vt_totTar);
            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetHTML += "-" + "-" + ex.Message.ToString();
        }
        return gDetHTML;
    }

    [WebMethod]
    public static string GetDetRecaudacion2(string vDia, string vMes, string vAnio)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "SELECT convert(date, FechaEmision) fecha,  Tipo_Servicio.Nombre AS TipoServi, sum( Documento.Total) total, sum(case when cast(FechaEmision as time) < '12:00' then Documento.Total else 0 end ) Manana, sum(case when cast(FechaEmision as time) >= '12:00' then Documento.Total else 0 end ) Tarde " +
                " FROM IRO.dbo.Documento INNER JOIN " +
                " IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie INNER JOIN " +
                " IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo INNER JOIN " +
                " IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                "WHERE Documento.IdTipoDoc<80 and (YEAR(Documento.FechaEmision) = '" + vAnio + "' AND MONTH(Documento.FechaEmision) = '" + vMes + "' AND DAY(Documento.FechaEmision) = '" + vDia + "' ) " +
                "group by   convert(date,FechaEmision),  Tipo_Servicio.Nombre " +
                "order by fecha, TipoServi  ";
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
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr><th class=''></th>";
                gDetAtenciones += "<th class=''>Servicio </th>";
                gDetAtenciones += "<th class=''>Monto</th>";
                gDetAtenciones += "<th class=''>Mañana</th>";
                gDetAtenciones += "<th class=''>Tarde</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["TipoServi"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' >" + dbRow["total"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' >" + dbRow["Manana"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' >" + dbRow["Tarde"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    [WebMethod]
    public static string GetDetRecaudacion2_2(string vDia, string vMes, string vAnio, string vServicio)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "SELECT     SERVICIOS.servicio AS ServiC, count( Documento.Total) cant, sum( Documento.Total) total " +
                "FROM IRO.dbo.Documento INNER JOIN IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie " +
                "INNER JOIN IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo " +
                "INNER JOIN IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                "WHERE Documento.IdTipoDoc<80 and (YEAR(Documento.FechaEmision) = '" + vAnio + "' AND MONTH(Documento.FechaEmision) = '" + vMes + "' AND DAY(Documento.FechaEmision) = '" + vDia + "' AND Tipo_Servicio.Nombre='" + vServicio + "') " +
                "group by   SERVICIOS.servicio";
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
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr><th class=''></th>";
                gDetAtenciones += "<th class=''>Servicio </th>";
                gDetAtenciones += "<th class=''>Cant</th>";
                gDetAtenciones += "<th class=''>Monto</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr data-toggle='modal' data-target='#modalDetRecClie' onclick=\"fDetRecClie('" + vDia + "', '" + vMes + "', '" + vAnio + "', '" + vServicio + "', '" + dbRow["ServiC"].ToString() + "', '" + dbRow["total"].ToString() + "')\">";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["ServiC"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["cant"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' >" + dbRow["total"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }
    [WebMethod]
    public static string GetDetRecClie(string vDia, string vMes, string vAnio, string vServicio, string vServicio2)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select Documento.idUsuario, Documento.serie + '-' + Documento.NroDocumento as NroDocumento, Documento.Estado, Documento.FechaEmision, Documento.idCliente, Natural.Apellidos + ' ' + Natural.Nombre as ClienteNom, Natural.fecha_nac, Documento.Subtotal, Documento.TotalOperacionesInafectas, Documento.MontoDesc, Documento.Total, SERVICIOS.servicio, Tipo_Servicio.Nombre  " +
                "from IRO.dbo.Documento inner join IRO.dbo.Natural On Documento.idCliente = Natural.idCliente   " +
                "INNER JOIN IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie " +
                "INNER JOIN IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo " +
                "INNER JOIN IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                "where Documento.IdTipoDoc<80 and YEAR(Documento.FechaEmision) = '" + vAnio + "' AND MONTH(Documento.FechaEmision) = '" + vMes + "' AND DAY(Documento.FechaEmision) = '" + vDia + "' AND Tipo_Servicio.Nombre='" + vServicio + "' and SERVICIOS.servicio='" + vServicio2 + "'; " +
                "" +
                "select Documento.idUsuario, Documento.serie + '-' + Documento.NroDocumento as NroDocumento, Documento.Estado, Documento.FechaEmision, Documento.idCliente, Natural.Apellidos + ' ' + Natural.Nombre as ClienteNom, Natural.fecha_nac, Documento.Subtotal, Documento.TotalOperacionesInafectas, Documento.MontoDesc, Documento.Total, SERVICIOS.servicio, Tipo_Servicio.Nombre  " +
                "from IRO.dbo.Documento inner join IRO.dbo.Natural On Documento.idCliente = Natural.idCliente   " +
                "INNER JOIN IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie " +
                "INNER JOIN IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo " +
                "INNER JOIN IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                "where Documento.IdTipoDoc<80 and YEAR(Documento.FechaEmision) = '" + vAnio + "' AND MONTH(Documento.FechaEmision) = '" + vMes + "' AND Tipo_Servicio.Nombre='" + vServicio + "' and SERVICIOS.servicio='" + vServicio2 + "' " +
                "order by Documento.FechaEmision";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];
            ClassGlobal.varDTGen = objdataset.Tables[1];

            //gvDetRecClie.DataSource = dtDato;
            //gvDetRecClie.DataBind();

            //gDetAtenciones = CargaTablaDetAtenciones(dtDatoDetAt, vanio, vmes, vdia, turno, vplaza);
            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetAtenciones += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetAtenciones += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetAtenciones += "<tr><th><a class='btn btn-default' title='Max.: 3,000 Registros' href='RecauDiaDescarga.aspx' target='_blank'>" +
                    "<i class='fa fa-download'></i></a></th>";
                gDetAtenciones += "<th class=''>Documento </th>";
                gDetAtenciones += "<th class=''>Hora</th>";
                gDetAtenciones += "<th class=''>Cliente</th>";
                gDetAtenciones += "<th class=''>Total</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["NroDocumento"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["FechaEmision"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' style='text-align: center;'>" + dbRow["ClienteNom"].ToString() + "</td>";
                    gDetAtenciones += "<td class='' >" + dbRow["Total"].ToString() + "</td>";
                    gDetAtenciones += "</tr>" + Environment.NewLine;
                }

                gDetAtenciones += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
            //gDetAtenciones - FINAL
        }
        catch (Exception ex)
        {
            gDetAtenciones += "-" + "-" + ex.Message.ToString();
        }
        return gDetAtenciones;
    }

    public void CargaMontoMeses()
    {
        string vAnio = DDLAnio.SelectedValue;
        //int vAnio = DateTime.Now.Year;
        try
        {
            //con.Open();
            string qSql = "SELECT    month(FechaEmision) Mes, Tipo_Servicio.Nombre, sum( Documento.Total) total " +
                "FROM            IRO.dbo.Documento INNER JOIN " +
                "    IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie INNER JOIN " +
                "    IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo INNER JOIN " +
                "    IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
            //"WHERE        (year(Documento.FechaEmision) = '" + vAnio + "') " +
            //"group by   month(FechaEmision) ";
            "WHERE      Documento.IdTipoDoc<80 and   (year(Documento.FechaEmision) = '" + vAnio + "') " +
            "group by  rollup( month(FechaEmision),  Tipo_Servicio.Nombre ) " +
            "order by month(FechaEmision), Tipo_Servicio.Nombre; ";

            qSql += "Select Nombre, Sum(Case when Mes=1 then Total End) Enero, " +
                 "Sum(Case when Mes=2 then Total End) Febrero, Sum(Case when Mes=3 then Total End) Marzo, " +
                 "Sum(Case when Mes=4 then Total End) Abril, Sum(Case when Mes=5 then Total End) Mayo, " +
                 "Sum(Case when Mes=6 then Total End) Junio, Sum(Case when Mes=7 then Total End) Julio, " +
                 "Sum(Case when Mes=8 then Total End) Agosto, Sum(Case when Mes=9 then Total End) Setiembre, " +
                 "Sum(Case when Mes=10 then Total End) Octubre, Sum(Case when Mes=11 then Total End) Noviembre, " +
                 "Sum(Case when Mes=12 then Total End) Diciembre, Sum(Case when Mes is null then Total End) Total " +
                 "from ( " +
                 "SELECT month(FechaEmision) Mes, isnull(Tipo_Servicio.Nombre, 'zTotal Mes') Nombre, sum( Documento.Total) total " +
                 " FROM IRO.dbo.Documento INNER JOIN " +
                 " IRO.dbo.DocumentoDetalle ON Documento.NroDocumento = DocumentoDetalle.NroDocumento AND Documento.serie = DocumentoDetalle.Serie INNER JOIN " +
                 " IRO.dbo.Servicios ON DocumentoDetalle.IdDepartamento = Servicios.IdDepartamento AND DocumentoDetalle.IdTipoServicio = Servicios.IdTipoServicio AND DocumentoDetalle.Codigo = Servicios.codigo INNER JOIN " +
                 " IRO.dbo.Tipo_Servicio ON Servicios.IdDepartamento = Tipo_Servicio.IdDepartamento AND Servicios.IdTipoServicio = Tipo_Servicio.IdTipoServicio " +
                 "WHERE Documento.IdTipoDoc<80 and (year(Documento.FechaEmision) = '2022') " +
                 "group by rollup( month(FechaEmision), Tipo_Servicio.Nombre ) " +
                 ") Tab1 " +
                 "group by Nombre; ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = objdataset.Tables[1];
            GVtable.DataBind();

            string L01 = "", L02 = "", L03 = "", L04 = "", L05 = "", L06 = "";
            string L07 = "", L08 = "", L09 = "", L10 = "", L11 = "", L12 = "";
            double vTotMonto = 0;
            double vConsGen = 0, vEmer = 0, vLab = 0, vProcEsp = 0, vRefr = 0, vSOP = 0, vTramDoc = 0, vOtroSer = 0;
            string vGConsGen = "", vGEmer = "", vGLab = "", vGProcEsp = "", vGRefr = "", vGSOP = "", vGTramDoc = "", vGOtroSer = "";

            foreach (DataRow dbRow in dtDato.Rows)
            {
                string vSubTitle = "", vSubTitleDB = "", vMesDB = "";
                vMesDB = dbRow["Mes"].ToString();

                if (dbRow["Nombre"] != DBNull.Value) { vSubTitle = dbRow["Nombre"].ToString() + ": "; vSubTitleDB = dbRow["Nombre"].ToString(); }
                else if (dbRow["Mes"] != DBNull.Value)
                {
                    vSubTitle = "<b>TOTAL: </b> <div style='display:none'>"+qSql.Substring(1,1)+"</div>"; vTotMonto = vTotMonto + Convert.ToDouble(dbRow["total"]);
                    //mdrecTitle1.InnerText = "Mes: " + vMesDB + " | " + vSubTitleDB + " | Total: " + ClassGlobal.formatoMillarDec(dbRow["total"].ToString());
                }

                if (vSubTitle == "TRAMITES DOCUMUNTARIOS: ") { vSubTitle = "Tram. Documentarios: "; }

                switch (vMesDB)
                {
                    case "1":
                        L01 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "2":
                        L02 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "3":
                        L03 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "4":
                        L04 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "5":
                        L05 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "6":
                        L06 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "7":
                        L07 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "8":
                        L08 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "9":
                        L09 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "10":
                        L10 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "11":
                        L11 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "12":
                        L12 += "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vSubTitleDB + "', '" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "')\"><div class='column'>   <div class='col-md-7' > " + vSubTitle + " </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    default:
                        break;
                }

                switch (dbRow["Nombre"].ToString())
                {
                    case "Consulta Oftalmologica General":
                        vConsGen = vConsGen + Convert.ToDouble(dbRow["total"]);
                        vGConsGen += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "Emergencia":
                        vEmer = vEmer + Convert.ToDouble(dbRow["total"]);
                        vGEmer += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "Laboratorio":
                        vLab = vLab + Convert.ToDouble(dbRow["total"]);
                        vGLab += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "Procedimientos Especializados":
                        vProcEsp = vProcEsp + Convert.ToDouble(dbRow["total"]);
                        vGProcEsp += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "Refraccion":
                        vRefr = vRefr + Convert.ToDouble(dbRow["total"]);
                        vGRefr += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "SOP":
                        vSOP = vSOP + Convert.ToDouble(dbRow["total"]);
                        vGSOP += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "TRAMITES DOCUMUNTARIOS":
                        vTramDoc = vTramDoc + Convert.ToDouble(dbRow["total"]);
                        vGTramDoc += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
                        break;
                    case "OTROS SERVICIOS":
                        vOtroSer = vOtroSer + Convert.ToDouble(dbRow["total"]);
                        vGOtroSer += "{x: " + vMesDB + ", label: '" + ClassGlobal.MesNroToTexto(vMesDB) + " " + vAnio + "', y: " + dbRow["total"].ToString() + " },";
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

            Lit01.Text = L01; Lit02.Text = L02; Lit03.Text = L03; Lit04.Text = L04;
            Lit05.Text = L05; Lit06.Text = L06; Lit07.Text = L07; Lit08.Text = L08;
            Lit09.Text = L09; Lit10.Text = L10; Lit11.Text = L11; Lit12.Text = L12;

            txtTotMonto.Text = ClassGlobal.formatoMillarDec(vTotMonto.ToString());

            txtConsGen.Text = ClassGlobal.formatoMillarDec(vConsGen.ToString());
            txtEmer.Text = ClassGlobal.formatoMillarDec(vEmer.ToString());
            txtLab.Text = ClassGlobal.formatoMillarDec(vLab.ToString());
            txtProcEsp.Text = ClassGlobal.formatoMillarDec(vProcEsp.ToString());
            txtRefr.Text = ClassGlobal.formatoMillarDec(vRefr.ToString());
            txtSOP.Text = ClassGlobal.formatoMillarDec(vSOP.ToString());
            txtTramDoc.Text = ClassGlobal.formatoMillarDec(vTramDoc.ToString());
            txtOtroSer.Text = ClassGlobal.formatoMillarDec(vOtroSer.ToString());

			string vGraf1 = "<div id='chartContainer1' style='height: 500px; width: 100%;'></div> " +
				"<script type='text/javascript'> " +
				"window.onload = function () " +
				"{ var chart = new CanvasJS.Chart('chartContainer1', " +
				"{ animationEnabled: true, theme: 'light2', " +
				"title:{text: 'Reporte de Recaudacion Diaria'}, axisY:[{title: 'Soles S/'}], " +
				"toolTip:{shared:true}, " +
				"data: " +
				"[ " +
				"{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Cons. Oft. Gen", 14) + "', legendText: 'Consulta Oft. General', dataPoints: [" + vGConsGen + "]}, " +
                "{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Emergencia", 14) + "', legendText: 'Emergencia', dataPoints: [" + vGEmer + "]}, " +
                "{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Laboratorio", 14) + "', legendText: 'Laboratorio', dataPoints: [" + vGLab + "]}, " +
                "{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Proc. Espec.", 14) + "', legendText: 'Proc. Especializados', dataPoints: [" + vGProcEsp + "]}, " +
				"{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Refraccion", 14) + "', legendText: 'Refraccion', dataPoints: [" + vGRefr + "]}, " +
				"{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("SOP", 14) + "', legendText: 'SOP__', dataPoints: [" + vGSOP + "]}, " +
				"{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Tram. Documentario", 14) + "', legendText: 'TRAMITES DOCUMUNTARIOS', dataPoints: [" + vGTramDoc + "]}, " +
				"{type: 'line', showInLegend: true, name:'" + ClassGlobal.RellenaTxt("Otros Servicios", 14) + "', legendText: 'Otros Servicios', dataPoints: [" + vGOtroSer + "]}, " +
				"] " +
				"}); chart.render(); " +
				"} " +
				"</script>";

            LitGraf1.Text = vGraf1;

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();
            //CargaTabla(dtDato);
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
        CargaMontoMeses();
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