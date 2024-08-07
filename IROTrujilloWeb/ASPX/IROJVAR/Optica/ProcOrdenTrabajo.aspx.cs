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

public partial class ASPX_IROJVAR_Optica_ProcOrdenTrabajo : System.Web.UI.Page
{
	SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
	protected void Page_Load(object sender, EventArgs e)
	{Server.ScriptTimeout = 3600;
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
        else
        {
            txtMontoTotal.Text = "";
        }

	}

	[WebMethod]
	public static string SetBtnGuardar(string vNumeroOT, string vAddOrden, string vEsferaODLejos, string vEsferaOILejos, string vEsferaODCerca, string vEsferaOICerca, string vCilindroODLejos, string vCilindroOILejos, string vCilindroODCerca, string vCilindroOICerca, string vEjeODLejos, string vEjeOILejos, string vEjeODCerca, string vEjeOICerca, string vColorODLejos, string vColorOILejos, string vColorODCerca, string vColorOICerca, string vDipLejos, string vDipCerca, string vComentario, string vEstadoCuota, string vUsuarioRegistro, string vUsuarioModificacion, string vTipCliente, string vTipReceta, string vcodigopac, string vDNIpac, string vnombrepac, string vtipodocumento, string vmontototal, string vmotiDesc, string vporcDesc, string vmontDesc, string vACuenta, string vACueC1, string vACueC2)
	{
		SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
		string gDetHtml = "";
		string qSql = "";
		try
		{
			qSql = "INSERT INTO " +
                "IROBDOptica.dbo.OrdenTrabajo (NumeroOT, AddOrden, EsferaODLejos, EsferaOILejos, EsferaODCerca, EsferaOICerca, CilindroODLejos, CilindroOILejos, CilindroODCerca, CilindroOICerca, EjeODLejos, EjeOILejos, EjeODCerca, EjeOICerca, ColorODLejos, ColorOILejos, ColorODCerca, ColorOICerca, DipLejos, DipCerca, Comentario, Estado, EstadoCuota, IsActivo, FechaRegistro, FechaModificacion, UsuarioRegistro, UsuarioModificacion, TipCliente, TipReceta, codigopac, DNIpac, nombrepac, tipodocumento, montototal, porcdescuento, montdescuento, ACuenta, Cuota1, Cuota2) " +
                "VALUES (@NumeroOT, @AddOrden, @EsferaODLejos, @EsferaOILejos, @EsferaODCerca, @EsferaOICerca, @CilindroODLejos, @CilindroOILejos, @CilindroODCerca, @CilindroOICerca, @EjeODLejos, @EjeOILejos, @EjeODCerca, @EjeOICerca, @ColorODLejos, @ColorOILejos, @ColorODCerca, @ColorOICerca, @DipLejos, @DipCerca, @Comentario, @Estado, @EstadoCuota, @IsActivo, @FechaRegistro, @FechaModificacion, @UsuarioRegistro, @UsuarioModificacion, @TipCliente, @TipReceta, @codigopac, @DNIpac, @nombrepac, @tipodocumento, @montototal, @porcdescuento, @montdescuento, @ACuenta, @Cuota1, @Cuota2) " +
				"SELECT @@Identity;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@NumeroOT", vNumeroOT);
			cmd.Parameters.AddWithValue("@AddOrden", vAddOrden);
			cmd.Parameters.AddWithValue("@EsferaODLejos", vEsferaODLejos);
			cmd.Parameters.AddWithValue("@EsferaOILejos", vEsferaOILejos);
			cmd.Parameters.AddWithValue("@EsferaODCerca", vEsferaODCerca);
			cmd.Parameters.AddWithValue("@EsferaOICerca", vEsferaOICerca);
			cmd.Parameters.AddWithValue("@CilindroODLejos", vCilindroODLejos);
			cmd.Parameters.AddWithValue("@CilindroOILejos", vCilindroOILejos);
			cmd.Parameters.AddWithValue("@CilindroODCerca", vCilindroODCerca);
			cmd.Parameters.AddWithValue("@CilindroOICerca", vCilindroOICerca);
			cmd.Parameters.AddWithValue("@EjeODLejos", vEjeODLejos);
			cmd.Parameters.AddWithValue("@EjeOILejos", vEjeOILejos);
			cmd.Parameters.AddWithValue("@EjeODCerca", vEjeODCerca);
			cmd.Parameters.AddWithValue("@EjeOICerca", vEjeOICerca);
			cmd.Parameters.AddWithValue("@ColorODLejos", vColorODLejos);
			cmd.Parameters.AddWithValue("@ColorOILejos", vColorOILejos);
			cmd.Parameters.AddWithValue("@ColorODCerca", vColorODCerca);
			cmd.Parameters.AddWithValue("@ColorOICerca", vColorOICerca);
			cmd.Parameters.AddWithValue("@DipLejos", vDipLejos);
			cmd.Parameters.AddWithValue("@DipCerca", vDipCerca);
			cmd.Parameters.AddWithValue("@Comentario", vComentario);
			cmd.Parameters.AddWithValue("@Estado", "1");
			cmd.Parameters.AddWithValue("@EstadoCuota", vEstadoCuota);
			cmd.Parameters.AddWithValue("@IsActivo", "1");
			cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);
			cmd.Parameters.AddWithValue("@FechaModificacion", DateTime.Now);
			cmd.Parameters.AddWithValue("@UsuarioRegistro", vUsuarioRegistro);
			cmd.Parameters.AddWithValue("@UsuarioModificacion", vUsuarioModificacion);
            cmd.Parameters.AddWithValue("@TipCliente", vTipCliente);
            cmd.Parameters.AddWithValue("@TipReceta", vTipReceta);
            cmd.Parameters.AddWithValue("@codigopac", vcodigopac);
            cmd.Parameters.AddWithValue("@DNIpac", vDNIpac);
            cmd.Parameters.AddWithValue("@nombrepac", vnombrepac);
            cmd.Parameters.AddWithValue("@tipodocumento", vtipodocumento);
            cmd.Parameters.AddWithValue("@montototal", vmontototal);
            cmd.Parameters.AddWithValue("@porcdescuento", vporcDesc);
            cmd.Parameters.AddWithValue("@montdescuento", vmontDesc);
            cmd.Parameters.AddWithValue("@ACuenta", vACuenta);
            cmd.Parameters.AddWithValue("@Cuota1", vACueC1);
            cmd.Parameters.AddWithValue("@Cuota2", vACueC2);

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
			qSql = "INSERT INTO IROBDOptica.dbo.OrdenTrabajoDet(CodigoOT, codigoprod, cantidad, preciov, importev, idproddet, codmontura) VALUES (@CodigoOT, @codigoprod, @cantidad, @preciov, @importev, @idproddet, @codmontura) SELECT @@Identity;";

			SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
			cmd.CommandType = CommandType.Text;
			SqlDataAdapter adapter = new SqlDataAdapter();

			cmd.Parameters.AddWithValue("@CodigoOT", idming);
			cmd.Parameters.AddWithValue("@codigoprod", CodigoProd);
			cmd.Parameters.AddWithValue("@idproddet", idproddet);
			cmd.Parameters.AddWithValue("@codmontura", codmontura);
			cmd.Parameters.AddWithValue("@cantidad", Cantidad);
			cmd.Parameters.AddWithValue("@preciov", PrecioC);
			cmd.Parameters.AddWithValue("@importev", ImporteC);

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
			string qSql = "select * from IROBDOptica.dbo.Correlativo where ValorSerie = 'OT';";
			SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
			cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
			adapter2.SelectCommand = cmd2;

			conSAP00i.Open();
			DataSet objdataset = new DataSet();
			adapter2.Fill(objdataset);
			conSAP00i.Close();

			DataTable dtOT = objdataset.Tables[0];

			if (dtOT.Rows.Count > 0)
			{
				gHTML += dtOT.Rows[0]["NumeroCorrelativo"].ToString();

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
			//string qSql = "select * from IROBDOptica.dbo.ActivoNegocio where Nombre COLLATE SQL_Latin1_General_Cp1_CI_AI like '%" + vbuscarProd + "%'";
			string qSql = "select PO.Codigo, Nombre, PrecioVenta, sPD.idproddet, sPD.codmontura, sPA.idalmacen, sPA.stock from IROBDOptica.dbo.ProductosOptica PO inner join IROBDOptica.dbo.stkProductoDet sPD on PO.Codigo = sPD.codigoprod inner join IROBDOptica.dbo.stkProductoAlmacen sPA on PO.Codigo = sPA.codigoprod and sPD.idproddet = sPD.idproddet where Nombre like '%" + vbuscarProd + "%'";
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
				gHTML += "<th class=''>producto</th>";
				gHTML += "<th class=''>precio</th>";
				gHTML += "<th class=''>Montura</th>";
				gHTML += "<th class=''>stock</th>";
				gHTML += "<th class=''>cantidad</th>";
				gHTML += "<th class=''>Agregar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					string vcod, vprod, vprec, vmont, vcodmont, vstk;
					vcod = dbRow["Codigo"].ToString();
					vprod = dbRow["Nombre"].ToString();
					vprec = dbRow["PrecioVenta"].ToString();
					vmont = dbRow["codmontura"].ToString();
					vcodmont = dbRow["idproddet"].ToString();
					vstk = dbRow["Stock"].ToString();
					//gHTML += "<tr onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vprec + "')\" data-dismiss='modal'>";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vcod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vprec + "</td>";
					gHTML += "<td style='text-align: left;' >" + vmont + "</td>";
					gHTML += "<td style='text-align: left;' >" + vstk + "</td>";
					gHTML += "<td style='text-align: left;' ><input type='number' name='txtCant_" + nroitem + "' id='txtCant_" + nroitem + "' onkeyup='agregaprodKD(" + nroitem + ")' class='form-control input-lg'></td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' id='btnAgregaProd" + nroitem + "' onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vmont + "', '" + vprec + "', document.getElementById('txtCant_" + nroitem + "').value, '" + vcodmont + "')\" data-dismiss='modal'> Agregar</div>" +
						"</td>";
					//cod, prod, codmontura, prec, cant, idmontura
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
			//string qSql = "select * from IROBDOptica.dbo.Paciente where NumeroDocumento + NumeroCelular + Apellidos + Nombres like '%" + vbuscarPac + "%';";
			string qSql = "exec IROBDOptica.dbo.spListaPaciente '" + vbuscarPac + "';";
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
				gHTML += "<th class=''>APP</th>";
				gHTML += "<th class=''>cod </th>";
				gHTML += "<th class=''>DNI</th>";
				gHTML += "<th class=''>Paciente</th>";
				gHTML += "<th class=''>Celular</th>";
				gHTML += "<th class=''>Seleccionar</th>";
				gHTML += "</tr>" + Environment.NewLine;
				int nroitem = 0;

				foreach (DataRow dbRow in dtDatoDetAt.Rows)
				{
					nroitem += 1;
					string vcod, vpac, vdni, vcel, vapp;
					vapp = dbRow["APP"].ToString();
					vcod = dbRow["Codigo"].ToString();
					vpac = dbRow["Apellidos"].ToString() + ", "+ dbRow["Nombres"].ToString();
					vdni = dbRow["DNI"].ToString();
					vcel = dbRow["Celular"].ToString();
					//gHTML += "<tr onclick=\"agregarFila('" + vcod + "', '" + vprod + "', '" + vprec + "')\" data-dismiss='modal'>";
					gHTML += "<tr>";
					gHTML += "<td class='' >" + vapp + "</td>";
					gHTML += "<td class='' >" + vcod + "</td>";
					gHTML += "<td style='text-align: left;' >" + vdni + "</td>";
					gHTML += "<td style='text-align: left;' >" + vpac + "</td>";
					gHTML += "<td style='text-align: left;' >" + vcel + "</td>";
					gHTML += "<td style='text-align: left;' >" +
						"<div class='btn btn-info' id='btnAgregaProd" + nroitem + "' onclick=\"agregarPaciente('" + vcod + "', '" + vdni + "', '" + vpac + "')\" data-dismiss='modal'> Seleccionar</div>" +
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

                    string AddOrden, EsferaODLejos, EsferaOILejos, EsferaODCerca, EsferaOICerca, CilindroODLejos, CilindroOILejos, CilindroODCerca, CilindroOICerca, EjeODLejos, EjeOILejos, EjeODCerca, EjeOICerca, ColorODLejos, ColorOILejos, ColorODCerca, ColorOICerca, DipLejos, DipCerca;
                    string vTipCliente, vTipReceta;

                    AddOrden = dbRow["AddOrden"].ToString();
                    EsferaODLejos = dbRow["EsferaODLejos"].ToString();
                    EsferaOILejos = dbRow["EsferaOILejos"].ToString();
                    EsferaODCerca = dbRow["EsferaODCerca"].ToString();
                    EsferaOICerca = dbRow["EsferaOICerca"].ToString();
                    CilindroODLejos = dbRow["CilindroODLejos"].ToString();
                    CilindroOILejos = dbRow["CilindroOILejos"].ToString();
                    CilindroODCerca = dbRow["CilindroODCerca"].ToString();
                    CilindroOICerca = dbRow["CilindroOICerca"].ToString();
                    EjeODLejos = dbRow["EjeODLejos"].ToString();
                    EjeOILejos = dbRow["EjeOILejos"].ToString();
                    EjeODCerca = dbRow["EjeODCerca"].ToString();
                    EjeOICerca = dbRow["EjeOICerca"].ToString();
                    ColorODLejos = dbRow["ColorODLejos"].ToString();
                    ColorOILejos = dbRow["ColorOILejos"].ToString();
                    ColorODCerca = dbRow["ColorODCerca"].ToString();
                    ColorOICerca = dbRow["ColorOICerca"].ToString();
                    DipLejos = dbRow["DipLejos"].ToString();
                    DipCerca = dbRow["DipCerca"].ToString();

                    vTipCliente = dbRow["TipCliente"].ToString();
                    vTipReceta = dbRow["TipReceta"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vcod + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vfec + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vpro + "</td>";
                    //gHTML += "<td style='text-align: left;' >" + vtdoc + "</td>";
                    //gHTML += "<td style='text-align: left;' >" + vndoc + "</td>";
                    gHTML += "<td style='text-align: left;' >" + vmon + "</td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-info' onclick=\"gSelNIng('" + vpro + "', '" + vfec2 + "', '" + vcod + "', '" + vmon + "', '" +
                        AddOrden + "', '" + EsferaODLejos + "', '" + EsferaOILejos + "', '" + EsferaODCerca + "', '" + EsferaOICerca + "', '" + CilindroODLejos + "', '" + CilindroOILejos + "', '" + CilindroODCerca + "', '" + CilindroOICerca + "', '" + EjeODLejos + "', '" + EjeOILejos + "', '" + EjeODCerca + "', '" + EjeOICerca + "', '" + ColorODLejos + "', '" + ColorOILejos + "', '" + ColorODCerca + "', '" + ColorOICerca + "', '" + DipLejos + "', '" + DipCerca + "', '" + vTipCliente + "', '" + vTipReceta + "')\" data-dismiss='modal'> Seleccionar</div>" +
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