using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using ClosedXML.Excel;


public partial class ASPX_Terminos_ExcelCarga : System.Web.UI.Page
{
    SqlConnection conLocal = new SqlConnection(ClassGlobal.conexion_local);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Create a new DataTable.
    DataTable dt = new DataTable();

    protected void ImportExcel(object sender, EventArgs e)
    {
        //Open the Excel file using ClosedXML.
        using (XLWorkbook workBook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
        {
            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);
            //Loop through the Worksheet rows.
            bool firstRow = true;
            foreach (IXLRow row in workSheet.Rows())
            {
                //Use the first row to add columns to DataTable.
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }
                    firstRow = false;
                }
                else
                {
                    //Add rows to DataTable.
                    dt.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cell in row.Cells())
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                        i++;
                    }
                }
                //GridView1.Caption = "123-" + Path.GetDirectoryName(FileUpload1.PostedFile.FileName).ToString();
                GridView1.Caption = FileUpload1.PostedFile.FileName + "/" + FileUpload1.FileName;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }

    protected void ImportExcel2(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            //Import_To_Grid(FilePath, Extension, "Yes"); //rbHDR.SelectedItem.Text
            Import_To_Grid(FilePath, Extension, "Yes"); //rbHDR.SelectedItem.Text
            if (File.Exists(FilePath)){ File.Delete(FilePath); }
        }
    }

    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();

        //DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        try
        {
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.Caption = FilePath;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception)
        {
            lblMensaje.Text = "Error al cargar el documento []";
            //throw;
        }

    }
    
    protected void Guardar1(object sender, EventArgs e)
    {
        //if (dt.Rows.Count > 0)
        //{
        //    int nroitem = 0;
        //    foreach (DataRow dbRow in dt.Rows)
        //    {
        //        nroitem += 1;
        //        GuardarReg(dbRow["NHC_SIS"].ToString(), dbRow["IDNI"].ToString(), dbRow["USU_TAR"].ToString(), dbRow["FUA"].ToString());
        //    }
        //}
        if (GridView1.Rows.Count > 0)
        {
            int nroitem = 0;
            foreach (GridViewRow dbRow in GridView1.Rows)
            {
                nroitem += 1;
                GuardarReg(dbRow.Cells[0].Text, dbRow.Cells[4].Text, dbRow.Cells[7].Text, dbRow.Cells[9].Text);
            }
            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + GridView1.Rows.Count + "');/*window.location.assign('../PruSave/Guardar.aspx');*/</script>");
        }
    }

    public void GuardarReg(string vnhc, string vdni, string vusu, string vfua)
    {
        try
        {
            //con.Open();
            string consql = "insert into Pruebas.dbo.tespera1 (NHC_SIS, IDNI, USU_TAR, FUA) Values (@NHC_SIS, @IDNI, @USU_TAR, @FUA) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conLocal);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@NHC_SIS", vnhc);
            cmd.Parameters.AddWithValue("@IDNI", vdni);
            cmd.Parameters.AddWithValue("@USU_TAR", vusu);
            cmd.Parameters.AddWithValue("@FUA", vfua);

            conLocal.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conLocal.Close();

            //this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');/*window.location.assign('../PruSave/Guardar.aspx');*/</script>");
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            lblMensaje.Text += ex.Message.ToString();
        }

    }

}