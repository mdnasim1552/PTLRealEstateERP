<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkAccSpLedger.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkAccSpLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            <%--var gvSpledger = $('#<%=this.gvSpledger.ClientID %>');
            var gvSPayment = $('#<%=this.gvSPayment.ClientID %>');
            var gvAllSupPay = $('#<%=this.gvAllSupPay.ClientID %>');
            var gvSPayment02 = $('#<%=this.gvSPayment02.ClientID %>');
            gvSpledger.Scrollable();
            gvSPayment.Scrollable();
            gvAllSupPay.Scrollable();
            gvSPayment02.Scrollable();--%>



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
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true">
                                        </cc1:CalendarExtender>

                                    </div>





                                </div>
                            </div>
                        </fieldset>

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Resource Heads"></asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkShowLedger_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
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




                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGroup" runat="server" CssClass="lblTxt lblName" Visible="false">Group</asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage"
                                            Visible="False">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkShowDet" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkShowDet_Click" Visible="False">Show Details</asp:LinkButton>

                                        </div>



                                    </div>




                                </div>

                            </div>


                        </fieldset>


                    </div>
                    
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="SpcLedger" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvSpledger" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvSpledger_RowDataBound1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Group Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrp" runat="server" CssClass="GridLebelL"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vou.Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher No.">

                                                <ItemTemplate>



                                                    <asp:HyperLink ID="HLgvvounum" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                        Width="105px"></asp:HyperLink>




                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOpAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr. Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr. Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Closing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvClAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblremarks" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Narration">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnarration" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>

                            </asp:View>

                            <asp:View ID="SPayment" runat="server">
                                <div class="table-responsive table">
                                    <asp:GridView ID="gvSPayment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="832px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vou.Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvoudate0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvounum0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCrAmts" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCrAmts" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Advance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDrAmts" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bal. Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCrAmts" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBalAmts" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                            <asp:View ID="AllSupPayment" runat="server">
                                <div class="table-responsive table">
                                    <asp:GridView ID="gvAllSupPay" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        OnRowDataBound="gvAllSupPay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupCode" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Supplier's Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvSupDesc" runat="server" __designer:wfdid="w38"
                                                        CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="270px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Dr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOpnAdv" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnadv")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpnAdv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Cr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOpnBill" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnbill")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpnbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Dr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDrAmountas" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDrAmtas" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Cr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCrAmtas" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCrAmtas" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Closing Dr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvClsAdv" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsadv")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFclsadv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing Cr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvClsbill" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsbill")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFclsbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uncleared Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvIsuAmt" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFIsuAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Pending Bill">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBillAmt" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFBillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Balance Dr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalDr" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isudr")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFBalDr" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Cr.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalCr" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isucr")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFBalCr" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>


                                </div>


                            </asp:View>
                            <asp:View ID="Supplierpayment02" runat="server">
                                <div class="table-responsive table">
                                    <asp:GridView ID="gvSPayment02" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="758px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vou.Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvoudatesp02" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bill/Voucher #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvounumsp02" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrefnosp02" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectNamesp02" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Opening Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnamsp02" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillsp02" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpaymentsp02" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing Bal">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclsamsp02" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewIssVsMonPay" runat="server">
                               
                                 <asp:GridView ID="gvMonIsuPay" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvMonIsuPay_PageIndexChanging"
                                    OnRowDataBound="gvMonIsuPay_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAccCod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgcatCod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ResCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acc. Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAccDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPVnum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvounum1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPVDate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchnono" runat="server" Width="100px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px">Total:</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque Date" Visible="false">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issued Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Cleared Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreconamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvbcldate" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                               
                            </asp:View>
                            <asp:View ID="ViewIssVsMonPaySum" runat="server">
                                <asp:GridView ID="gvIsuVsPaySum" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AllowPaging="True" OnPageIndexChanging="gvIsuVsPaySum_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ResCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvSu" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="180px">Total:</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issued Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Cleared Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreconamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </asp:View>
                        </asp:MultiView>

                   


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

