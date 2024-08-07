using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_ProgCupos_ProgPerAdicional : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        IniPersonalUsu(Session["idUser2"].ToString());
    }

    public void IniPersonalUsu(string codUsu)
    {
        try
        {
            string qSql = "select * from NUEVA.dbo.PERSONAL per left join NUEVA.dbo.USUARIO usu on per.USU_PER = usu.Cod_Usu where usu.Cod_Usu=@Cod_Usu ";
            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@Cod_Usu", codUsu);

            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            DataTable dtDato = objdataset.Tables[0];

            lblPerCod.Text = dtDato.Rows[0]["COD_PER"].ToString();
            lblPerNom.Text = dtDato.Rows[0]["APE_PER"].ToString();

        }
        catch (Exception ex)
        {
            LitTABL1.Text = ex.Message.ToString();
        }
    }

    [WebMethod]
    public static string SetBtnGuardar(string idProgPer, string DNIPac, string PPFechaCupos, string PPAEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO TabLibres.dbo.ProgPerAdi (idProgPer, DNIPac, PPFechaCupos, PPAEstado) VALUES (@idProgPer, @DNIPac, @PPFechaCupos, @PPAEstado) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            
            cmd.Parameters.AddWithValue("@idProgPer", idProgPer);
            cmd.Parameters.AddWithValue("@DNIPac", DNIPac);
            cmd.Parameters.AddWithValue("@PPFechaCupos", PPFechaCupos);
            cmd.Parameters.AddWithValue("@PPAEstado", "Activo");

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml += idReg.ToString() + " - Adicional Guardado";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string GetBtnBuscarServ(string vCodPer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "select PSer.idProgPer, PSer.COD_SER, Ser.DSC_SER, PSer.Tur_Ser, PPFechaCupos " +
                "from TabLibres.dbo.ProgPer PSer " +
                "left join NUEVA.dbo.SERVICIO Ser on PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where Cod_Per = '" + vCodPer + "' and PPFechaCupos >= GETDATE() and PPEstado = 'Activo' " +
                "order by PPFechaCupos ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class='text-center'>N° </th>";
                gDetH += "<th class='text-center'>Sel </th>";
                gDetH += "<th class='text-center'>Fecha</th>";
                gDetH += "<th class='text-center'>CodSer</th>";
                gDetH += "<th class='text-center'>Servicio</th>";
                gDetH += "<th class='text-center'>Turno</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelServicio('" + dbRow["idProgPer"].ToString() + "', '" + dbRow["COD_SER"].ToString() + "', '" + dbRow["DSC_SER"].ToString() + "', '" + dbRow["Tur_Ser"].ToString() + "', '" + dbRow["PPFechaCupos"].ToString().Substring(0, 10) + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["PPFechaCupos"].ToString().Substring(0, 10) + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["COD_SER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["DSC_SER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Tur_Ser"].ToString() + "</td>";

                    gDetH += "</tr>" + Environment.NewLine;
                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetH += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    [WebMethod]
    public static string GetBtnListarAdic(string idProgPer)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "SELECT DNIPac, PPFechaCupos, PPAFechaReg " +
                "from TabLibres.dbo.ProgPerAdi " +
                "where idProgPer = '" + idProgPer + "' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00i.Close();
            DataTable dtDatoDetAt = objdataset.Tables[0];

            if (dtDatoDetAt.Rows.Count > 0)
            {
                gDetH += Environment.NewLine + "<div class='box' style='color:#000000; font-size:20px;'><div class='box-body table-responsive no-padding'>";
                gDetH += "<table id='tbldscrg' class='table table-hover' style='text-align: right; font-size: 14px; '>";
                gDetH += "<tr>";
                gDetH += "<th class='text-center'>N° </th>";
                gDetH += "<th class='text-center'>Paciente </th>";
                gDetH += "<th class='text-center'>Fecha</th>";
                gDetH += "<th class='text-center'>Registrado</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {//, PPFechaCupos, PPFechaCupos
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["DNIPac"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["PPFechaCupos"].ToString().Substring(0, 10) + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["PPAFechaReg"].ToString() + "</td>";

                    gDetH += "</tr>" + Environment.NewLine;
                }

                gDetH += "</table></div></div><hr style='border-top: 1px solid blue'>";

            }
        }
        catch (Exception ex)
        {
            gDetH += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetH;
    }

    protected void btnListar_Click(object sender, EventArgs e)
    {
        try
        {
            string qSql = "SELECT PPER.PPFechaCupos, PPER.Cod_Per, PER.APE_PER, PPER.COD_SER, SER.DSC_SER, PPER.Tur_Ser, PPER.PIS_SER, PPER.PPAdicional, PPER.Hr_Ser " +
                "from TabLibres.dbo.ProgPer PPER " +
                "left join NUEVA.dbo.PERSONAL PER on PPER.Cod_Per COLLATE Modern_Spanish_CI_AS = PER.COD_PER COLLATE Modern_Spanish_CI_AS " +
                "left join NUEVA.dbo.SERVICIO SER on PPER.COD_SER COLLATE Modern_Spanish_CI_AS = SER.COD_SER COLLATE Modern_Spanish_CI_AS " +
                "where PPEstado = 'Activo' ";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            conSAP00.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            conSAP00.Close();

            GVtable.DataSource = objdataset.Tables[0];
            GVtable.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitTABL1.Text = ex.Message.ToString();
        }
    }

}
