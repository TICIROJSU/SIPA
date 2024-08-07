using System;
using System.Data;
using System.Data.SqlClient;

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
                        vCorreo = dbRow["Contenido"].ToString();
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
        //smtp.Port = 465;
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(correo);
            LabelError.Text = "Mensaje enviado satisfactoriamente";
        }
        catch (Exception ex)
        {
            LabelError.Text = "<div style='color:red'>Problemas para enviar el Correo. Intente de nuevo en unos momentos</div>"; // + ex.Message;
        }
    }
}