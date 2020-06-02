<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="RIS.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/jquery.range-min.js"></script>
    <link href="css/jquery.range.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $('.slider-input').jRange({
                from: 1,
                to: 10,
                step: 1,
                scale: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
                format: '%s',
                width: 500,
                showLabels: true,
                snap: true,

            });
            $(".slider-input").val("0")
            //$(".slider-input").change(function () {

            //    var rangesValues = $(".slider-input").val();

            //    alert(rangesValues)

            //});
        });
    </script>
    <style type="text/css">
        .slider-container .scale ins {
            font-size: 14px;
            text-decoration: none;
            position: absolute;
            left: 0;
            top: 5px;
            color: #999;
            line-height: 2;
        }
       .theme-green .back-bar .selected-bar{
                width: 222px;
    left: 0px;
    background-color: unset;
    background-image: none !important;
        }
        .theme-green .back-bar .pointer {
    width: 14px;
    height: 14px;
    top: -5px;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    box-sizing: border-box;
    border-radius: 10px;
    border: 1px solid red;
    background-color: red;
    background-image: linear-gradient(to bottom, red, red);
    background-repeat: repeat-x;
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffeeeeee', endColorstr='#ffdddddd', GradientType=0);
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" class="slider-input" value="0" />
        </div>
    </form>
</body>
</html>
