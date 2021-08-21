<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProGenCode.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.ProGenCode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   
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
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>

                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
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
                                   <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False"  
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" PageSize="15" 
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" >
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    
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
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>' 
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"  BorderStyle="none" BackColor="Transparent"
                                            MaxLength="5" 
                                           
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"   BorderStyle="none" BackColor="Transparent"
                                            
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="14px" 
                                            onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                            style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server"  BorderStyle="none" BackColor="Transparent"
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

                                 <asp:TemplateField HeaderText="Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvrate" runat="server"  BorderStyle="none"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="50px" style="text-align: right"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrate" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="50px" style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                            </Columns>

                            <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                            </div>
                        </fieldset>


                        </div>
                      </div>
                    
               <!-- end of Content Part-->

            </div>
            <!-- end of container Part-->

            



            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

