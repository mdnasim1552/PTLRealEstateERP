<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccIncome.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptAccIncome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvIncomeMon.ClientID %>');

            gv.Scrollable();



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
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblFdate" runat="server" CssClass=" smLbl_to"> From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server"
                                            Enabled="True" Format="dd-MM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lblTdate" runat="server" CssClass=" smLbl_to"> To </asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server"
                                            Enabled="True" Format="dd-MM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblPage" runat="server" Text="Page Size:" CssClass="lblName lblTxt "></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
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
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"
                                            TabIndex="5">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View runat="Server" ID="ViewIncomeMonthly">
                                    <asp:GridView ID="gvIncomeMon" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvIncomeMon_RowDataBound"
                                        ShowFooter="True" Width="616px"  CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCode" runat="server" __designer:wfdid="w38" CssClass="GridLebelL"
                                                        Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))  %>'
                                                        Width="90px" Style="font-size: 11px; color: Black;"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDescpaysum" runat="server" __designer:wfdid="w38" CssClass="GridLebelL"
                                                        Font-Size="12px" Font-Underline="False" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))  %>'
                                                        Width="280px" Style="font-size: 11px; color: Black;"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoamtmpaysum" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoamtmpaysum" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt1">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt8">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt9">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt11">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="amt12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamtmpaysum12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamtmpaysum12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
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
                                </asp:View>
                            </asp:MultiView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
