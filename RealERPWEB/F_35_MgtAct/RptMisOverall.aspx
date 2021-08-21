<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMisOverall.aspx.cs" Inherits="RealERPWEB.F_35_MgtAct.RptMisOverall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5  pading5px">
                                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName"
                                            Text="Date"></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" Text="Ok"></asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="gvMisOverall" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" PageSize="15" ShowFooter="True" Width="146px">
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
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgv" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvamt01" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="none" Font-Size="11px" Font-Underline="false"
                                        Style="background-color: Transparent; color: Black;" Target="_blank"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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

