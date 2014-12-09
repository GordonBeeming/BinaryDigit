<%@ Page Title="Sudoku Problem Solver" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Binary_Digit_Blog.Apps.Sudoku_Problem_Solver.Default" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Sudoku Problem Solver.</h1>
        <h2>We should be able to solve anything.</h2>
    </hgroup>

    <article>
        <iframe border="0" width="620px" height="620" src="Application.aspx" style="border: 0 none;"></iframe>
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>
</asp:Content>