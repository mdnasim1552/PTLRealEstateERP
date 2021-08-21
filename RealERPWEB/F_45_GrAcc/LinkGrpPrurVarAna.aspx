
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpPrurVarAna.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpPrurVarAna" %>

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
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImgbtnFindImpNo" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindImpNo_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                            BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="70px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="400">400</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to" Text="Group: "></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="smDropDown" Width="100px"
                                            TabIndex="6">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbldate" runat="server" CssClass="smLbl_to" Text="Date: "></asp:Label>
                                        <asp:Label ID="lblAsDate" runat="server" CssClass="smLbl_to" Text=""></asp:Label>


                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>

                                </div>
                            </div>

                        </fieldset>
                    </div>



                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True"
                        Width="919px" AllowPaging="True"
                        OnPageIndexChanging="gvRptResBasis_PageIndexChanging">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty2" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty4" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bud.Cons">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty5" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Bud. Stock">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdstkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phy. Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty3" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Variance">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty6" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
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

