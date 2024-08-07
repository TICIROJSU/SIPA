using RestSharp;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

public partial class ASPX_Terminos_PruebasRestDNI : System.Web.UI.Page
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
		////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
		ObtenerToken();
	}
	public void ObtenerToken()
	{
		//var client = new RestClient("https://cel.reniec.gob.pe/celweb/consultaCredencialCaduca");
		var client = new RestClient("https://cel.reniec.gob.pe/celweb/mensaje");
		RestRequest request = new RestRequest() { Method = Method.POST };

		request.AddHeader("Content-Type", "application/json;charset=utf-8");
		request.AddHeader("Access-Control-Allow-Origin", "*");
		//request.AddHeader("Authorization", "Basic VndvUVI5TVMwRDluV3hmRFQ4bmJSMUVmbUU0YTowc1BvX1BOVWN1Y295c2ZGbjlyTE41WlQzRWNh");
		//request.AddParameter("coServicio", "4");
		//request.AddParameter("coUsuario", "45545663");
		//request.AddParameter("ruc", "null");
		//request.AddParameter("credencial", "null");
		//request.AddParameter("tipoAutenticacion", "0");
		//request.AddParameter("coEmpresa", "null");
		//request.AddParameter("coGrupoServicio", "null");

		var response = client.Execute(request);
		LitToken.Text = "<br />" + response.Content.ToString() + Environment.NewLine + "<br /><br />";

		string json = response.Content.ToString();
		dynamic jsonObj = JsonConvert.DeserializeObject(json);
		//Console.WriteLine(jsonObj);
		//LitToken.Text += json;

		var vtoken = jsonObj["coError"].ToString();
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