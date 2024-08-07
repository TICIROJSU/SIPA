using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_CIEIMaster_Consultas : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    string html = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        if (!Page.IsPostBack)
        {
            CargaInicio();
        }
        if (Page.IsPostBack)
        {
            Guardar_Click();
        }
    }

    public void CargaInicio()
    {
        try
        {
            //con.Open();
            string qSql = "select * from NUEVA.dbo.JVARCIEIPag where Pagina='CONSULTAS' ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];
            CargaTabla(dtDato);
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message.ToString();
        }
    }

    protected void bntGuardar_Click(object sender, EventArgs e)
    {
        string consql = "";
        try
        {
            consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = @Titulo, Contenido = @Contenido, Detalle1 = @Detalle1, Detalle2 = @Detalle2, Detalle3 = @Detalle3 WHERE id=@id ";
            //consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = '"+ txtTitulo.Text + "', Contenido = '" + txtContenido.Text + "', Detalle1 = '" + txtDetalle1.Text + "', Detalle2 = '" + txtDetalle2.Text + "', Detalle3 = '" + txtDetalle3.Text + "' WHERE id=" + txtid.Text + " ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
            cmd.Parameters.AddWithValue("@Contenido", txtContenido.Text);
            cmd.Parameters.AddWithValue("@Detalle1", txtDetalle1.Text);
            cmd.Parameters.AddWithValue("@Detalle2", txtDetalle2.Text);
            cmd.Parameters.AddWithValue("@Detalle3", txtDetalle3.Text);
            cmd.Parameters.AddWithValue("@id", txtid.Text);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();
            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado Correctamente');window.location.assign('Consultas.aspx');</script>");
            //Literal1.Text = consql;
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message.ToString(); //consql + "<br />"
        }
    }

    public void Guardar_Click()
    {
        try
        {
            string consql = "UPDATE NUEVA.dbo.JVARCIEIPag SET Titulo = @Titulo, Contenido = @Contenido, Detalle1 = @Detalle1, Detalle2 = @Detalle2, Detalle3 = @Detalle3 WHERE id=@id ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
            cmd.Parameters.AddWithValue("@Contenido", txtContenido.Text);
            cmd.Parameters.AddWithValue("@Detalle1", txtDetalle1.Text);
            cmd.Parameters.AddWithValue("@Detalle2", txtDetalle2.Text);
            cmd.Parameters.AddWithValue("@Detalle3", txtDetalle3.Text);
            cmd.Parameters.AddWithValue("@id", txtid.Text);

            conSAP00.Open();
            cmd.ExecuteNonQuery();
            conSAP00.Close();
            Literal1.Text = txtid.Text;
        }
        catch (Exception ex)
        {
            Literal1.Text = ex.Message.ToString();
        }
    }

    public void CargaTabla(DataTable dtDato)
    {
        html = "";
        if (dtDato.Rows.Count > 0)
        {
            //html += "<table class='table table-condensed table-striped table-hover' style='text-align: right; font-size: 14px; '>";
            html += "<table class='table table-bordered' style='text-align: right; font-size: 14px; overflow-wrap: break-word;'>";
            html += "<tr>";
            html += "<th class='' style='text-align: center; display:block; '>#</th>";
            //html += "<th class=''>Pagina</th>";
            //html += "<th class=''>Seccion</th>";
            html += "<th class=''>Titulo</th>";
            html += "<th class=''>Contenido</th>";
            html += "<th class=''>Detalle1</th>";
            html += "<th class=''>Detalle2</th>";
            html += "<th class=''>Detalle3</th>";
            html += "<th class=''>Modificar</th>";
            html += "</tr>" + Environment.NewLine;
            int nroitem = 0; 
            foreach (DataRow dbRow in dtDato.Rows)
            {
                nroitem += 1;
                html += "<tr>";
                html += "<td class='' style='text-align: center;'>" + nroitem + "</td>";
                //html += "<td class='' style='text-align: left; '>" + dbRow["Pagina"].ToString() + "</td>";
                //html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Seccion"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Titulo"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Contenido"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Detalle1"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Detalle2"].ToString() + "</td>";
                html += "<td class='tdcelda' style='text-align: left;'>" + dbRow["Detalle3"].ToString() + "</td>";
                string vTmpEditar = "'" + dbRow["id"].ToString() + "', '" + dbRow["Pagina"].ToString() + "', '" + dbRow["Seccion"].ToString() + "', '" + dbRow["Titulo"].ToString() + "', '" + dbRow["Contenido"].ToString() + "', '" + dbRow["Detalle1"].ToString() + "', '" + dbRow["Detalle2"].ToString() + "', '" + dbRow["Detalle3"].ToString() + "'";
                html += "<td class='tdcelda' style='text-align: left;'><button type='button' class='btn bg-olive btn-flat margin' onclick=\"cargaContenido(" + vTmpEditar + ")\"> Modificar</button></td>";
                html += "</tr>" + Environment.NewLine;
            }

            html += "</table><hr style='border-top: 1px solid blue'>";
        }
        else
        {
            html += "<table>";
            html += "<tr><td class='FieldCaption' colspan=3>Sin registros encontrados</td></tr>";
            html += "</table><hr>";
        }
        LitSIConsulta.Text = html;
    }

}