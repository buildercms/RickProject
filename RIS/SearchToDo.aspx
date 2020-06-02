<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SearchToDo.aspx.vb" Inherits="RIS.SearchToDo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
     <div  class="right" id='maincontent'>
         <div class="row">
             <div class="col-md-12">
                 <h2>To Do's</h2>
             </div>
         </div>
        
          
		

					
    <div class="form-row" id ="divSearchGrid" runat="server" > 
              <div class="col-md-12" style="margin-bottom:5px;">
             <div class="card">
                   <div class="card-header" style="padding:0.75rem 0.75rem">
							<p style="color:#62DCBF; margin-bottom:0px" class="font-weight-bold">
							  To Do's Due Today
							</p>
						  </div>
                  <div class="card-body" style="padding:0px;">
                      <asp:GridView ID="gvTodayToDos" runat="server" AutoGenerateColumns="false" CssClass="table table-borderless" OnRowDataBound="gvTodayToDos_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataText="No To Do's found." EmptyDataRowStyle-HorizontalAlign="Center">
                          <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="To Do">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidToDoId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Id") %>' />
                            <asp:HiddenField ID="hidCustId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.CustomerId") %>' />
                             <asp:HiddenField ID="hidDueDate" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.DueDate") %>' />
                            <a id="aToDoViewLink" runat="server"  ><%# DataBinder.Eval(Container, "DataItem.Title") %> </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:HyperLinkField HeaderText="Client" DataTextField="CustomerName" DataNavigateUrlFields="CustomerId"
                    DataNavigateUrlFormatString="~/OverView.aspx?cid={0}" />
                     <%--<asp:BoundField HeaderText="Client" DataField="CustomerName" />--%>
                    <asp:BoundField HeaderText="Due Date" DataField="DueDate" DataFormatString="{0:MM/dd/yy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="complete" />
                     <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblToDoStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ToDoStatus") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                   <%-- <asp:BoundField HeaderText="Status" DataField="ToDoStatus" />--%>
                      <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <a  id="aToDoEditLink" runat="server" >Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCompleteStatus" CssClass="completetodo"  todoid='<%# DataBinder.Eval(Container, "DataItem.Id") %>' AutoPostBack="true" OnCheckedChanged="chkCompleteStatus_CheckedChanged"  runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" CssClass="complete"  />
                    </asp:TemplateField>
                  
                </Columns>
                          
            </asp:GridView>
                      </div>
                 </div>
           
        </div>
        <div class="col-md-12">
             <div class="card">
                   <div class="card-header" style="padding:0.75rem 0.75rem">
							<p style="color:#62DCBF; margin-bottom:0px" class="font-weight-bold">
							  Upcoming To Do's
							</p>
						  </div>
                  <div class="card-body" style="padding:0px;">
                      <asp:GridView ID="gvToDos" runat="server" AutoGenerateColumns="false" CssClass="table table-borderless" OnRowDataBound="gvToDos_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataText="No To Do's found." EmptyDataRowStyle-HorizontalAlign="Center">
                          <HeaderStyle CssClass="first" BackColor="#62DCBF" ForeColor="white" />
                <Columns>
                    <asp:TemplateField HeaderText="To Do">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidToDoId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Id") %>' />
                            <asp:HiddenField ID="hidCustId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.CustomerId") %>' />
                             <asp:HiddenField ID="hidDueDate" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.CustomerId") %>' />
                            <a id="aToDoViewLink" runat="server"  ><%# DataBinder.Eval(Container, "DataItem.Title") %> </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="Client" DataTextField="CustomerName" DataNavigateUrlFields="CustomerId"
                    DataNavigateUrlFormatString="~/OverView.aspx?cid={0}" />
                     <%--<asp:BoundField HeaderText="Client" DataField="CustomerName" />--%>
                    <asp:BoundField HeaderText="Due Date" DataField="DueDate" DataFormatString="{0:MM/dd/yy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="complete" />
                     <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblToDoStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ToDoStatus") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                   <%-- <asp:BoundField HeaderText="Status" DataField="ToDoStatus" />--%>
                      <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <a  id="aToDoEditLink" runat="server" >Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCompleteStatus" CssClass="completetodo"  todoid='<%# DataBinder.Eval(Container, "DataItem.Id") %>' AutoPostBack="true" OnCheckedChanged="chkCompleteStatus_CheckedChanged"  runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" CssClass="complete"  />
                    </asp:TemplateField>
                  
                </Columns>
                          
            </asp:GridView>
                      </div>
                 </div>
           
        </div>
       
    </div>
        </div>
    <script type="text/javascript">
        $(function () {
            $("ul[id$='ulSideBar']").find("li").each(function (){
                $(this).removeClass("active");
            });
            $("ul[id$='ulSideBar']").find("li[id$='liTodo']").addClass("active")
            
        });
        function EvaluationEmail(customerId, todoId) {
            if (confirm("Are you sure you want to send the Evaluation Email?")) {
                var dataValue = '{todoID: "' + todoId + '", customerID: "' + customerId + '"}';
                $.ajax({
                    url: "SearchToDo.aspx/EvaluateToDo",
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
        function CompareDecideEmail(customerId, todoId) {
             if (confirm("Are you sure you want to send the Discuss and Decide Email?")) {
                var dataValue = '{todoID: "' + todoId + '", customerID: "' + customerId + '"}';
                $.ajax({
                    url: "SearchTodo.aspx/CompareDecideToDo",
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
    <style type="text/css">
        .form-control {
            background-color:white !important;
        }
        .table-borderless {
 border:none;
}
       
    </style>
    <style type="text/css">
        #home {
            background-color:#cecece;
        }
        .complete {
            text-align:center;
        }
    </style>
    
</asp:Content>
