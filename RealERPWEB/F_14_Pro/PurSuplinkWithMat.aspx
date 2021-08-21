<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurSuplinkWithMat.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurSuplinkWithMat" %>

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
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="LblBookName2" runat="server" CssClass="lblName lblTxt"
                                            Text="Search Option:"></asp:Label>

                                        <asp:TextBox ID="txtsrch" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblmsg01" runat="server"
                                            Font-Italic="False" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvSupLink" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="True"
                        OnPageIndexChanging="gvSupLink_PageIndexChanging"
                        OnRowCancelingEdit="gvSupLink_RowCancelingEdit" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowEditing="gvSupLink_RowEditing" OnRowUpdating="gvSupLink_RowUpdating">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField Visible="False">

                                <ItemTemplate>
                                    <asp:Label ID="lbgvsupcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                        Width="20px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Supplier Name">


                                <ItemTemplate>
                                    <asp:Label ID="lblgvsupname" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True">
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                <ItemStyle Font-Bold="True" Font-Size="11px" ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Work">
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                        BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:TextBox ID="txtSerachmwgdesc" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                </td>
                                                <td class="style59">
                                                    <asp:ImageButton ID="ibtnSrchregis" runat="server" Height="16px"
                                                        ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchregis_Click" TabIndex="5"
                                                        Width="16px" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmwgdesc" runat="server" Width="200px" TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmwgdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mwgdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Materials">
                                <EditItemTemplate>
                                    <asp:Panel ID="Panelmat" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                        BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:TextBox ID="txtSerachmwgmdesc" runat="server" BorderStyle="Solid"
                                                        BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                </td>
                                                <td class="style59">
                                                    <asp:ImageButton ID="ibtnSrchmwgm" runat="server" Height="16px"
                                                        ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchmwgm_Click" TabIndex="5"
                                                        Width="16px" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmwgmdesc" runat="server" Width="200px" TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmwgdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="MWGCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmwgcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mwgcod")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="MWGCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmwgmcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgcod")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
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

