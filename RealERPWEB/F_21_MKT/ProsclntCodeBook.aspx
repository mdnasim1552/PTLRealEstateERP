<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProsclntCodeBook.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ProsclntCodeBook" %>

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

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                        <%--                                                <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server"
                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                                </cc1:ListSearchExtender>--%>

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
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <asp:Panel ID="PanelSearch" runat="server" BorderStyle="None" Visible="False">
                                    <div class="form-group">

                                        <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label lblTxt" Text="District:"></asp:Label>

                                        <div class="col-md-1 pading5px">
                                            <asp:DropDownList ID="ddlDistName" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlDistName_SelectedIndexChanged">
                                            </asp:DropDownList>


                                        </div>
                                        <div class="col-md-1 pading5px">

                                            <asp:Label ID="Label4" runat="server" CssClass="control-label lblTxt" Text="Catagory:"></asp:Label>
                                           
                                            
                                        </div>
                                         <div class="col-md-1 pading5px">
                                            <asp:DropDownList ID="ddlCatagory" CssClass="form-control inputTxt" runat="server">
                                            </asp:DropDownList>
                                                </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                       
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
                                                <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>





                                    
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>


                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" Font-Size="12px"
                        OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                        OnRowUpdating="grvacc_RowUpdating" PageSize="15" CssClass="table table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="639px" OnPageIndexChanging="grvacc_PageIndexChanging" >
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                            />
                        <FooterStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
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
                            <asp:TemplateField HeaderText=" ">
                                <EditItemTemplate>
                                    <asp:Label ID="lbgrcode" runat="server" Width="50px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod2"))+"-" %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Width="50px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod2"))+"-" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="16"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod4")) %>'
                                        Width="95px"></asp:TextBox>
                                    <asp:Label ID="lbgrcod1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod3")) %>'
                                        Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod4")) %>'
                                        Width="95px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvprosdesc" runat="server" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvaddress" runat="server"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'
                                        Width="150px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvaddress" runat="server" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Phone">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvphone" runat="server"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                        Width="80px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvphone" runat="server" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvemail" runat="server"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                        Width="150px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvemail" runat="server" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Client Status">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkactive" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>'
                                        Width="20px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle />
                        <EditRowStyle />
                        <SelectedRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <AlternatingRowStyle BackColor="" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
