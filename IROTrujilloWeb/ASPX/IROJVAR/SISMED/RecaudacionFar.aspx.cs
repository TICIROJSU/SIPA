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

public partial class ASPX_IROJVAR_SISMED_RecaudacionFar : System.Web.UI.Page
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
        string gHTML = "";
        try
        {
            string qSql = "select Emi.Mes, day(Emi.FechaEmision) Dia, Emi.FechaEmision, Emi.Total TotalEmi, isnull(Dev.Total, 0) TotalDev, Emi.Total - isnull(Dev.Total, 0) as Total, (Emi.Total - isnull(Dev.Total, 0)) / Emi.Total *100 PorTotal, isnull(Dev.Total, 0) / Emi.Total *100 PorcDev " +
                "from ( " +
                "SELECT MONTH(FechaEmision) as Mes, cast(FechaEmision as date) FechaEmision, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "FROM IROf.dbo.Documento WHERE (YEAR(FechaEmision) = " + vAnio + " and month(FechaEmision) = " + vMes + ")  and IdTipoDoc <80 and Estado <> 3 " +
                "group by MONTH(FechaEmision), cast(FechaEmision as date) " +
                ") Emi " +
                "full outer join ( " +
                "SELECT MONTH(FechaCreacion) as Mes, cast(FechaCreacion as date) as FechaDevol, Estado, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "FROM IROf.dbo.Documento WHERE (YEAR(FechaCreacion) = " + vAnio + " and month(FechaCreacion) = " + vMes + ") and IdTipoDoc <80 and Estado = 4 " +
                "group by MONTH(FechaCreacion), cast(FechaCreacion as date), Estado " +
                ") Dev" +
                " on (Emi.FechaEmision = Dev.FechaDevol) " +
                "order by 2 ; ";
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
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table class='table table-hover' style='text-align: right; font-size: 14px; '>";
                //gDetAtenciones += "<caption>" + vServicio + " | </caption>";
                gHTML += "<tr>";
                gHTML += "<th class=''>N° </th>";
                gHTML += "<th class=''>Dia</th>";
                gHTML += "<th style='text-align: center;'>Total</th>";
                gHTML += "<th style='text-align: center;'>Emitido</th>";
                gHTML += "<th style='text-align: center;'>Devuelto</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vftot = ClassGlobal.formatoMillarDec(dbRow["Total"].ToString());
                    string vftotEmi = ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString());
                    string vftotDev = ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString());
                    string vfPorcTot = ClassGlobal.formatoMillarDec(dbRow["PorTotal"].ToString());
                    string vfPorcDev = ClassGlobal.formatoMillarDec(dbRow["PorcDev"].ToString());
                    gHTML += "<tr data-toggle='modal' data-target='#modalDetRecaudacion2' onclick=\"fDetRecaudacion2('" + vAnio + "', '" + vMes + "', '" + dbRow["Dia"].ToString() + "', '" + dbRow["TotalEmi"].ToString() + "')\">";
                    gHTML += "<td style='text-align: left;'>" + nroitem + "</td>";
                    gHTML += "<td style='text-align: left;'>" + dbRow["Dia"].ToString() + "</td>";
                    gHTML += "<td >" + vftot + " (" + vfPorcTot + "%)</td>";
                    gHTML += "<td >" + vftotEmi + "</td>";
                    gHTML += "<td >" + vftotDev + " (" + vfPorcDev + "%)</td>";
                    gHTML += "</tr>" + Environment.NewLine;
                }
                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                gHTML += "||sep||";
                gHTML += " | Total: " + ClassGlobal.formatoMillarDec(dtDatoDetAt.Compute("Sum(Total)", String.Empty).ToString());
                gHTML += " | Emitido: " + ClassGlobal.formatoMillarDec(dtDatoDetAt.Compute("Sum(TotalEmi)", String.Empty).ToString());
                gHTML += " | Devuelto: " + ClassGlobal.formatoMillarDec(dtDatoDetAt.Compute("Sum(TotalDev)", String.Empty).ToString());
                gHTML += " | Promedio: " + ClassGlobal.formatoMillarDec(dtDatoDetAt.Compute("Avg(Total)", String.Empty).ToString());
                //ClassGlobal.formatoMillarDec(dtDato.Compute("SUM(Total)", String.Empty).ToString());
            }
        }
        catch (Exception ex)
        {
            gHTML += "<tr><td>" + ex.Message.ToString() + "<td><tr>";
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetDetRecaudacion2(string vAnio, string vMes, string vDia)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select isnull(Emi.Mes, '0') Mes, isnull(day(Emi.FechaEmision), '0') Dia, isnull(Emi.FechaEmision, '01/01/1901') FechaEmision, isnull(Emi.idCliente, '0') idCliente, isnull(Emi.Total, '0') TotalEmi, isnull(Dev.Total, 0) TotalDev, ISNULL(Emi.Total - isnull(Dev.Total, 0), '0') as Total, ISNULL((Emi.Total - isnull(Dev.Total, 0)) / Emi.Total * 100, '0') PorTotal, ISNULL(isnull(Dev.Total, 0) / Emi.Total * 100, '0') PorcDev " +
                "from( " +
                "    SELECT MONTH(FechaEmision) as Mes, cast(FechaEmision as date) FechaEmision, idCliente, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "    FROM IROf.dbo.Documento WHERE(YEAR(FechaEmision) = " + vAnio + " and month(FechaEmision) = " + vMes + " and day(FechaEmision) = " + vDia + ")  and IdTipoDoc < 80 and Estado <> 3 " +
                "    group by MONTH(FechaEmision), cast(FechaEmision as date), idCliente " +
                "    ) Emi " +
                "    full outer join( " +
                "    SELECT MONTH(FechaCreacion) as Mes, cast(FechaCreacion as date) as FechaDevol, Estado, idCliente, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "    FROM IROf.dbo.Documento WHERE(YEAR(FechaCreacion) = " + vAnio + " and month(FechaCreacion) = " + vMes + " and day(FechaEmision) = " + vDia + ") and IdTipoDoc<80 and Estado = 4 " +
                "    group by MONTH(FechaCreacion), cast(FechaCreacion as date), Estado, idCliente " +
                ") Dev " +
                "on(Emi.FechaEmision = Dev.FechaDevol) " +
                "order by 2; ";
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
                gDetAtenciones += "<th class=''>_id Cliente_</th>";
                gDetAtenciones += "<th class=''></th>";
                gDetAtenciones += "<th class=''>Registro</th>";
                gDetAtenciones += "<th class=''>Emitido</th>";
                gDetAtenciones += "<th class=''>Devuelto</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr data-toggle='modal' data-target='#modalDetRecaudacion3' onclick=\"fDetRecaudacion3('" + vAnio + "', '" + vMes + "', '" + vDia + "', '" + dbRow["idCliente"].ToString() + "')\">";
                    gDetAtenciones += "<td style='text-align: left;'>" + nroitem + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["idCliente"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + "" + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["FechaEmision"].ToString().Substring(0, 10) + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["TotalEmi"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["TotalDev"].ToString() + "</td>";
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
    public static string GetDetRecaudacion3(string vAnio, string vMes, string vDia, string vCliente)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetAtenciones = "";
        try
        {
            string qSql = "select Doc.serie + '-' + Doc.NroDocumento as NroDoc, " +
                "CliN.dni + '-' + CliN.Apellidos + '-' + CliN.Nombre as Cliente, Prod.Cod_SISMED, Prod.Producto, DocDet.Monto, Doc.Total " +
                "FROM IROf.dbo.Documento Doc " +
                "left join IROf.dbo.DocumentoDetalleProd DocDet on Doc.serie = DocDet.Serie and Doc.NroDocumento = DocDet.NroDocumento " +
                "left join IROf.dbo.Natural CliN on Doc.idCliente = CliN.idCliente " +
                "left join IROf.dbo.ProductosFarmacia Prod on DocDet.Codigo = Prod.CodigoPF " +
                "WHERE(YEAR(Doc.FechaEmision) = " + vAnio + " and month(Doc.FechaEmision) = " + vMes + " and day(Doc.FechaEmision) = " + vDia + ") and Doc.idCliente like '" + vCliente + "%' and Doc.IdTipoDoc < 80 and Doc.Estado <> 3 " +
                "order by 1 ; ";

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
                gDetAtenciones += "<th class=''>N° Doc</th>";
                gDetAtenciones += "<th class=''>Cliente</th>";
                gDetAtenciones += "<th class=''>CodMED</th>";
                gDetAtenciones += "<th class=''>Producto</th>";
                gDetAtenciones += "<th class=''>Monto</th>";
                gDetAtenciones += "<th class=''>Total</th>";
                gDetAtenciones += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetAtenciones += "<tr>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["NroDoc"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Cliente"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Cod_SISMED"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Producto"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Monto"].ToString() + "</td>";
                    gDetAtenciones += "<td style='text-align: left;'>" + dbRow["Total"].ToString() + "</td>";
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
            string qSql = "select Mes, Sum(TotalEmi) TotalEmi, Sum(TotalDev) TotalDev, Sum(Total) Total " +
                "from " +
                "( select Emi.Mes, Emi.FechaEmision, Emi.Total TotalEmi, isnull(Dev.Total, 0) TotalDev, Emi.Total - isnull(Dev.Total, 0) as Total " +
                "from " +
                "( SELECT MONTH(FechaEmision) as Mes, cast(FechaEmision as date) FechaEmision, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "FROM IROf.dbo.Documento WHERE (YEAR(FechaEmision) = "+vAnio+") and IdTipoDoc <80 and Estado <> 3 " +
                "group by MONTH(FechaEmision), cast(FechaEmision as date) ) Emi " +
                "full outer join " +
                "( SELECT MONTH(FechaCreacion) as Mes, cast(FechaCreacion as date) as FechaDevol, Estado, sum(Subtotal) Subtotal, sum(IGV) IGV, sum(Total) Total " +
                "FROM IROf.dbo.Documento WHERE (YEAR(FechaCreacion) = " + vAnio + ") and IdTipoDoc <80 and Estado = 4 " +
                "group by MONTH(FechaCreacion), cast(FechaCreacion as date), Estado ) Dev " +
                "on (Emi.FechaEmision = Dev.FechaDevol) " +
                ") Tab " +
                "group by Mes " +
                "order by 1 ;";

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

            txtTotMonto.Text = ClassGlobal.formatoMillarDec(dtDato.Compute("SUM(Total)", String.Empty).ToString());

            foreach (DataRow dbRow in dtDato.Rows)
            {
                string vMesDB = dbRow["Mes"].ToString();

                switch (vMesDB)
                {
                    case "1":
                        L01 = "<div style='display:none'>" + /*qSql +*/ "</div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "2":
                        L02 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "3":
                        L03 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "4":
                        L04 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "5":
                        L05 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "6":
                        L06 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "7":
                        L07 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "8":
                        L08 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "9":
                        L09 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "10":
                        L10 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "11":
                        L11 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
                        break;
                    case "12":
                        L12 = "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "', 'Total')\"><div class='column'>   <div class='col-md-7' > Total </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["Total"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "', 'TotalEmi')\"><div class='column'>   <div class='col-md-7' > Emitidas </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalEmi"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>" +
                            "<div class='row' data-toggle='modal' data-target='#modalDetRecaudacion' onclick=\"fDetRecaudacion('" + vMesDB + "', '" + vAnio + "', '" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "', 'TotalDev')\"><div class='column'>   <div class='col-md-7' > Devolucion </div></div><div class='column' ><div class='col-md-5 pull-right-container'> <input type='text' class='form-control input-sm' style='text-align:right' value='" + ClassGlobal.formatoMillarDec(dbRow["TotalDev"].ToString()) + "'  disabled='disabled' /> </div></div>  </div>";
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