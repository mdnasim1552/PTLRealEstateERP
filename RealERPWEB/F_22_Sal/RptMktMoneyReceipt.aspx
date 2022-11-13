<%@ Page Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMktMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptMktMoneyReceipt" %>


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
           <div class="card card-fluid mt-4">
                <div class="card-header">
                    <div class="row">
                        <div class="col-sm-12 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="form-label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2_txtfromdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm  w100"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1_txttodate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="form-label" OnClick="ibtnFindProject_Click">MR No</asp:LinkButton>
                                <asp:DropDownList ID="ddlMRNO" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlMRNO_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                </asp:DropDownList>

                                <asp:CheckBox ID="chkOrginal" runat="server" CssClass="chkBoxControl pading5px" Text="Orginal " Visible="False" />

                                <asp:Label ID="lPactCode" runat="server" CssClass="lblTxt lblName" Visible="False"></asp:Label>
                                <asp:Label ID="lusircode" runat="server" CssClass="lblName lblTxt" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-2 ">
                            <div class="form-group">
                               <asp:CheckBox ID="chkAllSchedul" runat="server" CssClass="form-label chkBoxControl pading5px"
                                    Text="Multiple Cheque No" /> </br>
                                <asp:LinkButton ID="lbShow0" runat="server" CssClass="btn btn-primary btn-xs mt20" OnClick="lblShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        
                    </div>
                </div>







                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
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





                        <asp:Label ID="lblrecdate" runat="server" Visible="false"></asp:Label>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

