<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpInflaEffect.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpInflaEffect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvRCost.ClientID %>');
            gv1.Scrollable();

        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindProj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProj_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlAccProject" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click" TabIndex="4">Show</asp:LinkButton>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                            BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="70px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="400">400</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl_to" Text="Date: "></asp:Label>

                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate" Enabled="true"></cc1:CalendarExtender>


                                    </div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblgroup" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Group"></asp:Label>

                                        <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                            Width="70px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White"></asp:Label>


                                    </div>


                                </div>
                            </div>

                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewCostSPerSFT" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">


                                        <div class="form-group">

                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="lblConArea" runat="server" CssClass="smLbl_to" Text="Construction Area: " Visible="False"></asp:Label>

                                                <asp:Label ID="lbltxtCArea" runat="server" CssClass="smLbl_to" Visible="False"></asp:Label>

                                            </div>

                                        </div>
                                    </div>

                                </fieldset>
                            </div>


                            <asp:GridView ID="gvCost" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvCost_PageIndexChanging"
                                ShowFooter="True" Width="902px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvresdesc" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Cost/SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbcncst" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bcncst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbcncst" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Cost/SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcncst" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cncst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcncst" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Cost/SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbSlcst" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bslcst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbslcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Cost/SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcnslcst" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cnslcst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcnslcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
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
                        </asp:View>
                        <asp:View ID="ViewRemainCost" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRCost" runat="server"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvRCost_RowDataBound">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl </B> (1)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Description &nbsp&nbsp&nbsp&nbsp (2)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdesc" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))  %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="White" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit &nbsp (3)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Qty &nbsp&nbsp (4)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:LinkButton ID="lbtntotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" OnClick="lbtntotal_Click">Total</asp:LinkButton>

                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty &nbsp (5)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrcvqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bal. Qty &nbsp (6)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbalqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Rate  (7)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbgdvrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Rate  (8)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbgdrerate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rerate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Last Pur. Rate &nbsp (9)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvLRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Pur. Rate &nbsp (10)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvacrate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price Increased &nbsp 11=(10-7)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Addtional Cost for Price Increased &nbsp 12=(5*(8-7))">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBdgPurIn" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "princ")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 7%; height: 25px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFbgdPuramt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Saving &nbsp 12=(5*(8-7))">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBdgPurDe" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pridesc")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 7%; height: 25px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFbgdPuramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Required for Rem. Purchase &nbsp&nbsp&nbsp&nbsp 13=(6*10)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbalPur" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdbalqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="85px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 7%; height: 25px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFbgdBalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="85px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avg Rate &nbsp&nbsp&nbsp&nbsp 14=(13+(5*8))/4">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvBgdReq" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbgdreq")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 7%; height: 25px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvtBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                            <asp:Panel ID="PanelNote" runat="server" BorderColor="Maroon"
                                BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                <div class="form-group">


                                    <div class="col-sm-6 pading5px">
                                        <asp:Label ID="lbtnBankPos" runat="server" CssClass="btn btn-success primaryBtn">Note</asp:Label>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="lblstatus" runat="server" CssClass=" smLbl_to" Width="200px" Text="Description"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Width="200px" Text="Amount"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Width="200px" Text="Const. SFT"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to" Width="200px" Text="Cost/SFT"></asp:Label>
                                                <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" Width="200px" Text="Sales SFT"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" Width="200px" Text="Sales/SFT"></asp:Label>
                                                <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Width="200px" Text="In %"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Width="200px" Text="Description"></asp:Label>
                                                <asp:Label ID="Label8" runat="server" CssClass=" smLbl_to" Width="200px" Text="Amount"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="lbllssue" runat="server" CssClass="smLbl_to" Width="200px" Text="A. Orginal Budget"></asp:Label>
                                                <asp:Label ID="lblOrgBgdVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblOrgBgdCon" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblOrgBgdConSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblOrgBgdSal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblOrgBgdSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblOrgBgdPr" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Width="200px" Text="A. Revised Budget"></asp:Label>
                                                <asp:Label ID="lblNAmt" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>

                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label16" runat="server" CssClass="smLbl_to" Width="200px" Text="B. Revised Budget"></asp:Label>
                                                <asp:Label ID="lblRevBgdHead" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">

                                                <asp:Label ID="Label10" runat="server" CssClass="smLbl_to" Width="200px" Text="B. Actual Cost"></asp:Label>
                                                <asp:Label ID="lblNAmt0" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label11" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Received As Per Budget"></asp:Label>
                                                <asp:Label ID="lblRevBgdAmVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label12" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Purchase"></asp:Label>
                                                <asp:Label ID="lblNAmt1" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>



                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label13" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Price Increased"></asp:Label>
                                                <asp:Label ID="lblRevPriIncVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>

                                            <div class="col-md-5">
                                                <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;General Advance"></asp:Label>
                                                <asp:Label ID="lblNAmt2" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label15" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;PriceDesrees"></asp:Label>
                                                <asp:Label ID="lblRevPriDecVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label17" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Sub-Contractor Advance"></asp:Label>
                                                <asp:Label ID="lblNAmt3" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>



                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label18" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;  Remaning Purchase"></asp:Label>
                                                <asp:Label ID="lblRevRemPurVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label ID="Label19" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Suplier Advance"></asp:Label>
                                                <asp:Label ID="lblNAmt4" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label20" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Total Amount"></asp:Label>
                                                <asp:Label ID="lblRevTotalVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblRevCon" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblRevConSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblRevSal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblRevSalSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblRevInPr" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label27" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Total Actual Cost"></asp:Label>
                                                <asp:Label ID="lblNAmt5" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <asp:Label ID="Label21" runat="server" CssClass="smLbl_to" Width="200px" Text="C. Increased"></asp:Label>
                                                <asp:Label ID="lblIncVal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblInccon" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblIncconSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblIncSal" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblIncSalSFT" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                                <asp:Label ID="lblIncPr" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label29" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Remaning Cost"></asp:Label>
                                                <asp:Label ID="lblNAmt6" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>

                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label22" runat="server" CssClass="smLbl_to" Width="200px" Text="C. Liabilities"></asp:Label>
                                                <asp:Label ID="lblNAmt7" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label23" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp; &nbsp;General Liabilities"></asp:Label>
                                                <asp:Label ID="lblNAmt8" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label24" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Sub-Contractor"></asp:Label>
                                                <asp:Label ID="lblNAmt9" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label25" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Suplier"></asp:Label>
                                                <asp:Label ID="lblNAmt10" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label26" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Total Liabilities"></asp:Label>
                                                <asp:Label ID="lblNAmt11" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7">
                                            </div>
                                            <div class="col-md-5">
                                                <asp:Label ID="Label28" runat="server" CssClass="smLbl_to" Width="200px" Text="&nbsp;&nbsp;Remaning Cost with Liabilities"></asp:Label>
                                                <asp:Label ID="lblNAmt12" runat="server" Width="200px" CssClass="inputTxt inputDateBox" Style="text-align: right;"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>

                                    </div>


                                </div>

                            </asp:Panel>

                        </asp:View>
                        <asp:View ID="ProjectGraphChart" runat="server">



                            <asp:Panel ID="pnlTarVsAchievement" runat="server" BorderColor="Yellow"
                                BorderStyle="Solid" BorderWidth="1px" Visible="False">


                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="form-group">
                                                    <div class="col-md-4 pading5px asitCol4">
                                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="Start Date"></asp:Label>
                                                        <asp:TextBox ID="lblStartDate" runat="server" CssClass="inputtextbox "></asp:TextBox>


                                                        <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="End Date"></asp:Label>
                                                        <asp:TextBox ID="lblEndDate" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                                    </div>


                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-4 pading5px asitCol4">
                                                        <asp:Label ID="Label30" runat="server" CssClass="lblTxt lblName" Text="Duration"></asp:Label>
                                                        <asp:TextBox ID="lblDuration" runat="server" CssClass="inputtextbox "></asp:TextBox>


                                                        <asp:Label ID="lblProgressInPer" runat="server" CssClass="smLbl_to" Text="End Date"></asp:Label>
                                                        <asp:CheckBox ID="chkGraph" runat="server" BCssClass="btn btn-primary checkBox" Text="Graph" />



                                                    </div>


                                                </div>
                                            </asp:Panel>


                                        </div>
                                    </fieldset>
                                </div>


                            </asp:Panel>

                            <asp:GridView ID="gvtarvsachivement" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Style="margin-right: 0px" Width="323px">
                                <PagerSettings Position="Top" />
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
                                    <asp:TemplateField HeaderText="Duration">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotalTarVsAchievement" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" OnClick="lbtnTotalTarVsAchievement_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvduration" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monyear")) %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target Amt.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtaramt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtaramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFacamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="60px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Completed (%)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcompleted" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper"))>0?"%":"") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cumulative Target">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdateTarVsAchievement" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" OnClick="lbtnUpdateTarVsAchievement_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcomtarget" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cumulative Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcomactual" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <asp:Chart ID="Chart1" runat="server" Height="264px" Width="663px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                        MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                        MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100">
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Titles>
                                    <asp:Title Font="Time New Romans, 16px" Name="Title1"
                                        Text="Project Target Vs. Achievement">
                                    </asp:Title>
                                </Titles>
                            </asp:Chart>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

