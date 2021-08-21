<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MatLedCodeBook.aspx.cs" Inherits="RealERPWEB.F_14_Pro.MatLedCodeBook" %>

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
                                <asp:Panel ID="PanelHeader" runat="server">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-10  pading5px  asitCol10">

                                                <asp:Label ID="LblBookName1" runat="server" CssClass=" lblName lblTxt" Text="Code Book:"></asp:Label>
                                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="ddlPage" Width="400px"></asp:DropDownList>

                                                <asp:Label ID="lbalterofddl" runat="server" CssClass=" inputtextbox" Visible="False" Width="400px"></asp:Label>

                                                <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage" Width="130px">
                                                    <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                    <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                                    <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:Label ID="lbalterofddl0" runat="server" CssClass=" inputtextbox" Visible="False" Width="130px"></asp:Label>

                                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-10  pading5px  asitCol10">

                                                <asp:Label ID="LblBookName2" runat="server" CssClass=" lblName lblTxt" Text="Search Option:"></asp:Label>

                                                <asp:TextBox ID="txtsrch" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                                <asp:LinkButton ID="ibtnSrch" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ibtnSrch_Click" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                <asp:Label ID="lblPage" runat="server" Text="Page Size :" Visible="False" CssClass=" smLbl_to"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass="ddlPage">
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
                                                <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                            </div>
                                        </div>
                                    </asp:Panel>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False"  CellPadding="4" Font-Size="12px" PageSize="15" Width="550px"
                OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True">
                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top" />
                <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Code">

                        <ItemTemplate>
                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Material">

                        <HeaderTemplate>
                            <asp:Label ID="Label8" runat="server" Text="Description of Material" Width="150px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                Width="325px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnUpdateMat" runat="server" OnClick="lbtnUpdateMat_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Lead Time">

                        <ItemTemplate>
                            <asp:TextBox ID="txtLeadTime" runat="server" BorderColor="#99CCFF"
                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                Style="text-align: right; background-color: Transparent"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "leadtime")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
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


            <%--<tr>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True"
                                                Font-Size="12px" ForeColor="#003366" Height="16px"
                                                Style="text-align: right; color: #FFFFFF;"
                                                Text="Select Code Book:" Width="150px"></asp:Label>
                                        </td>
                                        <td class="style28">
                                            <asp:DropDownList ID="ddlOthersBook" runat="server" BackColor="#68AED0"
                                                Font-Bold="True" Font-Size="16px" Style="margin-left: 0px" Width="400px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0"
                                                BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                Font-Size="14px" Style="margin-bottom: 1px" Visible="False" Width="400px"></asp:Label>
                                        </td>
                                        <td class="style29">
                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" BackColor="#68AED0"
                                                Font-Bold="True" Font-Size="16px" Style="margin-left: 0px" Width="130px">
                                                <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                                <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl0" runat="server" BackColor="#68AED0"
                                                BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                Font-Size="14px" Style="margin-bottom: 1px" Visible="False" Width="130px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="style22" Font-Bold="True"
                                                Font-Size="12px" Height="16px" OnClick="lnkok_Click" Width="43px">Ok</asp:LinkButton>
                                        </td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;</td>
                                    </tr>--%>

            <%--<tr>
                                        <td class="style26">&nbsp;</td>
                                        <td class="style24">
                                            <asp:Label ID="LblBookName2" runat="server" BorderStyle="None" Font-Bold="True"
                                                Font-Size="12px" ForeColor="#003366" Height="16px"
                                                Style="text-align: right; color: #FFFFFF;"
                                                Text="Search Option:" Width="150px"></asp:Label>
                                        </td>
                                        <td class="style25">
                                            <asp:TextBox ID="txtsrch" runat="server" BorderColor="Yellow"
                                                BorderStyle="Solid" BorderWidth="1px" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="style30">
                                            <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrch_Click" Width="16px"
                                                Visible="False" />
                                        </td>
                                        <td class="style31">
                                            <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right;" Text="Page Size :" Visible="False"
                                                Width="70px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                Width="80px">
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
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" Font-Bold="True"
                                                Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>--%>

            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


