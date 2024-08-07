using RestSharp;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public partial class ASPX_Terminos_PruebasREST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    protected void btnCargaToken_Click(object sender, EventArgs e)
    {
        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
        //ServicePointManager.Expect100Continue = true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ObtenerToken();
    }
    public void ObtenerToken()
    {
        var client = new RestClient("https://dapimanager.minsa.gob.pe:8243/token");
        RestRequest request = new RestRequest() { Method = Method.POST };

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Basic VndvUVI5TVMwRDluV3hmRFQ4bmJSMUVmbUU0YTowc1BvX1BOVWN1Y295c2ZGbjlyTE41WlQzRWNh");
        request.AddParameter("username", "usr_00005197_0001");
        request.AddParameter("password", "usr_00005197_0001");
        request.AddParameter("grant_type", "password");

        var response = client.Execute(request);
        LitToken.Text = response.Content.ToString() + Environment.NewLine + "<br />";

        string json = response.Content.ToString();
        dynamic jsonObj = JsonConvert.DeserializeObject(json);
        //Console.WriteLine(jsonObj);
        var vtoken = jsonObj["access_token"].ToString();
        LitToken.Text += vtoken + Environment.NewLine;
        //LitToken.Text += "<br />" + ClassGlobal.varJSonREST;

        //RegistrarDatos(jsonObj["access_token"].ToString());
    }
    public void RegistrarDatos(string varTokenRest)
    {
        var client = new RestClient("https://dapimanager.minsa.gob.pe:8243/indicadores/v1.0.0/registrar-datos");
        RestRequest request = new RestRequest() { Method = Method.POST };

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Bearer " + varTokenRest);
        //request.AddBody(ClassGlobal.varJSonREST);
        request.AddJsonBody(ClassGlobal.varJSonREST);

        var response = client.Execute(request);
        LitToken.Text += "<p></p><p></p>" + response.Content.ToString() + Environment.NewLine + "<br />";

        string json = response.Content.ToString();
        dynamic jsonObj = JsonConvert.DeserializeObject(json);
        Console.WriteLine(jsonObj);
        var vtoken = jsonObj["codigo_respuesta"].ToString();
        LitToken.Text += vtoken + Environment.NewLine;
    }
}