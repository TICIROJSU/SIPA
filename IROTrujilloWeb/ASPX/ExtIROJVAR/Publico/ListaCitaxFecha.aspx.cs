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
//using System.Speech.Synthesis;

public partial class ASPX_ExtIROJVAR_Publico_ListaCitaxFecha : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetListaCitados(string var)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select FORMAT( FEC_TAR, 'dd/MM/yyyy') as Fecha, HOR_TAR as Hora, TUR_TAR as Tur, HIC_TAR as HistClin, CONCAT(IAP, ' ', IAM, ', ', INO) as Paciente, SER_TAR as Ser, DES_SER as Servicio " +
                "from nueva.dbo.TARJETON " +
                "left join NUEVA.dbo.HISTORIA on HIC_TAR = IHC " +
                "left join NUEVA.dbo.SERVICIO on SER_TAR = COD_SER " +
                "where cast(FEC_TAR as date) = cast(GETDATE() as date) " +
                "	and cast(HOR_TAR as time) < cast(DATEADD(HOUR, 1, GETDATE()) as time) " +
                "	and concat(HIC_TAR, SER_TAR) not in ( " +
                "		select busqueda " +
                "		from TabLibres.dbo.BusqTarjetonJVAR " +
                "	) " +
                "order by TUR_TAR, HOR_TAR, Paciente ";

            qSql = "select top 13 FORMAT( FEC_TAR, 'dd/MM') as Fecha, HOR_TAR Hora, TUR_TAR as Tur, HIC_TAR HHCC, iap + ' ' + ias + ' ' + ino  as Paciente, des_ser Servicio, PIS_SER as Piso, ape_per as Responsable " +
                "from NUEVA.dbo.TARJETON " +
                "	inner join NUEVA.dbo.HISTORIA on HIC_TAR = IHC " +
                "	inner join NUEVA.dbo.SERVICIO on COD_SER = SER_TAR " +
                "	inner join NUEVA.dbo.PERSONAL on COD_PER = COM_TAR " +
                "where cast(FEC_TAR as date) = cast(GETDATE() as date) and TIP_TAR = 'C' " +
                "	and CIT_TAR = 'X' and PIS_SER = '3' and TUR_TAR = 'M' " +
                "order by TUR_TAR, HOR_TAR, iap ";

            qSql = "SELECT PIS_SER PISO , IAP + ' ' +IAS + ' ' + INO PACIENTE, DES_SER SERVICIO, DSC_SER, APE_PER RESPONSABLE " +
                "FROM NUEVA.dbo.CHEQ2011 " +
                "INNER JOIN NUEVA.dbo.SERVICIO ON COD_SER=COD_SERVSA1 " +
                "INNER JOIN NUEVA.dbo.PERSONAL ON PLAZA=HIS_PER " +
                "INNER JOIN NUEVA.dbo.HISTORIA ON IHC=FICHAFAM " +
                "WHERE ANO=2023 AND ESTLOTPAG = 'IA' " +
                "order by REG desc ";

            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gHTML += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gHTML += "<table class='table table-hover' style='text-align: right; font-size: 24px; '>";
                //gHTML += "<th class=''>Fecha </th>";
                //gHTML += "<th class=''>Hora</th>";
                gHTML += "<th class=''>Piso</th>";
                //gHTML += "<th class=''>HHCC</th>";
                gHTML += "<th class=''>Paciente</th>";
                gHTML += "<th class=''>Servicio</th>";
                //gHTML += "<th class=''>Responsable</th>";
                gHTML += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                string vtmp = "", vcolor = "";

                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    if (vtmp == "")
                    {
                        vtmp = dbRow["Paciente"].ToString() + "; " + dbRow["Piso"].ToString() + "° Piso; " + dbRow["DSC_SER"].ToString();
                    }
                    if (nroitem == 1) { vcolor = " id='trial1'"; }
                    else { vcolor = ""; }

                    gHTML += "<tr" + vcolor + ">";
                    //gHTML += "<td>" + dbRow["Fecha"].ToString() + "</td>";
                    //gHTML += "<td>" + dbRow["Hora"].ToString() + "</td>";
                    gHTML += "<td style='text-align: center;'>" + dbRow["Piso"].ToString() + "</td>";
                    //gHTML += "<td>" + dbRow["HHCC"].ToString() + "</td>";
                    gHTML += "<td style='text-align: left;'>" + dbRow["Paciente"].ToString() + "</td>";
                    gHTML += "<td style='text-align: left;'>" + dbRow["Servicio"].ToString() + "</td>";
                    //gHTML += "<td style='text-align: left;'>" + dbRow["Responsable"].ToString().PadRight(16).Substring(0, 12) + "</td>";
                    gHTML += "</tr>";
                    gHTML += Environment.NewLine;
                }

                gHTML += "</table><input type='hidden' id='vhPaciente' name='vhPaciente' value='" + vtmp + "' />" +
                    "</div></div><hr style='border-top: 1px solid blue'>";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }
        return gHTML;
    }

}