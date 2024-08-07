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

public partial class ASPX_ExtIROJVAR_Foto_fo : System.Web.UI.Page
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

        //CargaImagen();
    }

    protected void bntBuscar_Click(object sender, EventArgs e)
    {
        CargaTablaDT();
    }

    public void CargaImagen()
    {
        string ftp = "ftp://150.10.9.219/";

        //FTP Folder name. Leave blank if you want to list files from root folder.
        string ftpFolder = "HRDT/IDENTIFICADAS1/%C3%91ASCO%20VERA%20DOMINGA%20PERPETUA%20010816%2017905177/";
        try
        {
            string fileName = "17905177-01-OD-010816.jpg";
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //Enter FTP Server credentials.
            request.Credentials = new NetworkCredential("Juanky", "Iro.123");
            request.UsePassive = true;
            request.UseBinary = true;
            request.EnableSsl = false;

            //Fetch the Response and read it into a MemoryStream object.
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            using (MemoryStream stream = new MemoryStream())
            {
                response.GetResponseStream().CopyTo(stream);
                string base64String = Convert.ToBase64String(stream.ToArray(), 0, stream.ToArray().Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
            }
        }
        catch (WebException ex)
        {
            throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
        }

    }

    public void CargaTablaDT()
    {
        string vEESS = ddlEESS.SelectedValue;
        string vDNI = txtDNI.Text;
        string vtxtDesde = txtFechaDesde.Text;
        string vtxtHasta = txtFechaHasta.Text;

        string vSQLn = "", vSqlEESS = "", vSqlDNI = "", vSqlFecha = "";

        if (chkEESS.Checked)    { vSqlEESS  = " EST_FO like '%' + @vEESS + '%' "; }
        if (chkDNI.Checked)     { vSqlDNI   = " DNI_FO + APP_FO + APM_FO + NOM_FO like '%' + @vDNI + '%' "; }
        if (chkFechas.Checked)  { vSqlFecha = " (FEC_FO between @vFecDesde and @vFecHasta) "; }

        if (chkEESS.Checked)    { vSQLn = vSqlEESS; }
        if (chkDNI.Checked)     { vSQLn = vSqlDNI; }
        if (chkFechas.Checked)  { vSQLn = vSqlFecha; }
        if (chkDNI.Checked && chkFechas.Checked)  { vSQLn = vSqlDNI + " and " + vSqlFecha; }
        if (chkEESS.Checked && chkFechas.Checked) { vSQLn = vSqlEESS + " and " + vSqlFecha; }
        if (chkEESS.Checked && chkDNI.Checked && chkFechas.Checked) { vSQLn = vSqlEESS + " and " + vSqlDNI + " and " + vSqlFecha; }

        string gHTML = "";
        try
        {
            //con.Open();
            //string qSql = "SELECT *, APP_FO + ' ' + APM_FO + ' ' + NOM_FO AS PacienteNom " +
            //    "FROM NUEVA.dbo.FO " +
            //    "where DNI_FO + APP_FO + APM_FO + NOM_FO like '%' + @dni + '%' " +
            //    "order by FEC_FO desc ; ";
			//[ID_FO] as ID,
            string qSql = "SELECT  [FEC_FO] as  Fecha, [DNI_FO] DNI, [APP_FO] + ' ' + [APM_FO] + ' ' + [NOM_FO] as Paciente, " +
                "[EDA_FO] as Edad, [SEX_FO] as Sexo,[ESS_FO] as EESS,[AVSCD_FO] as AVSCD,[AVCCD_FO] as AVCCD, " +
                "[PID_FO] as PID, [AVSCI_FO] as AVSCI, [AVCCI_FO],[PII_FO],[TMD_FO],[FDO_FO], [NTF_FO], [DRR_FO], " +
                "[TES_FO],[TSG_FO],[DXP_FO],[DSD_FO] ,[DSI_FO],[REF_FO],[CTR_FO],[DRA_FO],[OBS_FO] ,[USU_FO], " +
                "[DCU_FO],[EST_FO],NSD,PDAUO,DPDAUO,PCAUO,DPCAUO " +
                "from NUEVA.dbo.FO " +
                "where " + vSQLn + 
                "order by FEC_FO desc ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            //cmd.Parameters.AddWithValue("@dni", vDNI);
            if (chkEESS.Checked)    { cmd.Parameters.AddWithValue("@vEESS", vEESS); }
            if (chkDNI.Checked)     { cmd.Parameters.AddWithValue("@vDNI", vDNI); }
            if (chkFechas.Checked)
            {
                cmd.Parameters.AddWithValue("@vFecDesde", (Convert.ToDateTime(vtxtDesde)).Date);
                cmd.Parameters.AddWithValue("@vFecHasta", (Convert.ToDateTime(vtxtHasta)).Date);
            }
            
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
            //CargaTabla(dtDato);
            
            DataTable dtDatoDetAt = objdataset.Tables[0];

            // if (dtDatoDetAt.Rows.Count > 0)
            if (20 > 30)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding' id='doublescroll' >";
                gHTML += "<table id='tblbscrJS' class='table table-bordered table-hover' style='text-align: left; font-size: 14px; '>";
                gHTML += "<tr><th class=''>N° </th>";
                gHTML += "<th class=''>Fecha</th>";
                gHTML += "<th class=''>DNI</th>";
                gHTML += "<th class=''>Paciente</th>";
                gHTML += "<th class=''>Edad</th>";
                gHTML += "<th class=''>EESS</th>";
                gHTML += "<th class=''>AVSCD</th>";
                gHTML += "<th class=''>AVCCD</th>";
                gHTML += "<th class=''>PID</th>";
                gHTML += "<th class=''>AVSCI</th>";
                gHTML += "<th class=''>AVCCI</th>";
                gHTML += "<th class=''>PII</th>";
                gHTML += "<th class=''>USUARIO</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gHTML += "<tr>";
                    gHTML += "<td class='' >" + nroitem + "</td>";
                    gHTML += "<td class='' >" + dbRow["FEC_FO"].ToString().Substring(0, 10) + "</td>";
                    gHTML += "<td class='' >" + dbRow["DNI_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["PacienteNom"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["EDA_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["ESS_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["AVSCD_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["AVCCD_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["PID_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["AVSCI_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["AVCCI_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["PII_FO"].ToString() + "</td>";
                    gHTML += "<td class='' >" + dbRow["USU_FO"].ToString() + "</td>";
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

        chkDNI.Checked = true;
        chkEESS.Checked = true;
        chkFechas.Checked = true;
    }

}
