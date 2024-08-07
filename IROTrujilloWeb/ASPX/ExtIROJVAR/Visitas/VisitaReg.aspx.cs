using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_ExtIROJVAR_Visitas_VisitaReg : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
        }
        CargaTablaDT();
    }

    [WebMethod]
    public static string GetPersonalIRO(string vtxtape, string vtxtnom)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select top 5 * from RRHH.dbo.PERSONAL " +
                "where NOM_PERSONAL like '%" + vtxtape + "%" + vtxtnom + "%'";
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
                gHTML += "<table class='table table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<th class=''>Personal </th>";
                gHTML += "<th class=''>Cargo</th>";
                gHTML += "<th class=''>Sel.</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vdni, vnom, vcargo, vuoper;
                    vdni = dbRow["DNI_PER"].ToString();
                    vnom = dbRow["NOM_PERSONAL"].ToString();
                    vcargo = dbRow["CARGO_PER"].ToString();
                    vuoper = dbRow["UO_PER"].ToString();
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vnom + "</td>";
                    gHTML += "<td class='' >" + vcargo + "</td>";
                    gHTML += "<td style='text-align: left;' >" +
                        "<div class='btn btn-warning' onclick=\"fSelPer('" + vdni + "', '" + vnom + "', '" + vcargo + "', '" + vuoper + "')\" data-dismiss='modal'> Seleccionar</div>" +
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
    public static string SetRegVisitaSalida(string vIdVisita)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        string qSql = "";
        try
        {
            //con.Open();
            qSql = "update RRHH.dbo.Visitante set HoraSal = cast(GETDATE() as smalldatetime) where idVisita = @idVisita ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@idVisita", vIdVisita);

            conSAP00i.Open();
            cmd.ExecuteNonQuery();
            conSAP00i.Close();

            gHTML = "Correcto";
        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        return gHTML;
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }


    public void CargaTablaDT()
    {
        string vNombre = txtProd.Text;
        string gHTML = "";
        try
        {
            //con.Open();
            string qSql = "select * from RRHH.dbo.Visitante " +
                "where VisDNI like @vN + '%' and fechavisita = cast(GETDATE() as date) " +
                "order by HoraSal, fecharegistro";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vN", vNombre);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            //GVtable.DataSource = objdataset.Tables[0];
            //GVtable.DataBind();
            //CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];



            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table class='table table-hover' style='text-align: left; font-size: 14px; '><tr>";
                gHTML += "<th class=''>Fecha </th>";
                gHTML += "<th class=''>Reg.Salida </th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Visitante</th>";
                gHTML += "<th class=''>Entidad</th>";
                gHTML += "<th class=''>Motivo</th>";
                gHTML += "<th class=''>Personal-IRO</th>";
                gHTML += "<th class=''>Unidad</th>";
                gHTML += "<th class=''>Ingreso</th>";
                gHTML += "<th class=''>Salida</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + dbRow["fechavisita"].ToString().Substring(0, 10) + "</td>";
                    if (dbRow["HoraSal"].ToString().Length < 1)
                    {
                        gHTML += "<td class='' style='text-align: left;'><button type='button' class='btn bg-navy ' onclick=\"fRegVisitaSalida('" + dbRow["idVisita"].ToString() + "')\"><i class='fa fa-fw fa-eject'></i> Salida</button></td>";
                    }
                    else
                    {
                        gHTML += "<td><i class='fa fa-fw fa-check'></i></td>";
                    }

                    gHTML += "<td class='stLeft' >" + dbRow["VisDNI"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["VisNombres"].ToString() + " " + dbRow["VisApellidos"].ToString() + "</td>";
                    gHTML += "<td class=' bg-light-blue' >" + dbRow["VisEntidad"].ToString() + "</td>";
                    gHTML += "<td class=' ' >" + dbRow["VisMotivo"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["PerNombres"].ToString() + " " + dbRow["PerApellidos"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["PerUnidad"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["HoraIng"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["HoraSal"].ToString() + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;

                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
                lblMensaje.Text = "";
            }
            else
            {
                //this.Page.Response.Write("<script language='JavaScript'>" +
                    //"document.getElementById('divRegVisita').style.display = 'block';" +
                    //"$('#divRegVisita').toggle();" +
                    //"$('#divRegVisita').show();" +
                    //"$('#divRegVisita').css({'display':''});" +
                    //"fShowRegVisita();" +
                    //"</script>");
                //gHTML += "Sin Registro en el Dia";
                lblMensaje.Text = "Sin Registro en el Dia";
                CargaDatosSinVisita();
            }

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            gHTML += ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }

        LitTABL1.Text = gHTML;

    }

    protected void btnNuevaVisita_Click(object sender, EventArgs e)
    {
        CargaDatosSinVisita();
    }

    public void CargaDatosSinVisita()
    {
        if (lblMensaje.Text.Length < 1) { lblMensaje.Text = "Nuevo Registro"; }        
        string vNombre = txtProd.Text;
        string gHTML = "";
        try
        {
            string qSql = "select top 1 * from RRHH.dbo.Visitante " +
                "where VisDNI = @vN order by fecharegistro desc";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vN", vNombre);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            txtVisDNI.Value = txtProd.Text;
            
            if (objdataset.Tables[0].Rows.Count > 0)
            {
                DataRow dbRow = objdataset.Tables[0].Rows[0];

                txtVisNombres.Value = dbRow["VisNombres"].ToString();
                txtVisApellidos.Value = dbRow["VisApellidos"].ToString();
                txtVisDNI.Value = dbRow["VisDNI"].ToString();
                txtVisEntidad.Value = dbRow["VisEntidad"].ToString();
            }

        }
        catch (Exception ex)
        {
            gHTML += ex.Message.ToString();
        }
        LitTABL1.Text = gHTML;
        txtVisNombres.Focus();

    }

    protected void btnRegistrarSalida_Click(object sender, EventArgs e)
    {
        string varSql = "select * from NUEVA.dbo.extUsuario where extUDNI = '" + txtProd.Text + "'";
        SqlCommand cmd = new SqlCommand(varSql, conSAP00);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        //con.Open();
        DataSet objdataset = new DataSet();
        adapter.Fill(objdataset);
        //con.Close();
        DataTable dtDato = objdataset.Tables[0];
        if (dtDato.Rows.Count > 0)
        {
            this.Page.Response.Write("<script language='JavaScript'>window.alert('Usuario ya se Encuentra Registrado');</script>");
            this.Page.Response.Write("<script language='JavaScript'>location='HojaRegistro.aspx?Usuario=" + txtProd.Text + "'</script>");
        }
        else
        {
            //GuardarUsuario();
        }

    }

    [WebMethod]
    public static string BtnRegIngreso(string fechavisita, string VisNombres, string VisApellidos, string VisDNI, string VisEntidad, string VisMotivo, string PerNombres, string PerApellidos, string PerDNI, string PerUnidad, string HoraIng)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "insert into RRHH.dbo.Visitante (fechavisita, VisNombres, VisApellidos, VisDNI, VisEntidad, VisMotivo, PerNombres, PerApellidos, PerDNI, PerUnidad, HoraIng) VALUES (@fechavisita, @VisNombres, @VisApellidos, @VisDNI, @VisEntidad, @VisMotivo, @PerNombres, @PerApellidos, @PerDNI, @PerUnidad, @HoraIng) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@fechavisita", fechavisita);
            cmd.Parameters.AddWithValue("@VisNombres", VisNombres);
            cmd.Parameters.AddWithValue("@VisApellidos", VisApellidos);
            cmd.Parameters.AddWithValue("@VisDNI", VisDNI);
            cmd.Parameters.AddWithValue("@VisEntidad", VisEntidad);
            cmd.Parameters.AddWithValue("@VisMotivo", VisMotivo);
            cmd.Parameters.AddWithValue("@PerNombres", PerNombres);
            cmd.Parameters.AddWithValue("@PerApellidos", PerApellidos);
            cmd.Parameters.AddWithValue("@PerDNI", PerDNI);
            cmd.Parameters.AddWithValue("@PerUnidad", PerUnidad);
            cmd.Parameters.AddWithValue("@HoraIng", HoraIng);

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml = "Correcto";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }
}