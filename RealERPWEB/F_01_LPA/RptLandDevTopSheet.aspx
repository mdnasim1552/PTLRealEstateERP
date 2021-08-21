<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLandDevTopSheet.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptLandDevTopSheet" %>

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
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="prname" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblDuration" runat="server" CssClass="btn btn-success primaryBtn"></asp:Label></td>
                        </div>
                    </div>
                    <asp:GridView ID="gvFeaPrjLand" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" Width="785px" OnRowDataBound="gvFeaPrjLand_RowDataBound"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnSelectedIndexChanged="gvFeaPrjLand_SelectedIndexChanged">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                           

                            

                            <asp:TemplateField HeaderText="Group Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                        Width="250px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            


                            <asp:TemplateField HeaderText="Land Department Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmtrep01" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmtc001" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Sales Department Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmtrep02" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmtc002" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Management Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmtrep03" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmtc003" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Land Department Profit (%)">

                                <ItemTemplate>
                                    <asp:Label ID="lgsalPr01" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "par1")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Department Profit (%)">

                                <ItemTemplate>
                                    <asp:Label ID="lgsalpar02" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "par2")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Management Profit (%)">

                                <ItemTemplate>
                                    <asp:Label ID="lgsalpar03" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "par3")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Land Department Per Year (%)" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lgsyear1" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "year1")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Department Per Year (%)" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lgsyear2" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "year2")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Management Per Year (%)" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lgsyear3" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                        Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "year3")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
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

