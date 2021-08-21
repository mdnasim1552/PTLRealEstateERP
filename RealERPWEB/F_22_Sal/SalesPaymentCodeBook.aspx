<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesPaymentCodeBook.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesPaymentCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                        <asp:DropDownList ID="ddlSalPayment" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
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

                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvPaySch" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        Font-Size="12px" OnRowCancelingEdit="gvPaySch_RowCancelingEdit" OnRowEditing="gvPaySch_RowEditing"
                        OnRowUpdating="gvPaySch_RowUpdating" PageSize="15" Width="284px" ShowFooter="True" OnPageIndexChanging="gvPaySch_PageIndexChanging">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                        <FooterStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                ShowEditButton="True">
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" MaxLength="6"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                              
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDescgdescbn" runat="server" Font-Size="12px" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                             
                                <ItemTemplate>
                                    <asp:Label ID="lbldescgdescbn" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
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





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
