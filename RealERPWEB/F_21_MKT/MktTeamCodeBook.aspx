<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktTeamCodeBook.aspx.cs" Inherits="RealERPWEB.F_21_MKT.MktTeamCodeBook" %>

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



                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage136 inputTxt"
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
                                    <div class="col-md-4 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">

                            <div class="row">


                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control inputTxt">
                                                </asp:DropDownList>


                                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                            </div>
                                            <div class="col-md-2 pading5px">
                                                <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control inputTxt" runat="server">
                                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                    <asp:ListItem Value="6">Sub Code-2</asp:ListItem>
                                                    <asp:ListItem Value="8">Sub Code-3</asp:ListItem>
                                                    <asp:ListItem Value="10">Sub Code-4</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="14">Details Code</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>


                                    </div>
                                </fieldset>
                            </div>


                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" Font-Size="12px"
                                OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15" ShowFooter="True"
                                Width="287px">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                    Visible="False" />
                                <FooterStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" ForeColor="Black"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode2"))+"-" %>' Width="25px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode2"))+"-" %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Team Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="16"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode4")) %>'
                                                Width="100px"></asp:TextBox>
                                            <asp:Label ID="lbgrcod1" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode4")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'
                                                Width="350px"></asp:TextBox>
                                        </EditItemTemplate>
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" ForeColor="Black" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvdesignatin" runat="server" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designtion")) %>'
                                                Width="120px"></asp:TextBox>
                                        </EditItemTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lbldesignatin" runat="server" ForeColor="Black" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designtion")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <EditItemTemplate>

                                            <table style="width: 100%;">
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlClientName" runat="server" Width="150px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clientdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Id">
                                        <EditItemTemplate>

                                            <table style="width: 100%;">
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlUserId" runat="server" Width="80px" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUsrid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewLetterInfo" runat="server">

                            <asp:GridView ID="grvletterinfo" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" Font-Size="12px"
                                OnRowCancelingEdit="grvletterinfo_RowCancelingEdit"
                                OnRowEditing="grvletterinfo_RowEditing"
                                OnRowUpdating="grvletterinfo_RowUpdating" PageSize="15" ShowFooter="True"
                                Width="915px">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                    Visible="False" />
                                <FooterStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" ForeColor="Black"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrletcode" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrletcode3" runat="server" Height="16px" MaxLength="6"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode3")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrketcode3" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode3")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDescletter" runat="server" Height="280px"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                TextMode="MultiLine" Width="750px"></asp:TextBox>
                                        </EditItemTemplate>
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDescletter" runat="server" ForeColor="Black"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                Width="750px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewSalLetterInfo" runat="server">

                            <asp:GridView ID="grvSalletterinfo" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False"
                                OnRowCancelingEdit="grvSalletterinfo_RowCancelingEdit"
                                OnRowEditing="grvSalletterinfo_RowEditing"
                                OnRowUpdating="grvSalletterinfo_RowUpdating" PageSize="15" ShowFooter="True"
                                Width="915px" OnPageIndexChanging="grvSalletterinfo_PageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                                <FooterStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid1" runat="server" ForeColor="Black"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrletcodesal" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrletcodesal" runat="server" Height="16px" MaxLength="6"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode3")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrketcode4" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode3")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesclettersal" runat="server" Height="280px"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                TextMode="MultiLine" Width="750px"></asp:TextBox>
                                        </EditItemTemplate>
                                     
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDesclettersal" runat="server" ForeColor="Black"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                Width="750px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


