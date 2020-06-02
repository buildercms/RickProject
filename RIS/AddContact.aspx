<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddContact.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="RIS.AddContact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">    
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hidCustomerId" runat="server" />
    <asp:HiddenField ID="hidOldCategoryId" runat="server" />
    <div class="right" id='maincontent'>
        <h1 id="lblPageTitle" runat="server">Add Contact</h1>
        <%--<form>--%>
        <div class="form-row customerdiv">
            <div class="col-md-12" style="text-align: center;">
                <span style="display: none; color:red" id="lblErrorJS"></span>
                <asp:Label ID="lblCustomerStatus" runat="server" Style="display: none; text-align: center;"></asp:Label>
            </div>
            <fieldset class="col-md-6">
                <div class="form-row">
                    <div class="form-group col-md-12 title">
                        <p style="font-weight: bold;">Primary Contact</p>
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>First Name:</label>
                        <input type="text" class="form-control" id="txtPrimaryFirstName" runat="server" style="outline: none !important;" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Last Name:</label>
                        <input type="text" class="form-control" id="txtPrimaryLastName" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Work Phone:</label>
                        <input type="text" class="form-control" id="txtPrimaryWorkPhone" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Mobile Phone:</label>
                        <input type="text" class="form-control" id="txtPrimaryMobilePhone" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Email:</label>
                        <input type="text" class="form-control" id="txtPrimaryEmail" runat="server" maxlength="50" />
                    </div>

                    <div class="form-group col-md-12">
                        <label>Street Address:</label>
                        <input type="text" class="form-control" id="txtPrimaryAddress" runat="server" autocomplete="off" maxlength="500" placeholder="" />
                    </div>

                    <div class="form-group col-md-12" style="margin-bottom:0.1rem !important;">
                        <div class="form-row">
                            <div class="form-group col-md-7">
                                <label>City:</label>
                                <input type="text" class="form-control" id="txtPrimaryCity" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-2">
                                <label>State:</label>
                                <input type="text" class="form-control" id="txtPrimaryState" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-3">
                                <label>Zip:</label>
                                <input type="text" class="form-control" id="txtPrimaryZip" style="text-transform:uppercase" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group col-md-2" style="display: none;">
                                <label>Country:</label>
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
                    <div class="form-group col-md-12">
                        <label><span style="color: red">*</span>Community:</label>
                        <asp:DropDownList ID="ddlCommunity" runat="server" AutoPostBack="true" CssClass="form-control" Width="388px" OnSelectedIndexChanged="ddlCommunity_SelectedIndexChanged">
                          
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-12" id="divVMStatus" runat="server" >
                           <label>Value Map Status:</label>
                       <asp:DropDownList ID="ddlVMStatus" runat="server" CssClass="form-control" style="margin-top:1px;">
                                            <asp:ListItem Text="Not Started" Value="Not Started"></asp:ListItem>
                                            <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                            <asp:ListItem Text="Sold Not Closed" Value="Sold Not Closed"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                        </asp:DropDownList>
                          </div>
                    
                    <div class="form-group col-md-12" style="margin-bottom:0.2rem !important;">
                        <asp:CheckBox ID="cbActive" runat="server" Checked ="true" Text="Active" CssClass="align-text-bottom"/>
                        </div>
                    
                   
                </div>
            </fieldset>

            <fieldset class="col-md-6">
                <div class="form-row">
                    <div class="form-group col-md-12  title">
                        <p style="font-weight: bold;">Secondary Contact</p>
                    </div>
                    <div class="form-group col-md-12">
                        <label>2nd First Name:</label>
                        <input type="text" class="form-control" id="txtSecondaryFirstName" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>2nd Last Name:</label>
                        <input type="text" class="form-control" id="txtSecondaryLastName" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Work Phone:</label>
                        <input type="text" class="form-control" id="txtSecondaryPhone" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Mobile Phone:</label>
                        <input type="text" class="form-control" id="txtSecondaryMobilePhone" runat="server" maxlength="20" />
                    </div>
                    <div class="form-group col-md-12">
                        <label>Email:</label>
                        <input type="text" class="form-control" id="txtSecondaryEmail" runat="server" maxlength="50" />
                    </div>
                    <div class="form-group col-md-12 notes" style="margin-bottom:15px !important;">
                        <div class="addressBox">
                                                <div class="head" style="padding-right: 0px; padding-left: 0px; padding-bottom: 1px; padding-top: 3px;">
                                                    <table style="margin-bottom:2px;" cellspacing="0" cellpadding="0" align="center" width="100%">
                                                        <tr>
                                                            <td  >Notes
                                                                <asp:Button ID="btnDeleteNotes" runat="server" CssClass="DeleteImage" Visible="false" />
                                                            </td>
                                                            <td class="float-right">
                                                                <asp:Button  ID="btnAddNewNote" CausesValidation="false"  runat="server" Text="Add New" CssClass="btn float-left" OnClientClick="return ToggleNote('Add');"  />
                                                                <asp:Button ID="btnSaveNote" CausesValidation="false" ValidationGroup="AddNote"  runat="server" Text="Save" CssClass="btn float-left" OnClientClick="return ValidateNote();" OnClick="btnSaveNote_Click"  Style="margin-right:5px;display:none;" />
                                                                <asp:Button ID="btnCancelNote" CausesValidation="false"  runat="server" Text="Cancel" CssClass="btn float-left" Style="display:none;" OnClientClick="return ToggleNote('Cancel');"   />
                                                            </td>
                                                                                                             </tr>
                                                    </table>
                                                </div>
                                            </div>
                        <div id="pnlAddNotes" runat="server" style="display: none; padding: 0px !important;" class="button">
                                                       
                            <asp:TextBox ID="txtAddNotes"  CssClass="form-control pb-3" TextMode="MultiLine" style="" runat="server"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ControlToValidate="txtAddNotes" InitialValue="" ErrorMessage=" " SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>  
                             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="rfvNotes"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
                                                    </div>
                                                    <div style="text-align: center; margin-top: 0px;" id="divNotesDisplay" runat="server">
                                                        <textarea  id="txtNotes" title="Double click for more detail"  readonly="readonly" runat="server" class="form-control pb-3" style="height:92px;"></textarea>
                                                       
                                                    </div>
                       <%-- <label>Notes:</label>
                        <textarea class="form-control pb-3" style="height:195px;" id="txtNotes" runat="server"></textarea>--%>
                    </div>
                      
                    <%-- <div class="form-group col-md-12">
                      

                    </div>--%>
                    <div class="form-group col-md-12" id="divVMCategory" runat="server">
                        <label><span style="color: red">*</span>Value Map Category:</label>
                        <asp:DropDownList ID="ddlVMCategory" runat="server" CssClass="form-control" Width="388px">
                            <asp:ListItem Text="One" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Two" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </fieldset>
            <div class="form-group col-md-12">
                        <asp:Button ID="btnAddContact" CausesValidation="true" ValidationGroup="Contact" runat="server" Text="Save" CssClass="btn float-left" OnClick="btnAddContact_Click" />  <asp:Button ID="btnCancel" CausesValidation="false"  runat="server" Text="Cancel" CssClass="btn float-right" OnClick="btnCancelContact_Click" />

                    </div>
        </div>
        <%--</form>--%>
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
        <asp:RequiredFieldValidator ID="rfvPrimaryFirstName" runat="server" ControlToValidate="txtPrimaryFirstName"
            ErrorMessage="First Name Required" Display="None" ValidationGroup="Contact" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvPrimaryLastName" runat="server" ControlToValidate="txtPrimaryLastName"
            ErrorMessage="Last Name Required" Display="None" ValidationGroup="Contact" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvVMCategory" runat="server" ControlToValidate="ddlVMCategory" InitialValue="0"
            ErrorMessage="VM Category Required" Display="None" ValidationGroup="Contact" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regexPrimaryEmail" runat="server" ErrorMessage='"Email" contains an invalid email address'
            ControlToValidate="txtPrimaryEmail" Display="None" ValidationExpression="\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="regexSecondaryEmail" runat="server" ErrorMessage='"Email" contains an invalid email address'
            ControlToValidate="txtSecondaryEmail" Display="None" ValidationExpression="\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvPrimaryFirstName"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvPrimaryLastName"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvVMCategory"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="regexPrimaryEmail"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="regexSecondaryEmail"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexPrimaryZip" runat="server" ErrorMessage='"Zip Code" in Work Address contains an invalid zip code'
            ControlToValidate="txtPrimaryZip" Display="None" ValidationExpression="(^[\s]*\d{5}(-\d{4})?[\s]*$)|(^[\s]*[A-Za-z][\d][A-Za-z][ ]?[\d][A-Za-z][\d][\s]*$)" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="regexPrimaryZip"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexPrimaryWorkPhone" runat="server" ErrorMessage='"Work Phone" must be in the form: ###-###-#### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Home Zip Code to enable International validation.'
            ControlToValidate="txtPrimaryWorkPhone" Display="None" ValidationExpression="\s*\(?\d{3}\)?.?\d{3}.?\d{4}\s*([xX]?\d{1,5})?" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="regexPrimaryWorkPhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexPrimaryMobilePhone" runat="server" ErrorMessage='"Mobile Phone" must be in the form: ###-###-#### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Home Zip Code to enable International validation.'
            ControlToValidate="txtPrimaryMobilePhone" Display="None" ValidationExpression="\s*\(?\d{3}\)?.?\d{3}.?\d{4}\s*([xX]?\d{1,5})?" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="regexPrimaryMobilePhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexSecondaryWorkPhone" runat="server" ErrorMessage='"Work Phone" must be in the form: ###-###-#### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Home Zip Code to enable International validation.'
            ControlToValidate="txtSecondaryPhone" Display="None" ValidationExpression="\s*\(?\d{3}\)?.?\d{3}.?\d{4}\s*([xX]?\d{1,5})?" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="regexSecondaryWorkPhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>

        <asp:RegularExpressionValidator ID="regexSecondaryMobilePhone" runat="server" ErrorMessage='"Mobile Phone" must be in the form: ###-###-#### or (###) ###-#### with an optional extension as: x#####.  If the number is an international number, please enter "99999" in the Home Zip Code to enable International validation.'
            ControlToValidate="txtSecondaryMobilePhone" Display="None" ValidationExpression="\s*\(?\d{3}\)?.?\d{3}.?\d{4}\s*([xX]?\d{1,5})?" ValidationGroup="Contact"></asp:RegularExpressionValidator>
        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="regexSecondaryMobilePhone"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
         <asp:RequiredFieldValidator ID="rfvCommunity" runat="server" ControlToValidate="ddlCommunity" InitialValue="0"
            ErrorMessage="Community Required" Display="None" ValidationGroup="Contact" SetFocusOnError="True"></asp:RequiredFieldValidator>
          <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="rfvCommunity"
            HighlightCssClass="validatorCalloutHighlight">
        </ajax:ValidatorCalloutExtender>
    </div>
    <style type="text/css">
        .validatorCalloutHighlight {
            background-color: pink !important;
            border-color: Red !important;
            border-width: 2px !important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable, #ValidatorCalloutExtender3_popupTable, #ValidatorCalloutExtender4_popupTable, #ValidatorCalloutExtender5_popupTable,
        #ValidatorCalloutExtender6_popupTable, #ValidatorCalloutExtender7_popupTable, #ValidatorCalloutExtender8_popupTable, #ValidatorCalloutExtender9_popupTable, #ValidatorCalloutExtender10_popupTable , #ValidatorCalloutExtender11_popupTable, #ValidatorCalloutExtender12_popupTable{
            visibility: hidden !important;
        }
    </style>
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
                    data: "{'zipCode':'" + zipCode.toUpperCase() + "'}",
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
                            TogglePhoneValidation(true);
                        }
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
            ValidatorEnable($("[id$='regexPrimaryWorkPhone']")[0], isValid);
            ValidatorEnable($("[id$='regexPrimaryMobilePhone']")[0], isValid);
            ValidatorEnable($("[id$='regexSecondaryWorkPhone']")[0], isValid);
            ValidatorEnable($("[id$='regexSecondaryMobilePhone']")[0], isValid);
            // alertDialog($("[id$='regexHomePhone']")[0].ValidationGrroup);
            //txtPrimaryWorkPhone,#txtPrimaryMobilePhone,#txtSecondaryPhone,#txtSecondaryMobilePhone
             
            CheckValidation('txtPrimaryWorkPhone');
            CheckValidation('txtPrimaryMobilePhone');
            CheckValidation('txtSecondaryPhone');
            CheckValidation('txtSecondaryMobilePhone');
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
    <script type="text/javascript">
        function ToggleNote(type) {
            if (type == 'Add') {
                $("#pnlAddNotes,#btnSaveNote,#btnCancelNote").css("display", "");
                $("#btnAddNewNote").css("display", "none");
                $("#txtAddNotes").val("").focus().removeClass("validatorCalloutHighlight");
                

            }
            if (type == 'Cancel') {
                 $("#pnlAddNotes,#btnSaveNote,#btnCancelNote").css("display", "none");
                $("#btnAddNewNote").css("display", "");
            }
            return false;
        }
        function ValidateNote() {
            var notes = $.trim($("#txtAddNotes").val());
            //if (notes == "") {
            //    $("#txtAddNotes").addClass("validatorCalloutHighlight")
            //    return false;
            //}
            return true;
        }
        $(function () {
            $("#txtAddNotes").on("keypress keyup keydown",function () {
                $("#txtAddNotes").removeClass("validatorCalloutHighlight");
            })
            CapitalizeFields();
            // $("#txtAddNotes").on("blur",function () {
            ////  var notes = $.trim($("#txtAddNotes").val());
            ////if (notes == "") {
            ////    $("#txtAddNotes").addClass("validatorCalloutHighlight")
               
            ////}
            //})
            //$("#txtNotes").on("focus click", function () {
            //    $(this).trigger("blur");
            //})
            
        })
        function CapitalizeFields() {
            $("#txtPrimaryFirstName,#txtPrimaryLastName,#txtSecondaryFirstName,#txtSecondaryLastName,#txtPrimaryAddress,#txtAddNotes,#txtPrimaryCity,#txtPrimaryState").on("keyup", function () {
                if ($.trim($(this).val()) != "") {
                    $(this).val(($(this).val().toString().replace(/^\s/, '')).capitalize());
                }
            })
        }
        String.prototype.capitalize = function() {
    return this.charAt(0).toUpperCase() + this.slice(1);
}
    </script>
    <style type="text/css">
        #home {
            background-color: #cecece;
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
        .form-control1 {
    display: block;
    width: 100%;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    color: #495057;
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        background: #cfcfcf;
    border: 1px solid #949597;
    border-radius: 0;
}
    </style>
    
</asp:Content>
