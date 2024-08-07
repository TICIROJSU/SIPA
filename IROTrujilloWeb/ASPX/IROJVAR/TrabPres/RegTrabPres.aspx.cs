using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ASPX_IROJVAR_TrabPres_RegTrabPres : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		int vd, vm, vy;
		vm = DateTime.Now.Month;
		vy = DateTime.Now.Year;

		CargaCalendario(vy, vm);
	}

	public void CargaCalendario(int vAnio, int vMes)
	{
		string lhtml = "";
		//vMes = 6;
		int Days = DateTime.DaysInMonth(vAnio, vMes);
		byte dia1 = (byte)DateTime.Now.DayOfWeek;
		DateTime dia = Convert.ToDateTime("14-02-2021");

		try
		{

			lhtml += "<table> ";
			lhtml += "	<thead class='fc-head'> ";
			lhtml += "		<tr> ";
			lhtml += "			<th>Dom</th> ";
			lhtml += "			<th>Lun</th><th>Mar</th><th>Mie</th> ";
			lhtml += "			<th>Jue</th><th>Vie</th><th>Sab</th> ";
			lhtml += "		</tr> ";
			lhtml += "	</thead> ";
			lhtml += "	<tbody class='fc-body'> ";

			DateTime vFechaTrab = Convert.ToDateTime("01-" + vMes + "-" + vAnio);
			DateTime vFechaFor = vFechaTrab;

			for (int i = 1; i <= Days; i++)
			{
				lhtml += "<tr height='70'> ";
				for (int j = 0; j <= 6; j++)
				{
					string vDia = "", vDiachk = "", vDiamsg = "";
					int diaSemNro = (int)vFechaFor.DayOfWeek;
					if (diaSemNro == j)
					{
						vDia = i.ToString();
						vDiachk = "<span><input type='checkbox' id='chkDia" + vDia + "' ></span> ";
						vDiamsg = "Meet";
						vFechaFor = vFechaTrab.AddDays(i);
						i++;
					}
					if (i > Days+1)
					{
						vDia = ""; vDiachk = ""; vDiamsg = "";
					}
					lhtml += "	<td> ";
					lhtml += "		<h3><span id='' class='pull-right'>" + vDiachk + vDia + "</span></h3> ";
					//lhtml += "  " + vDiachk;
					lhtml += "		<span >" + vDiamsg + "</span> ";
					lhtml += "	</td> ";
				}
				lhtml += "</tr> ";
				i--;
			}

			lhtml += "	</tbody> ";
			lhtml += "</table> ";

			LitCalendar.Text = lhtml;
		}
		catch (Exception ex)
		{
			LitCalendar.Text += ex.Message.ToString();
		}


		//LitCalendar.Text += "<br />" + Days.ToString() + " - ";
		//LitCalendar.Text += dia1.ToString() + " - ";
		//LitCalendar.Text += DateTime.Now.ToString() + " - ";
		//LitCalendar.Text += " <br /> ";
		//LitCalendar.Text += ((byte)dia.DayOfWeek).ToString() + " - ";
		//LitCalendar.Text += dia.ToString() + " - ";



	}

}