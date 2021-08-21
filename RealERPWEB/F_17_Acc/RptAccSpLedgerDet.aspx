<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccSpLedgerDet.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccSpLedgerDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    
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
                            <div class="form-horizontal text-center ">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt " Text="Ledger"></asp:Label>
                                    </div>
                                <div class="form-group">
                                     <asp:Label ID="LblAcchead" runat="server" CssClass="lblTxt" Text="Accounts Schedule for ...."></asp:Label>
                                </div>
                                  
                                   <div class="form-group">
                                     <asp:Label ID="LblSchReportPeriod" runat="server" CssClass="lblTxt" Text="Reporting Period"></asp:Label>
                                </div</div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvSpledger" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True"
                            Width="902px" OnRowDataBound="gvSpledger_RowDataBound"  CssClass="table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:TemplateField HeaderText="Group Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrp" runat="server" CssClass="GridLebelL"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" CssClass="GridLebelL"
                                            Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="85px"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOpAmount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                            Width="100px" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                            Width="100px" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                            Width="100px" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClAmount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                            Width="100px" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChqdat" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                             <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>




            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
