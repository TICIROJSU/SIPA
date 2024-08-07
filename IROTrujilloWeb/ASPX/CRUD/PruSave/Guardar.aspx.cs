using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASPX_CRUD_PruSave_Guardar : System.Web.UI.Page
{
    SqlConnection conLocal = new SqlConnection(ClassGlobal.conexion_local);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //this.Page.Response.Write("<script language='JavaScript'>window.history.back();</script>");
        }
        SqlCommand cmd = new SqlCommand("select * from Tablero.dbo.TblPrueba order by id desc", conLocal);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;

		DataSet objdataset = new DataSet();
		try
		{
			conLocal.Open();
			adapter.Fill(objdataset);
			conLocal.Close();
		}
		catch (Exception ex)
		{
			LitTABL1.Text = ex.Message.ToString();
			//throw;
		}


        //DataTable dtDato = objdataset.Tables[0];

        //GVtable.DataSource = dtDato;
        //GVtable.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string vnombre = txtnombre.Text;
        DateTime vfecha = DateTime.Parse(datepicker.Text);
        int vcantidad = Int32.Parse(txtcantidad.Text);
        try
        {
            //con.Open();
            string consql = "insert into Tablero.dbo.TblPrueba (Nombre, Fecha, Cantidad, FechaReg) Values (@Nombre, @Fecha, @Cantidad, @FechaReg) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conLocal);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Nombre", vnombre);
            cmd.Parameters.AddWithValue("@Fecha", vfecha);
            cmd.Parameters.AddWithValue("@Cantidad", vcantidad);
            cmd.Parameters.AddWithValue("@FechaReg", DateTime.Now);

            conLocal.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conLocal.Close();

            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');window.location.assign('../PruSave/Guardar.aspx');</script>");
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }
}