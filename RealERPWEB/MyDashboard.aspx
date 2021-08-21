<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MyDashboard.aspx.cs" Inherits="RealERPWEB.MyDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel='stylesheet' href='assets\css\style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\colors.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\comments.css' type='text/css' media='all' />
    <link rel='stylesheet' id='responsiveslides-css' href='assets\css\responsiveslides.css' type='text/css' media='all' />
    <link rel='stylesheet' id='reponsive-css' href='assets\css\reponsive.css' type='text/css' media='all' />
    <link rel='stylesheet' id='animate-custom-css' href='assets\css\animate-custom.css' type='text/css' media='all' />
   
     
 
     
    <style>
        @font-face {
            font-family: 'Agency FB';
            src: url('fonts/AgencyFB-Bold.eot?#iefix') format('embedded-opentype'), url('fonts/AgencyFB-Bold.woff') format('woff'), url('fonts/AgencyFB-Bold.ttf') format('truetype'), url('fonts/AgencyFB-Bold.svg#AgencyFB-Bold') format('svg');
            font-weight: normal;
            font-style: normal;
        }


        body {
            background: #3495f3 url("~/assets/images/deshbordbd.png") repeat scroll 0 0 / cover !important;
        }

        .metro-panel .space .boxdsh {
            left: 45% !important;
            top: 71px;
            padding-bottom: 10px;
        }


        .metro-panel .boxdsh1 {
            margin-left: 39%;
        }

        .metro-panel .boxdsh2 {
            left: 45% !important;
            top: 36px;
            padding-bottom: 10px;
        }

        .asit_container {
            width: 100% !important;
        }

        .container {
            width: 100% !important;
        }

        .pnlflowchart {
            background-image: url("Image/bg.PNG") !important;
        }

        .fadeInRightBig h3 {
            /*background: #232526;*/ /* fallback for old browsers */
            /*background: -webkit-linear-gradient(to left, #232526 , #414345);*/ /* Chrome 10-25, Safari 5.1-6 */
            /*background: linear-gradient(to left, #232526 , #414345);*/ /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            /*color: #fff;
                                    font-family: 'Agency FB';
                                    font-size: 24px;
                                    font-weight: bold;
                                    line-height: 40px;
                                    margin: 5px 0 0;
                                    text-align: center;
                                    text-decoration: underline;*/
            font-family: 'Agency FB';
            background: #d6cfbf;
            /*border: 1px solid #699f44;*/
            /*box-shadow: 0 0 4px 2px #bec9b6 inset;*/
            color: #7f6930;
            font-family: "Agency FB";
            font-size: 22px;
            line-height: 40px;
            margin: 5px -3px 0;
            text-align: center;
            text-decoration: underline;
            font-weight: normal;
        }

        .fa-4x {
            font-size: 18px !important;
        }

        .metro-panel .space > div {
            min-height: 70px;
        }

        .col103 {
            background: #046971 !important;
        }

     
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    



    

    <div class="myBody">

        <asp:Label ID="lblprintstk" runat="server" Visible="false"></asp:Label>


        <div id="spaces-main" class="pt-perspective">
            <section class="page-section home-page">
                <div class="container">
                    <div class="row metro-panel">
                        <div class="large-12 columns">
                            <div class="row menu-row boxShadow1">
                                <div class="large-12 columns">
                                    <div class="asit_ComLogo">
                                        <h1>
                                            <asp:Image ID="Image1" CssClass="Companylogo" runat="server" ImageUrl="~/Image/LOGO1.PNG" />
                                        </h1>
                                    </div>
                                    <div class="asit_ComInfo">
                                        <div class="AppscompName pading5px">
                                            <asp:Label ID="LblGrpCompany" CssClass="lblCompName" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="lbladd" CssClass="LblGrpCompanyAdress" runat="server" Text="Label"> </asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pull-right">

                                        <div class="asit_userinfoPanel">
                                            <div class="userMenu pull-right">
                                                <ul>
                                                    <li class="dropdown">
                                                        <a data-toggle="dropdown" data-target="#" href="#">
                                                            <asp:Image ID="UserImg" CssClass="img-circle" ImageUrl="~/Image/LOGO1.JPG" runat="server" AlternateText="Image Not Found" Style="width: 60px; height: 50px;" /></a>

                                                        <ul class="well dropdown-menu userInformation" aria-labelledby="dropdownMenu">
                                                            <li>
                                                                <div>
                                                                    <asp:Image ID="UserImg2" CssClass="img-circle" ImageUrl="~/Image/LOGO1.JPG" runat="server" AlternateText="Image Not Found" Style="width: 100px; height: 80px; float: left; margin-right: 15px;" />
                                                                    <p>
                                                                        <asp:Label ID="lblmoduleid" Style="display: none;" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p>
                                                                        <asp:Label ID="lblLoginInfo" runat="server">User ID : ADMIN</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <a class="btn-primary logoutBtn btn" id="Label3" href='<%=this.ResolveUrl("~/Login.aspx")%>'>
                                                                            <span class="glyphicon glyphicon-off"></span>Log out</a>

                                                                    </p>





                                                                </div>
                                                            </li>
                                                        </ul>









                                                    </li>


                                                </ul>
                                            </div>

                                        </div>
                                    </div>


                                </div>



                            </div>
                        </div>














                        <asp:Panel runat="server" CssClass="pnlflowchart" ID="Panel8" Visible="false" >


                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }



                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }




                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .col103 {
                                    color: #046971;
                                }

                                /*.color-1:hover { 
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-83:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-14:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                           
                                .color-72:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }



                                .col103:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }



                                .color-84:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-82:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-81:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-80:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-79:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-78:hover {
                                    background-color: #c27100 !important;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    /*padding: 10px 15px !important;*/
                                /*margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }*/

                                /*  .color-77:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-76:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }



                                .color-75:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-74:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-73:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-72:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }

                                .color-71:hover {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                   
                                }*/


                                .space div:hover {
                                    background-color: #c27100 !important;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100) !important;
                                    color: #000 !important;
                                    /*padding: 10px 15px !important;*/
                                    /*margin: 2px 0 !important;*/
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                    cursor: pointer;
                                }





                                /*metro-panel .space > div {
                                    min-height: 75px;
                                    text-align: center !important;
                                  
                                    overflow: hidden;
                                    margin: 3px -3px;
                                }*/

                                /* position: relative; */

                                /*metro-panel .space > div:hover
                                {
                                    background-color: #c27100;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100);
                                    color: #000 !important;
                                    padding: 10px 15px !important;
                                    margin: 2px 0 !important;
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                }*/
                            </style>

                            <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Visible="false" RepeatColumns="4"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                            </asp:RadioButtonList>


                            <div class="row boxShadow " style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns"></div>
                                <div class="boxInn">


                                    <div class="four large-4 columns">
                                        <h3>

                                            <a href="LandingPage.aspx" target="_blank" > Modules</a>


                                        </h3>
                                        <div class="row">

                                            <div class='twelve large-12 columns space'>
                                                <div class='color-71'>
                                                    <asp:LinkButton ID="lnkbtnStepOpra" runat="server" OnClick="lnkbtnStepOpra_Click">
                                                        <span class='box-title boxtex'>Steps Of Operation</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lnkbtnAbp_Click">
                                               
                                                    <span class='box-title'>Business Plan</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                          
                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">
                                                    <span class='box-title'>Land Feasibility</span>
                                                    <br/>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class=' color-81'>
                                                    <asp:LinkButton ID="lnkbtnRowMatInventory" runat="server" OnClick="lnkbtnMatr_Click">
                                                    <span class='box-title'>Budget</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-84'>

                                                    <asp:LinkButton ID="lnkbtnpurch" runat="server" OnClick="lnkbtnPlaning_Click">
                                                    <span class='box-title'>Project Planing</span>
                                                    <br>
                                                    <i class='fa-money fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="linkbtnprodMin" runat="server" OnClick="btnImp_Click">
                                                    <span class='box-title'>Project Implementation</span>
                                                    <br/>
                                                    <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="linkcentral" runat="server" OnClick="linkcentral_Click">
                                                    <span class='box-title'>Central Wearhouse</span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-78'>
                                                    <asp:LinkButton ID="lnkbtnGInventory" runat="server" OnClick="lnkbtnGoodsInv_Click">
                                                    <span class='box-title'>Inventory</span>
                                                    <br/>
                                                    <i class='fa-suitcase fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-79'>
                                                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="btnPur_Click">
                                                    <span class='box-title'>Procurement</span>
                                                    <br/>
                                                    <i class='fa-credit-card fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="LinkButton75" runat="server" OnClick="lnkbtnMKT_Click">
                                                    <span class='box-title'>CRM
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-82'>
                                                    <asp:LinkButton ID="lnkbtnSale" runat="server" OnClick="lnkbtnSale_Click">
                                                    <span class='box-title'>Sales
                                                    </span>
                                                    <br/>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-1'>
                                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lnkbtnCR_Click">
                                                    <span class='box-title'>CR
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-cc'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkbtnCC_Click">
                                                    <span class='box-title'>Customer Care
                                                    </span>
                                                    <br/>
                                                    <i class='fa fa-tty fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>
                                                    <asp:LinkButton ID="LinkButton56" runat="server" OnClick="lnkbtnAssets_Click">
                                                    <span class='box-title'>Fixed Assets</span>
                                                    <br/>
                                                    <i class=' fa-bank (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkbtnACC_Click">
                                                    <span class='box-title'>Accounts
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74 col103'>
                                                    <asp:LinkButton ID="LinkBtnaudit" runat="server" OnClick="LinkBtnaudit_Click">
                                                    <span class='box-title'>Audit</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="linkdocumt" runat="server" OnClick="linkdocumt_Click">
                                                    <span class='box-title'>Documentation</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="linkmanage" runat="server" OnClick="linkmanage_Click">
                                                    <span class='box-title'>Control Panel</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <%--                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton55" runat="server" OnClick="btnConstruction_Click">
                                                    <span class='box-title'>Construction</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            --%>





                                            <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="lnkbtnAccount" runat="server" OnClick="lnkbtnM_Click">                                                
                                                   <span class='box-title'>M.Transfer</span>
                                                    <br/>
                                                    <i class='fa-tachometer fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>





                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkBtnMis" runat="server" OnClick="lnkbtnMis_Click">
                                                    <span class='box-title'>MIS
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-83'>
                                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lnkbtnHRM_Click">
                                                    <span class='box-title'>HR Management
                                                    </span>
                                                    <br/>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Flow Chart</h3>

                                        <div class="row">
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-78'>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">
                                                        <span class='box-title'>General Flow</span>
                                                        <br>
                                                        <i class='fa-puzzle-piece fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-84'>
                                                    <a href="<%=this.ResolveUrl("~/DashboardHRM.aspx")%>">
                                                        <span class='box-title'>HRM Flow</span>
                                                        <br>
                                                        <i class='fa-key fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <%--<div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <a href="<%=this.ResolveUrl("~/MyDashboard.aspx?Type=5004")%>">
                                                        <span class='box-title'>KPI Interface</span>
                                                        <br>
                                                        <i class='fa-usd fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>--%>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="lnkSettings" runat="server" OnClick="lnkSettings_Click">
                                                        <span class='box-title'>Settings</span>
                                                        <br>
                                                        <i class='fa-legal (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>

                                            <div class='twelve small-12 columns twitter-feed-box space' style="padding-top: 9px;">
                                                <div class="sidepnaelMenu">

                                                    <div class="btn-group" id="sidepnaelMenu">
                                                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#" title="Open Menu">
                                                            <i class="fa fa-bars glypSideMenu" style="margin-left: 0 !important;"></i>

                                                        </a>
                                                        <ul class="dropdown-menu dropSideMenu">

                                                            <li><a href="Image/ASITProfile.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Profile</a></li>
                                                            <li>
                                                                <a href="<%=this.ResolveUrl("~/Technology.aspx")%>" target="_blank"><i class="fa fa-users dropFa"></i>
                                                                    <br />
                                                                    Tools</a>

                                                            </li>
                                                            <li><a href="Image/MicroVsOrac.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Microsoft
                        <br />
                                                                Vs Oracle</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/Clients_List1.aspx")%>" target="_blank"><i class="fa fa-sitemap dropFa"></i>
                                                                <br />
                                                                Client
                        <br />
                                                                List</a></li>
                                                            <li><a href="WorkOrder.aspx" target="_blank"><i class="fa fa-file dropFa"></i>
                                                                <br />
                                                                Work Order</a></li>
                                                            <li><a href="#"><i class="fa fa-info dropFa"></i>
                                                                <br />
                                                                Notification</a></li>
                                                            <li><a href="#"><i class="fa fa-user dropFa"></i>
                                                                <br />
                                                                Online User</a></li>
                                                            <li><a href="#supportPart" onclick="load_modal()"><i class="fa fa-phone dropFa"></i>
                                                                <br />
                                                                Support</a></li>
                                                            <li><a href="#"><i class="fa fa-question dropFa"></i>
                                                                <br />
                                                                Help</a></li>
                                                        </ul>
                                                        <!-- Small modal -->


                                                    </div>


                                                    <div id="supportPart" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                                        <div class="modal-dialog modal-sm">
                                                            <div class="modal-content well">
                                                                <div class="panel-heading">
                                                                    <h2>Online Support</h2>
                                                                </div>
                                                                <h3>Software Help:- Md Mahbubur Rahman (Raihan): 01917792844</h3>
                                                                <h3>Technical Help:- Mostak 0177545613</h3>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-79'>
                                                    <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">
                                                        <span class='box-title'>All Reports</span>
                                                        <br>
                                                        <i class=' fa-navicon (alias) fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <div class='twelve small-6 columns twitter-feed-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton82" runat="server" OnClick="lnkbtnGrp_Click">
                                                        <span class='box-title boxtex '>Group Information</span>
                                                         <br/>
                                                        <i class='fa fa-folder-open fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>




                                        </div>
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Work Interface
                                        </h3>
                                        <div class="row">

                                             <div class='six small-4 columns contact-box space'>
                                                <div class='color-14'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BusinessDashboard.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Business Development </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-80'>
                                                    <%--~/F_01_LPA/RptAllProTopSheet.aspx?Type=Report&comcod=--%>
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/FeasibilityInterface.aspx")%>">
                                                        <span class='box-title'>Land Feasibility </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-75'>
                                                    <%--~/F_01_LPA/RptAllProTopSheet.aspx?Type=Report&comcod=--%>
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/InventoryInterface.aspx")%>">
                                                        <span class='box-title'>Inventory</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                         

                                            
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74 '>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>">
                                                        <span class='box-title'>Budget </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Purchase </span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>">
                                                        <span class="box-title">General Bill</span>
                                                        <br />
                                                        <i class="fa-shopping-cart fa fa-4x"></i>

                                                    </a>



                                                </div>
                                            </div>



                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-72'>


                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Sub-Contract</span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </a>




                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-77'>

                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Bill Register</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>


                                                </div>
                                            </div>
                                            <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Recovery</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>

                                                </div>
                                            </div>
                                            <div class="six small-4 columns twitter-feed-box space">
                                                <div class="color-74 col103">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Accounts</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                           

                                        </div>

                                        <div class="row">

                                             <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-79">
                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>">
                                                        <span class="box-title">Voucher 360 <sup>0</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-74">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/HRInterfaceTopSheet.aspx")%>">
                                                        <span class="box-title">Salary 360 <sup>0</span></span>
                                                        <br />
                                                        <i class="fa-suitcase fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>
                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-80">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Sales/CRM</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                            <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-77"> <%--F_81_Hrm/F_92_Mgt/InterfaceHR.aspx--%>
                                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?Type=Report&empid=")%>">
                                                        <span class='box-title'>My HRM</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>



                                        </div>

                                        <%--<div class='twelve small-12 columns twitter-feed-box space' style="height: 146px;">
                                                <div class='color-14' style="padding-bottom: 50px;">
                                                    <asp:LinkButton ID="LinkButton27" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>  <br/>  <br/> 
                                                        <span class='box-title boxtex boxdsh'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                        <div class="row">

                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-81">
                                                    <asp:LinkButton ID="lbtnMyDashBoard" runat="server" OnClick="lbtnMyDashBoard_Click">
                                                       
                                                      
                                                        <span class='box-title'>DashBoard (HR)</span>
                                                         
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 
                                                           
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                             <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-74"> 
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AddWorkInterface.aspx")%>">
                                                        <span class='box-title'>Customer Care</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                               <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-78"> 
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptAuditInterface.aspx")%>">
                                                        <span class='box-title'>Audit</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                             <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-81"> 
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SMSInterface.aspx")%>">
                                                        <span class='box-title'>SMS/Mail</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">

                                            <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-74">

                                                    <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>
                                                        <span class='box-title boxtex boxdsh2'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                        </div>



                                    </div>


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>
                        
                        
                        
                        <asp:Panel runat="server" CssClass="pnlflowchart" ID="Paneluddl" Visible="false" >


                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .doublebox {
                                    height: 130px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }



                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }




                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .col103 {
                                    color: #046971;
                                }




                                .space div:hover {
                                    background-color: #c27100 !important;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100) !important;
                                    color: #000 !important;
                                    /*padding: 10px 15px !important;*/
                                    /*margin: 2px 0 !important;*/
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                    cursor: pointer;
                                }
                            </style>

                            <asp:RadioButtonList ID="RadioButtonList3" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Visible="false" RepeatColumns="4"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                            </asp:RadioButtonList>


                            <div class="row boxShadow " style="padding: 0 6px;">

                                <div id="before-tilesu" class="large-12 columns"></div>
                                <div class="boxInn">


                                    <div class="four large-4 columns">
                                        <h3>Modules</h3>
                                        <div class="row">

                                            <div class='twelve large-12 columns space'>
                                                <div class='color-71'>
                                                   <asp:LinkButton ID="LinkButton27" runat="server" OnClick="lnkbtnACC_Click">
                                                        <span class='box-title boxtex boxdsh2'>Accounts Module</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x boxdsh1'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                              <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:HyperLink ID="LinkButton31" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccCodeBook.aspx?InputType=Accounts">
                                               
                                                    <span class='box-title'>Accounts Code</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:HyperLink>
                                                </div>
                                            </div>
                                            
                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <asp:HyperLink ID="LinkButton38" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=res">
                                                    <span class='box-title'>Resource Code</span>
                                                    <br/>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:HyperLink>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class=' color-81'>
                                                    <asp:HyperLink ID="HyperLink39" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccOpening.aspx">
                                                    <span class='box-title'>Accounts Opening</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:HyperLink>
                                                </div>
                                            </div>


                                          


                                            <div class='twelve small-12 columns twitter-feed-box space'> <%-- class='six small-12 columns contact-box space'--%>
                                                <div class='color-73 doublebox'>
                                                    <asp:LinkButton ID="LinkButton62" runat="server" OnClick="linkmanage_Click">
                                                    <span class='box-title boxtex boxdsh2'>Control Panel</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x boxdsh1'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton63" runat="server" OnClick="lnkbtnMis_Click">
                                                    <span class='box-title boxtex boxdsh2'>MIS
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x boxdsh1'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Flow Chart</h3>
                                        
                                        
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Work Interface</h3>
                                        <div class="row">

                                             <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-79">
                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>">
                                                        <span class="box-title boxtex boxdsh2">Voucher 360 <sup>0</sup></span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x boxdsh1"></i>

                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-14'>

                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay")%>">
                                                        <span class='box-title'>Receipt & Payment </span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-75'>

                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTrialBalance.aspx?Type=Mains")%>">
                                                        <span class="box-title">Trial Balance</span>
                                                        <br />
                                                        <i class="fa-shopping-cart fa fa-4x"></i>

                                                    </a>



                                                </div>
                                            </div>



                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-72'>


                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFinalReports.aspx?RepType=BS")%>">
                                                        <span class='box-title boxtex boxdsh2'>Balance Sheet</span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x boxdsh1'></i>
                                                    </a>




                                                </div>
                                            </div>

                                          
                                          
                                           

                                        </div>

                                        <div class="row">

                                            <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-14 doublebox">

                                                    <asp:LinkButton ID="LinkButton68" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>
                                                        <span class='box-title boxtex boxdsh2'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                        </div>



                                    </div>


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>
                          <asp:Panel runat="server" CssClass="pnlflowchart" ID="panelConstruction" Visible="false">

                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }

                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }

                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .col103 {
                                    color: #046971;
                                }
                            </style>

                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Visible="false" RepeatColumns="4"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                            </asp:RadioButtonList>


                            <div class="row boxShadow " style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns"></div>
                                <div class="boxInn">


                                    <div class="four large-4 columns">
                                        <h3>Modules</h3>
                                        <div class="row">

                                            <div class='twelve large-12 columns space'>
                                                <div class='color-71'>
                                                    <asp:LinkButton ID="lnkbtnStepOpraCon" runat="server" OnClick="lnkbtnStepOpraCon_Click">
                                                        <span class='box-title boxtex'>Steps Of Operation</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton29" runat="server" OnClick="lnkbtnAbp_Click">
                                               
                                                    <span class='box-title'>Business Plan</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <%-- <div class='six small-4 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                              
                                                    <span class='box-title'>Land Offer</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                             
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <asp:LinkButton ID="lbtnTender" runat="server" OnClick="lbtnTender_Click">
                                                    <span class='box-title'>Tender</span>
                                                    <br/>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class=' color-78'>
                                                    <asp:LinkButton ID="LinkButton32" runat="server" OnClick="lnkbtnMatr_Click">
                                                    <span class='box-title'>Budget</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-74 col103'>

                                                    <asp:LinkButton ID="LinkButton33" runat="server" OnClick="lnkbtnPlaning_Click">
                                                    <span class='box-title'>Project Planing</span>
                                                    <br>
                                                    <i class='fa-money fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton34" runat="server" OnClick="btnImp_Click">
                                                    <span class='box-title'>Project Implementation</span>
                                                    <br/>
                                                    <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton35" runat="server" OnClick="linkcentral_Click">
                                                    <span class='box-title'>Central Wearhouse</span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-78'>
                                                    <asp:LinkButton ID="LinkButton36" runat="server" OnClick="lnkbtnGoodsInv_Click">
                                                    <span class='box-title'>Inventory</span>
                                                    <br/>
                                                    <i class='fa-suitcase fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-79'>
                                                    <asp:LinkButton ID="LinkButton37" runat="server" OnClick="btnPur_Click">
                                                    <span class='box-title'>Procurement</span>
                                                    <br/>
                                                    <i class='fa-credit-card fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="lnkBill" runat="server" OnClick="lnkBill_OnClick">
                                                    <span class='box-title'>Billing
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>







                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>
                                                    <asp:LinkButton ID="LinkButton42" runat="server" OnClick="lnkbtnAssets_Click">
                                                    <span class='box-title'>Fixed Assets</span>
                                                    <br/>
                                                    <i class=' fa-bank (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton43" runat="server" OnClick="lnkbtnACC_Click">
                                                    <span class='box-title'>Accounts
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton44" runat="server" OnClick="LinkBtnaudit_Click">
                                                    <span class='box-title'>Audit</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton45" runat="server" OnClick="linkdocumt_Click">
                                                    <span class='box-title'>Documentation</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="lbtnControlcosnt" runat="server" OnClick="lbtnControlcosnt_Click">
                                                    <span class='box-title'>Control Panel</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <%--                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton55" runat="server" OnClick="btnConstruction_Click">
                                                    <span class='box-title'>Construction</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            --%>





                                            <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="lnkbtnAccount" runat="server" OnClick="lnkbtnM_Click">                                                
                                                   <span class='box-title'>M.Transfer</span>
                                                    <br/>
                                                    <i class='fa-tachometer fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>





                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="lbtnMisCont" runat="server" OnClick="lbtnMisCont_Click">
                                                    <span class='box-title'>MIS
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-83'>
                                                    <asp:LinkButton ID="LinkButton48" runat="server" OnClick="lnkbtnHRM_Click">
                                                    <span class='box-title'>HR Management
                                                    </span>
                                                    <br/>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Flow Chart</h3>

                                        <div class="row">
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-78 col103'>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">
                                                        <span class='box-title'>General Flow</span>
                                                        <br>
                                                        <i class='fa-puzzle-piece fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-84'>
                                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">
                                                        <span class='box-title'>HRM Flow</span>
                                                        <br>
                                                        <i class='fa-key fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <%--<div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <a href="<%=this.ResolveUrl("~/MyDashboard.aspx?Type=5004")%>">
                                                        <span class='box-title'>KPI Interface</span>
                                                        <br>
                                                        <i class='fa-usd fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>--%>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="LinkButton49" runat="server" OnClick="lnkSettings_Click">
                                                        <span class='box-title'>Settings</span>
                                                        <br>
                                                        <i class='fa-legal (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>

                                            <div class='twelve small-12 columns twitter-feed-box space' style="padding-top: 9px;">
                                                <div class="sidepnaelMenu">

                                                    <div class="btn-group" id="sidepnaelMenu">
                                                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#" title="Open Menu">
                                                            <i class="fa fa-bars glypSideMenu" style="margin-left: 0 !important;"></i>

                                                        </a>
                                                        <ul class="dropdown-menu dropSideMenu">

                                                            <li><a href="Image/ASITProfile.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Profile</a></li>
                                                            <li>
                                                                <a href="<%=this.ResolveUrl("~/Technology.aspx")%>" target="_blank"><i class="fa fa-users dropFa"></i>
                                                                    <br />
                                                                    Tools</a>

                                                            </li>
                                                            <li><a href="Image/MicroVsOrac.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Microsoft
                        <br />
                                                                Vs Oracle</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/Clients_List1.aspx")%>" target="_blank"><i class="fa fa-sitemap dropFa"></i>
                                                                <br />
                                                                Client
                        <br />
                                                                List</a></li>
                                                            <li><a href="WorkOrder.aspx" target="_blank"><i class="fa fa-file dropFa"></i>
                                                                <br />
                                                                Work Order</a></li>
                                                            <li><a href="#"><i class="fa fa-info dropFa"></i>
                                                                <br />
                                                                Notification</a></li>
                                                            <li><a href="#"><i class="fa fa-user dropFa"></i>
                                                                <br />
                                                                Online User</a></li>
                                                            <li><a href="#supportPart" onclick="load_modal()"><i class="fa fa-phone dropFa"></i>
                                                                <br />
                                                                Support</a></li>
                                                            <li><a href="#"><i class="fa fa-question dropFa"></i>
                                                                <br />
                                                                Help</a></li>
                                                        </ul>
                                                        <!-- Small modal -->


                                                    </div>


                                                    <div id="supportPart" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                                        <div class="modal-dialog modal-sm">
                                                            <div class="modal-content well">
                                                                <div class="panel-heading">
                                                                    <h2>Online Support</h2>
                                                                </div>
                                                                <h3>Software Help:- Md Mahbubur Rahman (Raihan): 01917792844</h3>
                                                                <h3>Technical Help:- Mostak 0177545613</h3>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-79'>
                                                    <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">
                                                        <span class='box-title'>All Reports</span>
                                                        <br>
                                                        <i class=' fa-navicon (alias) fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <div class='twelve small-6 columns twitter-feed-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton50" runat="server" OnClick="lnkbtnGrp_Click">
                                                        <span class='box-title boxtex '>Group Information</span>
                                                         <br/>
                                                        <i class='fa fa-folder-open fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>




                                        </div>
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Work Interface
                                        </h3>
                                        <div class="row">

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-80'>

                                                    <a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>">
                                                        <span class='box-title'>Tender</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-14'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>">
                                                        <span class='box-title'>Budget </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Purchase </span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-75'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>">
                                                        <span class="box-title">General Bill</span>
                                                        <br />
                                                        <i class="fa-shopping-cart fa fa-4x"></i>

                                                    </a>



                                                </div>
                                            </div>



                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>


                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Construction</span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </a>




                                                </div>
                                            </div>





                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Billing Management</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>

                                                </div>
                                            </div>
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>

                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Bill Register</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>


                                                </div>
                                            </div>
                                            <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-74 col103">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Accounts</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-79">
                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>">
                                                        <span class="box-title">Voucher 360 <sup>0</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                            <div class="six small-12 columns twitter-feed-box space">
                                                <div class="color-77">
                                                    <a href="<%=this.ResolveUrl("~/HRMAllInOne.aspx")%>">
                                                        <span class='box-title'>My HRM</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>




                                            </div>
                                        </div>








                                        <div class="row">


                                            <div class='twelve small-12 columns twitter-feed-box space' style="height: 146px;">
                                                <div class='color-14' style="padding-bottom: 50px;">
                                                    <asp:LinkButton ID="LinkButton51" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>  <br/>  <br/> 
                                                        <span class='box-title boxtex boxdsh'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                        </div>

                                    </div>


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>




                        <asp:Panel runat="server" CssClass="pnlflowchart" ID="pnlEdison" Visible="false">

                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }

                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }

                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #00AF50;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .col103 {
                                    color: #046971;
                                }
                            </style>

                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Visible="false" RepeatColumns="4"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                            </asp:RadioButtonList>


                            <div class="row boxShadow " style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns"></div>
                                <div class="boxInn">


                                    <div class="four large-4 columns">
                                        <h3>Modules</h3>
                                        <div class="row">

                                            <div class='twelve large-12 columns space'>
                                                <div class='color-71'>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkbtnStepOpra_Click">
                                                        <span class='box-title boxtex'>Steps Of Operation</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lnkbtnAbp_Click">
                                               
                                                    <span class='box-title'>Business Plan</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <%-- <div class='six small-4 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                                              
                                                    <span class='box-title'>Land Offer</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                             
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton8_Click">
                                                    <span class='box-title'>Land Feasibility</span>
                                                    <br/>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class=' color-78'>
                                                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lnkbtnMatr_Click">
                                                    <span class='box-title'>Budget</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-74 col103'>

                                                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="lnkbtnPlaning_Click">
                                                    <span class='box-title'>Project Planing</span>
                                                    <br>
                                                    <i class='fa-money fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="btnImp_Click">
                                                    <span class='box-title'>Project Implementation</span>
                                                    <br/>
                                                    <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton14" runat="server" OnClick="linkcentral_Click">
                                                    <span class='box-title'>Central Wearhouse</span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                            <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-78'>
                                                    <asp:LinkButton ID="LinkButton15" runat="server" OnClick="lnkbtnGoodsInv_Click">
                                                    <span class='box-title'>Inventory</span>
                                                    <br/>
                                                    <i class='fa-suitcase fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-79'>
                                                    <asp:LinkButton ID="LinkButton16" runat="server" OnClick="btnPur_Click">
                                                    <span class='box-title'>Procurement</span>
                                                    <br/>
                                                    <i class='fa-credit-card fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="LinkButton17" runat="server" OnClick="lnkbtnMKT_Click">
                                                    <span class='box-title'>CRM
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-82'>
                                                    <asp:LinkButton ID="LinkButton18" runat="server" OnClick="lnkbtnSale_Click">
                                                    <span class='box-title'>Sales
                                                    </span>
                                                    <br/>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-1'>
                                                    <asp:LinkButton ID="LinkButton19" runat="server" OnClick="lnkbtnCR_Click">
                                                    <span class='box-title'>CR
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-cc'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="lnkbtnCC_Click">
                                                    <span class='box-title'>Customer Care
                                                    </span>
                                                    <br/>
                                                    <i class='fa fa-tty fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>
                                                    <asp:LinkButton ID="LinkButton21" runat="server" OnClick="lnkbtnAssets_Click">
                                                    <span class='box-title'>Fixed Assets</span>
                                                    <br/>
                                                    <i class=' fa-bank (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton22" runat="server" OnClick="lnkbtnACC_Click">
                                                    <span class='box-title'>Accounts
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LinkBtnaudit_Click">
                                                    <span class='box-title'>Audit</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton24" runat="server" OnClick="linkdocumt_Click">
                                                    <span class='box-title'>Documentation</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton25" runat="server" OnClick="linkmanage_Click">
                                                    <span class='box-title'>Control Panel</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <%--                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton55" runat="server" OnClick="btnConstruction_Click">
                                                    <span class='box-title'>Construction</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            --%>





                                            <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="lnkbtnAccount" runat="server" OnClick="lnkbtnM_Click">                                                
                                                   <span class='box-title'>M.Transfer</span>
                                                    <br/>
                                                    <i class='fa-tachometer fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>





                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton26" runat="server" OnClick="lnkbtnMis_Click">
                                                    <span class='box-title'>MIS
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Flow Chart</h3>

                                        <div class="row">
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-78 col103'>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">
                                                        <span class='box-title'>General Flow</span>
                                                        <br>
                                                        <i class='fa-puzzle-piece fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>


                                            <%--<div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <a href="<%=this.ResolveUrl("~/MyDashboard.aspx?Type=5004")%>">
                                                        <span class='box-title'>KPI Interface</span>
                                                        <br>
                                                        <i class='fa-usd fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>--%>
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="LinkButton28" runat="server" OnClick="lnkSettings_Click">
                                                        <span class='box-title'>Control Panel</span>
                                                        <br>
                                                        <i class='fa-legal (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>

                                            <div class='twelve small-12 columns twitter-feed-box space' style="padding-top: 9px;">
                                                <div class="sidepnaelMenu">

                                                    <div class="btn-group" id="sidepnaelMenu">
                                                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#" title="Open Menu">
                                                            <i class="fa fa-bars glypSideMenu" style="margin-left: 0 !important;"></i>

                                                        </a>
                                                        <ul class="dropdown-menu dropSideMenu">

                                                            <li><a href="Image/ASITProfile.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Profile</a></li>
                                                            <li>
                                                                <a href="<%=this.ResolveUrl("~/Technology.aspx")%>" target="_blank"><i class="fa fa-users dropFa"></i>
                                                                    <br />
                                                                    Tools</a>

                                                            </li>
                                                            <li><a href="Image/MicroVsOrac.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Microsoft
                        <br />
                                                                Vs Oracle</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/Clients_List1.aspx")%>" target="_blank"><i class="fa fa-sitemap dropFa"></i>
                                                                <br />
                                                                Client
                        <br />
                                                                List</a></li>
                                                            <li><a href="WorkOrder.aspx" target="_blank"><i class="fa fa-file dropFa"></i>
                                                                <br />
                                                                Work Order</a></li>
                                                            <li><a href="#"><i class="fa fa-info dropFa"></i>
                                                                <br />
                                                                Notification</a></li>
                                                            <li><a href="#"><i class="fa fa-user dropFa"></i>
                                                                <br />
                                                                Online User</a></li>
                                                            <li><a href="#supportPart" onclick="load_modal()"><i class="fa fa-phone dropFa"></i>
                                                                <br />
                                                                Support</a></li>
                                                            <li><a href="#"><i class="fa fa-question dropFa"></i>
                                                                <br />
                                                                Help</a></li>
                                                        </ul>
                                                        <!-- Small modal -->


                                                    </div>


                                                    <div id="supportPart" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                                        <div class="modal-dialog modal-sm">
                                                            <div class="modal-content well">
                                                                <div class="panel-heading">
                                                                    <h2>Online Support</h2>
                                                                </div>
                                                                <h3>Software Help:- Md Mahbubur Rahman (Raihan): 01917792844</h3>
                                                                <h3>Technical Help:- Mostak 0177545613</h3>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-79'>
                                                </div>
                                            </div>





                                        </div>
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Work Interface
                                        </h3>
                                        <div class="row">

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-80'>

                                                    <a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>">
                                                        <span class='box-title'>Land Feasibility </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-14'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>">
                                                        <span class='box-title'>Budget </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Purchase </span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-75'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>">
                                                        <span class="box-title">General Bill</span>
                                                        <br />
                                                        <i class="fa-shopping-cart fa fa-4x"></i>

                                                    </a>



                                                </div>
                                            </div>



                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>


                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Construction</span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </a>




                                                </div>
                                            </div>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-77'>

                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Bill Register</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>


                                                </div>
                                            </div>
                                            <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Sales</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>

                                                </div>
                                            </div>
                                            <div class="six small-4 columns twitter-feed-box space">
                                                <div class="color-74 col103">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Accounts</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <%-- <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-77">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx")%>">
                                                        <span class="box-title">Sales KPI</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>--%>
                                        </div>

                                        <div class="row">

                                            <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-80">
                                                </div>
                                            </div>





                                        </div>






                                        <div class="row">


                                            <div class='twelve small-12 columns twitter-feed-box space' style="height: 146px;">
                                                <div class='color-14' style="padding-bottom: 50px;">
                                                    <asp:LinkButton ID="LinkButton30" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>  <br/>  <br/> 
                                                        <span class='box-title boxtex boxdsh'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                        </div>

                                    </div>


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>


                        <asp:Panel runat="server" ID="pnlkMIS" CssClass="pnlflowchart" Visible="false">
                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .pnlflowchart {
                                    background-image: url("Image/bg.PNG") !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }

                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }

                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    /*background: #1F4E78;*/
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }
                            </style>
                            <div class="row" style="padding: 0 6px;">
                                <div id="before-tiless25" class="large-12 columns">
                                </div>
                                <div class="four large-4 columns">
                                    <h3>Input</h3>
                                    <div class="row">
                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-9'>
                                                <a href="<%=this.ResolveUrl("~/F_47_Kpi/EmpStdKpi.aspx")%>">
                                                    <span class='box-title'>KPI Setting</span>
                                                    <br>
                                                    <i class='fa-cubes fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-77'>
                                                <a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>">
                                                    <span class='box-title'>Entry
                                                    </span>
                                                    <br>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>




                                    </div>
                                </div>



                                <div class="four large-4 columns">
                                    <h3>Reports</h3>


                                    <div class="row">

                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-71'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptProWiseClOffered.aspx?Type=SalesDeci")%>">
                                                    <span class='box-title'>Sales Decision</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-80'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptProWiseClOffered.aspx?Type=SalesDeamnd")%>">
                                                    <span class='box-title'>Sales Demand Analysis</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>




                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-72'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptProWiseClOffered.aspx?Type=Capacity")%>">
                                                    <span class='box-title'>Client Capacity Analysis</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-73'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=DiscussHis&UType=Mgt")%>">
                                                    <span class='box-title'>Client History</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-74'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=OffPerformance&UType=Mgt")%>">
                                                    <span class='box-title'>Sales Person History</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>


                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-75'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=AllOffPerformance&UType=Mgt")%>">
                                                    <span class='box-title'>Sales Person History(All)</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <%--   <div class='six small-6 columns contact-box space'>
                                            <div class='color-76'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>">
                                                    <span class='box-title'>Todays Appoinment</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>--%>

                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-77'>
                                                <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=NextApp&UType=Mgt")%>">
                                                    <span class='box-title'>Appointment</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-4 columns contact-box space'>
                                            <div class='color-78'>

                                                <%--  <a href="<%=this.ResolveUrl("~/F_62_Mis/RptMktAppointment.aspx?Type=SalePerformance&UType=Mgt")%>">--%>

                                                <a href="<%=this.ResolveUrl("~/F_47_Kpi/LinkEmpMonthWiseEvaDet.aspx?Type=Mgt&dept=Sales&date=06-Aug-2017&empid=930100101003")%>">
                                                    <span class='box-title'>Sales Performance</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-4 columns contact-box space'>
                                            <div class='color-79'>
                                                <a href="<%=this.ResolveUrl("~/F_62_MIS/RptMktAppointment.aspx?Type=ClientLetter&UType=Mgt")%>">
                                                    <span class='box-title'>Send Letter</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-4 columns contact-box space'>
                                            <div class='color-78'>
                                                <a href="<%=this.ResolveUrl("~/F_62_MIS/RptMktAppointment.aspx?Type=SendOnlineLetter&UType=Mgt")%>">
                                                    <span class='box-title'>Send Letter(Online)</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-80'>
                                                <a href="<%=this.ResolveUrl("~/F_62_MIS/RptMktAppointment.aspx?Type=ProsClient&UType=Mgt")%>">
                                                    <span class='box-title'>Prospective Client</span>
                                                    <br>
                                                    <i class='fa fa-building fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-75'>
                                                <a href="<%=this.ResolveUrl("~/MyDashboard.aspx?Type=5000")%>">
                                                    <span class='box-title'>Main Page</span>
                                                    <br>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>

                                        <%-- <div class='six small-6 columns contact-box space'>
                                            <div class='color-1'>
                                                <div class="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>">
                                                    <a href='#Contact'><span class='box-title'>Settings</span>
                                                        <br>
                                                        <i class='fa-cogs fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-11'>
                                                <asp:LinkButton ID="LinkButton73" runat="server" OnClick="lnkbtnGeneral_Click">
                                               
                                                    <span class='box-title'>General </span>
                                                    <br>
                                                    <i class='fa-tasks fa fa-4x'></i>
                                              
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-12'>

                                                <asp:LinkButton ID="LinkButton74" runat="server" OnClick="lnkbtnHr_Click">
                                        <span class='box-title'>HR Management </span>
                                                    <br>
                                                    <i class='fa-puzzle-piece fa fa-4x'></i></asp:LinkButton>



                                            </div>
                                        </div>
                                        <div class='six small-6 columns contact-box space'>
                                            <div class='color-9'>
                                                <asp:LinkButton ID="LinkButton75" runat="server" OnClick="lnkbtnKPI_Click">

                                               
                                                    <span class='box-title'>KPI </span>
                                                    <br>
                                                    <i class='fa-key fa fa-4x'></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="four large-4 columns">
                                    <h3>DashBoard</h3>

                                    <div class="row">
                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-9'>
                                                <a href="<%=this.ResolveUrl("~/F_47_Kpi/RptEmpEvaSheet.aspx?Type=Mgt")%>" target="_self">
                                                    <span class='box-title'>KPI (Total)</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-77'>
                                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx")%>" target="_self">
                                                    <span class='box-title'>My Interface</span>
                                                    <br>
                                                    <i class='fa-credit-card fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>



                                        <div class='six small-12 columns contact-box space'>
                                            <div class='color-72'>
                                                <a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx")%>" target="_self">
                                                    <span class='box-title'>Marketing Interface</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>



                                    </div>
                                </div>
                            </div>


                        </asp:Panel>



                        <asp:Panel runat="server" CssClass="pnlflowchart" ID="PnlERPSt" Visible="false" >


                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }



                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }




                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    color: #fff;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    background: #BF8F00;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .col103 {
                                    color: #046971;
                                }

                               


                                .space div:hover {
                                    background-color: #c27100 !important;
                                    background-image: linear-gradient(to bottom,#F5E100,#c27100) !important;
                                    color: #000 !important;
                                    /*padding: 10px 15px !important;*/
                                    /*margin: 2px 0 !important;*/
                                    border-radius: 3px;
                                    opacity: 1 !important;
                                    cursor: pointer;
                                }





                              
                            </style>

                            <asp:RadioButtonList ID="RadioButtonList4" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Visible="false" RepeatColumns="4"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                            </asp:RadioButtonList>


                            <div class="row boxShadow " style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns"></div>
                                <div class="boxInn">


                                    <div class="four large-4 columns">
                                        <h3>Modules</h3>
                                        <div class="row">

                                            <div class='twelve large-12 columns space'>
                                                <div class='color-71'>
                                                    <asp:LinkButton ID="LinkButton39" runat="server" OnClick="lnkbtnStepOpra_Click">
                                                        <span class='box-title boxtex'>Steps Of Operation</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <%--<div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton40" runat="server" OnClick="lnkbtnAbp_Click">
                                               
                                                    <span class='box-title'>Business Plan</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                          
                                            <%--<div class='six small-4 columns contact-box space'>
                                                <div class='color-73'>

                                                    <asp:LinkButton ID="LinkButton41" runat="server" OnClick="LinkButton8_Click">
                                                    <span class='box-title'>Land Feasibility</span>
                                                    <br/>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class=' color-81'>
                                                    <asp:LinkButton ID="LinkButton46" runat="server" OnClick="lnkbtnMatr_Click">
                                                    <span class='box-title'>Budget</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                           <%-- <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-84'>

                                                    <asp:LinkButton ID="LinkButton47" runat="server" OnClick="lnkbtnPlaning_Click">
                                                    <span class='box-title'>Project Planing</span>
                                                    <br>
                                                    <i class='fa-money fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                            <div class='six small-8 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton52" runat="server" OnClick="btnImp_Click">
                                                    <span class='box-title'>Sub-Contracting</span>
                                                    <br/>
                                                    <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                          <%--  <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="LinkButton53" runat="server" OnClick="linkcentral_Click">
                                                    <span class='box-title'>Central Wearhouse</span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>



                                            <div class='twelve small-8 columns twitter-feed-box space'>
                                                <div class='color-78'>
                                                    <asp:LinkButton ID="LinkButton54" runat="server" OnClick="lnkbtnGoodsInv_Click">
                                                    <span class='box-title'>Inventory Control</span>
                                                    <br/>
                                                    <i class='fa-suitcase fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-79'>
                                                    <asp:LinkButton ID="LinkButton55" runat="server" OnClick="btnPur_Click">
                                                    <span class='box-title'>Procurement</span>
                                                    <br/>
                                                    <i class='fa-credit-card fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                           <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="LinkButton57" runat="server" OnClick="lnkbtnMKT_Click">
                                                    <span class='box-title'>CRM
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>



                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-82'>
                                                    <asp:LinkButton ID="LinkButton58" runat="server" OnClick="lnkbtnSale_Click">
                                                    <span class='box-title'>Sales
                                                    </span>
                                                    <br/>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-1'>
                                                    <asp:LinkButton ID="LinkButton59" runat="server" OnClick="lnkbtnCR_Click">
                                                    <span class='box-title'>CR/Recovery
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-cc'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                            <%--<div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton60" runat="server" OnClick="lnkbtnCC_Click">
                                                    <span class='box-title'>Customer Care
                                                    </span>
                                                    <br/>
                                                    <i class='fa fa-tty fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                           <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>
                                                    <asp:LinkButton ID="LinkButton61" runat="server" OnClick="lnkbtnAssets_Click">
                                                    <span class='box-title'>Fixed Assets</span>
                                                    <br/>
                                                    <i class=' fa-bank (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton64" runat="server" OnClick="lnkbtnACC_Click">
                                                    <span class='box-title'>Accounts
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                           <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-74 col103'>
                                                    <asp:LinkButton ID="LinkButton65" runat="server" OnClick="LinkBtnaudit_Click">
                                                    <span class='box-title'>Audit</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                           <%-- <div class='six small-3 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton66" runat="server" OnClick="linkdocumt_Click">
                                                    <span class='box-title'>Documentation</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>


                                            
                                          



                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="LinkButton69" runat="server" OnClick="lnkbtnMis_Click">
                                                    <span class='box-title'>MIS
                                                    </span>
                                                    <br/>
                                                    <i class='fa-cogs fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-83'>
                                                    <asp:LinkButton ID="LinkButton70" runat="server" OnClick="lnkbtnHRM_Click">
                                                    <span class='box-title'>HR Management
                                                    </span>
                                                    <br/>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="LinkButton67" runat="server" OnClick="linkmanage_Click">
                                                    <span class='box-title'>Control Panel</span>
                                                    <br/>
                                                    <i class='fa fa-building fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Flow Chart</h3>

                                        <div class="row">
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-78'>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">
                                                        <span class='box-title'>General Flow</span>
                                                        <br>
                                                        <i class='fa-puzzle-piece fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-84'>
                                                    <a href="<%=this.ResolveUrl("~/HrWinMenu.aspx")%>">
                                                        <span class='box-title'>HRM Flow</span>
                                                        <br>
                                                        <i class='fa-key fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <%--<div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <a href="<%=this.ResolveUrl("~/MyDashboard.aspx?Type=5004")%>">
                                                        <span class='box-title'>KPI Interface</span>
                                                        <br>
                                                        <i class='fa-usd fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>--%>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-73'>
                                                    <asp:LinkButton ID="LinkButton71" runat="server" OnClick="lnkSettings_Click">
                                                        <span class='box-title'>Settings</span>
                                                        <br>
                                                        <i class='fa-legal (alias) fa fa-4x'></i>
                                                    </asp:LinkButton>

                                                </div>
                                            </div>

                                            <div class='twelve small-12 columns twitter-feed-box space' style="padding-top: 9px;">
                                                <div class="sidepnaelMenu">

                                                    <div class="btn-group" id="sidepnaelMenu">
                                                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#" title="Open Menu">
                                                            <i class="fa fa-bars glypSideMenu" style="margin-left: 0 !important;"></i>

                                                        </a>
                                                        <ul class="dropdown-menu dropSideMenu">

                                                            <li><a href="Image/ASITProfile.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Profile</a></li>
                                                            <li>
                                                                <a href="<%=this.ResolveUrl("~/Technology.aspx")%>" target="_blank"><i class="fa fa-users dropFa"></i>
                                                                    <br />
                                                                    Tools</a>

                                                            </li>
                                                            <li><a href="Image/MicroVsOrac.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Microsoft
                        <br />
                                                                Vs Oracle</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/Clients_List1.aspx")%>" target="_blank"><i class="fa fa-sitemap dropFa"></i>
                                                                <br />
                                                                Client
                        <br />
                                                                List</a></li>
                                                            <li><a href="WorkOrder.aspx" target="_blank"><i class="fa fa-file dropFa"></i>
                                                                <br />
                                                                Work Order</a></li>
                                                            <li><a href="#"><i class="fa fa-info dropFa"></i>
                                                                <br />
                                                                Notification</a></li>
                                                            <li><a href="#"><i class="fa fa-user dropFa"></i>
                                                                <br />
                                                                Online User</a></li>
                                                            <li><a href="#supportPart" onclick="load_modal()"><i class="fa fa-phone dropFa"></i>
                                                                <br />
                                                                Support</a></li>
                                                            <li><a href="#"><i class="fa fa-question dropFa"></i>
                                                                <br />
                                                                Help</a></li>
                                                        </ul>
                                                        <!-- Small modal -->


                                                    </div>


                                                    <div id="supportPart" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                                        <div class="modal-dialog modal-sm">
                                                            <div class="modal-content well">
                                                                <div class="panel-heading">
                                                                    <h2>Online Support</h2>
                                                                </div>
                                                                <h3>Software Help:- Md Mahbubur Rahman (Raihan): 01917792844</h3>
                                                                <h3>Technical Help:- Mostak 0177545613</h3>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-79">

                                                     <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">
                                                         <br/>
                                                        <span class='box-title boxtex boxdsh2'>All Reports</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </a>
                                                </div>
                                            </div>

                                            <%--<div class='six small-12 columns contact-box space'>
                                                <div class='color-79'>
                                                    <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">
                                                        <span class='box-title'>All Reports</span>
                                                        <br>
                                                        <i class=' fa-navicon (alias) fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>--%>

                                           <%-- <div class='twelve small-6 columns twitter-feed-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="LinkButton72" runat="server" OnClick="lnkbtnGrp_Click">
                                                        <span class='box-title boxtex '>Group Information</span>
                                                         <br/>
                                                        <i class='fa fa-folder-open fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>




                                        </div>
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>Work Interface
                                        </h3>
                                        <div class="row">

                                             <%--<div class='six small-4 columns contact-box space'>
                                                <div class='color-14'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BusinessDashboard.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Business Development </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>--%>


                                           <%-- <div class='six small-4 columns contact-box space'>
                                                <div class='color-80'>
                                                   
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/FeasibilityInterface.aspx")%>">
                                                        <span class='box-title'>Land Feasibility </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>--%>
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-74 '>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>">
                                                        <span class='box-title'>Budget </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>
                                            
                                         

                                            
                                            

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-81'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Purchase </span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-75'>
                                                  
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/InventoryInterface.aspx")%>">
                                                        <span class='box-title'>Inventory</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>


                                            <%--<div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>

                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>">
                                                        <span class="box-title">General Bill</span>
                                                        <br />
                                                        <i class="fa-shopping-cart fa fa-4x"></i>

                                                    </a>



                                                </div>
                                            </div>--%>



                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-72'>


                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Sub-Contract</span>
                                                        <br />
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </a>




                                                </div>
                                            </div>
                                            <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Recovery</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>

                                                </div>
                                            </div>
                                            <div class="six small-4 columns twitter-feed-box space">
                                                <div class="color-74 col103">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class='box-title'>Accounts</span>
                                                        <br />
                                                        <i class='fa-database fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-77'>

                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Bill Register</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>


                                                </div>
                                            </div>
                                            
                                            <div class="twelve small-6 columns twitter-feed-box space">
                                                <div class="color-79">
                                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>">
                                                        <span class="box-title">Voucher 360 <sup>0</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                           

                                        </div>

                                        <div class="row">

                                             

                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-74">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/HRInterfaceTopSheet.aspx")%>">
                                                        <span class="box-title">Salary 360 <sup>0</span></span>
                                                        <br />
                                                        <i class="fa-suitcase fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>
                                            <%--<div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-80">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=")%>">
                                                        <span class="box-title">Sales/CRM</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>--%>

                                            <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-80"> 
                                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">
                                                        <span class='box-title'>My HRM</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            <div class="twelve small-6 columns twitter-feed-box space">
                                                <div class="color-81">
                                                    <asp:LinkButton ID="LinkButton73" runat="server" OnClick="lbtnMyDashBoard_Click">
                                                       
                                                      
                                                        <span class='box-title'>DashBoard (HR)</span>
                                                         
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 
                                                           
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>

                                        

                                        

                                        <div class="row">

                                            <div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-74">

                                                    <asp:LinkButton ID="LinkButton74" runat="server" OnClick="lnkbtnGeneral_Click">
                                                         <br/>
                                                        <span class='box-title boxtex boxdsh2'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                            </div>



                                        </div>



                                    </div>


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>


                    </div>
                </div>
                <div class="clearfix"></div>
            </section>

        </div>
    </div>
    <script type='text/javascript' src='assets\js\jquery\jquery.js'></script>
    <script type='text/javascript' src='assets\js\jquery\jquery-migrate.min.js'></script>
    <script type='text/javascript' src='assets/js/comment-reply.min.js'></script>
    <script type='text/javascript' src='assetsjs/vendor/custom.modernizr.js'></script>
    <script type='text/javascript' src='assets\js\foundation.min.js'></script>
    <script type='text/javascript' src='assets\js\modernizr.custom.js'></script>
    <script type='text/javascript' src='assets\js\foundation\foundation.section.js'></script>
    <script type='text/javascript' src='assets\js\responsiveslides.js'></script>
    <script type='text/javascript' src='assets\js\scripts.js'></script>
   
    
    <!-- jQuery library -->
    <script src="assets\js\jquery.min.js"></script>

    <script type='text/javascript' src='assets\js\wd-ajax-load\js\load-posts.js'></script>
    <script type='text/javascript' src='assets/js/jquery.form.min.js'></script>

    
</asp:Content>

