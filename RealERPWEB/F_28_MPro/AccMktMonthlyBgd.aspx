<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccMktMonthlyBgd.aspx.cs" Inherits="RealERPWEB.F_28_MPro.AccMktMonthlyBgd" %>

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
            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="lblMonth" runat="server">Month</asp:Label>
                            <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px"></asp:LinkButton>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2 lblmargin-top20px">
                            <label id="chkbod" runat="server" class="switch">
                                <asp:CheckBox ID="CpyCHeck" runat="server" AutoPostBack="true" OnCheckedChanged="CpyCHeck_CheckedChanged" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label runat="server" Text="Budget Copy?" Font-Bold="true" CssClass="btn btn-xs"></asp:Label>                           
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Page</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date" Style="display: none;"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox" Style="display: none;"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-2 col-md-2 col-sm-2" id="CopyTo" runat="server" visible="false">
                            <asp:Label ID="lblPrjName" runat="server" class="control-label" Text="To Month"></asp:Label>
                            <asp:DropDownList ID="ddltomonth" runat="server" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2" id="datediv" runat="server" visible="false">
                            <asp:Label ID="Label3" runat="server" class="control-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-sm-1 col-md-1 col-lg-1" id="Copybtn" runat="server" visible="false">
                            <asp:LinkButton ID="LbtnCopy" runat="server" Text="Ok" OnClick="LbtnCopy_Click" CssClass="btn btn-xs btn-success lblmargin-top20px" OnClientClick="return confirm('Do you agree to copy?')" ToolTip="Copy"><i class="fas fa-copy"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="DetailsCode" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="dgv3" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging" ShowFooter="True"
                                    CssClass="table-striped table-bordered grvContentarea">
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
                                                <asp:Label ID="gvlblActCode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Project">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblActDesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ATL" ItemStyle-HorizontalAlign="Center">
                                             <FooterTemplate>
                                                <asp:LinkButton ID="gvlnkFTotal" runat="server" OnClick="gvlnkFTotal_Click" Text="Total" CssClass="btn btn-warning btn-sm primarygrdBtn"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvTxtAmt1" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BTL" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" OnClick="lnkbtnUpdateRes_Click" Text="Update" CssClass=" btn  btn-success btn-sm  primarygrdBtn"></asp:LinkButton>
                                            </FooterTemplate>                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvTxtAmt2" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TTL" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvTxtAmt3" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
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
