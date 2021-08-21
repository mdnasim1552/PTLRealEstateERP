<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkTasAccDetAnalysis.aspx.cs" Inherits="RealERPWEB.F_07_Ten.LinkTasAccDetAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });

            var gv = $('#<%=this.gvDetailsAnalysis.ClientID%>');
            gv.Scrollable();

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A" id="details" runat="server">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="lblproj" runat="server" CssClass=" lblTxt  lblName" TabIndex="1" Text="Project Name:" Width="100px"></asp:Label>
                                <asp:Label ID="lblProject" runat="server" Text="Label" CssClass="inputlblVal"
                                    Font-Size="12px" Width="300px"></asp:Label>
                            </div>

                        </div>

                    </fieldset>

                </div>

                <div class="row">
                    <asp:GridView ID="gvDetailsAnalysis" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <PagerSettings Position="TopAndBottom" />
                        <RowStyle Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcWDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                        Width="230px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="False" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Floor Desc.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvflrdesc" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                        Width="35px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sch.Sl">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblgvitemslno" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmslno")) %>'
                                        Width="50px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch.Item No.">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblgvschitemno" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmschno")) %>'
                                        Width="50px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Blue" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvschqty" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sch.Rate">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvschrate" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Sch. Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lgvschamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFschamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="right" />
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


