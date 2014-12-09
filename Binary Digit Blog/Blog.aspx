<%@ Page Title="Blog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="Binary_Digit_Blog.Blog" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>
<%@ Register Src="~/Controls/BlogPostPreviews.ascx" TagPrefix="uc1" TagName="BlogPostPreviews" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Blogs.</h1>
        <h2>Things that I want to share.</h2>
    </hgroup>
    <article>
        <uc1:BlogPostPreviews runat="server" ID="BlogPostPreviews" />
    </article>

    
    <aside>
        <uc1:DefaultAsideContent runat="server" id="DefaultAsideContent" />
    </aside>
</asp:Content>