<%@ Page Title="My Blog Listing" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Binary_Digit_Blog.Account.Blogs.List" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>Blog Listing.</h1>
        <h2>You blogs.</h2>
    </hgroup>

    <article>
        <asp:GridView ID="gvData" runat="server" CssClass="list" AutoGenerateColumns="false" Width="100%">
            <EmptyDataTemplate>
                <p>no data found</p>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Title" DataField="PageTitle" ItemStyle-Width="300px" />
                <asp:BoundField HeaderText="Publish Date" DataField="PublishDate" ItemStyle-Width="200px" DataFormatString="{0:dd MMM yyyy HH:mm}" />
                <asp:TemplateField ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CommandArgument='<%# Eval("ArticleGuid") %>' />
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClientClick=" return confirm('Remove this blog post?') " OnClick="btnRemove_Click" CommandArgument='<%# Eval("ArticleGuid") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>

</asp:Content>