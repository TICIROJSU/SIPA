using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVARSIPA_Programacion_ProgServicios : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        IniServicios();

    }


    public void IniServicios()
    {
        try
        {

            string qSql = "select *, DES_SER + ' - ' + DSC_SER as DescServicio from NUEVA.dbo.SERVICIO order by DSC_SER ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //ListItem LisTMP = new ListItem("OFT. GEN.", "71", true);
            ddlServicio.DataSource = dtDato;
            //ddlServicio.Items.Add(LisTMP);
            ddlServicio.DataTextField = "DescServicio";
            ddlServicio.DataValueField = "COD_SER";
            ddlServicio.DataBind();

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    [WebMethod]
    public static string SetBtnGuardar(string COD_SER, string PIS_SER, string Tur_Ser, string PSCupos, string PSAdiLimite, string Hr_Ser, string PSObservacion, string PSEstado, string PSCantTurno)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO TabLibres.dbo.ProgServ (COD_SER, PIS_SER, Tur_Ser, PSCupos, PSAdiLimite, Hr_Ser, PSObservacion, PSEstado, PSCantTurno) VALUES (@COD_SER, @PIS_SER, @Tur_Ser, @PSCupos, @PSAdiLimite, @Hr_Ser, @PSObservacion, @PSEstado, @PSCantTurno) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@COD_SER", COD_SER);
            cmd.Parameters.AddWithValue("@PIS_SER", PIS_SER);
            cmd.Parameters.AddWithValue("@Tur_Ser", Tur_Ser);
            cmd.Parameters.AddWithValue("@PSCupos", PSCupos);
            cmd.Parameters.AddWithValue("@PSAdiLimite", PSAdiLimite);
            cmd.Parameters.AddWithValue("@Hr_Ser", Hr_Ser);
            cmd.Parameters.AddWithValue("@PSObservacion", PSObservacion);
            cmd.Parameters.AddWithValue("@PSEstado", PSEstado);
            cmd.Parameters.AddWithValue("@PSCantTurno", PSCantTurno);

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


    protected void btnListar_Click(object sender, EventArgs e)
    {
        try
        {
            string qSql = "SELECT PSer.COD_SER CSer, Ser.DSC_SER Servicio, PSer.PIS_SER Piso, " +
                "Tur_Ser Turno, PSCupos Cupos, PSAdiLimite Limite, Hr_Ser Horario, PSObservacion Observacion " +
                "from TabLibres.dbo.ProgServ PSer " +
                "left join NUEVA.dbo.SERVICIO Ser on PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

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
