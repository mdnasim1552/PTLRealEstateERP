<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CashBankposition.aspx.cs" Inherits="RealERPWEB.F_17_Acc.CashBankposition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>


    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />

    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 200px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 250px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>



    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function showmodal() {

            $("#myModal").modal('show');

        }
        function pageLoaded() {
            try {

                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });

                //$('.chzn-select').chosen({ search_contains: true });
                $(function () {
                    $('[id*=DropCheck1]').multiselect({
                        includeSelectAllOption: true,


                        enableCaseInsensitiveFiltering: true,
                        //enableFiltering: true,






                    });

                });

                $('#<%=this.GvBankCash.ClientID %>').tblScrollable();
                $('#<%=this.gvCashCredPur.ClientID %>').tblScrollable();


            }


            catch (e) {

            }

        };



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

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate" Enabled="true"></cc1:CalendarExtender>


                                                <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblProName" runat="server" CssClass="lblTxt lblName" Text="Project:"></asp:Label>
                                        </div>


                                        <div class="col-md-3">
                                            <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>
                                            <%--<asp:LinkButton ID="lnkOk" style="margin-top: -20px;float:left" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_OnClick" TabIndex="2">Ok</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary primaryBtn okBtn" Style="margin: -29px 0 0 220px; float: left !important;" OnClick="lnkOk_OnClick" TabIndex="8">Ok</asp:LinkButton>
                                        </div>
                                        <%--<cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" " style="float: left;"
                                    MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="200px"></cc1:DropCheck>--%>

                                        <div class="col-md-1 pading5px">
                                        </div>
                                        <div class="col-md-5">
                                            <asp:RadioButtonList ID="rbtntype" runat="server" CssClass="btn btn-primary  checkBox" Visible="false"
                                                RepeatColumns="6" RepeatDirection="Horizontal" Style="text-align: center; width: 485px; margin-left: 200px; margin-top: -20px;">
                                                <asp:ListItem Selected="True">Group</asp:ListItem>
                                                <asp:ListItem>Monthly</asp:ListItem>
                                                <asp:ListItem>Month Details</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>

                                </div>
                        </fieldset>
                        <asp:MultiView runat="server" ID="MultiView1">
                            <asp:View runat="server" ID="VbankCashPositon">
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Panel runat="server">
                                            <asp:GridView ID="GvBankCash" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvBankCash_OnRowDataBound"
                                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Style="text-align: left" Width="550px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid" runat="server"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnactdesc" runat="server" Font-Size="11px" OnClick="lbtnactdesc_OnClick" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                                Width="350px"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Font-Size="11px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Closing Dr. Amt" ItemStyle-Font-Size="11px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsdr")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="11px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                            HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing Cr. Amt" ItemStyle-Font-Size="11px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clscr")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Font-Size="11px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                            HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />

                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-6">

                                        <asp:GridView ID="gvBakCashDt" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvBakCashDt_OnRowDataBound"
                                            ShowFooter="True" Style="text-align: left" Width="400px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="slno" runat="server"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Year Of Month" ItemStyle-Font-Size="9px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblmonthid" Visible="False" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid"))%>'></asp:Label>
                                                        <asp:Label runat="server" ID="lgvcactcode" Visible="False" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>'></asp:Label>

                                                        <asp:LinkButton ID="lbtnmonthname" runat="server" Font-Size="11px" OnClick="lbtnmonthname_OnClick" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "monthnam"))%>'
                                                            Width="150px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Font-Size="11px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Debit" ItemStyle-Font-Size="11px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDr" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                        HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit" ItemStyle-Font-Size="11px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCr" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                        HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closing Balance" ItemStyle-Font-Size="11px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcls" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                        HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Font-Size="11px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdrcr" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "drcr")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000"
                                                        HorizontalAlign="Right" />
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

                                <div class="row">
                                    <div id="myModal" class="modal fade" role="dialog">
                                        <div class="modal-dialog modalcss">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close text-danger" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Details Information</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Sl">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                                                            Style="text-align: right"
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Description">
                                                                    <%-- <HeaderTemplate>
                                        <table style="width: 30%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                        Text="Group Description" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>--%>

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
                                                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="83px" CssClass="GridLebel" Style="text-align: left;"
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
                                                                            Width="90px"></asp:Label>

                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                            <asp:View runat="server" ID="Vcashcreditpurchase">

                                <asp:GridView ID="gvCashCredPur" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvCashCredPur_OnRowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serial" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" ItemStyle-Font-Size="9px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hydescription" runat="server" Font-Size="11px" Target="_blank" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))%>'
                                                    Width="350px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbldescriptionF" runat="server" Font-Size="11px"
                                                    Width="350px">Total (Cash & Credit)</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" ForeColor="blue"
                                                HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblquantity" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltrnqtyF" runat="server" Font-Size="11px"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="blue"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblRateF" runat="server" Font-Size="11px"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="blue"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblamountF" runat="server" Font-Size="11px"
                                                    Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="blue"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                            </asp:View>



                            <asp:View runat="server" ID="vwLabourDetails">

                                <asp:GridView ID="gvlabour" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvlabour_OnRowDataBound"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserial" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description" ItemStyle-Font-Size="9px">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescriptionlab" runat="server" Font-Size="11px" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))%>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbldescriptionlabF" runat="server" Font-Size="11px"
                                                    Width="350px">Total</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />
                                            <FooterStyle Font-Bold="true" Font-Size="14px" ForeColor="blue"
                                                HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamountlab" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblamountlabF" runat="server" Font-Size="11px"
                                                    Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="blue"
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                            </asp:View>

                        </asp:MultiView>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

