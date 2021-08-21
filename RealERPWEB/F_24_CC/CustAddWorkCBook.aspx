
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustAddWorkCBook.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustAddWorkCBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
  
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                         <asp:Panel ID="Panel1" runat="server">
                                         <div class="form-group">
                                            <div class="col-md-10 pading5px asitCol10">
                                                <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName"  Text="SC BOOK :" ></asp:Label>

                                                 <asp:DropDownList ID="ddlOthersBook" runat="server" Width="350px" CssClass="ddlPage"> </asp:DropDownList>

                                               
                                                      <asp:DropDownList ID="ddlOthersBookSegment" runat="server"  CssClass="ddlPage">
                                                        <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                        <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                                          <asp:ListItem Value="6">Sub-2</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="9">Details Code</asp:ListItem>
                                                    </asp:DropDownList>

                                                 <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                                <asp:Label ID="ConfirmMessage" runat="server"  CssClass="btn btn-danger primaryBtn"></asp:Label>

                                            </div>
                                              
                                            </div>
                                             </asp:Panel>
                                           

                                        </div>
                                       
                                    </fieldset>
                                 </div>
                    <div class="table table-responsive">

                              <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" BorderColor="SteelBlue" 
                            CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" PageSize="15" Width="284px" 
                            ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="#000" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
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
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>' 
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" 
                                            MaxLength="9" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="70px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
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
                                        <asp:TextBox ID="txtgvttpe" runat="server"  Style="border-style:none;"
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
                                  <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtunit" runat="server" Style="border-style:none;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgunit" runat="server"  
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>

                         <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                 </div>
                    </div>
                </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

