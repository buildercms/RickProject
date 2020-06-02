<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ViewToDo.aspx.vb" Inherits="RIS.ViewToDo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:HiddenField ID="hidCustomerId" runat="server" />
    <asp:HiddenField ID="hidToDoId" runat="server" />
     <div  class="right" id='maincontent'>
         <div class='row'>
                <div class='col-md-7'>
                    <h2 id="todoTitle" runat="server"></h2>
                </div>
                <div class='col-md-5'>
                    <div class='clearfix'>
                        <asp:Button  class="btn float-right" ID="btnEditToDo" OnClick="btnEditToDo_Click" runat="server" Text="Edit To Do" />
                    </div>
                   

                </div>
            </div>
         <div class="box">
           <div class='row' >
               <div class="col-md-8">
                   <p class="skyblue">Description:</p>
                   <asp:Label ID="lblDescription" runat="server"></asp:Label>
               </div>
               <div class='col-md-4'>

                    
                        <p class="skyblue">Details:</p>
                        <table class='table'>
                            <tr>
                                <td>Client:</td>
                                <td>
                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Due Date:</td>
                                <td>
                                    <asp:Label ID="lblDueDate" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Status:</td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                            </tr>
                           
                        </table>
                    </div>
                    
               
               </div>
             </div>
         </div>
    <style type="text/css">
        #home {
            background-color:#cecece;
        }
    </style>
</asp:Content>
