<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Rptfixedpage.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.Rptfixedpage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>

    <script type="text/javascript">

       


        $(document).ready(function () {

            //document.getElementById('imgbtnFindProject').style.backgroundColor = 'yellow';
           // $('#imgbtnFindProject').css('background-color', 'yellow');

            if ($('#imgbtnFindProject').length > 0)
            {
                alert('I am in ');

            }
            
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           

        });

       

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        
            $.keynavigation(gridview);
           
         
            
        };


        //function onlyNumberKey(evt) {

        //    // Only ASCII character in that range allowed
        //    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
        //    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        //        return false;
        //    return true;
        //}


  <%--      function Validate(e) {
            var keyCode = e.keyCode || e.which;
            var errorMsg = document.getElementById("lblErrorMsg");
            var textbox = document.getElementById('<%=txtSrcProject.ClientID%>');
   

            errorMsg.innerHTML = "";
       

            //Regex to allow only Alphabets Numbers Dash Underscore and Space
            var pattern = /^[a-z\d\-_\s]+$/i;
            

        
           
            


            //Validating the textBox value against our regex pattern.
            var isValid = pattern.test(String.fromCharCode(keyCode));

       
            if (!isValid) {
                errorMsg.innerHTML = "Invalid Attempt, only alphanumeric, dash , underscore and space are allowed.";
            }

            return isValid;
        }--%>
     

<%--        function Myfunction() {
            var textbox = document.getElementById('<%=txtSrcProject.ClientID%>');

            if (textbox.value.length == 0) {
                alert(" I am in ");
            }
        }--%>

        //function valid()
        //{
        //    document.getElementById(txtSrcProject).value;

        //}

       
        //    function userValid() {
        //        var Name, gender, con, EmailId, emailExp;
        //        Name = document.getElementById("txtSrcProject").value;
        //        gender = document.getElementById("ddlType").value;
        //        con = document.getElementById("txt2").value;
        //        EmailId = document.getElementById("txtmail").value;
        //        emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/; // to validate email id  

        //        if (Name == '' && gender == 0 && == '' && con == '' && EmailId == '') {
        //            alert("Enter All Fields");
        //            return false;
        //        }
        //        if (Name == '') {
        //            alert("Please Enter Login ID");
        //            return false;
        //        }
        //    }
        
    
     
    </script>

    <div class="contentPart">
        <div class="row">
              <fieldset class="scheduler-border fieldset_B">
                    <div class="form-horizontal">
                          <div class="form-group">
                              <div class="col-md-3">
                                     <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"  onkeypress="return onlyNumberKey(event);" ></asp:TextBox>
                                        
                                   <span style="color:red;" id="lblErrorMsg"></span>
                                  <%--  <asp:Label ID="lblErrorMsg" runat="server" CssClass="lblTxt lblName">  <span style="color:red;"></span></asp:Label>--%>
                                            
                                       <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn">ok </asp:LinkButton>

                              </div>
                              
                                         

                                      
                          </div>
                    </div>
              </fieldset>
        </div>


  <%--   <body bgcolor="#3366ff">    
    <form id="form2" runat="server">    
    <br />    
    <br />    
    <div>    
        <table>    
            <tr>    
                <td>    
                    Name    
                </td>    
                <td>    
                    <asp:TextBoxID="txtUserId" runat="server"></asp:TextBox>    
                </td>    
            </tr>    
            <tr>    
                <td>    
                    Email Id    
                </td>    
                <td>    
                    <asp:TextBox ID="txtmail" runat="server"></asp:TextBox>    
                </td>    
            </tr>    
            <tr>    
                <td>    
                    Gender    
                </td>    
                <td>    
                    <asp:DropDownList ID="ddlType" runat="server">    
                        <asp:ListItem Value="0">-Select-</asp:ListItem>    
                        <asp:ListItem Value="1">Male</asp:ListItem>    
                        <asp:ListItem Value="2">Female</asp:ListItem>    
                    </asp:DropDownList>    
                </td>    
            </tr>    
            <tr>    
                <td>    
                    word    
                </td>    
                <td>    
                    <asp:TextBox ID="txt1" runat="server" TextMode="word"></asp:TextBox>    
                </td>    
            </tr>    
            <tr>    
                <td>    
                    Confirm word    
                </td>    
                <td>    
                    <asp:TextBox ID="txt2" runat="server" TextMode="word"></asp:TextBox>    
                </td>    
            </tr>    
            <tr>    
                <td>    
                </td>    
                <td>    
                    <asp:Button ID="btnSave" runat="server" Text="Create" OnClientClick="return userValid();" />    
                    <asp:Button ID="Button1" runat="server" Text="Reset" />    
                </td>    
            </tr>    
        </table>    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>    
    </div>    
    </form>    
</body>  --%>

    </div>

</asp:Content>
