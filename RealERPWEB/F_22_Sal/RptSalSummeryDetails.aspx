<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalSummeryDetails.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSalSummeryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


           <%-- var gvSalVsColl = $('#<%=this.gvSalSummeryDetails.ClientID %>');

            gvSalVsColl.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });--%>

        }
        function printEnvelop(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
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
    <div class="container">
        <div class="contentPart">
            <div class="row">
                <div class="table table-responsive">
                    <asp:GridView ID="gvSalSummeryDetails" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <HeaderTemplate>
                                    <table style="width: 200px;">
                                        <tr>
                                            <td class="style58">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                    Text="Description" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style60">&nbsp;</td>
                                            <td>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center" Width="110px">Export Excel</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lgvname" runat="server"
                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>

                                
                                <ItemStyle HorizontalAlign="left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>"Description"</th>&nbsp
                                            <th>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                    CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                            </th>
                                        </tr>
                                    </table>

                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvCName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="MR NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCmrno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCrectypedesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rectypedesc")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Collection">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtocollection" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFtotalcal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />

                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bank Clearance">
                                <ItemTemplate>
                                    <asp:Label ID="lgvreconamt" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFbankClear" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />

                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Cheque Deposited">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchqdep" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFchqdepo" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="In Hand(Returned Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhrchq" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFreturnchq" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="In Hand(Fresh Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhfchq" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFretunfchq" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="In Hand(Post Dated Cheque)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinhpchq" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFpastdchq" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Replacement Cheque">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrepchq" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repchq")).ToString("#,##0;-#,##0; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterTemplate>
                                    <asp:Label ID="lgvFrepchq" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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


