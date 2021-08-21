<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMatReceipt.aspx.cs" Inherits="RealERPWEB.F_12_Inv.LinkMatReceipt" %>

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
            var gvscrol = $('#<%=this.gvMatStock.ClientID%>');
            var gvbudgetedmat = $('#<%=this.gvRptResBasis.ClientID%>');
            gvscrol.Scrollable();
            gvbudgetedmat.Scrollable();

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
                                    <div class="col-md-4 pading5px asitCol4">
                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="col-md-12 pading5px ">
                                        <asp:Label ID="lblprojectname" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:Label ID="lblvalprojectname" runat="server" CssClass="inputlblVal" Text="Project Name"></asp:Label>

                                        <asp:Label ID="lblmatname" runat="server" CssClass=" smLbl_to" Text="Material Name"></asp:Label>
                                        
                                        <asp:Label ID="lblvalmatname" runat="server" CssClass="inputlblVal" Text="Material Name"></asp:Label>
                                        <asp:Label ID="lbldate" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                        <asp:Label ID="lblvaldat" runat="server" CssClass=" inputtextbox" Text="" s></asp:Label>
                                    </div>





                                </div>


                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>




                                </div>

                            </div>
                        </fieldset>
                         <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewMatReceipt" runat="server">
                            <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
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
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Inwards">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinwards" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lblgvFinqty" runat="server" Width="70px" Font-Bold="True"
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
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFoutqty" runat="server" Width="70px" Font-Bold="True"
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
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lblgvFclsqty" runat="server" Width="70px" Font-Bold="True"
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

                        </asp:View>

                               <asp:View ID="ViewBudgetedMat" runat="server">
                                   <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="847px" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description">
                                            <FooterTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Total" Width="110px"></asp:Label>
                                             
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>

                                                 <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>
                                         

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



