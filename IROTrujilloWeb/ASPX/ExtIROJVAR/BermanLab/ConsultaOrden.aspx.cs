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

public partial class ASPX_ExtIROJVAR_BermanLab_ConsultaOrden : System.Web.UI.Page
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
        //string vNombre = ""; // txtProd.Text;
        string gHTML = "";
        string vWhere = "WHERE SOL_LAB<>''";
        if (chkVerTodo.Checked){ vWhere = ""; }
        try
        {
            //con.Open();
            string qSql = "SELECT ORDLAB.ID_LAB, CONVERT(VARCHAR(10), FEC_TAR, 103) CITA, HOR_TAR HORA, Des_Pg PACIENTE, IHC NHC, IAP + ' ' + IAS + ' ' + INO [APELLIDOS Y NOMBRES], DES_SER SERVICIO, APE_PER MEDICO, CONVERT(VARCHAR(10), FOR_LAB, 103) ORDEN, SOL_LAB [N° SOLICITUD] " +
                "FROM NUEVA.dbo.ORDLAB " +
                "INNER JOIN NUEVA.dbo.TARJETON ON CIT_LAB=ID_CITA INNER JOIN NUEVA.dbo.HISTORIA ON IHC=NHC_LAB " +
                "INNER JOIN NUEVA.dbo.PROGRAMA ON Cod_Pg=PRG_LAB INNER JOIN NUEVA.dbo.SERVICIO ON COD_SER=SER_LAB " +
                "INNER JOIN NUEVA.dbo.PERSONAL ON COD_PER=MED_LAB " + 
                vWhere + " ORDER BY IAP + ' ' + IAS + ' ' + INO; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@vN", vNombre);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
            //CargaTabla(dtDato);
            //DataTable dtDatoDetAt = objdataset.Tables[0];


        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            gHTML += ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }

        LitTABL1.Text = gHTML;

    }

}