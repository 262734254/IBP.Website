<%@ Page Language="C#" AutoEventWireup="true" Inherits="IBP.WapSite.Default" Codebehind="Default.aspx.cs" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<%@ Register src="LoginPanel.ascx" tagname="LoginPanel" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form id="Form1" runat="server">
        <uc1:LoginPanel ID="LoginPanel1" runat="server" />
    </mobile:Form>    
</body>
</html>
