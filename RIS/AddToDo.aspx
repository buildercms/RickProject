<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddToDo.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="RIS.AddToDo" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    
    <meta name="description" content=""/>
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE</title>    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
     <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager> 
        <asp:HiddenField ID="hidCustomerId" runat="server" />
    <asp:HiddenField ID="hidToDoId" runat="server" />
        <div  class="right" id='maincontent'>
			<h2 id="lblPageTitle" runat="server">Add "To Do" for:</h2>
			<%--<form>--%>
				<div class="form-row">		
                    <div class="col-md-12" style="text-align:center;">
                        <asp:Label ID="lblToDoStatus" runat="server" style="display:none; text-align:center;" ></asp:Label>
                    </div>
				  <fieldset class="col-md-6">
					<div class="form-row todo-content">	
						
						<div class="form-group col-md-12">
						  <label><span  style="color:red">*</span>Title:</label>
						  <input type="text" class="form-control" id="txtTitle" runat="server"  maxlength="500" />						  
						</div>
						<div class="form-group col-md-12" style="display:none;">
						  <label><span></span>Client:</label>
						  <input type="text" class="form-control is-valid" id="txtCustomerName" runat="server" disabled="disabled" />
						</div>
						<div class="form-group col-md-12">
						  <label><span  style="color:red">*</span>Due Date:</label>
						  <input type="text" class="form-control" id="txtDueDate" readonly="readonly" runat="server" maxlength="10"  />
						</div>
						<div class="form-group col-md-12">
						  <label><span  style="color:red">*</span>Status:</label>
						   <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            </asp:DropDownList>
						</div>
                         
				  </fieldset>			 
				
				  <fieldset class="col-md-6">
				  <div class="form-row todo-content">						
						<div class="form-group col-md-12 notes">
							<label>"To Do" Description:</label>
							<textarea class="form-control pb-2" style="height:194px" id="txtDescription" runat="server"></textarea>
						</div>	
						
					</div>
				  </fieldset>		
                     <fieldset class="col-md-12">
                          <div class="form-row">
                    <div class="form-group col-md-6">
                          <asp:Button ID="btnAddToDo" runat="server" Text="Save" CausesValidation="true" ValidationGroup="ToDo" CssClass="btn "  OnClick="btnAddToDo_Click" />
                    </div>
						<div class="form-group col-md-6">
                           
							<asp:Button ID="btnCancel" CausesValidation="false" runat="server" Text="Cancel"  CssClass="btn float-right" OnClick="btnCancel_Click" />
						</div>
                              </div>
                          </fieldset>
                    
					</div>
				</div>			  
			<%--</form>--%>

            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                            ErrorMessage="Title Required" Display="None" ValidationGroup="ToDo" SetFocusOnError="True"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="rfvDueDate" runat="server" ControlToValidate="txtDueDate"
                                            ErrorMessage="Due Date Required" Display="None" ValidationGroup="ToDo" SetFocusOnError="True"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" InitialValue="0"
                                            ErrorMessage="Task Status Required" Display="None" ValidationGroup="ToDo" SetFocusOnError="True"></asp:RequiredFieldValidator>
             <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvTitle"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
                                        <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvDueDate"
                                            HighlightCssClass="validatorCalloutHighlight">
                                             </ajax:ValidatorCalloutExtender>
                                              <ajax:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvStatus"
                                            HighlightCssClass="validatorCalloutHighlight">
                                        </ajax:ValidatorCalloutExtender>
		</div>
    
  <style type="text/css">
      #ui-datepicker-div {
          display:none;
      }
  </style>   
    <script type="text/javascript">
       
      $( function() {
          $("#txtDueDate").datepicker();
            $("#txtDueDate").click(function () {
              $('#txtDueDate').datepicker('show');
          })
  } );
    </script>
    <style type="text/css">
        #home {
            background-color:#cecece;
        }
          .form-control:focus {
  border-color: inherit;
  -webkit-box-shadow: none;
  box-shadow: none;
}
    </style>
    <style type="text/css">
        .validatorCalloutHighlight
        {
            background-color: pink !important;
            border-color: Red!important;
            border-width: 2px!important;
            border-style: Solid !important;
        }

        #ValidatorCalloutExtender1_popupTable, #ValidatorCalloutExtender2_popupTable, #ValidatorCalloutExtender3_popupTable
        {
            visibility: hidden!important;
        }
        .todo-content label {
            margin-bottom:0px;
        }

    </style>
 
</asp:Content>
