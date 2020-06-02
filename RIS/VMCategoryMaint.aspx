<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VMCategoryMaint.aspx.vb" Inherits="RIS.VMCategoryMaint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="<%= ResolveUrl("~/js/HideShow.js")%>" type="text/javascript"></script>
   <meta charset="utf-8">    
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE</title>
    <script type="text/javascript">
        function HideCategoryRow(id) {
            //alert(id);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  >
     <ajax:ToolkitScriptManager runat="server" ID="scriptManager1"></ajax:ToolkitScriptManager>
     <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
       <div class="right" id='maincontent'>
           <h2 id="lblPageTitle">Category Maintenance</h2>
           <div class="modal-panel" id="jqdialogGroup" style="display: none;  background-color: rgb(217, 217, 217);  padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upGroup" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 15px;" id="divGroupError" runat="server" visible="false">
                                <asp:Label ID="lblGroupError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <div style="margin: 0px; padding-left: 45px; width: 85%;">
                                <asp:GridView ID="gvGroups" runat="server" TabIndex="-1" AllowSorting="false"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" CssClass="table table-borderless border-0" AutoGenerateColumns="False"  ShowHeader="true" 
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                    <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    
                                    <HeaderStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GGID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GlobalGroupName")%>' Visible="true"></asp:Label>
                                                 <asp:Label ID="lblGroupCategoryCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCount")%>' Visible="false"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditGroupId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GGID")%>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lblEditGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GlobalGroupName")%>' Visible="false"></asp:Label>
                                                 <asp:TextBox ID="txtGroup" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GlobalGroupName")%>' MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEditGroupName" runat="server" ErrorMessage="Required" ControlToValidate="txtGroup" ValidationGroup="EditGroup" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"  />
                                            <HeaderStyle  />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewGroup"  runat="server" MaxLength="50" onkeydown="return groupkey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNewGroup" runat="server" ErrorMessage="Required" ControlToValidate="txtNewGroup" ValidationGroup="AddGroup" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGroupEdit" runat="server" Text="Edit"  Font-Bold="false" Font-Underline="false" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkGroupUpdate" runat="server" Text="Update"  Font-Bold="false" Font-Underline="false" CommandName="Update" ValidationGroup="EditGroup"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkGroupCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkGroupDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Group?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddGroup" CssClass="btn" runat="server" Text="Add New" CommandName="AddNew"  ValidationGroup="AddGroup" />
                                            </FooterTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal-panel" id="jqdialogCategory" style="display: none; background-color: rgb(217, 217, 217);  padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upCategory" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropDialogGroups" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 15px;" id="divCategoryError" runat="server" visible="false">
                                <asp:Label ID="lblCategoryError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <div id="divGroupCategory" runat="server">
                               Select Group: <asp:DropDownList ID="dropDialogGroups" runat="server" AutoPostBack="true"></asp:DropDownList>   <asp:Button ID="btnCopyCategory" runat="server" Text="Copy Category"  CssClass="btn"  style="margin-bottom:5px;" OnClick="btnCopyCategory_Click"  /><br />
                            </div>
                              <div id="divCopyGroupCategory" runat="server" visible="false" style="text-align:center;">
                            <table style="width:480px;">
                                <tr>
                                    <td align="right">Current Group:</td>
                                    <td align="left"><asp:DropDownList ID="dropDialogCurrentGroup" runat="server" AutoPostBack="false"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="right"> Copy From Group:</td>
                                    <td align="left"><asp:DropDownList ID="dropDialogCopyGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropDialogCopyGroup_SelectedIndexChanged"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="right">Copy Category:</td>
                                    <td align="left"><asp:DropDownList ID="dropDialogCopyGroupCategory" runat="server" AutoPostBack="false"></asp:DropDownList>

                                    </td>
                                    
                                </tr>
                                <tr>
                                    
                                    <td colspan="2"><asp:Button ID="btnSubmitCopyCategory" runat="server" Text="Submit"  CssClass="btn" style="margin-bottom:5px;" OnClick="btnSubmitCopyCategory_Click"  />&nbsp;&nbsp;<asp:Button ID="btnCancelCopyCategory" runat="server" Text="Cancel"  CssClass="btn" OnClick="btnCancelCopyCategory_Click" style="margin-bottom:5px;"  /></td>
                                </tr>
                            </table>
                            </div>
                            <div style="margin: 0px; padding-left: 45px; width: 85%;">
                                <asp:GridView ID="gvCategories" runat="server" TabIndex="-1" AllowSorting="false"
                                    ShowHeaderWhenEmpty="true"   CellPadding="0" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-borderless border-0"
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                    <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMCategoryID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>' Visible="true"></asp:Label>
                                                <asp:Label ID="lblCategoryCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCount")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"  />
                                            <HeaderStyle  />
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditCategoryId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMCategoryID")%>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lblEditCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>' Visible="false"></asp:Label>
                                                 <asp:TextBox ID="txtCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>' MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEditCategoryName" runat="server" ErrorMessage="Required" ControlToValidate="txtCategory" ValidationGroup="EditCategory" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewCategory" runat="server" MaxLength="50" onkeydown="return categorykey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="Required" ControlToValidate="txtNewCategory" ValidationGroup="AddCat" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCategoryEdit" runat="server" Text="Edit"  Font-Bold="false" Font-Underline="false" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkCategoryUpdate" runat="server" Text="Update"  Font-Bold="false" Font-Underline="false" CommandName="Update" ValidationGroup="EditCategory"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCategoryCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkCategoryDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Category?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddCategory" runat="server" Text="Add New" CommandName="AddNew" CssClass="btn" ValidationGroup="AddCat" />
                                            </FooterTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
           <div class="modal-panel" id="jqdialogCommunity" style="display: none;  background-color: rgb(217, 217, 217);  padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upCommunity" runat="server">
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropDialogCommunityGroups" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 15px;" id="divCommunityError" runat="server" visible="false">
                                <asp:Label ID="lblCommunityError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                             <div>
                               Select Group: <asp:DropDownList ID="dropDialogCommunityGroups" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div style="margin: 0px; padding-left: 45px; width: 85%;">
                                <asp:GridView ID="gvCommunities" runat="server" TabIndex="-1" AllowSorting="false"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" CssClass="table table-borderless border-0" AutoGenerateColumns="False"  ShowHeader="true" 
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                    <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    
                                    <HeaderStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Community" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommunityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCommunity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommName")%>' Visible="true"></asp:Label>
                                                 <asp:Label ID="lblCommunityCategoryCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCount")%>' Visible="false"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditCommunityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommID")%>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lblEditCommunity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommName")%>' Visible="false"></asp:Label>
                                                 <asp:TextBox ID="txtCommunity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommName")%>' MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEditCommunityName" runat="server" ErrorMessage="Required" ControlToValidate="txtCommunity" ValidationGroup="EditCommunity" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"  />
                                            <HeaderStyle  />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewCommunity"  runat="server" MaxLength="50" onkeydown="return groupkey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNewCommunity" runat="server" ErrorMessage="Required" ControlToValidate="txtNewCommunity" ValidationGroup="AddCommunity" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCommunityEdit" runat="server" Text="Edit"  Font-Bold="false" Font-Underline="false" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkCommunityUpdate" runat="server" Text="Update"  Font-Bold="false" Font-Underline="false" CommandName="Update" ValidationGroup="EditCommunity"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkCommunityCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkCommunityDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Community?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddCommunity" CssClass="btn" runat="server" Text="Add New" CommandName="AddNew"  ValidationGroup="AddCommunity" />
                                            </FooterTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
             <div class="modal-panel" id="jqdialogCommunityCategory" style="display: none;  background-color: rgb(217, 217, 217);  padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upCommunityCategory" runat="server">
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropDialogCommunityCategoryGroups" EventName="SelectedIndexChanged" />
<asp:AsyncPostBackTrigger ControlID="dropDialogCommunityCategory" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 15px;" id="divCommunityCategoryError" runat="server" visible="false">
                                <asp:Label ID="lblCommunityCategoryError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                             <div style="text-align:center; margin-left:20px">
                                 <table>
                                     <tr>
                                         <td align="right">
                                             Select Group: 
                                         </td>
                                         <td align="left">
<asp:DropDownList ID="dropDialogCommunityCategoryGroups" runat="server" AutoPostBack="true"></asp:DropDownList>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="right">
                                             Select Community: 
                                         </td>
                                         <td align="left">
                                             <asp:DropDownList ID="dropDialogCommunityCategory" runat="server" AutoPostBack="true"></asp:DropDownList>
                                         </td>
                                     </tr>
                                 </table>
                                
                            </div>

                            <div style="margin: 0px; padding-left: 45px; width: 85%;">
                                <asp:GridView ID="gvCommunityCategories" runat="server" TabIndex="-1" AllowSorting="false"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" CssClass="table table-borderless border-0" AutoGenerateColumns="False"  ShowHeader="true" 
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                    <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    
                                    <HeaderStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommunityCategoryId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMCategoryID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCommunityCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>' Visible="true"></asp:Label>
                                                 <asp:Label ID="lblCommunityCategoryAssociatedCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CategoryCount")%>' Visible="false"></asp:Label>
                                               
                                            </ItemTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Left"  />
                                            <HeaderStyle  />
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Include" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                              <asp:Checkbox ID="chkInclude"  OnCheckedChanged="chkInclude_CheckedChanged" AutoPostBack="true" runat="server" ValidationGroup='<%# DataBinder.Eval(Container, "DataItem.VMCategoryId") %>' Checked='<%# DataBinder.Eval(Container, "DataItem.IsAssigned") %>'></asp:Checkbox>
                                            </ItemTemplate>
                                            
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

           <div class="modal-panel" id="jqdialogUsers" style="display: none;  background-color: rgb(217, 217, 217);  padding: 5px !important; margin: 1px !important; text-align: center; height: auto !important;">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upUsers" runat="server">
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropDialogUsers" EventName="SelectedIndexChanged" />
                         
                    </Triggers>
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 15px;" id="divUserError" runat="server" visible="false">
                                <asp:Label ID="lblUserError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                             <div style="text-align:center; margin-left:20px">
                                 <table>
                                     <tr style="display:none;">
                                         <td align="right"></td>
                                         <td>
                                             <asp:Button ID="btnAddNewUser" runat="server" Text="ADD NEW USER"  CssClass="btn" style="margin-bottom:5px;"  />
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="right">
                                             Select User: 
                                         </td>
                                         <td align="left">
<asp:DropDownList ID="dropDialogUsers" runat="server" AutoPostBack="true"></asp:DropDownList>
                                         </td>
                                         <td>
                                              <asp:Button ID="btnAddUserCommunity" runat="server" Text="Add Community"  CssClass="btn" style="margin-left:5px;"  />
                                         </td>
                                     </tr>
                                     <tr id="trUserCommunity" runat="server">
                                         <td align="right">
                                            Community: 
                                         </td>
                                         <td align="left">
                                             <asp:DropDownList ID="dropDialogUserCommunities" runat="server" AutoPostBack="false"></asp:DropDownList>
                                         </td>
                                     </tr>
<tr  id="trUserAdmin" runat="server">
                                         <td align="right">
                                            
                                         </td>
                                         <td align="left">
                                             <asp:CheckBox ID="chkUserAdmin" runat="server" Text="Administrator" />
                                         </td>
                                     </tr>
                                     <tr id="trUserCommunitySubmit" runat="server">
                                       <td align="right">
                                            
                                         </td>
                                          <td align="left">
                                              <asp:Button ID="btnUserCommunitySubmit" runat="server" Text="Submit"  CssClass="btn" style="margin-left:0px;"  />
                                         </td>
                                     </tr>
                                 </table>
                                
                            </div>

                            <div style="margin: 0px; padding-left: 45px; width: 85%; margin-top:5px;">
                                <asp:GridView ID="gvUserCommunities" runat="server" TabIndex="-1" AllowSorting="false"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" CssClass="table table-borderless border-0" AutoGenerateColumns="False"  ShowHeader="true" 
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="false">
                                    <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    
                                    <HeaderStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Community" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                               <asp:Label ID="lblCommunityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCommunity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommName")%>' Visible="true"></asp:Label>
                                              
                                                                                          </ItemTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Left"  />
                                            <HeaderStyle  />
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admin" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                              <asp:Checkbox ID="chkCommunityInclude"  OnCheckedChanged="chkCommunityInclude_CheckedChanged" AutoPostBack="true" runat="server" ValidationGroup='<%# DataBinder.Eval(Container, "DataItem.CommId") %>' Checked='<%# DataBinder.Eval(Container, "DataItem.Admin") %>'></asp:Checkbox>
                                            </ItemTemplate>
                                            
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkUserCommunityDelete" runat="server" CssClass="DeleteImage" CommandName="DeleteCommunity" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.CommId") %>' OnClientClick="javascript:if(!confirm('Are you sure you want to de associate this Community?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                          
                                            </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal-panel" id="jqdialogABPriority" style="display: none; top: 30% !important; background: #d9d9d9">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upAB" runat="server">
                    <ContentTemplate>
                        <div>
                            <div class="section-title" style="font-size: 16px;" runat="server" visible="true">
                                <asp:Label ID="lblABTitle" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="section-title" style="font-size: 15px;" id="divABError" runat="server" visible="false">
                                <asp:Label ID="lblABError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <div style="margin: 0px; width: 100%;">
                                <asp:GridView ID="gvAffectsBenefits" CssClass="table table-borderless border-0" runat="server" TabIndex="-1" AllowSorting="false" Width="100%"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" AutoGenerateColumns="False" BorderStyle="Solid" ShowHeader="true" 
                                     AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                   <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="Affects" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblVMType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMType")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEditVMType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMType")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEditCS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtCS" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="Required" ControlToValidate="txtCS" ValidationGroup="EditAB" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddCS" runat="server" MaxLength="50" onkeydown="return benefitkey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ControlToValidate="txtAddCS" ValidationGroup="AddAB" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAffectsSort" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CSsort")%>' Visible="true" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCSSort" runat="server" MaxLength="3" Text='<%# DataBinder.Eval(Container, "DataItem.CSsort")%>' Width="35px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtCSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="EditAB"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddCSSort" runat="server" Width="35px" MaxLength="3" onkeydown="return benefitkey(event);"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtAddCSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddAB"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <ItemStyle  Width="10px" HorizontalAlign="Center" />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Benefits" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefits" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditBenefits" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ErrorMessage="Required" ControlToValidate="txtFS" ValidationGroup="EditAB" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddFS" runat="server" MaxLength="50" onkeydown="return benefitkey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required" ControlToValidate="txtAddFS" ValidationGroup="AddAB" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefitsSort" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSsort")%>' Visible="true" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFSSort" runat="server" MaxLength="3" Text='<%# DataBinder.Eval(Container, "DataItem.FSsort")%>' Width="35px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtFSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="EditAB"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddFSSort" runat="server" Width="35px" MaxLength="3" onkeydown="return benefitkey(event);"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtAddFSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddAB"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <ItemStyle  Width="15px" HorizontalAlign="Center" />
                                            <HeaderStyle  Width="15px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkABEdit" runat="server" Text="Edit"  Font-Bold="false" Font-Underline="false" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkABUpdate" runat="server" Text="Update"  Font-Bold="false" Font-Underline="false" CommandName="Update" ValidationGroup="EditAB"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkABCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkABDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Affects/Benefits?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddAB" runat="server"  Text="Add New" CommandName="AddNew"  CssClass="btn"  ValidationGroup="AddAB" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="75px" />
                                            <HeaderStyle  Width="75px" CssClass="text-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal-panel" id="jqdialogWWDCPriority" style="display: none; top: 30% !important; background: #d9d9d9">
        <div class="modal-panel-container">
            <div class="modal-panel">
                <asp:UpdatePanel ID="upWWDC" runat="server">
                    <ContentTemplate>
                        <div>
                            <div id="divWWDCTitle" class="section-title" style="font-size: 16px;" runat="server" visible="true">
                                <asp:Label ID="lblWWDCTitle" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="section-title" style="font-size: 15px;" id="divWWDError" runat="server" visible="false">
                                <asp:Label ID="lblWWDCError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <div style="margin: 0px; width: 100%;">
                                <asp:GridView ID="gvWWDC" runat="server" TabIndex="-1" AllowSorting="false" Width="100%"
                                    ShowHeaderWhenEmpty="true" CellPadding="0" AutoGenerateColumns="False" CssClass="table table-borderless border-0" ShowHeader="true" 
                                    AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                                     <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="WWDC" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblVMType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMType")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEditVMType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VMType")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblEditCS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtCS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CS")%>' MaxLength="50"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddCS" runat="server" MaxLength="50" onkeydown="return wwdckey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ControlToValidate="txtAddCS" ValidationGroup="AddWWDC" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left"  />
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAffectsSort" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CSsort")%>' Visible="true" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCSSort" runat="server" MaxLength="3" Text='<%# DataBinder.Eval(Container, "DataItem.CSsort")%>' Width="35px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtCSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="EditWWDC"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddCSSort" runat="server" Width="35px" MaxLength="3" onkeydown="return wwdckey(event);"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtAddCSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddWWDC"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="10px" />
                                            <HeaderStyle Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WWDF" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefits" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditBenefits" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtFS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FS")%>' MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ErrorMessage="Required" ControlToValidate="txtFS" ValidationGroup="EditWWDC" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddFS" runat="server" MaxLength="50" onkeydown="return wwdckey(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required" ControlToValidate="txtAddFS" ValidationGroup="AddWWDC" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign ="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefitsSort" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FSsort")%>' Visible="true" Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFSSort" runat="server" MaxLength="3" Text='<%# DataBinder.Eval(Container, "DataItem.FSsort")%>' Width="35px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtFSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="EditWWDC"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddFSSort" runat="server" Width="35px" MaxLength="3" onkeydown="return wwdckey(event);"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtAddFSSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddWWDC"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="10px" />
                                            <HeaderStyle  Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkABEdit" runat="server" Text="Edit"  Font-Bold="false" CausesValidation="false" Font-Underline="false" CommandName="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkABUpdate" runat="server" Text="Update"  Font-Bold="false" ValidationGroup="EditWWDC" Font-Underline="false" CommandName="Update"></asp:LinkButton>
                                                <asp:LinkButton ID="lnkABCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CausesValidation="false" CommandName="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemStyle  />
                                            <HeaderStyle  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkABDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this WWDC/WWDF?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAddWWDC" runat="server" Text="Add New"  CommandName="AddNew" CssClass="btn" ValidationGroup="AddWWDC" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="75px" />
                                            <HeaderStyle  Width="75px" CssClass="text-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div class="wrap">
                <%--<div class="section-title">Category Maintenance</div>--%>
                <div style="width: 100%; float: left; text-align: center;">
                    <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Blue" Font-Bold="true" Font-Size="15px"></asp:Label>
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="15px"></asp:Label>
                </div>
                <div style="padding: 30px 0 0 0px;">
                    <div style="float: left; width: 100%">
                        <asp:Button ID="lnkAddGroup" runat="server"  CssClass="btn"  Style="margin-right: 0px;" Text="GROUP MAINTENANCE">
                            
                        </asp:Button>

                        <asp:Button ID="lnkAddCategory" runat="server"  CssClass="btn"  Style="margin-right: 0px;" Text="CATEGORY MAINTENANCE">
                            
                        </asp:Button>
                        <asp:Button ID="lnkAddCommunity" runat="server"  CssClass="btn"  Style="margin-right: 0px;" Text="COMMUNITY MAINTENANCE">
                            
                        </asp:Button><br />
                        <asp:Button ID="lnkAddCommunityCategory" runat="server"  CssClass="btn"  Style="margin-right: 0px; margin-top:5px;" Text="COMMUNITY CATEGORY MAINTENANCE">
                            
                        </asp:Button>
                         <asp:Button ID="lnkAddUser" runat="server"  CssClass="btn"  Style="margin-right: 0px; margin-top:5px;" Text="USER COMMUNITY ASSOCIATIONS">
                            
                        </asp:Button>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 20px;">
                        <asp:Label ID="lblCustLabel" runat="server" Text="PRIORITIES FOR GROUP" Font-Bold="true" ForeColor="Black" Font-Size="16px"></asp:Label>
                        <asp:DropDownList ID="dropGroups" runat="server" widht="100px" AutoPostBack="true"></asp:DropDownList>
                                              <asp:Label ID="lblCustLabel1" runat="server" Text="AND CATEGORY" Font-Bold="true" ForeColor="Black" Font-Size="16px"></asp:Label>
                        <asp:DropDownList ID="dropCategories" runat="server"  style="max-width:350px; min-width:150px;"  AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div style="float: left; width: 100%; padding-top: 20px; font-size: 15px;">
                        <asp:GridView ID="gvPriorities" runat="server" TabIndex="-1" CssClass="table table-borderless border-0" AllowSorting="false" DataKeyNames="PriorityID"
                            ShowHeaderWhenEmpty="true" CellPadding="0" AutoGenerateColumns="False"  ShowHeader="true" 
                             AllowPaging="false" PagerSettings-Visible="false" ShowFooter="true">
                            <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Priority" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblPriority" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityName")%>' Visible="true"></asp:Label>
                                        <asp:Label ID="lblPriorityCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityCount")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEditPriorityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblEditPriorityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityName")%>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtPriority" MaxLength="50" runat="server" Width="335px" Text='<%# DataBinder.Eval(Container, "DataItem.PriorityName")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddPriority" MaxLength="50" runat="server" Width="300px" onkeydown="return prioritykey(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtAddPriority" ValidationGroup="AddPriority" ForeColor="Red" Font-Bold="true" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"  />
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Affects/Benefits" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAffectsBenefits" runat="server" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="AffectsBenefits" Text="Manage"  Font-Bold="false" Font-Underline="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"  />
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WWDC/WWDF" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkWWDC" runat="server" Text="Manage" CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' CommandName="WWDC" Font-Bold="false" Font-Underline="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"  />
                                   
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sort" ItemStyle-VerticalAlign="Middle" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrioritySort" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Sort")%>' Visible="true" Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Text='<%# DataBinder.Eval(Container, "DataItem.Sort")%>' Width="30px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtSort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddPriority"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddPrioritySort" runat="server" Width="30px" MaxLength="3" onkeydown="return prioritykey(event);"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numbers only" ControlToValidate="txtAddPrioritySort" ValidationExpression="^[0-9]+$" Font-Bold="true" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="AddPriority"></asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    <HeaderStyle  Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPriorityEdit" runat="server" Text="Edit"  Font-Bold="false" Font-Underline="false" CommandName="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkPriorityUpdate" runat="server" Text="Update"  Font-Bold="false" Font-Underline="false" CommandName="Update"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkPriorityCancel" runat="server" Text="Cancel"  Font-Bold="false" Font-Underline="false" CommandName="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <%--<FooterTemplate>
                                       
                                    </FooterTemplate>--%>
                                    <ItemStyle HorizontalAlign="Center"  />
                                    <HeaderStyle HorizontalAlign="Center" CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Button ID="lnkPriorityDelete" runat="server" CssClass="DeleteImage" CommandName="Delete" OnClientClick="javascript:if(!confirm('Are you sure you want to delete this Priority?')) return false;"></asp:Button>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <asp:Button ID="btnAddPriority" CssClass="btn" runat="server" Text="Add New" CommandName="AddNew"  ValidationGroup="AddPriority" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center"  Width="75px" />
                                            <HeaderStyle  Width="75px" CssClass="text-center" />
                                        </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="Progress">
                        <asp:Image ID="Image1" ImageUrl="~/images/processing.png" runat="server" Style="vertical-align: middle" />
                        Loading....
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
           </div>
    <style type="text/css">
        .ui-dialog {
            z-index:999999!important;
        }
                 .DeleteImage {
            background: url("Images/DeleteSmall_up.png") no-repeat scroll 0 0 transparent;
            border: 0 none;
            height: 17px;
            width: 14px;
            cursor: pointer;
            /*margin-top :5px !important;*/
        }
                 .ui-widget-content a {
    color: #5addbe;
}
                 .ui-widget {
     font-family: inherit; 
     font-size: inherit; 
}
    </style>
     <script type="text/javascript">
         function HideCategoryRow(id) {
             $('#'+id+' > tbody > tr:eq(1)').remove();
           // alert(id);
        }
        $(document).ready(function () {
            $("#jqdialogCategory").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'Category Maintenance',
                width: '500px',
                position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {

                    $(this).parent().appendTo("form");
                }
            });
             $("#jqdialogGroup").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'Group Maintenance',
                width: '500px',
                position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });
             $("#jqdialogCommunity").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'Community Maintenance',
                width: '500px',
                position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {

                    $(this).parent().appendTo("form");
                }
            });
             $("#jqdialogCommunityCategory").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'Community Category Maintenance',
                width: '500px',
                position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {

                    $(this).parent().appendTo("form");
                }
            });
             $("#jqdialogUsers").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'User Maintenance',
                width: '550px',
                position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {

                    $(this).parent().appendTo("form");
                }
            });
        });

        function jqdialogShowControl() {
            $("#jqdialogCategory").dialog("open");
            return false;
        }
        function jqdialogHideControl() {
            $("#jqdialogCategory").dialog("close");
            return false;
         }
         function jqdialogShowCommunityControl() {
            $("#jqdialogCommunity").dialog("open");
            return false;
        }
        function jqdialogHideCommunityControl() {
            $("#jqdialogCommunity").dialog("close");
            return false;
         }
          function jqdialogShowCommunityCategoryControl() {
            $("#jqdialogCommunityCategory").dialog("open");
            return false;
        }
        function jqdialogHideCommunityCategoryControl() {
            $("#jqdialogCommunityCategory").dialog("close");
            return false;
        }
          function jqdialogShowGroupControl() {
            $("#jqdialogGroup").dialog("open");
            return false;
        }
        function jqdialogHideGroupControl() {
            $("#jqdialogGroup").dialog("close");
            return false;
         }
          function jqdialogShowUserControl() {
            $("#jqdialogUsers").dialog("open");
            return false;
        }
        function jqdialogHideUserControl() {
            $("#jqdialogUsers").dialog("close");
            return false;
        }
        $(document).ready(function () {
            $("#jqdialogABPriority").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'MANAGE AFFECTS/BENEFITS',
                width: 'auto',
                 position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });

        });

        function jqdialogABShowControl() {
            $("#jqdialogABPriority").dialog("open");
            return false;
        }
        function jqdialogABHideControl() {
            $("#jqdialogABPriority").dialog("close");
            return false;
        }

        $(document).ready(function () {
            $("#jqdialogWWDCPriority").dialog({
                bgiframe: true,
                autoOpen: false,
                modal: true,
                stack: true,
                title: 'MANAGE WWDC/WWDF',
                width: 'auto',
                 position: {
        my: "center center",
        at: "center center",
        of: window
    },
                dialogClass: 'no-close',
                closeOnEscape: false,
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                }
            });

        });

        function jqdialogWWDCShowControl() {
            $("#jqdialogWWDCPriority").dialog("open");
            return false;
        }
        function jqdialogWWDCHideControl() {
            $("#jqdialogWWDCPriority").dialog("close");
            return false;
        }
         function prioritykey(e) {
              if (e.keyCode == 13) {
           // alert("Enter pressed");
                  $("#ContentPlaceHolder1_gvPriorities_btnAddPriority").trigger("click");
            return false; // prevent the button click from happening
        }
         }
         function groupkey(e) {
              if (e.keyCode == 13) {
           // alert("Enter pressed");
                  $("#ContentPlaceHolder1_gvGroups_btnAddGroup").trigger("click");
            return false; // prevent the button click from happening
        }
         }
         function categorykey(e) {
              if (e.keyCode == 13) {
           // alert("Enter pressed");
                  $("#ContentPlaceHolder1_gvCategories_btnAddCategory").trigger("click");
            return false; // prevent the button click from happening
        }
         }
          function benefitkey(e) {
              if (e.keyCode == 13) {
           // alert("Enter pressed");
                  $("#ContentPlaceHolder1_gvAffectsBenefits_btnAddAB").trigger("click");
            return false; // prevent the button click from happening
        }
         }
          function wwdckey(e) {
              if (e.keyCode == 13) {
           // alert("Enter pressed");
                  $("#ContentPlaceHolder1_gvWWDC_btnAddWWDC").trigger("click");
            return false; // prevent the button click from happening
        }
         }
    </script>
</asp:Content>
