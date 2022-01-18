

<%@ Page Language="C#"  MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMktMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptMktMoneyReceipt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            $('.chzn-select').chosen({ search_contains: true });

            
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
                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="Label5" runat="server"
                                            Text="From:" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox"
                                            Font-Bold="True" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">


                                        <asp:Label ID="Label6" runat="server" Text="To:" Font-Bold="True" CssClass="smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" Font-Bold="True"
                                            Width="80px" BorderStyle="None"></asp:TextBox>

                                        <asp:CheckBox ID="chkAllSchedul" runat="server" Font-Bold="True" CssClass="chkBoxControl pading5px"
                                            Text="Multiple Cheque No" />

                                        <asp:CheckBox ID="chkOrginal" runat="server" CssClass="chkBoxControl pading5px" Text="Orginal " Visible="False" />

                                        <asp:Label ID="lPactCode" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server" Text="MR No:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                 <div class="col-md-4 asitCol4 pading5px">




                                        <asp:DropDownList ID="ddlMRNO" runat="server" Font-Bold="True" CssClass="chzn-select form-control inputTxt" Width="250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlMRNO_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbShow0" runat="server"
                                            OnClick="lblShow_Click"
                                            CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>
                                        </td>
                                           
                                                <asp:Label ID="lusircode" runat="server" CssClass="lblName lblTxt" Visible="False"></asp:Label>

                                        <asp:Label ID="lmsg" runat="server" CssClass="lblmsg"></asp:Label>
                                  </div>
                                </div>
                            </div>
                        </fieldset>
                        
                            <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCheckno" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbankna" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrance" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" OnClick="lnkDelete_Click1">Delete</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Date">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-decoration: none;"> Total </asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaydate" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Amt." Visible="True">
                                        <FooterTemplate>
                                            <asp:Label ID="txtFTotal" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Lblpaidamount" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="110px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Discount " Visible="True">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFdisamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdiscount" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="110px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>



                        
                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                            TargetControlID="ddlMRNO">
                        </cc1:ListSearchExtender>

                        <asp:Label ID="lblrecdate" runat="server" Visible="false"></asp:Label>

                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy"
                            TargetControlID="txttodate"></cc1:CalendarExtender>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

