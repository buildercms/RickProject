<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MyProfile.aspx.vb" Inherits="RIS.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
     <div class="right" id='maincontent'>
         <div class="row">
            <div class="col-md-8">
                <h2>Settings</h2><br />
            </div>
             <div class="col-md-4" style="left:20px">
                 <button class="btn float-right font-weight-bold align-text-bottom"   id="btnEditProfile" onclick="javascript:window.location.href='EditProfile.aspx?mode=view';" type="button">Edit Profile</button>
             </div>
        </div>
         <div class="row" id="divSearchGrid" runat="server">
                <div class='col-md-12'>
                    <div class="card"  style="width:102.5%">
                        <div class="card-header">
                            <a style="color: #62DCBF; font-weight: bold">My Profile
                            </a>
                        </div>
                        <div class="card-body">
                            <div class="row">
                            <div class="col-md-2">
                                 <div class='profileinfo' style="margin: 100px 0px 30px 0px;text-align: center;">
                        <div class='image'>
                            <img src='images/logo.png' id="imgLogo" runat="server" style="cursor:pointer;" alt='' height='100' /></div>
                                     <a id="lnkFileUpload" style="cursor:pointer; text-decoration:underline; color:#5addbe">Edit Photo</a>
                                     <asp:FileUpload ID="fupPhoto1" runat="server" Style="display: none;" />
                                     <asp:Button ID="btnSaveLogo" runat="server" Text="Save" style="display:none;" OnClick="btnSaveLogo_Click" />
                                     </div>
                                <br />
                                <div style="margin-top:-90px;">
                                 <div class='profileinfo' style="margin: 100px 0px 30px 0px;text-align: center;">
                        <div class='image'>
                            <img src='images/logo.png' id="imgEditLogo" runat="server" style="cursor:pointer;" alt='' height='100' /></div>
                                     <a id="lnkLogoFileUpload" style="cursor:pointer; text-decoration:underline; color:#5addbe">Edit Logo</a>
                                     <asp:FileUpload ID="fupLogoPhoto1" runat="server" Style="display: none;" />
                                     <asp:Button ID="btnSaveEditLogo" runat="server" Text="Save" style="display:none;" OnClick="btnSaveEditLogo_Click" />
                                     </div>
                                    </div>
                            </div>
                               
                                  
                            <div class='col-md-8'style="left: 50px;">

                <div class='box' style="padding:25px !important">
                    <h5>Contact Information:</h5>
                    <table class='table' style="font-size: 12.5px;">
                        <tr>
                            <td style="min-width: 95px;">First Name:</td>
                            <td>
                                <asp:Label ID="lblViewContactFirstName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Last Name:</td>
                            <td style="text-overflow:ellipsis; overflow:hidden">
                                <asp:Label ID="lblViewContactLastName" runat="server" Style="float:left; max-width:135px;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Company:</td>
                            <td>
                                <asp:Label ID="lblViewCompany" runat="server"></asp:Label></td>
                        </tr>
                         <tr>
                            <td>Email:</td>
                            <td>
                                <asp:Label ID="lblViewContactEmail" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Phone Number:</td>
                            <td>
                                <asp:Label ID="lblViewContactMobilePhone" runat="server"></asp:Label></td>
                        </tr>
                       
                        <tr>
                            <td>Address:</td>
                            <td>
                                <asp:Label ID="lblBillingAddress" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>City/State:</td>
                            <td>
                                <asp:Label ID="lblCityState" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Zip:</td>
                            <td>
                                <asp:Label ID="lblZip" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
               
                
            </div>
                            <div class="col-md-5" style="display:none;">
                                <div class='box'>
                    <h5>Account Information:</h5>
                    <table class='table' style="font-size: 12.5px;">
                        <tr>
                            <td style="min-width: 95px;">My Plan:</td>
                            <td>
                                <asp:Label ID="lblMyPlan" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Pricing:</td>
                            <td style="text-overflow:ellipsis; overflow:hidden">
                                <asp:Label ID="lblPricing" runat="server" Style="float:left; max-width:135px;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Payment Info:</td>
                            <td>
                                <asp:Label ID="lblPaymentInfo" runat="server"></asp:Label></td>
                        </tr>
                         
                        
                    </table>
                </div>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>
                
                
            </div>
         </div>
    <script type="text/javascript">
        $(function () {
            $("#lnkFileUpload").on("click", function () {
                $("#fupPhoto1").trigger("click");
            });
            $("#fupPhoto1").on("change", function () {

                readURL(this);
                if ($(this).val() !== "") {

                }
            });
            $("#lnkLogoFileUpload").on("click", function () {
                $("#fupLogoPhoto1").trigger("click");
            });
            $("#fupLogoPhoto1").on("change", function () {

                readURLLogo(this);
                if ($(this).val() !== "") {

                }
            });
        });
         

      
        function readURL(input) {
            //url(../images/browse.PNG) center 90px no-repeat
            if (input.files && input.files[0]) {
                  var size =input.files[0].size;
                    //alert(size);
                    var maxSize = 1048576;
                if (size > maxSize) {
                    alertDialog("File Size should not exceed 1mb.");
                    return false;
                }
                var reader = new FileReader();

                reader.onload = function (e) {
                   // $("#divFileUpload").css("background", "url(" + e.target.result + ") no-repeat")
                    $('#imgLogo').attr('src', e.target.result);
                   
                }

                reader.readAsDataURL(input.files[0]);
                 $("#btnSaveLogo").trigger("click");
            }
        }
        function readURLLogo(input) {
            //url(../images/browse.PNG) center 90px no-repeat
            if (input.files && input.files[0]) {
                  var size =input.files[0].size;
                    //alert(size);
                    var maxSize = 1048576;
                if (size > maxSize) {
                    alertDialog("File Size should not exceed 1mb.");
                    return false;
                }
                var reader = new FileReader();

                reader.onload = function (e) {
                   // $("#divFileUpload").css("background", "url(" + e.target.result + ") no-repeat")
                    $('#imgEditLogo').attr('src', e.target.result);
                   
                }

                reader.readAsDataURL(input.files[0]);
                 $("#btnSaveEditLogo").trigger("click");
            }
        }
    </script>
    <style type="text/css">
        .profileinfo1 .image {
    width: 75px;
    height: 75px;
    background: #ccc;
    border-radius: 50%;
    margin: 0px auto 10px auto;
    overflow: hidden;
}
        .profileinfo1 {
    margin: 100px 0px 30px 0px;
    text-align: center;
}
    </style>
</asp:Content>
