<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Binary_Digit_Blog.About" %>

<%@ Register Src="~/Controls/DefaultAsideContent.ascx" TagPrefix="uc1" TagName="DefaultAsideContent" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>About.</h1>
        <h2>Who am I?</h2>
    </hgroup>

    <article>
        <p>
            Hi, my name is Gordon Beeming. I am <%= this.GetAge() %> years old and love coding. I enjoy spending time with my family and relaxing. I play XBox from time to time, really enjoy playing on-line first person team matches in various games.
        </p>

        <p>
            I am a Software developer from the awesome city of Durban in South Africa. I enjoy learning new things through code, I obsess over performance and streamlining applications but sometimes just want to get things done =D. I dev across multiple .net platforms and love "wasting" time making things look "awesome" using
            <a href="http://jquery.com">jquery</a> when I'm bored. I am also a MCP (profile card below). I am very passionate about any development that will make me think and keep my brain running.
        </p>
        <p>
            I don't like typing a lot so most "Tutorial" type blogs will be all the code in one shot, I normally look for code like that and dislike having to stitch pieces of code together as there is normally pieces missing so I wouldn't that to happen to people using my code.
        </p>
        <div class="microsoftProfile">
            <h3>Microsoft Profile</h3>
            <p>
                <iframe frameborder="0" scrolling="no" width="400px" height="177px" src="https://www.mcpvirtualbusinesscard.com/VBCServer/GordonBeeming/interactivecard"></iframe>
            </p>
        </div>
    </article>

    <aside>
        <uc1:DefaultAsideContent runat="server" ID="DefaultAsideContent" />
    </aside>
</asp:Content>