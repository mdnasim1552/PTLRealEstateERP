
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkConstruProgress.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkConstruProgress" %>

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
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
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
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="Date: "></asp:Label>

                                        <asp:Label ID="lblAsDate" runat="server" CssClass="smLbl_to" Text="A. Sales"></asp:Label>

                                    </div>

                                </div>
                            </div>

                        </fieldset>
                    </div>

                    <asp:GridView ID="gvConPro" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" ShowFooter="True" Width="378px" PageSize="20"
                        OnPageIndexChanging="gvConPro_PageIndexChanging" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvConPro_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Floor Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvflrCode" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Floor Description">
                                <FooterTemplate>

                                    <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="50px" Text="Total"></asp:Label>



                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Bold="true" Font-Underline="false" Target="_blank" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                        Width="150px"></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work % ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvWorkP" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perwork")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFWorkP" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Master Plan Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvMasPlan" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFMasPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Master Plan As Of Today">
                                <ItemTemplate>
                                    <asp:Label ID="lgvMPlanastoday" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFMPlanastoday" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Execution Amt.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvExAmt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Less Execution">
                                <ItemTemplate>
                                    <asp:Label ID="lgvlessExAmt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:HyperLink ID="hlnkgvFlessexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Blue" Target="_blank" Style="text-align: right" Width="70px"></asp:HyperLink>
                                </FooterTemplate>


                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Execution % on M.P As Of Today">
                                <ItemTemplate>
                                    <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ")%>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>

                                    <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>


                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                    <asp:Panel ID="Pnlnote" runat="server" Visible="False">
                        <div class="form-group">


                            <div class="col-sm-6 pading5px">
                                <asp:Label ID="lbtnBankPos" runat="server" CssClass="btn btn-success primaryBtn">Note</asp:Label>
                                <div class="clearfix"></div>
                                <div class="form-group">
                                    <asp:Label ID="lblCl" runat="server" CssClass=" smLbl_to" Width="200px" Text="Budgeted Execution in %"></asp:Label>
                                    <asp:Label ID="lPercentonbgd" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lbllssue" runat="server" CssClass="smLbl_to" Width="200px"  Text="Actual Execution in %"></asp:Label>
                                    <asp:Label ID="lPercentonbgdexe" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>
                                    <div class="clearfix"></div>
                                </div>

                            </div>


                        </div>
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


