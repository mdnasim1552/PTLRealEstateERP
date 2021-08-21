
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFeasiVsActualAll.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptFeasiVsActualAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    --%>
    <%-- <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {

           var gv1 = $('#<%=this.gvcustdues.ClientID %>');
           gv1.Scrollable();
       }
       </script>
    --%>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


                });
                function pageLoaded() {

                    var gv1 = $('#<%=this.grvFeaVsAct.ClientID %>');
                    gv1.Scrollable();




                }

            </script>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage0" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="grvFeaVsAct" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="grvFeallAnly_PageIndexChanging"
                            ShowFooter="True"
                            OnRowDataBound="grvFeaVsAct_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total:  "
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Sft (Feasibility)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSFea" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "feasize")).ToString("#,##;(#,##); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFSFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text=""
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Sft(Actual)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSAct" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actsize")).ToString("#,##;(#,##); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFSATotal" runat="server" Font-Bold="True" Font-Size="12px" Text=""
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDiffFea" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "differ")).ToString("#,##;(#,##); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="% On Actual">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvParFea" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
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
            </div>



            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


