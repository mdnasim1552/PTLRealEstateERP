<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpMisDailyActiviteis.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.RptGrpMisDailyActiviteis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Date "></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">OK</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-3 padingpx asitCol3">
                                        <div class="msgHandSt">


                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="30">
                                                <ProgressTemplate>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                        Text="Please Wait.........."></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                        </div>

                                    </div>


                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="form-group">

                            <div class="col-md-3 padingpx asitCol3">
                                <asp:Label ID="Label12" runat="server" CssClass="smLbl_to" Text="Company Name:"></asp:Label>
                                <asp:CheckBox ID="chkall" AutoPostBack="true" runat="server" OnCheckedChanged="chkall_CheckedChanged" Text="Check All" />
                            </div>
                            <div class="col-md-3 padingpx asitCol3">
                            </div>
                            <div class="col-md-3 padingpx asitCol3">
                                <asp:CheckBox ID="chkConsolidate" runat="server" OnCheckedChanged="chkConsolidate_CheckedChanged" Text="With Consolidate" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-group">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-11">

                                    <asp:CheckBoxList ID="cblCompany" runat="server" CellPadding="2" CellSpacing="0" CssClass="StyleCheckBoxList"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" Height="12px" RepeatColumns="4"
                                        RepeatDirection="Horizontal" Width="1000px" TextAlign="Right">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                        <asp:ListItem>dd</asp:ListItem>
                                        <asp:ListItem>ee</asp:ListItem>
                                        <asp:ListItem>ff</asp:ListItem>
                                        <asp:ListItem>gg</asp:ListItem>
                                        <asp:ListItem>hh</asp:ListItem>
                                        <asp:ListItem>mm</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblSales" runat="server" CssClass="smLbl_to" Text="A. Sales" Visible="False"></asp:Label>
                    </fieldset>


                    <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDayWSale_RowDataBound"
                        ShowFooter="True" Width="267px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lkgvcomnamesale" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="100px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcapacityotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmasbgdotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break-even">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbepotvachsal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monthly Target">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvtsaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monthly Target As on Today">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtatosaleamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosaleamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Sales">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvDSAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Achieved in %">
                                <ItemTemplate>
                                    <asp:Label ID="lgvperotsal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotsal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Graph">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvgraph" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblCollectionStatus" runat="server" CssClass="smLbl_to" Text="B. Collection Status" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvrcoll_RowDataBound"
                        ShowFooter="True" Width="267px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdeptcoderc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lkgvCompanyrc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="100px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcapacityotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmasbgdotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break-even">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbepotvachcl" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monthly Target">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvtcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Monthly Target As on Today">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtatocollamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tastcollamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Achievement">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvaccollAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Achieved in %">
                                <ItemTemplate>
                                    <asp:Label ID="lgvperotcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotcoll")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Graph">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvgraphcoll" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblChequeInHand" runat="server" CssClass="smLbl_to" Text="C. Cheque In Hand" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvchequeinhand" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="341px" OnRowDataBound="gvchequeinhand_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCompanycih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px"> 
                                            
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Hand(Returned Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhrchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Hand(Fresh Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhfchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Hand(Post Dated Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhpchqcih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvamtcin" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                        ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqinhand")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblRecAPayEncash" runat="server" CssClass="smLbl_to" Text="D. Receipt &amp; Payment(Encashment Basis)" Visible="False"></asp:Label>
                    </fieldset>


                    <asp:GridView ID="gvarecandpay" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="128px" OnRowDataBound="gvarecandpay_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCompanyrecapay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px"> 
                                            
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpayam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Amt.">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvbalpam" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                        ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblBankPosition" runat="server" CssClass="smLbl_to" Text="E. Bank Position" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="399px" OnRowDataBound="gvBankPosition_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlpsum2" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvbankposition" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent" Font-Underline="false"
                                        ForeColor="Black" Target="_blank" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))  %>'
                                        Width="140px"> 

                                            
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbankbalbp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closbal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Liabilities">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbankliabp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closlia")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available Loan Limit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnetcbolia" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avloan")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblRecAPayIssue" runat="server" CssClass="smLbl_to" Text="F. Receipt &amp; Payment(Issue Basis)" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvarecandpayis" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvarecandpayis_RowDataBound"
                        ShowFooter="True" Width="144px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCompanyrecapayis" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px"> 
                                            
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrecamis" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpayamis" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Amt.">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvbalamis" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                        ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblPDCCheque" runat="server" CssClass="smLbl_to" Text="G. PDC Issue Status" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvpdc" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpdc_RowDataBound"
                        ShowFooter="True" Width="399px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLgvCompanypaysum" runat="server" __designer:wfdid="w38" Font-Size="12px"
                                        Font-Underline="False" Style="font-size: 11px; color: Black;" Target="_blank"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))   %>'
                                        Width="140px"> 


                                            
                                            
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
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblProcurement" runat="server" CssClass="smLbl_to" Text="H. Procurement" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvprocure" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="341px" OnRowDataBound="gvprocure_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcomcodepro" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDepartpro" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px"> 
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Not Yet Received">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvreqpro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                        Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Materials Received">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvourchasepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                        Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Bill Completed">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvbillpro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                        Font-Underline="false" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Purchase History Material Wise">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvpurhmwisepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="100px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Purchase History Supplier Wise">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvpurhswisepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="100px">
                                            
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>


                    <fieldset>
                        <asp:Label ID="lblConstruction" runat="server" CssClass="smLbl_to" Text="I. Construction" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvMMPlanVsAch" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        OnRowDataBound="gvMMPlanVsAch_RowDataBound" Width="16px" CssClass="table-striped  table-hover table-bordered grvContentarea">
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
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvcomnamecons" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                        Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Target As Per Master Plan (Upto Date)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmasterplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masplan")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFmasPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monthly Work Target">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Monthly Execution">
                                <ItemTemplate>
                                    <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acheivement (%) on Mas. Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPerMasPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromasp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acheivement (%) on Monthly Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPerMonthPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromonp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Floor Wise Progress">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvflrwiseprogress" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="System Generated Target">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvsysgentarget" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inflation Effect">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvineffect" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblFeasibility" runat="server" CssClass="smLbl_to" Text="J. Bussiness Position(Today)" Visible="False"></asp:Label>
                    </fieldset>


                    <asp:GridView ID="gvFeasibility" runat="server" AutoGenerateColumns="False" OnRowDataBound=" gvFeasibility_RowDataBound"
                        ShowFooter="True" Width="399px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlpsum3" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvFeadesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" ForeColor="Black" Style="text-align: left; background-color: Transparent"
                                        Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim() : "")%>'
                                        Width="180px"> 

                                            
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Orginal">
                                <ItemTemplate>
                                    <asp:Label ID="lgvfearevnue" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="85px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Revised">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvfeacost" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" ForeColor="Black" Style="text-align: right; background-color: Transparent"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="85px"> 

                                            
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Change">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvfeamargin" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="85px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="%">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvfeapercnt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chngeper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblStock" runat="server" CssClass="smLbl_to" Text="M. Sold Status" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvpstk" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="341px" OnRowDataBound="gvpstk_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvcomname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                        Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="100px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Revenue">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtstkamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnSold Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvunsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsoldam")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sold Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldam")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtoramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receivable">
                                <ItemTemplate>
                                    <asp:Label ID="lgvatodues" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Not Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Previous Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvptoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcurduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delay & Return Charge">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdlyadcharge1"))%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblProjectStatus" runat="server" CssClass="smLbl_to" Text="N. Project Status (Upto Date)" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvProjectStatus01" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProjectStatus01_RowDataBound"
                        ShowFooter="True" Width="273px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvcomnameps1" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="100px"> 
                                            
                                          
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSales" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtoamtps1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetexpenses01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netexpamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Liabilities">
                                <ItemTemplate>
                                    <asp:Label ID="lgvliaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liaamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Position">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetloantamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netlnamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Report With Quantity">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvprorptwqty" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="90px"> 
                                            
                                          
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Budget Vs. Expenses">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvprobgdvsexp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="90px"> 
                                            
                                          
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Transaction - Day Wise">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvprotrdaywise" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="90px"> 
                                            
                                          
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblMonProStatus" runat="server" CssClass="smLbl_to" Text="O. Project Status(During the Period)" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvmonprost" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmonprost_RowDataBound"
                        ShowFooter="True" Width="273px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo14" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvcomnamemps" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname1")) %>'
                                        Width="100px"> 
                                            
                                          
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcostmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Collection ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcollamtmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Position">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetpositionmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblInventorystock" runat="server" CssClass="smLbl_to" Text="P. Inventory Status Report" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInventory_RowDataBound"
                        ShowFooter="True" Width="341px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre6" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcomcodeinv" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCompanyinv" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px"> 
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Issue Basis">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvissuebasisinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuebasis")) %>'
                                        Width="100px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Progress Basis">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvprobasisinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "probasis")) %>'
                                        Width="100px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Material Consumption">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvmatconinv" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matcon")) %>'
                                        Width="100px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblMarketing" runat="server" CssClass="smLbl_to" Text="Q. Marketing Information" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvcomwclients" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="234px" OnRowDataBound="gvcomwclients_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre4" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDescription" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="200px"> 
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Demand Analysis">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvSalDem" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        Font-Underline="false" Target="_blank" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Decision">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvSalDec" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        Font-Underline="false" Target="_blank" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Capacity Analysis">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvCliCap" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        Font-Underline="false" Target="_blank" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client History">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvCliHis" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        Font-Underline="false" Target="_blank" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Person History">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvSalPHis" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        Font-Underline="false" Target="_blank" BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>

                    <fieldset>
                        <asp:Label ID="lblFixedAssets" runat="server" CssClass="smLbl_to" Text="R. Fixed Asset Status" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvFxtAssets" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFxtAssets_RowDataBound"
                        ShowFooter="True" Width="223px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre7" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcomcodefxt" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvCompanyfxt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="140px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvclosamtfxt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblFinancialst" runat="server" CssClass="smLbl_to" Text="S. Finalcial Statement" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvFinalcialst" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFinalcialst_RowDataBound"
                        ShowFooter="True" Width="341px" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoidre8" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCompanyfin" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                        Width="120px"> 
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Income Statement">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvincomest" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Balance Sheet">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvbalancesheet" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Real Inflow & Outflow">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvinaoutflow" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Real Payment Summary">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvrealpsum" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Investment Plan">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvinplanfin" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Report-At a glance">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvpstaaglance" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                    <fieldset>
                        <asp:Label ID="lblHrMgt" runat="server" CssClass="smLbl_to" Text="T. HR Management" Visible="False"></asp:Label>
                    </fieldset>

                    <asp:GridView ID="gvHremp" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="37px" OnRowDataBound="gvHremp_RowDataBound" CssClass="table-striped  table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlpsum1" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkgvcomname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))   %>'
                                        Width="140px"> 
                                            
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Employee">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtoemp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toemp")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetsalary" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>


                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
