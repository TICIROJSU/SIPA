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

public partial class ASPX_IROJVAR_Sorteo202312_ListaParticipantes : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        CargaTablaDT();

    }


    public void CargaTablaDT()
    {
        try
        {
            string qSql = "select idPerSorteo as Nro, NOMBRE as Personal, CONDICION as Contrato, Sorteo as Ganador " +
                "from Prueba.dbo.RegistroSorteo202312 order by idPerSorteo; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            gvLista.DataSource = dtDato;
            gvLista.DataBind();
            //CargaTabla(dtDato);

        }
        catch (Exception ex)
        {

        }

    }

}