<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccFlow.aspx.cs" Inherits="RealERPWEB.F_22_Sal.AccFlow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            $('.chzn-select').chosen({ search_contains: true });

            //  $.keynavigation(gridview);
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


            <br />
            <br />
            <div class="row">
                <div class="col-md-6">
                    <div class=" card card-fluid">
                        <div class=" card-body" style="min-height: 250px;">

                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Main ERP </h4>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDaterange" runat="server">Company</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group m-0">
                                        <asp:DropDownList ID="ddlcomplist" runat="server" OnSelectedIndexChanged="ddlcomplist_SelectedIndexChanged" AutoPostBack="true" Width="280px" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" runat="server">Project</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group m-0">
                                        <asp:DropDownList ID="ddlProjectName" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" Width="280px" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" runat="server">Customer</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlcustomerlist" OnSelectedIndexChanged="ddlcustomerlist_SelectedIndexChanged" runat="server" Width="280px" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
                <div class="col-md-6">
                    <div class=" card card-fluid">
                        <div class=" card-body" style="min-height: 250px;">

                            <div class="row">
                                <div class="col-md-12">
                                    <h4>Accounts ERP </h4>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" id="Label2" runat="server">Company</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group m-0">
                                        <asp:DropDownList ID="ddlAccComplist" runat="server" OnSelectedIndexChanged="ddlAccComplist_SelectedIndexChanged" AutoPostBack="true" Width="280px" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" runat="server">Project</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group m-0">
                                        <asp:DropDownList ID="ddlAccPrjlist" runat="server" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="ddlAccPrjlist_SelectedIndexChanged" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group m-0">
                                        <label class="control-label  lblmargin-top9px" for="FromDate" runat="server">Customer</label>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group m-0">
                                        <asp:DropDownList ID="ddlAccCustomer" runat="server" Width="280px" CssClass="chzn-select  inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 pading5px ">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Add </asp:LinkButton>
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <div class=" card card-fluid">
                <div class=" card-body" style="min-height: 250px;">
                    <div class="row">
                        <div class="col-sm-12">
                           
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="false"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                 <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ERP DATA">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lblinflowid" runat="server" style="display:none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inflowid")) %>'
                                               ></asp:Label>

                                            <asp:Label ID="lgvidcardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "erp_comcod_desc")) %>'
                                                Font-Size="11PX"></asp:Label>,

                                           <asp:Label ID="Label12" runat="server"
                                               Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "erp_prj_desc")) %>'
                                               Font-Size="11PX"></asp:Label>,

                                         <asp:Label ID="Label13" runat="server"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "erp_cust_desc")) %>'
                                             Font-Size="11PX"></asp:Label>


                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Hide Accounts">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acc_comcod_desc")) %>'></asp:Label>,

                                         <asp:Label ID="Label14" runat="server" Style="text-align: left"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acc_prj_desc")) %>'></asp:Label>,

                                          <asp:Label ID="Label15" runat="server" Style="text-align: left"
                                              Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acc_cust_desc")) %>'></asp:Label>


                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete" ControlStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="True" CssClass="text-center" ToolTip="Delete" Style="text-align: right" OnClientClick="javascript:return  FunConfirm()"  OnClick="lnkDelete_Click"><i class=" fa fa-trash"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


