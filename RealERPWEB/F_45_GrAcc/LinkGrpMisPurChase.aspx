
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisPurChase.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMisPurChase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
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
                        </asp:Panel>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewreqAppStatus" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="gvReqStatusAp" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvReqStatusAp_PageIndexChanging" ShowFooter="True"
                                    Width="501px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <FooterTemplate>
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrfNoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDateap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnitap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQtyap" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprrateap" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvapramt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvreqapAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEntryUserap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aproved Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovdatap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App. User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprvuserap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ausrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
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
                        <asp:View ID="ViewWorkOrder" runat="server">
                            <div class=" table-responsive">
                                <asp:Panel ID="PanelDeWorkOrder" runat="server" ScrollBars="Horizontal"
                                    Width="1000px">
                                    <asp:GridView ID="gvDeWorkOrdSt" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvDeWorkOrdSt_PageIndexChanging" ShowFooter="True"
                                        Width="831px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdesc" runat="server" Font-Bold="true"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrdno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdRef" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Date ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdDat" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno2")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrfNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Suppliers Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSupDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Materials">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBrName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvditem" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" HorizontalAlign="Left" Style="text-align: right" Text="Total"
                                                        Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="35px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="35px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEnUser" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Approved">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrAppUser" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprusername")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Print">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrPrUser" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orusername")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </div>
                        </asp:View>
                        <asp:View ID="ViewPurchase" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="gvPurStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvPurStatus_PageIndexChanging"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="72px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdref0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBillNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="135px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <FooterTemplate>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecifi" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate1" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
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

                        <asp:View ID="ViewBillCOnfirmation" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="gvBillStatus" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvBillStatus_PageIndexChanging" ShowFooter="True"
                                    Width="831px" OnRowDataBound="gvBillStatus_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBillNobill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBilldate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNobill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdNobill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgReqNobill" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                                    Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNobill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdescbill" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterialsbill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="135px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnitbill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecification" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
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
                        <asp:View ID="ViewPurChasetk" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="734px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials3" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecification0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamount" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier01" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
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


                        <asp:View ID="ViewPendingstatus" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>

                                            </div>

                                            <div class="col-md-4 pading5px  asitCol4">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="LblReqno" runat="server" CssClass="lblTxt lblName" Text="MRR REF"></asp:Label>
                                                <asp:TextBox ID="txtMRFNO" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>

                                            </div>
                                        </div>

                                    </div>
                                </fieldset>
                            </div>
                            <div class=" table-responsive">
                                <asp:GridView ID="gvPenStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvPenStatus_PageIndexChanging"
                                    PageSize="15" ShowFooter="True" Width="853px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqNops" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNops" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdescps" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                    Width="140px">
                                                            
                                                            
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqdatps" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqrate" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CS Not Yet Prepared &lt;br/&gt; Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CS Not Yet Prepared &lt;br/&gt; Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFreqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CS Not Yet Prepared &lt;br/&gt;Vendor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvendorname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Approved &lt;br/&gt; Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvappdateps" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Approved &lt;br/&gt; Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvappqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Approved &lt;br/&gt; Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFappamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvappamt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Not Yet Placed &lt;br/&gt;  Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrderdateps" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Not Yet Placed  &lt;br/&gt; Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvorderqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Not Yet Placed  &lt;br/&gt; Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvForderamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvorderamt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Received &lt;br/&gt;Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMRRDateps" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Received  &lt;br/&gt; Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrrqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Yet Received &lt;br/&gt; Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmrramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrramt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
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

                    </asp:MultiView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


