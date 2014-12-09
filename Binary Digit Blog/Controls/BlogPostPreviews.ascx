<%@ Control Language="C#" EnableTheming="false" AutoEventWireup="true" CodeBehind="BlogPostPreviews.ascx.cs" Inherits="Binary_Digit_Blog.Controls.BlogPostPreviews" %>

<asp:Repeater ID="rptBlogs" runat="server">
    <ItemTemplate>
        <div class="blogItem">
        <h3>
            <a href='<%# GetLinkUrl() %>'><%# HttpUtility.HtmlEncode(Eval("PageTitle").ToString()) %></a>
        </h3>
        <small><i><%# ((DateTime)Eval("PublishDate")).ToString("dd MMMM yyyy") %></i></small><br />
        <p><%# HttpUtility.HtmlEncode(Eval("AutoBlurb").ToString()) %></p>
        </di>
    </ItemTemplate>
</asp:Repeater>