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

public partial class ASPX_IROJVAR_SISMED_DispoIROFechas : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        
        if (!Page.IsPostBack)
        {
            //CargaPeriodo();

            int vmonth = DateTime.Now.Month;
            //DDLMes.SelectedIndex = vmonth - 1;

            string vd, vm, vy;
            vd = DateTime.Now.Day.ToString();
            vm = DateTime.Now.Month.ToString();
            vy = DateTime.Now.Year.ToString();

            txtDesde.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            txtHasta.Text = vy + "-" + (vm).PadLeft(2, '0') + "-" + vd.PadLeft(2, '0');
            //DDLAnio.SelectedValue = vy;

        }
    }

    public void CargaPeriodo()
    {
        try
        {
            string qSql = "exec TabLibres.dbo.SP_Periodos '20220101', '20221215'; ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //DDLDesde.Items.Clear();
            //DDLHasta.Items.Clear();

            ////ListItem LisTMP = new ListItem("Todos", "", true);
            //DDLDesde.DataSource = dtDato;
            ////DDLDesde.Items.Add(LisTMP);
            //DDLDesde.DataTextField = "fPeriodo";
            //DDLDesde.DataValueField = "FechaMinF";
            //DDLDesde.DataBind();

            //DDLHasta.DataSource = dtDato;
            ////DDLHasta.Items.Add(LisTMP);
            //DDLHasta.DataTextField = "fPeriodo";
            //DDLHasta.DataValueField = "FechaMinF";
            //DDLHasta.DataBind();

        }
        catch (Exception ex)
        {
            LitTABL1.Text += ex.Message.ToString();
            //throw;
        }

    }


    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT("");
    }

    protected void btnPetitorio_Click(object sender, EventArgs e)
    {
        CargaTablaDT("1");
    }

    protected void btnNoPetitorio_Click(object sender, EventArgs e)
    {
        CargaTablaDT("0");
    }

    public void CargaTablaDT(string vPet)
    {
        //string vPeriodo = txtPeriodo.Text;
        DateTime vfDesde = DateTime.Parse(txtDesde.Text);
        DateTime vfHasta = DateTime.Parse(txtHasta.Text);

        try
        {
            string qSql = "exec SISMED.dbo.SP_JVAR_DispoSalma_x_Fechas @vfDes, @vfHas, @vPet ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@vfDes", vfDesde);
            cmd.Parameters.AddWithValue("@vfHas", vfHasta);
            cmd.Parameters.AddWithValue("@vPet", vPet);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            //GVtable.DataSource = dtDato;
            //GVtable.DataBind();

            int vNormoStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='NORMOSTOCK'").ToString());
            int vSobStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SOBRESTOCK'").ToString());
            int vSubStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SUBSTOCK'").ToString());
            int vSRStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='SINROTACION'").ToString());
            int vDesStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='DESABASTECIDO'").ToString());

            int vNCoStk = Convert.ToInt32(dtDato.Compute("count(CODSISMED)", "SSO='NoConsid'").ToString());

            int vItems = vNormoStk + vSobStk + vSubStk + vSRStk + vDesStk;
            double intvNS = (double)vNormoStk / (double)vItems * 100.00;
            double intvSob = (double)vSobStk / (double)vItems * 100.00;
            double intvSub = (double)vSubStk / (double)vItems * 100.00;
            double intvSR = (double)vSRStk / (double)vItems * 100.00;
            double intvDes = (double)vDesStk / (double)vItems * 100.00;
            double intvNCo = (double)vNCoStk / (double)vItems * 100.00;

            double intvDispo = intvNS + intvSob + intvSR;

            lblTitulo.Text = vfHasta.Year.ToString() + " - " + vfHasta.Month.ToString();

            lblNS.Text = ClassGlobal.formatoMillarDec(intvNS.ToString()) + " %";
            lblSob.Text = ClassGlobal.formatoMillarDec(intvSob.ToString()) + " %";
            lblSub.Text = ClassGlobal.formatoMillarDec(intvSub.ToString()) + " %";
            lblSR.Text = ClassGlobal.formatoMillarDec(intvSR.ToString()) + " %";
            lblDes.Text = ClassGlobal.formatoMillarDec(intvDes.ToString()) + " %";
            lblDisp.Text = ClassGlobal.formatoMillarDec(intvDispo.ToString()) + " %";

            lblNCo.Text = ClassGlobal.formatoMillarDec(intvNCo.ToString()) + " %";

            lblNSC.Text = vNormoStk.ToString();
            lblSobC.Text = vSobStk.ToString();
            lblSubC.Text = vSubStk.ToString();
            lblSRC.Text = vSRStk.ToString();
            lblDesC.Text = vDesStk.ToString();
            lblDispC.Text = vItems.ToString();

            lblNCoC.Text = vNCoStk.ToString();

            CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    public void CargaTabla(DataTable dtDato)
    {
        string html = "";
        int cantMes = Convert.ToInt32(txtCantMeses.Value);
        if (dtDato.Rows.Count > 0)
        {
            //html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
            html += Environment.NewLine + "<table id='tblbscrJS' class='table table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<tr>";
            html += "<th class=''></th>";
            html += "<th class=''>CodMED</th>";
            html += "<th class=''>Medicamento</th>";

            for (int i = 1; i <= cantMes; i++)
            {
                html += "<th class=''>Mes" + i + "</th>";
            }

            html += "<th style='text-align: center;'>Cons Prom</th>";
            html += "<th class='' style='text-align: center;'>Stock</th>";
            html += "<th class=''>Stock Mes</th>";

            html += "<th class='' style='text-align: center;'>SSO</th>";
            html += "<th class='' style='text-align: center;'>Pet</th>";

            html += "<th class='' style='text-align: center;'>Stock Farmacia</th>";
            html += "<th class='' style='text-align: center;'>Stock Almacen</th>";
            html += "<th class='' style='text-align: center;'>Precio Costo</th>";
            html += "<th class='' style='text-align: center;'>Fecha Min Venc</th>";

            html += "</tr>" + Environment.NewLine;
            int nroitem = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                string vFech = "", strFechVen = "";
                DateTime vfechaVen = DateTime.Now;
                if (dbRow["MinFechVen"] != DBNull.Value)
                {
                    vFech = dbRow["MinFechVen"].ToString().Substring(0, 10);
                    vfechaVen = DateTime.Parse(dbRow["MinFechVen"].ToString());
                }
                else { strFechVen = " class='bg-yellow'"; }

                if (vfechaVen < DateTime.Now && strFechVen == "")
                {
                    strFechVen = " class='bg-red'";
                }
                html += "<tr " + strFechVen + ">";
                html += "<td class='' style='text-align: left;'>" + nroitem + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["CODSISMED"].ToString() + "</td>";
                html += "<td class='' style='text-align: left;'>" + dbRow["Producto"].ToString() + "</td>";

                for (int i = 1; i <= cantMes; i++)
                {
                    html += "<td style='text-align: right;'>" + ClassGlobal.formatoMillar(dbRow["Mes" + i].ToString()) + "</td>";
                }

                html += "<td class='' style='text-align: right;'>" + ClassGlobal.formatoMillarDec(dbRow["ConsProm"].ToString()) + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["STK_TOTAL"].ToString() + "</td>";
                html += "<td class='' style='text-align: right;'>" + ClassGlobal.formatoMillarDec(dbRow["STK_MES"].ToString()) + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["SSO"].ToString() + "</td>";
                //html += "<td class='' style='text-align: center;'>" + dbRow["ESTADO"].ToString() + "</td>";
                html += "<td class='' style='text-align: center;'>" + dbRow["Pet"].ToString() + "</td>";
                html += "<td class='text-center'>" + dbRow["Stk_Disp"].ToString() + "</td>";
                html += "<td class='text-center'>" + dbRow["Stk_Alm"].ToString() + "</td>";
                html += "<td class='text-center'>" + ClassGlobal.formatoMillarDec(dbRow["PrecioCosto"].ToString()) + "</td>";

                html += "<td class='text-center'>" + vFech + "</td>";




                html += "</tr>" + Environment.NewLine;

            }

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        LitTABL1.Text = html;
        //LitDetAtenciones.Text = htmlDetAtenciones;
    }



}