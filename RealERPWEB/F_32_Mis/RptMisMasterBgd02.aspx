<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMisMasterBgd02.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptMisMasterBgd02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvColvsExp = $('#<%=this.gvColvsExp.ClientID %>');
            gvColvsExp.Scrollable();

        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="Server">

        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                            <div class="form-group">
                        <div class="col-md-3  pading5px">
                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                            <asp:TextBox ID="txtDateExpens" runat="server" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                TargetControlID="txtDateExpens"></cc1:CalendarExtender>

                            <asp:LinkButton ID="lbtnShowColvsExp" runat="server" CssClass="btn btn-primary primaryBtn"
                                OnClick="lbtnShowColvsExp_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-5 pading5px">
                            <asp:CheckBox ID="chkconsolidate" runat="server" CssClass=" btn btn-primary checkBox"
                                Text="Consolidate" />
                            <asp:CheckBox ID="chkCrore" runat="server" CssClass="btn btn-primary checkBox"
                                Text="Taka in Crore" />

                           
                        </div>

                        <div class="col-xs-1 col-md-1 col-lg-1">

                            <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=12")%>' target="_blank" style="margin: 0 0 0 5px; line-height: 18px;">NEXT</a>
                        </div>

                        <div class="clearfix"></div>
                    </div>
                    <div class="table table-responsive">
                         

                        <asp:GridView ID="gvColvsExp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText="Description">
                                    <HeaderTemplate>

                                        


                                        

                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                        Width="70px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCdataExelExp" runat="server">Export Excel</asp:HyperLink>

                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIActDesc" runat="server"
                                            
                                            


                                              
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "catmdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "catmdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'


                                            
                                       
                                            Width="180px"></asp:Label>
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
                                <asp:TemplateField HeaderText="Total Revenue <br/> A">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtosales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Sales <br/> B">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Collection <br/> C">
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
                                <asp:TemplateField HeaderText="Sales in % <br/> D=B/A">
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
                                <asp:TemplateField HeaderText="Collection in % <br/> E=C/A">
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
                                <asp:TemplateField HeaderText="Budgeted Cost <br/> F">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Cost <br/> G">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost in % <br/> H=G/F">
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
                                <asp:TemplateField HeaderText="Investment Position <br/> I=H-E">
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
                                <asp:TemplateField HeaderText="Remaining Collection <br/> From Sold <br/> J=B-C">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrecoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsalamt")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Remaining Collection <br/> From Sold & Un-sold <br/> K=A-C">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usoamt")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Remaining Cost <br/> L=F-G">
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
                                <asp:TemplateField HeaderText="Fund to be generated <br/> From Sold <br/> M=J-L">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvfsamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsamt")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Fund to be generated <br/> From Sold & Un-sold <br/> N=K-L">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvfsuamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fsuamt")).ToString("#,##0.00;-#,##0.00; ") %>'
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
                                <asp:TemplateField HeaderText="Investment  <br/> Required <br/> O=FxI<0">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFresdramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresdramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invreqamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Investment <br/> Blocked <br/>  P=FxI>0">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFrescramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrescramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invblkamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Liabilities <br/> Q">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFlibamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlibamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "libamt")).ToString("#,##0.00;-#,##0.00; ")%>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOI <br/> R">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnoiamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnoiamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan To H/O <br/> S=((C-G)+R)>0">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFltoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvltoamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltoamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan From H/O <br/> T=((C-G)+R)<0">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFlfrmamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlfrmamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfrmamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Sales <br/> U=AxH%">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbgdsaamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbgdsaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdsaamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Profit <br/> V=(A-F)/<br/>FxG+R">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "np")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Salable (SFT)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsalablearea"  runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right;" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalablearea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Sold (SFT)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFsoldarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsoldarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsold")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Unsold Sold (SFT)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFusoldarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusoldarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsold")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Area in SFT">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFconsarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconsarea" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Catagory">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcatagory" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagory"))%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Const. Progress in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconprogress" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conprogress")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completion Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcompletiondate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month Remaining">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonremain" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mremain")).ToString("#,##0;(#,##0); ") %>'
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

                        <div class=" clearfix"></div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12  pading5px">
                            <asp:Label ID="lblBankstatus" runat="server"  Visible="false"
                                Text="Note:" CssClass="smLbl_to" ></asp:Label>
                        </div>
                    </div>
                    <div class="clearfix"></div>


                    <asp:GridView ID="gvCatdesc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Catagory">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcatdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagory")) 
                                                                        %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdetcatdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) 
                                                                        %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="Black" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <asp:Panel ID="pnlNoteDe" runat="server" Visible="false">
                        <div class="form-group">
                            <div class="col-md-12  pading5px">
                                <asp:Label ID="lblColl" runat="server" CssClass=" smLbl_to"
                                    Text="A. COLLECTION % OF SALES (C/B x 100)"></asp:Label>

                                <asp:Label ID="txtColl" runat="server" CssClass="smLbl_to" Text="Collection %"></asp:Label>

                                <asp:Label ID="lblCost" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="B. NP % OF COST (V/G x 100)"></asp:Label>

                                <asp:Label ID="txtNp" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="NP %"></asp:Label>

                                <asp:Label ID="lblBgd" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="C. ACTUAL SALES % OF BUDGETED SALES (B/U x 100)"></asp:Label>

                                <asp:Label ID="txtBgd" runat="server" Font-Bold="True" CssClass="smLbl_to" Text="Bgd %"></asp:Label>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </asp:Panel>
                    <div class="clearfix"></div>



                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


