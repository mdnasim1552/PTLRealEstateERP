<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProSeriCode.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ProSeriCode" %>

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



                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName" Text="Select Code"></asp:Label>
                                    </div>



                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>

                                            <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                        </div>

                                        <div class="msgHandSt pull-right">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>



                                    </div>

                                </div>



                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" Width="572px" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging">

                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True"></asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catcode2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server"
                                            MaxLength="6"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catcode3")) %>'
                                            Width="40px" BorderStyle="none"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catcode3")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100" BorderStyle="none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc")) %>'
                                            Width="150px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catcode")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Catagory Head">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtteamhead" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cathdesc")) %>'
                                            Width="150px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblteamhead" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cathdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                    </div>

                    <!-- end of Content Part-->

                </div>
                <!-- end of container Part-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



