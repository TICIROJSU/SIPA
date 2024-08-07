using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_ExtIROJVAR_Foto_FOxDisminucionVis : System.Web.UI.Page
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
            string qSql = "SELECT tOrden.rFila, tOrden.EESS, PDAUO, COUNT(*) AS Total " +
                "FROM            nueva.dbo.FO " +
                "left join ( select ROW_NUMBER() OVER(ORDER BY EST_FO DESC) AS rFila, EST_FO as EESS " +
                "   from NUEVA.dbo.fo group by EST_FO " +
                "   ) tOrden on EST_FO = tOrden.EESS " +
                "where FEC_FO >= @vFecDesde and FEC_FO <= @vFecHasta " +
                "GROUP BY PDAUO, tOrden.rFila, tOrden.EESS " +
                "ORDER BY tOrden.EESS ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@dni", vDNI);
            cmd.Parameters.AddWithValue("@vFecDesde", (Convert.ToDateTime(vtxtDesde)).Date);
            cmd.Parameters.AddWithValue("@vFecHasta", (Convert.ToDateTime(vtxtHasta)).Date);

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
            
            string vGraf1 = "<div id='chartc1' style='height: 500px; width: 100%;'></div> " + Environment.NewLine +
                "<script type='text/javascript'> " + Environment.NewLine +
                "window.onload = function () { " + Environment.NewLine +
                "	var chart = new CanvasJS.Chart('chartc1', " + Environment.NewLine +
                "	{ " + Environment.NewLine +
                //"       animationEnabled: true, " + Environment.NewLine +
                "       theme: 'light2', " + Environment.NewLine +
                "		title:{ " + Environment.NewLine +
                "			text: '¿Disminución de visión en al menos un ojo debido a la RD?'  " + Environment.NewLine +
                "		},  " + Environment.NewLine +
                "		axisY:{ " + Environment.NewLine +
                "			title:'Atenciones', " + Environment.NewLine +
                //"			valueFormatString: '#0.#,.',  " + Environment.NewLine +
                "		},  " + Environment.NewLine +
                "		data: [ " + Environment.NewLine;
            string vNameDXP = "vNameDXP_ValorInicialSinSentido";
            for (int i = 0; i < dtDet.Rows.Count; i++)
            {
                DataRow dbRow = dtDet.Rows[i];
                if (vNameDXP != dbRow["PDAUO"].ToString())
                {
                    vNameDXP = dbRow["PDAUO"].ToString();
                    if (i > 0) { vGraf1 += " ]  }, " + Environment.NewLine; }
                    vGraf1 += "		{ " + Environment.NewLine +
                        "			type: 'stackedColumn100',  " + Environment.NewLine +
                        "			legendText: '" + dbRow["PDAUO"].ToString() + "',  " + Environment.NewLine +
                        "			name: '" + dbRow["PDAUO"].ToString() + "',  " + Environment.NewLine +
                        "			showInLegend: 'true', " + Environment.NewLine +
                        "			indexLabel: '{y}|[#percent]% ',  " + Environment.NewLine +
                        "			toolTipContent: '{name},<br />{label}: {y} - #percent %,<br />Total: [#total]', " + Environment.NewLine +
                        "			indexLabelFontColor: 'white',  " + Environment.NewLine +
                        "			indexLabelBackgroundColor: 'black',  " + Environment.NewLine +
                        "			dataPoints: [  " + Environment.NewLine;
                }
                vGraf1 += "				{x:" + dbRow["rFila"].ToString() + ", " +
                    "                   y: " + dbRow["Total"].ToString() + " , " +
                    "                   label: '" + dbRow["EESS"].ToString() + "'" +
                    "                   },  " + Environment.NewLine;
            }
            vGraf1 += "			]  " + Environment.NewLine +
                    "		}, " + Environment.NewLine;

            vGraf1 += "		]  " + Environment.NewLine +
                "	});  " + Environment.NewLine +
                "	chart.render();  " + Environment.NewLine +
                "} " + Environment.NewLine +
                "</script> " + Environment.NewLine;

            LitGraf1.Text = vGraf1;

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
