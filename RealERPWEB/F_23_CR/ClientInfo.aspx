<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientInfo.aspx.cs" Inherits="RealERPWEB.F_23_CR.ClientInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">


                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label12" runat="server" CssClass="btn btn-success primaryBtn" Style="margin-right: 20px;" Text="Field Information:"></asp:Label>
                                        <asp:CheckBox ID="chkallInfoList" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px" OnCheckedChanged="chkallTransList_CheckedChanged" Text="Check All" />


                                    </div>
                                    <div class="col-md-12">
                                        <asp:CheckBoxList ID="cblInfoList" runat="server" AutoPostBack="True"
                                            CellPadding="2" CssClass="rbtnList1 chkBoxControl margin5px"
                                            Width="100%"
                                            ForeColor="#000" Height="12px"
                                            OnSelectedIndexChanged="cblTransList_SelectedIndexChanged" RepeatColumns="10"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>aa</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>cc</asp:ListItem>
                                            <asp:ListItem>dd</asp:ListItem>
                                            <asp:ListItem>ee</asp:ListItem>
                                            <asp:ListItem>ff</asp:ListItem>
                                            <asp:ListItem>gg</asp:ListItem>
                                            <asp:ListItem>hh</asp:ListItem>

                                        </asp:CheckBoxList>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="row">
                                <div class=" form-group">
                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-4 pading5px asitCol4">

                                                    <asp:Label ID="lblSearchlist" runat="server" CssClass="lblTxt lblName"
                                                        Text="Search List"></asp:Label>

                                                    <asp:DropDownList ID="ddlFieldList1" runat="server" Width="152px" CssClass=" ddlPage inputTxt" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                    <div class="clearfix"></div>
                                                    <div class="form-group">

                                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName"
                                                            Text=""></asp:Label>

                                                        <asp:DropDownList ID="ddlFieldList2" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                                        </asp:DropDownList>


                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName"
                                                            Text=""></asp:Label>
                                                        <asp:DropDownList ID="ddlFieldList3" runat="server" Width="152px" CssClass=" ddlPage inputTxt">
                                                        </asp:DropDownList>


                                                    </div>



                                                </div>

                                                <div class="col-md-1 pading5px">
                                                    <div>
                                                        <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="form-group">

                                                        <asp:DropDownList ID="ddlSrch2" runat="server"  Width="90px" CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px" AutoPostBack="True"
                                                             CssClass=" ddlistPull">
                                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>


                                                <div class="col-md-2 pading5px asitCol2">
                                                    <div class="form-group">
                                                    <asp:Label ID="lbland1" runat="server" CssClass="lblTxt lblName" Text="And" Visible="False"
                                                        Width="25px"></asp:Label>


                                                    <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                       
                                                   <%-- <asp:DropDownList ID="ddltodesig1" runat="server" CssClass=" ddlPage62 inputTxt">
                                                    </asp:DropDownList>--%>

                                                    <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="ddlPage62 inputTxt">
                                                        <asp:ListItem Value="and">And</asp:ListItem>
                                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                                    </asp:DropDownList>
                                                     </div>
                                                     <div class="form-group">
                                                    <asp:TextBox ID="txtSearch2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="ddlPage62 inputTxt">
                                                        <asp:ListItem Value="and">And</asp:ListItem>
                                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="clearfix"></div>
                                                         </div>
                                                    <div class="form-group">
                                                    <asp:TextBox ID="txtSearch3" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                        </div>






                                                    <div class="clearfix"></div>

                                                   
                                                </div>

                                                <div class="col-md-5 pading5px">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to"
                                                                        Text="Order Field:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>

                                                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                                                </td>
                                                                <td>
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                        <ProgressTemplate>
                                                                            <asp:Label ID="Label3U" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td class="style115">
                                                                    <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlistPull" Width="150px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style116">
                                                                    <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                                        CssClass="ddlPage62">
                                                                        <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                        <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>


                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </div>


                                            </div>


                                        </div>
                                    </fieldset>

                                </div>


                            </div>

                            <div class="row">

                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                            Text="Page size:" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage62" Visible="False">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                              <%--  <div class="row">
                                    <div class=" form-group">
                                        <fieldset class="scheduler-border fieldset_A">

                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-4 pading5px asitCol4">

                                                        <asp:Label ID="lblSearchlist" runat="server" CssClass="lblTxt lblName"
                                                            Text="Search List"></asp:Label>


                                                        <asp:DropDownList ID="ddlFieldList1" runat="server"
                                                            Font-Bold="True" CssClass=" ddlPage inputTxt" Width="152px">
                                                        </asp:DropDownList>
                                                         

                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div class="col-md-1 pading5px">
                                                        <div>
                                                            <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" ddlistPull">
                                                                <asp:ListItem Value="like">Like</asp:ListItem>
                                                                <asp:ListItem Value="=">Equal</asp:ListItem>
                                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                                <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div class="clearfix"></div>
                                                        </div>
                                                        <div class="form-group">
                                                             <asp:DropDownList ID="ddlFieldList2" runat="server" CssClass=" ddlPage inputTxt"
                                                              Font-Bold="True"  Width="152px">
                                                          </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSrch2" runat="server" Font-Bold="True" CssClass="ddlistPull"
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                            <div class="clearfix"></div>
                                                        </div>
                                                        <div class="form-group">
                                                            
                                                        <asp:DropDownList ID="ddlFieldList3" runat="server"   CssClass=" ddlistPull inputTxt"
                                                              Font-Bold="True"   Width="152px">
                                                          </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlSrch3" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px" CssClass="ddlistPull">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                            <div class="clearfix"></div>
                                                        </div>

                                                    </div>


                                                    <div class="col-md-2 pading5px asitCol2">
                                                        <asp:Label ID="lbland1" runat="server" CssClass="lblTxt lblName" Text="And" Visible="False"
                                                            Width="25px"></asp:Label>


                                                        <asp:TextBox ID="txttoSearch1" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                        <asp:DropDownList ID="ddltodesig1" runat="server" CssClass=" ddlPage62 inputTxt">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="ddlPage62 inputTxt pull-right">
                                                            <asp:ListItem Value="and">And</asp:ListItem>
                                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                                        </asp:DropDownList>


                                                        <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                        <asp:DropDownList ID="ddldesig01" runat="server" CssClass=" ddlPage62 inputTxt">
                                                            <asp:ListItem Value="and">And</asp:ListItem>
                                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <div class="clearfix"></div>

                                                        <asp:TextBox ID="txtSearch2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                        <asp:DropDownList ID="ddldesig02" runat="server"
                                                            CssClass="ddlPage62 inputTxt">
                                                        </asp:DropDownList>

                                                        <asp:Label ID="lbland2" runat="server" Text="And" Visible="False"
                                                            CssClass="lblTxt lblName"></asp:Label>

                                                        <asp:TextBox ID="txttoSearch2" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                        <asp:DropDownList ID="ddltodesig2" runat="server" CssClass=" ddlPage62 inputTxt">
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="ddlPage62 inputTxt pull-right">
                                                            <asp:ListItem Value="and">And</asp:ListItem>
                                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                                        </asp:DropDownList>



                                                        <asp:Label ID="lbland3" runat="server" Text="And" Visible="False"
                                                            CssClass="lblTxt lblName"></asp:Label>

                                                        <asp:TextBox ID="txttoSearch3" runat="server" CssClass="txtboxformat"></asp:TextBox>

                                                        <asp:DropDownList ID="ddltodesig3" runat="server" CssClass=" ddlPage62 inputTxt">
                                                        </asp:DropDownList>




                                                        <div class="clearfix"></div>

                                                        <asp:TextBox ID="txtSearch3" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                        <asp:DropDownList ID="ddldesig03" runat="server" CssClass="ddlPage62 inputTxt">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-5 pading5px">
                                                        <asp:Panel ID="Panel5" runat="server">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to"
                                                                            Text="Order Field:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="ddlistPull" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                                            CssClass="ddlPage62">
                                                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>

                                                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                                                    </td>
                                                                    <td>
                                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                            <ProgressTemplate>
                                                                                <asp:Label ID="Label3U" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlistPull" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                                            CssClass="ddlPage62">
                                                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td class="style115">
                                                                        <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlistPull" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td class="style116">
                                                                        <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                                            CssClass="ddlPage62">
                                                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>


                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>


                                                </div>


                                            </div>
                                        </fieldset>

                                    </div>


                                </div>

                                <div class="row">

                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                                Text="Page size:" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage62" Visible="False">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>

                            </div>
                    </div>
                    </fieldset>
                        <div class="row">
                            <asp:GridView ID="gvClientInfo" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvClientInfo_RowDataBound" AllowPaging="True"
                                OnPageIndexChanging="gvClientInfo_PageIndexChanging">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCustname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spouse name">

                                        <ItemTemplate>
                                            <asp:Label ID="lgvSname" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spname")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPname" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proname")) %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Desc">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Car Parking">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCpar" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "carprk")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mailing Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMaddr" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailaddr")) %>'
                                                Width="145px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Permanent Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPaddr" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddr")) %>'
                                                Width="145px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPhone" runat="server" Style="word-break: break-all;"  
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvEmail" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Team">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSteam" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "steam")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Birth">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDofBirth" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "birth")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marriage Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMday" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "marr")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

