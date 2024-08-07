using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ASPX_IROJVAR_ProgCupos_ProgPersonal : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;

    }

    [WebMethod]
    public static string SetBtnGuardar(string idProgServ, string COD_SER, string Cod_Per, string PIS_SER, string Tur_Ser, string PPAnio, string PPMes, string PPFechaCupos, string PPCupos, string PPAdiLimite, string Hr_Ser, string PPObservacion, string PPEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetHtml = "";
        string qSql = "";
        try
        {
            qSql = "INSERT INTO TabLibres.dbo.ProgPer (idProgServ, COD_SER, Cod_Per, PIS_SER, Tur_Ser, PPAnio, PPMes, PPFechaCupos, PPCupos, PPAdicional, PPAdiLimite, Hr_Ser, PPObservacion, PPEstado) VALUES (@idProgServ, @COD_SER, @Cod_Per, @PIS_SER, @Tur_Ser, @PPAnio, @PPMes, @PPFechaCupos, @PPCupos, @PPAdicional, @PPAdiLimite, @Hr_Ser, @PPObservacion, @PPEstado) SELECT @@Identity;";

            SqlCommand cmd = new SqlCommand(qSql, conSAP00i);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            
            cmd.Parameters.AddWithValue("@idProgServ", idProgServ);
            cmd.Parameters.AddWithValue("@COD_SER", COD_SER);
            cmd.Parameters.AddWithValue("@Cod_Per", Cod_Per);
            cmd.Parameters.AddWithValue("@PIS_SER", PIS_SER);
            cmd.Parameters.AddWithValue("@Tur_Ser", Tur_Ser);
            cmd.Parameters.AddWithValue("@PPAnio", PPAnio);
            cmd.Parameters.AddWithValue("@PPMes", PPMes);
            cmd.Parameters.AddWithValue("@PPFechaCupos", PPFechaCupos);
            cmd.Parameters.AddWithValue("@PPCupos", PPCupos);
            cmd.Parameters.AddWithValue("@PPAdicional", "0");
            cmd.Parameters.AddWithValue("@PPAdiLimite", PPAdiLimite);
            cmd.Parameters.AddWithValue("@Hr_Ser", Hr_Ser);
            cmd.Parameters.AddWithValue("@PPObservacion", PPObservacion);
            cmd.Parameters.AddWithValue("@PPEstado", "Activo");

            adapter.SelectCommand = cmd;

            conSAP00i.Open();
            int idReg = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00i.Close();

            gDetHtml += idReg.ToString() + " - Programacion de Personal Guardado";
        }
        catch (Exception ex)
        {
            gDetHtml += "ERROR: " + ex.Message.ToString() + Environment.NewLine + ex.ToString();
        }
        return gDetHtml;
    }

    [WebMethod]
    public static string GetBtnBuscarServ(string PSEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "SELECT PSer.idProgServ, PSer.COD_SER CSer, Ser.DSC_SER Servicio, " +
                "PSer.PIS_SER Piso, Tur_Ser Turno, PSCupos Cupos, PSAdiLimite Limite, Hr_Ser Horario, PSObservacion Observacion " +
                "from TabLibres.dbo.ProgServ PSer " +
                "left join NUEVA.dbo.SERVICIO Ser on " +
                "PSer.COD_SER COLLATE Modern_Spanish_CI_AS = Ser.COD_SER COLLATE Modern_Spanish_CI_AS ";
            
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
                gDetH += "<th class='text-center'>CodSer</th>";
                gDetH += "<th class='text-center'>Servicio</th>";
                gDetH += "<th class='text-center'>Piso</th>";
                gDetH += "<th class='text-center'>Turno</th>";
                gDetH += "<th class='text-center'>Cupos</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                { 
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelServicio('" + dbRow["idProgServ"].ToString() + "', '" + dbRow["CSer"].ToString() + "', '" + dbRow["Servicio"].ToString() + "', '" + dbRow["Piso"].ToString() + "', '" + dbRow["Turno"].ToString() + "', '" + dbRow["Cupos"].ToString() + "', '" + dbRow["Limite"].ToString() + "', '" + dbRow["Horario"].ToString() + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["CSer"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Servicio"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Piso"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Turno"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["Cupos"].ToString() + "</td>";

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
    public static string GetBtnBuscarPer(string PSEstado)
    {
        SqlConnection conSAP00i = new SqlConnection(ClassGlobal.conexion_SAP00);
        string gDetH = "";
        string qSql = "";
        try
        {
            qSql = "select per.*, pro.DES_PRO " +
                "from NUEVA.dbo.PERSONAL per " +
                "left join NUEVA.dbo.PROFESION pro on per.COD_PRO = pro.COD_PRO " +
                "where (pro.COD_PRO = '01' or pro.COD_PRO = '07') and per.EST_PER = '1' " +
                "order by APE_PER ";

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
                gDetH += "<th class='text-center'>CodPer</th>";
                gDetH += "<th class='text-center'>Personal</th>";
                gDetH += "<th class='text-center'>Profesion</th>";
                gDetH += "</tr>" + Environment.NewLine;
                int nroitem = 0;
                foreach (DataRow dbRow in dtDatoDetAt.Rows)
                {
                    nroitem += 1;
                    gDetH += "<tr>";
                    gDetH += "<td class='text-center'>" + nroitem + "</td>";
                    gDetH += "<td class='text-center'><button type='button' class='btn bg-navy' data-dismiss='modal' onclick=\"fSelPersonal('" + dbRow["COD_PER"].ToString() + "', '" + dbRow["APE_PER"].ToString() + "')\"><i class='fa fa-fw fa-check'></i></button></td>";
                    gDetH += "<td class='text-center'>" + dbRow["COD_PER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["APE_PER"].ToString() + "</td>";
                    gDetH += "<td class='text-center'>" + dbRow["DES_PRO"].ToString() + "</td>";

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
