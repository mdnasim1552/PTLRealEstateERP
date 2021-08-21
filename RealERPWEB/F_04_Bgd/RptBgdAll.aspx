<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptBgdAll.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptBgdAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

          
        
        });


        function pageLoaded()
        {

            try {



                $("#<%=this.gvMatStock.ClientID%>").tblScrollable();
               
            }

            catch (e) {

                alert(e.message);
            }

        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="Server">

        <ContentTemplate>

                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">

                                <div class=" col-md-1">
                                    <div class="form-group">
                                                <label class="control-label lblmargin-top9px" for="FromDate" id="lbltoDate" runat="server">Date</label>

                                    </div>


                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnok" runat="server" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . ."
                                                    Width="150px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>
                                </div>
                            </div>
                   
                            <div class="row">
                                <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMatStock_RowDataBound"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">
                                            <ItemTemplate>


                                                <asp:Label ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px">
                                                </asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <asp:Label runat="server" ID="lblTotal">Total</asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Revenue</br>Budget">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lrvbgd" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lrvbgdf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Construction</br>Budget">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lconbgd" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbgdamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lconbgdf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="General</br>Budget">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvgbdg" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gbgd")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lgvgbdgf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total</br>Budget">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvtbdg" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbgdamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lgvtbdgf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin %">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvmargin" runat="server" Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "margin")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Time Plan">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvtpamt" runat="server" Text="Link" Target="_blank"
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <%-- Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpamt")).ToString("#,##0.00;(#,##0.00); ")%>'--%>
                                            <%--  <FooterTemplate>

                                                <asp:Label runat="server" ID="lgvtpamtf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cash Flow">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvcfamt" runat="server" Text="Link" Target="_blank"
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <%-- Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfamt")).ToString("#,##0.00;(#,##0.00); ")%>'--%>
                                            <%--<FooterTemplate>

                                                <asp:Label runat="server" ID="lgvcfamtf"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

