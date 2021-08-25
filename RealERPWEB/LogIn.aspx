<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="RealERPWEB.LogIn" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">



    <title></title>
    <link rel="apple-touch-icon" sizes="180x180" href="Image/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="Image/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="Image/favicon-16x16.png">
    <link rel="manifest" href="Image/site.webmanifest">

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Theme/Custom.css" rel="stylesheet" />
    <link href="Content/asitCommonStyle.css" rel="stylesheet" />
    <link href="Content/AsitnoneResponsive.css" rel="stylesheet" />
    <link href="Content/BDAccStyle.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/chosen.css" rel="stylesheet" />
    <link href="Content/AppsStyle.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.3.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/chosen.jquery.js"></script>
    <%--<%: Styles.Render("~/Content/version") %>
        <%: Styles.Render("~/Content/css") %>        
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Scripts.Render("~/bundles/Version") %>
        <%: Scripts.Render("~/bundles/Jquery") %>  
        <%: Scripts.Render("~/bundles/keynavigation") %>       
        <%: Scripts.Render("~/bundles/User") %>--%>

    <style>
        #loginform {
            width: 70%;
            margin: 0 auto;
            display: table;
        }

            #loginform .form-control {
                height: 34px;
                width: 258px;
            }

        .forgotform .form-control {
            height: 34px;
            width: 258px;
        }
        .chzn-drop {

           
            width: 258px !important; 
            
        
        }

        #loginform .form-group, #loginform .panel-body {
            margin: 0 !important;
            padding-bottom: 0 !important;
        }

        #loginform .input-group .input-group-addon {
            padding: 6px 2px;
            width: 60px;
        }

        .field-icon {
            float: right;
            margin-top: 9px;
            margin-left: -13px !important;
            padding-left: -30px;
            position: relative;
            z-index: 5;
        }

        .btn-circle {
            width: 30px;
            height: 30px;
            text-align: center;
            padding: 6px 0;
            font-size: 12px;
            line-height: 1.428571429;
            border-radius: 15px;
            margin-top: -11px;
            margin-left: 546px;
            position: absolute;
        }

        /* for Login Page*/

        /*.Companylogo {
            height: 80px;
            width: 104px !important;
            height: 80px !important;
            border: none;
        }*/




        body {
            background: White;
        }
        /* CSS Document */
        #asitulogmain {
            width: 490px;
            min-height: 280px;
            border: 1px solid #A9B6C0;
            margin: 10% auto 5px;
            background: #fff;
            box-shadow: 1px 0px 10px 4px rgb(101 125 142 / 75%);
            -moz-box-shadow: 1px 0px 10px 4px rgba(101, 125, 142, 0.75);
            -webkit-box-shadow: 1px 0px 10px 4px rgb(101 125 142 / 75%);
            border-radius: 20px;
        }
         #asitulogmain2 {
            width: 490px;
            min-height: 150px;
            border: 1px solid #A9B6C0;
            margin: 0 auto;
            padding:50px;
            background: #fff;
            box-shadow: 1px 0px 10px 4px rgb(101 125 142 / 75%);
            -moz-box-shadow: 1px 0px 10px 4px rgba(101, 125, 142, 0.75);
            -webkit-box-shadow: 1px 0px 10px 4px rgb(101 125 142 / 75%);
            border-radius: 20px;
        }

        .asitulogInner {
            height: 70px;
        }

            .asitulogInner .logo {
                float: left;
                margin: 10px 0px 0px 15px;
                width: 240px;
            }

            .asitulogInner .image {
                float: right;
                margin: 5px;
                width: 240px;
                margin-left: -10px;
            }

            .asitulogInner p {
                color: #7373ce;
                font-size: 11px;
                margin: 0px 0px 8px 0px;
            }

        .verson {
            color: #999;
            font-size: 9px;
            margin-left: 10px;
            margin-bottom: 10px !important;
        }
        /*----------------------------------------------------*/
        .asituloginnerIn {
            overflow: hidden;
            padding: 2px;
            width: 320px;
            /*margin: 0px auto;*/
        }

            .asituloginnerIn .lblName {
                width: 100px;
                float: left;
            }

            .asituloginnerIn .inputTxt {
                width: 204px;
                margin: 0 auto;
                display: table;
            }

            .asituloginnerIn .ddllist {
                width: 210px;
                float: right;
            }

            /*----------------------------------------------------*/
            .asituloginnerIn .remember {
                font-size: 15px;
                color: #333;
                /*margin-left: 50px;*/
            }

            .asituloginnerIn .asitulogbtn {
                font-size: 14px;
                margin-top: 20px;
                padding: 3px 26px;
                background-color: #337ab7;
                border-color: #2e6da4;
                color: #fff;
                border-radius: 4px;
                display: table;
                margin: 0 auto;
            }

                .asituloginnerIn .asitulogbtn:hover {
                }

        #particles {
            width: 100%;
            height: 100%;
            overflow: hidden;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            position: absolute;
            z-index: -2;
        }

        .iconmelon,
        .im {
            position: relative;
            width: 150px;
            height: 140px;
            display: block;
            fill: #525151;
        }

            .iconmelon:after,
            .im:after {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }
        /*Login Page Style*/



        .accstext {
            font-size: 19px;
            margin: 0px 0 0 5px;
            color: #099FD4;
            font-weight: 500;
        }

        img {
            vertical-align: bottom;
        }
    </style>

    <style>
        .appsContinerLogin {
            width: 100%;
        }

        .appsContinerLogin {
            position: relative;
            background: #4c4c4c;
            overflow: hidden;
            height: 100vh;
        }

        .mainpage {
            position: relative;
            z-index: 2;
        }
        /* You could use :after - it doesn't really matter */
        .appsContinerLogin:before {
            content: ' ';
            display: block;
            position: absolute;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 1;
            opacity: 0.3;
            background-image: url("Image/Loginbg.jpg");
            background-repeat: no-repeat;
            background-size: cover;
        }

        .appsContinerLogin {
            padding: 0;
            margin: 0;
        }

        .asitulogin {
        }

        #lblalrtmsg {
            font-size: 16px;
        }
    </style>

    <script type="text/javascript">

        function searchKeyPress(e) {

            e = e || window.event;

            if (e.keyCode == 13) {

                document.getElementById("<%=loginBtn.ClientID %>").click();

                return false;
            }
            return true;
        }

        $(document).ready(function () {
            $(".toggle-password").click(function () {



                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    $(this).removeClass("glyphicon glyphicon-eye-open");
                    $(this).addClass("glyphicon glyphicon-eye-close");
                    input.attr("type", "text");
                }
                else {
                    $(this).removeClass("glyphicon glyphicon-eye-close");
                    $(this).addClass("glyphicon glyphicon-eye-open");
                    input.attr("type", "password");
                }
            });

            $('#WCModal').modal('show');

            $('#ForgetPass').click(function () {
                $('#myModal').modal('toggle');//.modal('show')/.modal('hide');
            });
            $('#myModal').on('hidden.bs.modal', function () { $('#ForgetPass').removeAttr('checked'); })

            $('.chzn-select').chosen({ search_contains: true });


        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };

        function leave() {    ///// using festival option
            //  window.location = "http://webdesign.about.com";

            // $('#WCModal').modal('hide');
        }
        setTimeout("leave()", 8000);  ///// using festival option


    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });



        }

<%--        $(document).ready(function () {
            try {

               // alert("Programmer");

            }

            catch (e) {


                alert(e);
            }



            //   $('#WCModal').modal('show');


            // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            //$('#ForgetPass').click(function () {
            // $('#myModal').modal('toggle');//.modal('show')/.modal('hide');
            //});
            //$('#myModal').on('hidden.bs.modal', function () { $('#ForgetPass').removeAttr('checked'); })

            pageLoaded();





        });
        function leave() {    ///// using festival option
            //  window.location = "http://webdesign.about.com";

            // $('#WCModal').modal('hide');
        }
        setTimeout("leave()", 8000);  ///// using festival option
        $('.chzn-select').chosen({ search_contains: true });
      


        function pageLoaded() {


           
            try {

              

                $('#<%=this.txtuserid.ClientID%>').focus();


                   $("input, select").bind("keydown", function (event) {
                       var k1 = new KeyPress();
                       k1.TxtLogin(event);
                   });




                   $(".toggle-password").click(function () {
                      
                      
                       var input = $($(this).attr("toggle"));                       
                       if (input.attr("type") == "password") {

                          // alert('dd');
                           $(this).removeClass("glyphicon glyphicon-eye-open");
                           $(this).addClass("glyphicon glyphicon-eye-close");
                           input.attr("type", "text");
                           //alert('hi');
                       }
                       else {
                           
                           $(this).removeClass("glyphicon glyphicon-eye-close");
                           $(this).addClass("glyphicon glyphicon-eye-open");
                           input.attr("type", "password");
                           //alert('ki')
                       }
                   });
               }
               catch (e) {
                   alert(e);
               }


           };--%>
</head>
<body>

    <form id="form1" runat="server">


        <section class="appsContinerLogin">
            <div class="mainpage">
                <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div class="asitulogin">
                    <div id="asitulogmain">

                        <div class="asitulogInner">
                            <div class="col-md-4">
                                <asp:Image ID="Image1" CssClass="Companylogo" runat="server" ImageUrl="~/Image/LOGO1.PNG" Style="margin-top: 10px;" Width="500px" />
                                <%--<img src="Image1.jpg"  alt="img2" height="45" width="125" style="margin-top: 10px;" />--%>
                            </div>

                            <div class="col-md-3">
                                <img src="Image/handshake.jpg" height="50" width="70" style="margin-top: 10px;" alt="handshak" />
                                <span class="verson">Version 3.1.02</span>
                            </div>
                            <div class="col-md-5">
                                <img src="image/erplogo.png" alt="img" width="170" />
                                <p>We ensure your counting digitally </p>

                            </div>

                        </div>
                        <br />
                        <div class="form-group">

                            <div>
                                <div id="loginform" class="form-horizontal">
                                    <div style="margin-bottom: 1px" class="input-group">
                                        <span class="input-group-addon">Company</span>
                                        <asp:DropDownList ID="listComName" class="form-control chzn-select" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="listComName_SelectedIndexChanged"></asp:DropDownList>


                                    </div>
                                    <div style="margin-bottom: 1px; display: none" class="input-group">
                                        <span class="input-group-addon">Module</span>
                                        <asp:DropDownList ID="ListModulename" class="form-control chzn-select" runat="server" AutoPostBack="True" TabIndex="2"></asp:DropDownList>



                                    </div>
                                    <div style="margin-bottom: 1px" class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox ID="txtuserid" runat="server" class="form-control " name="txtuserid" value="" required="required" title="Please enter you username" onkeypress="return searchKeyPress(event);" placeholder="User Name or Id" TabIndex="3"></asp:TextBox>


                                    </div>

                                    <div style="margin-bottom: 1px" class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                        <%-- <asp:TextBox ID="txtuserpass" runat="server" class="form-control" name="txtuserpass" TextMode="Password" required="required" placeholder="Password" AutoCompleteType="Cellular" >
                            </asp:TextBox>
                             <span toggle="#pwd" class="glyphicon glyphicon-eye-open field-icon toggle-password " aria-hidden="true"></span>--%>
                                        <%--onkeypress="return searchKeyPress(event);"--%>
                                        <asp:TextBox runat="server" ID="txtuserpass" AutoCompleteType="Cellular" type="password" class="form-control" name="txtuserpass" onkeypress="return searchKeyPress(event);" placeholder="Password" TabIndex="4"></asp:TextBox>
                                        <span toggle="#txtuserpass" class="glyphicon glyphicon-eye-open field-icon toggle-password"></span>

                                    </div>

                                    <div class="asituloginnerIn">
                                        <span class="remember">
                                            <asp:CheckBox ID="ChkChangePass" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="ChkChangePass_CheckedChanged" />
                                            Change Pass</span>
                                        <span class="remember">
                                            <input type="checkbox" id="ForgetPass" />
                                            Forget Password</span>

                                        <asp:LinkButton ID="loginBtn" runat="server" OnClick="loginBtn_Click" Class="btn btn-primary pull-right " TabIndex="5"> Sign in</asp:LinkButton>
                                    </div>

                                    <div class="asituloginnerIn">
                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span id="lbloldPass" runat="server" visible="false" class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                            <asp:TextBox ID="txtuserOldrpass" runat="server" AutoCompleteType="Disabled" class="form-control" Visible="False" name="txtuserpass" TextMode="Password" placeholder="Password" TabIndex="6"></asp:TextBox>

                                        </div>


                                    </div>
                                    <div class="asituloginnerIn">
                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span class="input-group-addon" id="lblNewPass" runat="server" visible="false"><i class="glyphicon glyphicon-lock"></i></span>
                                            <asp:TextBox ID="txtuserNewrpass" runat="server" AutoCompleteType="Disabled" class="form-control" Visible="False" name="txtuserpass" TextMode="Password" placeholder="New Password" TabIndex="7"></asp:TextBox>

                                        </div>


                                    </div>
                                </div>




                            </div>

                        </div>

                        <div class="col-md-12"></div>
                        <asp:Label ID="lblmsg" runat="server" class="alert alert-danger col-sm-12" Visible="false">Wrong username or password</asp:Label>

                       
                    </div>

                   
                         <asp:Panel ID="pnlmsgbox" CssClass="text-center" runat="server" Visible="false">
                              <div id="asitulogmain2">
                            <p id="lblalrtmsg" runat="server"></p>
                                   </div>
                        </asp:Panel>
                   


                </div>
                <!---modal---->
                <div id="myModal" class="modal  animated rollIn " role="dialog">
                    <div class="modal-dialog ">
                        <div class="modal-content col-md-7 col-md-offset-3">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Forgot Password</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal forgotform" id="">
                                    <div class="form-group">
                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                            <asp:TextBox ID="TxtUserName" runat="server" class="form-control" name="txtuserid" value="" required="required" title="Please enter you username" placeholder="User Name or Id" TabIndex="3"></asp:TextBox>
                                        </div>

                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                                            <asp:TextBox ID="TxtEmail" runat="server" class="form-control" required="required" placeholder="Email" TabIndex="15"></asp:TextBox>

                                        </div>
                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                                            <asp:TextBox ID="TxtPhone" runat="server" class="form-control" required="required" placeholder="Phone Number" TabIndex="16"></asp:TextBox>

                                        </div>
                                        <div style="margin-bottom: 1px" class="input-group">
                                            <span class="pull-left" style="color: black; font-weight: bold; margin-right: 3px; margin-top: 2px; font-size: 12px;">Notification On: </span>
                                            <asp:RadioButtonList ID="rbtnAtten" CssClass="pull-right marrbtn" runat="server"
                                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected=""> Email</asp:ListItem>
                                                <asp:ListItem> Mobile</asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                        <div style="margin-bottom: 1px" class="input-group">

                                            <asp:Label ID="smsg" runat="server" class="control-label" Visible="false"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer ">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <asp:LinkButton ID="ForgotSubmit" runat="server" OnClick="ForgotSubmit_click" Class="btn btn-primary"> Submit</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>


                <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
            </div>
        </section>

    </form>

</body>
</html>

