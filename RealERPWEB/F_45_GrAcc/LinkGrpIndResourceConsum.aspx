<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpIndResourceConsum.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpIndResourceConsum" %>

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
                                            <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" BackColor="White"
                                            Font-Size="12px" ForeColor="Black" Height="16px" Visible="False" Width="300px"></asp:Label>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Material Name"></asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindResource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindResource_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px ">
                                            <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                            </asp:DropDownList>
                                            
                                        </div>

                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click" TabIndex="4">Show</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblItem10" runat="server" CssClass="lblTxt lblName" Text="Floor"></asp:Label>
                                            <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" Width="130px"
                                                TabIndex="2" CssClass="smDropDown inputTxt">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to" Text="Group"></asp:Label>

                                        </div>
                                        <div class="col-md-3 pading5px ">

                                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="smDropDown"
                                                TabIndex="6">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                                TabIndex="2" CssClass="smDropDown inputTxt" Width="70px">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <asp:Label ID="lblRptGroup0" runat="server" CssClass="smLbl_to" Text="Date: "></asp:Label>
                                        <asp:Label ID="lblAsDate" runat="server" CssClass="smLbl_to" Text=""></asp:Label>

                                    </div>


                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>



                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="799px" AllowPaging="True"
                        OnPageIndexChanging="gvRptResBasis_PageIndexChanging">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Floor">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                        Width="300px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvworktQty" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptbgdqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Material Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
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

