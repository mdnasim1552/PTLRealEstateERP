<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SMSTypeEntry.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.SMSTypeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            // $(".TxtDateVal").on("change",function (){ 
            //     alert("The text has been changed2.");
            // });
            // $("#txtgvdValdis").change(function(){
            //     alert("The text has been changed.");
            //});
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            // document.getElementById("divscroll").scrollTop = 0;
            try {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css('width', '600px');
                $('.chosen-continer').css('height', '50px');

                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e)
            {

            }
        }


    </script>
    <div class="card card-fluid container-data mt-5" id='printarea' style="min-height: 600px;">
        <div class="card-body">

            <div class="row">
            <div class="col-md-5">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-8 p-0 mt-2 pading5px">
                            <div class="input-group input-group-alt profession-slect">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Type For SMS</button>
                                </div>
                                <asp:TextBox ID="Txtsmsfor" placeholder="Enter for SMS" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValidationError" ErrorMessage="*Field Is Required" Display="Dynamic" ControlToValidate="Txtsmsfor" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-md-8 p-0 mt-2 pading5px">
                            <div class="input-group input-group-alt profession-slect ">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">SMS Code</button>
                                </div>
                                <asp:TextBox ID="txtCode" runat="server" placeholder="Enter code.. start(51001)" onchange="checkcode()"  CssClass="form-control"></asp:TextBox>
                                   
                                 <asp:RequiredFieldValidator ID="rqValidcode" runat="server" CssClass="ValidationError" ErrorMessage="*Field Is Required" Display="Dynamic" ControlToValidate="txtCode" ForeColor="Red" ValidationGroup="savcheck"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorNum" runat="server" Display="Dynamic" ErrorMessage="Accepts only numbers with digit 5" ValidationGroup="savcheck" ForeColor="Red" ControlToValidate="txtCode" ValidationExpression="[0-9]{5}">
                                            </asp:RegularExpressionValidator>
                                <asp:Label ID="lblcode" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>                   
                        <div class="form-group">
                        <asp:LinkButton ID="lnkSave" runat="server"  ValidationGroup="savcheck" OnClick="lnkSave_Click" CssClass="btn btn-primary okBtn">Save</asp:LinkButton>

                    </div>


                </div>
            </div>


            <div class="col-md-6">
                <div class="table table-responsive">
                    <asp:GridView runat="server" ID="GvSpecification" AutoGenerateColumns="False" CssClass="table-condensed table-hover table-bordered grvContentarea"
                      AllowPaging="true">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "." %>' Width="30px">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                                           
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                  <asp:Label ID="lblcode" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'></asp:Label>
                                    <asp:Label ID="lblid" Visible="false" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                           <asp:Label ID="lblcodeedit" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'></asp:Label>
                                 </EditItemTemplate>
                                

                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesciption" runat="server" Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server" Text="Edit" OnClick="lnkedit_Click" ></asp:LinkButton>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                    </asp:GridView>
                </div>
            </div>

            </div>

        </div>
    </div>
    <script>
        function checkcode() { //This function call on text change.        
            $.ajax({
                type: "POST",            
                url: "SMSTypeEntry.aspx/Checkcode", // this for calling the web method function in cs code.
                data: '{code: "' + $("#<%=txtCode.ClientID%>")[0].value + '" }',// user name or email value
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessCode,
                failure: function (response)
                {
                        alert(response);
                }
                });
        }
        function OnSuccessCode(response) {
            var msg = $("#<%=lblcode.ClientID%>")[0];
                switch (response.d) {
                    case "true":
                        msg.style.display = "block";
                        msg.style.color = "red";
                        msg.innerHTML = "Code already exist ";
                        $('#<%=lnkSave.ClientID %>').attr("disabled", "disabled");
                        break;
                    case "false":
                        msg.style.display = "block";
                        msg.style.color = "green";
                        msg.innerHTML = "Code available for used";
                        $('#<%=lnkSave.ClientID %>').removeAttr('disabled');
                        break;
                }
            }
    </script>
   

</asp:Content>
