<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurOrderTermsCon.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurOrderTermsCon" %>

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
                                     <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Type:"></asp:Label>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddltypecod" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="001">Service Terms</asp:ListItem>
                                            <asp:ListItem Value="002">Products Terms</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                        <%--<asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>--%>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15" 
                                 CssClass="table table-striped table-hover table-bordered grvContentarea" Width="700px">
                                
                                <FooterStyle BackColor="#5F9467" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="">
                                       <%-- <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typecod"))%>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typecod")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="3"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmid")) %>'
                                                Width="90px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lbgrcod3" runat="server" Font-Size="12px" Font-Underline="false" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmid")) %>'
                                                Width="90px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Subject">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txttrmsub" runat="server" Font-Size="12px" Height="16px"
                                                
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmsub")) %>'
                                                Width="90px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsubject" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmsub")) %>'
                                                Width="90px"></asp:Label>

                                            <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Code" HeaderStyle-Width="400px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="250"
                                                Style="border-style: none;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmdesc")) %>'
                                                Width="350px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="350px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trmdesc"))  %>'
                                                Width="350px">                                             
                                            
                                            </asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remarks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tlblgvrmrk" runat="server" Font-Size="12px" MaxLength="100"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="90px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

