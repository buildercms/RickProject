<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test.aspx.vb" Inherits="RIS.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .page_dimmer {
    position: fixed;
    height: 100%;
    width: 100%;
    top: 0px;
    left: 0px;
    background-color: #000000;
    z-index: 50;
    filter: alpha(opacity=50);
    /*-moz-opacity: 0.5;
    -khtml-opacity: 0.5;*/
    opacity: 0.7;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <a><img src='images/logo.png' alt='logo' onclick="javascript:window.location.href='home.aspx'"></a>
       <h3>Upload File using Jquery AJAX in Asp.net</h3>  
         <div id="divhide" class="page_dimmer" style="display: none;"></div>
    <table>  
        <tr>  
        <td>File:</td>  
        <td>  
            <asp:FileUpload ID="fupload" runat="server"   /></td>  
        <td><asp:Image ID="imgprv" runat="server" Height="90px" Width="75px"  /></td>  
        </tr>  
        <tr>  
        <td></td>  
        <td><asp:Button ID="btnUpload" runat="server" cssClass="button" Text="Upload Selected File" /></td>  
        </tr>  
    </table>  
    </form>
      <link href="css/styles.css" rel="stylesheet" type="text/css" />

    <%-- <script src="js/jquery-2.1.1.js"></script>--%>
    <script src="js/jquery-3.2.1.slim.min.js"></script>
    <script src="js/jquery-2.1.1.js"></script>
    <%--<script src="js/jquery-1.4.1.min.js"></script>--%>


    <%--  <script src="js/jquery-1.4.1.min.js"></script>--%>
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <link href="https://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap.min.js"></script>
    <script src="js/reimagine.js"></script>
    <script src="ajax-loading.js"></script>
    <script type="text/javascript">
        var loading = $.loading();
        $("#fupload").change(function (evt) {  
            var fileUpload = $("#fupload").get(0);  
            var files = fileUpload.files;  
  
            var data = new FormData();  
            for (var i = 0; i < files.length; i++) {  
                data.append(files[i].name, files[i]);  
                data.append("userid", 3);
                data.append("propertyid",5)
            }  
  
            $.ajax({  
                url: "FileUploadHandler.ashx",  
                type: "POST",  
                data: data,  
                contentType: false,  
                processData: false,  
                beforeSend: function () {
                    $("#divhide").show();
              loading.open();
           },
                success: function (result) { loading.close(); $("#divhide").hide(); alert(result);},  
                error: function (err) {  
                    alert(err.statusText);
                    loading.close();
                     $("#divhide").hide();
                }  
            });  
  
            evt.preventDefault();  
        });  
    </script>
</body>
</html>
