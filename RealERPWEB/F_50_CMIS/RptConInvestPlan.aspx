<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptConInvestPlan.aspx.cs" Inherits="RealERPWEB.F_50_CMIS.RptConInvestPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvInvest = $('#<%=this.gvInvest.ClientID %>');
            var grvPrjStatus = $('#<%=this.grvPrjStatus.ClientID %>');
            var gvMonPorStatus = $('#<%=this.gvMonPorStatus.ClientID %>');
            gvInvest.Scrollable();
            grvPrjStatus.Scrollable();
            gvMonPorStatus.Scrollable();

        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="form-group">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal ">
                                <div class="col-md-12">
                                    <div class="col-md-7">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to col-md-4" Text="Date:"></asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox col-md-8"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblToDate" runat="server" CssClass=" smLbl_to col-md-4" Text="To:"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox col-md-8"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">OK</asp:LinkButton>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkconsolidate" runat="server" BackColor="ForestGreen" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Text="Consolidate" Visible="true" Width="90px" />

                                            <asp:CheckBox ID="chkCash" runat="server" BackColor="ForestGreen" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Text=" Cash Basis" Visible="False" Width="90px" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblTkInCrore" runat="server" BackColor="Green" Font-Bold="True" Font-Size="12px" ForeColor="White" Text="Taka in Crore" Height="100%" Width="75px">
                                            </asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-5">
                                        <div class="pading5px asitCol5">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                                <ProgressTemplate>
                                                    <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Yellow" Style="text-align: center" Text="Please wait . . . . . . ." Width="218px">
                                                    </asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="table-responsive row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvInvest" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInvest_RowDataBound"
                                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Style="text-align: left" Width="696px">
                                                <%--   <RowStyle BackColor="#D2FFF7" Font-Size="11px" />--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField FooterText="Total" HeaderText="Description">
                                                        <HeaderTemplate>
                                                            <table style="width: 47%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description" Width="80px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                                            Width="90px">Export Exel</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvIActDesc" runat="server"
                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim()+ "</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")
                                                                    %>'
                                                                Width="180px" ForeColor="Black">
                                                                    
                                                                    
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Loc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjloc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Land Area">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lanarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lanarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nature of Land">
                                                        <ItemTemplate>
                                                            <asp:Label ID="natland" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "natland")) %>'
                                                                Width="45px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Revenue &lt;br/&gt; A">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtosales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="45px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFToSalVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="45px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales &lt;br/&gt; B">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFSaleAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Collection &lt;br/&gt; C">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCollAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales in % &lt;br/&gt; D=B/A">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvperonsale" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontosal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFperonsale" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Collection in % &lt;br/&gt; E=C/A">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvperontocol" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontocol")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFperontocol" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted Cost &lt;br/&gt; F">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBgdAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Cost &lt;br/&gt; G">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvExp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost in % &lt;br/&gt; H=G/F">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvperonPro" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronpgres")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFperonPro" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Investment Position &lt;br/&gt; I=H-E">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvipamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ipamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFipamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Collection &lt;br/&gt; From Sold &lt;br/&gt; J=B-C">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvrecoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFreCollAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Collection &lt;br/&gt; From Sold &amp; Un-sold &lt;br/&gt; K=A-C">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvusoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFusoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Cost &lt;br/&gt; L=F-G">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFRbgdAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvrebgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rbgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fund to be generated &lt;br/&gt; From Sold &lt;br/&gt; M=J-L">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvfsamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFfsamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fund to be generated &lt;br/&gt; From Sold &amp; Un-sold &lt;br/&gt; N=K-L">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvfsuamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFfsuamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Investment  &lt;br/&gt; Required &lt;br/&gt; O=FxI&lt;0">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFresdramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvresdramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invreqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Investment &lt;br/&gt; Blocked &lt;br/&gt;  P=FxI&gt;0">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFrescramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvrescramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invblkamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Liabilities &lt;br/&gt; Q">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlibamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvlibamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "libamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NOI &lt;br/&gt; R">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFnoiamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvnoiamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan To H/O &lt;br/&gt; S=((C-G)+R)&gt;0">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFltoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvltoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan From H/O &lt;br/&gt; T=((C-G)+R)&lt;0">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFlfrmamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvlfrmamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfrmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted Sales &lt;br/&gt; U=AxH%">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFbgdsaamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbgdsaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdsaamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net Profit &lt;br/&gt; V=(A-F)/FxG+R">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFnp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvnp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "np")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>

                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Panel ID="PanelNote" runat="server" BorderColor="Maroon" BorderStyle="Solid"
                                                BorderWidth="1px" Visible="False">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style114">
                                                            <asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Note:"
                                                                Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style115">&nbsp;
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
                                                        <td class="style114">
                                                            <asp:Label ID="lblColl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Text="A. COLLECTION % OF SALES (C/B x 100)"
                                                                Width="320px"></asp:Label>
                                                        </td>
                                                        <td class="style115">
                                                            <asp:Label ID="txtColl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Style="color: Black; text-align: right;" Text="Collection %"
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
                                                        <td class="style114">
                                                            <asp:Label ID="lblCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Style="color: Black" Text="B. NP % OF COST (V/G x 100)" Width="320px"></asp:Label>
                                                        </td>
                                                        <td class="style115">
                                                            <asp:Label ID="txtNp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Style="color: Black; text-align: right;" Text="NP %" Width="120px"></asp:Label>
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
                                                        <td class="style114">
                                                            <asp:Label ID="lblBgd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Style="color: Black" Text="C. ACTUAL SALES % OF BUDGETED SALES (B/U x 100)"
                                                                Width="320px"></asp:Label>
                                                        </td>
                                                        <td class="style115">
                                                            <asp:Label ID="txtBgd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                                Height="16px" Style="color: Black; text-align: right;" Text="Bgd %" Width="120px"></asp:Label>
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
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
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
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewProStatus" runat="server">
                                <asp:GridView ID="grvPrjStatus" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvPrjStatus_RowDataBound"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="margin-right: 0px">
                                    <%-- <RowStyle BackColor="#D2FFF7" Font-Size="11px" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
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
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcomnamep" runat="server" Font-Bold="false" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) + "</B>" %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Bold="false" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim()%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Location" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                                Width="90px">Export Exel</asp:HyperLink>
                                                        </td>
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
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Sales Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px">
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
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Construction + Admin + Selling Exp.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFExpAmtp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExpAmtp" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "texpamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Project Advance">
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
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Liabilities">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFLibAmtp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLibAmtp" runat="server" Font-Size="11PX" Style="text-align: right"
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
                                                <asp:Label ID="lgvFLtoamtp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLtoamtp" runat="server" Font-Size="11PX" Style="text-align: right"
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
                            </asp:View>
                            <asp:View ID="ViewMonProStatus" runat="server">


                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvMonPorStatus" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMonPorStatus_RowDataBound"
                                                CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="616px">
                                                <%-- <RowStyle BackColor="#D2FFF7" Font-Size="11px" />--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialno1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                                        <HeaderTemplate>
                                                            <table style="width: 150px">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                                            Width="70px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                                            Width="80px">Export Exel</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                                Width="300px">
                                                           
                                                           
                                                           
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R1">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFR1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                            <asp:Label ID="lgvFR14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="65px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Total Cost">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
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
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">

                                            <asp:Panel ID="PnlNote" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px" Visible="False">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Width="1150px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblConsCost" runat="server" Width="130px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblvalConsCost" runat="server" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style136">&nbsp;</td>
                                                        <td class="style132">&nbsp;</td>
                                                        <td class="style133">&nbsp;</td>
                                                        <td class="style134">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style125"></td>
                                                        <td class="style125">
                                                            <asp:Label ID="lblNonConsCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Text="Non Construction Cost:" Width="130px"></asp:Label>
                                                        </td>
                                                        <td class="style125">
                                                            <asp:Label ID="lblnonvalConsCost" runat="server" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="Black" Style="text-align: right;" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style137"></td>
                                                        <td class="style126"></td>
                                                        <td class="style127">&nbsp;</td>
                                                        <td class="style135">&nbsp;</td>
                                                        <td class="style125"></td>
                                                        <td class="style125"></td>
                                                        <td class="style125"></td>
                                                        <td class="style125"></td>
                                                        <td class="style125"></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>


                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>


            <asp:Panel ID="PnlRmrks" runat="server" BorderColor="Maroon" BorderStyle="Solid"
                BorderWidth="1px" Visible="False">
            </asp:Panel>
            </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

