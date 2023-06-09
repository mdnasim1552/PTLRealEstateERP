﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMRREntry.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktMRREntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


          <%--  var gridview = $('#<%=this.gvMRRInfo.ClientID %>');
            $.keynavigation(gridview);--%>
            $('.chzn-select').chosen({ search_contains: true });



            $('[id*=listGroup]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });



        }

        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
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
            <div class="card card-fluid mb-2 pb-3">
                <div class="card-body">
                    <asp:Panel ID="pnlMRRDettails" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblMRRDate" runat="server" class="control-label " Text="MRR Date"></asp:Label>
                                    <asp:TextBox ID="txtCurMRRDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNo" runat="server" class="control-label " Text="MRR No."></asp:Label>
                                    <asp:Label ID="lblCurMRRNo1" runat="server" class="control-label " Text="MRR00-"></asp:Label>
                                    <asp:TextBox ID="txtCurMRRNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblMRRNo" runat="server" class="control-label " Text="MRR Ref No."></asp:Label>
                                    <asp:TextBox ID="txtMRRRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:LinkButton ID="ImgbtnPreMRR" runat="server" CssClass="lblTxt lblName" OnClick="ImgbtnPreMRR_Click">Previous MRR</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevMRRList" runat="server" Width="180px" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row  mb-1">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon2">Project List</span>
                                    </div>
                                    <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary btn-sm" ToolTip="Find Project" OnClick="ImgbtnFindProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:CheckBox ID="chkdupMRR" runat="server" Text="Dup.MRR" CssClass="btn btn-primary checkBox" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon3">Supplier List</span>
                                    </div>
                                    <asp:TextBox ID="txtSupSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="ImgbtnFindSup" runat="server" CssClass="btn btn-primary btn-sm" ToolTip="Find Supplier" OnClick="ImgbtnFindSup_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:DropDownList ID="ddlSupList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSupList_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-1 ml-3 pull-right">
                                <asp:Label ID="lblPO" runat="server" class="control-label " Text="Order"></asp:Label>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-header">
                    <asp:Panel ID="pnlResDetails" runat="server" Visible="false">
                        <div class="row mt-1">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" class="control-label " Text="Chalan No"></asp:Label>
                                    <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" class="control-label " Text="QC No"></asp:Label>
                                    <asp:TextBox ID="txtQc" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblChaDate" runat="server" class="control-label " Text="Challan Date"></asp:Label>
                                    <asp:TextBox ID="txtChaDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtChaDate" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtChaDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPRType" runat="server" class="control-label " Text="Pur. Req. Type"></asp:Label>
                                    <asp:DropDownList ID="ddlPRType" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPRType_SelectedIndexChanged1"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 ml-1">
                                <div class="form-group">
                                    <asp:Label ID="lblActType" runat="server" class="control-label " Text="Activity Type"></asp:Label>
                                    <asp:DropDownList ID="ddlActType" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Select" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:LinkButton ID="lbtnSelectResAll" runat="server" Text="Select All" OnClick="lbtnSelectResAll_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvMRRInfo" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-bordered grvContentarea" OnSelectedIndexChanged="gvMRRInfo_SelectedIndexChanged">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqnomain" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvDelete" runat="server" Font-Bold="True" CssClass=" btn btn-xs" OnClick="lbtngvDelete_Click"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pur. Req. <br> Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPurReqType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activity Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActType" runat="server" Font-Bold="False" CssClass="d-block"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")) + "</B>"+
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")).Trim().Length>0 ?  "<br>" : "") 
                                                                         
                                                                    %>'></asp:Label>
                                        <asp:TextBox ID="txtgvRsirdetDesc" runat="server" Font-Bold="False" CssClass="from-control" Width="400px" TextMode="MultiLine" Rows="5" ReadOnly="true"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdetdesc"))%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order <br> Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecuptodate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance <br> Qty.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDelMRR" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnDelMRR_Click">Delete</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="This MRR">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnResFooterTotal" runat="server"
                                            OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-warning btn-sm form-control">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRQty" runat="server" BackColor="White" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateMRR" runat="server" OnClientClick="return Confirmation()" OnClick="lbtnUpdateMRR_Click" CssClass="btn btn-success btn-sm form-control">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="80px" Font-Bold="True"
                                            ></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chalan Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChlnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px"  Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "mrrnote").ToString() %>' Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooterNew" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeaderNew" />
                        </asp:GridView>
                    </div>

                    <asp:Panel ID="PnlNarration" runat="server" Visible="false">
                        <div class="row mt-2">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblreqnaration" runat="server" class="control-label " Text="Req Narration:" Font-Bold="true" Style="text-align: left"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-8 col-md-8 col-lg-8">
                                <div class="form-group">
                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Font-Bold="true" Text="Narration"></asp:Label>
                                    <asp:TextBox ID="txtMRRNarr" CssClass="form-control" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
