using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de ClassGlobal
/// </summary>
public class ClassGlobal
{
    public static string conexion_SAP00 = "Data Source = 150.10.9.2;Initial Catalog = IROf; User ID=jaguirrer; Password=jvar2021;";
    public static string conexion_SIGA = "Data Source = 150.10.9.6;Initial Catalog = SALMA_SIGA; User ID=sa; Password=Ir0Jsu-;";
    public static string conexion_local = "Data Source = TICINF02-PC;Initial Catalog = Tablero; Trusted_Connection=true;";
	public static string conexion_DIRESA = "Data Source=190.119.219.134;Initial Catalog = FE21; User ID=diresa; Password=12345;";

    //public static string conexion_EXTERNO = "Data Source=159.89.128.147;Initial Catalog = SISMED; User ID=SRVJ1; Password=ju@nsrv1;";
    public static string conexion_EXTERNO = "Data Source=181.177.238.107;Initial Catalog = SISMED; User ID=juan; Password=Laclavees2020++; Persist Security Info=False; ";

    public static string conexion_EXTERNO_SI = "Data Source=181.177.238.107;Initial Catalog = BD_COMPROMISOSGESTION; User ID=desarrollo; Password=Nopasarlaclave2022@; Persist Security Info=False; ";
    public static string conexion_EXTERNO_SI_TICInf02PC = "Data Source=TICINF02-PC;Initial Catalog = BD_COMPROMISOSGESTION; User ID=TIJAguirreR; Password=Tic2019*; Persist Security Info=False; ";


    public static string conSISMED = "Data Source=150.10.9.2;Initial Catalog = SISMED; User ID=jaguirrer; Password=jvar2021;";

	public static string varGlobalUser = ""; // No funciona, el mismo usuario aparece en todos lados
    public static string varGlobalTmp = "";
    public static string tmpvAnio = "";

    public static int varAnioActivo = 0;
    public static int varAnioActual = 0;


    public static DataTable varDTGen = null;

    public static string varJSonREST = "{ " +
            "\"id_referencia\": \"\", " +
            "\"id_cita\": \"\", " +
            "\"tipo\": \"2\", " +
            "\"ipress\": { " +
                "\"ipress_id\": \"0000\", " +
                "\"diresa_id\": \"18\", " +
                "\"red_id\": \"00\", " +
                "\"microred_id\": \"00\" " +
            "}, " +
            "\"paciente\": { " +
                "\"seguro_sis\": \"2\", " +
                "\"documento\": { " +
                    "\"tipo\" : \"1\", " +
                    "\"numero\" : \"48799266\" " +
                "}, " +
                "\"nombre\": { " +
                    "\"prenombres\": \"JOSE LUIS\", " +
                    "\"apellido_paterno\": \"PEREZ\", " +
                    "\"apellido_materno\" : \"SILVA\" " +
                "}, " +
                "\"ubigeo_domicilio\": { " +
                    "\"departamento_id\": \"13\", " +
                    "\"provincia_id\": \"01\", " +
                    "\"distrito_id\": \"01\" " +
                "} " +
            "}, " +
            "\"registro\": { " +
                "\"inicio\": \"2019-08-01 10:00:00\", " +
                "\"fin\": \"2019-08-26 09:30:00\" " +
            "} " +
        "}";

    public ClassGlobal()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        //string conexion_txt = conexion();

    }
    /*public string conexion() No Sirve
    {
        return "Data Source=.;Initial Catalog = DatabaseConnectivity;Trusted_Connection=true;";
    }*/
    public void Main()
    {
        //string conexion_txt = conexion();
    }

    public static string formatoMillar(string numero)
    {
        int longitud = numero.Length;
        string new_numero = "";
        for (int i = 1; i <= longitud; i++)
        {
            //new_numero = "," + numero.Substring(longitud - i,3) + new_numero;
            new_numero = numero.Substring(longitud - i, 1) + new_numero;
            if ((i % 3) == 0 && longitud > i)
            {
                new_numero = "," + new_numero;
            }
        }
        return new_numero;
    }

    public static string formatoMillarDec(string numero)
    {
        string[] result;
        if (!numero.Contains("."))
        {
            numero += ".00";
        }
        result = numero.Split('.');
        numero = result[0];
        /////////////////////////////////
        int longitud = numero.Length;
        string new_numero = "";
        for (int i = 1; i <= longitud; i++)
        {
            //new_numero = "," + numero.Substring(longitud - i,3) + new_numero;
            new_numero = numero.Substring(longitud - i, 1) + new_numero;
            if ((i % 3) == 0 && longitud > i)
            {
                new_numero = "," + new_numero;
            }
        }
        return new_numero + "." + (result[1] + "00").Substring(0,2);
    }

	public static string stringNull(DataRow dbRow)
	{
		string new_numero = "0";
		if (dbRow["total"] != DBNull.Value)
		{
			new_numero = dbRow["total"].ToString();
		}
		return new_numero;
	}

	public static string RellenaTxt(string strtxt, int largo)
	{
		string strtxt2 = strtxt;
		if (strtxt.Length <= largo)
		{
			for (int i = strtxt.Length - 1; i < largo; i++)
			{
				strtxt2 += "&nbsp;";
			}
		}
		else
		{
			strtxt2 = strtxt.Substring(0, largo);
		}
		return strtxt2;
	}

	public static string MesNroToTexto(string vMes)
    {
        string new_txtmes = "";
        switch (vMes)
        {
            case "1":
                new_txtmes = "Enero"; break;
            case "2":
                new_txtmes = "Febrero"; break;
            case "3":
                new_txtmes = "Marzo"; break;
            case "4":
                new_txtmes = "Abril"; break;
            case "5":
                new_txtmes = "Mayo"; break;
            case "6":
                new_txtmes = "Junio"; break;
            case "7":
                new_txtmes = "Julio"; break;
            case "8":
                new_txtmes = "Agosto"; break;
            case "9":
                new_txtmes = "Setiembre"; break;
            case "10":
                new_txtmes = "Octubre"; break;
            case "11":
                new_txtmes = "Noviembre"; break;
            case "12":
                new_txtmes = "Diciembre"; break;
            default:
                break;
        }
        return new_txtmes;
    }

    public static string DiaSemEsp(int vMes)
    {
        string new_txtmes = "";
        switch (vMes)
        {
            case 1:
                new_txtmes = "Lun"; break;
            case 2:
                new_txtmes = "Mar"; break;
            case 3:
                new_txtmes = "Mie"; break;
            case 4:
                new_txtmes = "Jue"; break;
            case 5:
                new_txtmes = "Vie"; break;
            case 6:
                new_txtmes = "Sab"; break;
            case 0:
                new_txtmes = "Dom"; break;
            default:
                break;
        }
        return new_txtmes;
    }

    public static string DecripPass(string clave1)
    {
        string T, op = "";
        try
        {
            int r = clave1.Length, w, o = 0;
            for (w = 0; w < r; w++)
            {
                //T = Mid(RTrim(CLAVE), w);
                T = clave1.Substring(w, 1);
                if (T != "") { o = (Encoding.ASCII.GetBytes(T)[0]) + 100; }
                //op = op + " " + o;
                op = op + (char)o;
                //If T<> "" Then o = Asc(T) - 100
                //op = op + Chr(o)
            }
        }
        catch (Exception ex)
        {
            op = ex.Message.ToString();
            //throw;
        }
        return op;
    }

    public static string MontoPorc(double MontoTot, double MontoSub)
    {
        string strMPorc = "";
        double Porc = MontoSub / MontoTot * 100;
        strMPorc = ClassGlobal.formatoMillarDec(MontoSub.ToString()) + " (" + ClassGlobal.formatoMillarDec(Porc.ToString()) + "%)";
        return strMPorc;
    }

    public static string CantPorc(double CantTotal, double CantSub)
    {
        string strMPorc = "";
        double Porc = CantSub / CantTotal * 100;
        strMPorc = ClassGlobal.formatoMillarDec(Porc.ToString());
        return strMPorc;
    }

    public static DataTable GetDataTable(GridView dtg)
    {
        DataTable dt = new DataTable();
        if (dtg.HeaderRow != null)
        {
            for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
            {
                dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
            }
        }

        foreach (GridViewRow row in dtg.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();
            for (int i = 0; i < row.Cells.Count; i++)
            {
                dr[i] = row.Cells[i].Text.Replace(" ", "");
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }


    //public static string ExportarExcelDataSet(string varSql, string nombrearchivo)
    //{
    //    SqlCommand cmd = new SqlCommand(varSql, con);
    //    SqlDataAdapter adapter = new SqlDataAdapter();
    //    adapter.SelectCommand = cmd;
    //    DataSet objdataset = new DataSet();
    //    adapter.Fill(objdataset);
    //    objdataset.WriteXml(@"D:\JAGUIRRER\VS_Net_Desarrollo\DIGEMID_JA_ASP\DIGEMID_JA_ASP\ASPX\Descargas\" + nombrearchivo + ".xls"); //Ruta de Guardado del Archivo dentro del Servidor

    //    Response.ContentType = "text/xml";
    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombrearchivo + ".xls");
    //    Response.TransmitFile(Server.MapPath(@"~\ASPX\Descargas\" + nombrearchivo + ".xls"));
    //    Response.End();

    //    return "Conforme";
    //}



}