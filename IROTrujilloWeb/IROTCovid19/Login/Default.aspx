<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="IROTCovid19_Login_Default" %>

<!DOCTYPE html>
<html><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
  
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, user-scalable=no">
  <link rel="stylesheet" href="../Estilos/sb-admin-2.min.css">
  <link rel="stylesheet" href="../Estilos/main.css">
  <link rel="icon" type="image/png" href="https://logincovid19.minsa.gob.pe/static/minsalogin/img/favicon.png">
  
  <title>Login Minsa</title>
</head>
<body class="bodyLogin">
  <header>
    <div class="navbar nav-default bg-danger box-shadow">
        <div class="container d-flex justify-content-between">
            <a href="https://logincovid19.minsa.gob.pe/accounts/login/?app_identifier=pe.gob.minsa.atencioncovid19&amp;login_uuid=#" class="navbar-brand d-flex align-items-center">
                <img class="logo-minsa" src="../Estilos/logo-minsa.jpeg" alt="minsa">
                <span class="covid-19">COVID-19</span>
            </a>
        </div>
    </div>
  </header>
  <div class="container-login">

    <div class="container">

      <div class="row justify-content-center">

        <div class="col-xl-10 col-lg-12 col-md-9 mt-5">

          <div class="card o-hidden border-0 shadow-lg my-5">
            <div class="card-body p-0">
              <div class="row">
                <div class="col-lg-6 d-none border-right d-flex justify-content-center align-items-center">
                  <img class="logo-login-minsa" src="../Estilos/LogoIRO.png">
                </div>
                <div class="col-lg-6">
                  <div class="p-5">
                    <div class="text-center">
                      <h1 class="h4 text-gray-900 mb-4">COVID 19</h1>
                    </div>
                    
					<%--<form class="user" action="https://logincovid19.minsa.gob.pe/accounts/login/" method="post">--%>
					<form class="user" ID="FormView1" runat="server" >
                      <input type="hidden" name="csrfmiddlewaretoken" value="iiu2WjF6CdMLIL8AffqjOx7Tr0QDKaRHgVMh9elWbBghB3ADNkRNdajA0heqywzc">
                      <div class="form-group">
                        <%--<input type="text" class="" placeholder="Usuario" name="username" autofocus="" required="">--%>
<asp:TextBox ID="username" runat="server" class="form-control form-control-user" placeholder="Usuario" ></asp:TextBox>
                      </div>
                      <div class="form-group">
                        <%--<input type="password" class="" placeholder="Contraseña" name="password" required="">--%>
<asp:TextBox ID="password" runat="server" TextMode="Password" class="form-control form-control-user" placeholder="Contraseña" ></asp:TextBox>
                      </div>
                      
                      <%--<button class="btn btn-secondary btn-user btn-block" id=""> Ingresar</button>--%>
<asp:Button ID="botonEnviarLogin" runat="server" Text="Ingresar" class="btn btn-secondary btn-user btn-block" OnClick="botonEnviarLogin_Click" />
                    </form>

                  </div>
                </div>
              </div>
            </div>
          </div>

        </div>

      </div>

    </div>

  </div>

  


</body></html>