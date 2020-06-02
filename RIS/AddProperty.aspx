<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddProperty.aspx.vb" Inherits="RIS.AddProperty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%= ResolveUrl("~/js/HideShow.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        function TogglePropertyName(ddlProperty, txtProperty) {
            if ($("#" + ddlProperty).val() == "-1") {
                $("#" + txtProperty).css("display", "");
                $("#" + txtProperty).focus();
                $("#" + ddlProperty).css("display", "none");
                return false;
            }
            else if ($("#" + ddlProperty).val() == "") {
                return false;
            }
            else {
                __doPostBack("'" + ddlProperty + "'", '');
              //  $("#" + txtProperty).val($("#" + dddlProperty).find("option:selected").text());
                return true
            }
        }
        function ValidateProperty() {
           // alert($("#ddlProperties").css("display"));
            if ($("#ddlProperties").css("display") == "none"){
                $("#hidDisplayMode").val("1");
            } else
                $("#hidDisplayMode").val("0");
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hidCustomerId" runat="server" />
    <asp:HiddenField ID="hidPropertyId" runat="server" />
    <asp:HiddenField ID="hidSelectedPropertyId" runat="server" />
    <asp:HiddenField ID="hidPropertyImage" runat="server" />
    <asp:HiddenField ID="hidValueMapProperty" runat="server" />
    <asp:HiddenField ID="hidValueMapPropertyContentImpacts1" runat="server" />
    <asp:HiddenField ID="hidValueMapPropertyContentImpacts2" runat="server" />
    <asp:HiddenField ID="hidValueMapPropertyContentImpacts3" runat="server" />

    <asp:HiddenField ID="hidValueMapPropertyContentBenefits1" runat="server" />
    <asp:HiddenField ID="hidValueMapPropertyContentBenefits2" runat="server" />
    <asp:HiddenField ID="hidValueMapPropertyContentBenefits3" runat="server" />
    <asp:HiddenField ID="hidAssignProperty" runat="server" Value="0" />
    <asp:HiddenField ID="hidCropFile" runat="server" />
    <asp:HiddenField ID="hidDisplayMode" runat="server" />

     <input type="hidden" id="X" />
     <input type="hidden" id="Y" />
     <input type="hidden" id="W" />
     <input type="hidden" id="H" />
     <div id="divhide" class="page_dimmer" style="display: none;"></div>
    <div class="right" id='maincontent'>
        <ul class='arrow-steps clearfix font-weight-bold'>
            <li class='step '>
                <span><a id="aOverView" runat="server" href='overview.aspx'>Overview</a></span>
            </li>
            <li class='step '>
                <a id="aCreateValueMap" runat="server" href='valuemap.html'><span><span class='first'>Step 1</span>
                    <span>View/Create<br />
                        Value Map</span></span></a>
            </li>
            <li class='step active'>
                <a id="aCreateProperty" href='#' runat="server"><span><span class='first'>Step 2</span>
                    <span>Add/Evaluate<br />
                        Properties</span></span></a>
            </li>
            <li class='step'>
                <a id="aCompareDecide" runat="server" href='#'><span><span class='first'>Step 3 </span>
                    <span>Discuss and<br />
                        Decide</span></span></a>
            </li>
        </ul>
        <div class='row' style="margin-top: -35px;">
            <div class='col-md-7'>
                <h2><span id="lblCustomerName" runat="server"></span></h2>
            </div>
            <div class='col-md-5'>
                <div class='clearfix'>

                    <asp:Button CssClass="btn float-right" ID="btnAddProperty" runat="server" Text="Add Property" OnClick="btnAddProperty_Click" Style="margin-right:-5px;" />
                </div>


            </div>
        </div>
        <div class="row">
            <div class="col-md-12 font-weight-bold">
                <asp:CheckBox ID="chkIncludeArchieve" runat="server" AutoPostBack="true" Text="Include Archived" OnCheckedChanged="chkIncludeArchieve_CheckedChanged" />
            </div>
        </div>
        <div class="row" id="divViewProperty" runat="server">
        </div>
        <div class="row" id="divAddProperty" runat="server">
            <div class="col-md-12" style="text-align: center;">
                <asp:Label ID="lblPropertyStatus" runat="server" Style="display: none; text-align: center;"></asp:Label>
            </div>
            <div class="form-group col-md-12 title">
                <h5 id="lblPageTitle" runat="server">Add Property</h5>
            </div>
            <div class="form-group col-md-12" id="divPropertyError" runat="server" visible="false">
                        <label><span style="color: red">Property Name already exists for this user, please enter different property name.</span></label>
                        </div>
            <fieldset class="col-md-6">
                <div class="form-row">
                    
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Property Nickname:</label>
                        <asp:DropDownList ID="ddlProperties" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProperties_SelectedIndexChanged"></asp:DropDownList>
                        <input type="text" class="form-control" id="txtPropertyName" runat="server" maxlength="100" style="display:none;" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Price:</label>
                        <input type="text" class="form-control" id="txtPrice" runat="server"  onkeyup="if((this.value.length>0)&&(this.value!='$')&&(this.value!=',')&&(!(IsCommaKey()))&&(!(IsHorizArrowKey()))&&(!(IsBackSpaceKey()))){this.value=FormatCurrencyNoCents(this.value)}"
                            onblur="this.value=FormatCurrency(this.value);" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Square Feet:</label>
                        <input type="text" class="form-control" id="txtSquareFeet" runat="server" maxlength="10" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Bedrooms:</label>
                        <input type="text" class="form-control" id="txtBedRooms" runat="server" maxlength="15" onkeypress="return isAlphaNumeric(event)" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Baths:</label>
                        <input type="text" class="form-control" id="txtBaths" runat="server" maxlength="15" onkeypress="return isAlphaNumeric(event)" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Street Address:</label>
                        <input type="text" class="form-control" id="txtPrimaryAddress" runat="server" autocomplete="off" maxlength="100" placeholder="" />
                    </div>

                    <div class="form-group col-md-12" style="margin-bottom: 0.2rem !important;">
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
                                <input type="text" class="form-control" id="txtPrimaryZip" style="text-transform: uppercase" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-2" style="display: none;">
                                <label>Country</label>
                                <input type="text" class="form-control" id="txtPrimaryCountry" runat="server" maxlength="50" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-md-12" style="margin-bottom: 0.2rem !important;">

                        <asp:CheckBox ID="chkArchieve" runat="server" Text="Archive" TextAlign="Right" />
                    </div>

                </div>
            </fieldset>

            <fieldset class="col-md-6">
                <div class="form-row">

                    <div class="form-group col-md-12 notes" style="height: 300px">
                        <label>Add Photo:</label>
                        <asp:FileUpload ID="fupPhoto1" runat="server" Style="display: none;" />
                        <div class="browse col-md-12 mb-3" id="divFileUpload" style="cursor: pointer; width: 292px; padding: 85px; background: url(images/browse.PNG) center 55px no-repeat;">
                            <div class="centered">Upload Image</div>
                        </div>
                        <%--<textarea class="form-control pb-3" rows="15" id="txtDescription" runat="server"></textarea>--%>
                    </div>
                    <div class="form-group col-md-12 notes" style="display: none;">
                        <label>Add Photo:</label>
                        <asp:FileUpload ID="fupPhoto2" runat="server" CssClass="browse" />
                        <%--<textarea class="form-control pb-3" rows="15" id="Textarea1" runat="server"></textarea>--%>
                    </div>

                </div>
            </fieldset>

            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice"
                ErrorMessage="Price Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvPropertyName" runat="server" ControlToValidate="txtPropertyName"
                ErrorMessage="Property Name Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvddlProperties" runat="server" ControlToValidate="ddlProperties"
                ErrorMessage="Property Name Required" Display="None" ValidationGroup="Property" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <%--<asp:RequiredFieldValidator ID="rfvSquareFeet" runat="server" ControlToValidate="txtSquareFeet"
                ErrorMessage="Sq ft. Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvBedRooms" runat="server" ControlToValidate="txtBedRooms"
                ErrorMessage="Bed Rooms Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvBaths" runat="server" ControlToValidate="txtBaths"
                ErrorMessage="Baths Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtPrimaryAddress"
                ErrorMessage="Street Address Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtPrimaryCity"
                ErrorMessage="City Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtPrimaryState"
                ErrorMessage="State Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtPrimaryZip"
                ErrorMessage="Zip Required" Display="None" ValidationGroup="Property" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPrice"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvPropertyName"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <%--<ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvSquareFeet"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvBedRooms"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvBaths"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>--%>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvStreet"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvCity"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="rfvState"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvZip"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <asp:RegularExpressionValidator ID="regexPrimaryZip" runat="server" ErrorMessage='"Zip Code" in Work Address contains an invalid zip code'
                ControlToValidate="txtPrimaryZip" Display="None" ValidationExpression="(^[\s]*\d{5}(-\d{4})?[\s]*$)|(^[\s]*[A-Za-z][\d][A-Za-z][ ]?[\d][A-Za-z][\d][\s]*$)" ValidationGroup="Contact"></asp:RegularExpressionValidator>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="regexPrimaryZip"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
            <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvddlProperties"
                HighlightCssClass="validatorCalloutHighlight">
            </ajax:ValidatorCalloutExtender>
        </div>
        <div class="row" id="divPropertyActions" runat="server">
            <div class="col-md-6">
                <asp:Button ID="btnSaveProperty" CausesValidation="true" Width="115px" ValidationGroup="Property" runat="server" Text="Save" CssClass="btn  " OnClick="btnSaveProperty_Click" />
            </div>
            <div class="col-md-6">
                <asp:Button ID="btnCancel" CausesValidation="false" Width="115px" runat="server" Text="Cancel" CssClass="btn float-right" OnClick="btnCancel_Click" />
            </div>
        </div>



    </div>

    <div class="modal-panel" id="jqdialog" style="display: none; background-color: #ccddef; border: 2px solid #999999 !important; padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <div style="width: 100%; text-align: center;">
                    <asp:Label ID="lblUSPSMessageError" runat="server" Style="margin: 0px; padding: 0px; width: 100%; color: red;" Text="" Visible="true"></asp:Label>
                </div>
                <table style="width: 100%;" id="tblHomeAddress" runat="server">
                    <tr>
                        <td align="right" style="width: 25%;"><span style="font-weight: bold; color: black;">CURRENT STREET ADDRESS: </span></td>
                        <td align="left" style="width: 25%;">
                            <asp:Label ID="lblUSPSCurrentAddress" runat="server" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 25%;"><span style="font-weight: bold; color: black;">USPS SUGGESTS: </span>
                        </td>
                        <td align="left" style="width: 25%;">
                            <asp:Label ID="lblUSPSStreetAddr" runat="server" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; text-align: center;" id="tblSuggest" runat="server">
                    <tr>
                        <td colspan="2" align="center"><span style="font-weight: bold; color: black;">Would you like to update your entry to the USPS suggestion?</span>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; text-align: center;">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnUSPSUpdate" runat="server" Text="Yes"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="btnUSPSCancel" runat="server" Text="No"></asp:Button>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: none!important;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <%--<div class="col-md-4"></div>--%>
                        <div class="col-md-12 text-center" style="margin-bottom: 40px;">
                            <input type="hidden" id="hidSelectPriorityId" value="" />
                            <span id="lblPriorityTitle" class="font-weight-bold" style="font-size: 20px;">Priority 1 of 3: Area/Location</span>
                        </div>
                        <%--<div class="col-md-2"></div>--%>
                        <div class="col-md-12 mx-auto" style="margin-bottom:30px; display: flex;
  justify-content: center;">
                           <table id="tblImpacts" border="0" class="items" cellspacing="20px" style="font-size:13px!important;align-self: center;">

                            </table>
                        </div>
                    <%-- <div class="col-6">
                            <ul id="ulBenefits" class="items text-left">
                                <li>Stay More Organized
                                </li>
                            </ul>
                        </div>--%>
                    <%-- <div class="col-md-3"></div>--%>
                    <div class="col-md-12 text-center">
                        <p class="font-weight-bold" style="font-size: 20px;">How well do you feel that you can accomplish these things?</p>
                    </div>
                    <div class="col-md-1"></div>
                    <div class="col-md-10" style="margin-bottom: 30px;">
                        <input type="hidden" class="slider-input" value="" />
                    </div>
                    <div class="col-md-1"></div>


                    </div>
                    <br />
                    <br />
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 float-left">
                        <label class="font-weight-bold">Notes:</label>
                        <textarea id="txtNotes" rows="5" cols="85" style="width: 650px"></textarea>
                    </div>
                </div>
                <div class="modal-footer" style="border-top: none;">
                    <div class='col-md-6'>
                        <asp:Button ID="btnPrevious" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                    </div>
                    <div class='col-md-6'>
                        <asp:Button ID="btnNext" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                    </div>
                </div>

            </div>

        </div>
    </div>
    <div id="myModalCrop" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: none!important;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">

                        
                    <img id="imgCrop" src="#" />
                        </div>

                    </div>
                   
                </div>
                 <div class="modal-footer" style="border-top: none;">
                    <div class='col-md-6'>
                        <asp:Button ID="btnCropImage" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Save" Enabled="true" />
                    </div>
                    <%--<div class='col-md-6'>
                        <asp:Button ID="Button2" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                    </div>--%>
                </div>

            </div>

        </div>
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
                    url: "AddContact.aspx/LoadZip",
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
            $("#txtPrimaryAddress,#txtPrimayZip").on("blur", function () {
                // alertDialog("hi");
                setTimeout(function () {
                    var zipCode = document.getElementById('txtPrimaryZip').value;
                    if (zipCode != "") {
                        // document.getElementById('txtZip').value = zipCode;
                        $.ajax({
                            type: "POST",
                            url: "AddContact.aspx/ValidateUSPS",
                            data: "{'zip':'" + zipCode + "','workAddress':'" + $("#txtPrimaryAddress").val() + "','state':'" + $("#txtPrimaryState").val() + "','city':'" + $("#txtPrimaryCity").val() + "','countryCode':''}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                //   alertDialog(msg.d);
                                var result = msg.d.split("~");
                                // alertDialog(msg.d);
                                // alertDialog(result[0])
                                if (result[0] == "invalid") {
                                    jqdialogHideControl();
                                    $("#btnUSPSCancel").val("No");
                                    $("#tblSuggest,#tblHomeAddress").css("display", "");
                                    $("#btnUSPSUpdate").css("display", "")
                                    $("#lblUSPSCurrentAddress").text($("#txtPrimaryAddress").val());
                                    $("#lblUSPSStreetAddr").text(result[1]);
                                    $("#lblUSPSMessageError").css("display", "none");
                                }
                                else if (result[0] == "false") {
                                    jqdialogHideControl();
                                    $("#btnUSPSCancel").val("Close");
                                    $("#btnUSPSUpdate").css("display", "none");
                                    $("#tblSuggest,#tblHomeAddress").css("display", "none");
                                    // $("#lblUSPSCurrentAddress").text($("#txtStreet").val());
                                    // $("#lblUSPSStreetAddr").text(result(1));
                                    $("#lblUSPSMessageError").css("display", "").html(result[1]);

                                }

                            },
                            error: function (data) {
                                alertDialog(data.msg);
                            }
                        });
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
            $("#btnAddProperty").click(function () {
                window.location.href = "AddProperty.aspx?cid=" + $("#hidCustomerId").val() + "&type=new";
                return false;
            })
        });

        function jqdialogShowControl() {
            $("#jqdialog").dialog("open");
            return false;
        }
        function jqdialogHideControl() {
            $("#jqdialog").dialog("close");
            return false;
        }
    </script>
    <script type="text/javascript">
        String.prototype.replaceAll = function (search, replacement) {
            var target = this;
            return target.replace(new RegExp(search, 'g'), replacement);
        };
        $(function () {
            $("#txtSquareFeet").bind("blur keyup", function () {
                // alertDialog("hi");
                if ($(this).val() != "") {
                    var number = parseInt($(this).val().replaceAll(",", ""));
                    number = number.toLocaleString("en-US");
                    $(this).val(number);
                }
            });
            //$("#txtPrice,#txtSquareFeet").bind("focus", function () {
            //    // alertDialog("hi");
            //    var number = $(this).val();
            //    number = number.replaceAll(",", "");
            //    //alertDialog(number);
            //    $(this).val(number);
            //});
            //$("#txtPrice,#txtSquareFeet").trigger("blur");
        })
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function isAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            //alertDialog(keyCode);
            if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <=
                122) || (keyCode == 32) || (keyCode == 46)) {
                return true;
            }
            return false;

        }
        $(function () {
            $("#divViewProperty").find(".card-link").on("click", function () {
                setTimeout(function () {
                    var expanded = $(this).attr("aria-expanded");

                    if (expanded == "true" || expanded == undefined) {
                        // alertDialog(expanded);
                        $(this).addClass("collapsed")

                    }
                    else {
                        $(this).removeClass("collapsed");
                    }
                }, 1000)

            })
            $(".form-control").bind("keydown", function (event) {
                //   alertDialog(event.keyCode);
                if (event.keyCode == 13) {
                    $("#btnSaveProperty").trigger("click");
                    return false;
                }
            })
        })
    </script>
    <style type="text/css">
        #home {
            background-color: #cecece;
        }

        .form-control:focus {
            border-color: inherit;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    </style>
    <style type="text/css">
        .validatorCalloutHighlight {
            background-color: pink !important;
            border-color: Red !important;
            border-width: 2px !important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable, #ValidatorCalloutExtender3_popupTable, #ValidatorCalloutExtender4_popupTable, #ValidatorCalloutExtender5_popupTable, #ValidatorCalloutExtender6_popupTable, #ValidatorCalloutExtender7_popupTable, #ValidatorCalloutExtender8_popupTable, #ValidatorCalloutExtender9_popupTable, #ValidatorCalloutExtender10_popupTable {
            visibility: hidden !important;
        }

        #divAddProperty label {
            margin-bottom: 0px !important;
        }
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

    <script src="js/jquery.range.js"></script>
    <link href="css/jquery.range.css" rel="stylesheet" />
      <script src="js/jquery.Jcrop.js" type="text/javascript"></script>
     <script src="ajax-loading.js"></script>
    <link href="css/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('.slider-input').jRange({
                from: 1,
                to: 10,
                step: 1,
                scale: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
                format: '%s',
                width: 550,
                showLabels: false,
                snap: true,

            });

            $('.slider-input').jRange('setValue', "0");
            //$(".slider-input").change(function () {

            //    var rangesValues = $(".slider-input").val();

            //    alertDialog(rangesValues)

            //});
        });
        var propertyScoreIndex = 1;
        var priorityContent = $("#hidValueMapProperty").val().split("~");
        $(function () {
            $(".btnModal").on("click", function () {
                var btnClass = $(this).attr("class");
                if (btnClass.indexOf("disabled") > -1)
                    return false;
                propertyScoreIndex = 1;
                var propertyId = $(this).attr("propertyid");
                $("#hidSelectedPropertyId").val(propertyId);
                BindPropertyDialogContent();
            })
            $("#btnPrevious").click(function () {
                propertyScoreIndex = propertyScoreIndex - 1;
                BindPropertyDialogContent();
                return false;
            })
            $("#btnNext").click(function () {
                propertyScoreIndex = propertyScoreIndex + 1;

                InsertPropertyScore();
                if (propertyScoreIndex > 3) {
                    var url = window.location.href;
                    window.location.href = url;
                    return false;
                }
                $('.slider-input').jRange('setValue', "0");
                $("#txtNotes").val("");
                BindPropertyDialogContent();
                return false;
            })
        });
        function BindPropertyDialogContent() {
            $("#hidSelectPriorityId").val(priorityContent[propertyScoreIndex - 1].split("!")[0]);
            $("#lblPriorityTitle").html("Priority" + " " + propertyScoreIndex + " of 3: " + decodeURIComponent(priorityContent[propertyScoreIndex - 1].split("!")[1]));
            var priortyImpactContent = $("#hidValueMapPropertyContentImpacts" + propertyScoreIndex).val();
            var priortyBenefitContent = $("#hidValueMapPropertyContentBenefits" + propertyScoreIndex).val();
            var contentArray = new Array();
            if (priortyImpactContent !== "") {
                var priortyImpactContentHtml = "";
                var priortyImpactContentSplit = priortyImpactContent.split("~");
                for (i = 0; i < priortyImpactContentSplit.length; i++) {
                    //priortyImpactContentHtml = priortyImpactContentHtml + "<li style='width:100%;'>" + decodeURIComponent(priortyImpactContentSplit[i]) + "</li>";
                    contentArray.push(decodeURIComponent(priortyImpactContentSplit[i]));
                }
                //$("#ulImpacts").html(priortyImpactContentHtml);
            }
            if (priortyBenefitContent !== "") {
                var priortyBenefitContentHtml = "";
                var priortyBenefitContentSplit = priortyBenefitContent.split("~");
                for (i = 0; i < priortyBenefitContentSplit.length; i++) {
                    //priortyBenefitContentHtml = priortyBenefitContentHtml + "<li style='width:100%;'>" + decodeURIComponent(priortyBenefitContentSplit[i]) + "</li>";
                    contentArray.push(decodeURIComponent(priortyBenefitContentSplit[i]));
                }
                //$("#ulImpacts").append(priortyBenefitContentHtml);
            }
            var priortyImpactContentHtml = "";
            for (i = 0; i < contentArray.length; i++) {
                    
                    if (priortyImpactContentSplit[i] != "") {
                        if (i % 2 == 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "<tr><td style=''><img src='images/item.png' style='margin-top:-2px'/>" + contentArray[i] + "</td>";
                        }
                        else {
                             priortyImpactContentHtml = priortyImpactContentHtml + "<td style=''><img src='images/item.png' style='margin-top:-2px;margin-left:30px'/>" + contentArray[i] + "</td>";
                        }
                        if (i % 2 != 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "</tr>";
                        }
                        //answerCount += 1;
                    }
                }
                $("#tblImpacts").html(priortyImpactContentHtml);
            if (propertyScoreIndex == 1) {
                $("#btnPrevious").attr("disabled", "disabled");
            }
            else {
                $("#btnPrevious").removeAttr("disabled");
            }
            $.ajax({
                type: "POST",
                url: "AddProperty.aspx/GetPropertyValueMapScore",
                data: "{'customerID':'" + $("#hidCustomerId").val() + "','propertyID':'" + $("#hidSelectedPropertyId").val() + "','priorityID':'" + $("#hidSelectPriorityId").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d.split("~");
                    var score = result[0];
                    var note = decodeURIComponent(result[1]);

                    $(".slider-input").val("0");
                    if (score == "" || score == "null" || score == null || score == "undefined" || score == undefined) {
                        $('.slider-input').jRange('setValue', "0");
                    }
                    else {

                        // $(".slider-input").val(score.toLocaleString());
                        $('.slider-input').jRange('setValue', score);
                        // $(".slider-input").setValue(score);
                    }
                    if (note == "" || note == "null" || note == null || note == "undefined" || note == undefined) {
                        $("#txtNotes").val("");
                    }
                    else {
                        //$("#txtNotes").val(escape(decodeURI(note)));
                        $("#txtNotes").val(note);
                    }
                },
                error: function (data) {
                    //alertDialog(data.msg);
                }
            });


        }
        function InsertPropertyScore() {
            $.ajax({
                type: "POST",
                url: "AddProperty.aspx/InsertPropertyValueMapScore",
                data: "{'customerID':'" + $("#hidCustomerId").val() + "','propertyID':'" + $("#hidSelectedPropertyId").val() + "','priorityID':'" + $("#hidSelectPriorityId").val() + "','score':'" + $(".slider-input").val() + "','notes':'" + encodeURI($("#txtNotes").val()) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d.split("~");
                    var score = result[0];
                    var note = decodeURIComponent(result[1]);
                    $('.slider-input').jRange('setValue', "0");
                    if (score == "" || score == "null" || score == null || score == "undefined" || score == undefined) {
                        $('.slider-input').jRange('setValue', "0");
                    }
                    else {
                        $('.slider-input').jRange('setValue', score);
                    }
                    if (note == "" || note == "null" || note == null || note == "undefined" || note == undefined) {
                        $("#txtNotes").val("");
                    }
                    else {
                        $("#txtNotes").val(decodeURIComponent(note));
                    }
                },
                error: function (data) {
                    //alertDialog(data.msg);
                }
            });
        }

    </script>
    <script type="text/javascript">
                var api;
        
        function setCrop()
    {
     api=   $('#imgCrop').Jcrop({
            onSelect: updateCoords,
            onChange: updateCoords,
            setSelect: [0, 0, 292, 172],

            minSize: [292, 172],
            maxSize: [292, 172]
        });
        $('#X').val("0");
        $('#Y').val("0");
        $('#W').val("292");
        $('#H').val("172");
    }

        function fnCheckforValidFormats() {
            var file = document.getElementById("<%= fupPhoto1.ClientID %>").value;
            if (file == "") {
                alertify.alert(selectfiletoupload);
            }
            if (file != '') {
                var exts = ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'tif', 'tiff'];
                // first check if file field has any value   
                if (file) {
                    // split file name at dot  
                    var get_ext = file.split('.');
                    // reverse name to check extension 
                    get_ext = get_ext.reverse();
                    // check file type is valid as given in 'exts' array      
                    if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
                        // checkCtrls(GetClientId('divDrop'));

                    } else {
                        // alert('Invalid file!');
                        alertify.alert(selectvalidthumbnail);
                        return false;
                    }
                }
            }
            else {
                //alert('Please select a file to upload!');
                return false;
            }
            return true;
        }
        var imgindex = 0;

     
        function updateCoords(c) {
            $('#X').val(c.x);
            $('#Y').val(c.y);
            $('#W').val(c.w);
            $('#H').val(c.h);
        };


      
                 
    </script>
    <script type="text/javascript">
          var loading = $.loading();
        $(function () {
            $("#divFileUpload").on("click", function () {
                $("#fupPhoto1").trigger("click");
            });
            $("#fupPhoto1").on("change", function () {
                readURL(this);
            });

        })
        function readURL(input) {
            //url(../images/browse.PNG) center 90px no-repeat
            if (input.files && input.files[0]) {
                var size = input.files[0].size;
                //alert(size);
                var maxSize = 8388608;
                if (size > maxSize) {
                    alertDialog("File Size should not exceed 8mb.");
                    return false;
                }
                var reader = new FileReader();
                reader.readAsDataURL(input.files[0]);
                reader.onload = function (e) {

                    var data = new FormData();

                    


                    // $.ajax({  
                    //     url: "FileUploadHandler.ashx",  
                    //     type: "POST",  
                    //     data: data,  
                    //     contentType: false,  
                    //     processData: false,  
                    //     beforeSend: function () {
                    //         $("#divhide").show();
                    //   loading.open();
                    //},
                    //     success: function (result) {
                    //         loading.close(); $("#divhide").hide(); 
                    //          var width = 292;
                    //         var height = 172;
                    //         JcropAPI = $('#imgCrop').data('Jcrop');
                    //         try {
                    //             JcropAPI.release();
                    //         }
                    //         catch (ex) { }
                    //         $("#myModalCrop").modal("show");
                    //         var fileName = result.split("~")[0];
                    //         var imgWidth = result.split("~")[1];
                    //         var imgHeight = result.split("~")[2];
                    //         //alert(imgWidth);
                    //        // alert(imgHeight);
                    //         $("#imgCrop").attr("src", fileName);
                    //       //  $("#myModalCrop").css("overflow-x", "auto !important");
                    //        // $(".modal-content").css("width",imgWidth.replace("px","") +"px")
                    //         if (imgWidth > 600) {
                    //             // $("#imgCrop").css("width","600px")
                    //             $("#myModalCrop").css("overflow-x", "auto !important");
                    //             $(".modal-content").css("width", imgWidth.replace("px", "") + "px");
                    //         }
                    //         // if (imgHeight > 500) {
                    //         //     $("#imgCrop").css("height","500px")
                    //         //}
                    //         //$("#imgCrop").css("max-width", "500px !important").css("max-height", "500px !important");
                    //          $('#imgCrop').Jcrop({
                    //                     onSelect: updateCoords,
                    //                     onChange: updateCoords,

                    //                     setSelect: [0, 0, width, height],
                    //                     minSize: [width, height],
                    //              maxSize: [width, height]
                    //                     });
                    //     },  
                    //     error: function (err) {  
                    //         alertdialog(err.statusText);
                    //         loading.close();
                    //          $("#divhide").hide();
                    //     }  
                    // });



                    $("#divFileUpload").css("background", "url(" + e.target.result + ") no-repeat")
                    // $('#blah').attr('src', e.target.result);
                    $(".centered").css("display", "none")
                }


            }
        }
        $(function () {
            var imagePath = $("#hidPropertyImage").val();
            //alertDialog(imagePath);
            if (imagePath != "") {
                imagePath = "CustomFiles/Property/" + <%= userId %> + "/" + $("#hidPropertyId").val() + "/" + imagePath;
                // alertDialog(imagePath);
                // $("#fupPhoto1").trigger("change");
                // $("#divFileUpload").css("background", "none");
                setTimeout(function () {
                    $("#divFileUpload").css("background-image", "url(" + imagePath + ") ");
                    $("#divFileUpload").css("background-repeat", "no-repeat ");
                    $("#divFileUpload").css("background-size", "292px 170px");
                     $("#divFileUpload").css("background-position", "center 0px")
                  //  alert(imagePath);
                   // $("#divFileUpload").css("background", "url(" + imagePath + ") no-repeat !important")
                    $(".centered").css("display", "none")
                }, 1000);

            }
            $(function () {
            $("#btnCropImage").click(function () {
              //  alert($("#imgCrop").attr("src"));
              //chordx: $('#X').val().toString(), chordy: $('#Y').val().toString(), Width: $('#W').val().toString(), Height: $('#H').val().toString()
                 $.ajax({
                        type: 'POST',
                        url: 'AddProperty.aspx/UploadImage',
                        data: '{ "imageData" : "' + $("#imgCrop").attr("src") + '","w":"'+$('#W').val().toString()+'","h":"'+$('#H').val().toString()+'","x":"'+$('#X').val().toString()+'","y":"'+$('#Y').val().toString()+'","custId":"'+$("#hidCustId").val()+'" }',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                     success: function (msg) {
                             JcropAPI = $('#imgCrop').data('Jcrop');
                         JcropAPI.release();
                       //  alert(msg.d);
                           $("#myModalCrop").modal("hide");
                         $("#divFileUpload").css("background", "url(" + msg.d + ") no-repeat")
                         $("#hidCropFile").val(msg.d);
                    // $('#blah').attr('src', e.target.result);
                    $(".centered").css("display", "none")
                           // alert('Image saved successfully !');
                            return false;
                        }
                });
                 return false;
            });
           
            })
            
        });
        $(function () {
            maxHeightPriorityButtons("streetaddress");
        })
        function maxHeightPriorityButtons(divClass) {
            var maxHeight = Math.max.apply(null, $("." + divClass).map(function () {
                return $(this).height();
            }).get());

           // alert(maxHeight);
            $("." + divClass).css("min-height", maxHeight);
           // $("#" + divClass).find(".btns .btn-default").css("min-height", maxHeight);
        }
    </script>
    <style type="text/css">
        .slider-container .scale ins {
            font-size: 14px;
            text-decoration: none;
            position: absolute;
            left: -5px;
            top: 8px;
            color: #999;
            line-height: 2;
        }

        .theme-green .scale span {
            border-left: 2px solid #e5e5e5 !important;
            height: 10px !important;
            top: -10px !important;
        }

         .theme-green .back-bar {
            height: 10px !important;
        }

        .theme-green .back-bar .selected-bar {
            width: 222px;
            left: 0px;
            background-color: unset;
            background-image: none !important;
        }

        .theme-green .back-bar .pointer {
            width: 14px;
            height: 14px;
            top: -3px;
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

        .pointer-label high {
            display: none !important;
        }

        .items li {
            width: auto !important;
        }

        .modal-lg {
            max-width: 700px;
        }

        .centered {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        a:disabled {
            color: lightgray;
            cursor: text;
        }

        .propertybox img {
            max-width: 245px !important;
            height: auto;
        }
    </style>
</asp:Content>
