<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderHistorySup.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptWorkOrderHistorySup" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
       /* .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
            height: 28px !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }*/

       tr#ContentPlaceHolder1_Cal3_daysTableHeaderRow td {
        }

        .GridViewScrollHeader TH, .GridViewScrollHeader TD, .GridViewScroll1Header TH, .GridViewScroll1Header TD, .GridViewScroll2Header TH, .GridViewScroll2Header TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }


        .GridViewScrollItem TD, .GridViewScroll1Item TD, .GridViewScroll2Item TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD, .GridViewScroll1ItemFreeze TD, .GridViewScroll2ItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD, .GridViewScroll1FooterFreeze TD, .GridViewScroll2FooterFreeze TD {
            /*white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;*/
            /*white-space: nowrap;
            background-color: #005F8D;
            border: 1px solid #ddd;
            color: #fff;
            line-height: 1.42857;
            padding: 1px 12px;
            text-decoration: none;*/
        }

        .grvHeader {
            height: 55px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }

        .mt20 {
            margin-top: 20px;
        }

    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            var gridViewScroll = new GridViewScroll({
                elementID: "WorkOrdHisSup",
                width: 1160,
                height: 400,
                freezeColumn: true,
                freezeFooter: true,
                freezeColumnCssClass: "GridViewScrollItemFreeze",
                freezeFooterCssClass: "GridViewScrollFooterFreeze",
                freezeHeaderRowCount: 1,
                freezeColumnCount: 8,

            });

            gridViewScroll.enhance();



            var gridViewsScroll = new GridViewScroll({
                elementID: "gvWorkOrdHisRes",
                width: 1160,
                height: 400,
                freezeColumn: true,
                freezeFooter: true,
                freezeColumnCssClass: "GridViewScrollItemFreeze",
                freezeFooterCssClass: "GridViewScrollFooterFreeze",
                freezeHeaderRowCount: 1,
                freezeColumnCount: 8,

            });

            gridViewsScroll.enhance();

           <%-- var WorkOrdHisSup = $('#<%=this.WorkOrdHisSup.ClientID%>');
            WorkOrdHisSup.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 11


            });



            var gvWorkOrdHisRes = $('#<%=this.gvWorkOrdHisRes.ClientID%>');
            gvWorkOrdHisRes.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 11


            });--%>


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
            </div>
            <div class="card mt-3">
                <div class="card-header well">
                    <div class="row">
                        <div class="col-md-2" runat="server" ID="project" visible="false" >
                            <div class="form-group">
                                <asp:Label ID="lblproject" runat="server" CssClass="lblTxt lblName" Text="Peoject Name"></asp:Label>
                                <asp:DropDownList ID="ddlprojectname" runat="server" CssClass="chzn-select form-control  ">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblAcchead" runat="server" CssClass="lblTxt lblName" Text="Supplier Name">
                                    <asp:LinkButton ID="imgbtnFindSupplier" runat="server" OnClick="imgbtnFindSupplier_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="chzn-select form-control  ">
                                </asp:DropDownList>
                            </div>
                        </div>
                                                
                            <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lbldateRange" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-1 mt-4">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" OnClick="lnkbtnOk_Click" CssClass="btn btn-primary btn-sm">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                    

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Visible="false" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass="chzn-select form-control">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewWorkHisSup" runat="server">
                            <asp:GridView ID="WorkOrdHisSup" runat="server" AllowPaging="True" ClientIDMode="Static"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="WorkOrdHisSup_PageIndexChanging" ShowFooter="True"
                                
                                OnRowDataBound="WorkOrdHisSup_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupplier" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordDate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRF NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrfno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpecification" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRsUnit" runat="server" Font-Size="11px" Width="30px"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorderqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnitRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Purchase Order Cost Provision">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPurOrdCos" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFPurOrdCos" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDelvNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivered Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeliveredQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivered Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeliveredAmt" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFDeliveredAmt" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBillNo" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBillQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalAmt" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBalAmt" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Paid Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcPamt" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acpaidamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAcPamt" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amount based on PO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBlamtPO" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balbonpo")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBlamtPO" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amount based on Delivery">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalAmBasDelv" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balbonmrr")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBalAmBasDelv" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amount based on Unpaid Bill">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalAmBasUnpad" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balbonbill")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBalAmBasUnpad" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewWorkHisRes" runat="server">
                            <div class="row">
                                
                                    <div class="col-md-3 ">
                                         <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Resource Code">
                                            <asp:LinkButton ID="ImgbtnSrchRes" runat="server" OnClick="ImgbtnSrchRes_Click"><span class=" fa fa-search"> </span></asp:LinkButton>
                                        </asp:Label>
                                        <asp:TextBox ID="txtResCode" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                    </div>
                                        </div>

                                    <div class="col-md-3 mt-3 ">
                                         <div class="form-group">
                                        <asp:DropDownList ID="ddlResoName" runat="server" CssClass="chzn-select form-control">
                                        </asp:DropDownList>
                                    </div>
                                        </div>
                                
                            </div>


                            <asp:GridView ID="gvWorkOrdHisRes" runat="server" AllowPaging="True"  ClientIDMode="Static"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvWorkOrdHisRes_PageIndexChanging" ShowFooter="True"
                               
                                OnRowDataBound="gvWorkOrdHisRes_RowDataBound">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordnores" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <%--<asp:Label ID="lblgvTotalres" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>--%>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordDateres" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqnores" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRF NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrfnores" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpecificationres" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRsUnitres" runat="server" Font-Size="11px" Width="30px"
                                               
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjDescres" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnitRateres" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorderqtyres" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Purchase Order Cost Provision">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPurOrdamtres" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                           <%-- <asp:Label ID="lblgvFPurOrdCos0" runat="server" Font-Size="12px"
                                                Height="16px" Style="text-align: right"></asp:Label>--%>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delivery No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDelvNores" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delivered Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeliveredQtyres" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivered Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeliveredAmtres" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBillNo" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBillQty" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalAmt" runat="server" Font-Size="11px"
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcPamtqty" runat="server" Font-Size="11px"
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acpaidqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payment Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcPamtres" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acpaidamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorddbalqty" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvorddbalamtres" runat="server" Font-Size="11px"
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordbalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrrbalqty" runat="server" Font-Size="11px"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mrr Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrrbalamtres" runat="server" Font-Size="11px"
                                             
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrbalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillbalqty" runat="server" Font-Size="11px"
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bilbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Balance Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillbalPamtres" runat="server" Font-Size="11px"
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bilbalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>


                        </asp:View>

                        <asp:View ID="ViewOrderVsSupplier" runat="server" >
                            <asp:GridView ID="gv_OrderVsSupplier" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gv_OrderVsSupplier_PageIndexChanging"
                                ShowFooter="True">
                               
                                 <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" 
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactdesc" runat="server" Height="16px" Width="250px"
                                                    Text='<%#Eval("pactdesc").ToString()%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupsupplier" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supplier")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="left" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsuporderdate" runat="server" Height="16px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="110px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="left" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsuporderdate" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                         <ItemStyle HorizontalAlign="left" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reque No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsuporderdate" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="Center" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupordrqty" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="right" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupmrramt" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                         <ItemStyle HorizontalAlign="right" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Balance QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupbalanceqty" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balanceqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                         <ItemStyle HorizontalAlign="right" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="ToTal Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsupmrramt" runat="server" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lblgvTotal" runat="server" Font-Size="12px"
                                                Text="Total" Width="60px"></asp:Label>
                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="right" />
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <RowStyle />
                                 <FooterStyle CssClass="grvFooter" />                              
                               
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

