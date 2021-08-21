<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccMultiReport.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccMultiReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvMonIsuPay.ClientID %>');
            gv1.Scrollable();
        }


        function PrintRpt(printype) {

            window.open('../RptViewer.aspx?PrintOpt=' + printype + '', '_blank');
        }






        function PrintRptCheque(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

        }


        function PrintRptVoucher(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

        }
    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>


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
            <fieldset class="scheduler-border fieldset_A">
                <div class="form-horizontal">
                    <div class="form-group">

                        <%--  <div class="col-md-3 pading5px asitCol3">
                               
                                <asp:DropDownList ID="ddl" runat="server" CssClass=" form-control inputTxt chzn-select " Width="350px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 pading5px ">
                                <asp:DropDownList ID="ddlSupList" runat="server" CssClass=" form-control inputTxt chzn-select " Width="350px">
                                </asp:DropDownList>
                            </div>--%>

                        <asp:Panel ID="PanelProWork" runat="server" Visible="false">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-1 pading5px asitCol1">
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-3   padding5px">
                               
                                <label id="chkbod" runat="server"
                                    class="switch">
                                    <asp:CheckBox ID="chkcost" runat="server" Visible="false" />

                                    <span class="btn btn-xs slider round"></span>
                                </label>
                                <asp:Label runat="server" Text="Without Cost Adjustment" CssClass="btn btn-xs"></asp:Label>


                              


                            </div>

                            

                           
                        </asp:Panel>


                    </div>
                </div>
            </fieldset>

            <asp:MultiView ID="MultiView1" runat="server">




                <asp:View ID="ScheduleView" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Control Accounts Name" CssClass="rptheadTitel3"></asp:Label>

                                    <asp:Label ID="lblRptType" runat="server" Visible="False" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblSchReportTitle" runat="server" CssClass="rptheadTitel3"
                                        Text="ACCOUNTS CONTROL SCHEDULE"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblSchReportPeriod" runat="server" CssClass="rptheadTitel3"
                                        Text="Reporting Period"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblSchReportTitle2" runat="server"
                                        Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>
                                </div>
                            </div>

                        </fieldset>
                    </div>
                    <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="False"
                        BackColor="#DDFFEE" ShowFooter="True" Width="911px"
                        OnRowDataBound="gvSchedule_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                        Width="300px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                FooterStyle-HorizontalAlign="Right" FooterText="Total"
                                HeaderText="Descryption of Account">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                        Width="300px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Opening Dr. Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfopnDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvopenDramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Cr. Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfopnCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvopenCramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Dr. Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfcloDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingDramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Cr. Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfcloCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingCramt" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:Label ID="lblfcloNetamt" runat="server" CssClass="GridLebel"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingNetamt" runat="server" CssClass="GridLebel" Width="115px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") +
                                                        "&nbsp;&nbsp;&nbsp;"+
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "clremks"))  %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>
                </asp:View>

                <asp:View ID="LedgerView" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="LblLgReportTitle" runat="server" Text="l e d g e r" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblLgLedgerHead" runat="server" CssClass="rptheadTitel3"
                                        Text="Control Accounts Name"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblLgReportPeriod" runat="server" CssClass="rptheadTitel3"
                                        Text="Control Accounts Name"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server"
                                        Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>
                                </div>
                            </div>

                        </fieldset>
                        <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLedger_RowDataBound" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")).ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" CssClass="GridLebelL"
                                            Font-Underline="False" Target="_blank"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="85px"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
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
                </asp:View>


                <asp:View ID="VoucherView" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="LblVUVouTitle" runat="server" Text="Voucher Title" CssClass="rptheadTitel1"></asp:Label>
                                </div>
                                <div class="form-group ">


                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="LblControlCode1" runat="server" Text="Control Code" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:Label ID="LblVUControlDesc" runat="server" Text="Control Accounts Name" CssClass=" dataLblview"></asp:Label>


                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="LblVouDate1" runat="server" Text="Date" CssClass=" smLbl_to"></asp:Label>

                                        <asp:Label ID="LblVUControlCode" runat="server" Text="Control Code" CssClass="inputtextbox"></asp:Label>
                                        <asp:Label ID="LblVUVouDate" runat="server" Text="Date" CssClass="smLbl_to"></asp:Label>
                                    </div>

                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="LblRefNo1" runat="server" Text="Cheq./Ref. No." CssClass="smLbl_to"></asp:Label>

                                        <asp:Label ID="LblVURefNo" runat="server" Text="Control Code" CssClass="inputtextbox"></asp:Label>

                                        <asp:Label ID="LblVouNum1" runat="server" Text="Voucher No." CssClass="smLbl_to"></asp:Label>

                                        <asp:Label ID="LblVUVouNum" runat="server" Text="Cheq./Ref. No." CssClass="inputtextbox"></asp:Label>
                                    </div>


                                </div>

                            </div>
                        </fieldset>
                        <asp:GridView ID="gvVoucher" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouQty" runat="server" Style="text-align: right" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouRate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouDrAmt" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" CssClass="GridLebel" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouCrAmt" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" CssClass="GridLebel" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouRemarks" runat="server" CssClass="GridLebel"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="LblVUInWord" runat="server" Text="Inword" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblVUSrinfo1" runat="server" Text="Add. Ref." CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblVUSrinfo" runat="server" Text="Inword" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblNarration1" runat="server" Text="Narration" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblVUNarration" runat="server" Text="Narration" CssClass="rptheadTitel3"></asp:Label>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                </asp:View>


                <asp:View ID="SpLedgerVeiw" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="lblHeaderName" runat="server" Text="Account Special Ledger" CssClass="rptheadTitel1"></asp:Label>

                                    <asp:Label ID="lblResName" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="LblLgResRptPeriod" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <div class="table-responsive">
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                OnRowDataBound="dgv2_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                           <%-- <HeaderTemplate>
                                                <table style="width: 30%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                                Text="Project Name" Width="150px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCBdataExelsp" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>--%>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjectdesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Vou.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No.">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel"
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
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
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
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
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
                </asp:View>

                <asp:View ID="DeailsTB" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="Label2" runat="server" Text="ACCOUNTS DETAILS SCHEDULE" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="lblRptPeriod" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label><br />
                                    <asp:Label ID="LblSchReportTitle5" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                                    <asp:Label ID="lblspclecode" runat="server" Text="ACCOUNTS DETAILS SCHEDULE"  Visible="false"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="grvDTB" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvDTB_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px; font-weight:bold;"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total"
                                    HeaderText="Descryption of Account">

                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Description Of Accounts" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>

                                                <td class="style60">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;</td>
                                                <td>

                                                    <asp:HyperLink ID="hlbtntbCdataExel1" CssClass="btn btn-xs btn-denger" runat="server" ForeColor="#ff0000" Font-Bold="true" Font-Size="14px">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>

                                                    <%--  <asp:HyperLink ID="hlbtntbCdataExel1" runat="server" 
                                                              CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="Solid" Text="Export to Excel"></asp:HyperLink>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>

                </asp:View>

                <asp:View ID="ViewAccRecFin" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="lblAccFec" runat="server" Text="Account Special Ledger" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblAccRecCustomer" runat="server" Text="Resource Name" CssClass="rptheadTitel2"></asp:Label>
                                    <asp:Label ID="lblAccleb" runat="server" Text="Reporting Period" CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>

                        <asp:GridView ID="grvAccRecFin" runat="server" AutoGenerateColumns="False" BorderWidth="2px" ShowFooter="True" OnRowDataBound="grvAccRecFin_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>

                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel"
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
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusernamesp" runat="server" CssClass="GridLebelL"
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

                </asp:View>

                <asp:View ID="ViewRecPaySchu" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="Label3" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblRDate" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label>
                                    <asp:Label ID="lblRecPayCode" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>

                        <asp:GridView ID="grvRecPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True"
                            OnRowDataBound="grvRecPay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:"
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px; color: Black; font-weight: bold"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>

                <asp:View ID="ViewDetTBRP" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="Label4" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblDetRP" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label>
                                    <asp:Label ID="lblActRp" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="grvDetTbRp" runat="server" AutoGenerateColumns="False"
                            BackColor="#DDFFEE" ShowFooter="True"
                            OnRowDataBound="grvDetTbRp_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px; color: Black; font-weight: bold"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="400px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:"
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>

                </asp:View>



                <asp:View ID="ViewPrjRepRP" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblDuType" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label>
                                    <asp:Label ID="lblActcodePRJ" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="grvPrjRptRP" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvPrjRptRP_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="SubCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSubcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "subcode1").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="14px" HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDesc" runat="server"
                                            CssClass="GridLebelL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total Amount:" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Size="14px" HeaderText="Resource  Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" __designer:wfdid="w38"
                                            CssClass="GridLebelL" Font-Size="12px" Style="color: Black; font-weight: bold" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Receipt Amt" ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Payment Amt" ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>

                </asp:View>

                <asp:View ID="View1" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="Label3123" runat="server" Text="Date" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblDate" runat="server" CssClass="rptheadTitel2"></asp:Label>
                                    <asp:Label ID="Label13" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="lblResDesc" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gvMonIsuPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" OnRowDataBound="gvMonIsuPay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgcatCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSupname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAccDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVnum" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounum1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVDate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchnono" runat="server" Width="100px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Cleared Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreconamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbcldate" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>
                </asp:View>


                <asp:View ID="SpcLedgerprj" runat="server">
                    <div class="row">


                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group ">
                                    <asp:Label ID="lblledgern" runat="server" Text="Account Special Ledger" CssClass="rptheadTitel1"></asp:Label>
                                    <asp:Label ID="lblres" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>
                                    <asp:Label ID="lblperiod" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>

                                </div>
                            </div>
                        </fieldset>
                        <div class="table table-responsive">
                            <asp:GridView ID="gvSpledgerprj" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvSpledgerprj_OnRowDataBound"
                                ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrp" runat="server" CssClass="GridLebelL"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">


                                        <%--<HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblheader" runat="server" Text="Project Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExelsp" runat="server" CssClass=" btn btn-success btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>--%>


                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Vou.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No.">

                                        <ItemTemplate>



                                            <asp:HyperLink ID="HLgvvounum" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                                CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="80px"></asp:HyperLink>




                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClAmount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Narration">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvnarration" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
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
                </asp:View>




            </asp:MultiView>



            <div class="table table-responsive">
                <asp:GridView ID="gvSpledger" runat="server" AutoGenerateColumns="False"
                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvSpledger_RowDataBound"
                    ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Group Description">
                            <ItemTemplate>
                                <asp:Label ID="lblGrp01" runat="server" CssClass="GridLebelL"
                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project Name">


                            <HeaderTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblheader01" runat="server" Text="Project Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="hlbtntbCdataExelsp01" runat="server" CssClass=" btn btn-success btn-xs  fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>


                            <ItemTemplate>
                                <asp:Label ID="lblProjectName01" runat="server" CssClass="GridLebelL"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                    Width="120px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Vou.Date">
                            <ItemTemplate>
                                <asp:Label ID="lblgvvoudate01" runat="server" CssClass="GridLebelL"
                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Voucher No.">

                            <ItemTemplate>



                                <asp:HyperLink ID="HLgvvounum01" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                    Width="75px"></asp:HyperLink>




                            </ItemTemplate>
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Opening">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOpAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Dr. Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvDrAmount01" runat="server" CssClass="GridLebel"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cr. Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblgvCrAmount01" runat="server" CssClass="GridLebel"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Closing">
                            <ItemTemplate>
                                <asp:Label ID="lblgvClAmount01" runat="server" CssClass="GridLebel"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cheque No">
                            <ItemTemplate>
                                <asp:Label ID="lblChqNo01" runat="server" CssClass="GridLebelL"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                    Width="65px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Narration">
                            <ItemTemplate>
                                <asp:Label ID="lblgvnarration01" runat="server" CssClass="GridLebelL"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                    Width="220px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblremarks01" runat="server" CssClass="GridLebelL"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                    Width="85px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle VerticalAlign="Top" />
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




</asp:Content>


