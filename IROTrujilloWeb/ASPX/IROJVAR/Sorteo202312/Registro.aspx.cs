using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Net;

public partial class ASPX_IROJVAR_Sorteo2_Registro : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
       
    protected void enviar_Click(object sender, EventArgs e)
    {
        string dni = txtDNI.Text;
        string ape = txtApellidos.Text;
        string nom = txtNombres.Text;
        string vreg = "Registrar";

        string gDetHtml = "";
        string qSql = "";

        if (ValidaRegistro(dni) != "NoExiste")
        {
            gDetHtml += ValidaRegistro(dni) + Environment.NewLine;
            vreg = "Existe";
        }
        if (ValidaSiEmpleado(dni) != "SiExiste")
        {
            gDetHtml += "DNI no Existe en el Listado Empleados. " + Environment.NewLine;
            vreg = "NoExiste";
        }

        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);

        if (vreg == "Registrar")
        {
            try
            {
                qSql = "INSERT INTO Prueba.dbo.RegistroSorteo(DNI, Apellidos, Nombres, Sorteo) VALUES (@DNI, @Apellidos, @Nombres, @Sorteo) SELECT @@Identity;";
                SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd.Parameters.AddWithValue("@DNI", dni);
                cmd.Parameters.AddWithValue("@Apellidos", ape);
                cmd.Parameters.AddWithValue("@Nombres", nom);
                cmd.Parameters.AddWithValue("@Sorteo", 0);
                adapter.SelectCommand = cmd;

                conSAP00i.Open();
                int idReg = Convert.ToInt32(cmd.ExecuteScalar());
                conSAP00i.Close();

                gDetHtml += "Registro Realizado en la Posicion: " + idReg.ToString();            
            }
            catch (Exception ex)
            {
                gDetHtml += "ERROR: " + ex.Message.ToString() + ". " + ex.ToString();
            }
        }
        lblMensaje.Text = gDetHtml;
    }

    public static string ValidaRegistro(string vDNI)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from Prueba.dbo.RegistroSorteo where DNI = @DNI";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@DNI", vDNI);

            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtOT = objdataset.Tables[0];

            if (dtOT.Rows.Count > 0)
            {
                gHTML = "Ya se Registro en el Puesto: " + dtOT.Rows[0]["idPerSorteo"].ToString();
            }
            else
            {
                gHTML = "NoExiste";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }

        return gHTML;
    }

    public static string ValidaSiEmpleado(string vDNI)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gHTML = "";
        try
        {
            string qSql = "select * from Prueba.dbo.PersonalIRO2021 where DNI = @DNI";
            SqlCommand cmd2 = new SqlCommand(qSql, conSAP00i);
            cmd2.CommandType = CommandType.Text;
            cmd2.Parameters.AddWithValue("@DNI", vDNI);

            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = cmd2;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter2.Fill(objdataset);
            conSAP00i.Close();

            DataTable dtOT = objdataset.Tables[0];

            if (dtOT.Rows.Count > 0)
            {
                gHTML = "SiExiste";
            }
            else
            {
                gHTML = "NoExiste";
            }
        }
        catch (Exception ex)
        {
            gHTML += "-" + "-" + ex.Message.ToString();
        }

        return gHTML;
    }

}