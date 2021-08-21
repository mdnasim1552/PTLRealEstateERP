<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPayRecCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPayRecCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblName lblTxt" Text="Code Book:"></asp:Label>

                                            <asp:DropDownList ID="ddlPRecBook" runat="server" Width="300px" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lbalterofddl" runat="server" CssClass=" inputtextbox" Visible="False" Width="300px">></asp:Label>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblPage" runat="server" CssClass="lblName lblTxt" Text="Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="85px">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                            <asp:GridView ID="grvPayRecCod" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" OnPageIndexChanging="grvPayRecCod_PageIndexChanging"
                                OnRowCancelingEdit="grvPayRecCod_RowCancelingEdit" OnRowEditing="grvPayRecCod_RowEditing"
                                OnRowUpdating="grvPayRecCod_RowUpdating" AllowPaging="True">

                                <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prcode2"))+"-" %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="4"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prcode3")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prcode3")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prcode")) %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




