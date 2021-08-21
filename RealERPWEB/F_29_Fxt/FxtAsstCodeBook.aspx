<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FxtAsstCodeBook.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.FxtAsstCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPartSmall row">

                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="form-group">
                                    <div class="col-md-8  pading5px  asitCol8">
                                        <asp:Label ID="LblBookName1" runat="server" CssClass=" lblName lblTxt" Text="CODE BOOK :"></asp:Label>

                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="ddlPage" Width="350"> </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl" runat="server" CssClass="inputtextbox" Visible="false" Style="width:350px;"></asp:Label>

                                        <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl0" runat="server" CssClass="inputtextbox" Visible="false" Width="80"></asp:Label>

                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                        <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-8  pading5px  asitCol8">

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False"  CellPadding="4"  OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing" PageSize="10"
                OnRowUpdating="grvacc_RowUpdating" OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True">
                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                    Visible="False" />
                <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                        SelectText="" ShowEditButton="True">
                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <asp:Label ID="lblgrcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgcod2"))+"-" %>'
                                Width="15px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" 
                                MaxLength="13"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgcod3")) %>'
                                Width="90px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgcod3")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgdesc")) %>'
                                Width="250px"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                Font-Bold="True" Font-Size="14px"
                                OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid; color:black;"
                                Width="150px">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                Style="font-size: 12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgdesc")) %>'
                                Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Left" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod1" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgcod")) %>'
                                Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgval")) %>'
                                Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvtype" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fxtgval")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
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




            <%--<tr>
                    <td style="margin-left: 40px" class="style24">
                        &nbsp;</td>
                    <td class="style25">
                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="#003366" Height="12px" 
                            style="FONT-SIZE: 12px; color: #FFFFFF;" Text="SELECT CODE BOOK :" 
                            Width="134px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOthersBook" runat="server" Font-Bold="True" 
                            Font-Size="12px" style="margin-left: 0px; text-align: left;" Width="350px">
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl" runat="server" BackColor="White" Font-Bold="True" 
                            Font-Size="14px" 
                            style="margin-bottom:1px; margin-right: 0px; text-align: left;" Visible="False" 
                            Width="350px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOthersBookSegment" runat="server" Font-Bold="True" 
                            Font-Size="12px" style="margin-left: 0px" Width="129px">
                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl0" runat="server" BackColor="#68AED0" 
                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" style="margin-bottom:1px" Visible="False" Width="129px"></asp:Label>
                    </td>
                    <td class="style19">
                        <asp:LinkButton ID="lnkok" runat="server" BackColor="#003366" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lnkok_Click" 
                            style="text-align: center" Width="50px">Ok</asp:LinkButton>
                    </td>
                    <td class="style19">
                        &nbsp;</td>
                    <td class="style19">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style50">
                        
                            &nbsp;</td>
                    <td class="style48">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
            <%--<tr>
                     <td class="style24" style="margin-left: 40px">
                         &nbsp;</td>
                     <td class="style25">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" 
                             Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                     </td>
                     <td class="style19">
                         &nbsp;</td>
                     <td class="style19">
                         &nbsp;</td>
                     <td class="style19">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td class="style50">
                         &nbsp;</td>
                     <td class="style48">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>--%>

         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

