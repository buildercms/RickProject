<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SearchContact.aspx.vb" Inherits="RIS.SearchContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="right" id='maincontent'>
        <div class="row">
            <div class="col-md-12">
                <h2>Contacts</h2>
            </div>
        </div>
        <div class='row'>
            <div class='col-md-12'>

                <div id="accordion" class='accordioncontent' style="width: 102.5%">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-8">
                                    <a class="" data-toggle="collapse in" style="color: #62DCBF; font-weight: bold">Search
                                    </a>
                                </div>
                                <div class="col-md-4">
                                    <button class="btn float-right font-weight-bold" style="margin-top: -5px; margin-bottom: -5px;" id="btnMasterAddContact" onclick="javascript:window.location.href='AddContact.aspx?mode=search';" type="button"><i class="fa fa-plus"></i>Add Contact</button>
                                </div>
                            </div>
                        </div>
                        <div id="collapseOne" class="collapse show" data-parent="#accordion">
                            <div class="card-body">
                                <div class="form-row" style="background-color: white!important;">
                                    <div class="form-group col-6">
                                        <label><span></span>First Name:</label>
                                        <input type="text" class="form-control searchcontact" id="txtSearchFirstName" runat="server" placeholder="First Name" style="outline: none!important;" />
                                    </div>
                                    <div class="form-group col-6">
                                        <label><span></span>Last Name:</label>
                                        <input type="text" class="form-control  searchcontact" id="txtSearchLastName" runat="server" placeholder="Last Name" />
                                    </div>
                                    <div class="form-group col-6">
                                        <label><span></span>Note Search:</label>
                                        <input type="text" class="form-control searchcontact" id="txtNoteSearch" runat="server" placeholder="Note Search" />
                                    </div>

                                    <div class="form-group col-6">
                                        <label><span></span>Value Map Status:</label>
                                        <asp:DropDownList ID="ddlVMStatus" runat="server" CssClass="form-control searchcontact">
                                            <asp:ListItem Text="All" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Not Started" Value="Not Started"></asp:ListItem>
                                            <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                            <asp:ListItem Text="Sold Not Closed" Value="Sold Not Closed"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                            <asp:ListItem Text="Sold" Value="Sold"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-6">
                                        <label><span></span>Community:</label>
                                        <asp:DropDownList ID="ddlCommunity" AutoPostBack="true" OnSelectedIndexChanged="ddlCommunity_SelectedIndexChanged" runat="server" CssClass="form-control searchcontact">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-6">
                                        <label><span></span>Value Map Category:</label>
                                        <asp:DropDownList ID="ddlVMCategory" runat="server" CssClass="form-control searchcontact">
                                            <asp:ListItem Text="Value Map Category" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-6"></div>
                                    <div class="form-group col-6 text-left text-bottom searchcontact">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label style=""><span># of Records:</span></label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <asp:DropDownList ID="ddlRecordCount" runat="server" CssClass="form-control searchcontact" Width="85px">
                                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                    <asp:ListItem Text="150" Value="150"></asp:ListItem>
                                                                    <asp:ListItem Text="250" Value="250"></asp:ListItem>
                                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-7 align-text-baseline" style="font-size: 13px; top: 20px; vertical-align: text-bottom">
                                                                <asp:CheckBox ID="chkInactive" CssClass="align-text-bottom" runat="server" Text="Include Inactive" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-4 " style="margin-top: 25px;">

                                                <asp:Button ID="btnSearch" Style="margin-left: -20px;" runat="server" Text="Search" CausesValidation="false" Width="115px" CssClass="btn px-3 searchbutton font-weight-bold" OnClick="btnSearch_Click" />
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
        <div class='row' id="divAllContactsGrid" runat="server" style="display: none;">
            <div class='col-md-6'>
                <div class="card">
                    <div class="card-header">
                        <p style="color: #62DCBF">
                            Contacts (A-Z)
                        </p>
                    </div>
                    <div class="card-body">
                        <asp:ListView ID="lvAllContacts" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <a id="aCustName" runat="server" href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "OverView.aspx?cid={0}") %>'><%# DataBinder.Eval(Container, "DataItem.CustomerName") %></a>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <a href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "AddContact.aspx?cid={0}") %>'>Edit</a>
                                    </div>
                                    <div class="col-md-4 text-right">
                                        <a id="aValueMap" runat="server" href="#">Value Map</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div class="co1-md-2"></div>
            <div class='col-md-6'>
                <div class="card">
                    <div class="card-header" style="color: #62DCBF">
                        <p>
                            Recently Viewed
                        </p>
                    </div>
                    <div class="card-body">
                        <asp:ListView ID="lvRecentContacts" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-md-6">
                                        <a id="aCustName" runat="server" href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "AddContact.aspx?cid={0}") %>'><%# DataBinder.Eval(Container, "DataItem.CustomerName") %></a>
                                    </div>
                                    <div class="col-md-2 text-right">
                                        <a href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "AddContact.aspx?cid={0}") %>'>Edit</a>
                                    </div>
                                    <div class="col-md-4 text-right">
                                        <a id="aValueMap" runat="server" href="#">Value Map</a>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>


        <div class="form-row" id="divSearchGrid" runat="server" style="display: none;">
            <div class="col-md-12">
                <div class="card" style="width: 102.5%; padding: 0; background: none">
                    <div class="card-header">
                        <a style="color: #62DCBF; font-weight: bold;" id="lblSearchTitle" runat="server">Search Results (<asp:Label ID="lblTotalCount" runat="server" Text="0"></asp:Label>)
                        </a>
                    </div>
                    <div class="card-body" style="padding: 0px;">

                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="table table-borderless" ShowHeaderWhenEmpty="true" EmptyDataText="No Contacts found." EmptyDataRowStyle-HorizontalAlign="Center">
                            <HeaderStyle CssClass="first" BackColor="#62DCBF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Contact Name" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <a id="aCustomerLink" runat="server" target="_blank" href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "OverView.aspx?cid={0}") %>'><%# DataBinder.Eval(Container, "DataItem.FirstName") %> <%# DataBinder.Eval(Container, "DataItem.LastName") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Map Category" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValueMapCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMCategory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Map Status" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValueMapStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Value Map Category" DataField="VMCategory" />
                                <asp:BoundField HeaderText="Value Map Status" DataField="VMStatus" />--%>
                                <asp:TemplateField HeaderText="Edit" Visible="false" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <a href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "AddContact.aspx?cid={0}") %>'>Edit</a>
                                        <asp:Label ID="lblCustomerStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Active") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Map" Visible="false" HeaderStyle-ForeColor="white">
                                    <ItemTemplate>
                                        <a id="aValueMap" runat="server" href='<%# DataBinder.Eval(Container, "DataItem.CustomerId", "ValueMap.aspx?cid={0}") %>'>Value Map</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle ForeColor="Gray" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $(document).find("input,select,button").css("outline", "none !important")
            $("ul[id$='ulSideBar']").find("li").each(function () {
                $(this).removeClass("active");
            });
            $("ul[id$='ulSideBar']").find("li[id$='liContact']").addClass("active")
            $(".searchcontact").bind("keydown", function (event) {
                //   alert(event.keyCode);
                if (event.keyCode == 13) {
                    $(".searchbutton").trigger("click");
                    return false;
                }
            })
        });

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
    </style>
</asp:Content>
