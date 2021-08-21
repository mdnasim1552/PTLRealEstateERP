<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpRevsiFeasibility.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpRevsiFeasibility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
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
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblProjectName" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Width="380px" Text="A. Sales"></asp:Label>



                                    </div>

                                </div>




                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewReviseFea" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label11" runat="server" AutoCompleteType="Disabled" Text="Saleable Area (Total):" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <asp:GridView ID="gvAreaCal" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" OnRowDataBound="gvAreaCal_RowDataBound">
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
                                            <asp:Label ID="txtgvlsizess" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
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
                                            <asp:Label ID="lgvFtssfts" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtssfts" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvBQTY" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvBDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc"))%>'
                                                Width="80px"></asp:Label>
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label10" runat="server" AutoCompleteType="Disabled" Text="Saleable Area Distribution:" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-7 pading5px ">
                                                <asp:Label ID="lblLownertext" runat="server" CssClass="lblTxt lblName" Text="Land Owner:"></asp:Label>
                                                <asp:Label ID="lblLownerval" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                                <asp:Label ID="lblCompanytext" runat="server" CssClass="smLbl_to" Text="Company"></asp:Label>
                                                <asp:Label ID="lblCompanyval" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                                <asp:Label ID="lblcft" runat="server" CssClass="smLbl_to" Text="Cost/SFT"></asp:Label>
                                                <asp:Label ID="lblCFTVal" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>



                            <asp:GridView ID="gvDevShare" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px" OnRowDataBound="gvDevShare_RowDataBound">
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
                                            <asp:Label ID="lblgvSize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Land Owner">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlowner" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcompany" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase From Land Owner">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvpurfrmlanowner" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Adjustment">

                                        <ItemTemplate>
                                            <asp:Label ID="txtgvadj" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtotalcompany" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Constration Area">

                                        <ItemTemplate>
                                            <asp:Label ID="txtgvConArea" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;-#,##; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BEP">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBEP" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;-#,##; ") %>'
                                                Width="70px"></asp:Label>
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" runat="server" CssClass="btn btn-success primaryBtn" Text="Sale Revenue:"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-7 pading5px">

                                                <asp:Label ID="lblDuration0" runat="server" CssClass="lblTxt lblName" Text="Project Duration:"></asp:Label>
                                                <asp:Label ID="lblDurationVal" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                                <asp:Label ID="lblProfit0" runat="server" CssClass="smLbl_to" Text="Profit %:"></asp:Label>
                                                <asp:Label ID="lblProfitVal" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                                <asp:Label ID="lblTarRev" runat="server" CssClass="smLbl_to" Text="Target Revenue:"></asp:Label>
                                                <asp:Label ID="lblTrevVal" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="640px" OnRowDataBound="gvSales_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
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

                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemdesc4" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor Desc " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgFlr" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPar" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllsizes" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgratesalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0.00;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamtsalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgbroratsalrev" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brochure Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrBAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BEP">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgBEP" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profit </Br>Addition </Br>in %">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgProfit" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Price">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgMinPrice" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrMAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label9" runat="server" AutoCompleteType="Disabled" Text="Sold Information:" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="gvSold" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px" OnRowDataBound="gvSold_RowDataBound">
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
                                            <asp:Label ID="lblinfdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:Label>
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
                                            <asp:Label ID="lbllsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcontosft" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utility & Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstonum" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvlblgvtamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFacing" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvView" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook")) %>'
                                                Width="70px"></asp:Label>
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label8" runat="server" AutoCompleteType="Disabled" Text="UnSold Information:" CssClass="btn btn-success primaryBtn"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>



                            <asp:GridView ID="gvUnsold" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px" OnRowDataBound="gvUnsold_RowDataBound">
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
                                            <asp:Label ID="lblinfdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:Label>
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
                                            <asp:Label ID="lbllsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlsisessoldinfo" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcontosft" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFcontosft" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvlsizes" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvtsizes" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utility & Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstonum" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Facing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFacing" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvView" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook")) %>'
                                                Width="70px"></asp:Label>
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label7" runat="server" AutoCompleteType="Disabled" Text="Variance" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="gvVari" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                            <asp:Label ID="lblinfdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcontosft" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFcontosft" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="Label6" runat="server" AutoCompleteType="Disabled" Text="Project Status:" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="gvPrjStatus" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsoldinfop" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinfdescp" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtamtp" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0;(#,##0); ") %>'
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

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-5 pading5px">

                                                <asp:Label ID="lblSaleRevenue" runat="server" AutoCompleteType="Disabled" Text="Income Statement:" CssClass="btn btn-success primaryBtn" ></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="gvInSta" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="651px" OnRowDataBound="gvInSta_RowDataBound">
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
                                    <asp:TemplateField HeaderText="Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCodsoldinfo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinfdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                Width="230px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Orginal Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcontosft" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFcontosft" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Revised Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvlsizes" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Orginal Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgratesoldinfo" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##00.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="% Revised Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvtsizes" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% of Orginal Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstonum" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvstonum" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% of Revised Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtcsizes" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvtcsizes" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
