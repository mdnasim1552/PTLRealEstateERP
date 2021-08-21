<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptBgdvsExpensDetails.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkRptBgdvsExpensDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

           <%-- var gv1 = $('#<%=this.gvBgdvsExpense.ClientID %>');
            gv1.Scrollable();--%>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    
                                </div>
                                <%-- <div class="form-group" visible="false" >
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

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
                                           <asp:ListItem Value="300">500</asp:ListItem>
                                            <asp:ListItem Value="300">700</asp:ListItem>
                                            <asp:ListItem Value="300">900</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                    
                                    
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>


                    <asp:GridView ID="gvBgdvsExpense" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnRowDataBound="gvBgdvsExpense_RowDataBound"
                        ShowFooter="true">

                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="vounum" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvmonthid" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))+"</b>" %>'
                                        Width="100px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Resource">
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Resource" Width="130px"></asp:Label>
                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFresource" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvresource" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField>
                                

                                <FooterTemplate>
                                    <asp:Label ID="lgvFvounum1" runat="server" Font-Bold="True"> Total :</asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvactDesc" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1"))+"</b>" %>'
                                        Width="100px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                             

                            <asp:TemplateField HeaderText="qty">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt" runat="server" Font-Bold="True" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
