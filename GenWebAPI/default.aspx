<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GenWebAPI._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate WebAPI ZIP</title>
</head>
<style type="text/css">
    .main {
        margin-left: 30px;
        margin-right: 30px;
        margin-bottom: 15px;
        padding: 15px;

        background-color: #ebebeb;
    }
    .download {
        background-color:deepskyblue;
        padding:10px;
    }
</style>
<body>
    <form id="form1" runat="server">
        <h1>Generate WebAPI ZIP&nbsp; <img src="API-icon.png" height="50" /></h1>
        <div class="main">
            <p>Type a new project name and click download ZIP.</p>
            <p>Template project will have all internal references renamed  (CSPROJ, CS, assembly properties, namespace, etc.).   After project opens press F5 to run and view homepage in the browser.  Clicking on "api/Hello" will execute the default controller and return JSON/XML response HTTP 200 with the current time.  Enjoy!</p>
            <br />
            <p>Much faster than making a new WebAPI project, configuring CORS, configuring Windows Auth, creating readme.aspx showing build number.  More than a base template this has real world WebAPI configurations ready to go.</p>
            
            <hr />
            <br />
            <ol>
                <asp:Label ID="Label1" runat="server" Text="Web API Name"></asp:Label>
                <li>
                    <asp:TextBox ID="ProjectName" runat="server"></asp:TextBox></li>
                <br />
                <li>
                    <asp:Button CssClass="download" ID="Generate" runat="server" Text="Download Visual Studio ZIP" OnClick="Generate_Click" />
                </li>
                <br />
                <li>Press F5 to run and test</li>
            </ol>
            <br />
            <hr />
            <img src="tree.gif" /><br />
            <img src="1.gif" /><br />
            <img src="2.gif" />
            <br />


        </div>

        <hr />
        
        <div>
            <img src="https://en.gravatar.com/userimage/46254763/1789248e2a473e8fbfa80818faf70551.jpeg" height="40" width="40" />&nbsp;
            <a href="https://twitter.com/spjeff">@spjeff</a>&nbsp;|&nbsp;
            <a href="http://www.spjeff.com/">spjeff.com</a>
        </div>

    </form>

    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%= ProjectName.ClientID%>').focus();
        };
    </script>
</body>
</html>
