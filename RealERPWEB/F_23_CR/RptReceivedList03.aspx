<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptReceivedList03.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptReceivedList03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
                                    <div class="col-md-5  pading5px  ">
                                        <asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Size :"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" CssClass="inputTxt ddlPage" TabIndex="13" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn margin5px"
                                            OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-1 pull-left pading5px ">
                                    </div>





                                    <div class="col-md-3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar" Text="Please wait......"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px ">
                                        <asp:Label ID="lblAmount0" runat="server" Text="Amount:" CssClass=" smLbl_to lblTxt"></asp:Label>
                                        <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass="form-control " TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" Width="209px">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                            <asp:ListItem Value="between">Between</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:TextBox ID="txtAmountC1" runat="server" CssClass="iinputTxt inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="lblToCash" runat="server" CssClass=" smLbl_to blName lblTxt" Text="To:" Visible="false"></asp:Label>

                                        <asp:TextBox ID="txtAmountC2" runat="server" Visible="false" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        
                                    </div>
                                     <div class="col-xs-3 col-md-3 col-lg-3">
                                          </div>
                                    <div class="col-xs-1 col-md-1 col-lg-1">
                                    
                                    <a class="btn btn-primary primaryBtn"  href='<%=this.ResolveUrl("~/F_22_Sal/RptSalSummery.aspx?Type=dSaleVsColl")%>' target="_blank" style="margin: 0 0 0 5px; line-height: 18px;">NEXT</a>
                                </div>

                                </div>


                            </div>

                        </fieldset>

                    </div>

                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewallProDues" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea"  
                                                OnPageIndexChanging="dgvAccRec03_PageIndexChanging" 
                                                Width="654px" OnRowDataBound="dgvAccRec03_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                                        ForeColor="Black" Target="_blank" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                        Width="150px"></asp:HyperLink>
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
                                            <asp:TemplateField HeaderText="Car Parking & Others">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFcpaocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcpaocostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpaocost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
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
                                                        Width="75px"></asp:Label>
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
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
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
                                            <asp:TemplateField HeaderText="Dues & OverDue">
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
                                                        Width="75px"></asp:Label>
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

                                <asp:Panel ID="pnlIndPro" runat="server" Visible="False">
                                    <div class="row">


                                        <div class="col-md-12">
                                            <asp:Label runat="server" CssClass="GrpHeader" Font-Bold="True" Font-Size="12px"
                                                Width="300px">Note</asp:Label>
                                            </div>
                                            <div class="clearfix">
                                            </div>

                                         <div class="col-md-12">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered grvContentarea" ID="gvinpro" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True">
                                                <PagerSettings Position="Top" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid0" runat="server"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dues Upto Dec 2014">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cureent Due Dec 2014">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcurduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtoduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
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
                                      
                                            
                                        </div>
                                </asp:Panel>
                  

                    </asp:View>
                        </asp:MultiView>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

