
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkAccLedger.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkAccLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#155273" ForeColor="White" CssClass="btn btn-primary checkBox"
                                        RepeatColumns="6" RepeatDirection="Horizontal">
                                        <asp:ListItem>With Narration</asp:ListItem>
                                        <asp:ListItem>Without Narattion</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-3 pading5px pull-right">
                                    <div class="msgHandSt">
                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                    </div>


                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads"></asp:Label>
                                    <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="IbtnSearchAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkShowLedger_Click">Show</asp:LinkButton>

                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom">
                                    </cc1:CalendarExtender>
                                    

                                        <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto">
                                        </cc1:CalendarExtender>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server" Visible="false" Width="1036px">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Get Resource Heads"></asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlConAccResHead_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlConAccResHead">
                                        </cc1:ListSearchExtender>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:CheckBox ID="chkqty" runat="server" CssClass="checkBox" Text="With qty" />
                                    </div>

                                </div>
                            </fieldset>


                        </asp:Panel>
                        
                    </div>
                    <div class="table-responsive row">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="dgv2_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Group Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="85px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="250px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnrate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="100px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="100px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                            </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

