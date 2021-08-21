<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PayTypCodeBook.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.PayTypCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--<script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

        });


    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblUserName" CssClass="lblTxt lblName" runat="server">User Name:</asp:Label>

                                        <asp:TextBox ID="txtUserSearch1" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindUser1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindUser1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                   
                                    </div>
                                    <div class="col-md-4 pading5px">
                                             <asp:DropDownList ID="ddlUserList" runat="server" Font-Bold="True" CssClass=" ddlPage"
                                          >
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary primaryBtn margin5px" 
                                                OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                    </div>
                                  
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-primary primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="gvMarLinkInfo" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                AutoGenerateColumns="False" ShowFooter="True" Width="16px">
                                                <PagerSettings Visible="False" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="user Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResCod0" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Min Amount">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnSuplUpdate" runat="server"  
CssClass="btn btn-danger primaryBtn"
                                                                OnClick="lbtnSuplUpdate_Click">Final Update</asp:LinkButton>
                                                        </FooterTemplate>

                                                        <ItemTemplate>
                                                            <asp:TextBox ID="gvMinamt" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                                CssClass="GridTextbox"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Max Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="gvMaxamt" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                                CssClass="GridTextbox"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                               <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />

                                            </asp:GridView>
                </div>



                <%--<table style="width: 100%;">




                    <tr>
                        <td colspan="8">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="background-color: #C1D2C4;">
                                    <tr>
                                        <td class="style52">
                                            <asp:Label ID="lblUserName" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Text="User Name:" Width="100px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserSearch1" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="105px"
                                                TabIndex="3"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImgbtnFindUser1" runat="server" Height="19px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindUser1_Click" TabIndex="2"
                                                Width="21px" />
                                        </td>
                                        <td class="style78">
                                            <asp:DropDownList ID="ddlUserList" runat="server" Font-Size="12px"
                                                Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                                Width="300px">
                                            </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ListSearchExt3" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlUserList">
                                            </cc1:ListSearchExtender>
                                        </td>
                                        <td colspan="2">
                                            
                                        </td>
                                        <td class="style86">&nbsp;</td>
                                        <td class="style87">
                                            
                                        </td>
                                        <td class="style73">&nbsp;</td>
                                        <td class="style66">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="height: 200px" valign="top">
                                            
                                        </td>
                                        <td class="style56" valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style52">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style78">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style67" colspan="2">&nbsp;</td>
                                        <td class="style87"></td>
                                        <td class="style73"></td>
                                        <td class="style53">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="style60">&nbsp;</td>
                        <td class="style25">&nbsp;</td>
                        <td class="style22">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

