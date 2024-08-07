using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_SISMED_ComparaDispoSIS : System.Web.UI.Page
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
        string vEESS = txtDato.Text;
        try
        {
            //con.Open();
            string qSql = "select * " +
                "from TabLibres.dbo.SIS_TABLERODISPOMED SISDisp " +
                "left join IROf.dbo.ztmp_SPW_CONSUMOANUAL_602 tmpDisp on SISDisp.CODMED_SIS COLLATE Modern_Spanish_CI_AS = tmpDisp.CODSISMED COLLATE Modern_Spanish_CI_AS " +
                "order by [MEDICAMENTO/INSUMO] asc, CONSUMO_PROMEDIO desc  ";

            qSql = "select LEFT(EESS_SIS, 5) as EESS, CODMED_SIS as CODMED, DESCRIPCIONMED_SIS as DescripcionMed, " +
                "	TIPMED_SIS as TIP, format(CPMA_SIS, '#####0.00') as CPMA_SIS, STK_SISMED_SIS as Stk_SIS, format(STK_SISMED_SIS / CPMA_SIS, '#####0') [STK_Mes SIS], LEFT(ESTADO_SIS, 7) as SSO_SIS,  " +
                "	'¶' as IrD, C#C, " +
                "	MES9, MES10, MES11, MES12, MES13, N_MESES_CONSUMIDOS AS Meses, CONSUMO_PROMEDIO as ConsProm, STOCK_TOTAL as Stk_Tot, format(STOCK_TOTAL / CONSUMO_PROMEDIO, '#####0') as STK_MesIRO, LEFT(SSO, 7) as SSO_IRO, " +
                "	'█' as Prb, CODSISMED_PROB as CODMED_Prob, [DESCRIPCION SISMED] + '-' + isnull(CONCENTRACION, '') + '-' + isnull(FORMA_FARMACEUTICA, '') + '-' + isnull(PRESENTACION, '') as DescripcionProbable, (CASE when SSO is null then 0 else 1 end) as ord1 " +
                "from TabLibres.dbo.SIS_TABLERODISPOMED SISDisp " +
                "left join ( " +
                "		select CODSISMED, MAX(CODSIGA) AS CODSIGA, MAX([MEDICAMENTO/INSUMO]) AS [MEDICAMENTO/INSUMO], MAX(TIPO) AS TIPO, " +
                "		MAX(PETITORIO) AS PETITORIO, MAX([C#C]) AS [C#C], MAX([DEPARTAMENTO/SERVICIO]) AS [DEPARTAMENTO/SERVICIO], " +
                "		SUM(CAST(MES1 AS INT)) AS MES1, SUM(CAST(MES2 AS INT)) AS MES2, SUM(CAST(MES3 AS INT)) AS MES3, " +
                "		SUM(CAST(MES4 AS INT)) AS MES4, SUM(CAST(MES5 AS INT)) AS MES5, SUM(CAST(MES6 AS INT)) AS MES6, " +
                "		SUM(CAST(MES7 AS INT)) AS MES7, SUM(CAST(MES8 AS INT)) AS MES8, SUM(CAST(MES9 AS INT)) AS MES9, " +
                "		SUM(CAST(MES10 AS INT)) AS MES10, SUM(CAST(MES11 AS INT)) AS MES11, SUM(CAST(MES12 AS INT)) AS MES12, " +
                "		SUM(CAST(MES13 AS INT)) AS MES13, " +
                "		MAX(CAST( cast(CONSUMO_TOTAL as decimal(12,0)) AS INT)) AS CONSUMO_TOTAL, " +
                "		SUM(CAST( cast(CONSUMO_MINIMO as decimal(12,0)) AS INT)) AS CONSUMO_MINIMO, " +
                "		SUM(CAST( cast(CONSUMO_MAXIMO as decimal(12,0)) AS INT)) AS CONSUMO_MAXIMO, " +
                "		SUM(CAST(CONSUMO_PROMEDIO AS DECIMAL(10, 2))) AS CONSUMO_PROMEDIO, " +
                "		SUM(CAST( cast(N_MESES_CONSUMIDOS as decimal(12,0)) AS INT)) AS N_MESES_CONSUMIDOS, " +
                "		SUM(CAST(CONSUMO_PROMEDIO_AJUSTADO AS DECIMAL(10, 2))) AS CONSUMO_PROMEDIO_AJUSTADO, " +
                "		SUM(CAST(STOCK_ALMACEN AS DECIMAL(10, 0))) AS STOCK_ALMACEN, SUM(CAST(STOCK_DISPENSACION AS DECIMAL(10, 0))) AS STOCK_DISPENSACION, " +
                "		SUM(CAST(STOCK_TOTAL AS DECIMAL(10, 0))) AS STOCK_TOTAL, SUM(CAST(STOCK_MES AS DECIMAL(10, 2))) AS STOCK_MES, MAX(SSO) AS SSO, " +
                "		SUM(CAST(STOCK_MES_AJUSTADO AS DECIMAL(10, 2))) AS STOCK_MES_AJUSTADO, MAX(SSO_AJUSTADO) AS SSO_AJUSTADO " +
                "	from IROf.dbo.ztmp_SPW_CONSUMOANUAL_602 " +
                "	GROUP BY CODSISMED " +
                ") tmpDisp on SISDisp.CODMED_SIS COLLATE Modern_Spanish_CI_AS = tmpDisp.CODSISMED COLLATE Modern_Spanish_CI_AS " +
                "left join IROf.dbo.CatSISMED on SISDisp.CODSISMED_PROB COLLATE Modern_Spanish_CI_AS = CatSISMED.COD_SISMED COLLATE Modern_Spanish_CI_AS " +
                "order by ord1, TIPMED_SIS desc, DESCRIPCIONMED_SIS asc, CONSUMO_PROMEDIO desc ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Dato", vEESS);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            GVtable.DataSource = dtDato;
            GVtable.DataBind();
            
            int v1NormoStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='NORMOST'").ToString());
            int v1SobStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='SOBRE S'").ToString());
            int v1SubStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='SUBSTOC'").ToString());
            int v1SRStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='SIN ROT'").ToString());
            int v1DesStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='DESABAS'").ToString());
            int v1NCoStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_SIS='NO CONS'").ToString());

            int v1Items = v1NormoStk + v1SobStk + v1SubStk + v1SRStk + v1DesStk;
            double intv1NS = (double)v1NormoStk / (double)v1Items * 100.00;
            double intv1Sob = (double)v1SobStk / (double)v1Items * 100.00;
            double intv1Sub = (double)v1SubStk / (double)v1Items * 100.00;
            double intv1SR = (double)v1SRStk / (double)v1Items * 100.00;
            double intv1Des = (double)v1DesStk / (double)v1Items * 100.00;
            double intv1NCo = (double)v1NCoStk / (double)v1Items * 100.00;
            double intv1Dispo = intv1NS + intv1Sob + intv1SR;

            lbl1NS.Text = ClassGlobal.formatoMillarDec(intv1NS.ToString()) + " %";
            lbl1Sob.Text = ClassGlobal.formatoMillarDec(intv1Sob.ToString()) + " %";
            lbl1Sub.Text = ClassGlobal.formatoMillarDec(intv1Sub.ToString()) + " %";
            lbl1SR.Text = ClassGlobal.formatoMillarDec(intv1SR.ToString()) + " %";
            lbl1Des.Text = ClassGlobal.formatoMillarDec(intv1Des.ToString()) + " %";
            lbl1Disp.Text = ClassGlobal.formatoMillarDec(intv1Dispo.ToString()) + " %";
            lbl1NCo.Text = ClassGlobal.formatoMillarDec(intv1NCo.ToString()) + " %";

            lbl1NSC.Text = v1NormoStk.ToString();
            lbl1SobC.Text = v1SobStk.ToString();
            lbl1SubC.Text = v1SubStk.ToString();
            lbl1SRC.Text = v1SRStk.ToString();
            lbl1DesC.Text = v1DesStk.ToString();
            lbl1DispC.Text = v1Items.ToString();
            lbl1NCoC.Text = v1NCoStk.ToString();

            //////////////////////////////////////////////////////

            int v2NormoStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='NORMOST'").ToString());
            int v2SobStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='SOBREST'").ToString());
            int v2SubStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='SUBSTOC'").ToString());
            int v2SRStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='SIN ROT'").ToString());
            int v2DesStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='DESABAS'").ToString());
            int v2NCoStk = Convert.ToInt32(dtDato.Compute("count(CODMED)", "SSO_IRO='NO CONS'").ToString());

            int v2Items = v2NormoStk + v2SobStk + v2SubStk + v2SRStk + v2DesStk;
            double intv2NS = (double)v2NormoStk / (double)v2Items * 100.00;
            double intv2Sob = (double)v2SobStk / (double)v2Items * 100.00;
            double intv2Sub = (double)v2SubStk / (double)v2Items * 100.00;
            double intv2SR = (double)v2SRStk / (double)v2Items * 100.00;
            double intv2Des = (double)v2DesStk / (double)v2Items * 100.00;
            double intv2NCo = (double)v2NCoStk / (double)v2Items * 100.00;
            double intv2Dispo = intv2NS + intv2Sob + intv2SR;

            lbl2NS.Text = ClassGlobal.formatoMillarDec(intv2NS.ToString()) + " %";
            lbl2Sob.Text = ClassGlobal.formatoMillarDec(intv2Sob.ToString()) + " %";
            lbl2Sub.Text = ClassGlobal.formatoMillarDec(intv2Sub.ToString()) + " %";
            lbl2SR.Text = ClassGlobal.formatoMillarDec(intv2SR.ToString()) + " %";
            lbl2Des.Text = ClassGlobal.formatoMillarDec(intv2Des.ToString()) + " %";
            lbl2Disp.Text = ClassGlobal.formatoMillarDec(intv2Dispo.ToString()) + " %";
            lbl2NCo.Text = ClassGlobal.formatoMillarDec(intv2NCo.ToString()) + " %";

            lbl2NSC.Text = v2NormoStk.ToString();
            lbl2SobC.Text = v2SobStk.ToString();
            lbl2SubC.Text = v2SubStk.ToString();
            lbl2SRC.Text = v2SRStk.ToString();
            lbl2DesC.Text = v2DesStk.ToString();
            lbl2DispC.Text = v2Items.ToString();
            lbl2NCoC.Text = v2NCoStk.ToString();

        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    protected void ExportarExcel2_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        response.Write(LitTABL1.Text);
        response.End();
    }

    protected void ExportarExcel_Click(object sender, EventArgs e)
    {
        HttpResponse response = Response;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pageToRender = new Page();
        HtmlForm form = new HtmlForm();
        form.Controls.Add(GVtable);
        pageToRender.Controls.Add(form);
        response.Clear();
        response.Buffer = true;
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=Reporte.xls");
        response.Charset = "UTF-8";
        response.ContentEncoding = Encoding.Default;
        pageToRender.RenderControl(htw);
        response.Write(sw.ToString());
        response.End();
    }

}
