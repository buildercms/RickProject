<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="RIS.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta name="description" content=""/>
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE SELLING</title>    
      <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link href="css/styles.css" rel="stylesheet" />
</head>
<body id='login' class='text-center'>
    <form id="form1" runat="server">
         <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager> 
     			<img class="mb-4" src="images/loginlogo.png" alt="" />
      <label for="inputEmail" class="sr-only">User Name</label>
      <input type="text" runat="server" id="txtUserId" class="form-control" placeholder="User Name" />
      <label for="inputPassword" class="sr-only">Password</label>
      <input type="password" runat="server" id="txtPassword" class="form-control" placeholder="Password" />
         <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                                            ErrorMessage="User Name Required" Display="None" ValidationGroup="Login" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvUserId"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPassword"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
           <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="Password Required" Display="None" ValidationGroup="Login" SetFocusOnError="True"></asp:RequiredFieldValidator>
      <div class="checkbox mb-2">
        <label>
          <input type="checkbox" value="remember-me"/> Remember me
        </label>
          <label class="float-right">
          <span id="lblForgot Password" style="text-decoration:underline; cursor:pointer; color:white;">Forgot Password</span>
              </label>
      </div>
      
       
         <asp:Button runat="server" ID="btnLogin"  ValidationGroup="Login" CssClass="btn btn-lg btn-default btn-block" Text="Login" Font-Bold="false"   />
          <div class="checkbox mb-2">
        <label>
            <span id="spanPatentPending" style="text-align:center; color:white;margin-left:190px;font-size:0.8rem;">Patent Pending</span>
             </label>
      </div>
        <asp:Label ForeColor="Blue"  runat="server" ID="lblmsg" Text="" Font-Bold ="true"></asp:Label>
     <%-- <button class="btn btn-lg btn-default btn-block" type="submit">Sign in</button> --%>     
    <script src="js/jquery-3.2.1.slim.min.js" ></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/reimagine.js"></script>
    </form>
     <style type="text/css">
        .validatorCalloutHighlight
        {
            background-color: pink !important;
            border-color: Red!important;
            border-width: 2px!important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable
        {
            visibility: hidden!important;
        }
    

    </style>
  
</body>
</html>
