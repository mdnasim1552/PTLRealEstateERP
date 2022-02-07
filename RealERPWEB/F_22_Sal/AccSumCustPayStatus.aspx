<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSumCustPayStatus.aspx.cs" Inherits="RealERPWEB.F_22_Sal.AccSumCustPayStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
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
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" ReadOnly="true" Style="padding: 10px; color: maroon; font-size: 14px;" Font-Bold="True"></asp:TextBox>
                                        <%--  <div class="col-md-12 pading5px">

                                            <asp:Label ID="txtCustName" runat="server" CssClass="lblTxt lblName " Text="Customer : "></asp:Label>                                            
                                        </div> --%>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>

                    <div class="row">
                        <asp:TextBox ID="txFdate" runat="server" CssClass="inputtextbox " AutoPostBack="true" Visible="false"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1_txFdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>

                        <div class="table table-responsive">

                            <asp:GridView ID="gvCustLedger" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCustLedger_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <HeaderTemplate>
                                            <table style="width: 105px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Particulars" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticlr" runat="server" ForeColor="Black" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schdule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPmntDueDate" runat="server" ForeColor="Black" Height="16px"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy"))%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>


                                        <FooterTemplate>
                                            <asp:Label ID="lbltotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Width="100px">Total</asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shcdule Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueAmnt" runat="server" ForeColor="Black" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFscamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MR No(System)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmrNo" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MR No(Manual)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRNoM" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRcvDate" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recdate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recdate")).ToString("dd-MMM-yyyy"))%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash/Chq No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCash" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).ToString("dd-MMM-yyyy"))%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBnkName" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch">
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" Text="Total Rcv" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFBalamt" Text="Bal. Amount" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblBrnch" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRcvAmnt" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:Label ID="lblFrcvamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFBalTamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </FooterTemplate>

                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank St. Clearing Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBnkStClrDte" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndate")).ToString("dd-MMM-yyyy"))%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uncleared">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnClr" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bunamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="As On Dues" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblasondues" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asondues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Dues">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDues" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clearing Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClrBank" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <h4>Account Collection</h4>

                            <asp:Panel ID="pnlhidedataLink" Visible="false" runat="server">

                                <asp:GridView ID="gvAccCash" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCustLedger_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <HeaderTemplate>
                                                <table style="width: 105px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Particulars" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>

                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticlr" runat="server" ForeColor="Black" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Schdule Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPmntDueDate" runat="server" ForeColor="Black" Height="16px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Width="100px">Total</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shcdule Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDueAmnt" runat="server" ForeColor="Black" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFscamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR No(System)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmrNo" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR No(Manual)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMRNoM" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRcvDate" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recdate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recdate")).ToString("dd-MMM-yyyy"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cash/Chq No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCash" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).ToString("dd-MMM-yyyy"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBnkName" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" Text="Total Rcv" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFBalamt" Text="Bal. Amount" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblBrnchACC" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRcvAmntACC" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="lblFrcvamtACC" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFBalTamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank St. Clearing Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBnkStClrDte" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndate")).Year == 1900 ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndate")).ToString("dd-MMM-yyyy"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uncleared">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnClr" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bunamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="As On Dues" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblasondues" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asondues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Dues">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDues" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Clearing Bank">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClrBank" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:Panel>

                            <asp:Panel ID="pnlhideCasSales" runat="server" Visible="false">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvCustLedger2" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Particulars">
                                                <HeaderTemplate>
                                                    <table style="width: 105px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Particulars" Width="80px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParticlr2" runat="server" ForeColor="Black" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schdule Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPmntDueDate2" runat="server" ForeColor="Black" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate"))%>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>


                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Width="100px">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shcdule Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDueAmnt2" runat="server" ForeColor="Black" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>


                                                    <asp:Label ID="lblFscamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR No(System)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrNo2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR No(Manual)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRNoM2" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRcvDate2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "recdate"))
                                                %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash/Chq No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCash2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paiddate"))%>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBnkName2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch">
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" Text="Total Rcv" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFBalamt2" Text="Bal. Amount" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrnch2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRcvAmnt2" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>

                                                                <asp:Label ID="lblFrcvamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFBalTamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </FooterTemplate>

                                                <FooterStyle HorizontalAlign="right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank St. Clearing Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBnkStClrDte2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndate"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uncleared">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnClr" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bunamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dues">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDues2" runat="server" ForeColor="Black" Style="text-align: right" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Clearing Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClrBank2" runat="server" ForeColor="Black" Style="text-align: left" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bname")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>


                        </div>

                    </div>
                    <div class="row">

                        <h4>Apartment Summary</h4>
                         
                        <div class="col-md-3">
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default">Apartment Cost:</button>
                                <button type="button" id="TotalbtnAppCost" runat="server" class="btn btn-default">0.00</button>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default">Recived:</button>
                                <button type="button"  id="TotalbtnAppRecived" runat="server" class="btn btn-default">0.00</button>

                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="btn-group" role="group">
                                <button type="button" class="btn btn-default">Balance:</button>
                                <button type="button" id="TotalbtnAppBalance" runat="server" class="btn btn-default">0.00</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
