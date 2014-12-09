<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultAsideContent.ascx.cs" Inherits="Binary_Digit_Blog.Controls.DefaultAsideContent" %>
<%@ Register Src="~/Controls/TagCloud.ascx" TagPrefix="uc1" TagName="TagCloud" %>
<%@ Register Src="~/Controls/SearchBox.ascx" TagPrefix="uc1" TagName="SearchBox" %>
<%@ Register Src="~/Controls/ArticleArchiveTree.ascx" TagPrefix="uc1" TagName="ArticleArchiveTree" %>



<uc1:SearchBox runat="server" ID="SearchBox" />
<br /><br />


<uc1:TagCloud runat="server" ID="TagCloud" />
<br /><br />


<uc1:ArticleArchiveTree runat="server" ID="ArticleArchiveTree" />
<br /><br />

<% if (this.ShowGoogleAdd)
   { %>
    <div class="GoogleAd">
        <h3>Google Ad</h3><br />
        <div style="margin: 0 auto; width: 160px;">
            <script type="text/javascript">
<!--
                google_ad_client = "ca-pub-6150916001722677";
                /* Side Bar */
                google_ad_slot = "4879596604";
                google_ad_width = 160;
                google_ad_height = 600;
                //-->
            </script>
            <script type="text/javascript"
                    src="http://pagead2.googlesyndication.com/pagead/show_ads.js">

            </script>
        </div>
        <br /><br />
    </div>
<% } %>