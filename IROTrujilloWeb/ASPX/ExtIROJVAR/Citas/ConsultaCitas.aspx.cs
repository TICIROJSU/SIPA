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

public partial class ASPX_ExtIROJVAR_Citas_ConsultaCitas : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            int vmonth = DateTime.Now.Month;
        }
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
            string qSql = "select cast(cast(FEC_TAR as date) as varchar) Fecha, HOR_TAR Hora, ihc [Historia Clinica], idni DNI, iap + ' ' + ias + ', ' + ino Nombres, des_ser Servicio, APE_PER Medico, Des_Pg [Tipo Paciente] " +
                "from NUEVA.dbo.TARJETON " +
                "inner join NUEVA.dbo.HIStoria on ihc=HIC_TAR " +
                "inner join NUEVA.dbo.SERVICIO on COD_SER=SER_TAR " +
                "inner join NUEVA.dbo.PERSONAL on COD_PER=COM_TAR " +
                "left join NUEVA.dbo.PROGRAMA on PRG_TAR=Cod_Pg " +
                "where FEC_TAR>=cast(GETDATE() as date) and CIT_TAR='x' and ihc + IDNI + iap+' '+ias+', '+ino like '%' + @vN + '%' " +
                "order by FEC_TAR desc;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vN", vNombre);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
            //CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];

            GuardarUsuario(vNombre);

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            gHTML += ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }

        LitTABL1.Text = gHTML;

    }
    
    protected void GuardarUsuario(string vDNI)
    {
        string varSql = "INSERT INTO TabLibres.dbo.LogHistConsultaCita(LogDNI, LogFechaReg) " +
            "VALUES(@LogDNI, @LogFechaReg); ";
        SqlCommand cmd = new SqlCommand(varSql, conSAP00);
        cmd.Parameters.AddWithValue("@LogDNI", vDNI);
        cmd.Parameters.AddWithValue("@LogFechaReg", DateTime.Now);
        conSAP00.Open();
        cmd.ExecuteNonQuery();
        conSAP00.Close();
        //this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Correcto'); location='HojaRegistro.aspx?Usuario=" + txtDNIreg.Text + "'</script>");
        //Response.Redirect("../Login/");
    }


}