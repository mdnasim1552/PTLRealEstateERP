<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ResGenCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.ResGenCodeBook" %>

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
                            <legend style="color: black; font: 12px; font-weight: bold;">Code Information</legend>
                            <div class="form-horizontal">

                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="lblName lblTxt" Text="Code Book:"></asp:Label>

                                            <asp:DropDownList ID="ddlGenCodeBook" runat="server" Width="350px" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lblGenCode" runat="server" CssClass=" inputtextbox" Visible="False" Width="400px">></asp:Label>

                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="lblGenCode0" runat="server" CssClass=" inputtextbox" Visible="False" Width="125px"></asp:Label>

                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="LblBookName2" runat="server" CssClass="lblName lblTxt" Text="Search Option:"></asp:Label>

                                            <asp:TextBox ID="txtsrch" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-primary srearchBtn" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                         <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid"
                BorderWidth="2px" CellPadding="4" Font-Size="12px"
                OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                OnRowUpdating="grvacc_RowUpdating" PageSize="15" Width="576px">
                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                    Visible="False" />
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
                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                        SelectText="" ShowEditButton="True">
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText=" ">
                        <EditItemTemplate>
                            <asp:Label ID="lbgrcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode1"))+"-" %>'
                                Width="20px"></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblgrcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode1"))+"-" %>'
                                Width="20px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                MaxLength="6"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode2")) %>'
                                Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode2")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>'
                                Width="325px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
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
                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>'
                                Width="325px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Type Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px" MaxLength="100"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gentdesc")) %>'
                                Width="150px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                Style="font-size: 12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gentdesc")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
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
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#003366" Height="16px" 
                                            style="TEXT-ALIGN: right; color: #FFFFFF;" 
                                            Text="Select Code Book:" Width="150px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGenCodeBook" runat="server" BackColor="#68AED0" 
                                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="400px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblGenCode" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" style="margin-bottom:1px" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOthersBookSegment" runat="server" BackColor="#68AED0" 
                                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="130px">
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblGenCode0" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" style="margin-bottom:1px" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="style22" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" onclick="lnkok_Click" Width="43px" 
                                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                            ForeColor="White">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                </tr>--%>

            <%--<tr>
                                    <td class="style30">
                                        &nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="LblBookName2" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#003366" Height="16px" 
                                            style="TEXT-ALIGN: right; color: #FFFFFF;" 
                                            Text="Search Option:" Width="150px"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:TextBox ID="txtsrch" runat="server" BorderColor="Yellow" 
                                            BorderStyle="Solid" BorderWidth="1px" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" Width="16px" 
                                            Visible="False" />
                                    </td>
                                    <td>
                                        <asp:Label ID="ConfirmMessage" runat="server" Font-Italic="False" 
                                            Font-Size="12px"  BackColor="Red" Font-Bold="True" 
                                            ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>--%>

           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

