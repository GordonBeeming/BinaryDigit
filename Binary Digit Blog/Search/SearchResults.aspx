<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="Binary_Digit_Blog.Search.SearchResults" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>
<%@ Register Src="~/Controls/BlogPostPreviews.ascx" TagPrefix="uc1" TagName="BlogPostPreviews" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Searching for </h1>
        <h2><%= this.BlogPostPreviews.SearchTerm %></h2>
    </hgroup>

    <article>
        <uc1:BlogPostPreviews runat="server" ID="BlogPostPreviews" Searching="true" />
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>
</asp:Content>