<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBox.ascx.cs" Inherits="Binary_Digit_Blog.Controls.SearchBox" %>


<h3>Search</h3>
<asp:TextBox ID="txtSearchTerm" runat="server" Width="100%"></asp:TextBox><br />
<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick=" return searchInputValid(); " />

<script>
    $(document).ready(function() {
        $("#<%= this.txtSearchTerm.ClientID %>").keypress(function(e) {
            if (e.keyCode == 13 || e.keyCode == 13) {
                document.getElementById("<%= this.txtSearchTerm.ClientID %>").click();
            }
        });
    });

    function searchInputValid() {
        return $.trim(document.getElementById("<%= this.txtSearchTerm.ClientID %>").value).length > 0;
    }
</script>