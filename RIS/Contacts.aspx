<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Contacts.aspx.vb" Inherits="RIS.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    
    <meta name="description" content=""/>
    <meta name="author" content="" />
    <link rel="icon" href="favicon.ico" />
    <title>REIMAGINE</title>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <nav class="col-md-2 d-none d-md-block  sidebar mt-5">
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
        </nav>
        <div  class="col-md-9 ml-sm-auto col-lg-10 pt-5 px-5" id='maincontent'>
			<h1>Welcome Back, lisa.</h1>
			<div class='row'>
				<div class='col-md-7'>
					<table class="table">
					  <thead>
						<tr>
						  <th scope="col">Recent Activity</th>
						  <th scope="col" ></th>
						</tr>
					  </thead>
					  <tbody>
						<tr>
						  <td> Today</td>
						  <td >A <a href='#'>To Do Action</a> was created for <a href='#'>Sam Smith</a></td>
						</tr>					
						<tr>
						  <td> Today</td>
						  <td >A <a href='#'>To Do Action</a> was created for <a href='#'>Sam Smith</a></td>
						</tr>					
						<tr>
						  <td> Today</td>
						  <td >A <a href='#'>To Do Action</a> was created for <a href='#'>Sam Smith</a></td>
						</tr>					
						<tr>
						  <td> Today</td>
						  <td >A <a href='#'>To Do Action</a> was created for <a href='#'>Sam Smith</a></td>
						</tr>					
					  </tbody>
					</table>
				</div>
				<div class='col-md-5'>
					<table class="table">
					  <thead>
						<tr>
						  <th scope="col">To Do</th>
						  <th scope="col" ></th>
						</tr>
					  </thead>
					  <tbody>
						<tr>
						  <td class='underline'> Today</td>
						  <td class='text-center'><a href='#'>Edit</a></td>
						</tr>					
						<tr>
						  <td class='underline'> Today</td>
						  <td class='text-center'><a href='#'>Edit</a></td>
						</tr>					
						<tr>
						  <td class='underline'> Today</td>
						  <td class='text-center'><a href='#'>Edit</a></td>
						</tr>					
						<tr>
						  <td class='underline'> Today</td>
						  <td class='text-center'><a href='#'>Edit</a></td>
						</tr>					
					  </tbody>
					</table>
				</div>
			</div>
		</div>
      </div>
</asp:Content>
