<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.AccCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    
    



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                       <div class="col-md-2 pading5px">
                                        <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName160" Text="Select Code Book:"></asp:Label>
                                            </div>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                                Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                            </cc1:ListSearchExtender>

                                            <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control inputTxt" runat="server">
                                                <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>

                                            <asp:LinkButton ID="lnkcancel" runat="server" Text="Change" Visible="False" OnClick="lnkcancel_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>

                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <div class="msgHandSt">
                                                <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <%--<div class="form-group">
                                    <asp:Label ID="LblBookName2" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    </div>
                                    <asp:Label ID="lblPage" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Page Size" Visible="False"></asp:Label>
                                    <div class="col-md-1 pading5px">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>--%>
                                </div>
                            </div>
                        </fieldset>


                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False"
                            BorderWidth="2px" CellPadding="4"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" Width="761px" PageSize="15">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                Visible="False" />
                            <FooterStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Code(Main)">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>' with="60px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>' with="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="12"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                            Width="90px"></asp:TextBox>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                            Visible="False"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Accounts">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 44%;">
                                            <tr>
                                                <td class="style63">
                                                    <asp:Label ID="Label8" runat="server" Text="Head of Accounts" Width="160px"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="style61">
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                                        Font-Bold="True" Font-Size="14px"
                                                        OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridlevel" runat="server" 
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="40px" Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeCode" runat="server" Font-Size="12px" MaxLength="20"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

