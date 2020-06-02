<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.vb" Inherits="RIS.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>REIMAGINE</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <div class="right" id='maincontent'>
        <%--<div class='homecontent col-offset-2' id="divNewCustomer" runat="server">--%>
        <asp:Panel CssClass='homecontent col-offset-2' ID="divNewCustomer" runat="server">
            <h1 style="color: #dbdbdb; font-size: 25px;">Welcome to the future of Real Estate, <span id="lblUserName" runat="server"></span>.<br />
                Let's gets started! 
            </h1>
            <br />
            <p>
                <a href='MyProfile.aspx'>1. Update your Profile</a>
            </p>
            <p>
                <a href='AddContact.aspx'>2. Add a Contact</a>
            </p>
            <p>
                <a href='#'>3. Add a To Do</a>
            </p>
        </asp:Panel>
        <%-- <div class='homecontent col-offset-2' id="divReturnCustomer" runat="server">--%>
        <asp:Panel CssClass='homecontent col-offset-2' ID="divReturnCustomer" runat="server">
            <h1 style="color: #030305; font-size: 25px;">Welcome back, <span id="lblUserName1" runat="server"></span>.
            </h1>
            <br />
            <div class="row" id="divSearchGrid" runat="server">
                <div class='col-md-7'>
                    <div class="card">
                        <div class="card-header">
                            <a style="color: #62DCBF; font-weight: bold">Recent Activity
                            </a>
                        </div>
                        <div class="card-body">
                            <asp:ListView ID="lvRecentActivity"  runat="server">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidActivityType" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.ActivityType") %>'  />
                                     <asp:HiddenField ID="hidCreatedDate" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.CreatedDate") %>'  />
                                     <asp:HiddenField ID="hidDescription" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.Description") %>'  />
                                    <asp:HiddenField ID="hidReferenceId" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.ReferenceId") %>'  />
                                    <asp:HiddenField ID="hidCustomerName" runat="server"  Value='<%# DataBinder.Eval(Container, "DataItem.CustomerName") %>'  />
                                    <div class="row activity">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblTimeLine" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-9 text-left">
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                <%-- <div class="co1-md-3"></div>--%>
                <div class='col-md-5'>
                    <div class="card" style="width:117%;margin-bottom: 15px">
                        <div class="card-header" style="color: #62DCBF">
                            <a style="color: #62DCBF; font-weight: bold">Today's To Dos
                            </a>
                        </div>
                        <div class="card-body">
                            <asp:ListView ID="lvTodayToDo" runat="server">
                                <ItemTemplate>
                                    <div class="row todo">
                                        <asp:HiddenField ID="hidToDoId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Id") %>' />
                                        <asp:HiddenField ID="hidCustId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.CustomerId") %>' />
                                        <asp:HiddenField ID="hidDueDate" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.DueDate") %>' />
                                        <div class="col-md-8">
                                            <asp:Label ID="lblTodoTitle" runat="server"></asp:Label>
                                            <a id="aToDoViewLink" style="display:none;" runat="server"><%# DataBinder.Eval(Container, "DataItem.Title") %> </a>
                                        </div>
                                        <div class="col-md-4 text-right" style="display:none;">
                                            <a id="aCustomerLink" runat="server"><%# DataBinder.Eval(Container, "DataItem.CustomerName") %> </a>
                                        </div>
                                        <div class="col-md-4 text-right">
                                            <a id="aToDoEditLink" runat="server">Edit</a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <div class="card" style="width:117%">
                        <div class="card-header" style="color: #62DCBF">
                            <a style="color: #62DCBF; font-weight: bold">To Dos
                            </a>
                        </div>
                        <div class="card-body">
                            <asp:ListView ID="lvToDo" runat="server">
                                <ItemTemplate>
                                    <div class="row todo">
                                        <asp:HiddenField ID="hidToDoId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Id") %>' />
                                        <asp:HiddenField ID="hidCustId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.CustomerId") %>' />
                                        <asp:HiddenField ID="hidDueDate" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.DueDate") %>' />
                                        <div class="col-md-8">
                                            <asp:Label ID="lblTodoTitle" runat="server"></asp:Label>
                                            <a id="aToDoViewLink" style="display:none;" runat="server"><%# DataBinder.Eval(Container, "DataItem.Title") %> </a>
                                        </div>
                                        <div class="col-md-4 text-right" style="display:none;">
                                            <a id="aCustomerLink" runat="server"><%# DataBinder.Eval(Container, "DataItem.CustomerName") %> </a>
                                        </div>
                                        <div class="col-md-4 text-right">
                                            <a id="aToDoEditLink" runat="server">Edit</a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
                
            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        $(function () {
            if ($("#divReturnCustomer").css("display") == "none")
                $("#maincontainer").css("background-color", "#030305 !important")
            else
                $("#maincontainer").css("background-color", "#cecece !important")
        })
    </script>
    <style type="text/css">
        /*#maincontainer {
            background-color:#030305 !important;
        }*/
        .homecontent p a {
            color: #62dcbf;
            font-size: 20px;
        }
        .navigate{
            /*text-decoration:underline*/
        }
        .activity span {
            font-size:14px;
        }
           .todo span {
            font-size:14px;
        }
    </style>
</asp:Content>
