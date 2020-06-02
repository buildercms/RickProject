<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Reports.aspx.vb" Inherits="RIS.Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
   
    <ajax:ToolkitScriptManager ID="sc1" runat="server" ></ajax:ToolkitScriptManager>
    <div class="right" id='maincontent'>
        <div class="row">
            <div class="col-md-12">
                <h2>Reports</h2>
            </div>
        </div>
        <div class='row'>
            <div class='col-md-12'>
                <div id="accordion" class='accordioncontent' style="width: 102.5%">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-8">
                                    <a class="" data-toggle="collapse in" style="color: #62DCBF; font-weight: bold">Make Selections
                                    </a>
                                </div>
                                <div class="col-md-4">
                                    
                                </div>
                            </div>
                        </div>
                        <div id="collapseOne" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important;">
                                    <div class="form-group col-6">
                                        <label><span></span>Community:</label>
                                        <asp:ListBox ID="lbCommunity" runat="server" CssClass="form-control searchcontact" SelectionMode="Multiple" style="min-height:170px">
                                          
                                        </asp:ListBox>
                                    </div>
                                    <div class="form-group col-6">
                                        <div class="form-row" style="background-color: white!important;">
                                    <div class="form-group col-6">
                                        <label><span></span>Start Date:</label>
                                        <asp:TextBox ID="txtStartDate" CssClass="form-control searchcontact" style="outline: none!important;"  runat="server"></asp:TextBox>
                                       <%--<input type="text" class="form-control searchcontact" id="txtStartDate" runat="server" placeholder="Start Date" style="outline: none!important;" />--%>
                                       <%-- <ajax:CalendarExtender ID="ceStartDate" runat="server" TargetControlID="txtStartDate"
								Format="MM/dd/yyyy" />--%>
                                    </div>
                                              <div class="form-group col-6">
                                        <label><span></span>End Date:</label>
                                                   <asp:TextBox ID="txtEndDate" CssClass="form-control searchcontact" style="outline: none!important;"  runat="server"></asp:TextBox>
                                       <%--<input type="text" class="form-control searchcontact" id="txtEndDate" runat="server" placeholder="End Date" style="outline: none!important;" />--%>
                                                 <%-- <ajax:CalendarExtender ID="ceEndDate" runat="server" TargetControlID="txtEndDate"
								Format="MM/dd/yyyy" />--%>
                                    </div>
                                            <div class="form-group col-4">
                                                <asp:LinkButton ID="lnkYearToDate" runat="server" ForeColor="#333132" Text="Year to Date"  OnClick="lnkYearToDate_Click"></asp:LinkButton><br />
                                                <asp:LinkButton ID="lnkMonthToDate" runat="server" ForeColor="#333132" Text="Month to Date"  OnClick="lnkMonthToDate_Click"></asp:LinkButton><br />
                                                <asp:LinkButton ID="lnkWeekToDate" runat="server" ForeColor="#333132" Text="Week to Date"  OnClick="lnkWeekToDate_Click"></asp:LinkButton><br />
                                                <asp:LinkButton ID="lnkLastWeek" runat="server" ForeColor="#333132" Text="Last Week"  OnClick="lnkLastWeek_Click"></asp:LinkButton><br />
                                                <asp:LinkButton ID="lnkYesterday" runat="server" ForeColor="#333132" Text="Yesterday"  OnClick="lnkYesterday_Click"></asp:LinkButton>
                                                </div>
                                            <div class="form-group col-8">
                                                <asp:Button ID="btnGenerate" Visible="false" Style="margin-top:80px; min-width:110px!important" OnClick="btnGenerate_Click"  runat="server" Text="Generate" CausesValidation="false"  CssClass="btn float-left font-weight-bold" /><asp:Button ID="btnResetDates" Style="margin-top:80px; min-width:110px!important;"  runat="server" Text="Reset Dates" CausesValidation="false" OnClick="btnResetDates_Click"  CssClass="btn float-right font-weight-bold" />
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class='row' style="margin-right: -35px!important;">
            <div class='col-md-4'>
                  <div id="accordion1" class='accordioncontent' >
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-12">
                                    <a class="" runat="server" id="lnkSales"  data-toggle="collapse in" onserverclick="lnkSales_ServerClick" style="color: #62DCBF; font-weight: bold">Sales
                                    </a>
                                </div>
                              
                            </div>
                        </div>
                        <div id="collapseOne1" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important; margin-bottom:37px;">
                                    <div class="form-group col-12">
                                         <asp:CheckBox ID="chkForeCast" CssClass="align-text-bottom" runat="server" Text="Forecast" /><br />
                                         <asp:CheckBox ID="chkDivisionForeCast" CssClass="align-text-bottom" runat="server" Text="Division Forecast" /><br />
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                      </div>
                
                </div>
             <div class='col-md-4'>
                 <div id="accordion2" class='accordioncontent'  >
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-12">
                                    <a class="" runat="server" id="lnkPrioritySelection" onserverclick="lnkPrioritySelection_ServerClick" data-toggle="collapse in" style="color: #62DCBF; font-weight: bold">Priority Count Percent
                                    </a>
                                </div>
                               
                            </div>
                        </div>
                        <div id="collapseOne2" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important; margin-bottom:50px;">
                                    
                                    <div class="form-group col-12">
                                       
                                        <asp:DropDownList ID="ddlPriority" style="visibility:hidden;" runat="server" CssClass="form-control searchcontact"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                      </div>
                </div>
             <div class='col-md-4'>
                 <div id="accordion3" class='accordioncontent' >
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-12">
                                    <a class="" id="lnkWordSelection" runat="server" onserverclick="lnkWordSelection_ServerClick" data-toggle="collapse in" style="color: #62DCBF; font-weight: bold">Word Selection Totals
                                    </a>
                                </div>
                               
                            </div>
                        </div>
                        <div id="collapseOne3" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important;">
                                    <div class="form-group col-12">
                                         <asp:DropDownList ID="ddlWordSelectionPriority" runat="server" CssClass="form-control searchcontact"></asp:DropDownList>
                                        </div><br />
                                         <asp:CheckBox ID="chkCurrentFuture" CssClass="align-text-bottom" runat="server" Text="WWD Current/WWD Future" /><br />
                                        <asp:CheckBox ID="ChkImpactBenefit" CssClass="align-text-bottom" runat="server" Text="Impact/Benefit" />
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                      </div>
             
        
    </div>
        <div class="row" style="margin-right: -35px!important ;margin-top:20px;">
            <div class='col-md-4'>
                 <div id="accordion4" class='accordioncontent' >
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-12">
                                    <a class="" id="lnkSalesPerformance" runat="server" onserverclick="lnkSalesPerformance_ServerClick" data-toggle="collapse in" style="color: #62DCBF; font-weight: bold">Sales Performance
                                    </a>
                                </div>
                               
                            </div>
                        </div>
                        <div id="collapseOne4" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important;">
                                    <div class="form-group col-12" style="visibility:hidden;">
                                         <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control searchcontact"></asp:DropDownList>
                                        </div><br />
                                         <asp:CheckBox ID="CheckBox1" style="visibility:hidden;" CssClass="align-text-bottom" runat="server" Text="WWD Current/WWD Future" /><br />
                                        <asp:CheckBox ID="CheckBox2" style="visibility:hidden;" CssClass="align-text-bottom" runat="server" Text="Impact/Benefit" />
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                      </div>
                </div>
            </div>
        
    <script type="text/javascript">
       

    </script>
    <style type="text/css">
        .form-control {
            background-color: white !important;
        }

        .table-borderless {
            border: none;
        }

        .card-body label {
            margin-bottom: 0px !important;
        }

        .card-body {
            padding-bottom: 0px;
        }
    </style>
    <style type="text/css">
        #home {
            background-color: #cecece;
        }

        textarea:focus, input:focus {
            outline: none;
        }

        .form-control:focus {
            border-color: inherit;
            -webkit-box-shadow: none;
            box-shadow: none;
            outline: none;
        }
        input[type$=checkbox] {
            margin-right:1px;
        }
    </style>
    <style type="text/css">
      #ui-datepicker-div {
          display:none;
      }
  </style>   
    <script type="text/javascript">
       
      $( function() {
          $("#txtStartDate,#txtEndDate").datepicker();
          $("#txtStartDate,#txtEndDate").click(function () {
              $(this).datepicker('show');
          })
  } );
    </script>
    <script type="text/javascript">
        $(function () {
            $("#chkCurrentFuture").on("click", function () {
                var isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#ChkImpactBenefit").prop("checked", false);
                }
            })
             $("#ChkImpactBenefit").on("click", function () {
                var isChecked = $(this).prop("checked");
                if (isChecked) {
                    $("#chkCurrentFuture").prop("checked", false);
                }
            })
        })
    </script>
</asp:Content>
