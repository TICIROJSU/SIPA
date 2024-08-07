using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class IROTWeb_wPac_Consultorio_ConsRegistro : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {

        tdDiaHIS.InnerText = DateTime.Now.Day.ToString();
        tdMesHIS.InnerText = DateTime.Now.Month.ToString();
        tdAnioHIS.InnerText = DateTime.Now.Year.ToString();

        var fecTardeM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 1, 0);
        var fecActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

        // Turno Tarde
        if (fecActual >= fecTardeM){ cboSerTurno.Value = "T"; }
        else { cboSerTurno.Value = "M"; }

        loadProfesional();
        loadServicio();
        loadFuenteFin();
        loadEtniaProced();
    }

    [WebMethod]
    public static string GetMBuscaDxCIEX(string vDes)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "SELECT CODIGO, COD_ENF AS CIE, upper(DESC_ENF) AS DIAGNOSTICO, TABLA " +
                " FROM NUEVA.dbo.ENFERMEDADES " +
                " WHERE (COD_ENF + DESC_ENF) like '%' + @DES + '%' ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@DES", vDes);
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gHTML += "<tr onclick='fnMDxSelect(this)'>";
                    gHTML += "<td>" + dbRow["DIAGNOSTICO"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE"].ToString() + "</td>";
                    gHTML += "<td style='display: none'>" + dbRow["CODIGO"].ToString() + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetbuscarHIS(string vAnio, string vMes, string vDia, string vPlaza, string vCodServ1, string vTurno)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = " SELECT  NUM_REG AS Nro, DIA, FICHAFAM AS HHCC, COD_DPTO + '' +COD_PROV + '' + COD_DIST UBIGEO, " +
                " EDAD, TIP_EDAD AS T, SEXO AS S, ESTABLEC AS E, SERVICIO AS S, " +
                " DIAGNOST1 AS TD1, LABCONF1 AS LB1, CODI1 AS CIE1, DIAGNOST2 AS TD2, LABCONF2 AS LB2, CODI2 AS CIE2, " +
                " DIAGNOST3 AS TD3, LABCONF3 AS LB3, CODI3 AS CIE3, DIAGNOST4 AS TD4, LABCONF4 AS LB4, CODI4 AS CIE4, " +
                " DIAGNOST5 AS TD5, LABCONF5 AS LB5, CODI5 AS CIE5, DIAGNOST6 AS TD6, LABCONF6 AS LB6, CODI6 AS CIE6, " +
                " DX1, DX2, DX3, DX4, DX5, DX6, " +
                " SUBSTRING(DNI, 5,8) AS DNI, COD_FN + ': ' + DES_FN AS FI, COD_ET + ': ' + DES_ET AS ET, ID_HIS, " +
                " '' AS 'O.LAB.', '' AS 'O.CIR.' " +
                " FROM NUEVA.dbo.CHEQ2011	" +
                " 	INNER JOIN NUEVA.dbo.FINANCIADOR ON FI=COD_FN " +
                " 	INNER JOIN NUEVA.dbo.ETNIA ON ET=COD_ET " +
                " WHERE ANO = @Anio AND MES = @Mes and DIA = @Dia AND PLAZA = @Plaza AND COD_SERVSA1 = @CodServ1 and MT = @MT " +
                " ORDER BY NUM_PAG, NUM_REG ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();

            cmd2.Parameters.AddWithValue("@Anio", vAnio);
            cmd2.Parameters.AddWithValue("@Mes", vMes);
            cmd2.Parameters.AddWithValue("@Dia", vDia);
            cmd2.Parameters.AddWithValue("@Plaza", vPlaza);
            cmd2.Parameters.AddWithValue("@CodServ1", vCodServ1);
            cmd2.Parameters.AddWithValue("@MT", vTurno);
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gHTML += "<tr>";
                    gHTML += "<td>" + dbRow["Nro"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["DIA"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["HHCC"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["EDAD"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["T"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["S"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD1"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB1"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE1"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD2"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB2"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE2"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD3"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB3"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE3"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD4"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB4"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE4"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD5"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB5"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE5"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["TD6"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["LB6"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["CIE6"].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["O.LAB."].ToString() + "</td>";
                    gHTML += "<td>" + dbRow["O.CIR."].ToString() + "</td>";
                    gHTML += "<td>" + "" + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    [WebMethod]
    public static string GetPacienteHC(string vHC, string vServ)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select *, " +
                "(case when annioedad > 0 then annioedad else (case when mesedad > 0 then mesedad else (diaedad) end) end) as EdadNum, " +
                "(case when annioedad > 0 then 'A' else (case when mesedad > 0 then 'M' else ('D') end) end) as EdadTip " +
                "from ( " +
                "	select *, " +
                "		DATEPART(DAY, GETDATE()) - DATEPART(DAY, IFN) as diaedad, " +
                "		DATEDIFF(MONTH, IFN, GETDATE())%12 as mesedad, " +
                "		FLOOR(DATEDIFF(day, IFN, GETDATE()) / 365.25) as annioedad, " +
                "		year(getdate())-YEAR(IFN) edadregistro " +
                "	from NUEVA.dbo.HISTORIA where IHC = @vIHC " +
                ") Tab1; " +
                "Select top 1 *, (case when ANO = year(GETDATE()) then 'C' else 'R' end) pEST " +
                "FROM NUEVA.dbo.CHEQ2011 " +
                "WHERE FICHAFAM = @vIHC " +
                "order by ANO desc; " +
                "Select *, (case when ANO = year(GETDATE()) then 'C' else 'R' end) pEST " +
                "FROM NUEVA.dbo.CHEQ2011 " +
                "WHERE FICHAFAM = @vIHC and ( (ANO = year(GETDATE()) and COD_SERVSA1 = @vServ) or (ANO < year(GETDATE()) and COD_SERVSA1 in (@vServ, '71', '50', '26')) ) " +
                "order by ANO desc; " +
                "select * from NUEVA.dbo.SIS where cast(FEC_SIS as date)=cast(getdate() as date) and NHC_SIS=@vIHC and NUE_SIS<>'0'; " +
                "select * from NUEVA.dbo.PAPELETA where cast(Fec_PT as date)=cast(getdate() as date) and HC_PT=@vIHC; ";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            cmd2.Parameters.AddWithValue("@vIHC", vHC);
            cmd2.Parameters.AddWithValue("@vServ", vServ);
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtOT = objdataset.Tables[0];
            DataTable dtEst = objdataset.Tables[1];
            DataTable dtServ = objdataset.Tables[2];

            DataTable dtSIS = objdataset.Tables[3];
            DataTable dtPAP = objdataset.Tables[4];

            if (dtOT.Rows.Count > 0)
            {
                string vCondEst = "N", vCondSer = "N", vFI = "1";
                if (dtEst.Rows.Count > 0)
                { vCondEst = dtEst.Rows[0]["pEST"].ToString(); }
                if (dtServ.Rows.Count > 0)
                { vCondSer = dtServ.Rows[0]["pEST"].ToString(); }

                if (dtSIS.Rows.Count > 0)
                { vFI = "2"; }
                else if (dtPAP.Rows.Count > 0)
                { vFI = "11"; }

                gHTML += dtOT.Rows[0]["IDNI"].ToString() + "||sep||"; // DNI
                gHTML += dtOT.Rows[0]["IAP"].ToString() + " " + dtOT.Rows[0]["IAM"].ToString() + "||sep||"; // Apellidos
                gHTML += dtOT.Rows[0]["INO"].ToString() + "||sep||"; // Nombres
                gHTML += dtOT.Rows[0]["IDB"].ToString() + "||sep||"; // Diabetico IDB 0-No, 1-Si 
                gHTML += dtOT.Rows[0]["EdadNum"].ToString() + "||sep||"; // Edad Numero
                gHTML += dtOT.Rows[0]["EdadTip"].ToString() + "||sep||"; // Edad Tipo
                gHTML += dtOT.Rows[0]["ISE"].ToString() + "||sep||"; // Sexo
                gHTML += dtOT.Rows[0]["IPC"].ToString() + "||sep||"; // Ubigeo
                gHTML += dtOT.Rows[0]["ILN"].ToString() + "||sep||"; // Distrito
                gHTML += vCondEst + "||sep||"; // Estb
                gHTML += vCondSer + "||sep||"; // Serv
                gHTML += vFI + "||sep||"; // Financiador de Salud
                //gHTML += dtOT.Rows[0][""].ToString() + "||sep||"; // Procedencia Etnica
            }
            else
            {
                gHTML += "No encontrado||sep||Sin Registros||sep||No Encontrado||sep||-||sep||" +
                    "-||sep||-||sep||-||sep||-||sep||-||sep||-||sep||" +
                    "-||sep||-||sep||-||sep||-||sep||-||sep||-||sep||" +
                    "-||sep||-||sep||-||sep||-||sep||-||sep||-||sep||" +
                    "-||sep||-||sep||-||sep||-||sep||";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

    protected void loadProfesional()
    {
        string vCodUsu = "0"; //228-JHUAMANP
        //if (Request.Cookies.Get("idUser") != null)
        //{ vCodUsu = Request.Cookies.Get("idUser").Value; }
        if (Session["idUser2"] != null)
        { vCodUsu = Session["idUser2"].ToString(); }
        try
        {
            string qSql = "SELECT APE_PER + ': ' + HIS_PER as APE_PER, HIS_PER, COD_PER, COD_PRO " +
                "FROM NUEVA.dbo.PERSONAL WHERE USU_PER=@USU_PER ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("@USU_PER", vCodUsu);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            tdPersonalID.InnerText = objdataset.Tables[0].Rows[0]["APE_PER"].ToString();
            tdPerCodProfH.Value = objdataset.Tables[0].Rows[0]["COD_PRO"].ToString();
            tdPersonalHis.Value = objdataset.Tables[0].Rows[0]["HIS_PER"].ToString();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message.ToString();
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }

    }

    protected void loadServicio()
    {
        string vPerCodProf = tdPerCodProfH.Value;
        string vCodSer = "";
        try
        {
            switch (vPerCodProf)
            {
                case "01":
                case "02":
                    vCodSer = " and COD_SER NOT IN('03','72','37','90','91','06','80') ";
                    break;
                case "04":
                case "05":
                    vCodSer = " and COD_SER IN ('03','72','37','90','91','69','40','81','2s') ";
                    break;
                case "09":
                    vCodSer = " and COD_SER IN ('04') ";
                    break;
                case "10":
                    vCodSer = " and COD_SER IN ('06') ";
                    break;
                default:
                    vCodSer = " and COD_SER = '00' ";
                    break;
            }

            string qSql = "SELECT COD_SER, DES_SER + ': ' + HIS_SER + ': ' + COD_SER AS [DESCRIPCION] " +
                "FROM NUEVA.dbo.SERVICIO " +
                "WHERE HIS_SER > 0 " + vCodSer + " and EST_SER = 1 " +
                "ORDER BY DES_SER ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            ListItem LisTMP = new ListItem("", "0", true);
            ddlServicio.DataSource = objdataset.Tables[0];
            ddlServicio.Items.Add(LisTMP);
            ddlServicio.DataTextField = "DESCRIPCION";
            ddlServicio.DataValueField = "COD_SER";
            ddlServicio.DataBind();

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message.ToString();
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }

    }

    protected void loadFuenteFin()
    {
        try
        {
            string qSql = "SELECT COD_FN, COD_FN + ': ' + DES_FN AS [DESCRIPCION] " +
                "FROM NUEVA.dbo.FINANCIADOR " +
                "WHERE COD_FN IN ('1','2','11') " +
                "ORDER BY COD_FN ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            ddlPacFinS.DataSource = objdataset.Tables[0];
            ddlPacFinS.DataTextField = "DESCRIPCION";
            ddlPacFinS.DataValueField = "COD_FN";
            ddlPacFinS.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message.ToString();
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }

    }

    protected void loadEtniaProced()
    {
        try
        {
            string qSql = "SELECT (case when COD_ET = '80' then 1 else 2 end) as orden, COD_ET, COD_ET + ': ' + DES_ET AS [DESCRIPCION] " +
                "FROM NUEVA.dbo.ETNIA ORDER BY 1, COD_ET  ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            ddlPacEtn.DataSource = objdataset.Tables[0];
            ddlPacEtn.DataTextField = "DESCRIPCION";
            ddlPacEtn.DataValueField = "COD_ET";
            ddlPacEtn.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message.ToString();
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('" + ex.Message.ToString() + "');</script>");
        }

    }

}