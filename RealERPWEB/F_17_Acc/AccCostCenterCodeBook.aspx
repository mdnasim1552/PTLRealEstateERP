<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccCostCenterCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccCostCenterCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="col-md-12">
                        <div class="row">


                            <fieldset class="scheduler-border fieldset_A">


                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCostCodeBook" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>


                                            <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                                <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                                <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="LblBookName2" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                        <div class="col-md-2 pading5px">
                                            <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="ibtnSrch" runat="server"  CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
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


                                   <div class="table-responsive">
                                <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                    OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging"
                                    OnRowDataBound="grvacc_RowDataBound" CssClass="table table-striped table-hover table-bordered grvContentarea" Width="600px">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="TopAndBottom"
                                        Mode="NumericFirstLast" />
                                    <FooterStyle BackColor="#5F9467" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
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
                                        <asp:TemplateField HeaderText=" ">

                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvcode" runat="server" Target="_blank" Font-Underline="false"
                                                    ForeColor="Black" Font-Bold="true"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcod2"))+"-" %>'
                                                    Width="20px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrcode" runat="server"
                                                    MaxLength="13"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcod4")) %>'
                                                    Width="90px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lbgrcod" runat="server" ForeColor="Black" Font-Underline="false" Width="90px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcod4")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvcentername" runat="server" MaxLength="100"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectname")) %>'
                                                    Width="350px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectname")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="10px" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Short Description" HeaderStyle-Width="350px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                    Style="border-style: none;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectdesc")) %>'
                                                    Width="200px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="200px"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                    Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectdesc"))  %>'
                                                    Width="200px">  
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrcod1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcod3")) %>'
                                                    Visible="False"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                     <%--   <asp:TemplateField HeaderText="Unit Code" Visible="false">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlunit" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60">
                                                </asp:DropDownList>


                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:TemplateField>--%>


                                        <%--<asp:TemplateField HeaderText="Std.Rate" >
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                                    Style="border-style: none;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsirval" runat="server" Font-Size="12px"
                                                    Style="font-size: 12px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
--%>


                                       <%-- <asp:TemplateField HeaderText="Type" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="10"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                    Width="60px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                    Width="60px"></asp:Label>

                                                <%--    <asp:Label ID="Label1" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                    Width="60px"></asp:Label>--%>
                                        <%--    </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                                      <%--      <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsircode" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details Description" HeaderStyle-Width="150">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                    Width="150"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                                    Style="font-size: 12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                    Width="150"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrcod1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                                    Visible="False"></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="tlblgvUsr" runat="server" Font-Size="12px"  
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod111" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
  
                                        <%--  <asp:TemplateField HeaderText="Method">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvMethod" runat="server" CssClass="form-control inputTxt" MaxLength="100"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "method")) %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMethod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "method")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>

                                      

                                      <%--  <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkSpcf" runat="server" Target="_blank" Font-Underline="false" Font-Size="11px"
                                                    Width="70px" Text="Specification"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>--%>
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
                    <div class="clearfix"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

