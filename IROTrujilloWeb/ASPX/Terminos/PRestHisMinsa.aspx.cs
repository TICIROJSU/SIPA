using RestSharp;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

public partial class ASPX_Terminos_PRestHisMinsa : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (Page.IsPostBack)
        {

        }
    }

    public void ProcesaHISMINSA()
    {
        try
        {
            //con.Open();
            string qSql = "select '' numeroafiliacion, cast(ano as varchar) + format(mes, '00') + format(dia ,'00') as fechaatencion, 'A' estadoregistro, LABCONF1, DIAGNOST1,CODI1,'CX' tipoitem1, LABCONF2, DIAGNOST2,CODI2,'CX' tipoitem2, LABCONF3, DIAGNOST3,CODI3,'CX' tipoitem3, LABCONF4, DIAGNOST4,CODI4,'CX' tipoitem4, LABCONF5, DIAGNOST5,CODI5,'CX' tipoitem5, LABCONF6, DIAGNOST6,CODI6,'CX' tipoitem6, COD_SERVSA, CAST(COD_2000 AS int) COD_2000, 31-day(ifa) diaedad, (Convert(Integer, Datediff(Day, ifn, Getdate())/30))%12 mesedad, year(getdate())-YEAR(IFN) annioedad, year(getdate())-YEAR(IFN) edadregistro, MT idturno, TIP_EDAD idtipedadregistro, FI, SUBSTRING(CODIF,2,8) per_nrodocumento, 'MAURO' per_nombres, 'PAZ' per_apepaterno, 'PORRAS' per_apematerno, 'PER' per_idpais, '42' per_idprofesion,'1' per_idtipodoc, 'M' per_idsexo, '2' per_idcondicion, SUBSTRING(PLAZA,2,8) pea_nrodocumento, left(APE_PER,9) pea_apepaterno, substring(APE_PER,9,8) pea_apematerno, right(APE_PER,5) pea_nombres, 'PER' pea_idpais, '13' pea_idprofesion, cast(year(FEN_PER) as varchar) + format(month(FEN_PER), '00') + format(day(FEN_PER), '00') as pea_fechanacimiento, '1' pea_idtipodoc, 'M' pea_idsexo, '2' pea_idcondicion, SUBSTRING(dni,5,8) pac_nrodocumento, '5' pac_idflag, iap pac_apepaterno, iam pac_apematerno, ino pac_nombres, FICHAFAM pac_NHC, '1' pac_idtipodoc, '58' pac_etnia, cast(year(ifn) as varchar) + format(month(ifn), '00') + format(day(ifn) ,'00') pac_fechanacimiento, CAST(cod_2000 AS int) pac_idestablecimiento, 'PER' pac_idpais, sexo pac_idsexo , PLAZA from NUEVA.dbo.CHEQ2011 LEFT JOIN NUEVA.dbo.HISTORIA ON RIGHT('000000' + Ltrim(Rtrim(FICHAFAM)),6)=IHC LEFT JOIN NUEVA.dbo.PERSONAL ON PLAZA=HIS_PER where ano=2020 and mes=1 and COD_2000<>'' and NUM_FRT=946 order by NOM_LOTE,NUM_PAG,NUM_FRT ;";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            CargaTablaDT(dtDato);
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitToken.Text += ex.Message.ToString() + "<br />";
        }
    }

    public void CargaTablaDT(DataTable dtDato)
    {
        if (dtDato.Rows.Count > 0)
        {
            LitToken.Text += "<br />" + "Informacion: " + "<br />";
            int numrow = 0;
            foreach (DataRow dbRow in dtDato.Rows)
            {
                numrow += 1;
                LitToken.Text += "<br />" +numrow+ "° Json: " + "<br />";
                string varJSonHISMINSA = "{ " +
        "	\"cita\": {                                     " +
        "		\"numeroafiliacion\": \"" + dbRow["numeroafiliacion"].ToString() + "\",    " +
        "		\"fechaatencion\": \"" + dbRow["fechaatencion"].ToString() + "\",       " +
        "		\"estadoregistro\": \"" + dbRow["estadoregistro"].ToString() + "\",      " +
        "		\"items\": [   " +
        "	    { " +
        "	    \"labs\": [  ";
                if (dbRow["LABCONF1"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += "{  " +
                    "		\"codigo\": \"\",  " +
                    "		\"valor\": \"" + dbRow["LABCONF1"].ToString().Trim() + "\"  " +
                    "   	} "; 
                }
                varJSonHISMINSA += "], " +
        "				\"tipodiagnostico\": \"" + dbRow["DIAGNOST1"].ToString() + "\", " +
        "				\"codigo\": \"" + dbRow["CODI1"].ToString() + "\",          " +
        "				\"tipoitem\": \"" + dbRow["tipoitem1"].ToString() + "\"         " +
        "			}";
                if (dbRow["DIAGNOST2"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "	\"labs\": [  ";
                    if (dbRow["LABCONF2"].ToString().Trim() != "")
                    {
                        varJSonHISMINSA += " { " +
                        "		\"codigo\": \"\",  " +
                        "		\"valor\": \"" + dbRow["LABCONF2"].ToString().Trim() + "\"  " +
                        "   	} ";
                    }
                    varJSonHISMINSA += "], " +
                        "	\"tipodiagnostico\": \"" + dbRow["DIAGNOST2"].ToString() + "\", " +
                    "	\"codigo\": \"" + dbRow["CODI2"].ToString() + "\",          " +
                    "	\"tipoitem\": \"" + dbRow["tipoitem2"].ToString() + "\"         " +
                    "} ";
                }
                if (dbRow["DIAGNOST3"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "	\"labs\": [  ";
                    if (dbRow["LABCONF3"].ToString().Trim() != "")
                    {
                        varJSonHISMINSA += " { " +
                        "		\"codigo\": \"\",  " +
                        "		\"valor\": \"" + dbRow["LABCONF3"].ToString().Trim() + "\"  " +
                        "   	} ";
                    }
                    varJSonHISMINSA += "], " +
                    "	\"tipodiagnostico\": \"" + dbRow["DIAGNOST3"].ToString() + "\", " +
                    "	\"codigo\": \"" + dbRow["CODI3"].ToString() + "\",          " +
                    "	\"tipoitem\": \"" + dbRow["tipoitem3"].ToString() + "\"         " +
                    "} ";
                }
                if (dbRow["DIAGNOST4"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "	\"labs\": [  ";
                    if (dbRow["LABCONF4"].ToString().Trim() != "")
                    {
                        varJSonHISMINSA += " { " +
                        "		\"codigo\": \"\",  " +
                        "		\"valor\": \"" + dbRow["LABCONF4"].ToString().Trim() + "\"  " +
                        "   	} ";
                    }
                    varJSonHISMINSA += "], " +
                    "	\"tipodiagnostico\": \"" + dbRow["DIAGNOST4"].ToString() + "\", " +
                    "	\"codigo\": \"" + dbRow["CODI4"].ToString() + "\",          " +
                    "	\"tipoitem\": \"" + dbRow["tipoitem4"].ToString() + "\"         " +
                    "} ";
                }
                if (dbRow["DIAGNOST5"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "	\"labs\": [  ";
                    if (dbRow["LABCONF5"].ToString().Trim() != "")
                    {
                        varJSonHISMINSA += " { " +
                        "		\"codigo\": \"\",  " +
                        "		\"valor\": \"" + dbRow["LABCONF5"].ToString().Trim() + "\"  " +
                        "   	} ";
                    }
                    varJSonHISMINSA += "], " +
                    "	\"tipodiagnostico\": \"" + dbRow["DIAGNOST5"].ToString() + "\", " +
                    "	\"codigo\": \"" + dbRow["CODI5"].ToString() + "\",          " +
                    "	\"tipoitem\": \"" + dbRow["tipoitem5"].ToString() + "\"         " +
                    "} ";
                }
                if (dbRow["DIAGNOST6"].ToString().Trim() != "")
                {
                    varJSonHISMINSA += ", {  " +
                        "	\"labs\": [  ";
                    if (dbRow["LABCONF6"].ToString().Trim() != "")
                    {
                        varJSonHISMINSA += " { " +
                        "		\"codigo\": \"\",  " +
                        "		\"valor\": \"" + dbRow["LABCONF6"].ToString().Trim() + "\"  " +
                        "   	} ";
                    }
                    varJSonHISMINSA += "], " +
                    "	\"tipodiagnostico\": \"" + dbRow["DIAGNOST6"].ToString() + "\", " +
                    "	\"codigo\": \"" + dbRow["CODI6"].ToString() + "\",          " +
                    "	\"tipoitem\": \"" + dbRow["tipoitem6"].ToString() + "\"         " +
                    "} ";
                }
                string var_idups = dbRow["COD_SERVSA"].ToString();
                if (int.Parse(var_idups) >=303413)
                {
                    var_idups = "303408";
                }
                varJSonHISMINSA += " ], " +
        "		\"idups\": \"" + var_idups + "\",               " +
        "		\"idestablecimiento\": \"" + dbRow["COD_2000"].ToString() + "\",   " +
        "		\"diaedad\": \"" + dbRow["diaedad"].ToString() + "\",             " +
        "		\"mesedad\": \"" + dbRow["mesedad"].ToString() + "\",             " +
        "		\"annioedad\": \"" + dbRow["annioedad"].ToString() + "\",           " +
        "		\"edadregistro\": \"" + dbRow["edadregistro"].ToString() + "\",        " +
        "		\"idturno\": \"" + dbRow["idturno"].ToString() + "\",             " +
        "		\"idtipedadregistro\": \"" + dbRow["idtipedadregistro"].ToString().Trim() + "\",  " +
        "		\"fgdiag\": \"21\",              " +
        "		\"componente\": \"\",          " +
        "		\"idfinanciador\": \"" + dbRow["FI"].ToString().Trim() + "\",       " +
        "		\"examenfisico\": {}                                           " +
        "	},                                              " +
        "	\"personal_registra\": {                        " +
        "		\"nrodocumento\": \"" + dbRow["per_nrodocumento"].ToString() + "\",   " +
        "		\"nombres\": \"" + dbRow["per_nombres"].ToString() + "\",             " +
        "		\"apepaterno\": \"" + dbRow["per_apepaterno"].ToString() + "\",       " +
        "		\"apematerno\": \"" + dbRow["per_apematerno"].ToString() + "\",       " +
        "		\"idpais\": \"" + dbRow["per_idpais"].ToString() + "\",               " +
        "		\"idprofesion\": \"" + dbRow["per_idprofesion"].ToString() + "\",     " +
        "		\"fechanacimiento\": \"19910314\",       " +
        "		\"idtipodoc\": \"" + dbRow["per_idtipodoc"].ToString() + "\",         " +
        "		\"idsexo\": \"" + dbRow["per_idsexo"].ToString() + "\",               " +
        "		\"idcondicion\": \"" + dbRow["per_idcondicion"].ToString() + "\"      " +
        "	},                                              " +
        "	\"personal_atiende\": {                         " +
        "		\"nrodocumento\": \"" + dbRow["pea_nrodocumento"].ToString() + "\",   " +
        "		\"apepaterno\": \"" + dbRow["pea_apepaterno"].ToString() + "\",       " +
        "		\"apematerno\": \"" + dbRow["pea_apematerno"].ToString() + "\",       " +
        "		\"nombres\": \"" + dbRow["pea_nombres"].ToString() + "\",             " +
        "		\"idpais\": \"" + dbRow["pea_idpais"].ToString() + "\",               " +
        "		\"idprofesion\": \"" + dbRow["pea_idprofesion"].ToString() + "\",     " +
        "		\"fechanacimiento\": \"" + dbRow["pea_fechanacimiento"].ToString() + "\", " +
        "		\"idtipodoc\": \"" + dbRow["pea_idtipodoc"].ToString() + "\",         " +
        "		\"idsexo\": \"" + dbRow["pea_idsexo"].ToString() + "\",               " +
        "		\"idcondicion\": \"" + dbRow["pea_idcondicion"].ToString() + "\"      " +
        "	},                                              " +
        "	\"paciente\": {                                 " +
        "		\"nrodocumento\": \"" + dbRow["pac_nrodocumento"].ToString() + "\",   " +
        "		\"apepaterno\": \"" + dbRow["pac_apepaterno"].ToString() + "\",       " +
        "		\"apematerno\": \"" + dbRow["pac_apematerno"].ToString() + "\",       " +
        "		\"nombres\": \"" + dbRow["pac_nombres"].ToString() + "\",             " +
        "		\"idflag\": \"5\",               " +
        "		\"nrohistoriaclinica\": \"" + dbRow["pac_NHC"].ToString() + "\",     " +
        "		\"idtipodoc\": \"" + dbRow["pac_idtipodoc"].ToString() + "\",         " +
        "		\"idetnia\": \"" + dbRow["pac_etnia"].ToString().Trim() + "\",               " +
        "		\"fechanacimiento\": \"" + dbRow["pac_fechanacimiento"].ToString() + "\", " +
        "		\"idestablecimiento\": \"" + dbRow["pac_idestablecimiento"].ToString() + "\", " +
        "		\"idpais\": \"" + dbRow["pac_idpais"].ToString() + "\",                 " +
        "		\"idsexo\": \"" + dbRow["pac_idsexo"].ToString() + "\"                  " +
        "	} " +
        "}    ";
                LitToken.Text += varJSonHISMINSA + "<br />";
                ObtenerToken(varJSonHISMINSA);
            }
        }
        else
        {
            LitToken.Text += "<br />" + "Sin Informacion " + "<br />";
        }
        
    }

    public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    protected void btnCargaToken_Click(object sender, EventArgs e)
    {
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ProcesaHISMINSA();
    }
    public void ObtenerToken(string varJSonHISMINSA)
    {
        var client = new RestClient("http://dpidesalud.minsa.gob.pe:18080/mcs-sihce-hisminsa/integracion/v1.0/paquete/actualizar");
        RestRequest request = new RestRequest() { Method = Method.POST };

        request.AddJsonBody(varJSonHISMINSA);
        var response = client.Execute(request);
        LitToken.Text += response.Content.ToString() + Environment.NewLine + "<br />";

        string json = response.Content.ToString();
        dynamic jsonObj = JsonConvert.DeserializeObject(json);
        //var vtoken = jsonObj["anio"][0].ToString();
        //LitToken.Text += vtoken + Environment.NewLine;
        //LitToken.Text += "<br />" + vtoken;

    }

}