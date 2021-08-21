<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdAnalinkWithWork.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdAnalinkWithWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol3" style="width: 220px">

                                            <asp:Label ID="LblBookName2" runat="server" CssClass="lblTxt lblName" Width="120px">Search Option</asp:Label>

                                            <asp:TextBox ID="txtsrch" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnSrch" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrch_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblPage" runat="server" Text="Page Size :" CssClass="smLbl_to"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblmsg01" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>

                    <div class="table table-responsive">
                        <asp:GridView ID="gvAnaLink" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" AllowPaging="true" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvAnaLink_PageIndexChanging"
                            OnRowCancelingEdit="gvAnaLink_RowCancelingEdit"
                            OnRowEditing="gvAnaLink_RowEditing" OnRowUpdating="gvAnaLink_RowUpdating">
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
                                        <asp:Label ID="lbgvsircode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Name">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvworkname" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
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
                                <asp:TemplateField HeaderText="Analysis">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:TextBox ID="txtSerachwrkdesc" runat="server" BorderStyle="Solid"
                                                            BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td class="style59">
                                                        <asp:ImageButton ID="ibtnSrchregis" runat="server" Height="16px"
                                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchregis_Click" TabIndex="5"
                                                            Width="16px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlwrkdesc" runat="server" Width="150px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvwrkdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MWGCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvwrkcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkcode")) %>'
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
            </div>

            <table style="width: 100%;">
                <tr>
                    <td colspan="12">

                        <%--<asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                    BorderWidth="1px">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="style26">
                                                &nbsp;</td>
                                            <td class="style24">
                                                <asp:Label ID="LblBookName2" runat="server" BorderStyle="None" Font-Bold="True" 
                                                    Font-Size="12px" ForeColor="#003366" Height="16px" 
                                                    style=" color: #FFFFFF;" Text="Search Option:" Width="82px"></asp:Label>
                                            </td>
                                            <td class="style25">
                                                <asp:TextBox ID="txtsrch" runat="server" BorderColor="Yellow" 
                                                    BorderStyle="Solid" BorderWidth="1px" Width="100px"></asp:TextBox>
                                            </td>
                                            <td class="style30">
                                                <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                                                    ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" 
                                                    Width="16px" />
                                            </td>
                                            <td class="style31">
                                                
                                            </td>
                                            <td>
                                               
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Label ID="lblmsg01" runat="server" BackColor="Red" Font-Bold="True" 
                                                    Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                              
                    </td>
                </tr>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table>

                <tr>

                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

