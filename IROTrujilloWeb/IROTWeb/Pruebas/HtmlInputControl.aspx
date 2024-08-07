<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlInputControl.aspx.cs" Inherits="IROTWeb_Pruebas_HtmlInputControl" EnableEventValidation="false" %>

<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head>
    <title> HtmlInputControl Name Example </title>
<script runat="server">

    void Page_Load(Object sender, EventArgs e)
    {
        // Create a data source.
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("Value", typeof(string)));
        for (int i = 0; i < 4; i++)
        {
            dr = dt.NewRow();
            dr[0] = "Item " + i.ToString();
            dt.Rows.Add(dr);
        }
        // Bind the data source to the Repeater control.
        Repeater1.DataSource = new DataView(dt);
        Repeater1.DataBind();
    }

    void AddButton_Click(Object sender, EventArgs e)
    {

        Message.Text = "The name of the HtmlInputControl clicked is " +
                        ((HtmlInputControl)sender).Name;
        Message.Text += "<br />" + ((Object)sender).ToString();
        Message.Text += "<br />" + ((HtmlInputControl)sender).Value;
        Message.Text += "<br />" + ((HtmlInputControl)sender).UniqueID;
        Message.Text += "<br />" + ((HtmlInputControl)sender).TagName;
        Message.Text += "<br />" + ((HtmlInputControl)sender).ClientID;
    }
   </script>
</head>

<body>
   <form id="form1" runat="server">
      <h3> HtmlInputControl Name Example </h3>
      <asp:Repeater id="Repeater1" runat="server">
         <ItemTemplate>
            <input type="submit" name="AddButton" onserverclick="AddButton_Click" runat="server" 
                   value='<%# DataBinder.Eval(Container.DataItem, "Value") %>' />
         </ItemTemplate>
      </asp:Repeater>
      <br /><br />
      <asp:Label id="Message" runat="server"/>
   </form>
</body>
</html>
