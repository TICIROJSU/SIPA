using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

public partial class IROTWeb_CIEI_Consultas_Default : System.Web.UI.Page
{
    SqlConnection conSAP00 = new SqlConnection(ClassGlobal.conexion_SAP00);
    string vCorreo = "jaguirrer@irotrujillo.gob.pe";
    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 3600;
        try
        {
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
            string vLitContactos = "";

            foreach (DataRow dbRow in dtDato.Rows)
            {
                switch (dbRow["Seccion"].ToString())
                {
                    case "SUPDER":
                        vLitContactos += "<div class='dept-subtitle-tabs'>" + dbRow["Titulo"].ToString() + "</div>";
                        vLitContactos += "<p>" + dbRow["Contenido"].ToString();
                        vLitContactos += "<br />" + dbRow["Detalle1"].ToString();
                        if (dbRow["Detalle2"].ToString() != "")
                        { vLitContactos += "<br />" + dbRow["Detalle2"].ToString(); }
                        if (dbRow["Detalle3"].ToString() != "")
                        {
                            if (dbRow["Detalle3"].ToString().Contains("@"))
                            {   string co = dbRow["Detalle3"].ToString();
                                vLitContactos += "<br /><a href='mailto:" + co + "'>" + co + "</a>";
                            }
                            else
                            { vLitContactos += "<br />" + dbRow["Detalle3"].ToString(); }
                        }
                        vLitContactos += "</p>";
                        break;

                    case "CORREO":
                        vCorreo = "jaguirrer@irotrujillo.gob.pe";
                        break;

                    default:
                        break;
                }
            }
            LitContactos.Text = vLitContactos;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            LitError.Text = ex.Message.ToString();
        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress(email.Text);
        correo.To.Add(vCorreo);
        correo.To.Add(email.Text);
        correo.Subject = "CIEI-Web / " + subject.Text;
        message.Text += "\n\n" + 
            "De: " + name.Text + "\n" + 
            "Correo: " + email.Text + "\n" +
            "Fecha y hora: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        correo.Body = message.Text;
        correo.IsBodyHtml = false;
        correo.Priority = System.Net.Mail.MailPriority.Normal;

        string fileName = Path.GetFileName(file.PostedFile.FileName);
        Attachment myAttachment = new Attachment(file.FileContent, fileName);
        correo.Attachments.Add(myAttachment);

        //
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //
        //---------------------------------------------
        // Estos datos debes rellanarlos correctamente
        //---------------------------------------------
        smtp.Host = "smtp.gmail.com"; //"smtp.hotmail.com"; //smtp.live.com
        smtp.UseDefaultCredentials = false;
        //smtp.Credentials = new System.Net.NetworkCredential("jaguirrer@irotrujillo.gob.pe", "Tc09");
        smtp.Credentials = new System.Net.NetworkCredential("irotciei@gmail.com", "IROciei123");
        smtp.Port = 587;
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
        string estadoenvio = "";
        try
        {
            smtp.Send(correo);
            LabelError.Text = "Mensaje enviado satisfactoriamente";
            estadoenvio = "Correcto";
        }
        catch (Exception ex)
        {
            estadoenvio = "Error";
            LabelError.Text = "<div style='color:red'>Problemas para enviar el Correo. Intente de nuevo en unos momentos</div>"; // + ex.Message;
        }

        string folderPath = Server.MapPath("~/IROTWeb/Correo/Images/");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string varImg = folderPath + Path.GetFileName(file.FileName);
        file.SaveAs(varImg);

        GuardaMail(email.Text, vCorreo, message.Text, varImg, estadoenvio);
    }

    public void GuardaMail(string varDe, string varPara, string varMsj, string varImg, string varEst)
    {
        try
        {
            //con.Open();
            string consql = "insert into NUEVA.dbo.JVARMail (aplicacion, mailde, mailpara, mailmsj, mailimg, mailest, estado, mailfecha) Values (@aplicacion, @mailde, @mailpara, @mailmsj, @mailimg, @mailest, @estado, @mailfecha) SELECT @@Identity; ";
            SqlCommand cmd = new SqlCommand(consql, conSAP00);
            cmd.CommandType = CommandType.Text;
            //asignamos el valor de los textbox a los parametros
            cmd.Parameters.AddWithValue("@aplicacion", "Informes");
            cmd.Parameters.AddWithValue("@mailde", varDe);
            cmd.Parameters.AddWithValue("@mailpara", varPara);
            cmd.Parameters.AddWithValue("@mailmsj", varMsj);
            cmd.Parameters.AddWithValue("@mailimg", varImg);
            cmd.Parameters.AddWithValue("@mailest", varEst);
            cmd.Parameters.AddWithValue("@estado", "Activo");
            cmd.Parameters.AddWithValue("@mailfecha", DateTime.Now);

            conSAP00.Open();
            //cmd.ExecuteScalar();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conSAP00.Close();

            this.Page.Response.Write("<script language='JavaScript'>window.alert('Registro Guardado: " + count + "');window.location.assign('../Consultas/Default2.aspx');</script>");
        }
        catch (Exception ex)
        {
            //Label1.Text = "Error";
            ex.Message.ToString();
            LabelError.Text += ex.Message.ToString();
        }

    }


}