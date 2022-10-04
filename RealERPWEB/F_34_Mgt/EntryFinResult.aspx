<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryFinResult.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.EntryFinResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="card mt-4 pb-4">
        <div class="card-body">
            <div class="row">
                <div class="col-md-5 pading5px asitCol5">

                    <asp:LinkButton ID="lbtnYearbgd" runat="server" CssClass="btn btn-sm btn-primary"
                        OnClick="lbtnYearbgd_Click">Ok</asp:LinkButton>

                    <asp:Label ID="lblmsg02" runat="server"
                        CssClass="form-label"></asp:Label>

                </div>
            </div>
        </div>
    </div>
    <div class="card" style="min-height:480px;">
        <div class="card-body">
            <div class="row">
                <asp:GridView ID="gvFinResult" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    ShowFooter="True" OnRowDataBound="gvFinResult_RowDataBound">
                    <RowStyle />
                    <Columns>

                        <asp:TemplateField HeaderText="Sl.">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="Black" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNomon" runat="server" Font-Bold="True"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=" Description">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                    Font-Size="12px" ForeColor="Black" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>

                                <asp:HyperLink ID="HygvResDesc" runat="server" Font-Underline="false"
                                    ForeColor="Black" Target="_blank"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                    Width="280px"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="amt1">
                            <FooterTemplate>
                                <%-- <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Black" style="text-align: right" Width="80px"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt1" runat="server" BackColor="Transparent"
                                    Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="90px" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="amt2">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt2" runat="server" BackColor="Transparent"
                                    Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="90px" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Black" style="text-align: right" Width="80px"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt3">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt3" runat="server" BackColor="Transparent"
                                    Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="90px" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Black" style="text-align: right" Width="80px"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt4">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt4" runat="server" BackColor="Transparent"
                                    Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="90px" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>

                            <FooterTemplate>
                                <%--<asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Black" style="text-align: right" Width="80px"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amt5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt5" runat="server" BackColor="Transparent"
                                    Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="90px" BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Black" style="text-align: right" Width="80px"></asp:Label>--%>
                            </FooterTemplate>
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
    </div>






</asp:Content>


