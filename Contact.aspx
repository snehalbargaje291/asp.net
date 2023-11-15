<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MiniProject.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center" style="background-color: #99CCFF"><%: Title %>.</h2>
    <h3 class="text-center">Your contact page.</h3>
    <address class="text-center">
        This is address<br />
        xuyrtefibtevgdiuvyeu<br />
        <abbr title="Phone">P:</abbr>
        946573647
    </address>

    <address class="text-center">
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@myminiproject.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@myminiproject.com</a>
    </address>
</asp:Content>
