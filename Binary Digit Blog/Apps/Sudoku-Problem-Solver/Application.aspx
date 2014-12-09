<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SudokuProblemSolver.Web.Default" %>

<%@ Register Src="~/Apps/Sudoku-Problem-Solver/Controls/ctrlSudokuBoard.ascx" TagPrefix="uc1" TagName="ctrlSudokuBoard" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sudoku Problem Solver</title>
        <script src="/Scripts/jquery-1.8.3.js"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                    <Services>
                        <asp:ServiceReference Path="/Services/WSSudokuProblemSolver.asmx" />
                    </Services>
                </asp:ScriptManager>
                <uc1:ctrlSudokuBoard runat="server" id="ctrlSudokuBoard" />
            </div>
        </form>
    </body>
</html>