<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CompCodeBook.aspx.cs" Inherits="RealERPWEB.F_24_CC.CompCodeBook" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlComp" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOtherComp" CssClass="form-control inputTxt" runat="server">
                                            <%--<asp:ListItem Value="2">Main Code</asp:ListItem>--%>
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                           <%-- <asp:ListItem Value="8">Sub Code-2</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                
                                <div class="form-group">
                                    <asp:Label ID="LblBookName2" runat="server" CssClass="col-md-2 control-label lblTxt"  Visible="false" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control inputTxt" Visible="false" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                      <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    </div>
                                    <asp:Label ID="lblPage" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Page Size" Visible="False"></asp:Label>
                                        <div class="col-md-1 pading5px">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                           Visible="False">
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

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvCompCode" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnRowCancelingEdit="gvSalPlnCode_RowCancelingEdit" OnRowEditing="gvSalPlnCode_RowEditing"
                            OnRowUpdating="gvSalPlnCode_RowUpdating" PageSize="10" Width="284px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnPageIndexChanging="gvCompCode_PageIndexChanging" >
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
                        <ItemTemplate>
                            <asp:Label ID="lblgrcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcod"))+"-" %>'
                                Width="15px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                MaxLength="13"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcod3")) %>'
                                Width="90px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcod3")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        <ItemStyle Font-Size="12px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                Width="250px"></asp:TextBox>
                        </EditItemTemplate>
                     <%--   <FooterTemplate>
                            <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                Font-Bold="True" Font-Size="14px"
                                OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid; color:black;"
                                Width="150px">
                            </asp:DropDownList>
                        </FooterTemplate>--%>
                        <ItemTemplate>
                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                Style="font-size: 12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                Width="250px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Type">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvtype" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
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

