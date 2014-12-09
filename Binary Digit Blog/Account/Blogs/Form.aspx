<%@ Page Title="My Blog Form" Language="C#" ValidateRequest="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Binary_Digit_Blog.Account.Blogs.Form" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(function() {
            $(".datepicker").datepicker({
                dateFormat: 'd MM yy'
            });
            $(".datepicker").keydown(function() {
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form">
        <div class="field">
            <label>Title: <asp:HyperLink ID="lnkViewOnSite" runat="server" Visible="false">view on site</asp:HyperLink></label>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        </div>
        
        <div class="field">
            <label>Publish Date:</label>
            <asp:TextBox ID="txtPublishDate" runat="server" CssClass="datepicker"></asp:TextBox>
        </div>
        
        <div class="field">
            <label>&nbsp;</label>
            &nbsp;
        </div>
        
        <div class="field">
            <label>Publish Time:</label>
            <asp:TextBox ID="txtDateHour" runat="server" Width="50px"></asp:TextBox>
            h
            <asp:TextBox ID="txtDateMinute" runat="server" Width="50px"></asp:TextBox>
        </div>

        <div class="field">
            <label>Keywords:</label>
            <asp:TextBox ID="txtKeywords" runat="server"></asp:TextBox>
        </div>

        <div class="field">
            <label>Technologies:</label>
            <asp:TextBox ID="txtTechnologies" runat="server"></asp:TextBox>
        </div>
        
        <div class="field double">
            <label>Blurb: - <asp:LinkButton ID="btnGetFromContent" runat="server" OnClick="btnGetFromContent_Click">Parse Article Content</asp:LinkButton></label>
            <label id="blurbLetterCount"></label>
            <asp:TextBox ID="txtBlub" runat="server" TextMode="MultiLine" Width="810px" Rows="4"></asp:TextBox>
        </div>
        
        <div class="field double">
            <label>Content:</label>
            <CKEditor:CKEditorControl ID="txtArticleContent" BasePath="/ckeditor/" Height="400px" runat="server"></CKEditor:CKEditorControl>
        </div>
        
        <div class="field double">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <div class="clear-fix"></div>
        </div>
    </div>

    <script>
        $(document).ready(function() {
            checkBlurbCount();
            $("#<%= this.txtBlub.ClientID %>").keypress(function() {
                checkBlurbCount();
            });
            $("#<%= this.txtBlub.ClientID %>").keyup(function() {
                checkBlurbCount();
            });
            $("#<%= this.txtBlub.ClientID %>").keydown(function() {
                checkBlurbCount();
            });
        });

        function checkBlurbCount() {
            $("#blurbLetterCount").html($("#<%= this.txtBlub.ClientID %>").val().length);
        }
    </script>

</asp:Content>