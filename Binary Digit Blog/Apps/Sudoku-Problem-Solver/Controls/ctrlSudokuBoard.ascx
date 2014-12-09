<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlSudokuBoard.ascx.cs" Inherits="SudokuProblemSolver.Web.Apps.Sudoku_Problem_Solver.Controls.ctrlSudokuBoard" %>

<style>
    .sudokuBoard {
        background-color: #BBBBBB;
        background-image: url('/Apps/Sudoku-Problem-Solver/Images/User556x556.png');
        border: solid 1px #000000;
        height: 556px;
        width: 556px;
    }

    .sudokuBoard td.inputCell {
        border: solid 1px #000000;
        height: 50px;
        width: 50px;
    }

    .sudokuBoard td.inputCell input {
        border: none;
        font-size: 30px;
        height: 50px;
        padding: 0;
        text-align: center;
        vertical-align: middle;
        width: 50px;
    }

    .sudokuBoard td.spacerDownCell {
        background-color: #000000;
        border: solid 1px #000000;
        height: 50px;
        width: 20px;
    }

    .sudokuBoard td.spacerUpCell {
        background-color: #000000;
        border: solid 1px #000000;
        height: 20px;
        width: 50px;
    }

    .sudokuBoard td.spacerUpDownCell {
        background-color: #000000;
        border: solid 1px #000000;
        height: 20px;
        width: 20px;
    }
</style>

<table class="sudokuBoard" cellspacing="0" cellpadding="0">

    <tr>
        <%--Row Spacer 0--%>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
    </tr>

    <tr>
        <%--Row 0--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt00" />
        </td>
        <td class="inputCell">
            <input class="txt10" />
        </td>
        <td class="inputCell">
            <input class="txt20" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt30" />
        </td>
        <td class="inputCell">
            <input class="txt40" />
        </td>
        <td class="inputCell">
            <input class="txt50" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt60" />
        </td>
        <td class="inputCell">
            <input class="txt70" />
        </td>
        <td class="inputCell">
            <input class="txt80" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>
    </tr>

    <tr>
        <%--Row 1--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt01" />
        </td>
        <td class="inputCell">
            <input class="txt11" />
        </td>
        <td class="inputCell">
            <input class="txt21" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt31" />
        </td>
        <td class="inputCell">
            <input class="txt41" />
        </td>
        <td class="inputCell">
            <input class="txt51" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt61" />
        </td>
        <td class="inputCell">
            <input class="txt71" />
        </td>
        <td class="inputCell">
            <input class="txt81" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row 2--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt02" />
        </td>
        <td class="inputCell">
            <input class="txt12" />
        </td>
        <td class="inputCell">
            <input class="txt22" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt32" />
        </td>
        <td class="inputCell">
            <input class="txt42" />
        </td>
        <td class="inputCell">
            <input class="txt52" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt62" />
        </td>
        <td class="inputCell">
            <input class="txt72" />
        </td>
        <td class="inputCell">
            <input class="txt82" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row Spacer 1--%>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
    </tr>

    <tr>
        <%--Row 3--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt03" />
        </td>
        <td class="inputCell">
            <input class="txt13" />
        </td>
        <td class="inputCell">
            <input class="txt23" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt33" />
        </td>
        <td class="inputCell">
            <input class="txt43" />
        </td>
        <td class="inputCell">
            <input class="txt53" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt63" />
        </td>
        <td class="inputCell">
            <input class="txt73" />
        </td>
        <td class="inputCell">
            <input class="txt83" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row 4--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt04" />
        </td>
        <td class="inputCell">
            <input class="txt14" />
        </td>
        <td class="inputCell">
            <input class="txt24" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt34" />
        </td>
        <td class="inputCell">
            <input class="txt44" />
        </td>
        <td class="inputCell">
            <input class="txt54" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt64" />
        </td>
        <td class="inputCell">
            <input class="txt74" />
        </td>
        <td class="inputCell">
            <input class="txt84" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row 5--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt05" />
        </td>
        <td class="inputCell">
            <input class="txt15" />
        </td>
        <td class="inputCell">
            <input class="txt25" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt35" />
        </td>
        <td class="inputCell">
            <input class="txt45" />
        </td>
        <td class="inputCell">
            <input class="txt55" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt65" />
        </td>
        <td class="inputCell">
            <input class="txt75" />
        </td>
        <td class="inputCell">
            <input class="txt85" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row Spacer 2--%>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
    </tr>

    <tr>
        <%--Row 6--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt06" />
        </td>
        <td class="inputCell">
            <input class="txt16" />
        </td>
        <td class="inputCell">
            <input class="txt26" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt36" />
        </td>
        <td class="inputCell">
            <input class="txt46" />
        </td>
        <td class="inputCell">
            <input class="txt56" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt66" />
        </td>
        <td class="inputCell">
            <input class="txt76" />
        </td>
        <td class="inputCell">
            <input class="txt86" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row 7--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt07" />
        </td>
        <td class="inputCell">
            <input class="txt17" />
        </td>
        <td class="inputCell">
            <input class="txt27" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt37" />
        </td>
        <td class="inputCell">
            <input class="txt47" />
        </td>
        <td class="inputCell">
            <input class="txt57" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt67" />
        </td>
        <td class="inputCell">
            <input class="txt77" />
        </td>
        <td class="inputCell">
            <input class="txt87" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row 8--%>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt08" />
        </td>
        <td class="inputCell">
            <input class="txt18" />
        </td>
        <td class="inputCell">
            <input class="txt28" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt38" />
        </td>
        <td class="inputCell">
            <input class="txt48" />
        </td>
        <td class="inputCell">
            <input class="txt58" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

        <td class="inputCell">
            <input class="txt68" />
        </td>
        <td class="inputCell">
            <input class="txt78" />
        </td>
        <td class="inputCell">
            <input class="txt88" />
        </td>

        <td class="spacerDownCell">&nbsp;
        </td>

    </tr>

    <tr>
        <%--Row Spacer 3--%>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpCell">&nbsp;</td>
        <td class="spacerUpDownCell">&nbsp;</td>
    </tr>
</table>

<br />

<button type="button" onclick=" solvePuzzle() ">Solve</button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<button type="reset">Reset</button>


<script type="text/javascript">
    var baseTime = 100;
    var currentAnimation = 1;
    var min = 0;
    var max = 9;

    var values = {};
    var _doneToDo;

    function solvePuzzle() {
        animateBoard(0, 9, 0, function(x, y) {
            //values.00 = $('.txt" + x + y + " input').val();
            setTimeout("values.xy" + x + y + " = $('.txt" + x + y + "').val();", 0);
        }, function() {
            setTimeout("GetSolvedProblem();", 10);
        });
    }

    function animateBoard(newMin, newMax, animationType, workToDo, doneToDo) {
        _doneToDo = doneToDo;
        min = newMin;
        max = newMax;
        currentAnimation = animationType;
        if (min < max) {
            for (var y = min; y < max; y++) {
                for (var x = min; x < max; x++) {
                    if (currentAnimation == 0) {
                        setTimeout("$('.txt" + x + y + "').fadeOut();", (y * baseTime + x * baseTime));
                    } else if (currentAnimation == 1) {
                        setTimeout("$('.txt" + x + y + "').fadeOut();", (y * 9 * baseTime + x * baseTime));
                    }
                    if (workToDo != undefined) {
                        workToDo(x, y);
                    }
                }
            }
        } else {
            for (var y = min; y > max; y--) {
                for (var x = min; x > max; x--) {
                    if (currentAnimation == 0) {
                        setTimeout("$('.txt" + x + y + "').fadeIn();", Math.abs((y * baseTime + x * baseTime) - (8 * baseTime + 8 * baseTime)));
                    } else if (currentAnimation == 1) {
                        setTimeout("$('.txt" + x + y + "').fadeIn();", Math.abs((y * 9 * baseTime + x * baseTime) - (8 * 9 * baseTime + 8 * baseTime)));
                    }
                    if (workToDo != undefined) {
                        workToDo(x, y);
                    }
                }
            }
        }
        if (doneToDo != undefined) {
            setTimeout("_doneToDo()", 2000);
        }
    }

    function GetSolvedProblem() {
        SudokuProblemSolver.Web.Services.WSSudokuProblemSolver.Solve(values, function(e) {
            values = e;
            if (values.error != "") {
                alert(values.error);
            } else {
                animateBoard(8, -1, 0, function(x, y) {
                    setTimeout("$('.txt" + x + y + "').val(values.xy" + x + y + ");", 0);
                }, function() {

                });
            }
        });
    }
</script>