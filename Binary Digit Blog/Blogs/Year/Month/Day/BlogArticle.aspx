<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlogArticle.aspx.cs" Inherits="Binary_Digit_Blog.Blogs.Year.Month.Day.BlogArticle" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <script type="text/javascript"> var switchTo5x = true; </script>
    <script type="text/javascript" src="http://w.sharethis.com/button/buttons.js"></script>
    <script type="text/javascript" src="http://s.sharethis.com/loader.js"></script>

    <link href="/Content/prettyPhoto.css" rel="stylesheet" />

    <script src="/Scripts/jquery.prettyPhoto.js"></script>

    <script>
        $(document).ready(function() {
            //$(".blogArticleContent img").css({ maxWidth: "96%" });
            //$(".blogArticleContent img").prettyPhoto();
        });
    </script>

    <style>
        article {
            float: left;
            width: 100%;
        }

        aside {
            border: 1px solid #EFEFEF;
            display: inline-block;
            float: right;
            margin-bottom: 20px;
            margin-left: 20px;
            padding: 0 15px 15px;
            width: 240px;
        }

        .blogArticleContent { border: none; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <hgroup class="title">
        <h1>
            <asp:Literal ID="litTitle" runat="server"></asp:Literal>.
        </h1>
        <h2>
            <asp:Literal ID="litDateOfPublish" runat="server"></asp:Literal>.
        </h2>
        <br />
        <small><i>
                   <asp:Literal ID="litTags" runat="server"></asp:Literal>
               </i></small>
    </hgroup>
    <article>
        <div class="blogArticleContent">

            <aside>
                <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" ShowGoogleAdd="false" />
            </aside>
            <asp:Literal ID="litContent" runat="server"></asp:Literal>
        </div>
        <br />
        <br />
        <div id="disqus_thread"></div>
        <script type="text/javascript">
            /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
            var disqus_shortname = 'b1n4ryd1g1t'; // required: replace example with your forum shortname

            /* * * DON'T EDIT BELOW THIS LINE * * */
            (function() {
                var dsq = document.createElement('script');
                dsq.type = 'text/javascript';
                dsq.async = true;
                dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
                (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
            })();
        </script>
        <noscript>
            Please enable JavaScript to view the
            <a href="http://disqus.com/?ref_noscript">comments powered by Disqus.</a>
        </noscript>
        <a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">Disqus</span>
        </a>

    </article>

    <script type="text/javascript"> stLight.options({ publisher: "73b85ae5-7363-4c2f-913b-d12a47c5493f", doNotHash: false, doNotCopy: false, hashAddressBar: true }); </script>
    <script>
        var options = { "publisher": "73b85ae5-7363-4c2f-913b-d12a47c5493f", "position": "left", "ad": { "visible": false, "openDelay": 5, "closeDelay": 0 } };
        var st_hover_widget = new sharethis.widgets.hoverbuttons(options);
    </script>
</asp:Content>