<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryRegCodeBook.aspx.cs" Inherits="RealERPWEB.F_24_CC.EntryRegCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="lnknewentry" runat="server" Font-Bold="True"
                                        Font-Size="12px" Style="height: 16px"
                                        Visible="False" Width="120px" CssClass="style15">New Entry</asp:LinkButton>

                                    <div class="clearfix"></div>
                                </div>
                                <%--<div class="form-group">
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" Font-Size="12px" OnRowCancelingEdit="grvacc_OnRowCancelingEdit"
                        OnRowEditing="grvacc_OnRowEditing" OnRowUpdating="grvacc_OnRowUpdating"
                        PageSize="50" Width="284px"
                        ShowFooter="True">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                        <FooterStyle Font-Bold="True" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True">
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                            </asp:CommandField>

                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regcod2"))+"-" %>'
                                        Width="30px"></asp:Label>

                                    <%--<asp:TextBox ID="lblgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="5"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regcod2")) %>'
                                        Width="30px"></asp:TextBox>--%>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="5"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regcod")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regcod")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="14px"
                                        OnSelectedIndexChanged="ddlPageNo_OnSelectedIndexChanged"
                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                        Width="150px">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regval")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtype" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "regval")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <AlternatingRowStyle BackColor="" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

