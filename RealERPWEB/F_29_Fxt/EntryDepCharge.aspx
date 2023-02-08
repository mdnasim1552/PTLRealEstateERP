<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryDepCharge.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.EntryDepCharge" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
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
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel3" runat="server">

                                <div class="form-group">
                                    <div class="col-md-8   pading5px  asitCol8">
                                        <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="From:"></asp:Label>
                                        <asp:TextBox ID="txtFromdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txtTodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>

                                        <asp:CheckBox ID="chkStraight" runat="server" TabIndex="10" Text="Straight" CssClass="btn btn-primary checkBox" />
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-8  pading5px  asitCol8">
                                        <asp:Label ID="txtDays" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:GridView ID="grDep" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                ShowFooter="True" Style="text-align: left" Width="810px" OnRowCreated="grDep_RowCreated" PageSize="20">

                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid1" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particulars">
                        <ItemTemplate>
                            <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: left" Text="Total :" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance as on ">
                        <ItemTemplate>
                            <asp:Label ID="lgvOpening" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Addition During The year">
                        <ItemTemplate>
                            <asp:Label ID="lgvAddition" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTAddition" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Disposal During the year">
                        <ItemTemplate>
                            <asp:Label ID="lgvsalesdec" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleam")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFsalesdec" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Revaluation During The year">
                        <ItemTemplate>
                            <asp:Label ID="lgvdisposal" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTDisposal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Balance as on ">
                        <ItemTemplate>
                            <asp:Label ID="lgvTotal" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="left" />
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate Of Dep. %">
                        <FooterTemplate>
                            <asp:HyperLink ID="hlnkgvFdep" runat="server" Font-Bold="True"
                                Font-Size="12px"  CssClass="btn   btn-primary " Style="text-align: right" Target="_blank" Text="Voucher"
                                Width="60px"></asp:HyperLink>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvDepPar" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depreciation as on">
                        <ItemTemplate>
                            <asp:Label ID="lgvDepOpen" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndep")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTDepOpen" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addition During The year ">
                        <ItemTemplate>
                            <asp:Label ID="lgvDepCur" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdep")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTDepCur" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Disposal during the year ">
                        <ItemTemplate>
                            <asp:Label ID="lgvadjment" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjam")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFadjment" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>

                    
                    <asp:TemplateField HeaderText="Balance as at">
                        <ItemTemplate>
                            <asp:Label ID="lgvDepTotal" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todep")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTDepTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W.D Values as on ">
                        <ItemTemplate>
                            <asp:Label ID="lgvCBal" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTCBal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="right" Width="80px" />
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
            </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
