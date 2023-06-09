﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktPurReqEntry.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktPurReqEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });          

        };
        function CloseModal() {
            $('#detialsinfo').modal('hide');
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

            <div class="card card-fluid mb-2">
                <div class="card-body">
                    <asp:Panel ID="pnlReqDet" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPrjName" runat="server" class="control-label  lblmargin-top9px" Text="Cost Center"></asp:Label>
                                    <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblReqDate" runat="server" class="control-label  lblmargin-top9px" Text="Req. Date"></asp:Label>
                                    <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtCurReqDate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCurNo" runat="server" class="control-label  lblmargin-top9px" Text="Requisition No."></asp:Label>
                                    <asp:Label ID="lblCurReqNo1" runat="server" class="control-label  lblmargin-top9px" for="ReqNoCur">Requisition No.</asp:Label>
                                    <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:LinkButton ID="ImgbtnFindReq" runat="server" Text="Prev. Req.List" Font-Underline="false" OnClick="ImgbtnFindReq_Click"></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="form-control chzn-select form-control-sm"></asp:DropDownList>
                            </div>

                        </div>

                        <div class="row">
                            <%--<div class="col-3 ml-1 display-none">
                                <div class="form-group">
                                    <asp:Label ID="lblmrfno" runat="server" class="control-label  lblmargin-top9px" for="ReqNoCur">M.R.F. No.</asp:Label>
                                    <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:CheckBox ID="chkdupMRF" runat="server" Text="Dup.M.R.F" CssClass="btn btn-primary checkBox" Visible="false" />
                                <asp:CheckBox ID="chkneBudget" runat="server" Text="Not Exceed Budget" CssClass="btn btn-primary checkBox" Visible="false" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-header">
                    <asp:Panel ID="pnlSpeDet" runat="server">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPRType" runat="server" class="control-label  lblmargin-top9px" Text="Pur. Req. Type"></asp:Label>
                                    <asp:DropDownList ID="ddlPRType" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPRType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblActType" runat="server" class="control-label  lblmargin-top9px" Text="Activity Type"></asp:Label>
                                    <asp:DropDownList ID="ddlActType" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblMatType" runat="server" class="control-label  lblmargin-top9px" Text="Marketing Type"></asp:Label>
                                    <asp:DropDownList ID="ddlMarkType" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm  lblmargin-top20px" Style="margin-top: 20px;">Select</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvReqInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCancelingEdit="gvReqInfo_RowCancelingEdit"
                            OnRowEditing="gvReqInfo_RowEditing" OnRowUpdating="gvReqInfo_RowUpdating" PageSize="15" ShowFooter="True" CssClass="table-striped table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvReqDelete" runat="server" Font-Bold="True"
                                            CssClass=" btn btn-xs" OnClick="lbtngvReqDelete_Click" ToolTip="Delete Requisition">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PR Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prdesc")) %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activity Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActCat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Marketing Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMarkType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mktdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvJustification" runat="server" BackColor="Transparent" BorderStyle="none" TextMode="MultiLine" Rows="5"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "justific")) %>' Width="220px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" OnClick="lbtnResFooterTotal_Click" Width="120px"
                                            CssClass="btn btn-warning btn-sm">Total</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Budgeted Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnCheecked" runat="server" OnClientClick="return FunCheckConfirm();" OnClick="lbtnCheecked_Click" CssClass="btn  btn-success btn-sm form-control">Approved</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                </asp:TemplateField>

                                <%--9--%>
                                <asp:TemplateField HeaderText="Approved Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReqappQty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Apprx. Unit</br> Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAppUnitPrice" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' Width="85px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateResReq" runat="server" OnClick="lbtnUpdateResReq_Click" CssClass="btn btn-success btn-sm form-control">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>

                              <%--11--%>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpreqamt" runat="server" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFpreqamt" runat="server" Font-Bold="true" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Width="70px"></asp:Label>

                                    </FooterTemplate>
                                </asp:TemplateField>

                                <%--12--%>
                                <asp:TemplateField HeaderText="Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvareqamt" runat="server" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFareqamt" runat="server" Font-Bold="true" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Width="70px"></asp:Label>

                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Expected Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvExpDate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expusedt")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvExpDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvExpDate" PopupPosition="TopLeft" PopupButtonID="txtgvExpDate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>' Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="File Attach.">
                                        <ItemTemplate>
                                            <asp:Image ID="imageControl" runat="server" Width="100" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "filepath") != null ? Eval("filepath") : Eval("filepath")  %>'></asp:Image>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle CssClass="grvFooterNew" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeaderNew" />
                        </asp:GridView>
                    </div>

                    <div class="col-6 mt-2" id="dNarr" runat="server">
                        <div class="form-group">
                            <asp:Label ID="lblNarr" runat="server" CssClass="control-label  lblmargin-top9px" Font-Bold="true" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6" id="dCCDNarr" runat="server" visible="false">
                        <div class="form-group mb-2">
                            <asp:Label ID="lblCCDNarr" runat="server" Text="CCD Narr :"></asp:Label>
                            <asp:TextBox ID="txtCCDNarr" runat="server" class="form-control" TextMode="MultiLine" Rows="7"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6 mb-2" id="dCMat" runat="server">
                        <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn"
                            NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank" Visible="false">Create Material</asp:HyperLink>
                    </div>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="detialsinfo" data-backdrop="static" data-keyboard="false" aria-labelledby="staticBackdropLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="staticBackdropLabel"><i class="fas fa-hand-point-right"></i>&nbsp;Add Specification Code</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col" id="idMoSpecDet" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblMoDet" runat="server" CssClass="control-label  lblmargin-top9px" Text="Details:"></asp:Label>
                                    <asp:TextBox ID="txtspcfdesc" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbtnUpdateSpeDetails" runat="server" class="btn btn-success" data-dismiss="modal" OnClientClick="CloseModal();">Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

