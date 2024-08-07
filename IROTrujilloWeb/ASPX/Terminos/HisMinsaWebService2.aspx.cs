using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ASPX_Terminos_HisMinsaWebService2 : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (Page.IsPostBack)
        {

        }
    }

    protected void CargaHISMINSA_Click(object sender, EventArgs e)
    {
        //string vstrp = "Variable interna";
        //string vmostrar = $"mostrar: {{{vstrp}}}";
        //LitString.Text = vmostrar;
        ProcesaHISMINSA();
    }

    public void ProcesaHISMINSA()
    {
        try
        {
            //con.Open();
            string qSql = "";

            qSql = "select top 2 (case when diaedadant < 0 then diaedadant + 30 else diaedadant end ) diaedad, (case when diaedadant < 0 then mesedadant + 1 else mesedadant end ) mesedad, * from ( select id_his, '' numeroafiliacion, cast(ano as varchar) + format(mes, '00') + format(dia ,'00') as fechaatencion, 'A' estadoregistro, LABCONF1, DIAGNOST1,CODI1,'CX' tipoitem1, LABCONF2, DIAGNOST2,CODI2, 'CX' tipoitem2, LABCONF3, DIAGNOST3,CODI3,'CX' tipoitem3, LABCONF4, DIAGNOST4,CODI4, 'CX' tipoitem4, LABCONF5, DIAGNOST5,CODI5,'CX' tipoitem5, LABCONF6, DIAGNOST6,CODI6, 'CX' tipoitem6, COD_SERVSA, CAST(COD_2000 AS int) COD_2000, DATEPART(DAY, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar)) - DATEPART(DAY, IFN) diaedadant, DATEDIFF(MONTH, IFN, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar))%12 mesedadant, FLOOR(DATEDIFF(day, IFN, cast(DIA as varchar)+'/'+cast(MES as varchar)+'/'+cast(ANO as varchar)) / 365.25) annioedad, year(getdate())-YEAR(IFN) edadregistro, MT idturno, TIP_EDAD idtipedadregistro, FI, SUBSTRING(CODIF,2,8) per_nrodocumento, 'MAURO' per_nombres, 'PAZ' per_apepaterno, 'PORRAS' per_apematerno, 'PER' per_idpais, '42' per_idprofesion,'1' per_idtipodoc, 'M' per_idsexo, '2' per_idcondicion, SUBSTRING(PLAZA,2,8) pea_nrodocumento, left(APE_PER,9) pea_apepaterno, substring(APE_PER,9,8) pea_apematerno, right(APE_PER,5) pea_nombres, 'PER' pea_idpais, '13' pea_idprofesion, cast(year(FEN_PER) as varchar) + format(month(FEN_PER), '00') + format(day(FEN_PER), '00') as pea_fechanacimiento, '1' pea_idtipodoc, 'M' pea_idsexo, '2' pea_idcondicion, SUBSTRING(dni,5,8) pac_nrodocumento, '5' pac_idflag, iap pac_apepaterno, iam pac_apematerno, ino pac_nombres, FICHAFAM pac_NHC, '1' pac_idtipodoc, '58' pac_etnia, cast(year(ifn) as varchar) + format(month(ifn), '00') + format(day(ifn) ,'00') pac_fechanacimiento, CAST(cod_2000 AS int) pac_idestablecimiento, 'PER' pac_idpais, sexo pac_idsexo , PLAZA from NUEVA.dbo.CHEQ2011 LEFT JOIN NUEVA.dbo.HISTORIA ON RIGHT('000000' + Ltrim(Rtrim(FICHAFAM)),6)=IHC LEFT JOIN NUEVA.dbo.PERSONAL ON PLAZA=HIS_PER where ano=2021 and MES='2' and COD_2000<>'' and COD_SERVSA1='81' ) tabHISMINSA where pac_apepaterno is not null";

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
                LitToken.Text += "<br />" + numrow + "° Json: " + "<br />";

                string var_idups = dbRow["COD_SERVSA"].ToString();
                if (int.Parse(var_idups) >= 303413)
                {
                    var_idups = "303408";
                }

                string vannioedad = dbRow["annioedad"].ToString(), 
                    vcomponente = "", 
                    vdiaedad = dbRow["diaedad"].ToString(), 
                    vedadregistro = dbRow["edadregistro"].ToString(), 
                    vestadoregistro = dbRow["estadoregistro"].ToString(), 
                    vexamenfisico = "", 
                    vhemoglobina = "", 
                    vperimetroabdominal = "", 
                    vperimetrocefalico = "", vpeso = "", vtalla = "", 
                    vfechaatencion = dbRow["fechaatencion"].ToString(), 
                    vfgdiag = "21", vfgsis = "", 
                    vidcita = dbRow["id_his"].ToString(), 
                    videesscita = dbRow["COD_2000"].ToString(), 
                    vidfinanciador = dbRow["FI"].ToString().Trim(), 
                    vidlote = "", vidpaciente = "", vidpersonalatiende = "", vidpersonalregistra = "", vidtipcondestab = "", vidtipcondserv = "", 
                    vidtipedadregistro = dbRow["idtipedadregistro"].ToString().Trim(), 
                    vidturno = dbRow["idturno"].ToString(), vidups = var_idups, 
                    vitems = "", 
                    vcodigoitem = "", vcodigolote = "", vfecharesultado = "", vfechasolicitud = "", 
                    vlabs = "", vcodigolab = "", vvalor = "", vtipodiagnostico = "", vtipoitem = "", 
                    vmesedad = "", vmsgcita = "", vnumeroafiliacion = "";
                string vpaciente = "", vapematernopac = "", vapepaternopac = "", vdescripcionocupacionpac = "", vdescripcionreligionpac = "", vdomicilioresidenciapac = "", vdomicilioresidenciaactualpac = "", vfechabajapac = "", vfechanacimientopac = "", vfecharegistropac = "", vfgestadopac = "", vfgreniecpac = "", vfgrnacidopac = "", vfotopersonapac = "", videstablecimientopac = "", videstadocivilpac = "", videtnia = "", vidflag = "", vidgradoinstruccionpac = "", vidpaispac = "", vidpersonapac = "", vidsexopac = "", vidtipodocpac = "", vidubigeonacimientopac = "", vidubigeoresidenciapac = "", vidubigeoresidencia_actualpac = "", vidubigeoresidenciaactualpac = "", vmsgpac = "", vnombrespac = "", vnrodocumentopac = "", vnrohisfamiliar = "", vnrohistoriaclinica = "", vreferenciadomiciliopac = "", vtiposangrepac = "";
                string vpersonal_atiende = "", vapematernopera = "", vapepaternopera = "", vdescripcionocupacionpera = "", vdescripcionreligionpera = "", vdomicilioresidenciapera = "", vdomicilioresidenciaactualpera = "", vestablecimientopera = "", vcodigounicopera = "", vidpera = "", viddisapera = "", vidmicroredpera = "", vidredpera = "", vidsectorpera = "", vfechabajapera = "", vfechanacimientopera = "", vfecharegistropera = "", vfgestadopera = "", vfgreniecpera = "", vfgrnacidopera = "", vfotopersonapera = "", vidcondicionpera = "", videstablecimientopera = "", videstadocivilpera = "", vidgradoinstruccionpera = "", vidpaispera = "", vidpersonapera = "", vidperxeesspera = "", vidprofesionpera = "", vidsexopera = "", vidtipodocpera = "", vidubigeonacimientopera = "", vidubigeoresidenciapera = "", vidubigeoresidencia_actualpera = "", vidubigeoresidenciaactualpera = "", vmsgpera = "", vnombrespera = "", vnrodocumentopera = "", vnumdocmedpera = "", vreferenciadomiciliopera = "", vtiposangrepera = ""; 
                string vpersonal_registra = "", vapematernoperr = "", vapepaternoperr = "", vdescripcionocupacionperr = "", vdescripcionreligionperr = "", vdomicilioresidenciaperr = "", vdomicilioresidenciaactualperr = "", vestablecimientoperr = "", vcodigounicoperr = "", vidperr = "", viddisaperr = "", vidmicroredperr = "", vidredperr = "", vidsectorperr = "", vfechabajaperr = "", vfechanacimientoperr = "", vfecharegistroperr = "", vfgestadoperr = "", vfgreniecperr = "", vfgrnacidoperr = "", vfotopersonaperr = "", vidcondicionperr = "", videstablecimientoperr = "", videstadocivilperr = "", vidgradoinstruccionperr = "", vidpaisperr = "", vidpersonaperr = "", vidperxeessperr = "", vidprofesionperr = "", vidsexoperr = "", vidtipodocperr = "", vidubigeonacimientoperr = "", vidubigeoresidenciaperr = "", vidubigeoresidencia_actualperr = "", vidubigeoresidenciaactualperr = "", vmsgperr = "", vnombresperr = "", vnrodocumentoperr = "", vnumdocmedperr = "", vreferenciadomicilioperr = "", vtiposangreperr = "";

                string varJSonHISMINSA = $"{{ " +
                        $"  \"cita\": {{ " +
                        $"    \"annioedad\": {vannioedad}, " +
                        $"    \"componente\": \"{vcomponente}\", " +
                        $"    \"diaedad\": {vdiaedad}, " +
                        $"    \"edadregistro\": {vedadregistro}, " +
                        $"    \"estadoregistro\": \"{vestadoregistro}\", " +
                        $"    \"examenfisico\": {{" +
                        $"      \"hemoglobina\": \"{vhemoglobina}\", " +
                        $"      \"perimetroabdominal\": \"{vperimetroabdominal}\", " +
                        $"      \"perimetrocefalico\": \"{vperimetrocefalico}\", " +
                        $"      \"peso\": \"{vpeso}\", " +
                        $"      \"talla\": \"{vtalla}\" " +
                        $"    }}, " +
                        $"    \"fechaatencion\": \"{vfechaatencion}\", " +
                        $"    \"fgdiag\": {vfgdiag}, " +
                        $"    \"fgsis\": {vfgsis}, " +
                        $"    \"idcita\": {vidcita}, " +
                        $"    \"idestablecimiento\": {videesscita}, " +
                        $"    \"idfinanciador\": \"{vidfinanciador}\", " +
                        $"    \"idlote\": \"{vidlote}\", " +
                        $"    \"idpaciente\": {vidpaciente}, " +
                        $"    \"idpersonalatiende\": {vidpersonalatiende}, " +
                        $"    \"idpersonalregistra\": {vidpersonalregistra}, " +
                        $"    \"idtipcondestab\": \"{vidtipcondestab}\", " +
                        $"    \"idtipcondserv\": \"{vidtipcondserv}\", " +
                        $"    \"idtipedadregistro\": \"{vidtipedadregistro}\", " +
                        $"    \"idturno\": \"{vidturno}\", " +
                        $"    \"idups\": \"{vidups}\", " +
                        $"    \"items\": [ " +
                        $"      {{ " +
                        $"        \"codigo\": \"{vcodigoitem}\", " +
                        $"        \"codigolote\": \"{vcodigolote}\", " +
                        $"        \"fecharesultado\": \"{vfecharesultado}\", " +
                        $"        \"fechasolicitud\": \"{vfechasolicitud}\", " +
                        $"        \"labs\": [ " +
                        $"          {{ " +
                        $"            \"codigo\": {vcodigolab}, " +
                        $"            \"valor\": \"{vvalor}\" " +
                        $"          }} " +
                        $"        ], " +
                        $"        \"tipodiagnostico\": \"{vtipodiagnostico}\", " +
                        $"        \"tipoitem\": \"{vtipoitem}\" " +
                        $"      }} " +
                        $"    ], " +
                        $"    \"mesedad\": {vmesedad}, " +
                        $"    \"msg\": \"{vmsgcita}\", " +
                        $"    \"numeroafiliacion\": \"{vnumeroafiliacion}\" " +
                        $"  }}, " +
                        $"  \"paciente\": {{ " +
                        $"    \"apematerno\": \"{vapematernopac}\", " +
                        $"    \"apepaterno\": \"{vapepaternopac}\", " +
                        $"    \"descripcionocupacion\": \"{vdescripcionocupacionpac}\", " +
                        $"    \"descripcionreligion\": \"{vdescripcionreligionpac}\", " +
                        $"    \"domicilioresidencia\": \"{vdomicilioresidenciapac}\", " +
                        $"    \"domicilioresidenciaactual\": \"{vdomicilioresidenciaactualpac}\", " +
                        $"    \"fechabaja\": \"{vfechabajapac}\", " +
                        $"    \"fechanacimiento\": \"{vfechanacimientopac}\", " +
                        $"    \"fecharegistro\": \"{vfecharegistropac}\", " +
                        $"    \"fgestado\": {vfgestadopac}, " +
                        $"    \"fgreniec\": {vfgreniecpac}, " +
                        $"    \"fgrnacido\": {vfgrnacidopac}, " +
                        $"    \"fotopersona\": \"{vfotopersonapac}\", " +
                        $"    \"idestablecimiento\": {videstablecimientopac}, " +
                        $"    \"idestadocivil\": \"{videstadocivilpac}\", " +
                        $"    \"idetnia\": \"{videtnia}\", " +
                        $"    \"idflag\": {vidflag}, " +
                        $"    \"idgradoinstruccion\": \"{vidgradoinstruccionpac}\", " +
                        $"    \"idpais\": \"{vidpaispac}\", " +
                        $"    \"idpersona\": {vidpersonapac}, " +
                        $"    \"idsexo\": \"{vidsexopac}\", " +
                        $"    \"idtipodoc\": {vidtipodocpac}, " +
                        $"    \"idubigeonacimiento\": \"{vidubigeonacimientopac}\", " +
                        $"    \"idubigeoresidencia\": \"{vidubigeoresidenciapac}\", " +
                        $"    \"idubigeoresidencia_actual\": \"{vidubigeoresidencia_actualpac}\", " +
                        $"    \"idubigeoresidenciaactual\": \"{vidubigeoresidenciaactualpac}\", " +
                        $"    \"msg\": \"{vmsgpac}\", " +
                        $"    \"nombres\": \"{vnombrespac}\", " +
                        $"    \"nrodocumento\": \"{vnrodocumentopac}\", " +
                        $"    \"nrohisfamiliar\": \"{vnrohisfamiliar}\", " +
                        $"    \"nrohistoriaclinica\": \"{vnrohistoriaclinica}\", " +
                        $"    \"referenciadomicilio\": \"{vreferenciadomiciliopac}\", " +
                        $"    \"tiposangre\": \"{vtiposangrepac}\" " +
                        $"  }}, " +
                        $"  \"personal_atiende\": {{ " +
                        $"    \"apematerno\": \"{vapematernopera}\", " +
                        $"    \"apepaterno\": \"{vapepaternopera}\", " +
                        $"    \"descripcionocupacion\": \"{vdescripcionocupacionpera}\", " +
                        $"    \"descripcionreligion\": \"{vdescripcionreligionpera}\", " +
                        $"    \"domicilioresidencia\": \"{vdomicilioresidenciapera}\", " +
                        $"    \"domicilioresidenciaactual\": \"{vdomicilioresidenciaactualpera}\", " +
                        $"    \"establecimiento\": {{ " +
                        $"      \"codigounico\": {vcodigounicopera}, " +
                        $"      \"id\": {vidpera}, " +
                        $"      \"iddisa\": {viddisapera}, " +
                        $"      \"idmicrored\": \"{vidmicroredpera}\", " +
                        $"      \"idred\": \"{vidredpera}\", " +
                        $"      \"idsector\": {vidsectorpera} " +
                        $"    }}, " +
                        $"    \"fechabaja\": \"{vfechabajapera}\", " +
                        $"    \"fechanacimiento\": \"{vfechanacimientopera}\", " +
                        $"    \"fecharegistro\": \"{vfecharegistropera}\", " +
                        $"    \"fgestado\": {vfgestadopera}, " +
                        $"    \"fgreniec\": {vfgreniecpera}, " +
                        $"    \"fgrnacido\": {vfgrnacidopera}, " +
                        $"    \"fotopersona\": \"{vfotopersonapera}\", " +
                        $"    \"idcondicion\": {vidcondicionpera}," +
                        $"    \"idestablecimiento\": {videstablecimientopera}, " +
                        $"    \"idestadocivil\": \"{videstadocivilpera}\", " +
                        $"    \"idgradoinstruccion\": \"{vidgradoinstruccionpera}\", " +
                        $"    \"idpais\": \"{vidpaispera}\", " +
                        $"    \"idpersona\": {vidpersonapera}, " +
                        $"    \"idperxeess\": {vidperxeesspera}, " +
                        $"    \"idprofesion\": \"{vidprofesionpera}\", " +
                        $"    \"idsexo\": \"{vidsexopera}\", " +
                        $"    \"idtipodoc\": {vidtipodocpera}, " +
                        $"    \"idubigeonacimiento\": \"{vidubigeonacimientopera}\", " +
                        $"    \"idubigeoresidencia\": \"{vidubigeoresidenciapera}\", " +
                        $"    \"idubigeoresidencia_actual\": \"{vidubigeoresidencia_actualpera}\", " +
                        $"    \"idubigeoresidenciaactual\": \"{vidubigeoresidenciaactualpera}\", " +
                        $"    \"msg\": \"{vmsgpera}\", " +
                        $"    \"nombres\": \"{vnombrespera}\", " +
                        $"    \"nrodocumento\": \"{vnrodocumentopera}\", " +
                        $"    \"numdocmed\": \"{vnumdocmedpera}\", " +
                        $"    \"referenciadomicilio\": \"{vreferenciadomiciliopera}\", " +
                        $"    \"tiposangre\": \"{vtiposangrepera}\" " +
                        $"  }}, " +
                        $"  \"personal_registra\": {{ " +
                        $"    \"apematerno\": \"{vapematernoperr}\", " +
                        $"    \"apepaterno\": \"{vapepaternoperr}\", " +
                        $"    \"descripcionocupacion\": \"{vdescripcionocupacionperr}\", " +
                        $"    \"descripcionreligion\": \"{vdescripcionreligionperr}\", " +
                        $"    \"domicilioresidencia\": \"{vdomicilioresidenciaperr}\", " +
                        $"    \"domicilioresidenciaactual\": \"{vdomicilioresidenciaactualperr}\", " +
                        $"    \"establecimiento\": {{ " +
                        $"      \"codigounico\": {vcodigounicoperr}, " +
                        $"      \"id\": {vidperr}, " +
                        $"      \"iddisa\": {viddisaperr}, " +
                        $"      \"idmicrored\": \"{vidmicroredperr}\", " +
                        $"      \"idred\": \"{vidredperr}\", " +
                        $"      \"idsector\": {vidsectorperr} " +
                        $"    }}, " +
                        $"    \"fechabaja\": \"{vfechabajaperr}\", " +
                        $"    \"fechanacimiento\": \"{vfechanacimientoperr}\", " +
                        $"    \"fecharegistro\": \"{vfecharegistroperr}\", " +
                        $"    \"fgestado\": {vfgestadoperr}, " +
                        $"    \"fgreniec\": {vfgreniecperr}, " +
                        $"    \"fgrnacido\": {vfgrnacidoperr}, " +
                        $"    \"fotopersona\": \"{vfotopersonaperr}\", " +
                        $"    \"idcondicion\": {vidcondicionperr}," +
                        $"    \"idestablecimiento\": {videstablecimientoperr}, " +
                        $"    \"idestadocivil\": \"{videstadocivilperr}\", " +
                        $"    \"idgradoinstruccion\": \"{vidgradoinstruccionperr}\", " +
                        $"    \"idpais\": \"{vidpaisperr}\", " +
                        $"    \"idpersona\": {vidpersonaperr}, " +
                        $"    \"idperxeess\": {vidperxeessperr}, " +
                        $"    \"idprofesion\": \"{vidprofesionperr}\", " +
                        $"    \"idsexo\": \"{vidsexoperr}\", " +
                        $"    \"idtipodoc\": {vidtipodocperr}, " +
                        $"    \"idubigeonacimiento\": \"{vidubigeonacimientoperr}\", " +
                        $"    \"idubigeoresidencia\": \"{vidubigeoresidenciaperr}\", " +
                        $"    \"idubigeoresidencia_actual\": \"{vidubigeoresidencia_actualperr}\", " +
                        $"    \"idubigeoresidenciaactual\": \"{vidubigeoresidenciaactualperr}\", " +
                        $"    \"msg\": \"{vmsgperr}\", " +
                        $"    \"nombres\": \"{vnombresperr}\", " +
                        $"    \"nrodocumento\": \"{vnrodocumentoperr}\", " +
                        $"    \"numdocmed\": \"{vnumdocmedperr}\", " +
                        $"    \"referenciadomicilio\": \"{vreferenciadomicilioperr}\", " +
                        $"    \"tiposangre\": \"{vtiposangreperr}\" " +
                        $"  }} " +
                        $"}}        ";
                               
                LitToken.Text += varJSonHISMINSA + "<br />";
                GuardaEnTabla(varJSonHISMINSA, dbRow["id_his"].ToString());
            }
        }
        else
        {
            LitToken.Text += "<br />" + "Sin Informacion " + "<br />";
        }
    }

    public void GuardaEnTabla(string varJSon, string NroCita)
    {
        try
        {
            //con.Open();
            string consql = "insert into NUEVA.dbo.TRAMA_INTEGRACION_HISMINSA_JSON (V_CITA_ORIGEN, V_JSON) Values (@V_CITA_ORIGEN, @V_JSON) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@V_CITA_ORIGEN", NroCita);
            cmd.Parameters.AddWithValue("@V_JSON", varJSon);

            conSAP00.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00.Close();
            LitToken.Text += " ||--> Nro Reg.: " + count + "<br />";
            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');window.location.assign('../PruSave/Guardar.aspx');</script>");
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitToken.Text += "<br />" + ex.Message.ToString() + "<br />";
            if (conSAP00.State == ConnectionState.Open) { conSAP00.Close(); }
        }
    }

}