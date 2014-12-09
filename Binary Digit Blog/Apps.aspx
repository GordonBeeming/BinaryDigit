<%@ Page Title="Applications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="Binary_Digit_Blog.Applications" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>Applications.</h1>
        <h2>Some random apps.</h2>
    </hgroup>

    <article>
        <p>
            Hi, below you can find random applications that are created to test and play around with Microsoft Technologies.
        </p>

        <section class="contact">
            <header>
                <h3><a href="/Apps/Sudoku-Problem-Solver/Default">Sudoku Problem Solver</a>:</h3>
            </header>
            <p>
                Sudoku Problem Solver was created to test the <a href="http://msdn.microsoft.com/en-us/devlabs/hh145003.aspx" target="_blank">Microsoft.Solver.Foundation</a>. Solver Foundation makes it easier to build and solve real optimization models. Solver Foundation includes a declarative modeling language (OML) for specifying optimization models; a .NET API and runtime (Solver Foundation Services) for model creation, reporting, and analysis; and powerful built-in solvers.
            </p>
        </section>
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>
</asp:Content>