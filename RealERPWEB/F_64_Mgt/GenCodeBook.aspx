<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GenCodeBook.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.GenCodeBook" %>

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

                                    <asp:Label ID="LblBookName1" runat="server" Visible="false" CssClass="lblTxt lblName" Text="Select Group"></asp:Label>

                                   
                                        <asp:DropDownList ID="ddlOthersBook" Visible="false" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>


                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                   
                                    <div class="col-md-7 pading5px">
                                    <asp:Label ID="Label3" runat="server"   CssClass="lblTxt lblName" Text="Select Type"></asp:Label>

                                        <asp:DropDownList ID="ddlOthersBookSegment" OnSelectedIndexChanged="ddlOthersBookSegment_SelectedIndexChanged" AutoPostBack="true" CssClass=" ddlPage inputTxt" runat="server">
                                           
<%--                                            <asp:ListItem Value="7">Employes</asp:ListItem>
                                            <asp:ListItem Value="9">Client Group</asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>



                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass=" ddlPage inputTxt"></asp:Label>
                                         <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Select Details"></asp:Label>
                                         <asp:DropDownList ID="ddlemplist" runat="server" CssClass=" ddlPage inputTxt" Width="280px">
                                        </asp:DropDownList>
                                       

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

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                   
                                   
                                        <div class="col-md-4 pading5px">

                                       

                                        <%--                                                <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server"
                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                                </cc1:ListSearchExtender>--%>

                                        <asp:Label ID="Label1" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblBookName2" Visible="false" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtsrch" Visible="false" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="ibtnSrch"  runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    </div>
                                  
                                   

                                </div>

                            </div>
                        </fieldset>
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging"
                                OnRowDataBound="grvacc_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea" >
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
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
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="13"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="90px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>



                                            <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                Width="250px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirunit" runat="server" MaxLength="100"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std.Rate" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
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
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
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
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px" MaxLength="20"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="User Name">
                                        <EditItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">


                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchUserName" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="86px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchUse" runat="server" OnClick="ibtnSrchProject_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUserName" runat="server" CssClass="ddlPage" Width="200px" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvempname1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname1")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                     <asp:TemplateField HeaderText="Team">
                                        <EditItemTemplate>
                                            <asp:Panel ID="pnlTeam" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">


                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchteam" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchteam" runat="server" OnClick="ibtnSrchteam_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlteam" runat="server" CssClass="ddlPage" Width="130px" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                   
                                    

                                </Columns>


                                <RowStyle />
                                <EditRowStyle />
                                <SelectedRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <AlternatingRowStyle BackColor="" />
                            </asp:GridView>
                            <div class="table-responsive">
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

