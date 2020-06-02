<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteCustomer.Master" CodeBehind="ValueMapCustomer.aspx.vb" Inherits="RIS.ValueMapCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE</title>
    <style>
        .triangle-left {
            width: 0;
            position: absolute;
            top: 50%;
            height: 0;
            right: 0;
            border-top: 100px solid transparent;
            border-right: 100px solid #ccc;
            border-bottom: 100px solid transparent;
            margin-top: -85px;
        }

        .triangle-right {
            width: 0;
            position: absolute;
            top: 50%;
            height: 0;
            left: 0;
            border-top: 100px solid transparent;
            border-left: 100px solid #ccc;
            border-bottom: 100px solid transparent;
            margin-top: -85px;
        }

        .box.valuemap {
            padding: 20px;
        }

            .box.valuemap .font-weight-bold {
                font-size: 12px;
            }

        .valuemap .col.col3 {
            position: relative;
        }

        .valuemap .col .circle {
            z-index: 1;
            background-color: #5addbe;
            margin-top: -54px;
            width: 140px;
            height: 140px;
            position: absolute;
            top: 50%;
            padding-top: 10px;
            left: 50%;
            margin-left: -69px;
            display: table;
        }

        .valuemap .col .square {
            background-color: #333132;
        }

            .valuemap .col .square .text {
                color: #fff;
            }

        .valuemap .col .circle .text {
            color: #333132;
        }

        .col .square span.leftcircle {
            left: -10px;
        }

        .square span.red {
            background-color: #F15944;
            color: #fff;
        }

        .col .square span.rightcircle {
            right: -10px;
        }

        .square span.skyblue {
            background-color: #5addbe;
            color: #333132;
        }

        .circle > div {
            display: table-cell;
            vertical-align: middle;
        }

            .circle > div p {
                margin-bottom: 0px;
            }

        .theme-green .back-bar {
            height: 10px !important;
        }

            .theme-green .back-bar .pointer {
                top: -3px !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <asp:HiddenField ID="hidPriority" runat="server" />
    <asp:HiddenField ID="hidCustId" runat="server" />
    <asp:HiddenField ID="hidPriorityId" runat="server" />
    <asp:HiddenField ID="hidPriorityName" runat="server" />
    <asp:HiddenField ID="hidCustomerPriority" runat="server" />
    <asp:HiddenField ID="hidQuestionType" runat="server" />
    <asp:HiddenField ID="hidSelectedPriorities" runat="server" Value="" />
    <asp:HiddenField ID="hidImpactAnswer" runat="server" />
    <asp:HiddenField ID="hidUserEmail" runat="server" />
    <asp:HiddenField ID="hidUserMode" runat="server" />
    <asp:Button id="btnRealtorEmail" runat="server" OnClick="btnRealtorEmail_Click" style="display:none;" />
     <asp:HiddenField ID="hidValueMapStatus" runat="server" />
    <asp:HiddenField ID="hidValueMapQuestionDisplay" runat="server" />
    <%-- <div class="row">--%>
    <%--<nav class="col-md-2 d-none d-md-block  sidebar mt-5">
		  <div class='profileinfo'>			
			<div class='image'><img src='images/logo.png' alt='' height='100' /></div>
			<p><a href='settings.html'>User Name</a></p>
		  </div>
          <div class="sidebar-sticky text-center">
            <ul class="nav flex-column">
              <li class="nav-item active">
                <a class="nav-link " href="#">
                 <i class="fa fa-user"></i>Contacts
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="findincontact.html">
                  <i class="fa fa-user"></i>To Do's
                </a>
              </li>
              <li class="nav-item">
                <a class="nav-link" href="todoactions.html">
                  <i class="fa fa-user"></i>Settings
                </a>
              </li>              
            </ul>
          </div>
			<div class='valuemapselect col-md-12'>
			  <div class='row'>
					<div class='col col1'>
						
						<div class='square'>
						
						</div>
						<div class='square'>
						
						</div>
						<div class='square'>
						
						</div>
						
					</div>
					<div class='col col2'>
						
						<div class='square selected'>
						</div>
						<div class='square'>
						
						</div>
						<div class='square'>
						
						</div>
					</div>
					<div class='col col3 text-center'>
						
						<div class='circle selected'>
						</div>
						<div class='circle selected '>
						</div>
						<div class='circle '>
						</div>
					</div>
					<div class='col col4'>
						
						<div class='square'>
						</div>
						<div class='square'>
						
						</div>
						<div class='square'>
						
						</div>
					</div>
					<div class='col col5 '>
						
						<div class='square selected'>
						</div>
						<div class='square'>
						
						</div>
						<div class='square'>
						
						</div>
					</div>
				</div>
				<p>2 of 12 <br/> Answers Completed</p>
		  </div>		  
        </nav>--%>
    <div class="right" id='maincontent'>
        <div class="row" id="divCompleted" runat="server" style="top: 340px!important; position: absolute; overflow-x: hidden; width: 945px; display: none;">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <p class="font-weight-bold" style="color:white;
    font-size: 20px;margin-left: -6%;">
                    Your Value Map has been submitted to your Sales Associate:
                </p>
                <p class="font-weight-bold text-left" id="pUserName" runat="server" style="margin-left: 22%; color: white;
    font-size: 20px;"></p>
            </div>

        </div>
        <div class="row" id="divVerifyEmail" runat="server" style="top: 270px!important; position: absolute; overflow-x: hidden; width: 945px;">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <p class="font-weight-bold">
                    For verification purposes please confirm your email address:
                </p>
            </div>
            <div class="col-md-2"></div>
            <div class="col-md-9" style="text-align: center; max-width: 45%;">
                <p class="font-weight-bold">
                    <span id="lblUserEmail" runat="server" style="text-align: center;">e***r@aol.com</span>
                </p>
            </div>
            <div class="col-md-3"></div>
            <div class="col-md-9" style="text-align: center;">
                <asp:TextBox ID="txtVerifyEmail" runat="server" Width="450px" Style="margin-left: 50px"></asp:TextBox>

            </div>
            <div class="col-md-4"></div>
            <div class="col-md-9" style="text-align:center;">
                <br />
                <asp:Button ID="btnVerifyEmail" CssClass="btn" runat="server" Text="Verify" Enabled="true" style="margin-left:65px;"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelEmail" CssClass="btn" runat="server" Text="Cancel" Enabled="true" />
            </div>
        </div>
        <ul class='arrow-steps clearfix font-weight-bold' style="display: none;">
            <li class='step'>
                <a id="aOverView" runat="server" href='#'><span>Overview</span></a>
            </li>
            <li class='step active'>
                <a id="aCreateValueMap" runat="server" href='#'><span><span class='first'>Step 1</span>
                    <span>View/Create<br />
                        Value Map</span></span></a>
            </li>
            <li class='step'>
                <a href='#' id="aCreateProperty" runat="server"><span><span class='first'>Step 2</span>
                    <span>Add/Evaluate<br />
                        Properties</span></span></a>
            </li>
            <li class='step'>
                <a id="aCompareDecide" runat="server" href='#'><span><span class='first'>Step 3 </span>
                    <span>Discuss and<br />
                        Decide</span></span></a>
            </li>
        </ul>
        <div class="row queries my-3" id="divCustomerInfo" runat="server" style="display: none;">
            <div class='col-md-7'>
                <h2><span id="lblCustomerName" runat="server"></span></h2>
            </div>
            <div class='col-md-5' style="padding: 10px;">
                <button class="btn float-right align-text-bottom font-weight-bold px-3" id="btnVmReview" runat="server" type="button">&nbsp;VM Review&nbsp;</button>
            </div>
        </div>
        <div class="row">


            <div class="col-md-12" style="margin-left: 15px;">
                <div class='row' id="divPriorityOverView" runat="server">
                    <div class='col-12 valuemap box'>
                        <div class='row'>
                            <div class='col col1'>
                                <p class='text-center red font-weight-bold text-uppercase'>Current Impacts</p>
                                <div class='square csimpactcontent'>
                                    <div class='text csimpact' questiontype="1" id="divPriority1Impacts" runat="server">
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                    <span class='leftcircle red' id="lblImpactsScore1" runat="server" style="display: none;">7</span>
                                </div>
                            </div>
                            <div class='col col2'>
                                <p class='text-center  red font-weight-bold text-uppercase'>WWD Current</p>
                                <div class='square csdesccontent'>
                                    <div class='text csdesc' questiontype="2" id="divPriority1CSDesc" runat="server">
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                    <%--<span class='leftcircle red'>7</span>--%>
                                </div>

                            </div>
                            <div class='col col3 text-center'>
                                <div class="triangle-right"></div>
                                <div class='circle '>
                                    <div id="divPriority1Content" class="prioritycircle">
                                        <p class='font-weight-bold' style="font-size: 10px;">PRIORITY 1</p>
                                        <h3 id="lblPriority1Content" runat="server" style="font-size: 13px">Location</h3>
                                    </div>
                                </div>
                                <div class="triangle-left"></div>
                            </div>
                            <div class='col col4'>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>WWD Future</p>
                                <div class='square fsdesccontent'>
                                    <div class='text fsdesc' questiontype="3" id="divPriority1FSDesc" runat="server">
                                    </div>
                                    <span class='rightcircle skyblue' style="display: none;">7</span>
                                </div>

                            </div>
                            <div class='col col5 '>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>Future Benefits</p>
                                <div class='square fsimpactcontent'>
                                    <div class='text fsimpact' questiontype="4" id="divPriority1FSBenefits" runat="server">
                                    </div>
                                    <span id="lblBenefitScore1" class='rightcircle skyblue' runat="server" style="display: none;">6</span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class='col-12 valuemap box'>
                        <div class='row'>
                            <div class='col col1'>
                                <p class='text-center red font-weight-bold text-uppercase'>Current Impacts</p>
                                <div class='square csimpactcontent'>
                                    <div class='text csimpact' questiontype="1" id="divPriority2Impacts" runat="server">
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                    <span class='leftcircle red' id="lblImpactsScore2" runat="server" style="display: none;">7</span>

                                </div>
                            </div>
                            <div class='col col2'>
                                <p class='text-center  red font-weight-bold text-uppercase'>WWD Current</p>
                                <div class='square csdesccontent '>
                                    <div class='text csdesc' questiontype="2" id="divPriority2CSDesc" runat="server">
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                </div>

                            </div>
                            <div class='col col3 text-center'>
                                <div class="triangle-right"></div>
                                <div class='circle '>
                                    <div class="prioritycircle">
                                        <p class='font-weight-bold' style="font-size: 10px;">PRIORITY 2</p>
                                        <h3 id="lblPriority2Content" runat="server" style="font-size: 13px;">Location</h3>
                                    </div>
                                </div>
                                <div class="triangle-left"></div>

                            </div>
                            <div class='col col4'>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>WWD Future</p>
                                <div class='square fsdesccontent'>
                                    <div class='text fsdesc' questiontype="3" id="divPriority2FSDesc" runat="server">
                                    </div>
                                    <span class='rightcircle skyblue' style="display: none;">7</span>
                                </div>

                            </div>
                            <div class='col col5 '>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>Future Benefits</p>
                                <div class='square fsimpactcontent'>
                                    <div class='text fsimpact' questiontype="4" id="divPriority2FSBenefits" runat="server">
                                    </div>
                                    <span id="lblBenefitScore2" class='rightcircle skyblue' runat="server" style="display: none;">6</span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class='col-12 valuemap box'>
                        <div class='row'>
                            <div class='col col1'>
                                <p class='text-center red font-weight-bold text-uppercase'>Current Impacts</p>
                                <div class='square csimpactcontent'>
                                    <div class='text csimpact' questiontype="1" id="divPriority3Impacts" runat="server">
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                    <span class='leftcircle red' id="lblImpactsScore3" runat="server" style="display: none;">7</span>

                                </div>
                            </div>
                            <div class='col col2'>
                                <p class='text-center  red font-weight-bold text-uppercase'>WWD Current</p>
                                <div class='square csdesccontent '>
                                    <div class='text csdesc' questiontype="2" id="divPriority3CSDesc" runat="server">
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            <br />
                                        </p>
                                        <p>
                                            Hard to do
                                            <br />
                                            Markup
                                        </p>
                                    </div>
                                </div>

                            </div>
                            <div class='col col3 text-center'>
                                <div class="triangle-right"></div>
                                <div class='circle '>
                                    <div class="prioritycircle">
                                        <p class='font-weight-bold' style="font-size: 10px;">PRIORITY 3</p>
                                        <h3 id="lblPriority3Content" runat="server" style="font-size: 13px;">Location</h3>
                                    </div>
                                </div>
                                <div class="triangle-left"></div>

                            </div>
                            <div class='col col4'>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>WWD Future</p>
                                <div class='square fsdesccontent'>
                                    <div class='text fsdesc' questiontype="3" id="divPriority3FSDesc" runat="server">
                                    </div>
                                    <span class='rightcircle skyblue' style="display: none;">7</span>
                                </div>

                            </div>
                            <div class='col col5 '>
                                <p class='text-center skyblue font-weight-bold text-uppercase'>Future Benefits</p>
                                <div class='square fsimpactcontent'>
                                    <div class='text fsimpact' questiontype="4" id="divPriority3FSBenefits" runat="server">
                                    </div>
                                    <span id="lblBenefitScore3" class='rightcircle skyblue' runat="server" style="display: none;">6</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="divCustomerPriorityContent">
                <div id="divEditPriority" runat="server" style="margin-top: -40px;">

                    <div class="row queries my-3">
                        <div class="col-md-12">
                            <div id="divPriority" style="margin-top: -15px!important;" runat="server">

                                <div class='row queries my-3 '>
                                    <div class='col-md-12'>
                                        <p style="font-size: 19px; font-weight: bold" class="priority-header">Define Your Priorities</p>
                                    </div>
                                    <div class='col-md-12'>
                                        <p style="font-weight: bold" class="priority-header">What 3 things are most important to you in a new place to live?</p>
                                    </div>
                                </div>
                                <div class='row queries my-3 priorities' runat="server" id="divPriorities">
                                    <div class='col-md-6'>
                                    </div>
                                    <div class='col-md-6'>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousPriority" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextPriority" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divRank" runat="server" style="display: none; margin-top: -15px;">
                                <div class='row queries my-3'>

                                    <div class='col-md-12'>
                                        <p style="font-weight: bold; font-size: 19px" class="priority-header">Rank your priorities in order of importance</p>
                                    </div>
                                </div>
                                <div class='row queries my-3 rank' runat="server" id="divPriorityRank">
                                    <div class='col-md-2' style="font-weight: bold">
                                        <span class="spn-default" style="padding: 10px; height: 45px; line-height: 3">Rank 1</span>
                                        <span class="spn-default" style="padding: 10px; height: 45px; line-height: 3">Rank 2</span>
                                        <span class="spn-default" style="padding: 10px; height: 45px; line-height: 3">Rank 3</span>
                                    </div>
                                    <div class='col-md-6'>
                                        <ul id="sortable" class="ui-sortable" style="margin-left: -90px;">
                                            <li class="list-group-item" style="cursor: move!important;">
                                                <span class="spn-default" priorityid="1" style="cursor: move!important;" id="lblItem1" runat="server">Floor Plane1</span>
                                            </li>
                                            <li class="list-group-item">
                                                <span class="spn-default" priorityid="2" style="cursor: move!important;" id="lblItem2" runat="server">Floor Plane2</span>
                                            </li>
                                            <li class="list-group-item">
                                                <span class="spn-default" priorityid="3" style="cursor: move!important;" id="lblItem3" runat="server">Floor Plane3</span>
                                            </li>
                                        </ul>



                                    </div>

                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousRank" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextRank" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divCSFuture" runat="server" style="display: none;">

                                <div class='row'>
                                    <div class='col-md-12'>
                                        <h2 style="font-size: 19px; font-weight: bold">Priority: <span class="priority-title"></span></h2>
                                    </div>

                                    <div class='col-md-12'>
                                        <p style="font-weight: bold" class="priority-header">What words describe your <b>IDEAL </b><span id="lblPriorityName" runat="server" class="priority-title"></span>? <span style="font-size:13px!important">(select up to 4 answers)</span></p>
                                    </div>
                                </div>
                                <div class='row queries my-3 futures' runat="server" id="divFuture">
                                    <div class='col-md-3'>
                                    </div>
                                    <div class='col-md-3'>
                                    </div>
                                    <div class='col-md-3'>
                                    </div>
                                    <div class='col-md-3'>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousFuture" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextFuture" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divCSCurrent" runat="server" style="display: none;">

                                <div class='row'>
                                    <div class='col-md-12'>
                                        <h2 style="font-size: 19px; font-weight: bold">Priority: <span class="priority-title"></span></h2>
                                    </div>

                                    <div class='col-md-12'>
                                        <p style="font-weight: bold" class="priority-header">What words describe your <b>CURRENT </b><span id="lblPriorityCurrentTitle" runat="server" class="priority-title"></span>? <span style="font-size:13px!important">(Choose from one list & select up to 4 answers</span>)</p>
                                    </div>
                                </div>
                                <div class='row queries my-3'>
                                    <div class="col-6" id="divCSFutureContent" runat="server">
                                        <div class="row" style="background: white; margin-left: 0px; margin-right: 0px;">
                                            <div class="col-md-12">
                                                <p class="text-center" style="color: #62DCBF; margin-top: 10px; font-size: 22px; font-weight: bold;">I'M HAPPY</p>
                                            </div>
                                            <div class="col-md-12" style="font-size: 13px;">
                                                <p style="margin-top: -10px;text-align:center">Choose from this list if you're HAPPY with your current <br /><span class="priority-title"></span>?</p>
                                            </div>
                                        </div>
                                        <div class='' id="divCSFutureChoice" runat="server" style="margin-top: 0px !important;" data-wwdc="1">
                                            <div class='col-md-6'>
                                            </div>
                                            <div class='col-md-6'>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-6" id="divCSCurrentContent" runat="server">
                                        <div class="row" style="background: white; margin-left: 0px; margin-right: 0px;">
                                            <div class="col-md-12">
                                                <p class="text-center" style="color: #F05A4B; margin-top: 10px; font-size: 22px; font-weight: bold">I'M UNHAPPY</p>
                                            </div>
                                            <div class="col-md-12" style="font-size: 13px;">
                                                <p style="margin-top: -10px;text-align:center;">Choose from this list if you're UNHAPPY with your current <br /><span class="priority-title"></span>?</p>
                                            </div>
                                        </div>
                                        <div class='' id="divCSCurrentChoice" runat="server" style="margin-top: 0px !important;">
                                            <div class='col-md-6'>
                                            </div>
                                            <div class='col-md-6'>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousCurrent" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextCurrent" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divCSImpact" runat="server" style="display: none;">

                                <div class='row'>
                                    <div class='col-md-12'>
                                        <h2 style="font-size: 19px; font-weight: bold">Priority: <span class="priority-title"></span></h2>
                                    </div>

                                    <div class='col-md-12'>
                                        <p style="font-weight: bold" class="priority-header priorityimpacttitle">How does <span id="lblImpact" class="lblImpact">XXX,XXXXX and XXXX</span> <b>IMPACT </b>your every day life and the way you live? <span style="font-size:13px!important">(select up to 4 answers)</span></p>
                                    </div>
                                </div>
                                <div class='row queries my-3 futures' runat="server" id="divImpact">
                                    <div class='col-md-4'>
                                    </div>
                                    <div class='col-md-4'>
                                    </div>
                                    <div class='col-md-4'>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousImpact" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextImpact" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divCSBenefit" runat="server" style="display: none;">

                                <div class='row'>
                                    <div class='col-md-12'>
                                        <h2 style="font-size: 19px; font-weight: bold">Priority: <span class="priority-title"></span></h2>
                                    </div>

                                    <div class='col-md-12'>
                                        <p style="font-weight: bold" class="priority-header ">How would "<span id="lblBenefit" style="">XXX,XXXXX and XXXX</span>" <b>BENEFIT</b> your every day life and the way you live? <span style="font-size:13px!important">(select up to 4 answers)</span></p>
                                    </div>
                                </div>
                                <div class='row queries my-3 futures' runat="server" id="divBenefit">
                                    <div class='col-md-4'>
                                    </div>
                                    <div class='col-md-4'>
                                    </div>
                                    <div class='col-md-4'>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnPreviousBenefit" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                                    </div>
                                    <div class='col-md-6'>
                                        <asp:Button ID="btnNextBenefit" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row " style="margin-top: -20px;">
            <div class="col-md-12" id="divEditPriorityTitleHead" style="display: none;">
                <h2 style="font-size: 19px; font-weight: bold;">Priority: <span id="lblEditPriorityTitle"></span></h2>
            </div>
            <div class="col-md-12">
                <span id="lblEditPriorityType" style="font-size: 19px; font-weight: bold;"></span>
            </div>
            <div class="col-md-12">
                <div class='row queries my-3' runat="server" id="divEditModalContent" style="display: none;">
                    <div class='col-md-4'>
                    </div>
                    <div class='col-md-4'>
                    </div>
                    <div class='col-md-4'>
                    </div>
                </div>
                <div class='row queries my-3' id="divEditModalContent1" style="display: none;">
                    <div class="col-6" id="divCSFutureContentEdit">
                        <div class="row" style="background: white; margin-left: 0px; margin-right: 0px;">
                            <div class="col-12">
                                <p class="text-center" style="color: #62DCBF; margin-top: 10px; font-size: 22px; font-weight: bold;">I'M HAPPY</p>
                            </div>
                            <div class="col-12" style="font-size: 13px;">
                                <p style="margin-top: -10px;text-align:center">Choose from this list if you're HAPPY with your current <br /><span class="priority-title"></span>?</p>
                            </div>
                        </div>
                        <div id="divCSFutureChoiceEdit">
                            <div class='col-6'>
                            </div>
                            <div class='col-6'>
                            </div>
                        </div>

                    </div>

                    <div class="col-6" id="divCSCurrentContentEdit" runat="server">
                        <div class="row" style="background: white; margin-left: 0px; margin-right: 0px;">
                            <div class="col-12">
                                <p class="text-center" style="color: Red; margin-top: 10px; font-size: 22px; font-weight: bold;">I'M UNHAPPY</p>
                            </div>
                            <div class="col-12" style="font-size: 13px;">
                                <p style="margin-top: -10px; padding: 0px;text-align:center">Choose from this list if you're UNHAPPY with your current <br /><span class="priority-title"></span>?</p>
                            </div>



                        </div>
                        <div id="divCSCurrentChoiceEdit" runat="server">
                            <div class='col-6'>
                            </div>
                            <div class='col-6'>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-12" style="display: none;" id="divEditButtons">

                <asp:Button ID="btnCancel" CssClass="btn float-right " runat="server" Text="Cancel" Enabled="true" />
                <asp:Button ID="btnOk" CssClass="btn float-right mx-2 " runat="server" Text="Ok" Enabled="true" />
            </div>
        </div>
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="width: 700px;">
                <div class="modal-header" style="border-bottom: none!important;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <%-- <div class="col-md-3"></div>--%>
                        <div class="col-md-12 text-center" style="margin-bottom: 40px;">
                            <input type="hidden" id="hidSelectPriorityId" value="" />
                            <span id="lblPriorityDialogTitle" class="font-weight-bold" style="font-size: 20px;">Priority 1: Area/Location</span>
                        </div>
                        <%-- <div class="col-md-2"></div>--%>
                        <div class="col-md-12 mx-auto" style="margin-bottom: 40px;display: flex;
  justify-content: center;">
                            <%-- <ul id="ulImpacts" class="items" data-columns="2" style="column-count: 2; -webkit-column-count: 2; -moz-column-count: 2; -webkit-column-break-inside: avoid; page-break-inside: avoid; break-inside: avoid;">
                                <li>Too Many Grocery Tips
                                </li>
                            </ul>--%>
                            <table id="tblImpacts" border="0" class="items" cellspacing="20px" style="font-size: 13px!important;align-self: center;">
                            </table>
                        </div>
                        <%--<div class="col-md-3"></div>--%>
                        <div class="col-md-12 text-center">
                            <p class="font-weight-bold" id="lblImpactsTitle" style="font-size: 20px;">How much do these things bother you?</p>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-10" style="margin-bottom: 30px;">
                            <input type="hidden" class="slider-input" value="" />
                        </div>
                        <div class="col-md-1"></div>


                    </div>


                </div>
                <div class="modal-footer" style="border-top: none;">
                    <div class='col-md-6'>
                        <asp:Button ID="btnPreviousScore" CssClass="btn float-left my-4 font-weight-bold" runat="server" Text="Previous" Enabled="true" />
                    </div>
                    <div class='col-md-6'>
                        <asp:Button ID="btnNextScore" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />
                    </div>
                </div>

            </div>

        </div>
    </div>

    <div id="priorityModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="width: 800px; background-color:black; left:-50px;">
                <div class="modal-header" style="border-bottom: none!important;">
                    <button type="button" class="close" data-dismiss="modal" style="display:none;">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <%-- <div class="col-md-3"></div>--%>
                        <div class="col-md-12 text-center" style="margin-bottom: 0px; display: table; min-height: 500px; max-height: 500px;">
                             <p style="margin-top:190px; display:inline-block; ">
                                     <span id="lblPriorityDesc" class="font-weight-bold" style="display: table-cell;
    vertical-align: middle;
    color: White;
    font-size: 28px;
    padding: 10px;">Begin Your Value Map</span><br />
                               
                                  
                                <button type="button" class="btn  font-weight-bold" data-dismiss="modal" style="font-size:24px">OK</button>
                            </p>
                                </div>
                            </div>

                    </div>
                    <div class="modal-footer" style="border-top: none; display:none;">

                        <div class='col-md-6'>
                            

                        </div>
                    </div>

            </div>
        </div>
    </div>

    <div id="valueMapModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="width: 800px; background-color:black; left:-50px;">
                <div class="modal-header" style="border-bottom: none!important; display:none;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <%-- <div class="col-md-3"></div>--%>
                        <div class="col-md-12 text-center" style="margin-bottom: 0px; display: table; min-height: 500px; max-height: 500px;">
                             <p style="margin-top:190px; display:inline-block; ">
                                     <span id="lblvalueMapDesc" class="font-weight-bold" style="display: table-cell;
    vertical-align: middle;
    color: white;
    font-size: 28px;
    padding: 10px;">Begin Your Value Map</span><br />
                               
                                  
                                <button type="button" class="btn  font-weight-bold" data-dismiss="modal" style="font-size:24px">OK</button>
                            </p>
                                </div>
                            </div>

                    </div>
                    <div class="modal-footer" style="border-top: none; display:none;">

                        <div class='col-md-6'>
                            

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <div id="otherItemModal" class="modal fade" role="dialog" >
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content" >
      <div class="modal-header" style="border-bottom:none!important;">
      <%--  <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
      </div>
      <div class="modal-body" style="padding-top:0px; padding-bottom:0px;">
        <div class="row">
            
            <div class="col-md-12 text-center" >
                <input type="hidden" id="hidOther" />
            <span style="font-weight:bold;">Other: </span> <input type="text" id="txtOther" />
        </div>
            <div class='col-md-12 text-center'>
                                        <button type="button" class="btn  my-4 font-weight-bold" style="margin-right:10px;min-width:110px!important;" id="btnOtherItemOk" onclick="AddOtherItem()" >OK</button>
                                        <button type="button" class="btn  my-4 font-weight-bold" data-dismiss="modal" style="min-width:110px!important" id="btnOtherItemCancel">Cancel</button>
                                        
                                        
                                        <%--<asp:Button ID="btnNextScore" data-dismiss="modal" CssClass="btn float-right my-4 font-weight-bold" runat="server" Text="Next" />--%>
                                    </div>
                                    </div>
      </div>
         <div class="modal-footer" style="border-top:none; padding:0px;display:none;">
            
                                    
             </div>
      
    </div>

  </div>
</div>

    <style type="text/css">
        #sortable .list-group-item {
            width: 100%;
            border: 0;
            border-bottom: 1px solid #ffffff;
            background: #62DCBF;
            color: #fff;
            padding: 10px;
            text-align: center;
            margin-bottom: 1px;
        }

        .blur-div {
            opacity: 0.3;
        }

        .no-blur-div {
        }

        .text {
            color: white !important;
            cursor: pointer;
        }

        .csimpact p, .csdesc p, .fsdesc p, .fsimpact p {
            font-size: 11px;
        }

        .csimpactcontent, .csdesccontent, .fsdesccontent, .fsimpactcontent {
            cursor: pointer !important;
        }
    </style>
    <script type="text/javascript">
        function priorityDialog(msg) {
            $('#priorityModal').modal('show');
            $("#lblPriorityDesc").html(msg);
            return false;
        }
        var currentPriority = 1;
        var displayQuestionTypeId = 3;
        var valueMapStatus = "new";
        var impactAnswer = "";
        // var p1
        function BindPriorityName() {
            //  alertDialog($("#hidPriorityName").val());
            $(document).find(".priority-title").each(function () {

                $(this).html($("#hidPriorityName").val());
            })
            $(document).find("h1 .priority-title").each(function () {
                $(this).html($("#hidPriorityName").val());
            })
            $(document).find("p .priority-title").each(function () {
                $(this).html($("#hidPriorityName").val());
            })
        };
        $(function () {
            $("#sortable").sortable();
            $(".priorities").find("button").each(function () {
                $(this).click(function () {

                    $(this).toggleClass("active");
                    var toggleCount = $(".priorities").find(".active").length;

                    // alertDialog(toggleCount);
                    if (toggleCount > 3) {
                        $(this).removeClass("active");
                        $(this).attr("rank", "-1");
                        alertDialog("You can select 3 items. Please unselect one to choose another.");

                        //$(this).toggleClass("active");
                        return false;
                        //$(".priorities").find(".active:eq(0)").removeClass("active");
                    }
                    if ($(this).attr("class").indexOf("active") > -1) {
                        $(this).attr("rank", toggleCount);
                    }
                    else {
                        var currentRank = parseInt($(this).attr("rank"));
                        if (currentRank == -1 || currentRank == 3) {
                        }
                        else {
                            $(".priorities").find("button.active").each(function () {
                                var activeRank = parseInt($(this).attr("rank"));
                                if (activeRank > currentRank) {
                                    activeRank = activeRank - 1;
                                    $(this).attr("rank", activeRank);
                                }
                            });
                        }
                        $(this).attr("rank", "-1");
                    }
                })
            })
            $("#btnPreviousPriority").on("click", function () {
                var url = window.location.href;
                // window.location.href = "Overview.aspx?cid=" + $("#hidCustId").val();
                window.location.href = url;
                return false;
            });
            $("#btnNextPriority").on("click", function () {
                var priority = "";
                var priorityIds = "";
                var activeCount = $(".priorities").find("button.active").length;
                if (activeCount < 3) {
                    alertDialog("Please select 3 priorities");
                    return false;
                }
                var i = 1;
                $("#lblItem1").text($(".priorities").find("button.active[rank$='1']").text()).attr("priorityid", $(".priorities").find("button.active[rank$='1']").attr("id"));
                $("#lblItem2").text($(".priorities").find("button.active[rank$='2']").text()).attr("priorityid", $(".priorities").find("button.active[rank$='2']").attr("id"));
                $("#lblItem3").text($(".priorities").find("button.active[rank$='3']").text()).attr("priorityid", $(".priorities").find("button.active[rank$='3']").attr("id"));
                // return false;
                //$(".priorities").find("button.active").each(function () {
                //    $("#lblItem" + i).text($(this).text());
                //    $("#lblItem" + i).attr("priorityid", $(this).attr("id"))
                //    priorityIds = priorityIds + $(this).attr("id") + "~";
                //    priority += $(this).attr("id") + "~" + $(this).text() + "!";
                //    i = i + 1;
                //})
                $("#sortable").find("li").each(function () {

                    priorityIds = priorityIds + $(this).find("span").attr("priorityid") + "~";
                    priority += $(this).find("span").attr("priorityid") + "~" + $(this).find("span").text() + "!";
                    i = i + 1;
                })
                // alert(priorityIds);
                //alert(priority);
                var defaultPriorities = $("#hidSelectedPriorities").val();
                var selectedPriorities = priorityIds;
                if (defaultPriorities == "") {
                }
                else {
                    if (defaultPriorities != selectedPriorities) {
                        //if (confirm("Are you sure you want to remove All of your current Value Map selections and start from the beginning?")) {
                        //    DeleteAllCustomerPriority();
                        //    $("#hidSelectedPriorities").val("");
                        //    // alertDialog("hi");
                        //}
                        //else {
                        //    return false;
                        //}
                    }

                }
                //return false;
                $("#hidPriority").val(priority);
                $("#divPriority").css("display", "none");
                InsertCustomerPriorityChoices();
                $("#divRank").show();
                //  alertDialog(priority);
                return false;
            })
            $("#btnPreviousRank").click(function () {
                $("#divPriority").css("display", "");
                $("#divRank").hide();
                return false;
            })
            $("#btnNextRank").click(function () {
                // var i = 1;
                var priority = "";

                $("#sortable").find("li").each(function () {
                    priority += $(this).find("span").attr("priorityid") + "~" + $(this).find("span").text() + "!";

                });
                //for (i = 1; i < 4; i++) {
                //    //$("#lblItem"+i).text($(this).text());
                //   // $("#lblItem"+i).attr("priorityid",$(this).attr("id"))
                //    priority += $("#lblItem"+i).attr("priorityid") + "~" + $("#lblItem"+i).text() + "!";
                //}

                // alertDialog(priority);
               var priorityId = $("#sortable").find("li:eq("+(currentPriority-1)+")").find("span").attr("priorityid");
                var priorityName = $("#sortable").find("li:eq("+(currentPriority-1)+")").find("span").text();
                $("#hidPriorityId").val(priorityId);
                $("#hidPriorityName").val(priorityName);

                $("#hidPriority").val(priority);
                //  BindPriorityName();
               // UpdateCustomerPriorityChoiceRank();
               // $("#divCSFuture").show();
                if (currentPriority > 1) {
                    //  $("#btnPreviousFuture").attr("disabled", "disabled");
                }
                else {
                    //  $("#btnPreviousFuture").removeAttr("disabled");
                }
                $("#divRank").hide();
                 if (valueMapStatus == "new") {
                    priorityDialog("Begin Priority 1 of 3: " + priorityName)
                     UpdateCustomerPriorityChoiceRank();
                $("#divCSFuture").show();
                    $("#divCSFuture").find(".priority-title").html($("#hidPriorityName").val());
                }
                else {
                    BindPriorityName();
                    if (displayQuestionTypeId == "4") {
                        BindAnswer(3, "lblBenefit");
                        setTimeout(function () {  $("#divCSBenefit").show();
                        GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                        BindPriorityName(); }, 1000);
                    }
                    else if (displayQuestionTypeId == "3") {
                       GetCustomerFuture();
                       BindPriorityName();
                        $("#divCSFuture").show();
                        //$("#divCSFuture").find(".priority-title").html($("#hidPriorityName").val());
                    }
                    else if (displayQuestionTypeId == "2") {
                        GetCSCurrent($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                        $("#divCSCurrent").show();
                        $("#divCSFuture").hide();
                        $("#divCSCurrent").find(".priority-title").html($("#hidPriorityName").val());
                        $("#divCSCurrentContent,#divCSFutureContent").removeClass("blur-div");
                        GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                    }
                    else if (displayQuestionTypeId == "1") {
                        BindImpactAnswer(2, "lblImpact");
                       
                       
                    }
                }
               // priorityDialog("Begin Priority 1 of 3: " + priorityName )
                //$("#divCSFuture").find(".priority-title").html($("#hidPriorityName").val());

                return false;
            })
        })
        $(function () {
            $("#btnPreviousFuture").click(function () {
                if (currentPriority == 1) {
                    $("#divCSFuture").hide();
                    $("#divRank").show();
                }
                else {
                    $("#divFuture,#divCSFutureChoice,#divCSCurrentChoice,#divImpact,#divBenefit").html("");
                    currentPriority = currentPriority - 1;
                    $("#divCSBenefit").show();
                    $("#divCSFuture").hide();
                    if (currentPriority == 1) {
                        priorityId = $("#sortable").find("li").first().find("span").attr("priorityid");
                        priorityName = $("#sortable").find("li").first().find("span").text();
                    }
                    else if (currentPriority == 2) {
                        priorityId = $("#sortable").find("li").first().next().find("span").attr("priorityid");
                        priorityName = $("#sortable").find("li").first().next().find("span").text();
                    }
                    else if (currentPriority == 3) {
                        priorityId = $("#sortable").find("li").first().next().next().find("span").attr("priorityid");
                        priorityName = $("#sortable").find("li").first().next().next().find("span").text();
                    }
                    $("#hidPriorityId").val(priorityId);
                    $("#hidPriorityName").val(priorityName);
                    
                    GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);

                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                     BindAnswer(3, "lblBenefit");
                    // $("#divCSFuture").hide();
                    // $("#divCSBenefit").hide();
                    BindPriorityName();
                    GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);

                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                    // $("#divCSFuture").hide();
                    // $("#divCSBenefit").hide();
                    BindPriorityName();
                }
                return false;
            })
            $("#divFuture").find(".btn-default").on("click", function () {
                //  $(this).on("click",function () {
                //  alertDialog("hi");
                $(this).toggleClass("active");
                var toggleCount = $("#divFuture").find(".active").length;
                //  alertDialog(toggleCount);

                if (toggleCount > 4) {
                    $(this).removeClass("active");
                    alertDialog("You can select 4 items. Please unselect one to choose another.");
                    return false;

                    //  $("#divFuture").find(".active:eq(0)").removeClass("active");
                }

            })
            $("#btnNextFuture").click(function () {
                // var i = 1;
                var activeLength = $("#divFuture").find(".active").length;
                var answer = "";
                $("#divFuture").find(".active").each(function () {
                    answer += $(this).text() + "!";
                })
                //alertDialog(activeLength);
                if (activeLength == 0) {
                    alertDialog("Please select at least one choice.");
                    return false;
                }
                InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 3, answer,-1)
                GetCSCurrent($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                $("#divCSCurrent").show();
                $("#divCSFuture").hide();
                $("#divCSCurrent").find(".priority-title").html($("#hidPriorityName").val());
                $("#divCSCurrentContent,#divCSFutureContent").removeClass("blur-div");
               // GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                //BindPriorityName();
                return false;
            })
        });
        $(function () {
            $("#btnPreviousCurrent").click(function () {
                $("#divCSCurrent").hide();
                $("#divCSFuture").show();
                //  UpdateCustomerPriorityChoiceRank();
                GetCSIdeal();
                return false;
            });
            $("#divCSFutureChoice").find(".btn-default").on('click', function () {

                $(this).toggleClass("active");
                var toggleCount = $("#divCSFutureChoice").find(".active").length;
                //alertDialog(toggleCount);
                if (toggleCount == 0) {
                    $("#divCSCurrentContent").removeClass("blur-div");
                    $("#divCSCurrentChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                }
                else {
                    $("#divCSCurrentContent").addClass("blur-div");
                    $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                }
                if (toggleCount > 4) {
                    $(this).removeClass("active");
                    alertDialog("You can select 4 items. Please unselect one to choose another.");
                    return false;
                    //   $("#divCSFutureChoice").find(".active:eq(0)").removeClass("active");
                }



            })

            $("#divCSCurrentChoice").find(".btn-default").on('click', function () {

                $(this).toggleClass("active");
                var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                //  alertDialog(toggleCount);
                if (toggleCount == 0) {
                    $("#divCSFutureContent").removeClass("blur-div");
                    $("#divCSFutureChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                }
                else {
                    $("#divCSFutureContent").addClass("blur-div");
                    $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                }
                if (toggleCount > 4) {
                    $(this).removeClass("active");
                    //  $("#divCSCurrentChoice").find(".active:eq(0)").removeClass("active");
                    alertDialog("You can select 4 items. Please unselect one to choose another.");
                    return false;

                }
            })
            $("#btnNextCurrent").on('click', function () {
                var currentClass = $("#divCSCurrentContent").attr("Class");
                //   var futureClass = $("#divCSCurrentnt").attr("Class");
                // alertDialog(currentClass.indexOf("blur-div"));
                // return false;
                if (currentClass.indexOf("blur-div") > -1) {
                    var toggleCount = $("#divCSFutureChoice").find(".active").length;
                    if (toggleCount == 0) {
                        alertDialog("Please select at least one choice.");
                        return false;
                    }
                    var answer = "";
                    $("#divCSFutureChoice").find(".active").each(function () {
                        answer += $.trim($(this).text()) + "!";
                    })
                    $("#divCSCurrent").hide();
                    $("#divCSImpact").show();
                    InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 2, answer,1)
                    answer = answer.replaceAll("!", ",");
                    answer = answer.substr(0, answer.length - 1);
                    answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    answer = answer.replaceAll(",", ", ");
                    //alert(answer);
                    $(".priorityimpacttitle").html("");
                   // $("#lblImpact").text(answer);
                    impactAnswer = answer;
                    GetCustomerPriorityImpact($("#hidCustId").val(), $("#hidPriorityId").val(), 1)
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                    BindPriorityName();


                    return false;
                }
                else {
                    var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                    if (toggleCount == 0) {
                        alertDialog("Please select at least one choice.");
                        return false;
                    }
                    var answer = "";
                    $("#divCSCurrentChoice").find(".active").each(function () {
                        answer += $.trim($(this).text()) + "!";
                    })
                    $("#divCSCurrent").hide();
                    $("#divCSImpact").show();
                    InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 2, answer,-1)
                    answer = answer.replaceAll("!", ",");
                    answer = answer.substr(0, answer.length - 1);
                    answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    answer = answer.replaceAll(",", ", ");
                    $(".priorityimpacttitle").html("");
                   // $("#lblImpact").text(answer);
                    impactAnswer = answer;
                    GetCustomerPriorityImpact($("#hidCustId").val(), $("#hidPriorityId").val(), 1)
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                    BindPriorityName();

                    // alert(answer);


                    return false;
                }
                return false;
            })

        })
        String.prototype.replaceAll = function (search, replacement) {
            var target = this;
            return target.replace(new RegExp(search, 'g'), replacement);
        };
        $(function () {
            $("#btnPreviousImpact").click(function () {
                $("#divCSCurrent").show();
                $("#divCSImpact").hide();
                GetCSCurrent($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                return false;
            });
            $("#divImpact").find(".btn-default").on("click", function () {
                //  $(this).on("click",function () {
                //  alertDialog("hi");
                $(this).toggleClass("active");
                var toggleCount = $("#divImpact").find(".active").length;
                //  alertDialog(toggleCount);

                if (toggleCount > 4) {
                    $(this).removeClass("active");
                    alertDialog("You can select 4 items. Please unselect one to choose another.");
                    return false;
                    //  $("#divImpact").find(".active:eq(0)").removeClass("active");
                }

            })
            $("#btnNextImpact").click(function () {
                // var i = 1;
                var activeLength = $("#divImpact").find(".active").length;
                var answer = "";
                $("#divImpact").find(".active").each(function () {
                    answer += $.trim($(this).text()) + "!";
                })
                //alertDialog(activeLength);
                if (activeLength == 0) {
                    alertDialog("Please select at least one choice.");
                    return false;
                }
                InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 1, answer,-1)
                BindValueMapImpactDialogContent(answer);
                // return false;

                GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);

                BindPriorityName();
                BindAnswer(3, "lblBenefit");
                //answer = answer.replaceAll("!", ",");
                //answer = answer.substr(0, answer.length - 1);
                //answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                //answer = answer.replaceAll(",", ", ");
                //$("#lblBenefit").text(answer);
                return false;
            })
        });
        function BindAnswer(questionTypeId, lblId) {
            var dataValue = '{customerID: "' + $("#hidCustId").val() + '", priorityID: "' + $("#hidPriorityId").val() + '",questionType: "' + questionTypeId + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetValueMapChoicesByQuestionId",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var answer = msg.d;
                    answer = answer.replaceAll("!", ",");
                    answer = answer.substr(0, answer.length - 1);
                    answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    answer = answer.replaceAll(",", ", ");
                    $("#" + lblId).text(answer);
                    //alert(answer);
                    return false;
                },
                error: function () { }
            });
        }
        function BindImpactAnswer(questionTypeId, lblId) {
             var dataValue = '{customerID: "' + $("#hidCustId").val() + '", priorityID: "' + $("#hidPriorityId").val() + '",questionType: "' + questionTypeId + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetValueMapChoicesByQuestionId",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success:BindImpactData,
                //success: function (msg) {
                   
                //},
                error: function () { }
            });
        }
        function BindImpactData(msg) {
             var answer = msg.d;
                    answer = answer.replaceAll("!", ",");
                    answer = answer.substr(0, answer.length - 1);
                    answer = answer.replace(/,(?=[^,]*$)/, ' and ');
                    answer = answer.replaceAll(",", ", ");
                    $("#lblImpact").text(answer);
             setTimeout(function () {
                 impactAnswer = answer;
                // alert(impactAnswer);
                        $("#divCSCurrent").hide();
                    $("#divCSImpact").show();
                    GetCustomerPriorityImpact($("#hidCustId").val(), $("#hidPriorityId").val(), 1)
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                    BindPriorityName();
                        },1000)
                    return false;
        }
        $(function () {
            $("#btnPreviousBenefit").click(function () {
                $("#divCSBenefit").hide();
                $("#divCSImpact").show();
                 BindAnswer(2, "lblImpact");
                setTimeout(function () {
                    
                    impactAnswer = $("#lblImpact").text();
                   //alert(impactAnswer);
                     GetCustomerPriorityImpact($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                }, 1000)
                return false;
            });
            $("#divBenefit").find(".btn-default").on("click", function () {
                //  $(this).on("click",function () {
                //  alertDialog("hi");
                $(this).toggleClass("active");
                var toggleCount = $("#divBenefit").find(".active").length;
                //  alertDialog(toggleCount);

                if (toggleCount > 4) {
                    $(this).removeClass("active");
                    alertDialog("You can select 4 items. Please unselect one to choose another.");

                    return false;
                    // $("#divBenefit").find(".active:eq(0)").removeClass("active");
                }

            })
            $("#btnNextBenefit").click(function () {
                // var i = 1;
                var activeLength = $("#divBenefit").find(".active").length;
                var answer = "";
                $("#divBenefit").find(".active").each(function () {
                    answer += $(this).text() + "!";
                })
                //alertDialog(activeLength);
                if (activeLength == 0) {
                    alertDialog("Please select at least one choice.");
                    return false;
                }

                InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 4, answer,-1)
                var customerPriority = $("#hidCustomerPriority").val();
                //if (customerPriority == "old") {
                //    //window.location.reload();
                //      var URL = window.location.href;
                //         window.location.href = URL;
                //}
                //else {

                // alertDialog(currentPriority)
                BindValueMapBenefitDialogContent(answer);
                $("#divFuture,#divCSFutureChoice,#divCSCurrentChoice,#divImpact,#divBenefit").html("");
                return false;
                currentPriority += 1;
                var priorityId;
                var priorityName;
                if (currentPriority > 3) {
                    //window.location.reload();
                    //var URL = window.location.href;
                    //window.location.href = URL;
                    $("#btnRealtorEmail").trigger("click");
                }
                $("#divFuture,#divCSFutureChoice,#divCSCurrentChoice,#divImpact,#divBenefit").html("");
                if (currentPriority == 2) {
                    priorityId = $("#sortable").find("li").first().next().find("span").attr("priorityid");
                    priorityName = $("#sortable").find("li").first().next().find("span").text();
                    priorityDialog("Begin Priority 2 of 3: " + priorityName )
                }
                else {
                    priorityId = $("#sortable").find("li").first().next().next().find("span").attr("priorityid");
                    priorityName = $("#sortable").find("li").first().next().next().find("span").text();
                    priorityDialog("Begin Priority 3 of 3: " + priorityName )
                }
                //  alertDialog(priorityId);
                //  alertDialog(priorityName);
                $("#hidPriorityId").val(priorityId);
                $("#hidPriorityName").val(priorityName);

                $("#divCSFuture").show();
                $("#divCSBenefit").hide();
                if (currentPriority > 1) {
                    //$("#btnPreviousFuture").attr("disabled", "disabled");
                }
                else {
                    //  $("#btnPreviousFuture").removeAttr("disabled");
                }
                GetCustomerFuture();
                BindPriorityName();
                return false;
                // }
                //$("#divCSBenefit").show();
                //    $("#divCSImpact").hide();
                //    GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                return false;
            })
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $(".prior").click(function () {
                var divId = $(this).attr("id");
                var lblId = divId.replace("div", "lbl");
                //alertDialog($('#' + lblId).attr("priorityid"));
            })
        })
    </script>
    <script type="text/javascript">

        function InsertCustomerPriorityChoices() {
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", Priority: "' + $("#hidPriority").val() + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/InsertCustomerPriorityChoices",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    //alertDialog(msg.d);
                    if (msg.d == "success") {

                    }
                    else {

                    }
                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }

       

         function UpdateCustomerPriorityChoiceRank() {
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", Priority: "' + $("#hidPriority").val() + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/UpdateCustomerPriorityChoiceRank",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divFuture").html("").html(msg.d);
                    ToggleDivHover("divFuture");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 3);
                    //setTimeout(function () { GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 3); }, 2000);
                    maxHeightPriorityButtons("divFuture");
                    //maxWidthPriorityButtons("divFuture");
                    $("#divFuture").find(".btn-default").off("click").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                       // alert($(this).text().toLowerCase());
                        var btnText = $(this).text().toLowerCase();
                         var toggleCount = $("#divFuture").find(".active").length;
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog("divFuture");
                            }
                        }
                        else {
                           
                            $(this).toggleClass("active");
                        }

                       
                        //  alertDialog(toggleCount);
                        toggleCount = $("#divFuture").find(".active").length;
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divFuture").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                        }
                        
                        

                        // })
                    })
                    FutureButtonClick();
                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }
        function FutureButtonClick() {
            $("#divFuture").find(".btn-default").off("click").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                       // alert($(this).text().toLowerCase());
                        var btnText = $(this).text().toLowerCase();
                         var toggleCount = $("#divFuture").find(".active").length;
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog("divFuture");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                        }

                       
                        //  alertDialog(toggleCount);
                toggleCount = $("#divFuture").find(".active").length;
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divFuture").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                        }
                        
                        

                        // })
                    })
        }
         function FutureCSButtonClick() {
            $("#divCSFutureChoice").find(".btn-default").off("click").on('click', function () {

                       
                       
                        //alertDialog(toggleCount);
                         var btnText = $(this).text().toLowerCase();
                        if (btnText == "other") {
                            $(this).removeClass("active");
                             var toggleCount = $("#divCSFutureChoice").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                 $("#divCSCurrentContent").addClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSFutureChoice");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divCSFutureChoice").find(".active").length;
                            if (toggleCount == 0) {
                                $("#divCSCurrentContent").removeClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSCurrentContent").addClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                                //   $("#divCSFutureChoice").find(".active:eq(0)").removeClass("active");
                            }
                        }



                    })

                  
        }
         function FutureCSEditButtonClick() {
            $("#divCSFutureChoiceEdit").find(".btn-default").off("click").on('click', function () {

                       
                       
                        //alertDialog(toggleCount);
                         var btnText = $(this).text().toLowerCase();
                        if (btnText == "other") {
                            $(this).removeClass("active");
                             var toggleCount = $("#divCSFutureChoiceEdit").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                 $("#divCSCurrentContentEdit").addClass("blur-div");
                                $("#divCSCurrentChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSFutureChoiceEdit");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divCSFutureChoiceEdit").find(".active").length;
                            if (toggleCount == 0) {
                                $("#divCSCurrentContentEdit").removeClass("blur-div");
                                $("#divCSCurrentChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSCurrentContentEdit").addClass("blur-div");
                                $("#divCSCurrentChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                                //   $("#divCSFutureChoice").find(".active:eq(0)").removeClass("active");
                            }
                        }



                    })

                  
        }
         function CurrentCSButtonClick() {
             $("#divCSCurrentChoice").find(".btn-default").off("click").on('click', function () {
                  
                         var btnText = $(this).text().toLowerCase();
                 if (btnText == "other") {
                            
                     $(this).removeClass("active");
                     var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                 $("#divCSFutureContent").addClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSCurrentChoice");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                            //  alertDialog(toggleCount);
                            if (toggleCount == 0) {
                                $("#divCSFutureContent").removeClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSFutureContent").addClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                //  $("#divCSCurrentChoice").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                            }
                        }
                    })
        }
        function CurrentCSEditButtonClick() {
             $("#divCSCurrentChoiceEdit").find(".btn-default").off("click").on('click', function () {
                  
                         var btnText = $(this).text().toLowerCase();
                 if (btnText == "other") {
                            
                     $(this).removeClass("active");
                     var toggleCount = $("#divCSCurrentChoiceEdit").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                 $("#divCSFutureContenEditt").addClass("blur-div");
                                $("#divCSFutureChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSCurrentChoiceEdit");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divCSCurrentChoiceEdit").find(".active").length;
                            //  alertDialog(toggleCount);
                            if (toggleCount == 0) {
                                $("#divCSFutureContentEdit").removeClass("blur-div");
                                $("#divCSFutureChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSFutureContentEdit").addClass("blur-div");
                                $("#divCSFutureChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                //  $("#divCSCurrentChoice").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                            }
                        }
                    })
        }
        function BenefitButtonClick() {
            $("#divBenefit").find(".btn-default").off("click").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                       // alert($(this).text().toLowerCase());
                        var btnText = $(this).text().toLowerCase();
                         var toggleCount = $("#divBenefit").find(".active").length;
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog("divBenefit");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                        }

                       
                        //  alertDialog(toggleCount);
                toggleCount = $("#divBenefit").find(".active").length;
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divFuture").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                        }
                        
                        

                        // })
                    })
        }
        function ImpactButtonClick() {
            $("#divImpact").find(".btn-default").off("click").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                       // alert($(this).text().toLowerCase());
                        var btnText = $(this).text().toLowerCase();
                         var toggleCount = $("#divImpact").find(".active").length;
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog("divImpact");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                        }

                       toggleCount = $("#divImpact").find(".active").length;
                        //  alertDialog(toggleCount);

                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divFuture").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                        }
                        
                        

                        // })
                    })
        }
         function FutureEditButtonClick(type) {
            $("#divEditContent").find(".btn-default").off("click").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                       // alert($(this).text().toLowerCase());
                        var btnText = $(this).text().toLowerCase();
                         var toggleCount = $("#divEditContent").find(".active").length;
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog(type);
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                        }

                       
                        //  alertDialog(toggleCount);
                toggleCount = $("#divEditContent").find(".active").length;
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divFuture").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                        }
                        
                        

                        // })
                    })
        }
        function ShowOtherItemDialog(divId) {
            $("#otherItemModal").modal("show");
            $("#hidOther").val(divId);
            $("#txtOther").val("");
            setTimeout(function () {$("#txtOther").focus(); }, 1000);
             
            //$("#btnOtherItemOk").addClass("disabled");

        }
        function AddOtherItem() {
            var divId = $("#hidOther").val();
            $("#otherItemModal").modal("hide");
           
            if (divId == "divFuture") {
                var minHeight = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Height");
                var minWidth = $("#" + $("#hidOther").val()).find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                 $("#" + $("#hidOther").val()).find(".btns").last().before("<div class='col-md-3 btns' style='min-height:"+minHeight+";' ><button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> </div>");
                FutureButtonClick();
            }
            if (divId == "divCSFutureChoice" || divId=="divCSCurrentChoice") {
                var btnCount = $("#" + divId).find(".btn-default").length-1;
                if (btnCount % 2 == 0) {
                 //   alert("hi");
                    $("#" + $("#hidOther").val()).find(".btns").last().css("min-width","100%").css("max-width","100%")
                    $("#" + $("#hidOther").val()).find(".btns").last().prepend("<button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> ");
                }
                else {
                  //  alert("hi1");
                    var minHeight = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Height");
                    var minWidth = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Width");
                  //  $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").last().remove();
                    //$("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().next().remove();
                    var firstBtn = $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().html();
                    $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().next().remove();
                  //  var nextBtn=
                    $("#" + $("#hidOther").val()).find(".btns").last().append("<button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> ");
                     $("#" + $("#hidOther").val()).find(".btns").last().after("<div class='btns' style='min-Height:"+minHeight+"; min-Width:50%;max-Width:50%'><button type='button' class='btn-default' id='" + "Other" + "'>" + "Other" + "</button> </div>");
                }
                if (divId == "divCSFutureChoice") {
                    FutureCSButtonClick();
                }
                else {CurrentCSButtonClick();}
            }
            if (divId == "divBenefit") {
                 var minHeight = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Height");
                var minWidth = $("#" + $("#hidOther").val()).find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                $("#" + $("#hidOther").val()).find(".btns").last().before("<div class='col-md-3 btns' style='min-height:"+minHeight+";min-width:"+minWidth+"'><button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> </div>");
                BenefitButtonClick();
            }
             if (divId == "divImpact") {
                 var minHeight = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Height");
                var minWidth = $("#" + $("#hidOther").val()).find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                $("#" + $("#hidOther").val()).find(".btns").last().before("<div class='col-md-3 btns' style='min-height:"+minHeight+";min-width:"+minWidth+"'><button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> </div>");
                ImpactButtonClick();
            }
            if (divId == "divEditModalContentBenefit") {
                var minHeight = $("#divEditModalContent" ).find(".btns").first().css("min-Height");
                var minWidth = $("#divEditModalContent").find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                 $("#divEditModalContent").find(".btns").last().before("<div class='col-md-4 btns' style='min-height:"+minHeight+";' ><button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> </div>");
                FutureEditButtonClick("divEditModalContentBenefit");
            }
              if (divId == "divEditModalContentFuture") {
                var minHeight = $("#divEditModalContent" ).find(".btns").first().css("min-Height");
                var minWidth = $("#divEditModalContent" ).find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                 $("#divEditModalContent").find(".btns").last().before("<div class='col-md-3 btns' style='min-height:"+minHeight+";' ><button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> </div>");
                FutureEditButtonClick("divEditModalContentFuture");
            }
             if (divId == "divCSFutureChoiceEdit" || divId=="divCSCurrentChoiceEdit") {
                var btnCount = $("#" + divId).find(".btn-default").length-1;
                if (btnCount % 2 == 0) {
                 //   alert("hi");
                    $("#" + $("#hidOther").val()).find(".btns").last().css("min-width","100%").css("max-width","100%")
                    $("#" + $("#hidOther").val()).find(".btns").last().prepend("<button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> ");
                }
                else {
                  //  alert("hi1");
                    var minHeight = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Height");
                    var minWidth = $("#" + $("#hidOther").val()).find(".btns").first().css("min-Width");
                  //  $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").last().remove();
                    //$("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().next().remove();
                    var firstBtn = $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().html();
                    $("#" + $("#hidOther").val()).find(".btns").last().find(".btn-default").first().next().remove();
                  //  var nextBtn=
                    $("#" + $("#hidOther").val()).find(".btns").last().append("<button type='button' class='btn-default active' id='" + $("#txtOther").val() + "'>" + $("#txtOther").val() + "</button> ");
                     $("#" + $("#hidOther").val()).find(".btns").last().after("<div class='btns' style='min-Height:"+minHeight+"; min-Width:50%;max-Width:50%'><button type='button' class='btn-default' id='" + "Other" + "'>" + "Other" + "</button> </div>");
                }
                if (divId == "divCSFutureChoiceEdit") {
                    FutureCSEditButtonClick();
                }
                else {CurrentCSEditButtonClick();}
            }
        }
        function GetCSIdeal() {
            var dataValue = '{customerID: "' + $("#hidCustId").val() + '", priorityID: "' + $("#hidPriorityId").val() + '",questionType: "' + 3 + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityIdeal",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    $("#divFuture").html("").html(msg.d);
                    ToggleDivHover("divFuture");
                    maxHeightPriorityButtons("divFuture");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 3);
                    $("#divFuture").find(".btn-default").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                        $(this).toggleClass("active");
                        var toggleCount = $("#divFuture").find(".active").length;
                        //  alertDialog(toggleCount);

                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");
                            return false;
                            //  $("#divImpact").find(".active:eq(0)").removeClass("active");
                        }

                    })
                    //alertDialog(msg.d);

                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }

 function InsertCustomerPriorityChoiceDetails(custId, priorityId, questionType, answer,wwdc) {

            var dataValue = '{CustId: "' + custId + '", PriorityId: "' + priorityId + '",QuestionTypeId: "' + questionType + '", Answer: "' + encodeURIComponent(answer) + '", WWDC: "' + wwdc + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/InsertCustomerPriorityChoiceDetails",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    //alertDialog(msg.d);
                    if (msg.d == "success") {

                    }
                    else {

                    }
                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }
        function GetCSCurrent(custId, priorityId, questionType) {
            var dataValue = '{CustId: "' + custId + '", PriorityId: "' + priorityId + '",QuestionTypeId: "' + questionType + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityCurrent",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    $("#divCSFutureChoice").html("").html(msg.d.split("~")[1]);
                    $("#divCSCurrentChoice").html("").html(msg.d.split("~")[0]);
                    var showcurrentorfeature = msg.d.split("~")[2];
                    $("#divCSFutureChoice").attr("data-wwdc",showcurrentorfeature)
                    ToggleDivHover("divCSFutureChoice");
                    ToggleDivHover("divCSCurrentChoice");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 2);
                    //maxHeightPriorityButtons("divCSFutureChoice");
                    //maxHeightPriorityButtons("divCSCurrentChoice");
                    //maxWidthPriorityButtons("divCSFutureChoice");
                    //maxWidthPriorityButtons("divCSCurrentChoice");
                    maxHeightPriorityButtonsNew("divCSFutureChoice", "divCSCurrentChoice");
                    //maxWidthPriorityButtonsNew("divCSFutureChoice", "divCSCurrentChoice")
                    var totalfsLength = $("#divCSFutureChoice").find(".btns").last().find(".btn-default").length;
                    if (totalfsLength == 1) {
                        $("#divCSFutureChoice").find(".btns").last().css("max-width", "50%").css("min-width", "50%");
                    }
                    var totalcsLength = $("#divCSCurrentChoice").find(".btns").last().find(".btn-default").length;
                    if (totalcsLength == 1) {
                        $("#divCSCurrentChoice").find(".btns").last().css("max-width", "50%").css("min-width", "50%");
                    }
                    
                    $("#divCSFutureChoice").find(".btn-default").on('click', function () {

                       
                       
                        //alertDialog(toggleCount);
                         var btnText = $(this).text().toLowerCase();
                        if (btnText == "other") {
                            $(this).removeClass("active");
                             var toggleCount = $("#divCSFutureChoice").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                 $("#divCSCurrentContent").addClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSFutureChoice");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                             var toggleCount = $("#divCSFutureChoice").find(".active").length;
                            if (toggleCount == 0) {
                                $("#divCSCurrentContent").removeClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSCurrentContent").addClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                                //   $("#divCSFutureChoice").find(".active:eq(0)").removeClass("active");
                            }
                        }



                    })

                    $("#divCSCurrentChoice").find(".btn-default").on('click', function () {
                         var btnText = $(this).text().toLowerCase();
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                  $("#divCSFutureContent").addClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                ShowOtherItemDialog("divCSCurrentChoice");
                            }
                        }
                        else {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divCSCurrentChoice").find(".active").length;
                            //  alertDialog(toggleCount);
                            if (toggleCount == 0) {
                                $("#divCSFutureContent").removeClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").removeAttr("disabled").removeClass("active");
                            }
                            else {
                                $("#divCSFutureContent").addClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                            }
                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                //  $("#divCSCurrentChoice").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");
                                return false;
                            }
                        }
                    })
                    //alertDialog(msg.d);

                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }
        function GetCustomerPriorityImpact(custId, priorityId, questionType) {
            var dataValue = '{CustId: "' + custId + '", PriorityId: "' + priorityId + '",QuestionTypeId: "' + questionType + '"}';
            $("#divImpact").html("");
            setTimeout(function () { BindCustomerPriorityImpact(dataValue); }, 1000);
        }
        function BindCustomerPriorityImpact(dataValue) {
            //alert("hi");
             $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityImpact",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                     $("#divImpact").html("").html(msg.d.split("~")[0]);
                    var wwdc = msg.d.split("~")[1];
                   // alert(wwdc);
                    ToggleDivHover("divImpact");
                    maxHeightPriorityButtons("divImpact");
                    maxWidthPriorityButtons("divImpact");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 1);
                    //if ($("#divImpact").find(".btns .btn-default").first().attr("future") == "1") {
                    if (wwdc == "1") {
                        // alert($("#divImpact").find(".btns .btn-default").first().attr("future"));
                        // var answer = $("#lblImpact").html();
                        $(".priorityimpacttitle").html("").html('What do you like about currently having "' + impactAnswer + '" what do these things allow you to do? <span style="font-size:13px!important">(select up to 4 answers)</span>');
                    }
                    else {
                        $(".priorityimpacttitle").html("").html('How does ' + impactAnswer + ' <b>IMPACT</b> your every day life and the way you live? <span style="font-size:13px!important">(select up to 4 answers)</span>');
                    }

                    $("#divImpact").find(".btn-default").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                        $(this).toggleClass("active");
                        var toggleCount = $("#divImpact").find(".active").length;
                        //  alertDialog(toggleCount);

                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");
                            return false;
                            //  $("#divImpact").find(".active:eq(0)").removeClass("active");
                        }

                    })
                    //alertDialog(msg.d);

                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }
        function GetCustomerPriorityBenefit(custId, priorityId, questionType) {
            var dataValue = '{CustId: "' + custId + '", PriorityId: "' + priorityId + '",QuestionTypeId: "' + questionType + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityBenefit",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    $("#divBenefit").html("").html(msg.d);
                    //alertDialog(msg.d);
                    ToggleDivHover("divBenefit");
                    maxHeightPriorityButtons("divBenefit");
                    maxWidthPriorityButtons("divBenefit");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                    $("#divBenefit").find(".btn-default").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                        var btnText = $(this).text().toLowerCase();
                        if (btnText == "other") {
                            $(this).removeClass("active");
                            var toggleCount = $("#divBenefit").find(".active").length;
                            if (toggleCount >= 4) {
                                $(this).removeClass("active");
                                // $("#divFuture").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                            else {
                                ShowOtherItemDialog("divBenefit");
                            }
                        }
                        else {
                        $(this).toggleClass("active");
                        var toggleCount = $("#divBenefit").find(".active").length;
                        //  alertDialog(toggleCount);

                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                            return false;
                            // $("#divBenefit").find(".active:eq(0)").removeClass("active");
                        }
                    }

                    })
                    //alertDialog("Success");
                    return false;
                },
                error: function () { }
            });
        }
        function GetCustomerFuture() {
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", PriorityId: "' + $("#hidPriorityId").val() + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerFuture",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divFuture").html("").html(msg.d);
                    ToggleDivHover("divFuture");
                    maxHeightPriorityButtons("divFuture");
                    maxWidthPriorityButtons("divFuture");
                    GetSelectedPriorityChoices($("#hidCustId").val(), $("#hidPriorityId").val(), 3);
                    $("#divFuture").find(".btn-default").on("click", function () {
                        //  $(this).on("click",function () {
                        //  alertDialog("hi");
                        $(this).toggleClass("active");
                        var toggleCount = $("#divFuture").find(".active").length;
                        //  alertDialog(toggleCount);

                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");
                            return false;
                            //  $("#divFuture").find(".active:eq(0)").removeClass("active");
                        }

                    })
                    return false;
                },
                error: function () { }
            });
        }
         function inArray(item,arr)
{
    var count=arr.length;
    for(var i=0;i<count;i++)
    {
        if(arr[i]===item){return true;}
    }
    return false;
}
        function GetSelectedPriorityChoices(custId, priorityId, questionType) {
            // alertDialog(custId);
            // alertDialog(priorityId);
            // alertDialog(questionType);

            var dataValue = '{customerID: "' + custId + '", priorityID: "' + priorityId + '",questionTypeID: "' + questionType + '"}';
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityChoices",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    var result = msg.d;
                    if (result == "") {
                    }
                    else {
                        var answers = result.split("~");
                        // alertDialog(answers);
                        if (questionType == 3) {
                             var btnArray = new Array();
                                 $("#divCSFuture").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                     btnArray.push(btnValue);
                            });
                            for (var i = 0; i < answers.length; i++) {
                                if (inArray(answers[i], btnArray) == false) {
                                   // alert("future");
                                    var minHeight =$("#divFuture").find(".btns").first().css("min-Height");
                var minWidth = $("#divFuture").find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                                     $("#divFuture").find(".btns").last().before("<div class='col-md-3 btns' style='min-height:"+minHeight+";'><button type='button' class='btn-default active' style='min-height:"+minHeight+";' id='" + answers[i] + "'>" + answers[i] + "</button> </div>");
                                }
                            }
                            for (var i = 0; i < answers.length; i++) {
                               
                                
                                $("#divCSFuture").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                    if (btnValue == $.trim(answers[i])) {
                                        $(this).addClass("active");
                                    }
                                });
                            }
                            FutureButtonClick();
                        }
                        if (questionType == 2) {
                            $("#divCSCurrentContent").removeClass("blur-div");
                            $("#divCSCurrentChoice").find(".btn-default").removeAttr("disabled");
                            $("#divCSFutureContent").removeClass("blur-div");
                            $("#divCSFutureChoice").find(".btn-default").removeAttr("disabled");
                            var btnArray = new Array();
                            var isFutureExist = true;
                                 $("#divCSFutureChoice").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                     btnArray.push(btnValue);
                            });
                            for (var i = 0; i < answers.length; i++) {
                                if (inArray(answers[i], btnArray) == false) {
                                    isFutureExist = false;
                                    // $("#divCSFutureChoice").find(".btns").last().before("<div class='col-md-3 btns'><button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> </div>");
                                }
                                else {
                                    isFutureExist = true;
                                    break;
                                }
                            }
                            var wwdc = $("#divCSFutureChoice").attr("data-wwdc");
                            try {
                                if (parseInt(wwdc) == 1) {
                                    isFutureExist = true;
                                }
                                else isFutureExist = false;
                            }
                            catch (ex) {
                                isFutureExist = false;
                            }
                            //alert(isFutureExist);
                            if (isFutureExist == true) {
                                for (var i = 0; i < answers.length; i++) {
                                    if (inArray(answers[i], btnArray) == false) {
                                        var btnCount = $("#divCSFutureChoice").find(".btn-default").length - 1;
                                        if (btnCount % 2 == 0) {
                                            //   alert("hi");
                                            $("#divCSFutureChoice").find(".btns").last().css("min-width", "100%").css("max-width", "100%")
                                            $("#divCSFutureChoice").find(".btns").last().prepend("<button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> ");
                                        }
                                        else {
                                            //  alert("hi1");
                                            var minHeight = $("#divCSFutureChoice").find(".btns").first().css("min-Height");
                                            var minWidth = $("#divCSFutureChoice").find(".btns").first().css("min-Width");

                                            var firstBtn = $("#divCSFutureChoice").find(".btns").last().find(".btn-default").first().html();
                                            $("#divCSFutureChoice").find(".btns").last().find(".btn-default").first().next().remove();
                                            //  var nextBtn=
                                            $("#divCSFutureChoice").find(".btns").last().append("<button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> ");
                                            $("#divCSFutureChoice").find(".btns").last().after("<div class='btns' style='min-Height:" + minHeight + "; min-Width:50%;max-Width:50%'><button type='button' class='btn-default' id='" + "Other" + "'>" + "Other" + "</button> </div>");
                                        }
                                         FutureCSButtonClick();
                                    }

                                }
                            }
                            else {
                                   btnArray = new Array();
                                 $("#divCSCurrentChoice").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                     btnArray.push(btnValue);
                            });
                                for (var i = 0; i < answers.length; i++) {
                                    if (inArray(answers[i], btnArray) == false) {
                                        var btnCount = $("#divCSCurrentChoice").find(".btn-default").length - 1;
                                        if (btnCount % 2 == 0) {
                                            //   alert("hi");
                                            $("#divCSCurrentChoice").find(".btns").last().css("min-width", "100%").css("max-width", "100%")
                                            $("#divCSCurrentChoice").find(".btns").last().prepend("<button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> ");
                                        }
                                        else {
                                            //  alert("hi1");
                                            var minHeight = $("#divCSCurrentChoice").find(".btns").first().css("min-Height");
                                            var minWidth = $("#divCSCurrentChoice").find(".btns").first().css("min-Width");
                                            var firstBtn = $("#divCSCurrentChoice").find(".btns").last().find(".btn-default").first().html();
                                            $("#divCSCurrentChoice").find(".btns").last().find(".btn-default").first().next().remove();
                                            //  var nextBtn=
                                            $("#divCSCurrentChoice").find(".btns").last().append("<button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> ");
                                            $("#divCSCurrentChoice").find(".btns").last().after("<div class='btns' style='min-Height:" + minHeight + "; min-Width:50%;max-Width:50%'><button type='button' class='btn-default' id='" + "Other" + "'>" + "Other" + "</button> </div>");
                                        }
                                        CurrentCSButtonClick();
                                    }

                                }
                            }
                            for (var i = 0; i < answers.length; i++) {
                                $("#divCSFutureChoice").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                    if (btnValue == $.trim(answers[i])) {
                                        $(this).addClass("active");
                                    }
                                });
                            }
                            var activeLength = $("#divCSFutureChoice").find(".active").length;
                            if (activeLength == 0) {
                                $("#divCSFutureContent").addClass("blur-div");
                                $("#divCSFutureChoice").find(".btn-default").attr("disabled", "disabled");
                                for (var i = 0; i < answers.length; i++) {
                                    $("#divCSCurrentChoice").find(".btn-default").each(function () {
                                        var btnValue = $.trim($(this).html());
                                        if (btnValue == $.trim(answers[i])) {
                                            $(this).addClass("active");
                                        }
                                    });
                                }
                            }
                            else {
                                $("#divCSCurrentContent").addClass("blur-div");
                                $("#divCSCurrentChoice").find(".btn-default").attr("disabled", "disabled");
                            }
                        }
                        if (questionType == 1) {
                               var btnArray = new Array();
                                 $("#divImpact").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                     btnArray.push(btnValue);
                            });
                            for (var i = 0; i < answers.length; i++) {
                                if (inArray(answers[i], btnArray) == false) {
                                    var minHeight = $("#divImpact").find(".btns").first().css("min-Height");
                var minWidth = $("#divImpact").find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                                     $("#divImpact").find(".btns").last().before("<div class='col-md-4 btns' ><button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> </div>");
                                }
                            }
                            for (var i = 0; i < answers.length; i++) {
                                $("#divImpact").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                    if (btnValue == $.trim(answers[i])) {
                                        $(this).addClass("active");
                                    }
                                });
                            }
                            ImpactButtonClick();
                        }
                        if (questionType == 4) {
                             var btnArray = new Array();
                                 $("#divBenefit").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                     btnArray.push(btnValue);
                            });
                            for (var i = 0; i < answers.length; i++) {
                                if (inArray(answers[i], btnArray) == false) {
                                    var minHeight = $("#divBenefit").find(".btns").first().css("min-Height");
                var minWidth = $("#divBenefit").find(".btns").first().outerWidth().toString().replace("px", "") + "px";
                                     $("#divBenefit").find(".btns").last().before("<div class='col-md-4 btns' ><button type='button' class='btn-default active' id='" + answers[i] + "'>" + answers[i] + "</button> </div>");
                                }
                            }
                            for (var i = 0; i < answers.length; i++) {
                                $("#divBenefit").find(".btn-default").each(function () {
                                    var btnValue = $.trim($(this).html());
                                    if (btnValue == $.trim(answers[i])) {
                                        $(this).addClass("active");
                                    }
                                });
                            }
                             BenefitButtonClick();
                        }
                    }

                    //alertDialog("Success");
                    //  return false;
                },
                error: function () { }
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        $(".fsdesccontent,.fsimpactcontent,.csimpactcontent,.csdesccontent").on("click", function () { $(this).find("div").first().trigger("click"); })
        $(".fsdesc").on("click", function () {

            if (TogglePriorityClick() == 1)
                return false;
            $("#btnVmReview").css("display", "none");
            var htmlContent = "<p>What words describe your <b>IDEAL </b> " + $(this).attr("priorityname") + "? <span style='font-size:13px!important'>(select up to 4 answers)</span></p>";
            $("#lblEditPriorityType").html(htmlContent);
            $("#lblEditPriorityTitle").text($(this).attr("priorityname"));
            // $('#editModal').modal('show');
            $("#hidPriorityId").val($(this).attr("priorityid"));
            $("#hidQuestionType").val("3");
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", PriorityId: "' + $(this).attr("priorityid") + '"}';
            var list = new Array();
            $(this).find("p").each(function () {
                list.push($(this).text());
            });
            //  alertDialog(list.length);
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerFutureEdit",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divEditModalContent").html("").html(msg.d);
                    $("#divEditModalContent,#divEditButtons,#divEditPriorityTitleHead").css("display", "");
                    $("#divPriorityOverView").css("display", "none");
                    ToggleDivHover("divEditModalContent");
                    maxHeightPriorityButtons("divEditModalContent");
                    //maxWidthPriorityButtons("divEditModalContent");
                    $("#divEditModalContent").find(".btn-default").each(function () {
                        for (var i = 0; i < list.length; i++) {
                            if ($.trim($(this).text()) == $.trim(list[i])) {
                                $(this).addClass("active");
                                break;
                            }
                        }
                        $(this).click(function () {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divEditModalContent").find(".active").length;
                            //  alertDialog(toggleCount);

                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                // $("#divEditModalContent").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                        })
                    })

                    return false;
                },
                error: function () { }
            });
            return false;
        })
        $(".fsimpact").on("click", function () {
            if (TogglePriorityClick() == 1)
                return false;
            $("#btnVmReview").css("display", "none");
            //divPriority1FSBenefits divPriority1Impacts
            var descId = $(this).attr("id").replace("FSBenefits", "FSDesc");
            var answer = "";
            $("#" + descId).find("p").each(function () {
                answer = answer + $.trim($(this).text()) + ",";
            });
            // answer = answer.replaceAll("!", " ,");
            answer = answer.substr(0, answer.length - 1);
            answer = answer.replace(/,(?=[^,]*$)/, ' and ');
            answer = answer.replaceAll(",", ", ");

            if (answer == "") {
                answer = $(this).attr("priorityname");
            }
            // alertDialog($(this).attr("id"));
            // var htmlContent = '<p>If you had "' + answer + '" how would it benefit the way you live?</p>';
            var htmlContent = '<p>How would "' + answer + '" <b>BENEFIT</b> your every day life and the way you live? <span style="font-size:13px!important">(select up to 4 answers)</span></p>';

            $("#lblEditPriorityType").html(htmlContent);
            $("#lblEditPriorityTitle").text($(this).attr("priorityname"));
            // $('#editModal').modal('show');
            $("#hidPriorityId").val($(this).attr("priorityid"));
            $("#hidQuestionType").val("4");
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", PriorityId: "' + $(this).attr("priorityid") + '"}';
            var list = new Array();
            $(this).find("p").each(function () {
                list.push($(this).text());
            });

            //  alertDialog(list.length);
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerFutureImpactEdit",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divEditModalContent").html("").html(msg.d);
                    $("#divEditModalContent,#divEditButtons,#divEditPriorityTitleHead").css("display", "");
                    $("#divPriorityOverView").css("display", "none");
                    ToggleDivHover("divEditModalContent");
                    maxHeightPriorityButtons("divEditModalContent");
                    maxWidthPriorityButtons("divEditModalContent");
                    $("#divEditModalContent").find(".btn-default").each(function () {
                        for (var i = 0; i < list.length; i++) {
                            if ($.trim($(this).text()) == $.trim(list[i])) {
                                $(this).addClass("active");
                                break;
                            }
                        }
                        $(this).click(function () {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divEditModalContent").find(".active").length;
                            //  alertDialog(toggleCount);

                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                // $("#divEditModalContent").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                        })
                    })

                    return false;
                },
                error: function () { }
            });
            return false;
        })
        $(".csimpact").on("click", function () {
            if (TogglePriorityClick() == 1)
                return false;
            $("#btnVmReview").css("display", "none");
            //divPriority1Impacts divPriority1CSDesc
            var descId = $(this).attr("id").replace("Impacts", "CSDesc");
            var answer = "";
            $("#" + descId).find("p").each(function () {
                answer = answer + $.trim($(this).text()) + ",";
            });
            // answer = answer.replaceAll("!", " ,");
            answer = answer.substr(0, answer.length - 1);
            answer = answer.replace(/,(?=[^,]*$)/, ' and ');
            answer = answer.replaceAll(",", ", ");
            if (answer == "") {
                answer = $(this).attr("priorityname");
            }
            // alertDialog($(this).attr("id"));
            var htmlContent = "<p>How does " + answer + "</span> <b>IMPACT </b>your every day life and the way you live? <span style='font-size:13px!important'>(select up to 4 answers)</span></p>";
            $("#lblEditPriorityType").html(htmlContent);
            $("#lblEditPriorityTitle").text($(this).attr("priorityname"));
            // $('#editModal').modal('show');
            $("#hidPriorityId").val($(this).attr("priorityid"));
            $("#hidQuestionType").val("1");
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", PriorityId: "' + $(this).attr("priorityid") + '"}';
            var list = new Array();
            $(this).find("p").each(function () {
                list.push($(this).text());
            });
            //  alertDialog(list.length);
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerCSImpactEdit",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divEditModalContent").html("").html(msg.d);
                    $("#divEditModalContent,#divEditButtons,#divEditPriorityTitleHead").css("display", "");
                    $("#divPriorityOverView").css("display", "none");
                    ToggleDivHover("divEditModalContent");
                    maxHeightPriorityButtons("divEditModalContent");
                    maxWidthPriorityButtons("divEditModalContent");
                    if ($("#divEditModalContent").find(".btns .btn-default").first().attr("future") == "1") {
                        // alert($("#divImpact").find(".btns .btn-default").first().attr("future"));
                        // var answer = $("#lblImpact").html();
                        $("#lblEditPriorityType").html("").html('What do you like about currently having "' + answer + '" what do these things allow you to do? <span style="font-size:13px!important">(select up to 4 answers)</span>');
                    }
                    else {
                        $("#priorityimpacttitle").html("").html('How does ' + answer + ' <b>IMPACT</b> your every day life and the way you live? <span style="font-size:13px!important">(select up to 4 answers)</span>');
                    }
                    $("#divEditModalContent").find(".btn-default").each(function () {
                        for (var i = 0; i < list.length; i++) {
                            if ($.trim($(this).text()) == $.trim(list[i])) {
                                $(this).addClass("active");
                                break;
                            }
                        }
                        $(this).click(function () {
                            $(this).toggleClass("active");
                            var toggleCount = $("#divEditModalContent").find(".active").length;
                            //  alertDialog(toggleCount);

                            if (toggleCount > 4) {
                                $(this).removeClass("active");
                                // $("#divEditModalContent").find(".active:eq(0)").removeClass("active");
                                alertDialog("You can select 4 items. Please unselect one to choose another.");

                                return false;
                            }
                        })
                    })

                    return false;
                },
                error: function () { }
            });
            return false;
        })
        $(".csdesc").on("click", function () {
            if (TogglePriorityClick() == 1)
                return false;
            $("#btnVmReview").css("display", "none");
            var htmlContent = "<p>What words best describe your <b>CURRENT </b>" + $(this).attr("priorityname") + "? <span style='font-size:13px!important'>(Choose from one list & select up to 4 answers</span>)</p>";
            $("#lblEditPriorityType").html(htmlContent);
            $("#lblEditPriorityTitle").text($(this).attr("priorityname"));
            // $('#editModal').modal('show');
            $("#hidPriorityId").val($(this).attr("priorityid"));
            $("#hidQuestionType").val("2");
            var dataValue = '{CustId: "' + $("#hidCustId").val() + '", PriorityId: "' + $(this).attr("priorityid") + '"}';
            var list = new Array();
            $(this).find("p").each(function () {
                list.push($(this).text());
            });
            //  alertDialog(list.length);
            $.ajax({
                url: "ValueMapCustomer.aspx/GetCustomerPriorityCurrentEdit",
                type: "POST",
                dataType: "json",
                data: dataValue,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    // alertDialog(msg.d);
                    $("#divCSFutureChoiceEdit").html("").html(msg.d);
                    $("#divEditModalContent1,#divEditButtons,#divEditPriorityTitleHead").css("display", "");
                    $("#divEditModalContent").css("display", "none");
                    $("#divPriorityOverView").css("display", "none");
                    $("#divCSFutureChoiceEdit").html("").html(msg.d.split("~")[1]);
                    $("#divCSCurrentChoiceEdit").html("").html(msg.d.split("~")[0]);
                    $("#divCSCurrentContentEdit").removeClass("blur-div");
                    $("#divCSCurrentChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                    $("#divCSFutureContentEdit").removeClass("blur-div");
                    $("#divCSFutureChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                    ToggleDivHover("divEditModalContent1");
                    // maxHeightPriorityButtons("divCSFutureChoiceEdit");
                    //maxWidthPriorityButtons("divCSCurrentChoiceEdit");
                    // maxHeightPriorityButtons("divCSCurrentChoiceEdit");
                    //maxWidthPriorityButtons("divCSFutureChoiceEdit");
                    maxHeightPriorityButtonsNew("divCSFutureChoiceEdit", "divCSCurrentChoiceEdit");
                    var totalfsLength = $("#divCSFutureChoiceEdit").find(".btns").last().find(".btn-default").length;
                    if (totalfsLength == 1) {
                        $("#divCSFutureChoiceEdit").find(".btns").last().css("max-width", "50%").css("min-width", "50%");
                    }
                    var totalcsLength = $("#divCSCurrentChoiceEdit").find(".btns").last().find(".btn-default").length;
                    if (totalcsLength == 1) {
                        $("#divCSCurrentChoiceEdit").find(".btns").last().css("max-width", "50%").css("min-width", "50%");
                    }
                    $(".priority-title").text($("#lblEditPriorityTitle").text())
                    $("#divCSFutureChoiceEdit").find(".btn-default").each(function () {
                        for (var i = 0; i < list.length; i++) {
                            if ($.trim($(this).text()) == $.trim(list[i])) {
                                $(this).addClass("active");
                                $("#divCSCurrentContentEdit").addClass("blur-div");
                                $("#divCSCurrentChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                break;
                            }
                        }
                    });
                    $("#divCSCurrentChoiceEdit").find(".btn-default").each(function () {
                        for (var i = 0; i < list.length; i++) {
                            if ($.trim($(this).text()) == $.trim(list[i])) {
                                $(this).addClass("active");
                                $("#divCSFutureContentEdit").addClass("blur-div");
                                $("#divCSFutureChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                                break;
                            }
                        }
                    });

                    $("#divCSFutureChoiceEdit").find(".btn-default").on('click', function () {

                        $(this).toggleClass("active");
                        // alertDialog("hi");
                        var toggleCount = $("#divCSFutureChoiceEdit").find(".active").length;
                        //  alertDialog(toggleCount);
                        if (toggleCount == 0) {
                            $("#divCSCurrentContentEdit").removeClass("blur-div");
                            $("#divCSCurrentChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                        }
                        else {
                            $("#divCSCurrentContentEdit").addClass("blur-div");
                            $("#divCSCurrentChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                        }
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            // $("#divCSFutureChoiceEdit").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");
                            return false;

                        }
                        return false;


                    })

                    $("#divCSCurrentChoiceEdit").find(".btn-default").on('click', function () {

                        $(this).toggleClass("active");
                        var toggleCount = $("#divCSCurrentChoiceEdit").find(".active").length;
                        //  alertDialog(toggleCount);
                        if (toggleCount == 0) {
                            $("#divCSFutureContentEdit").removeClass("blur-div");
                            $("#divCSFutureChoiceEdit").find(".btn-default").removeAttr("disabled").removeClass("active");
                        }
                        else {
                            $("#divCSFutureContentEdit").addClass("blur-div");
                            $("#divCSFutureChoiceEdit").find(".btn-default").attr("disabled", "disabled").removeClass("active");
                        }
                        if (toggleCount > 4) {
                            $(this).removeClass("active");
                            //$("#divCSCurrentChoiceEdit").find(".active:eq(0)").removeClass("active");
                            alertDialog("You can select 4 items. Please unselect one to choose another.");

                        }

                        return false;
                    })
                    return false;
                },
                error: function () { }
            });
            return false;
        })
        $("#btnOk").on("click", function () {
            var answer = "";
            var questionType = $("#hidQuestionType").val();
            if (questionType == "2") {
                var currentClass = $("#divCSCurrentContentEdit").attr("Class");
                //   var futureClass = $("#divCSCurrentnt").attr("Class");
                // alertDialog(currentClass.indexOf("blur-div"));
                // return false;
                if (currentClass.indexOf("blur-div") > -1) {
                    var toggleCount = $("#divCSFutureChoiceEdit").find(".active").length;
                    if (toggleCount == 0) {
                        alertDialog("Please select at least one choice.");
                        return false;
                    }
                    var answer = "";
                    $("#divCSFutureChoiceEdit").find(".active").each(function () {
                        answer += $(this).text() + "!";
                    })

                    InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 2, answer,1)


                }
                else {
                    var toggleCount = $("#divCSCurrentChoiceEdit").find(".active").length;
                    if (toggleCount == 0) {
                        alertDialog("Please select at least one choice.");
                        return false;
                    }
                    var answer = "";
                    $("#divCSCurrentChoiceEdit").find(".active").each(function () {
                        answer += $(this).text() + "!";
                    })

                    InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), 2, answer,-1)


                }


            }
            else {
                var toggleCount = $("#divEditModalContent").find(".active").length;
                if (toggleCount == 0) {
                    alertDialog("Please select at least one choice.");
                    return false;
                }

                $("#divEditModalContent").find(".active").each(function () {
                    answer += $(this).text() + "!";
                });
                 InsertCustomerPriorityChoiceDetails($("#hidCustId").val(), $("#hidPriorityId").val(), $("#hidQuestionType").val(), answer);
            }
           
            if (questionType == "1" || questionType == "4") {
                if (questionType == "1")
                    BindValueMapImpactDialogContent(answer);
                else
                    BindValueMapBenefitDialogContent(answer);
                $("#btnPreviousScore").css("display", "none");
                $("#btnNextScore").val("Ok");
                return false;
            }
            var href = window.location.href;
            window.location.href = href;
            return false;
        })


    </script>
    <script type="text/javascript">
        $(function () {
            $("#divCustomerPriorityContent").find(".btn-default").each(function () {
                $(this).hover(function () {
                    $(this).css("background", "#b0eddf");
                })
                $(this).mouseout(function () {
                    if ($(this).attr("class").indexOf("active") == -1) {
                        $(this).css("background", "#58595b");
                    }
                    else {
                        $(this).css("background", "#62DCBF");
                    }
                })
            })
        })
    </script>
    <script type="text/javascript">
        $(function () {
            ToggleDivHover("divPriorities");
            var href = $("#aOverView").attr("href");
            // $("#aOverView").attr("href", href.replace("../", ""))
            href = $("#aCreateValueMap").attr("href");
            // $("#aCreateValueMap").attr("href", href.replace("../", ""))
        })
        function ToggleDivHover(divId) {
            $("#" + divId).find(".btn-default").each(function () {
                $(this).hover(function () {
                    //   alertDialog("hi");
                    $(this).css("background", "#b0eddf").css("color", "grey");
                })
                $(this).mouseout(function () {
                    if ($(this).attr("class").indexOf("active") == -1) {
                        $(this).css("background", "#58595b").css("color", "white");
                    }
                    else {
                        $(this).css("background", "#62DCBF").css("color", "black!important");
                    }
                })
            })

        }
    </script>

    <script src="js/jquery.range.js"></script>
    <link href="css/jquery.range.css" rel="stylesheet" />
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
        function BindValueMapImpactDialogContent(answers) {
            $('.slider-input').jRange('setValue', "0");
            $('#myModal').modal('show');
            $("#lblPriorityDialogTitle").text("Current " + $("#hidPriorityName").val());
            if ($("#hidPriorityName").val() == "") {
                $("#lblPriorityDialogTitle").text("Current " + $("#lblEditPriorityTitle").text());
            }
            // $("#lblPriorityDialogTitle").text("Impact of your Current " + $("#hidPriorityName").val());

            $("#lblImpactsTitle").text("How much do these things bother you?");
            if ($("#divImpact").find(".btns .btn-default").first().attr("future") == "1")
                $("#lblImpactsTitle").text("How much do you like having these things?");
            //  $("#divImpact").find(".btns .btn-default").first().attr("future") 
            if (answers !== "") {
                var priortyImpactContentHtml = "";
                var priortyImpactContentSplit = answers.split("!");
                var answerCount = 0;
                //for (i = 0; i < priortyImpactContentSplit.length; i++) {
                //    if (priortyImpactContentSplit[i] != "") {
                //        priortyImpactContentHtml = priortyImpactContentHtml + "<li>" + priortyImpactContentSplit[i] + "</li>";
                //        answerCount += 1;
                //    }
                //}
                ////alert(answerCount);
                //if (answerCount % 2 != 0) {
                //    priortyImpactContentHtml += "<li>" + "</li>";
                //}
                // $("#ulImpacts").html(priortyImpactContentHtml);
                for (i = 0; i < priortyImpactContentSplit.length; i++) {

                    if (priortyImpactContentSplit[i] != "") {
                        if (i % 2 == 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "<tr><td style=''><img src='images/item.png' style='margin-top:-2px'/>" + priortyImpactContentSplit[i] + "</td>";
                        }
                        else {
                            priortyImpactContentHtml = priortyImpactContentHtml + "<td style=''><img src='images/item.png' style='margin-top:-2px;margin-left:30px'/>" + priortyImpactContentSplit[i] + "</td>";
                        }
                        if (i % 2 != 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "</tr>";
                        }
                        answerCount += 1;
                    }
                }
                $("#tblImpacts").html(priortyImpactContentHtml);
                BindValueMapScore("1");
            }

        }
        function BindValueMapBenefitDialogContent(answers) {
            $('.slider-input').jRange('setValue', "0");
            $('#myModal').modal('show');
            $("#lblPriorityDialogTitle").text("Ideal " + $("#hidPriorityName").val());
            if ($("#hidPriorityName").val() == "") {
                $("#lblPriorityDialogTitle").text("Ideal " + $("#lblEditPriorityTitle").text());
            }
            $("#lblImpactsTitle").text("How important is it to have these Benefits?");
            if (answers !== "") {
                var priortyImpactContentHtml = "";
                var priortyImpactContentSplit = answers.split("!");
                var answerCount = 0;
                //for (i = 0; i < priortyImpactContentSplit.length; i++) {
                //    if (priortyImpactContentSplit[i] != "") {
                //        priortyImpactContentHtml = priortyImpactContentHtml + "<li>" + priortyImpactContentSplit[i] + "</li>";
                //        answerCount += 1;
                //    }
                //}
                //if (answerCount % 2 != 0) {
                //    priortyImpactContentHtml += "<li>" + "</li>";
                //}
                //$("#ulImpacts").html(priortyImpactContentHtml);
                for (i = 0; i < priortyImpactContentSplit.length; i++) {

                    if (priortyImpactContentSplit[i] != "") {
                        if (i % 2 == 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "<tr><td style=''><img src='images/item.png' style='margin-top:-2px'/>" + priortyImpactContentSplit[i] + "</td>";
                        }
                        else {
                            priortyImpactContentHtml = priortyImpactContentHtml + "<td style=''><img src='images/item.png' style='margin-top:-2px;margin-left:30px'/>" + priortyImpactContentSplit[i] + "</td>";
                        }
                        if (i % 2 != 0) {
                            priortyImpactContentHtml = priortyImpactContentHtml + "</tr>";
                        }
                        answerCount += 1;
                    }
                }
                $("#tblImpacts").html(priortyImpactContentHtml);
                BindValueMapScore("4");
            }

        }
        $(function () {
            $("#btnPreviousScore").on("click", function () {
                $('#myModal').modal('hide');
                var title = $("#lblPriorityDialogTitle").text();
                if (title.indexOf("Current") > -1) {
                }
                else {
                    GetCustomerPriorityBenefit($("#hidCustId").val(), $("#hidPriorityId").val(), 4);
                }
                return false;
            })
            $("#btnNextScore").on("click", function () {
                $('#myModal').modal('hide');
                $("#divCSBenefit").show();
                 maxHeightPriorityButtons("divBenefit");
                    maxWidthPriorityButtons("divBenefit");
                var title = $("#lblPriorityDialogTitle").text();
                if (title.indexOf("Current") > -1) {
                    $("#divCSImpact").hide();
                    $('#myModal').modal('hide');
                    InsertValueMapScore("1");
                    if ($(this).val().toLowerCase() == "ok") {
                        var URL = window.location.href;
                        window.location.href = URL;
                    }
                }
                else {
                    InsertValueMapScore("4");
                    if ($(this).val().toLowerCase() == "ok") {
                        var URL = window.location.href;
                        window.location.href = URL;
                    }
                    currentPriority += 1;
                    var priorityId;
                    var priorityName;
                    if (currentPriority > 3) {
                        //window.location.reload();
                        //var URL = window.location.href;
                        //window.location.href = URL;
                         $("#btnRealtorEmail").trigger("click");
                    }
                    if (currentPriority == 2) {
                        priorityId = $("#sortable").find("li").first().next().find("span").attr("priorityid");
                        priorityName = $("#sortable").find("li").first().next().find("span").text();
                        if (valueMapStatus == "new") {
                            priorityDialog("Begin Priority 2 of 3: " + priorityName)
                        }
                    }
                    else {
                        priorityId = $("#sortable").find("li").first().next().next().find("span").attr("priorityid");
                        priorityName = $("#sortable").find("li").first().next().next().find("span").text();
                       if (valueMapStatus == "new") {
                            priorityDialog("Begin Priority 3 of 3: " + priorityName)
                        }
                    }
                    //  alertDialog(priorityId);
                    //  alertDialog(priorityName);
                    $("#hidPriorityId").val(priorityId);
                    $("#hidPriorityName").val(priorityName);

                    $("#divCSFuture").show();
                    $("#divCSBenefit").hide();
                    if (currentPriority > 1) {
                        //  $("#btnPreviousFuture").attr("disabled", "disabled");
                    }
                    else {
                        //  $("#btnPreviousFuture").removeAttr("disabled");
                    }
                    GetCustomerFuture();
                    BindPriorityName();
                }
                return false;
            })
        })
        function InsertValueMapScore(QuestionTypeId) {
            $.ajax({
                type: "POST",
                url: "ValueMapCustomer.aspx/InsertValueMapScore",
                data: "{'customerID':'" + $("#hidCustId").val() + "','questionTypeID':'" + QuestionTypeId + "','priorityID':'" + $("#hidPriorityId").val() + "','score':'" + $(".slider-input").val() + "'}",
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
                },
                error: function (data) {
                    //alertDialog(data.msg);
                }
            });
        }
        function BindValueMapScore(questionTypeID) {

            $.ajax({
                type: "POST",
                url: "ValueMapCustomer.aspx/GetValueMapScore",
                data: "{'customerID':'" + $("#hidCustId").val() + "','questionTypeID':'" + questionTypeID + "','priorityID':'" + $("#hidPriorityId").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;
                    var score = result;
                    //var note = decodeURIComponent(result[1]);

                    $(".slider-input").val("0");
                    if (score == "" || score == "null" || score == null || score == "undefined" || score == undefined) {
                        $('.slider-input').jRange('setValue', "0");
                    }
                    else {

                        // $(".slider-input").val(score.toLocaleString());
                        $('.slider-input').jRange('setValue', score);
                        // $(".slider-input").setValue(score);
                    }

                },
                error: function (data) {
                    //alertDialog(data.msg);
                }
            });
        }
    </script>

    <script type="text/javascript">
        function ToggleVMReview() {

            if ($("#divEditPriority").css("display") == "none" && $("#btnVmReview").css("display") != "none") {
                $("#btnVmReview").css("display", "");
            }
            else {
                $("#btnVmReview").css("display", "none");
            }
        }

        $(function () {
            ToggleVMReview();
            $("#btnVmReview").on("click", function () {

                //confirmDialog("Are you sure you want to remove ALL of your current Value Map selections and start from the beginning?"); 
                $("#divPriorityOverView,#btnVmReview").css("display", "none");
                $("#divEditPriority").css("display", "");
                $("#hidCustomerPriority").val("new");
                if ($("#hidValueMapStatus").val() == "old") {
                    currentPriority = $("#hidValueMapQuestionDisplay").val().toString().split("~")[2];
                    displayQuestionTypeId = $("#hidValueMapQuestionDisplay").val().toString().split("~")[1];
                    valueMapStatus = "old";
                }
                else {
                    currentPriority = 1;
                    displayQuestionTypeId = 3;
                    valueMapStatus = "new";
                    $("#valueMapModal").modal("show");
                }
                $("#btnNextPriority").trigger("click");

                $("#btnNextRank").trigger("click");
                $("#btnConfirmOk").on("click", function () {
                    DeleteAllCustomerPriority();
                    ToggleVMReview();
                    var URL = window.location.href;
                    window.location.href = URL;
                    $("#divPriorityOverView,#btnVmReview").css("display", "none");
                    $("#divEditPriority").css("display", "");
                    $("#hidCustomerPriority").val("new");
                    $('#confirmModal').modal('hide');
                    // return true;
                })
                return false;
            });
            $(".prioritycircle").on("click", function () {
                if (TogglePriorityClick() == 1)
                    return false;
                $("#divPriorityOverView,#btnVmReview").css("display", "none");
                $("#divEditPriority").css("display", "");
                // $("#hidCustomerPriority").val("new");
                $('#confirmModal').modal('hide');
                return false;
            })
        })
        function DeleteAllCustomerPriority() {
            $.ajax({
                type: "POST",
                url: "ValueMapCustomer.aspx/DeleteAllCustomerPriority",
                data: "{'customerID':'" + $("#hidCustId").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var result = msg.d;


                },
                error: function (data) {
                    //alertDialog(data.msg);
                }
            });
        }
    </script>

    <script type="text/javascript">
        $(function () {
            // $(".csimpact p,.csdesc p").matchHeight();
            var maxHeight = Math.max.apply(null, $(".csimpact p,.csdesc p,.fsdesc p,.fsimpact p").map(function () {
                return $(this).height();
            }).get());
            $(".csimpact p,.csdesc p,.fsdesc p,.fsimpact p").css("height", maxHeight);
        });
        function maxHeightPriorityButtons(divId) {
            var maxHeight = Math.max.apply(null, $("#" + divId).find(".btns").map(function () {
                return $(this).height();
            }).get());

            //alert(maxHeight);
            $("#" + divId).find(".btns").css("min-height", maxHeight);
            $("#" + divId).find(".btns .btn-default").css("min-height", maxHeight);
        }
        function maxWidthPriorityButtons(divId) {
            var maxWidth = Math.max.apply(null, $("#" + divId).find(".btns").map(function () {
                return $(this).width();
            }).get());
            $("#" + divId).find(".btns").css("min-width", maxWidth);
        }
        function maxHeightPriorityButtonsNew(divId, divId1) {
            var maxHeight1 = Math.max.apply(null, $("#" + divId).find(".btns").map(function () {
                return $(this).height();
            }).get());
            //alert(maxHeight);
            var maxHeight2 = Math.max.apply(null, $("#" + divId1).find(".btns").map(function () {
                return $(this).height();
            }).get());
            if (maxHeight1 >= maxHeight2) {
                $("#" + divId).find(".btns,.btns .btn-default").css("min-height", maxHeight1);
                $("#" + divId1).find(".btns,.btns .btn-default").css("min-height", maxHeight1);
            }
            else {
                $("#" + divId).find(".btns,.btns .btn-default").css("min-height", maxHeight2);
                $("#" + divId1).find(".btns,.btns .btn-default").css("min-height", maxHeight2);
            }


        }
        function maxWidthPriorityButtonsNew(divId, divId1) {
            var maxWidth1 = Math.max.apply(null, $("#" + divId).find(".btns").map(function () {
                return $(this).width();
            }).get());
            var maxWidth2 = Math.max.apply(null, $("#" + divId1).find(".btns").map(function () {
                return $(this).width();
            }).get());
            if (maxWidth1 >= maxWidth2) {
                $("#" + divId).find(".btns").css("min-width", maxWidth1);
                $("#" + divId1).find(".btns").css("min-width", maxWidth1);
            }
            else {
                $("#" + divId).find(".btns").css("min-width", maxWidth2);
                $("#" + divId1).find(".btns").css("min-width", maxWidth2);
            }
        }
        function TogglePriorityClick() {
            if ($("#btnVmReview").css("display") == "none") {
                return 1;
            }
            return 0
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#btnVerifyEmail").click(function () {
                var inputEmail = $.trim($("#txtVerifyEmail").val().toLowerCase());
                var userEmail = $("#hidUserEmail").val().toLowerCase();
                if (inputEmail == "") {
                    alertDialog("Please enter your email address.");
                    return false;
                }
                if (inputEmail != userEmail) {
                    alertDialog("Email address not matched.");
                    return false;
                }
                var customerPriority = $("#hidCustomerPriority").val();
                $("#divVerifyEmail").css("display", "none");
              //  $("#valueMapModal").modal("show");
                $("#divCustomerInfo").css("display", "");
                if (customerPriority == "new") {
                    $("#divPriorityOverView").css("display", "none");
                    $("#divEditPriority").css("display", "");
                    //divPriorityOverView.Style.Add("display", "none")
                    // divEditPriority.Style.Add("display", "none")
                }
                else {
                    $("#btnVmReview").trigger("click");
                }
                $("#btnVmReview").css("display", "none")
                return false;
            });
            $("#txtVerifyEmail").focus();
            var completed = $("#divCompleted").css("display");
            //alert($("#hidUserMode").val());
            if (completed != "none") {
                $("#maincontainer").css("background-color", "black");
             
            }
            else {
                if ($("#hidUserMode").val() == "1") {

                    $("#txtVerifyEmail").val($("#hidUserEmail").val().toLowerCase());
                   var customerPriority = $("#hidCustomerPriority").val();
                $("#divVerifyEmail").css("display", "none");
               // $("#valueMapModal").modal("show");
                $("#divCustomerInfo").css("display", "");
                if (customerPriority == "new") {
                    $("#divPriorityOverView").css("display", "none");
                    $("#divEditPriority").css("display", "");
                    //divPriorityOverView.Style.Add("display", "none")
                    // divEditPriority.Style.Add("display", "none")
                }
                else {
                    $("#btnVmReview").trigger("click");
                }
                $("#btnVmReview").css("display", "none")
                return false;
                }
            }
        });
    </script>
    <style type="text/css">
        #home {
            background-color: #cecece;
        }

        .queries .btn-default.active {
            background: #62DCBF !important;
            outline: none !important;
        }

        .queries .btn-default {
            font-size: 13px;
            background: #58595b;
            outline: none !important;
        }

        .btn-default {
            font-size: 13px;
            background: #58595b;
            outline: none !important;
        }

            .btn-default.active {
                background: #62DCBF !important;
                outline: none !important;
            }

        .arrow-steps {
            margin-bottom: 0px;
        }

        .active {
            color: black !important;
        }

        .ui-sortable span {
            color: black !important;
        }
        /*#divCSCurrentChoiceEdit div button {
            overflow: hidden;
            white-space: nowrap;
            text-align: left;
            text-overflow: ellipsis;
        }
        #divCSFuture div button {
            overflow: hidden;
            white-space: nowrap;
            text-align: left;
            /*text-overflow: ellipsis;*/
        }
        */
    </style>
    <style type="text/css">
        .slider-container .scale ins {
            font-size: 14px;
            text-decoration: none;
            position: absolute;
            left: 0;
            top: 5px;
            color: #999;
            line-height: 2;
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
            top: -5px;
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


        .btns {
            display: -webkit-flex;
            /*border-bottom: 1px solid #ffffff;*/
        }

            .btns .btn-default {
                border-bottom: 1px solid #ffffff;
            }

        #divCSFutureChoice .btns .btn-default {
            border-bottom: none;
        }

        #divCSCurrentChoice .btns .btn-default {
            border-bottom: none;
        }

        #divCSFutureChoiceEdit .btns .btn-default {
            border-bottom: none;
        }

        #divCSCurrentChoiceEdit .btns .btn-default {
            border-bottom: none;
        }

        .items li {
            width: inherit;
        }

        .prioritycircle {
            cursor: pointer;
        }

        .priority-header {
            font-size: 19px !important;
        }
    </style>
</asp:Content>
