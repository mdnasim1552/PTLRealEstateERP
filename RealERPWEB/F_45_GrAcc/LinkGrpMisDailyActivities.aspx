
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisDailyActivities.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMisDailyActivities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            var gvFeaAllPro = $('#<%=this.gvFeaAllPro.ClientID %>');
            var gvgpnp = $('#<%=this.gvgpnp.ClientID %>');
            var grvPrjStatus = $('#<%=this.grvPrjStatus.ClientID %>');
            gvFeaAllPro.Scrollable();
            gvgpnp.Scrollable();
            grvPrjStatus.Scrollable();

        }
    </script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="lblDateRange" runat="server" CssClass="lblTxt lblName" Text="Date: "></asp:Label>

                                <asp:Label ID="lblAsDate" runat="server" CssClass="smLbl_to" Text="A. Sales"></asp:Label>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    TabIndex="2" CssClass=" ddlPage">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        </div>

                    </div>
                </fieldset>

            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewDetailsCollection" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvCollDet" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvCollDet_PageIndexChanging" OnRowDataBound="gvCollDet_RowDataBound"
                            ShowFooter="True" Width="1052px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrno" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "mrno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")).Trim() : "")
                                                                         
                                                                    %>'
                                            Width="150px"> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name Of Client">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px">Total:</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px">Net Total:</asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcustname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chq. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBaName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBranch" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Amount">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"> </asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcashamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Amount">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="70px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewRecAPay" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvrecandpay" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvrecandpay_RowDataBound"
                            ShowFooter="True" Width="973px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Total Receipts:" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Net Balance" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Head Accounts Head"
                                                        Width="180px"></asp:Label></td>
                                                <td class="style60">&nbsp;&nbsp; </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnRcvPayCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                        Width="90px">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDesc" runat="server" Font-Underline="False" OnClick="btnRecDesc_Click"
                                            Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblgvFrecpam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:HyperLink ID="lgvFNetBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Target="_blank" Width="100px"></asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDesc" runat="server" Font-Underline="False" OnClick="btnPayDesc_Click"
                                            Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Total Payments:" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFpayam1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <asp:Panel ID="PanelNote" runat="server" Visible="False">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblBankstatus" runat="server" CssClass="lblTxt lblName" Text="Bank Status: "></asp:Label>


                                    </div>


                                </div>

                            </div>
                        </fieldset>

                        <asp:GridView ID="gvbankbal" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbankbal_RowDataBound" ShowFooter="True" Width="258px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>'
                                            Width="280px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Change">
                                    <ItemTemplate>
                                        <asp:Label ID="lgbalambb" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclosambb" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </asp:Panel>
                </asp:View>
                <asp:View ID="ViewBankPosition" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" OnRowDataBound="gvBankPosition_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcodebank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Description of Accounts">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58" style="width: auto">
                                                    <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Description of Accounts"></asp:Label></td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnbnkpdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Black">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescbank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="400px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Opening Liabilities" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnliabilities" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Deposit" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDramtbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Withdrawn" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramtbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Closing Balance" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclobalbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Closing Liabilities" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcloliabilities" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Bank Limit" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbankLim" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Bank Available Balance" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbankBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewSalesStock" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvMMPlanVsAch" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="662px" OnRowDataBound="gvMMPlanVsAch_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                <asp:TemplateField HeaderText="Pactcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactcode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc1" runat="server" Font-Bold="false" Font-Size="11PX" Font-Underline="false"
                                            ForeColor="Black" Style="text-align: left" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="300px">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                                         
                                                                  
                                                                
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Master Plan  (Upto Date)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmasterplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmasPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monthly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Mas. Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMasPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acmasplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Monthly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMunthPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acmonplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewDayStarandAchievement" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSalVsColl" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalVsColl_RowDataBound"
                            ShowFooter="True" Width="531px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptcode" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Team">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mdeptname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length > 0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mdeptname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="180px">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                                     
                                                                     
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Sale Target &lt;br&gt; Monthly">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonsalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msaleamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Actual  &lt;br&gt; Monthly ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvuatsalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uasaleamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achieved in  &lt;br&gt; % ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperontsale" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontsale")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpmonsalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pmonsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Target  &lt;br&gt; Today's">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtsalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Sales Actual  &lt;br&gt; Today's">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatsaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tasaleamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Collection Target  &lt;br&gt; Monthly">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmoncollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mcollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection Actual &lt;br&gt; Monthly ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvuatcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uacollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achieved in  &lt;br&gt; % ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperontcoll" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontcoll")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection Target  &lt;br&gt; Today's">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Collection Actual  &lt;br&gt; Today's  ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tacollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Collection">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpmoncollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pmoncollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewSoldUnsold" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="dgvAccRec03_PageIndexChanging" OnRowDataBound="dgvAccRec03_RowDataBound"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="654px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                            ForeColor="Black" Style="text-align: left" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                            Width="250px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value Of Stock">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtstkamal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtstkamal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tstkam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size(Av.)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFususizeal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ususize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Amt.(Av.)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFusuamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusuamtal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpercental" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat Size">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFusizeal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Avg. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrateal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apartment Price">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFaptcostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvaptcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking &amp; Others">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcpaocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcpaocostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpaocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocsotal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Encash">
                                    <FooterTemplate>
                                        <asp:Label ID="lgFEncashal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEncashal" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Returned">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtretamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtretamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Today's">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtframtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtframtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post Dated">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtpdamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtpdamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoreceivedal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotreceivedal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFatoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatoduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues Upto Dec">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotalduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues &amp; OverDue">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Booking">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpbduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Installment">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpinsduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Booking ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCbookingal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Installment ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCinstallmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Current Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCInstalmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCoCInstalmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFvbaamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvbaamtal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vbamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delay Charge">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdelchargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Cheque">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdischargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdischargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnettoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnettoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewIssueVsCollection" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvarecandpay" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcodeac" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Total Receipts:" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Net Balance" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="Head Accounts Head"
                                                        Width="180px"></asp:Label></td>
                                                <td class="style60">&nbsp;&nbsp; </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnacRcvPayCdataExel" runat="server" BackColor="#000066" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                        Width="90px">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescac" runat="server" Font-Bold="True" Font-Underline="False"
                                            OnClick="btnRecDesc_Click" Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamac" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblgvFrecpamac" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFNetBalanceac" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycodeac" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescac" runat="server" Font-Bold="True" Font-Underline="False"
                                            OnClick="btnPayDesc_Click" Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Text="Total Payments:" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamac" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFpayamac" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="100px"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewPDCIssueSt" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvgrpchqissued" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvgrpchqissued_RowDataBound"
                            PageSize="100" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounumgp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvissuenumgp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVDategp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgactdescgp" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Description">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFGrandTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Text="Grand Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresdescgp" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))  %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <HeaderTemplate>
                                        <table style="width: 220px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Bank Name" Width="100px"></asp:Label></td>
                                                <td class="style60">
                                                    <asp:HyperLink ID="hlbtnbtbCdataExelgp" runat="server" BackColor="#000066" BorderColor="White"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                        Width="90px">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbanknamegp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim() %>'
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchnono1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchdat1" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvParName1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewFeasibility" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvFeaAllPro" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFeaAllPro_RowDataBound"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfoCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Info Desc" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfodesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDescfea" runat="server" Style="font-size: 12px; color: Black; text-decoration: none;"
                                            Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                            Width="300px">&#160; 
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;  
                                                                    
                                                                    
                                                                    
                                                                    
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Text="Grand Total:  " Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Revenue">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvoRev" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orevamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvoFRevenue" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="logvCost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ocostamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="logvoFCost" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Margin">
                                    <ItemTemplate>
                                        <asp:Label ID="logvmargin" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "omargin")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="logvoFmargin" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% on Orginal&lt;br /&gt; Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperorCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perocost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Revenue">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRev" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRevenue" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCost" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Margin">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvProfit" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                            Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prolos")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmargin" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% on&lt;br /&gt;Revised&lt;br /&gt; Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Changed %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerRev" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "change")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewGPNP" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvgpnp" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvgpnp_RowDataBound"
                            ShowFooter="True" Width="651px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjectgn" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")   %>'
                                            Width="180px">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                                                         
                                                                         
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Const. Area">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconarea" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFconarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsaleamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFsaleamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Const. Cost(a)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconsct" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conscost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFconsct" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Interest Of Const.(b)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbinterest" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "binterest")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbinterest" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total(c=a+b)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtconabin" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tconabin")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtconabit" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Land Cost(d)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "landcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFlcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Of Fund(e)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcoffund" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfundamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcoffund" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total(f=d+e)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtocfaland" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcfaland")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtocfaland" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Cost(g)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvadcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFadcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Product(a+b+d+e+g)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtprcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tprcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtprcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgp" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gprofit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFgp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Over Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvovrhead" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovrcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFovrhead" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RF &amp; Provision">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrfund" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rfapcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrfund" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtopcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtopcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtocost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtocost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnp" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nprofit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP % On Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvperoncost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pgponct")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFperoncost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP % On  Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvperonsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pgponsl")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFperonsl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP % On Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnpperoncost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnponct")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnpperoncost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP % On Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnpperonsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnponsl")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnpperonsl" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewProjectst" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="grvPrjStatus" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvPrjStatus_RowDataBound"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActcode" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrpdesc" runat="server" Font-Bold="false" Font-Size="11PX" Font-Underline="false"
                                            ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrpProjectdesc" runat="server" Font-Bold="false" Font-Size="11PX"
                                            Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>" %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDescps" runat="server" Font-Bold="false" Font-Size="11PX"
                                            Font-Underline="false" ForeColor="Black" Style="text-align: left" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim()%>'
                                            Width="150px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false" Font-Underline="false" ForeColor="Black"
                                            style="text-align: left" 
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim(): "")+"</B>"  + 
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")%>'
                                            Width="250px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                               </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Location">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label30" runat="server" Font-Bold="True" Text="Location" Width="80px"></asp:Label></td>
                                                <td class="style60">&nbsp;&nbsp; </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                        Width="90px">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlocation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjloc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Land Area(Katha)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLarea" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lanarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total: " HeaderText="Nature of Land">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNLand" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "natland")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Sales Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                        </asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="This Month">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTmonSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTmonSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msalamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received against Sales">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTReSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTReSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Non-Operating Income">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFNOI" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNOI" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRecamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRecamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance (Sale Value-Received against Sales)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBRecSalamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBRecSalamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balsalrec")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction + Admin + Selling Exp.">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExpAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "texpamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPAdvAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPAdvAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpadvamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Land Cost(Non-Refundable)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFLCNFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLCNFAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tlcamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOvmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOvmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tovamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Interest">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFIAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbankinamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Actual Expenses">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtExp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtExp" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tactamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Liabilities">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFLibAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLibAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tliamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan from Head Office">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFLframt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLframt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfrmhoff")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan to Head Office">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFLtoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLtoamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltohoff")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refundable Loan">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRLamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRLamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "treloanamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnodup" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewMonProStatus" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvMonPorStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="616px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                    <HeaderTemplate>
                                        <table style="width: 150px">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                        Width="70px"></asp:Label></td>
                                                <td class="style60">&nbsp;&nbsp; </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                        Width="80px">Export Exel</asp:HyperLink></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R1">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R2">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R3">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R4">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R5">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R6">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R7">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R8">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R9">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R10">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R11">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R12">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R13">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R14">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r14")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="65px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Cost">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Collection">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCollection" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoCollection" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Position">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetposition" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetposition" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescdup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvserial" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewPDCSummary" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvpdc" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpdc_RowDataBound"
                            ShowFooter="True" Width="399px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDescpaysum" runat="server" Font-Size="12px" Font-Underline="False"
                                            Style="font-size: 11px; color: Black;" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pgrpdesc")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "pgrpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim() : "")%>'
                                            Width="300px"> 
                                           
                                                   
                                                   
                                                   
                                                   


                                            
                                            
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        


&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                        
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 
                                           
                                                   
                                                   
                                                   
                                                   


                                            
                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtpdc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due to Pay">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDC">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpdc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdcam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
                </asp:View>
                <asp:View ID="ViewDaywiseSales" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDayWSale" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvDayWSale_PageIndexChanging" OnRowDataBound="gvDayWSale_RowDataBound"
                            ShowFooter="True" Width="770px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDcuname" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFditem" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="Left" Style="text-align: right" Text="Total" Width="150px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgUnit" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUSize" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUpsft" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Team">
                                    <ItemTemplate>
                                        <asp:Label ID="lgDCper" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDTAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDSAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDSchdate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDDisAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lgDvDisPer" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                </asp:View>
                <asp:View ID="ViewCollSummary" runat="server">
                    <div class="table-responsive">
                        <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvrcoll_RowDataBound" ShowFooter="True" Width="531px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidre" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptcoderc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartmentrc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Collection">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocollectionsum" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Clearance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreconamt" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Deposited">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqdep" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Hand(Returned Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhrchq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Hand(Fresh Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhfchq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Hand(Post Dated Cheque)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinhpchq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Replacement Cheque">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrepchq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repchq")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Collection(Without Replacement)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvncollamt" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ncollamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
            </asp:MultiView>

        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

