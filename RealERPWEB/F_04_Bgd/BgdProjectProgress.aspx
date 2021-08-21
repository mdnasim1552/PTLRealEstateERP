<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdProjectProgress.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdProjectProgress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

        };


    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5" CssClass="ddlPage">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvProProgress" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                Style="margin-right: 0px" Width="16px" AllowPaging="True"
                OnPageIndexChanging="gvEditPur_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo7" runat="server" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project">

                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:Label ID="lblgvproject" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Catagory">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvcatagory" runat="server" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagory")) %>'
                                Width="65px" BackColor="Transparent"></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="left" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Construction Progress">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvconprogress" runat="server" BorderStyle="None" Font-Size="11px"
                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conprogress")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="65px" BackColor="Transparent"></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completion Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvcomdate" runat="server" BorderStyle="None" Font-Size="11px"
                                Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdate")) %>'
                                Width="65px" BackColor="Transparent"></asp:TextBox>
                        </ItemTemplate>


                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
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


            <%-- BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px"
                            Width="1000px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style37"></td>
                                    <td class="style36">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right; color: #FFFFFF;"
                                            Text="Size:"></asp:Label>
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="80px"
                                            TabIndex="5">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style57">&nbsp;
                                    </td>
                                    <td class="style66">&nbsp;
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True"
                                            ForeColor="#000"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>--%>

            
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

