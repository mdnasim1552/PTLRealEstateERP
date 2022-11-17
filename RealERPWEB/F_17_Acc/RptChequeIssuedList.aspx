<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptChequeIssuedList.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptChequeIssuedList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $(document).ready(function () {
                $('.chzn-select').chosen({ search_contains: true });
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            });

            function pageLoaded() {

                $('.chzn-select').chosen({ search_contains: true });
                //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            }
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName">Bank Name</asp:Label>
                                        <asp:TextBox ID="txtSrcBank" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="lblmsg1" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-5  pading5px  ">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Size :"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" CssClass="inputTxt ddlPage" TabIndex="13" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1200</asp:ListItem>
                                            <asp:ListItem>1500</asp:ListItem>
                                            <asp:ListItem>1800</asp:ListItem>
                                        </asp:DropDownList>


                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>








                                </div>


                            </div>

                        </fieldset>


                        <div class=" table-responsive">
                            <asp:GridView ID="gvChequelist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvChequelist_PageIndexChanging" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="654px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Purticulars">
                                        <ItemTemplate>

                                            <asp:Label ID="lgpurticulars" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partyname")) %>'
                                                Width="200px"></asp:Label>
                                            <%-- <asp:Label ID="lgpurticulars" runat="server" Text='<% Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="220px"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgPrjname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mode of Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgmodepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pmode")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvoudate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Cheque No">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCheque" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: Center">Total :</asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgChequeno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cheqeno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmks")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>


