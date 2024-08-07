using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ASPX_ExtIROJVAR_Foto_FOxDxRD : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            txtFechaDesde.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            txtFechaHasta.Text = txtFechaDesde.Text;

        }

        CargaTablaDT();

    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaTablaDT()
    {
        string vtxtDesde = txtFechaDesde.Text;
        string vtxtHasta = txtFechaHasta.Text;

        string gHTML = "";
        try
        {
            string qSql = "select 'RDP ' DIAGNOSTICO, count(*) TOTAL	 from NUEVA.dbo.fo " +
                "where DSD_FO='RDp' or DSi_FO='RDp' " +
                "union all " +
                "select  'RDNP ' DIAGNOSTICO, count(*)TOTAL	 from NUEVA.dbo.fo " +
                "where left(DSD_FO,4)='RDnp' or left(DSi_FO,4)='RDnp' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            ////cmd.Parameters.AddWithValue("@dni", vDNI);
            //cmd.Parameters.AddWithValue("@vFecDesde", (Convert.ToDateTime(vtxtDesde)).Date);
            //cmd.Parameters.AddWithValue("@vFecHasta", (Convert.ToDateTime(vtxtHasta)).Date);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
            //CargaTabla(dtDato);

            DataTable dtDet = objdataset.Tables[0];
            double vTotAtenc = Convert.ToDouble(dtDet.Compute("SUM(Total)", "").ToString());

            //string vGraf1 = "<div id='chartContainer1' style='height: 500px; width: 100%;'></div> " +
            //    "<script type='text/javascript'> " +
            //    "window.onload = function () " +
            //    "{ var chart = new CanvasJS.Chart('chartContainer1', " +
            //    "{ animationEnabled: true, theme: 'light2', " +
            //    "title:{text: 'REPORTE DE PACIENTES POR SEXO'}, " +
            //    "toolTip:{shared:true}, " +
            //    "data: " +
            //    "[ ";
            //vGraf1 += "{type: 'column', " +
            //    "indexLabel: '{y}', indexLabelPlacement: 'inside', indexLabelFontColor: '#EEEEEE', " +
            //    "showInLegend: true, " +
            //    "name: 'Atenciones', " +
            //    "dataPoints: " +
            //    "[";
            //if (dtDet.Rows.Count > 0)
            //{
            //    int i = 0;
            //    foreach (DataRow dbRow in dtDet.Rows)
            //    {
            //        i++;
            //        //string vTotPorc = (Convert.ToDouble(dbRow["Total"].ToString()) / vTotAtenc * 100).ToString();
            //        string vTotPorc = ClassGlobal.CantPorc(vTotAtenc, Convert.ToDouble(dbRow["Total"].ToString()));
            //        vGraf1 += "{x: " + i + ", " +
            //            "label: '" + dbRow["EESS"].ToString() + " - Gen: " + dbRow["Genero"].ToString() + " [" + vTotPorc + "%]', " +
            //            "y: " + dbRow["Total"].ToString() + " }, ";

            //    }
            //}
            //vGraf1 += "]" +
            //"}, ";
            //vGraf1 += "] " +
            //"}); chart.render(); " +
            //"} " +
            //"</script>";

            //LitGraf1.Text = vGraf1;

            //string vGraf2 = "<div id='chartContainer2' style='height: 500px; width: 100%;'></div> " +
            //    "<script type='text/javascript'> " +
            //    "	var chart2 = new CanvasJS.Chart('chartContainer2',          " +
            //    "	{                                                         " +
            //    "		theme: 'light2',                                      " +
            //    "		title:{                                               " +
            //    "			text: 'Reporte'              " +
            //    "		},		                                              " +
            //    "		data: [                                               " +
            //    "		{                                                     " +
            //    "			type: 'pie',                                      " +
            //    "			showInLegend: true,                               " +
            //    "			toolTipContent: '{y} - #percent %',               " +
            //    "			yValueFormatString: '#,##0.##',         " +
            //    "			legendText: '{indexLabel}',                       " +
            //    "			dataPoints: [                                     ";

            //if (dtDet.Rows.Count > 0)
            //{
            //    int i = 0;
            //    foreach (DataRow dbRow in dtDet.Rows)
            //    {
            //        i++;
            //        vGraf2 += " { y: " + dbRow["Total"].ToString() + ", indexLabel: '" + dbRow["EESS"].ToString() + " - Gen: " + dbRow["Genero"].ToString() + "' }, ";

            //    }
            //}

            //vGraf2 += "			]        " +
            //    "		}                " +
            //    "		]                " +
            //    "	});                  " +
            //    "	chart2.render();     " +
            //    "</script>               ";

            //LitGraf2.Text = vGraf2;

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
