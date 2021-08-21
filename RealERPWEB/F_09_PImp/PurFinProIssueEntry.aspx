<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurFinProIssueEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurFinProIssueEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        };
    </script>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="pnlMain" runat="server" >
                                    <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol12">

                                        <asp:Label ID="lblProjectList" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnSearchProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnSearchProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlprjlist" runat="server" Width="300px"  CssClass="ddlPage">
                                        </asp:DropDownList>


                                        <asp:Label ID="lblddlProject" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                      
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    

                                    </div>
                                </div>

                                    <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol12">

                                        <asp:Label ID="lblMonth" runat="server" CssClass=" lblName lblTxt" Text="Month:"></asp:Label>

                                      <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                </asp:Panel>
                                    <asp:Panel ID="pnlSub" runat="server" Visible="False">
                                        <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol12">

                                        <asp:Label ID="lblProjectList0" runat="server" CssClass=" lblName lblTxt" Text="Item:"></asp:Label>

                                        <asp:TextBox ID="txtsrchItemName" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnSearchItemList" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnSearchItem_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlitemlist" runat="server" Width="300px"  CssClass="ddlPage">
                                        </asp:DropDownList>
                                      
                                        <asp:LinkButton ID="lbtnISelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnISelect_Click">Select</asp:LinkButton>

                                    

                                    </div>
                                </div>
                                        <div class="form-group">
                                    <div class="col-md-12  pading5px  asitCol12">

                                        <asp:Label ID="lblProjectList1" runat="server" CssClass=" lblName lblTxt" Text="Page:"></asp:Label>/asp:LinkButton>

                                      <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"  CssClass="ddlPage">
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

                                         <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>

                                    </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvissue" runat="server" AllowPaging="True" Font-Size="11px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="599px"
                            OnRowDeleting="grvissue_RowDeleting"
                            OnPageIndexChanging="grvissue_PageIndexChanging">
                            <PagerSettings Position="TopAndBottom" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvitemcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Work Description">
                                    <FooterTemplate>
                                        <table style="width: 39%; height: 27px;">
                                            <tr>
                                                <td class="style35">
                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvwrkdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvitemunit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Master Plan Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvmqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monthly Plan Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvmonqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Execuation Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvexeqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Master Plan Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvmFTotalAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmamount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Monthly Plan Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvmonFTotalAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonamount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Execution Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvexeFTotalAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvexeamount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
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
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

