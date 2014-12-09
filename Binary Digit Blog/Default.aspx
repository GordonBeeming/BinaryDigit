<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Binary_Digit_Blog._Default" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>
<%@ Register Src="~/Controls/BlogPostPreviews.ascx" TagPrefix="uc1" TagName="BlogPostPreviews" %>


<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>Home.</h1>
        <h2>Welcome to B1n4ry D1g1t?</h2>
    </hgroup>

    <article>
        <h3>The name.</h3>
        <p>I use the name B1n4ry D1g1t as my alias across the internet with my tech based accounts. I originally used Binary Digit but as this name seems to be a frequently used one I started using B1n4ry D1g1t. Being a computer guy, I decided on a name that every none programmer associates with programming and that is Zero's and One's. I haven't yet decided which binary digit I am yet but maybe one day I will.</p>

        <h3>Recent Blog Posts - <a href="/Blog">view more</a></h3>
        <p>
            <uc1:BlogPostPreviews runat="server" id="BlogPostPreviews" ItemCount="5" />
        </p>

        <div class="myTwitter">
            <h3>Twitter</h3>
            <div class="tweets">
                <a class="twitter-timeline"  href="https://twitter.com/B1n4ry_D1g1t" data-widget-id="284596464771010561">Tweets by @B1n4ry_D1g1t</a>
                <script>
                    !function(d, s, id) {
                        var js, fjs = d.getElementsByTagName(s)[0];
                        if (!d.getElementById(id)) {
                            js = d.createElement(s);
                            js.id = id;
                            js.src = "//platform.twitter.com/widgets.js";
                            fjs.parentNode.insertBefore(js, fjs);
                        }
                    }(document, "script", "twitter-wjs");
                </script>
            </div>
        </div>
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>
</asp:Content>