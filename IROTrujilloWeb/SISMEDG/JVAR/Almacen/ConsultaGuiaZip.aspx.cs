using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

public partial class SISMEDG_JVAR_Almacen_ConsultaGuiaZip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string vMovNumero = Request.QueryString["movnum"];
        string vCodEESS = Request.QueryString["codes"];
        string vNumDco = Request.QueryString["numdoc"];
        ZipGuiaRem(vMovNumero, vCodEESS, vNumDco);
    }

    protected void ZipGuiaRem(string vMovNumero, string vCodEESS, string vNumDco)
    {
        SqlConnection conSISMEDi = new SqlConnection(ClassGlobal.conexion_EXTERNO);
        string gDetH = "";
        try
        {
            string qSql = "SELECT TM.MOVCODITIP, TM.MOVNUMERO, TM.ALMCODIORG, " +
                "TM.ALMCODIDST + 'F01' ALMCODIDST, " +
                "TM.MOVTIPODCI, TM.MOVNUMEDCI, TM.MOVTIPODCO, TM.MOVNUMEDCO, TM.MOVFECHREC, " +
                "TM.MOVFECHEMI, TM.CCTCODIGO, TM.MOVTOT, TM.PRVNUMERUC, TM.PRVDESCRIP, " +
                "TM.MOVREFE, TM.USRCODIGO, '' APPVERSION, TM.MOVFECHREG, TM.MOVFECHULT, " +
                "TM.MOVSITUA, TMD.MOVNUMEITE, TMD.MEDCOD, TMD.MEDCODIFAB, TMD.MEDREGSAN, " +
                "TMD.MEDLOTE, TMD.MEDFECHVTO, TMD.MOVCANTID, TMD.MOVPRECIO, " +
                "TM.ALMORGVIR, TM.ALMCODIDST + 'F0101' ALMDSTVIR, TM.MOVINDPRC, TM.MOVFFINAN " +
                "from tmovim tm " +
                "inner join tmovimdet tmd on tm.coddep = tmd.coddep and tm.movcoditip = tmd.movcoditip and tm.movnumero = tmd.movnumero " +
                "where tm.coddep = '018A01' and tm.movcoditip = 'S' and tm.movnumero = '" + vMovNumero + "';";
            SqlCommand cmd2 = new SqlCommand(qSql, conSISMEDi);
            cmd2.CommandType = CommandType.Text; SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSISMEDi.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSISMEDi.Close();


            //S + CodEESS + 01 + _ + MOVNUMERO(6 ULTIMOS DIGITOS) + S + MOVNUMEDCO
            string nombrearchivo = "S0531201_024494_S0253646";
            nombrearchivo = "S" + vCodEESS + "01_" + vMovNumero.Substring(3, 6) + "_S" + vNumDco;
            //210 024494
            string nomArchivo = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\" + nombrearchivo;
            try
            {
                File.Delete(nomArchivo + ".zip");
            }
            catch (Exception)
            {
            }
            try
            {
                System.IO.Directory.Delete(nomArchivo);
            }
            catch (Exception)
            {
            }
            Directory.CreateDirectory(nomArchivo);

            string archivoOrigen = "", archivoDestino="";
            archivoOrigen = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\PRecios.dbf";
            archivoDestino = nomArchivo + @"\PR" + vMovNumero.Substring(3, 6) + ".dbf";
            System.IO.File.Copy(archivoOrigen, archivoDestino, true);
            archivoOrigen = @"J:\IROTrujilloWeb\IROTrujilloWeb\ASPX\Descargas\MV.dbf";
            archivoDestino = nomArchivo + @"\MV" + vMovNumero.Substring(3, 6) + ".dbf";
            System.IO.File.Copy(archivoOrigen, archivoDestino, true);


            OleDbConnection con = new OleDbConnection(GetConnection("J:\\IROTrujilloWeb\\IROTrujilloWeb\\ASPX\\Descargas\\"+ nombrearchivo));
            
            foreach (DataRow row in objdataset.Tables[0].Rows)
            {
                string insertSql = "insert into MV" + vMovNumero.Substring(3, 6) + "" +
                    "(MOVCODITIP, MOVNUMERO, ALMCODIORG, ALMCODIDST, MOVTIPODCI, MOVNUMEDCI, MOVTIPODCO, MOVNUMEDCO, MOVFECHREC, MOVFECHEMI, CCTCODIGO, MOVTOT, PRVNUMERUC, PRVDESCRIP, MOVREFE, USRCODIGO, APPVERSION, MOVFECHREG, MOVFECHULT, MOVSITUA, MOVNUMEITE, MEDCOD, MEDCODIFAB, MEDREGSAN, MEDLOTE, MEDFECHVTO, MOVCANTID, MOVPRECIO, ALMORGVIR, ALMDSTVIR, MOVINDPRC, MOVFFINAN) " +
                    "values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                OleDbCommand cmd = new OleDbCommand(insertSql, con);
                cmd.CommandType = CommandType.Text;
                OleDbDataAdapter adapter = new OleDbDataAdapter();

                cmd.Parameters.AddWithValue("@MOVCODITIP", row["MOVCODITIP"].ToString());
                cmd.Parameters.AddWithValue("@MOVNUMERO", row["MOVNUMERO"].ToString());
                cmd.Parameters.AddWithValue("@ALMCODIORG", row["ALMCODIORG"].ToString());
                cmd.Parameters.AddWithValue("@ALMCODIDST", row["ALMCODIDST"].ToString());
                cmd.Parameters.AddWithValue("@MOVTIPODCI", row["MOVTIPODCI"].ToString());
                cmd.Parameters.AddWithValue("@MOVNUMEDCI", row["MOVNUMEDCI"].ToString());
                cmd.Parameters.AddWithValue("@MOVTIPODCO", row["MOVTIPODCO"].ToString());
                cmd.Parameters.AddWithValue("@MOVNUMEDCO", row["MOVNUMEDCO"].ToString());
                cmd.Parameters.AddWithValue("@MOVFECHREC", row["MOVFECHREC"]);
                cmd.Parameters.AddWithValue("@MOVFECHEMI", row["MOVFECHEMI"]);
                cmd.Parameters.AddWithValue("@CCTCODIGO", row["CCTCODIGO"].ToString());
                cmd.Parameters.AddWithValue("@MOVTOT", row["MOVTOT"]);
                cmd.Parameters.AddWithValue("@PRVNUMERUC", row["PRVNUMERUC"].ToString());
                cmd.Parameters.AddWithValue("@PRVDESCRIP", row["PRVDESCRIP"].ToString());
                cmd.Parameters.AddWithValue("@MOVREFE", row["MOVREFE"].ToString());
                cmd.Parameters.AddWithValue("@USRCODIGO", row["USRCODIGO"].ToString());
                cmd.Parameters.AddWithValue("@APPVERSION", row["APPVERSION"].ToString());
                cmd.Parameters.AddWithValue("@MOVFECHREG", row["MOVFECHREG"]);
                cmd.Parameters.AddWithValue("@MOVFECHULT", row["MOVFECHULT"]);
                cmd.Parameters.AddWithValue("@MOVSITUA", row["MOVSITUA"].ToString());
                cmd.Parameters.AddWithValue("@MOVNUMEITE", row["MOVNUMEITE"].ToString());
                cmd.Parameters.AddWithValue("@MEDCOD", row["MEDCOD"].ToString());
                cmd.Parameters.AddWithValue("@MEDCODIFAB", row["MEDCODIFAB"].ToString());
                cmd.Parameters.AddWithValue("@MEDREGSAN", row["MEDREGSAN"].ToString());
                cmd.Parameters.AddWithValue("@MEDLOTE", row["MEDLOTE"].ToString());
                cmd.Parameters.AddWithValue("@MEDFECHVTO", row["MEDFECHVTO"]);
                cmd.Parameters.AddWithValue("@MOVCANTID", row["MOVCANTID"]);
                cmd.Parameters.AddWithValue("@MOVPRECIO", row["MOVPRECIO"]);
                cmd.Parameters.AddWithValue("@ALMORGVIR", row["ALMORGVIR"].ToString());
                cmd.Parameters.AddWithValue("@ALMDSTVIR", row["ALMDSTVIR"].ToString());
                cmd.Parameters.AddWithValue("@MOVINDPRC", row["MOVINDPRC"].ToString());
                cmd.Parameters.AddWithValue("@MOVFFINAN", row["MOVFFINAN"].ToString());

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            
            //objdataset.WriteXml(nomArchivo + "\\" + nombrearchivo + ".xls"); //Ruta de Guardado del Archivo dentro del Servidor
            ZipFile.CreateFromDirectory(nomArchivo, nomArchivo + ".zip");
            Response.ContentType = "application/zip";
            //context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "inline; filename=" + nombrearchivo + ".zip");
            Response.TransmitFile(Server.MapPath(@"~\ASPX\Descargas\" + nombrearchivo + ".zip"));
            Response.End();

            gDetH += "Descarga Correcta";
        }
        catch (Exception ex)
        {
            gDetH += "-" + "-" + ex.Message.ToString() + ex.ToString();
        }
        lblmsj.Text = gDetH;
    }

    private static string GetConnection(string path)
    {
        //return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBASE IV;";
        return "Provider=VFPOLEDB.1;Data Source=" + path + ";";
    }

    public static string ReplaceEscape(string str)
    {
        str = str.Replace("'", "''");
        return str;
    }

}
