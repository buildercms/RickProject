<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Login.Master" CodeBehind="Loginold.aspx.vb"  Inherits="RIS.Loginold" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager> 
    <div  class="right" id='maincontent'>
			
			<div class='row'>
                <div class="col-md-2"></div>
				<div class='col-md-6'>
					<table class="table">
					 <thead>
						<tr>
						  <th scope="col" colspan="2" class="text-center">User Login</th>
						  
						</tr>
					  </thead>
                        <tbody>
                                <tr>
                                    <td >
                                        &nbsp;
                                    </td>
                                    <td >
                                        <div>
                                            <span class="text-left" id="spanEmail">User Name</span>
                                        </div>
                                        <br />
                                        <input type="text" runat="server" id="txtUserId"  placeholder="User Name" maxlength="50" /><br />
                                        <%--<asp:TextBox ID="txtUserId" placeholder="User Name" MaxLength="50" runat="server" Width="170px"></asp:TextBox><br />--%>
                                        <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                                            ErrorMessage="User Name Required" Display="None" ValidationGroup="Login" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvUserId"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPassword"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td >
                                        <div>
                                            <span  id="spanPassword">Password</span>
                                        </div>
                                        <br />
                                        <input type="password" runat="server" id="txtPassword" placeholder="Password" maxlength="50"  /><br />
                                        <%-- <asp:TextBox ID="txtPassword" placeholder="Password" MaxLength="50"
                                            TextMode="Password" runat="server" Width="170px"></asp:TextBox><br />--%>
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="Password Required" Display="None" ValidationGroup="Login" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="text-center">
                                        <asp:Button runat="server" ID="btnLogin"  ValidationGroup="Login" CssClass="btn" Text="Login" Font-Bold="false"   />
                                    </td>
                                </tr>
                            <tr>
                                <td></td>
                                <td class="text-center">
                                    <asp:Label ForeColor="Blue"  runat="server" ID="lblmsg" Text="" Font-Bold ="true"></asp:Label>
                            <a href="#">Forgot Password</a><br />
                                </td>
                            </tr>
                            </tbody>
					</table>
                    <table id="tblWarning" runat="server"  class="table" style="display:none;">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label2" runat="server" Text="***Action Required***" Font-Size="Large" ForeColor="Red" Font-Bold="true" CssClass="blink"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label3" runat="server" Text="Turn on Compatibility view. Click " Font-Size="Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true">HERE</asp:HyperLink>--%>
                                        <a href="HelpDoc/Compatibility ViewIE10-11.pdf" target="_blank" class="doc">HERE</a>
                                        <asp:Label ID="Label4" runat="server" Text="for instructions or contact CMS Tech Support at 561-214-4780" Font-Size="Small" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                     
				</div>
				
			</div>
		</div>
     <script type="text/javascript">
        $(function () {
            $("#spanEmail,#spanPassword").css("display", "none");

            // alert(windowwidth);
            $("#txtUserId").bind("click focus", function () {
             
                $(this).attr("placeholder", "");
                //  if($(this).val()=="")
                $("#spanEmail").css("display", "inline");

                // $(this).attr("placeholder","User Name");

            });
            $("#txtUserId").blur(function () {
                $(this).attr("placeholder", "User Name");
                if ($(this).val() == "")
                    $("#spanEmail").css("display", "none");
                else
                    $("#spanEmail").css("display", "");

            });
            $("#txtPassword").bind("click focus", function () {

                $("#spanPassword").css("display", "");
                $(this).attr("placeholder", "");

            });
            $("#txtPassword").blur(function () {
                $(this).attr("placeholder", "Password");
                if ($(this).val() == "")
                    $("#spanPassword").css("display", "none");
                else
                    $("#spanPassword").css("display", "");
                //$(this).attr("placeholder", "Password");

            });
            $("#txtUserId").trigger("focus");

        });
    </script>
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
        .form-search-header {
            display:none !important;
        }

    </style>
     <style type="text/css">
        #maincontainer {
            background-color:black !important;
        }
    </style>
</asp:Content>

