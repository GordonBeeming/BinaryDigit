<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Binary_Digit_Blog.Contact" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: this.Title %>.</h1>
        <h2>Use the details below to get in touch.</h2>
    </hgroup>

    <article>
        <div style="height: 570px;">
            <script type="text/javascript"> id = 154918; </script>
            <script type="text/javascript" src="http://kontactr.com/wp.js"></script>
        </div>
    </article>
    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
        
        <section class="contact">
            <header>
                <h3>Email:</h3>
            </header>
            <p>
                <span class="label">Support:</span>
                <span>
                    <script> document.write("<a href='mailto:support@beeming.co.za'>support@beeming.co.za</a>"); </script>
                </span>
            </p>
            <p>
                <span class="label">General:</span>
                <span>
                    <script> document.write("<a href='mailto:general@beeming.co.za'>general@beeming.co.za</a>"); </script>
                </span>
            </p>
        </section>
    </aside>
</asp:Content>