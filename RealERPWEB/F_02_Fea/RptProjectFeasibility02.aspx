<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectFeasibility02.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptProjectFeasibility02" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
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
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control inputTxt">
                                            <asp:ListItem>Main Group</asp:ListItem>
                                            <asp:ListItem>Sub Group</asp:ListItem>
                                            <asp:ListItem>Sub-2 Group</asp:ListItem>
                                            <asp:ListItem>Sub-3 Group</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>


                                </div>
                            </div>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewProjectInfo" runat="server">
                            <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="772px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>' Width="4px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvVal" runat="server" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="0px" Height="20px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                                Width="510px"></asp:Label>
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
                        </asp:View>
                        <asp:View ID="ViewCost" runat="server">
                            <asp:GridView ID="gvFeaPrjFC" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodfc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdesc2" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum2" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Land">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlandsize" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sft.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvconktosft" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sft.Per Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsftperf" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" MGC %">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpercntge" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sft./Floor">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvlsizes" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No.of Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstonum" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Sft.">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtcsft" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtcsft" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total (%)">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPercent" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perent")).ToString("#,##0.00;(#,##00.00); ")+" %" %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvFeaPrjC_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="792px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdescc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnumc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvqtyc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvratec" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Orginal Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgveamtc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFeAmtc" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="App. Add Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvaaddamtc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aadam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFaaddAmtc" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expected Add Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvexaddamtc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eadam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFexaddAmtc" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Save Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsaveamtc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "savam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFsaveAmtc" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Revised Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgvtotalamtc" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalAmtc" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Per Sft">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcospersft" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costpsft")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalCpSft" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                         
                        </asp:View>
                        <asp:View ID="ViewSalesRevenue" runat="server">
                            <div class=" form-group">
                                <asp:Label ID="lblSaleableAreaa" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="Saleable Area (Total):"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvFeaPrjFCS" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodfcs" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdesc3" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum3" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Land">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlandsizes" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sft.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvconktosfts" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sft Per F">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsftperfs" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MGC %">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpercntges" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total SFT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvlsizess" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sto Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstonums" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="T S Sft.">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtssfts" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvtssfts" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvBQTY" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvBDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc"))%>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                            <div class=" form-group">
                                <asp:Label ID="lblSaleableAreaDistributot" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="Saleable Area Distribution"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:Panel ID="Panel2" runat="server" BackColor="#F5F5F5">
                                <div class=" form-group">
                                    <asp:Label ID="lblLownertext" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Land Owner Share:"></asp:Label>
                                    <asp:TextBox ID="lblLownerval" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                    <asp:Label ID="lblCompanytext" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Company Share:"></asp:Label>

                                    <asp:TextBox ID="lblCompanyval" runat="server" Visible="False" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                    <asp:Label ID="lblProfit" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Profit %:"></asp:Label>

                                    <asp:TextBox ID="lblProfitVal" runat="server" Visible="False" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                    <div class="clearfix"></div>
                                </div>


                            </asp:Panel>

                            <asp:GridView ID="gvFeaPrj" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvFeaPrj_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTotal1" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdesc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalsh" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Land Owner">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlowner" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlownersh" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcompany" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "company")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcompanysh" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase From Land Owner">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpurfrmlanowner" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pflowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpfrmlownersh" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Adjustment">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvadj" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjmnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFadjmnt" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtotalcompany" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalcom")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtcompanysh" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Constration Area">

                                        <ItemTemplate>
                                            <asp:Label ID="txtgvConArea" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iconarea")).ToString("#,##0;-#,##; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>



                            <div class=" form-group">
                                <asp:Label ID="lblSaleRevenue" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="Sale Revenue:"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>



                            <asp:GridView ID="gvFeaPrjsalrev" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDeleting="gvFeaPrj_RowDeleting"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsalrev" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTotal2" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdesc4" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizessalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisessalrev" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgratesalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtsalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtsalrev" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgbroratsalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brochure Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrBAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgrFBAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Price">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgMinPrice" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minprice")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgrFMAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrMAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                            <div class=" form-group">
                                <asp:Label ID="lblSodInformation" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="Sold Information"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>


                            <asp:GridView ID="gvFeaPrjSoldInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <FooterTemplate>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsoldinfo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItemdescsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnumsoldinfo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizessoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisessoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamtsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtsoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Average Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgavgratsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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



                            <div class=" form-group">
                                <asp:Label ID="lblUnSodInformation" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="UnSold Information"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvFeaPrjusoldinfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <FooterTemplate>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodusoldinfo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItemdescusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitusoldinfo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizesusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisesusoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrateusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamtusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtusoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Average Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgavgratusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <asp:View ID="ViewReport" runat="server">
                            <asp:GridView ID="gvFeaPrjRep" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                Width="785px" OnRowDataBound="gvFeaPrjRep_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdescRep" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnumRep" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lgSizerep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lgnumrep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total SFT / Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lgtsizecRep" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate/SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgsalraterep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmtc0" runat="server" Font-Bold="True" Font-Size="12px"  ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewSalesLand" runat="server">

                            <asp:GridView ID="grvSaleRevLand" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsalrev" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">

                                        <ItemTemplate>

                                            <asp:TextBox ID="txtgvItemdescsalrev" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:TextBox>


                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizessalrev" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisessalrev" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesalrev" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamtsalrev" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtsalrev" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brochure Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgbroratsalrev" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                            <div class=" form-group">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="Sold Information"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>


                            <asp:GridView ID="grvSoldLand" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsoldinfo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItemdescsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnumsoldinfo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizessoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisessoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamtsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtsoldinfo" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Average Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgavgratsoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                            <div class=" form-group">
                                <asp:Label ID="Label10" runat="server" Font-Bold="True"
                                    Font-Size="12px" Text="UnSold Information"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="grvUnSoldLAnd" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodusoldinfo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItemdescusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitusoldinfo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtglsizesusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisesusoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrateusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamtusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFamtusoldinfo" runat="server" Font-Bold="True" Font-Size="12px"
                                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Average Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgavgratusoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                    </asp:MultiView>
                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
