<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccMktMonthlyBgd.aspx.cs" Inherits="RealERPWEB.F_28_MPro.AccMktMonthlyBgd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
         .mt20 {
            margin-top: 20px;
        }
    </style>

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
            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="lblMonth" runat="server">Month</asp:Label>
                            <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1 mt20">
                            <asp:CheckBox ID="CpyCHeck" AutoPostBack="true" OnCheckedChanged="CpyCHeck_CheckedChanged"  runat="server" Text='<span class="lblTxt">Budget Copy?</span>' />
                        </div>                       
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date" Style="display: none;"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox" Style="display: none;"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-3 col-md-3 col-sm-6" id="CopyTo" runat="server" visible="false">
                            <asp:Label ID="lblPrjName" runat="server" class="control-label  lblmargin-top9px" Text="To Month"></asp:Label>
                            <asp:DropDownList ID="ddltomonth" runat="server" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6" id="datediv" runat="server" visible="false">
                            <asp:Label ID="Label3" runat="server" class="control-label  lblmargin-top9px" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-sm-1 col-md-1 col-lg-1" id="Copybtn" runat="server" visible="false">
                            <asp:LinkButton ID="LbtnCopy" runat="server" Text="Ok" OnClick="LbtnCopy_Click" CssClass="btn btn-xs btn-success mt20" OnClientClick="return confirm('Do you agree to copy?')" ToolTip="Copy"><i class="fas fa-copy"></i></asp:LinkButton>
                        </div>
                    </div>

                </div>
                <div class="card-body" style="min-height: 350px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="DetailsCode" runat="server">
                            <asp:Panel ID="Panel3" runat="server">
                                <div class="row">
                                    <div class="col-sm-2 col-md-2 col-lg-2 mt20">
                                        <div class="form-group">
                                            <div class="input-group input-group-alt input-group-sm">
                                                <div class="input-group-prepend ">
                                                    <span class="input-group-text">Page</span>
                                                </div>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                    <asp:ListItem>900</asp:ListItem>
                                                    <asp:ListItem>1500</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="table-responsive">
                                <asp:GridView ID="dgv3" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging" ShowFooter="True"
                                    CssClass="table-striped table-bordered grvContentarea">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblrescode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Project">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblResDesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblresunit" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" OnClick="lnkbtnUpdateRes_Click" Text="Update" CssClass=" btn  btn-success btn-sm  primarygrdBtn"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>



                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="gvlnkFTotal" runat="server" OnClick="gvlnkFTotal_Click" Text="Total" CssClass="btn btn-warning btn-sm primarygrdBtn"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblRate" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None"
                                                    Font-Bold="True" Font-Size="11px" ForeColor="black" ReadOnly="True" Style="text-align: right" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                    Font-Bold="True" Font-Size="11px" ForeColor="black" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });


        function pageLoaded() {
            try {
                $('#<%=this.dgv3.ClientID%>').Scrollable();
            }

            catch (e) {

                alert(e);
            }


        }
    </script>
</asp:Content>
