using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_ExtIROJVAR_Citas_ConsultaMedicam : System.Web.UI.Page
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

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {

        string gHTML = "";
        try
        {
            //con.Open();
            //string qSql = "exec NUEVA.dbo.SP_JVAR_ConsMedicamFarm;";
            string qSql = "exec IROf.dbo.SPW_LISTADOPRECIOSMEDICAMENTOS 603; " +
                "select Nombre from IROf.dbo.Almacen where IdAlmacen like 'u%' AND EsEspecialidad=1 order by Nombre; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            //GVtable.DataSource = objdataset.Tables[0];
            //GVtable.DataBind();
            //CargaTabla(dtDato);

            DataTable dtDatoDetAt = objdataset.Tables[0];

            ddlCategoria.Items.Clear();
            ddlServicio.Items.Clear();

            DataTable dtCat = dtDatoDetAt.AsEnumerable().GroupBy(r => r.Field<string>("Nombre")).Select(g => g.First()).CopyToDataTable();
            DataTable dtServ = dtDatoDetAt.AsEnumerable().GroupBy(r => r.Field<string>("DEPARTAMENTO/SERVICIO")).Select(g => g.First()).CopyToDataTable();

            ListItem LisTMP = new ListItem("Todos", "", true);
            ddlCategoria.DataSource = dtCat;
            ddlCategoria.Items.Add(LisTMP);
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Nombre";
            ddlCategoria.DataBind();

            ddlServicio.DataSource = dtServ;
            ddlServicio.Items.Add(LisTMP);
            ddlServicio.DataTextField = "DEPARTAMENTO/SERVICIO";
            ddlServicio.DataValueField = "DEPARTAMENTO/SERVICIO";
            ddlServicio.DataBind();

            ddlServicio2.DataSource = objdataset.Tables[1];
            ddlServicio2.Items.Add(LisTMP);
            ddlServicio2.DataTextField = "Nombre";
            ddlServicio2.DataValueField = "Nombre";
            ddlServicio2.DataBind();

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table id='tblbscrJS' class='table table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr><th class=''>Medicamento </th>";
                gHTML += "<th class='hide'>Catg</th>";
                gHTML += "<th class='hide'>Serv</th>";
                gHTML += "<th class=''>Fecha Vcto.</th>";
                gHTML += "<th class=''>Cant.</th>";
                gHTML += "<th class=''>Precio Unit.</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                string vCabSub = "", vCabServ = "";

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    string vMed, vFVen, vCan, vPUn;
                    vMed = dbRow["Producto"].ToString();
                    vFVen = dbRow["FECHAVCTO"].ToString();
                    vCan = dbRow["StockActual"].ToString();
                    vPUn = dbRow["PrecioUnit"].ToString();

                    if (nroitem == 1)
                    {
                        vCabSub = dbRow["Nombre"].ToString();
                        vCabServ = dbRow["DEPARTAMENTO/SERVICIO"].ToString();
                        gHTML += "<tr style='background-color:#0099FF; font-weight:bold'> <td class=''>" + vCabSub + "</td>";
                        gHTML += "<td class='hide'>" + vCabSub + "</td>";
                        gHTML += "<td class='hide'></td>";
                        gHTML += "<td class=''>Fecha Vcto.</td>";
                        gHTML += "<td class=''>Cant.</td>";
                        gHTML += "<td class=''>Precio Unit.</td>";
                        gHTML += "</tr>" + Environment.NewLine;
                    }

                    if (vCabSub != dbRow["Nombre"].ToString())
                    {
                        gHTML += "<tr style='background-color:#0099FF; font-weight:bold'><td class=''>" + dbRow["Nombre"].ToString() + "</td>";
                        gHTML += "<td class='hide'>" + dbRow["Nombre"].ToString() + "</td>";
                        gHTML += "<td class='hide'></td>";
                        gHTML += "<td class=''>Fecha Vcto.</td>";
                        gHTML += "<td class=''>Cant.</td>";
                        gHTML += "<td class=''>Precio Unit.</td>";
                        gHTML += "</tr>" + Environment.NewLine;
                    }
                    vCabSub = dbRow["Nombre"].ToString();
                    vCabServ = dbRow["DEPARTAMENTO/SERVICIO"].ToString();

                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + vMed + "</td>";
                    gHTML += "<td class='hide' >" + vCabSub + "</td>";
                    gHTML += "<td class='hide' >" + vCabServ + "</td>";
                    gHTML += "<td class='' >" + vFVen + "</td>";
                    gHTML += "<td class='' >" + vCan + "</td>";
                    gHTML += "<td class='' >" + vPUn + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;

                }

                gHTML += "</table></div></div><hr style='border-top: 1px solid blue'>";
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

}