<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkPayAcc.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.LinkPayAcc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewBankPos02" runat="server">
                    <div class="container moduleItemWrpper">
                        <div class="contentPart">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblasdateb" runat="server" CssClass="lblTxt lblName" Text="As On Date"></asp:Label>
                                                <asp:TextBox ID="lblAsDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <asp:Label ID="lblreportlevelbk" runat="server" CssClass="lblTxt lblName"
                                                    Text="Report Level :"></asp:Label>
                                            </div>

                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:DropDownList ID="ddlReportLevelbk02" runat="server" CssClass="ddlPage">
                                                    <asp:ListItem Value="2">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="4">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="8">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12">Level-4</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-sm-4 pull-right">
                                                <asp:UpdateProgress ID="UpdateProgresscon0" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="lblcon0" runat="server" ForeColor="#FF3300"
                                                            Style="color: #FFFF66" Text="Please wait......" Width="144px"></asp:Label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                        </div>

                                    </div>
                                </fieldset>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvBankPosition02" runat="server" AutoGenerateColumns="False"
                                     CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodebank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                                                Text="Description of Accounts"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbnkpdataExel02" runat="server" CssClass="btn btn-warning primaryBtn" >Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbank02" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="400px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobalbank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcloliabilities02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Issue Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Collection Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcollamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Net Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetbal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Bank Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbankliabilities" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "banklia")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                    </div>



                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
