<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Rptfixedpage.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.Rptfixedpage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           

        });

       

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        
            $.keynavigation(gridview);
           
            $('#txtSrcProject').keypress(
                function (event) {
                    //Allow only backspace and delete
                    if (event.keyCode != 46 && event.keyCode != 8) {
                        if (!parseInt(String.fromCharCode(event.which))) {
                            event.preventDefault();
                        }
                    }
                }
            );
            
        };


        $(function () {
            $('#txtSrcProject').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                    }
                }
            });

        var input = document.getElementById('txtSrcProject');
        input.onkeydown = function (e) {
            var k = e.which;
            /* numeric inputs can come from the keypad or the numeric row at the top */
            if ((k < 48 || k > 57) && (k < 96 || k > 105)) {
                e.preventDefault();
                return false;
            }
        };

        function Myfunction() {
            var textbox = document.getElementById('<%=txtSrcProject.ClientID%>');

            if (textbox.value.length == 0) {
                alert(" I am in ");
            }
        }

        //function validatin() {

        //    var ref = true;

        //    if (document.getElementById("txtSrcProject").nodeValue == "")
        //        alert(" I am in ");
              
        //};


            function isNumberKey(txt, evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;

                if (charCode == 46) {
                    //Check if the text already contains the . character
                    if (txt.value.indexOf('.') === -1) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    if (charCode != 43 && charCode != 45 && charCode != 42 && charCode != 47 && charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                }
                return true;
            }

    
     
    </script>

    <div class="contentPart">
        <div class="row">
              <fieldset class="scheduler-border fieldset_B">
                    <div class="form-horizontal">
                          <div class="form-group">
                              <div class="col-md-3">
                                     <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"  onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                            
                                       <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClientClick="return Myfunction()" OnClick="imgbtnFindProject_Click"   >ok </asp:LinkButton>

                              </div>
                              
                                         

                                      
                          </div>
                    </div>
              </fieldset>
        </div>

    </div>

</asp:Content>
