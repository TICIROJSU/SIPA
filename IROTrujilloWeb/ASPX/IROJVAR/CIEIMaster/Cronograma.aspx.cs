using System;
using System.Data;
using System.Data.SqlClient;

public partial class ASPX_IROJVAR_CIEIMaster_Cronograma : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            try
            {
                string qSql = "select * from NUEVA.dbo.JVARCIEIPag where Pagina='CRONOGRAMA' or Pagina='TARIFARIO' ";
                SqlCommand cmd = new SqlCommand(qSql, conSAP00);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                conSAP00.Open();
                DataSet objdataset = new DataSet();
                adapter.Fill(objdataset);
                conSAP00.Close();

                DataTable dtDato = objdataset.Tables[0];

                foreach (DataRow dbRow in dtDato.Rows)
                {
                    switch (dbRow["Pagina"].ToString())
                    {
                        case "CRONOGRAMA":
                            txtTituloCron.Text = dbRow["Titulo"].ToString();
                            txtContenidoCron.Text = dbRow["Contenido"].ToString();
                            txtURLimgCron.Text = dbRow["Detalle1"].ToString();
                            txtURLimgIDCron.Text = dbRow["Detalle2"].ToString();
                            LitSICron.Text = "<img id='imgCron' class='img-responsive' src='http://drive.google.com/uc?export=view&id=" + dbRow["Detalle2"].ToString() + "' />";
                            break;

                        case "TARIFARIO":
                            txtTituloTar.Text = dbRow["Titulo"].ToString();
                            //txtContenidoCron.Text = dbRow["Contenido"].ToString();
                            txtURLimg.Text = dbRow["Detalle1"].ToString();
                            txtURLimgID.Text = dbRow["Detalle2"].ToString();
                            LitSITar.Text = "<img id='imgCron' class='img-responsive' src='http://drive.google.com/uc?export=view&id=" + dbRow["Detalle2"].ToString() + "' />";
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //Label1.Text = "Error";
                ex.Message.ToString();
                //LitError.Text = ex.Message.ToString();
            }
        }
    }

    protected void bntGuardarCronSI_Click(object sender, EventArgs e)
    {
        string vTitulo = txtTituloCron.Text;
        string vContenido = txtContenidoCron.Text;
        string vDetalle1 = txtURLimgCron.Text;
        string vDetalle2 = txtURLimgIDCron.Text;
        try
        {
            string consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = @Titulo, Contenido = @Contenido, Detalle1 = @Detalle1, Detalle2 = @Detalle2 WHERE Pagina = 'CRONOGRAMA' AND Seccion = 'SUPIZQ' ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Titulo", vTitulo);
            cmd.Parameters.AddWithValue("@Contenido", vContenido);
            cmd.Parameters.AddWithValue("@Detalle1", vDetalle1);
            cmd.Parameters.AddWithValue("@Detalle2", vDetalle2);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            Literal1.Text = ex.Message.ToString();
        }
    }

    protected void bntGuardarTarSI_Click(object sender, EventArgs e)
    {
        string vTitulo = txtTituloTar.Text;
        //string vContenido = txtContenidoCron.Text;
        string vDetalle1 = txtURLimg.Text;
        string vDetalle2 = txtURLimgID.Text;
        try
        {
            string consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = @Titulo, Detalle1 = @Detalle1, Detalle2 = @Detalle2 WHERE Pagina = 'TARIFARIO' AND Seccion = 'SUPIZQ' ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Titulo", vTitulo);
            //cmd.Parameters.AddWithValue("@Contenido", vContenido);
            cmd.Parameters.AddWithValue("@Detalle1", vDetalle1);
            cmd.Parameters.AddWithValue("@Detalle2", vDetalle2);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            Literal1.Text = ex.Message.ToString();
        }
    }
}