<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_new.aspx.cs" Inherits="RealERPWEB.login_new" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- End Required meta tags -->
    <!-- Begin SEO tag -->
    <title>Sign In | Looper - Bootstrap 4 Admin Theme </title>
    <meta property="og:title" content="Sign In">
    <meta name="author" content="Beni Arisandi">
    <meta property="og:locale" content="en_US">
    <meta name="description" content="Responsive admin theme build on top of Bootstrap 4">
    <meta property="og:description" content="Responsive admin theme build on top of Bootstrap 4">
    <link rel="canonical" href="//uselooper.com">
    <meta property="og:url" content="//uselooper.com">
    <meta property="og:site_name" content="Looper - Bootstrap 4 Admin Theme">
    <!-- Favicons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/apple-touch-icon.png">
    <link rel="shortcut icon" href="assets/favicon.ico">
    <meta name="theme-color" content="#3063A0">
    <%--  <!-- BEGIN BASE STYLES -->
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/vendor/font-awesome/css/fontawesome-all.min.css">
    <!-- END BASE STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link rel="stylesheet" href="assets/stylesheets/main.min.css">
    <link rel="stylesheet" href="assets/stylesheets/custom.css">
    <!-- END THEME STYLES -->--%>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/Content/cssnew") %>
       
        
    </asp:PlaceHolder>

</head>
<body>
    <!-- .auth -->
    <main class="auth auth-floated">
        <!-- form -->
        <form class="auth-form">
            <div class="mb-5 mt-5">
                <div class="mb-3">
                    <asp:Image ID="Image1" CssClass="rounded" runat="server" ImageUrl="~/Image/LOGO1.PNG" alt="" Height="100" />
                </div>

            </div>
            <!-- .form-group -->
            <div class="form-group mb-4">
                <label class="d-block text-left" for="inputUser">Company</label>
                <asp:DropDownList ID="listComName" class="form-control form-control-lg chzn-select" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="listComName_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="form-group d-none mb-4">
                <label class="d-block text-left" for="inputUser">Module</label>

                <asp:DropDownList ID="ListModulename" class="form-control form-control-lg chzn-select" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="listComName_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <!-- .form-group -->
            <div class="form-group mb-4">
                <label class="d-block text-left" for="inputUser">Username</label>
                <asp:TextBox ID="txtuserid" runat="server" class="form-control form-control-lg" name="txtuserid" autofocus="" value="" required="required" title="Please enter you username" onkeypress="return searchKeyPress(event);" placeholder="User Name or Id" TabIndex="3"></asp:TextBox>

            </div>
            <!-- /.form-group -->
            <!-- .form-group -->
            <div class="form-group mb-4">
                <label class="d-block text-left" for="inputPassword">Password</label>
                <asp:TextBox runat="server" ID="txtuserpass" AutoCompleteType="Cellular" type="password" class="form-control form-control-lg" name="txtuserpass" onkeypress="return searchKeyPress(event);" placeholder="Password" TabIndex="4"></asp:TextBox>

            </div>
            <!-- /.form-group -->
            <!-- .form-group -->
            <div class="form-group mb-4">
                <button class="btn btn-lg btn-primary btn-block" type="submit">Sign In</button>
            </div>
            <!-- /.form-group -->
            <!-- .form-group -->
            <div class="form-group text-center">
                <div class="custom-control custom-control-inline custom-checkbox">
                    <input type="checkbox" class="custom-control-input" id="remember-me">
                    <label class="custom-control-label" for="remember-me">Keep me sign in</label>
                </div>
            </div>
            <!-- /.form-group -->
            <!-- recovery links -->
            <p class="py-3">
                <span class="mx-2">·</span>
                <a href="auth-recovery-password.html" class="link">Forgot Password?</a>
            </p>

        </form>
        <!-- /.auth-form -->
        <!-- .auth-announcement -->
        <section id="announcement" class="auth-announcement" style="background-image: url(image/Loginbg.jpg);">
            <div class="announcement-body">
                <h2 class="announcement-title"></h2>

            </div>
        </section>
        <!-- /.auth-announcement -->
    </main>
    <!-- /.auth -->
    <!-- BEGIN PLUGINS JS -->
    <script src="assets/vendor/particles.js/particles.min.js"></script>
    <script>
        /* particlesJS.load(@dom-id, @path-json, @callback (optional)); */
        particlesJS.load('announcement', 'assets/javascript/particles.json');
    </script>
</body>
</html>
