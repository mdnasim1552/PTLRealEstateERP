<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjFeasibility04.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptPrjFeasibility04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            var gv1 = $('#<%=this.gvgpnp.ClientID %>');
            gv1.Scrollable();

        };
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="col-md-3 pading5px asitCol2">
                                        <asp:Label ID="lblDate" runat="server" CssClass=" lblName lblTxt" Text=" Date:"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="3">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkconsolidate" runat="server" Text="Consolidate" CssClass="chkBoxControl margin5px" />
                                    </div>
                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGroup" runat="server" CssClass="lblName lblTxt" Visible="False"
                                            Text="Group :"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChanged"
                                            Visible="False" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>


                                </div>
                  
                    </fieldset>
                        </asp:Panel>
                </div>
                <div class="row">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewSoldUSold" runat="server">
                            <div class="table table-responsive">
                            <asp:GridView ID="gvSoldUnSold" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProject" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Unit Size(Seleable)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtusize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tusize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtusize" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking(Seleable)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtparking" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtparking" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate(Seleable)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount(Seleable)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtuamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtuamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Unit Size(Sales)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsusize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "susize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFsusize" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking(Sales)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsparking" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFsparking" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount(Sales)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsuamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFSuamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Unit Size(UnSold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunusize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unusize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFunusize" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking(Unsold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunparking" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unpqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFunparking" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate(Unsold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unrate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount(Unsold)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunuamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFunuamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                        <asp:View ID="ViewAllProgpnp" runat="server">
                            <div class="table table-responsive">
                            <asp:GridView ID="gvgpnp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="651px" OnRowDataBound="gvgpnp_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjectgn" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")   %>'
                                                Width="180px">
                                                                         
                                                                         
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
                                            <asp:Label ID="lblgvFconarea" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFsaleamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFconsct" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
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
                                            <asp:Label ID="lblgvFtconabit" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFlcost" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFcoffund" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
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
                                            <asp:Label ID="lblgvFadcost" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFtprcost" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFgp" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFovrhead" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RF & Provision">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrfund" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rfapcost")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFrfund" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFtopcost" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFtocost" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                            <asp:Label ID="lblgvFnp" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
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
                                            <asp:Label ID="lblgvFperonsl" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
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
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
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
                                                 ForeColor="#000"  Style="text-align: right"></asp:Label>
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
                               
                        </asp:View>
                        <asp:View ID="ViewPriceList" runat="server">
                              <div class="table table-responsive">
                            <asp:GridView ID="gvFeaPriceList02" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFeaPriceList02_RowDataBound"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="785px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvProdescpr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvitemdescpr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lglocpr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "loc")) %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lgTypepr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ptype")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Handover Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgHoverpr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hdate")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Car Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcparkingpr" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Utility">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvutilitypr" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Beautification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbeautification" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Of Sale">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvperontorev" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbamtpr" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Minimum Sales Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRatepr" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                                  
                        </asp:View>
                        <asp:View ID="ViewFeaCost" runat="server">
                            <div class="table table-responsive">
                            <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="792px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpproject" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mpactdesc"))%>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flat No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflat" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stored">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvstored" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stored"))%>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Face">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvface" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "face"))%>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvusize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFusize" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvfloor" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flr"))%>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Car Parking">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcparking" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cparking")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvpurrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purrate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpuramt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpuramt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Car Parking">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcpamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcpamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utility">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvutility" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFutility" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BD">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvbdamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbdamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commision">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcommision" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcommision" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvothamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFothamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtotalaamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalaamt" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000" 
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
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
            </div>

            <table style="width: 100%;">
                <%--<tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style70">
                                        &nbsp;
                                    </td>
                                    <td class="style71">
                                        <asp:Label ID="lblDate" runat="server" CssClass="label2" Text=" Date:" Width="29px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        <asp:TextBox ID="txtDate" runat="server" BorderStyle="None" TabIndex="4" Width="97px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style64">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="16px"
                                            OnClick="lnkbtnSerOk_Click" Style="color: #FFFFFF; text-align: center;" TabIndex="3"
                                            Width="26px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style65">
                                        &nbsp;
                                        <asp:CheckBox ID="chkconsolidate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Checked="True" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Text="Consolidate" 
                                            Visible="False" Width="90px" />
                                    </td>
                                    <td class="style66">
                                        &nbsp;
                                    </td>
                                    <td class="style69">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style70">
                                        &nbsp;
                                    </td>
                                    <td class="style71">
                                        <asp:Label ID="lblGroup" runat="server" CssClass="style18" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="Group:" Visible="False" Width="30px"></asp:Label>
                                    </td>
                                    <td class="style57">
                                        
                                    </td>
                                    <td class="style64">
                                        &nbsp;
                                    </td>
                                    <td class="style65">
                                        &nbsp;
                                    </td>
                                    <td class="style66">
                                        &nbsp;
                                    </td>
                                    <td class="style69">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <tr>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
