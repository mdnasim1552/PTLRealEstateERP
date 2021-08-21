<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FinanceBgdEntry.aspx.cs" Inherits="RealERPWEB.F_03_Fin.FinanceBgdEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

    
   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>--%>
    <%-- <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        }
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
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt"
                                            Visible="False"></asp:Label>
                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3">

                                        <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                            </div>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewCost" runat="server">
                             <asp:Panel ID="PnlColoumn" runat="server">
                                            <table style="width: 100%;">

                                                <tr>
                                                    <td class="style111">
                                                        <asp:Label ID="lblsstartdatevalc" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style111">
                                                        <asp:Label ID="lblsenddatevalc" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style118">
                                                        <asp:Label ID="lblduration" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="style113">&nbsp;</td>
                                                    <td class="style116">&nbsp;</td>
                                                    <td></td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblColGroup" runat="server" BackColor="#660033"
                                                            Font-Size="20px" ForeColor="#000" Height="20px" Style="text-align: center;"
                                                            Width="26px">1</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP1" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" Height="16px" OnClick="lbtngvP_Click"
                                                            Style="text-align: center" Width="17px">1</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">2</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP3" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click"
                                                            Style="text-align: center; height: 15px;" Width="17px">3</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">4</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP5" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">5</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP6" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">6</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">7</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP8" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">8</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvP9" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvP_Click" Style="text-align: center"
                                                            Width="17px">9</asp:LinkButton>
                                                    </td>
                                                </tr>

                                            </table>
                                        </asp:Panel>


                            <asp:GridView ID="gvProCost" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" HeaderStyle-CssClass="HeaderStyle" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvProCost_PageIndexChanging" ShowFooter="True"
                                            Width="16px" PageSize="15">

                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description ">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                            Width="150px">
                                                                         
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvqty" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrate" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvamount" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcostam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total Allocation">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFalloamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvalloamount" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdiscost")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Difference">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFdifamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdifamount" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Pre- Consturction">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpreconstam" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pconstam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFpreconstam" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="YM1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty001" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym1")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym1qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty002" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym2")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym2qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty003" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym3")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym3qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM4">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty004" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym4")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym4qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM5">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty005" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym5")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym5qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM6">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty006" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym6")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym6qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM7">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty007" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym7")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym7qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM8">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty008" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym8")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym8qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM9">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty009" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym9")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym9qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM10" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty010" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym10")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym10qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM11" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty011" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym11")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym11qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM12" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty012" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym12")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym12qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM13" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty013" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym13")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym13qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM14" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty014" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym14")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym14qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM15" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty015" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym15")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym15qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM16" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty016" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym16")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym16qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM17" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty017" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym17")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym17qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM18" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty018" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym18")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym18qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM19" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty019" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym19")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym19qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM20" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty020" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym20")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym20qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM21" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty021" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym21")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym21qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM22" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty022" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym22")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym22qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM23" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty023" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym23")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym23qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM24" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty024" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym24")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym24qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM25" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty025" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym25")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym25qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM26" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty026" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym26")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym26qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM27" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty027" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym27")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym27qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM28" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty028" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym28")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym28qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM29" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty029" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym29")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym29qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM30" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty030" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym30")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym30qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM31" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty031" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym31")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym31qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM32" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty032" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym32")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym32qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM33" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty033" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym33")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym33qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM34" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty034" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym34")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym34qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM35" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty035" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym35")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym35qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM36" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty036" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym36")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym36qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM37" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty037" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym37")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym37qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM38" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty038" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym38")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym38qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM39" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty039" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym39")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym39qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM40" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty040" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym40")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym40qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM41" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty041" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym41")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym41qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM42" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty042" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym42")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym42qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM43" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty043" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym43")).ToString("#,##0(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym43qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM44" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty044" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym44")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym44qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM45" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty045" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym45")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym45qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ym46" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty046" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym46")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym46qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM47" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty047" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym47")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym47qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM48" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty048" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym48")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym48qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM49" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty049" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym49")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym49qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM50" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty050" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym50")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym50qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM51" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty051" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym51")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym51qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM52" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty052" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym52")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym52qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM53" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty053" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym53")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym53qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM54" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty054" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym54")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym54qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM55" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty055" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym55")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym55qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM56" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty056" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym56")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym56qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM57" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty057" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym57")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym57qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM58" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty058" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym58")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym58qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM59" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty059" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym59")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym59qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM60" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty060" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym60")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFym60qty" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
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
                        </asp:View>

                        <asp:View ID="ViewSalesRevenue" runat="server">
                            <asp:Panel ID="Panel2" runat="server">
                                            <div class="form-group">
                                                <asp:Label ID="lblsstartdatetext" runat="server" Text="Sales Start Date :-" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:Label ID="lblsstartdateval" runat="server" CssClass=" smLbl_to"></asp:Label>
                                                <asp:Label ID="lblsenddatetext" runat="server" Text="Sales Finished :-" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:Label ID="lblsenddateval" runat="server" CssClass=" smLbl_to"></asp:Label>

                                                <asp:Label ID="lblSaleablesfttext" runat="server" Text="Saleable Sft :-" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:Label ID="lblSaleablesftval" runat="server" CssClass=" smLbl_to"></asp:Label>
                                                <div class="clearfix"></div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="lblbookingtext" runat="server" Text="Booking % :-" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:Label ID="lblbookingval" runat="server" CssClass=" smLbl_to"></asp:Label>
                                                <asp:Label ID="lblinstallmenttext" runat="server" Text="Installment :-" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:Label ID="lblinstallmentval" runat="server" CssClass=" smLbl_to"></asp:Label>
                                                <div class="clearfix"></div>
                                            </div>
                                           
                                        </asp:Panel>

                            <div class=" form-group">
                                <asp:Label ID="lblSaleRevenue" runat="server" CssClass="btn btn-success primaryBtn" Text="Sale Revenue:"
                                    Width="206px"></asp:Label>
                                <div class="clearfix"></div>
                            </div>

                            <asp:GridView ID="gvPrjsalrev" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" Width="202px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnFUpdatesale" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnFUpdatesale_Click">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvmondesc" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon"))%>'
                                                            Width="80px"></asp:Label>


                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpercent" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lbkgvFpercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sft">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsft" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "persft")).ToString("#,##0; -#,##0; ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvrate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsalamt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0; -#,##0; ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFsalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                      
                        </asp:View>
                        <asp:View ID="ViewEquity" runat="server">
                            <asp:Panel ID="PnlColoumn0" runat="server" BorderColor="Yellow"
                                            BorderStyle="Solid" BorderWidth="1px">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style111">
                                                        <asp:Label ID="lblsstartdatevale" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style111">
                                                        <asp:Label ID="lblsenddatevale" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style118">
                                                        <asp:Label ID="lblduratione" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Yellow" Height="16px" Visible="False" Width="80px"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="style113">&nbsp;</td>
                                                    <td class="style116">&nbsp;</td>
                                                    <td></td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="lblColGroupe" runat="server" BackColor="#660033"
                                                            Font-Size="20px" ForeColor="#000" Height="20px" Style="text-align: center;"
                                                            Width="26px">1</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe1" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Height="16px" OnClick="lbtngvPe_Click"
                                                            Style="text-align: center" Width="17px">1</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">2</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe3" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click"
                                                            Style="text-align: center; height: 15px;" Width="17px">3</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">4</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe5" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">5</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe6" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">6</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">7</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe8" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click" Style="text-align: center"
                                                            Width="17px">8</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtngvPe9" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" OnClick="lbtngvPe_Click"
                                                            Style="text-align: center; height: 15px;" Width="17px">9</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                            <asp:GridView ID="gvProRevnue" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" HeaderStyle-CssClass="HeaderStyle"
                                            OnPageIndexChanging="gvProRevnue_PageIndexChanging" PageSize="15"
                                            ShowFooter="True" Width="16px">

                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description ">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateEqty" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnUpdateEqty_Click">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItemDescEqty" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                            Width="150px">
                                                                         
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalrevEqty" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnTotalrevEqty_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnitrevEqty" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Allocation">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFalloamountEqty" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvalloamountEqty" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdiseqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pre- Consturction">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpreconstamEqty" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pconstam")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFpreconstamEqty" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty001" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym1")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty1" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty002" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym2")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty003" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym3")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty3" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM4">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty004" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym4")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM5">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty005" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym5")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty5" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM6">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty006" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym6")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty6" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM7">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty007" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym7")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM8">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty008" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym8")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty8" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM9">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty009" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym9")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty9" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM10" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty010" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym10")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty10" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM11" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty011" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym11")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty11" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM12" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty012" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym12")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty12" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM13" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty013" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym13")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty13" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM14" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty014" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym14")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty14" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM15" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty015" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym15")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty15" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM16" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty016" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym16")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty16" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM17" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty017" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym17")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty17" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM18" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty018" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym18")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty18" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM19" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty019" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym19")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty19" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM20" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty020" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym20")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty20" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM21" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty021" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym21")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty21" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM22" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty022" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym22")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty22" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM23" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty023" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym23")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty23" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM24" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty024" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym24")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty24" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM25" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty025" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym25")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty25" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM26" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty026" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym26")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty26" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM27" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty027" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym27")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty27" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM28" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty028" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym28")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty28" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM29" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty029" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym29")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty29" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM30" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty030" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym30")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty30" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM31" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty031" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym31")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty31" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM32" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty032" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym32")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty32" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM33" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty033" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym33")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty33" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM34" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty034" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym34")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty34" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM35" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty035" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym35")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty35" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM36" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty036" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym36")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty36" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM37" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty037" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym37")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty37" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM38" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty038" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym38")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty38" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM39" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty039" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym39")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty39" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM40" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty040" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym40")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty40" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM41" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty041" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym41")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty41" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM42" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty042" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym42")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty42" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM43" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty043" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym43")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty43" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM44" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty044" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym44")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty44" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM45" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty045" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym45")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty45" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ym46" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty046" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym46")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty46" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM47" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty047" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym47")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty47" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM48" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty048" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym48")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty48" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM49" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty049" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym49")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty49" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM50" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty050" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym50")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty50" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM51" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty051" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym51")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty51" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM52" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty052" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym52")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty52" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM53" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty053" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym53")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty53" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM54" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty054" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym54")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty54" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM55" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty055" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym55")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty55" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM56" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty056" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym56")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty56" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM57" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty057" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym57")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty57" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM58" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty058" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym58")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty58" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM59" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty059" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym59")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty59" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YM60" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQtyEqty060" runat="server" CssClass="style101"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym60")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFymEqty60" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right; width: 55px;"></asp:Label>
                                                    </FooterTemplate>
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
                            
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

