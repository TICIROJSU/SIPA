using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Descripción breve de csgMySql
/// </summary>
public class csgMySql
{
    public static string conMySQL_IIS = "Database=mysql; Data Source=localhost; User Id=remote; Password=1234; convert zero datetime=True";
    //"Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";

    public csgMySql()
    {

    }

    public string clave_incremCifra(string cadena)
    {
        int ePub = 15;
        char remplaza;
        string re_incrementa = "";
        for (int i = 0; i < cadena.Length; i++)
        {
            remplaza = (char)((int)cadena[i] + ePub);
            re_incrementa = re_incrementa + remplaza.ToString();
        }
        return re_incrementa;
    }

    public string clave_incremDesci(string cadena)
    {
        int ePub = 15;
        char remplaza;
        string re_incrementa = "";
        for (int i = 0; i < cadena.Length; i++)
        {
            remplaza = (char)((int)cadena[i] - ePub);
            re_incrementa = re_incrementa + remplaza.ToString();
        }
        return re_incrementa;
    }


}