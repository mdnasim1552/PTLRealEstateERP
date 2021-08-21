<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMatRequirement.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.LinkMatRequirement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            $('#<%=this.gvMatStock.ClientID%>').tblScrollable();
            $('#<%=this.gvRptResBasis.ClientID%>').tblScrollable();

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
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:Label ID="lblvalDate" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:Label>
                                        <asp:Label ID="lblproject" runat="server" CssClass=" smLbl_to" Text="Project:"></asp:Label>
                                        <asp:Label ID="lblvalproject" runat="server" CssClass=" inputlblVal" Text=""></asp:Label>
                                        <asp:Label ID="lblMaterial" runat="server" CssClass=" smLbl_to" Text="Material:"></asp:Label>
                                        <asp:Label ID="lblvalMaterial" runat="server" CssClass=" inputlblVal" Text=""></asp:Label>

                                    </div>
                                </div>




                            </div>
                        </fieldset>

                        <div class="col-md-5">

                              <asp:Label ID="lblReceipt" runat="server" CssClass="inputlblVal" Text="A. Received:"></asp:Label>
                            <div class="clearfix"></div>
                        <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvMatStock_PageIndexChanging" Width="445px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "vdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounum" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vnumber")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrefno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Inwards">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvinwards" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFinqty" runat="server" Width="60px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>


                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outwards">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvoutqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFoutqty" runat="server" Width="55px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>


                                    <ItemStyle HorizontalAlign="Right" />

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclsqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFclsqty" runat="server" Width="60px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                            </div>


                        <div class="col-md-7">
                            <asp:Label ID="lblconsumtion" runat="server" CssClass="inputlblVal" Text="B. Requirement:"></asp:Label>
                            <div class="clearfix"></div>

                            <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                        <PagerSettings Position="Top" />
                        <Columns>
                           
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                        Width="270px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvworktQty" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptbgdqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" Material Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
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


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



