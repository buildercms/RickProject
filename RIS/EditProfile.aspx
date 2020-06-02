<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditProfile.aspx.vb" Inherits="RIS.EditProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
      <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager>
    <div class="right" id='maincontent'>
        <h1 id="lblPageTitle" runat="server">Edit Profile</h1>
        <%--<form>--%>
        <div class="form-row customerdiv">
            <div class="col-md-12" style="text-align: center;">
                <span style="display: none; color:red" id="lblErrorJS"></span>
                <asp:Label ID="lblCustomerStatus" runat="server" Style="display: none; text-align: center;"></asp:Label>
            </div>
            <fieldset class="col-md-6">
                <div class="form-row">
                   <%-- <div class="form-group col-md-12 title">
                        <p style="font-weight: bold;">Primary Contact</p>
                    </div>--%>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>First Name:</label>
                        <input type="text" class="form-control" id="txtPrimaryFirstName" runat="server" style="outline: none !important;" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Last Name:</label>
                        <input type="text" class="form-control" id="txtPrimaryLastName" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12" style="display:none;">
                        <label><span style="color: red">*</span>Company:</label>
                        <input type="text" class="form-control" id="txtPrimaryCompany" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Mobile Phone:</label>
                        <input type="text" class="form-control" id="txtPrimaryMobilePhone" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Email:</label>
                        <input type="text" class="form-control" id="txtPrimaryEmail" runat="server" maxlength="50" />
                    </div>
                     <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Company:</label>
                        <input type="text" class="form-control" id="txtCompanyName" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Street Address:</label>
                        <input type="text" class="form-control" id="txtPrimaryAddress" runat="server" autocomplete="off" maxlength="500" placeholder="" />
                    </div>

                    <div class="form-group col-md-12" style="margin-bottom:0.1rem !important;">
                        <div class="form-row">
                            <div class="form-group col-md-7">
                                <label><span style="color: red">*</span>City:</label>
                                <input type="text" class="form-control" id="txtPrimaryCity" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-2">
                                <label><span style="color: red">*</span>State:</label>
                                <input type="text" class="form-control" id="txtPrimaryState" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red">*</span>Zip:</label>
                                <input type="text" class="form-control" id="txtPrimaryZip" style="text-transform:uppercase" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-2" style="display: none;">
                                <label><span style="color: red">*</span>Country:</label>
                                <input type="text" class="form-control" id="txtPrimaryCountry" runat="server" maxlength="50" />
                            </div>
                        </div>
                    </div>
                   <%-- <div class="form-group col-md-12" style="display: none;">
                        <label>Category:</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                            <asp:ListItem Text="One" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Two" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>--%>
                  
                   
                    
                   
                </div>
            </fieldset>

            
            <div class="form-group col-md-12">
                        <asp:Button ID="btnSaveProfile" CausesValidation="true" ValidationGroup="Profile" runat="server" Text="Save" CssClass="btn float-left" OnClick="btnSaveProfile_Click"  />  <asp:Button ID="btnCancel" CausesValidation="false"  runat="server" Text="Cancel" CssClass="btn float-right" OnClick="btnCancel_Click"  />

                    </div>
             <asp:RequiredFieldValidator ID="rfvPrimaryFirstName" runat="server" ControlToValidate="txtPrimaryFirstName"
            ErrorMessage="First Name Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvPrimaryLastName" runat="server" ControlToValidate="txtPrimaryLastName"
            ErrorMessage="Last Name Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
              <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
            ErrorMessage="Company Name Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
       
        <asp:RegularExpressionValidator ID="regexPrimaryEmail" runat="server" ErrorMessage='"Email" contains an invalid email address'
            ControlToValidate="txtPrimaryEmail" Display="None" ValidationExpression="\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" ValidationGroup="Profile"></asp:RegularExpressionValidator>
       
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvPrimaryFirstName"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPrimaryLastName"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
      
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="regexPrimaryEmail"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexPrimaryZip" runat="server" ErrorMessage='"Zip Code" in Work Address contains an invalid zip code'
            ControlToValidate="txtPrimaryZip" Display="None" ValidationExpression="(^[\s]*\d{5}(-\d{4})?[\s]*$)|(^[\s]*[A-Za-z][\d][A-Za-z][ ]?[\d][A-Za-z][\d][\s]*$)" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="regexPrimaryZip"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

         <asp:RequiredFieldValidator ID="rfvPrimaryMobilePhone" runat="server" ControlToValidate="txtPrimaryMobilePhone"
            ErrorMessage="Mobile Phone Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="rfvPrimaryMobilePhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
             <asp:RequiredFieldValidator ID="rfvPrimaryEmail" runat="server" ControlToValidate="txtPrimaryEmail"
            ErrorMessage="Email Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvPrimaryEmail"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <asp:RegularExpressionValidator ID="regexPrimaryMobilePhone" runat="server" ErrorMessage='"Mobile Phone" must be in the form: ###-###-#### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Home Zip Code to enable International validation.'
            ControlToValidate="txtPrimaryMobilePhone" Display="None" ValidationExpression="\s*\(?\d{3}\)?.?\d{3}.?\d{4}\s*([xX]?\d{1,5})?" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="regexPrimaryMobilePhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
             <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtPrimaryAddress"
                ErrorMessage="Street Address Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtPrimaryCity"
                ErrorMessage="City Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtPrimaryState"
                ErrorMessage="State Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtPrimaryZip"
                ErrorMessage="Zip Required" Display="None" ValidationGroup="Profile" SetFocusOnError="True"></asp:RequiredFieldValidator>
              <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvStreet"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvCity"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvState"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvZip"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
              <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvCompanyName"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>

      
        </div>
        <%--</form>--%>
    </div>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDzhaPno1XDsbloeoJyzs33ueGQtbx9XiA&libraries=places"></script>
    <script type="text/javascript">
        var google_address;
        initializegoogleaddress();
        function initializegoogleaddress() {
            try {
                google_address = new google.maps.places.Autocomplete(
              /** @type {HTMLInputElement} */(document.getElementById('txtPrimaryAddress')),
                    { types: ['geocode'] });
                google.maps.event.addListener(google_address, 'place_changed', function () {
                    fillInAddress(google_address);
                });
            }
            catch (err) {
            }
        }

        function fillInAddress(selected_address) {
            var place = selected_address.getPlace();
            fillInStreetAddress(place);
            return false;
        }

        function fillInStreetAddress(place) {
            var componentForm = {
                street_number: 'short_name',
                route: 'long_name',
                locality: 'long_name',
                administrative_area_level_1: 'short_name',
                country: 'long_name',
                postal_code: 'short_name'
            };

            var address = "";
            var zipCode = "";

            for (var i = 0; i < place.address_components.length; i++) {
                var addressType = place.address_components[i].types[0];
                if (componentForm[addressType]) {
                    var val = place.address_components[i][componentForm[addressType]];
                    if (addressType == "street_number" || addressType == "route") {
                        address = address + val + " ";
                    }
                    if (addressType == "postal_code") {
                        zipCode = val;
                    }

                }
            }

            document.getElementById('txtPrimaryAddress').value = place.name;
            if (zipCode != "") {
                document.getElementById('txtPrimaryZip').value = zipCode;
                $.ajax({
                    type: "POST",
                    url: "EditProfile.aspx/LoadZip",
                    data: "{'zipCode':'" + zipCode.toUpperCase() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d.split("~");
                        var city = result[0];
                        var state = result[1];
                        var countryCode = result[2];
                        if (city == "" && state == "" && zipCode != "") {
                            alert("hi1");
                            $("#txtPrimaryZip").val("").focus();
                            alertDialog(zipCode + " is not a valid US or Canadian zip code.\nPlease try again.");
                            return false;
                        }
                        document.getElementById('txtPrimaryCity').value = city;
                        document.getElementById('txtPrimaryState').value = state;
                        document.getElementById('txtPrimaryCountry').value = countryCode;
                        if (countryCode.length == 2) {
                            //document.getElementById('hidCountry').value = countryCode;
                        }
                        //focussedControl = 'txtWorkStreet';
                        //$("#" + focussedControl).focus();
                    },
                    error: function (data) {
                        //alertDialog(data.msg);
                    }
                });
            }
        }

        var focussedControl = "";
        function handle(e) {
            if (e.keyCode === 13) {
                e.preventDefault();
            }
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $("#txtPrimaryAddress,#txtPrimaryZip").on("blur", function () {
                // alertDialog("hi");
                setTimeout(function () {
                    var zipCode = document.getElementById('txtPrimaryZip').value;
                    if (zipCode != "") {
                        // document.getElementById('txtZip').value = zipCode;
                        if (zipCode == '99999') {
                             document.getElementById('txtPrimaryCity').value = "";
                        document.getElementById('txtPrimaryState').value = "";
                        document.getElementById('txtPrimaryCountry').value = "";
                            TogglePhoneValidation(false);
                            $('#txtPrimaryWorkPhone,#txtPrimaryMobilePhone,#txtSecondaryPhone,#txtSecondaryMobilePhone').removeClass('validatorCalloutHighlight');
                        }
                        else {
                           $.ajax({
                    type: "POST",
                    url: "EditProfile.aspx/LoadZip",
                    data: "{'zipCode':'" + zipCode + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var result = msg.d.split("~");
                        var city = result[0];
                        var state = result[1];
                        var countryCode = result[2];
                        if (city == "" && state == "" && zipCode != "") {
                            
                            $("#txtPrimaryZip").val("").focus();
                            alertDialog(zipCode + " is not a valid US or Canadian zip code.\nPlease try again.");
                            return false;
                        }
                        document.getElementById('txtPrimaryCity').value = city;
                        document.getElementById('txtPrimaryState').value = state;
                        document.getElementById('txtPrimaryCountry').value = countryCode;
                        if (countryCode.length == 2) {
                            //document.getElementById('hidCountry').value = countryCode;
                        }
                        //focussedControl = 'txtWorkStreet';
                        //$("#" + focussedControl).focus();
                    },
                    error: function (data) {
                        //alertDialog(data.msg);
                    }
                });
                            TogglePhoneValidation(true);
                        }
                   
                    }
                }, 2000);

            })
            $("#btnUSPSCancel").on("click", function () {
                jqdialogHideControl();
                return false;
            })
            $("#btnUSPSUpdate").on("click", function () {
                jqdialogHideControl();
                $("#txtPrimaryAddress").val($("#lblUSPSStreetAddr").text());
                return false;
            })
        })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#jqdialog").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'USPS Address Verification',
                width: '700px',
                position: 'center',
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) { $(this).parent().appendTo("form"); }
            });

        });

        function jqdialogShowControl() {
            $("#jqdialog").dialog("open");
            return false;
        }
        function jqdialogHideControl() {
            $("#jqdialog").dialog("close");
            return false;
        }
        function TogglePhoneValidation(isValid) {
           // ValidatorEnable($("[id$='regexPrimaryWorkPhone']")[0], isValid);
            ValidatorEnable($("[id$='regexPrimaryMobilePhone']")[0], isValid);
           // ValidatorEnable($("[id$='regexSecondaryWorkPhone']")[0], isValid);
          //  ValidatorEnable($("[id$='regexSecondaryMobilePhone']")[0], isValid);
            // alertDialog($("[id$='regexHomePhone']")[0].ValidationGrroup);
            //txtPrimaryWorkPhone,#txtPrimaryMobilePhone,#txtSecondaryPhone,#txtSecondaryMobilePhone
             
           // CheckValidation('txtPrimaryWorkPhone');
            CheckValidation('txtPrimaryMobilePhone');
            //CheckValidation('txtSecondaryPhone');
           // CheckValidation('txtSecondaryMobilePhone');
            //alertDialog(isValid);
        }
        function CheckValidation(txtId)
            {
               $("#lblErrorJS").css("display","none");
                // TogglePhoneValidation(true);
                //if(txtId=="txtHomePhone")
                //{
                if($("#txtPrimaryZip").val()=="99999" )
                {
                    // alertDialog("hi");
                    $("#"+txtId).removeClass("validatorCalloutHighlight");
                    //TogglePhoneValidation(false);
                }
              
                   
              
               
                    //var homePhoneClass=$("#txtHomePhone").attr("class");
                    //var workPhoneClass=$("#txtWorkPhone").attr("class");
                    //var cellPhoneClass=$("#txtCellPhone").attr("class");
                else{
                   
                    var className=$("#"+txtId).attr("class");
                    //var errorMsg="";
                    //if(homePhoneClass.indexOf("validatorCalloutHighlight")>-1)
                    //{

                    //}
                    if(className.indexOf("validatorCalloutHighlight")>-1)
                    {
                       
                        $("#lblErrorJS").css("display","inline");
                        $("#lblErrorJS").text('"Work Phone" must be in the form: ###-###-### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Primary Zip Code to enable International validation.');

                    }
                    else
                    {
                        // $("#lblErrorJS").css("display","none");
                    }
                }
                //alertDialog($("#"+txtId).attr("class"));
            }
    </script>
    <style type="text/css">
        .validatorCalloutHighlight {
            background-color: pink !important;
            border-color: Red !important;
            border-width: 2px !important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable, #ValidatorCalloutExtender3_popupTable, #ValidatorCalloutExtender4_popupTable, #ValidatorCalloutExtender5_popupTable,
        #ValidatorCalloutExtender6_popupTable, #ValidatorCalloutExtender7_popupTable, #ValidatorCalloutExtender8_popupTable, #ValidatorCalloutExtender9_popupTable, #ValidatorCalloutExtender10_popupTable,
        #ValidatorCalloutExtender11_popupTable,#ValidatorCalloutExtender12_popupTable {
            visibility: hidden !important;
        }
        .customerdiv label {
            margin-bottom: 0px !important;
        }

        textarea:focus, input:focus {
            outline: none;
        }

        .form-control:focus {
            border-color: inherit;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    </style>
</asp:Content>
