<%@ Page Title="File Browser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileBrowser.aspx.cs" Inherits="Binary_Digit_Blog.Account.Blogs.FileBrowser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1>File Browser.</h1>
        <h2>All files currently active in the database</h2>
    </hgroup>

    <article>
        <h3>Files</h3>
        <asp:GridView ID="gvData" runat="server" CssClass="list" AutoGenerateColumns="false" Width="100%">
            <EmptyDataTemplate>
                <p>no data found</p>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="File Name" ItemStyle-Width="190px">
                    <ItemTemplate>
                        <a href='<%# Eval("CalcFileRawUrl") %>'><%# GetFileSize() %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Raw Url" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <%# Eval("CalcFileRawUrl") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Date Time Stamp" DataField="RecordInfo.CreateDateTime" ItemStyle-Width="150px" DataFormatString="{0:dd MMM yyyy HH:mm:ss}" />
                <asp:TemplateField ItemStyle-Width="130px">
                    <ItemTemplate>
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClientClick=" return confirm('Remove this smart file?') " OnClick="btnRemove_Click" CommandArgument='<%# Eval("SmartGuid") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </article>

    <aside>
        <h3>Upload Files</h3>
        <div>
            <asp:FileUpload ID="upFile" runat="server" /><br />
            <asp:Button ID="btnUploadFile" runat="server" Text="Upload" OnClick="btnUploadFile_Click" />
        </div>
    </aside>
</asp:Content>