﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProjectStock.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptProjectStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            width: 280px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }
        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }
        .multiselect-text {
            width: 200px !important;
        }
        .caret {
            display: none !important;
        }
        span.multiselect-selected-text {
            width: 200px !important;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
        .form-control {
            height: 34px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $(function () {
                $('[id*=chkResourcelist]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,
                });
            });

            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvMatStock.ClientID %>');
            gv.Scrollable();
            var gv1 = $('#<%=this.gvMatStockSpec.ClientID%>');
            gv1.Scrollable();
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lbldate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" TabIndex="1" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lbltodate" runat="server" CssClass="control-label" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" TabIndex="1" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectList0" runat="server" CssClass="control-label" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" Visible="false" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlProName" runat="server" CssClass="chzn-select form-control form-control-sm" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblresName" runat="server" CssClass="control-label" Text="Material List"></asp:Label>
                                <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="lbtnresource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnresource_Click" Visible="false" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                <div class="col-md-4 pl-0">

                                    <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblRptGroup" runat="server" CssClass="control-label" Text="Group"></asp:Label>
                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="form-control form-control-sm chzn-select">
                                <asp:ListItem>Main</asp:ListItem>
                                <asp:ListItem>Sub-1</asp:ListItem>
                                <asp:ListItem>Sub-2</asp:ListItem>
                                <asp:ListItem>Sub-3</asp:ListItem>
                                <asp:ListItem Selected="True">Details</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>900</asp:ListItem>
                                <asp:ListItem>1200</asp:ListItem>
                                <asp:ListItem>1500</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                                <asp:ListItem>6000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:CheckBox ID="chln" CssClass="checkBox" runat="server" Text="Chalan Qty" />
                            </div>
                        </div>

                    </div>

                </div>
               
            </div>
           
            <div class="card card-fluid" style="min-height:500px;">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvMatStock_PageIndexChanging" OnRowDataBound="gvMatStock_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resource Description">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Resource Description" Width="100px"></asp:Label>

                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="" ToolTip="Export Excel"><i class="fa fa-file-excel"></i>
                                        </asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                            Width="150px">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgcUnitF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"> Total :</asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Budgeted Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbudgetqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbudgetqty" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opening Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOp" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvOpF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRecamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvRecamtF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer In">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTraninqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvTraninqtyF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transfer Out">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTranoutqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvTranoutqtyF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Damage/Lost/Sold">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDamage" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvDamageF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvTamtF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIsuamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvIsuamtF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAcSamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvAcSamtF" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Diviation" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdiviation" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diviation")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdiviation" runat="server" Width="80px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                        <asp:GridView ID="gvMatStockSpec" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvMatStock_PageIndexChanging" OnRowDataBound="gvMatStock_RowDataBound">
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resource Description">
                                    <ItemTemplate>


                                        <asp:HyperLink ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                            Width="100px">
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcspec" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOp" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRecamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer In">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTraninqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transfer Out">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTranoutqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Damage/Lost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDamage" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIsuamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAcSamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
