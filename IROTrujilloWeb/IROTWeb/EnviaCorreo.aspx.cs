using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IROTWeb_EnviaCorreo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Esto (en ASP.NET 2.0) no se ejecuta... si AutoEventWireup="false"
        if (!IsPostBack)
        {
            txtTexto.Text = "Hola,\n" +
                "Esto es una prueba de envio de correo usando ASP.NET 2.0 (C#)\n" +
                "Saludos!!!";
            LabelError.Text = "";
        }
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress(txtDe.Text);
        correo.To.Add(txtPara.Text);
        correo.Subject = txtAsunto.Text;
        txtTexto.Text += "\n\nFecha y hora GMT: " +
            DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss");
        correo.Body = txtTexto.Text;
        correo.IsBodyHtml = false;
        correo.Priority = System.Net.Mail.MailPriority.Normal;
        //
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //
        //---------------------------------------------
        // Estos datos debes rellanarlos correctamente
        //---------------------------------------------
        smtp.Host = "smtp.gmail.com"; //"smtp.hotmail.com"; //smtp.live.com - smtp.gmail.com
        smtp.UseDefaultCredentials = false;
        //smtp.Credentials = new System.Net.NetworkCredential("jaguirrer@irotrujillo.gob.pe", "Tic2019");
        smtp.Credentials = new System.Net.NetworkCredential("jvaguirrerosales@gmail.com", "Digemid2018");
        smtp.Port = 587;
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true;
        try
        {
            smtp.Send(correo);
            LabelError.Text = "Mensaje enviado satisfactoriamente";
        }
        catch (Exception ex)
        {
            LabelError.Text = "ERROR: " + ex.Message;
        }
    }

}