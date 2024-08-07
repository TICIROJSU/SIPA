using System;
using System.Data;
using System.Data.SqlClient;

public partial class IROTWeb_Direccion_Resoluciones_Default : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        //string siTit = "", siCon = "";
        //try
        //{
        //    string qSql = "select * from NUEVA.dbo.JVARCIEIPag where Pagina='CRONOGRAMA' ";
        //    SqlCommand cmd = new SqlCommand(qSql, conSAP00);
        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = cmd;

        //    conSAP00.Open();
        //    DataSet objdataset = new DataSet();
        //    adapter.Fill(objdataset);
        //    conSAP00.Close();

        //    DataTable dtDato = objdataset.Tables[0];

        //    foreach (DataRow dbRow in dtDato.Rows)
        //    {
        //        switch (dbRow["Seccion"].ToString())
        //        {
        //            case "SUPIZQ":
        //                LitSITitulo.Text = dbRow["Titulo"].ToString();
        //                LitSIContenido.Text = dbRow["Contenido"].ToString();
        //                LitSIimg.Text = "<img alt='' class='img-responsive' src='http://drive.google.com/uc?export=view&id=" + dbRow["Detalle2"].ToString() + "' />";
        //                break;

        //            default:
        //                break;
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    //Label1.Text = "Error";
        //    ex.Message.ToString();
        //    LitError.Text = ex.Message.ToString();
        //}
    }
}