using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;

/// <summary>
/// Descripción breve de cGlobalSISMED
/// </summary>
public class cGlobalSISMED
{
    DataSet ds = new DataSet();
    OleDbDataAdapter da = new OleDbDataAdapter();
    OleDbConnection conn = new OleDbConnection();
    string raizSISMED1 = @"\\TICINF02-PC\SISMEDv2_IRO\DATOS\";
    string raizSISMED2 = @"\\TICINF02-PC\SISMEDv2_IRO\DATOS2\";

    public cGlobalSISMED()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet vbFDataSet(string qSQL)
    {

        conn = new OleDbConnection(@"Provider=VFPOLEDB.1; Data Source=" + raizSISMED2 + @"; Password=""""; Collating Sequence=MACHINE; Exclusive=false; User Cache Authentication=False; Nulls=false; TABLEVALIDATE=0;");

        try
        {
            conn.Open();
            da = new OleDbDataAdapter(qSQL, conn);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw new Exception("Mensaje de Error", ex);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public string CopiaDBF(string vtable)
    {
        string vArchivoOrig = raizSISMED1 + vtable;
        string vArchivoDest = raizSISMED2 + vtable;

        try
        {
            File.Delete(vArchivoDest + ".DBF");

        }
        catch (Exception)
        {

            throw;
        }

        //Try
        //    My.Computer.FileSystem.DeleteFile(vArchivoDest + ".DBF",
        //        FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        //    My.Computer.FileSystem.DeleteFile(vArchivoDest + ".CDX",
        //        FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
        //Catch ex As Exception

        //End Try

        //My.Computer.FileSystem.CopyFile(
        //    vArchivoOrig + ".DBF", vArchivoDest + ".DBF",
        //    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        //My.Computer.FileSystem.CopyFile(
        //    vArchivoOrig + ".CDX", vArchivoDest + ".CDX",
        //    Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

            return "";
    }



}