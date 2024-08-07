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

public partial class ASPX_IROJVAR_Optica_ProcVenta02Copia : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth;
            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            txtFechaEmi.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            txtFDesde.Text = txtFechaEmi.Text;
            txtFHasta.Text = txtFechaEmi.Text;

        }

    }

    [WebMethod]
    public static string GetNIngresoList(string vDesde, string vHasta)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from IROBDOptica.dbo.OrdenTrabajo where FechaRegistro between @vdesde and DATEADD(DAY,1,@vhasta);";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@vdesde", (Convert.ToDateTime(vDesde)).Date);
            cmd2.Parameters.AddWithValue("@vhasta", (Convert.ToDateTime(vHasta)).Date);

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
                gHTML += "<th class=''>O.Trabajo </th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>Cliente</th>";
                gHTML += "<th class=''>Monto</th>";
                gHTML += "<th class=''>Seleccionar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vcod, vfec, vpro, ctcli, vtrec, vndoc, vmon;
                    vcod = dbRow["CodigoOT"].ToString();
                    vfec = dbRow["FechaRegistro"].ToString();
                    vndoc = dbRow["DNIpac"].ToString();
                    vpro = dbRow["nombrepac"].ToString();
                    ctcli = dbRow["TipCliente"].ToString();
                    vtrec = dbRow["TipReceta"].ToString();
                    vmon = dbRow["montototal"].ToString();

                    string vd, vm, vy;
                    DateTime vfecDT = Convert.ToDateTime(vfec);
                    vd = vfecDT.Day.ToString();
                    vm = vfecDT.Month.ToString();
                    vy = vfecDT.Year.ToString();
                    string vfec2 = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');

                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vfec + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vpro + "</td>";
                    //gHTML += "<td style='text-align: left;' >" + vtdoc + "</td>";
                    //gHTML += "<td style='text-align: left;' >" + vndoc + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vmon + "</td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-info' onclick=\"gSelNIng('" + vndoc + "', '" + vpro + "', '" + vfec2 + "', '" + vcod + "', '" + vmon + "')\" data-dismiss='modal'> Seleccionar</div>" +
                        "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetNIngresoDet(string vcod)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from IROBDOptica.dbo.OrdenTrabajoDet OTD left join IROBDOptica.dbo.ProductosOptica PO on OTD.codigoprod = PO.Codigo where CodigoOT = @idming;";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@idming", vcod);
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    string vcodd, vpro, vmontura, vcant, vprec, vimp;
                    vcodd = dbRow["CodigoProd"].ToString();
                    vpro = dbRow["Nombre"].ToString();
                    vmontura = dbRow["codmontura"].ToString();
                    vcant = dbRow["Cantidad"].ToString();
                    vprec = dbRow["Preciov"].ToString();
                    vimp = dbRow["Importev"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcodd + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vpro + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vmontura + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vcant + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vprec + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vimp + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string SetGeneraIdMontura(string vCodProd, string vCodMontura)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO IROBDOptica.dbo.stkProductoDet (codigoprod, codmontura, stock) VALUES (@codigoprod, @codmontura, @stock) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@codigoprod", vCodProd);
            cmd.Parameters.AddWithValue("@codmontura", vCodMontura);
            cmd.Parameters.AddWithValue("@stock", 0);

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

    [WebMethod]
    public static string SetstkProductoDet(string IdProdDet, string CodProd, string CodMontura, string Cantidad)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "UPDATE IROBDOptica.dbo.stkProductoDet SET stock = stock + @stock " +
                "where idproddet=@idproddet and codigoprod=@codigoprod and codmontura=@codmontura;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@stock", Cantidad);
            cmd.Parameters.AddWithValue("@idproddet", IdProdDet);
            cmd.Parameters.AddWithValue("@codigoprod", CodProd);
            cmd.Parameters.AddWithValue("@codmontura", CodMontura);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Registro Correcto";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
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
    public static string SetstkProductoAlmacen(string idDestino, string CodProd, string IdProdDet, string Cantidad)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        try
        {
            string qSql = "INSERT INTO IROBDOptica.dbo.stkProductoAlmacen (idalmacen, codigoprod, idproddet, stock) values (@idalmacen, @codigoprod, @idproddet, @stock)";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@idalmacen", idDestino);
            cmd.Parameters.AddWithValue("@codigoprod", CodProd);
            cmd.Parameters.AddWithValue("@idproddet", IdProdDet);
            cmd.Parameters.AddWithValue("@stock", Cantidad);
            adapter.SelectCommand = cmd;

            conSAP00i.Open(); cmd.ExecuteScalar(); conSAP00i.Close();

            gDetHtml += "Registro Correcto";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string SetCompruebaMontura(string vCodProd, string vCodMontura)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from IROBDOptica.dbo.stkProductoDet where codigoprod = @codProd and codmontura = @codMont;";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@codProd", vCodProd);
            cmd2.Parameters.AddWithValue("@codMont", vCodMontura);

            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtOT = objdataset.Tables[0];

            if (dtOT.Rows.Count > 0)
            {
                gHTML = dtOT.Rows[0]["idproddet"].ToString();
            }
            else
            {
                gHTML = "0";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
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

    [WebMethod]
    public static string SetBtnGuardarDet(string idming, string CodigoProd, string idproddet, string codmontura, string Cantidad, string PrecioC, string ImporteC)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO IROBDOptica.dbo.MIngresoDet (idming, CodigoProd, idproddet, codmontura, Cantidad, PrecioC, ImporteC) VALUES (@idming, @CodigoProd, @idproddet, @codmontura, @Cantidad, @PrecioC, @ImporteC) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@idming", idming);
            cmd.Parameters.AddWithValue("@CodigoProd", CodigoProd);
            cmd.Parameters.AddWithValue("@idproddet", idproddet);
            cmd.Parameters.AddWithValue("@codmontura", codmontura);
            cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
            cmd.Parameters.AddWithValue("@PrecioC", PrecioC);
            cmd.Parameters.AddWithValue("@ImporteC", ImporteC);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml += idReg.ToString() + " - Registro Guardado";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string GetBtnNuevo(string vTipDoc, string vUser)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select max(NumeroOrdenTrabajo) + 1 NumeroOrdenTrabajo from IROBDOptica.dbo.OrdenTrabajo; " +
                "select * from IROBDOptica.dbo.Correlativo where Codigo='6';";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtOT = objdataset.Tables[0];
            DataTable dtBol = objdataset.Tables[1];

            if (dtOT.Rows.Count > 0)
            {
                gHTML += dtOT.Rows[0]["NumeroOrdenTrabajo"].ToString();
                gHTML += "||sep||";
                gHTML += dtBol.Rows[0]["NumeroSerie"].ToString();
                gHTML += " - ";
                gHTML += dtBol.Rows[0]["NumeroCorrelativo"].ToString();

            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetbuscarProd(string vbuscarProd)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select sPAlm.*, sPDet.codmontura, PO.Nombre, PO.PrecioCompra " +
                "from IROBDOptica.dbo.stkProductoAlmacen sPAlm " +
                "inner join IROBDOptica.dbo.stkProductoDet sPDet on sPAlm.idproddet=sPDet.idproddet " +
                "inner join IROBDOptica.dbo.ProductosOptica PO on sPAlm.codigoprod=PO.Codigo " +
                "where sPAlm.idalmacen='2' and PO.Nombre COLLATE SQL_Latin1_General_Cp1_CI_AI like '%' + @vBuscar + '%'";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@vBuscar", vbuscarProd);
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
                gHTML += "<th class=''>cod </th>";
                gHTML += "<th class=''>producto</th>";
                gHTML += "<th class=''>CodMontura</th>";
                gHTML += "<th class=''>stock</th>";
                gHTML += "<th class=''>cantidad</th>";
                gHTML += "<th class=''>Agregar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vcod, vprod, vmontura, vmontcod, vstk, vprec;
                    vcod = dbRow["Codigoprod"].ToString();
                    vprod = dbRow["Nombre"].ToString();
                    vmontura = dbRow["codmontura"].ToString();
                    vmontcod = dbRow["idproddet"].ToString();
                    vstk = dbRow["stock"].ToString();
                    vprec = dbRow["PrecioCompra"].ToString();
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vprod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vmontura + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vstk + "</td>";
                    gHTML += "<td style='text-align: left;' ><input type='number' name='txtCant_" + nroitem + "' id='txtCant_" + nroitem + "' onkeyup='agregaprodKD(" + nroitem + ")' class='form-control input-lg'></td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-info' onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vmontura + "', '" + vprec + "', document.getElementById('txtCant_" + nroitem + "').value, '" + vmontcod + "')\" data-dismiss='modal'> Agregar</div>" +
                        "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetbuscarPac(string vbuscarPac)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from IROBDOptica.dbo.Proveedor where NumeroDocumento + RazonSocial + PersonaContacto like '%" + vbuscarPac + "%' and Estado = '1';";
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
                gHTML += "<th class=''>cod </th>";
                gHTML += "<th class=''>RUC</th>";
                gHTML += "<th class=''>Proveedor</th>";
                gHTML += "<th class=''>Contacto</th>";
                gHTML += "<th class=''>Seleccionar</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vcod, vpac, vdni, vcel;
                    vcod = dbRow["Codigo"].ToString();
                    vpac = dbRow["RazonSocial"].ToString();
                    vdni = dbRow["NumeroDocumento"].ToString();
                    vcel = dbRow["PersonaContacto"].ToString();
                    //gHTML += "<tr onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vprec + "')\" data-dismiss='modal'>";
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vdni + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vpac + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vcel + "</td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-info' id='btnAgregaPer" + nroitem + "' onclick=\"agregarPer('" + vcod + "', '" + vdni + "', '" + vpac + "')\" data-dismiss='modal'> Seleccionar</div>" +
                        "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
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

}