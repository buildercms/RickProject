<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ValueMapNew.aspx.vb" Inherits="RIS.ValueMapNew" %>
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
        </nav>
        <div  class="col-md-9 ml-sm-auto col-lg-10 pt-5 px-5" id='maincontent'>
			<ul class='arrow-steps clearfix'>
				<li class='step'>
					<a href='overview.html'><span>Overview</span></a>
				</li>
				<li class='step active'>
					<a href='valuemap.html'><span><span class='first'>Step1</span>
					<span>View/Create<br/>Value Map</span></span></a>
				</li>
				<li  class='step'>
					<a href='overview.html'><span><span class='first'>Step1</span>
					<span>View/Create<br/>Value Map</span></span></a>
				</li>
				<li  class='step'>
					<a href='overview.html'><span><span class='first'>Step1 </span>
					<span>View/Create<br/>Value Map</span></span></a>
				</li>
			</ul>
			<div class='row'>
				<div class='col-md-7'>
					<h1>Sam Smith</h1>
				</div>
				<div class='col-md-5'>
					<div class='clearfix'>
						<button class="btn float-right " type="button">Edit Value Map</button>							
					</div>					
				</div>
				<div class='col-md-12'>
				<p>What 3 things are most important to you in new place to live?</p>
				</div>
			</div>
			<div class='row'>
				<div class='col col1'>
					<p class='text-center'>Current Impacts</p>
					<div class='square'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
					
					</div>
					<div class='square'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
					</div>
					<div class='square'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
					</div>
					
				</div>
				<div class='col col2'>
					<p class='text-center'>Current Description</p>
					<div class='square red'>
						<div class='text'>
							<p><br/></p>
							<p><br/></p>
							<p>Hard to do <br/>Markup</p>
						</div>
					</div>
					<div class='square red'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
					</div>
					<div class='square red'>
						<span>6</span>
					</div>
				</div>
				<div class='col col3 text-center'>
					<p class='text-center'>Priorities</p>
					<div class='circle '>
					</div>
					<div class='circle '>
					</div>
					<div class='circle '>
					</div>
				</div>
				<div class='col col4'>
					<p class='text-center'>Future Description</p>
					<div class='square'>
						<span>7</span>
					</div>
					<div class='square'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
						<span>4</span>
					</div>
					<div class='square'>
						<div class='text'>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
							<p>Hard to do <br/>Markup</p>
						</div>
					</div>
				</div>
				<div class='col col5 '>
					<p class='text-center'>Future Impacts</p>
					<div class='square'>
					</div>
					<div class='square'>
					
					</div>
					<div class='square'>
					
					</div>
				</div>
			</div>
		</div>
      </div>
</asp:Content>
