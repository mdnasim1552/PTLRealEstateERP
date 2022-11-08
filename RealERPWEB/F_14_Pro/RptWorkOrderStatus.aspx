<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptWorkOrderStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            try {

                
                var gridViewScroll = new GridViewScroll({
                    elementID: "gvDeWorkOrdSt",
                    width: 1200,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });
                var gridViewScroll1 = new GridViewScroll({
                    elementID: "gvReqStatus",
                    width: 1200,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScroll1ItemFreeze",
                    freezeFooterCssClass: "GridViewScroll1FooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });
                var gridViewScroll2 = new GridViewScroll({
                    elementID: "gvReqVsOrder",
                    width: 1200,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScroll2ItemFreeze",
                    freezeFooterCssClass: "GridViewScroll2FooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });
              

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvBonus",
                //    width: 1000,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 8,

                //});
              
                gridViewScroll.enhance();
                gridViewScroll1.enhance();
                gridViewScroll2.enhance();
        
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }
        }
    </script>
    
    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD,.GridViewScroll1Header TH, .GridViewScroll1Header TD,.GridViewScroll2Header TH, .GridViewScroll2Header TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }


        .GridViewScrollItem TD, .GridViewScroll1Item TD,.GridViewScroll2Item TD{
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD, .GridViewScroll1ItemFreeze TD,  .GridViewScroll2ItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD, .GridViewScroll1FooterFreeze TD, .GridViewScroll2FooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 38px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
                       .mt20 {
            margin-top: 20px;
        }
                       .table th, .table td{
                           padding:0px;
                       }
                       .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;}
                       
    </style>

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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-3 pading5px asitCol3 d-none">

                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                            <div class="colMdbtn">
                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>

                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label">Project Name</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select chzn-single form-control  form-control-sm">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-2 ml-2">
                            <asp:Label ID="lbldatefrm" runat="server" CssClass="form-label">Date</asp:Label>


                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbldateto" runat="server" CssClass="form-label" Style="margin-right: 7px;">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 pading5px" style="margin-top:22px;">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                        </div>

                        <div class="col-md-1 pading5px asitCol3">
                            <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="form-label"></asp:Label>

                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm"
                                TabIndex="4">
                                <asp:ListItem Value="10">10</asp:ListItem>
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
                    <div class="row mt-3 mb-2">
                         <div class="col-md-2 ">
                           
                            <asp:RadioButtonList ID="rbtnpurtype" runat="server" Visible="true" BackColor="#DBEBD4" CssClass="form-control" RepeatColumns="6" RepeatDirection="Horizontal" >
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Credit</asp:ListItem>
                                <asp:ListItem Selected="True">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-3">
                             <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#DBEBD4"
                                CssClass="form-control"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Requisition Basis</asp:ListItem>
                                <asp:ListItem>Material Basis</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                       
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkBalance" runat="server" Text="Without Zero Balance" CssClass="form-control" />
                        </div>



                    </div>
                    
                </div>
                </div>
            <div class="card" style="min-height:480px;">
                <div class="card-body">
                    <div class="row">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="WorkIOrdStatus" runat="server">
                        <asp:GridView ID="gvReqStatus" ClientIDMode="Static" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvReqStatus_PageIndexChanging"
                            Style="text-align: left" Width="889px">
                            
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Font-Size="11px" 
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDesc" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqNo1" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRF No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMrfNo" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Materials">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResDesc" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px" 
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Completed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderQty" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remaining Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpfDesc" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="DetailsWorkIOrdStatus" runat="server">


                        <asp:GridView ID="gvDeWorkOrdSt" runat="server" AutoGenerateColumns="False" ClientIDMode="Static"
                            ShowFooter="True" Width="831px" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea "
                            OnPageIndexChanging="gvDeWorkOrdSt_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Font-Size="11px" 
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                     <HeaderTemplate>
                                        <asp:Label ID="lblexle" runat="server" Font-Bold="True" Width="100px" Font-Size="11px" 
                                            Text="Project Name">
                                            <asp:HyperLink ID="hlbtntbCdataExelSP" runat="server"
                                                CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                        </asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPactdesc" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px" Font-Bold="true"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrdno" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Ref">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdRef" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdDat" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqNo" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno2")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRF No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMrfNo" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Appoved No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAppNo" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno2")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Suppliers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSupDesc" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Materials">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatDesc" runat="server" Font-Size="11px" style="margin-left:20px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBrName" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdqty" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOrderqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approve Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAppqty" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAppqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTAmt" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Process">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEnUser" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrAppUser" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprusername")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Print">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrPrUser" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orusername")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />


                        </asp:GridView>


                    </asp:View>


                    <asp:View ID="ReqVsOrder" runat="server">


                        <asp:GridView ID="gvReqVsOrder" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea "
                            OnPageIndexChanging="gvDeWorkOrdSt_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Font-Size="11px" 
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPactdescreqVsOrd" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px" Font-Bold="true"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mrf No">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrfreqVsOrd" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Req. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqNo" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Req Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvReqVsOrdDat" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgOrN1o" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="gvOrderdat1" runat="server" Font-Size="11px"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrddaylimit" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daylimit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdaylimit" runat="server" Font-Bold="True" Font-Size="11px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Suppliers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSupDescorde" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatreqvsord" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBrordName" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvdiordtem" runat="server" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnitord" runat="server" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <%--    <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOrderqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approve Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAppqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrAppUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprusername")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Print">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrPrUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orusername")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
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




            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="11">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">

                                <tr>
                                    <td class="style29">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 1018px;">
                                                <tr>
                                                    <td class="style35" style="text-align: left">
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style36" style="text-align: left">
                                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat"
                                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td class="style44" style="text-align: left">
                                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px"
                                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnFindProject_Click"
                                                            Width="16px" />
                                                    </td>
                                                    <td class="style67" style="text-align: left" colspan="3">
                                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="300px">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                                        </cc1:ListSearchExtender>

                                                        <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#99FFCC" Height="20px"
                                                            OnClick="lnkbtnOk_Click" Style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: left" class="style58"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                    <td style="text-align: left"></td>
                                                </tr>
                                                <tr>
                                                    <td class="style46" style="text-align: left">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="color: #FFFFFF; text-align: right;" Text="Date:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style47" style="text-align: left">
                                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style48" style="text-align: left">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="14px"
                                                            Style="text-align: right; color: #FFFFFF;" Text="To:"></asp:Label>
                                                    </td>
                                                    <td class="style49" style="text-align: left" colspan="3">
                                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat"
                                                            Font-Bold="False" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style57" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                    <td class="style50" style="text-align: left"></td>
                                                </tr>
                                                <tr>
                                                    <td class="style35" style="text-align: left">
                                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="#993300" Style="color: #FFFFFF; text-align: right;" Text="Page Size"
                                                            Visible="False" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style36" style="text-align: left">
                                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                            <asp:ListItem>100</asp:ListItem>
                                                            <asp:ListItem>150</asp:ListItem>
                                                            <asp:ListItem>200</asp:ListItem>
                                                            <asp:ListItem>300</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style44" style="text-align: left">&nbsp;</td>
                                                    <td class="style68" style="text-align: left">
                                                       
                                                    </td>
                                                    <td class="style62" style="text-align: left">
                                                        
                                                    </td>
                                                    <td class="style62" style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left" class="style58">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                    <td style="text-align: left">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



