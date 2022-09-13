﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptPurchaseStatusUr.aspx.cs" Inherits="RealERPWEB.F_14_Pro.LinkRptPurchaseStatusUr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        };
        function printTracking(data) {
            window.open(data, '_blank');
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
 
                                    <div class="col-md-2">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to" Visible="false">Group</asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage" Visible="False" Width="80px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Visible="false">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage" Visible="false" Style="width: 72px;">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1200</asp:ListItem>
                                            <asp:ListItem>1500</asp:ListItem>
                                            <asp:ListItem>3000</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 pading5px asitCol11">
                                        <div runat="server" id="datepart" class="col-md-3">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate" Enabled="true"></cc1:CalendarExtender>

                                            <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-4 pull-right">
                                            <asp:CheckBox ID="chkDirect" runat="server" Visible="false" Text="Petty Cash" CssClass="btn btn-primary checkBox" />

                                            <asp:Label ID="LblReqno" runat="server" CssClass="lblTxt lblName" Visible="false" Text="MRR REF"></asp:Label>
                                            <asp:TextBox ID="txtSrcMrfNo" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </fieldset>                        
                        <asp:GridView ID="gvBgdBal" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="512px">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requsition No" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRF No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvReqDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Req Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFareqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvareqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Process">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvproqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFprogqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Place">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdrQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Mrr. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmrrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Adjust">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdradjQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oadjqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordradjqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                        <asp:Panel ID="Panelbgdbal" runat="server" Visible="False">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Label ID="lbltxtConfirmation" runat="server" CssClass="btn btn-success primaryBtn" Text="Confirmation:"
                                        Width="120px"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbltxtOpenig1" runat="server" CssClass="lblName lblTxt" Text="Budgeted Qty"></asp:Label>
                                <asp:Label ID="lblvalBudget" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Label ID="lbltxtSuppinchain" runat="server" CssClass="btn btn-success primaryBtn" Text="Supply/ In-process"
                                        Width="120px"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>


                            <div class="form-group">
                                <asp:Label ID="lbltxtOpenig" runat="server" CssClass="lblName lblTxt" Text="Opening"></asp:Label>
                                <asp:Label ID="lblvalOpenig" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbltxtdqty" runat="server" CssClass="lblName lblTxt" Text="Direct"></asp:Label>
                                <asp:Label ID="lbltxtvaldqty" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbltxtRequisition" runat="server" CssClass="lblName lblTxt" Text="Requisition"></asp:Label>
                                <asp:Label ID="lblvalRequisition" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lbltxttransfer" runat="server" CssClass="lblName lblTxt" Text="Transfer"></asp:Label>
                                <asp:Label ID="lblvaltrans" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbltxtOpenig3" runat="server" CssClass="lblName lblTxt" Text="Total Qty"></asp:Label>
                                <asp:Label ID="lblvalTotalSupp" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lbltxtOpenig2" runat="server" CssClass="lblName lblTxt" Text="Balance"></asp:Label>
                                <asp:Label ID="lblvalBalance" runat="server" CssClass="smLbl_to"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <%-- <table style="width: 100%;">
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                               
                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtSuppinchain" runat="server" Font-Bold="True" Font-Size="14px"
                                                    ForeColor="Yellow" Style="text-align: Left; text-decoration: underline;" Text=""
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Opening" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: left" Text="Requisition" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbltxtRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxttransfer" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Transfer" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvaltrans" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69"></td>
                                            <td colspan="2">
                                                <div style="width: 230px; border-bottom: 1px solid yellow;">
                                                </div>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Total Qty" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalTotalSupp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style68">
                                                <asp:Label ID="lbltxtOpenig2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: left" Text="Balance" Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblvalBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style69">&nbsp;
                                            </td>
                                            <td class="style67" colspan="2">
                                                <div style="width: 230px; border-top: 1px solid yellow;">
                                                </div>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>--%>
                        </asp:Panel>
                    </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
