<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Overview.aspx.vb" Inherits="RIS.Overview" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">

    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>REIMAGINE</title>
    <style>
        #items {
            list-style: none;
            font-size: 17px;
            color: #333333;
        }

        hr {
            width: 85%;
            background-color: #E4E4E4;
            border-color: #E4E4E4;
            color: #E4E4E4;
        }

        #cntnr {
            display: none;
            position: fixed;
            border: 1px solid #B2B2B2;
            width: 150px;
            background: #F9F9F9;
            box-shadow: 3px 3px 2px #E9E9E9;
            border-radius: 4px;
            z-index: 10 !important;
        }

        li {
            padding: 3px;
            padding-left: 10px;
        }

        #btnRedirectVMOLink {
            margin: 3px;
        }
        #btnRedirectVMLink {
               margin-bottom: 3px;
        }

        #items :hover {
            color: white;
            background: #284570;
            border-radius: 2px;
        }
    </style>
    <script type="text/javascript">       
        $(document).ready(function () {

            $("#op").bind("click", function (e) {
                e.stopPropagation();
                console.log(e.pageX + "," + e.pageY);
                $("#cntnr").css("left", e.pageX);
                $("#cntnr").css("top", e.pageY);
                $("#cntnr").show();

            });

            $(document).on("click", function () {
                $("#cntnr").hide();
            });

            console.log("ready!");
        });
        function EvaluateProperty(propertyId) {
            //alert($("#chk" + propertyId).prop("checked"));
            var isChecked = 0;
            if ($("#chk" + propertyId).prop("checked")) {
                isChecked = 1;
            }
            UpdateProperty(propertyId, isChecked);
        }
        function UpdateProperty(propertyId, selected) {
            var dataValue = '{propertyID: "' + propertyId + '", selected: "' + selected + '", customerID: "' + $("#hidCustomerID").val() + '"}';
            $.ajax({
                url: "OverView.aspx/UpdateProperty",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var answer = msg.d;
                    //answer = answer.replaceAll("!", ",");
                    //answer = answer.substr(0, answer.length - 1);
                    //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    //answer = answer.replaceAll(",", ", ");
                    //$("#" + lblId).text(answer);
                    //alert(answer);
                    return false;
                },
                error: function () { }
            });
        }
        function Evaluate(id) {
            if (confirm("Are you sure you want to send the Evaluation Email?")) {
                var dataValue = '{todoID: "' + id + '", customerID: "' + $("#hidCustomerID").val() + '"}';
                $.ajax({
                    url: "OverView.aspx/EvaluateToDo",
                    type: "POST",
                    dataType: "json",
                    data: dataValue,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        var answer = msg.d;
                        //answer = answer.replaceAll("!", ",");
                        //answer = answer.substr(0, answer.length - 1);
                        //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                        //answer = answer.replaceAll(",", ", ");
                        //$("#" + lblId).text(answer);
                        //alert(answer);
                        window.location.reload();
                        return false;
                    },
                    error: function () { }
                });
            }
        }
        function CompareDecide(id) {
            if (confirm("Are you sure you want to send the Discuss and Decide Email?")) {
                var dataValue = '{todoID: "' + id + '", customerID: "' + $("#hidCustomerID").val() + '"}';
                $.ajax({
                    url: "OverView.aspx/CompareDecideToDo",
                    type: "POST",
                    dataType: "json",
                    data: dataValue,
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        var answer = msg.d;
                        //answer = answer.replaceAll("!", ",");
                        //answer = answer.substr(0, answer.length - 1);
                        //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                        //answer = answer.replaceAll(",", ", ");
                        //$("#" + lblId).text(answer);
                        //alert(answer);
                        window.location.reload();
                        return false;
                    },
                    error: function () { }
                });
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <asp:HiddenField ID="hidPropertyContent" runat="server" />
    <asp:HiddenField ID="hidCustomerID" runat="server" />
    <div class="right" id='maincontent'>
        <ul class='arrow-steps clearfix font-weight-bold'>
            <li class='step active'>
                <span><a id="aOverView" runat="server" href='overview.aspx'>Overview</a></span>
            </li>
            <li class='step '>
                <a id="aCreateValueMap" runat="server" href='valuemap.html'><span><span class='first'>Step 1</span>
                    <span>View/Create<br />
                        Value Map</span></span></a>
            </li>
            <li class='step'>
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
        <div class='row' style="margin-top: -30px;">
            <div class='col-md-4'>
                <h2>
                    <asp:LinkButton ID="lnkCustomerName" runat="server" ForeColor="Black" Style="text-decoration: none;"></asp:LinkButton><span id="lblCustomerName" runat="server" visible="false"></span><span id="lblCustomerEmail" runat="server" visible="false"></span><asp:LinkButton ID="lnkEditContact" runat="server" Text="&nbsp;&nbsp;Edit Contact" Style="min-width: 40px!important; background-color: none; color: #333132; font-weight: bold; font-size: 12px; text-decoration: none;" OnClick="lnkEditContact_Click"></asp:LinkButton></h2>
            </div>

            <div class='col-md-8'>
                <div class='clearfix'>
                    <asp:Button CssClass="btn float-right px-3" ID="btnSendVMLink" runat="server" Text="Send VM Link" OnClick="btnSendVMLink_ServerClick" Style="margin-right: -15px" />
                    <span id="op" class="btn float-right mx-2">VM</span>
                    <div id='cntnr'>
                        <ul id='items'>
                            <li>
                                <asp:Button CssClass="btn float-right mx-2" ID="btnRedirectVMOLink"
                                    runat="server" Text="VMO Now" OnClick="btnGetVMOLink_ServerClick" />
                                <asp:Button CssClass="btn float-right mx-2" ID="btnRedirectVMLink"
                                    runat="server" Text="VM Now" OnClick="btnGetVMLink_ServerClick" />
                            </li>
                            <li>
                                <asp:Button CssClass="btn float-right mx-2" ID="btnAddToDo" runat="server" Text="Add To Do" OnClick="btnAddToDo_Click" Visible="false" /></li>

                        </ul>
                    </div>
                    <%-- <button class="btn float-right px-3" type="button" runat="server" id="btnSendVMLink" onserverclick="btnSendVMLink_ServerClick">Send VM Link</button>--%>
                    <div class="dropdown">
                    </div>
                    <div class='text-right my-0 mt-2' id="pVMLink" runat="server" visible="false" style="margin-right: -15px"></div>
                    <%-- <p class='text-right '>VM link sent 8/21/2018</p>--%>
                </div>
            </div>
            <div class='row' style="width: 106.3%">
                <div class='col-md-7'>

                    <div id="accordion" class='accordioncontent'>
                        <div class="card" style="display: none;">
                            <div class="card-header" style="padding: 0.75rem 0.75rem">
                                <a class="card-link" data-toggle="collapse" href="#collapseOne" style="font-weight: bold;">Reason for Primary change
                                </a>
                            </div>
                            <div id="collapseOne" class="collapse show" data-parent="#accordion">
                                <div class="card-body">
                                    Primary change Content
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header" style="padding: 0.75rem 0.75rem">
                                <a class="card-link" data-toggle="collapse" href="#collapseTwo" style="font-weight: bold;">Value Map Summary
                                </a>
                            </div>
                            <div id="collapseTwo" class="collapse show" data-parent="#accordion1">
                                <div class="card-body" style="padding: 0px">
                                    <table class='table' style="font-size: 13px;">

                                        <tr class="first" style="font-size: 14px">
                                            <td style="width: 100px">Priority</td>
                                            <td>Impact</td>
                                            <td>Current</td>
                                            <td>Future</td>
                                            <td>Benefits</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label runat="server" ID="lblPriority1">None Selected</asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblImpact1"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblCurrent1"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblFuture1"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblBenefits1"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label runat="server" ID="lblPriority2">None Selected</asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblImpact2"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblCurrent2"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblFuture2"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblBenefits2"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label runat="server" ID="lblPriority3">None Selected</asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblImpact3"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblCurrent3"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblFuture3"></asp:Label></td>
                                            <td class="text-center">
                                                <asp:Label runat="server" ID="lblBenefits3"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header" style="padding: 0.75rem 0.75rem">
                                <a class="card-link" data-toggle="collapse" href="#collapseThree" style="font-weight: bold;">Property Evaluation Summary
                                </a>
                            </div>
                            <div id="collapseThree" class="collapse show" data-parent="#accordion2">
                                <div class="card-body" style="padding: 0px">
                                    <table class='table' style="font-size: 13px;" id="tblPropertyOverview" runat="server">

                                        <tr class="first" style="font-size: 14px">
                                            <td style="width: 280px">Property Address</td>
                                            <td>Selected</td>
                                            <td>Evaluated</td>
                                            <td>Rank</td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />

                    <table class="table" id="tblToDo">
                        <thead>
                            <tr>
                                <th scope="col"><span style="">"To Do"</span></th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody id="tblToDoBody" style="font-size: 13px;">
                            <tr class='first' style="font-size: 14px;">
                                <td>"To Do"</td>
                                <td>Due Date</td>
                                <td>Status</td>
                                <td class='text-center'>Edit</td>
                            </tr>
                            <%--<tr>
                                <td class='underline'>Send VP Link</td>
                                <td>9/20</td>
                                <td>Complete</td>
                                <td class='text-center'><a href='#'>Edit</a></td>
                            </tr>
                            <tr>
                                <td class='underline'>Send VP Link</td>
                                <td>9/20</td>
                                <td class='red'>Complete</td>
                                <td class='text-center'><a href='#'>Edit</a></td>
                            </tr>
                            <tr>
                                <td class='underline'>Send VP Link</td>
                                <td>9/20</td>
                                <td>Complete</td>
                                <td class='text-center'><a href='#'>Edit</a></td>
                            </tr>
                            <tr>
                                <td class='underline'>Send VP Link</td>
                                <td>9/20</td>
                                <td>Complete</td>
                                <td class='text-center'><a href='#'>Edit</a></td>
                            </tr>--%>
                        </tbody>
                    </table>
                </div>
                <div class='col-md-5' style="overflow: hidden;">

                    <div class='box'>
                        <h5>Contact Information:</h5>
                        <table class='table' style="font-size: 12.5px;">
                            <tr>
                                <td style="min-width: 95px;">First Name:</td>
                                <td>
                                    <asp:Label ID="lblViewContactSecondaryFirstName" Style="display: none;" runat="server"></asp:Label>
                                    <asp:Label ID="lblViewContactSecondaryLastName" Style="display: none;" runat="server"></asp:Label>
                                    <asp:Label ID="lblViewContactFirstName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Last Name:</td>
                                <td style="text-overflow: ellipsis; overflow: hidden">
                                    <asp:Label ID="lblViewContactLastName" runat="server" Style="float: left; max-width: 135px;"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Community:</td>
                                <td style="text-overflow: ellipsis; overflow: hidden">
                                    <asp:Label ID="lblViewContactCommunity" runat="server" Style="float: left; max-width: 135px;"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Category:</td>
                                <td>
                                    <asp:Label ID="lblViewContactVMCategory" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Work:</td>
                                <td>
                                    <asp:Label ID="lblViewContactWorkPhone" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Mobile:</td>
                                <td>
                                    <asp:Label ID="lblViewContactMobilePhone" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Email:</td>
                                <td>
                                    <asp:Label ID="lblViewContactEmail" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td>Street Address:</td>
                                <td>
                                    <asp:Label ID="lblViewContactStreetAddress" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>City/State:</td>
                                <td>
                                    <asp:Label ID="lblViewContactCity" runat="server"></asp:Label>
                                    <asp:Label ID="lblViewContactState" runat="server" Style="display: none;"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Zip:</td>
                                <td>
                                    <asp:Label ID="lblViewContactZip" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class='box'>
                        <h5>Notes:</h5>
                        <table class='table'>
                            <tr>
                                <td colspan="2" style="word-break: break-word; font-size: 13px;">
                                    <asp:Label ID="lblViewContactNotes" runat="server" Style="max-width: 100%"></asp:Label>
                                </td>

                            </tr>

                        </table>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="divHtmlContent" runat="server" />
        <%-- <div id="divHtmlContent" runat="server" style="display:none;"></div>--%>
        <script type="text/javascript">
            $(function () {
                var href = $("#aOverView").attr("href");
                //alert(href);
                // $("#aOverView").attr("href", href.replace("../", ""))
                href = $("#aCreateValueMap").attr("href");
                //  $("#aCreateValueMap").attr("href", href.replace("../", ""))
                // alert($("#aCreateValueMap").attr("href"));
                //alert($("#divHtmlContent").val());
                $("#tblToDo tbody").append($("#divHtmlContent").val())

                $("#btnEditContact").click(function () {
                    window.location.href = "AddContact.aspx?cid=" + <%= Request.QueryString("cid") %> +"&mode=overview";
                    return false;
                })
                $("#btnAddToDo").click(function () {
                    window.location.href = "AddToDo.aspx?cid=" + <%= Request.QueryString("cid") %> +"&mode=overview";
                    return false;
                })
                $("#tblPropertyOverview").append($("#hidPropertyContent").val())
                $("#hidPropertyContent").val("");
                $("#btnSendVMLink").click(function () {
                    var btnClass = $(this).attr("class");
                    if (btnClass.indexOf("btn-disabled") > -1) {
                        alertDialog("No email address on record");
                        return false;
                    }
                    else {
                        //  alertDialog("email address on record");
                    }
                    return true;
                })
            })
        </script>
        <style type="text/css">
            #home {
                background-color: #cecece;
            }

            .table span {
                text-overflow: ellipsis;
                overflow: hidden;
                max-width: 170px;
                float: left;
            }

            .evaluation, .rank {
                vertical-align: middle !important;
            }

            .btn-disabled {
                color: gray;
            }
        </style>
</asp:Content>
