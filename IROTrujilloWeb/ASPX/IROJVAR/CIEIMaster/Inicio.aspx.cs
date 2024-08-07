using System;
using System.Data;
using System.Data.SqlClient;

public partial class ASPX_IROJVAR_CIEIMaster_Inicio : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            //string siTit = "", siCon = "";
            try
            {
                string qSql = "select * from NUEVA.dbo.JVARCIEIPag where Pagina='INICIO' ";
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
                    switch (dbRow["Seccion"].ToString())
                    {
                        case "SUPIZQ":
                            txtTitulo.Text = dbRow["Titulo"].ToString();
                            froala_editor.InnerText = dbRow["Contenido"].ToString();
                            break;

                        case "SUPDER":
                            txtURLimg.Text = dbRow["Contenido"].ToString();
                            txtURLimgID.Text = dbRow["Detalle1"].ToString();
                            LitSDMiembros.Text = "<img id='imgMiemb' class='img-responsive' src='http://drive.google.com/uc?export=view&id=" + dbRow["Detalle1"].ToString() + "' />";
                            break;

                        default:
                            break;
                    }
                }

                //txtTitulo.Text = siTit;
                //froala_editor.InnerText = siCon;
                //dbRow["PIS_SER"].ToString();
            }
            catch (Exception ex)
            {
                //Label1.Text = "Error";
                ex.Message.ToString();
                //LitError.Text = ex.Message.ToString();
            }
        }
        
    }

    protected void bntGuardarSI_Click(object sender, EventArgs e)
    {
        //string txtEdit = froala_editor.InnerText;
        //Literal1.Text = txtEdit; //.Substring(0, txtEdit.Length - 230);
        string vTitulo = txtTitulo.Text;
        string vContenido = froala_editor.InnerText;
        if (vContenido.Contains(">Froala Editor<"))
        { vContenido = vContenido.Substring(0, vContenido.Length - 229); }
        
        try
        {
            //con.Open();
            string consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = @Titulo, Contenido = @Contenido WHERE Pagina = 'INICIO' AND Seccion = 'SUPIZQ' ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Titulo", vTitulo);
            cmd.Parameters.AddWithValue("@Contenido", vContenido);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();

            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado');window.location.assign('../PruSave/Guardar.aspx');</script>");
            //Literal1.Text = cmd.CommandText;
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            Literal1.Text = ex.Message.ToString();
        }
    }

    protected void bntGuardarSD_Click(object sender, EventArgs e)
    {
        string vContenido = txtURLimg.Text;
        string vDetalle1 = txtURLimgID.Text;
        try
        {
            string consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Contenido = @Contenido, Detalle1 = @Detalle1 WHERE Pagina = 'INICIO' AND Seccion = 'SUPDER' ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Contenido", vContenido);
            cmd.Parameters.AddWithValue("@Detalle1", vDetalle1);

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