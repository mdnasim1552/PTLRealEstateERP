<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLinkImpExeStatus.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptLinkImpExeStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .txtboxformat {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
        }

        .style19 {
            width: 9px;
            height: 23px;
        }

        .style20 {
            width: 82px;
            height: 23px;
        }

        .style21 {
            height: 23px;
        }

        .style23 {
            height: 23px;
        }

        .style24 {
            height: 23px;
            width: 656px;
        }

        .style25 {
            height: 23px;
            width: 10px;
        }

        .style26 {
            width: 475px;
            height: 17px;
        }

        .style27 {
            height: 17px;
        }

        .style28 {
            height: 17px;
            width: 213px;
        }


        .style37 {
            height: 23px;
            width: 137px;
        }

        .style40 {
            height: 23px;
            width: 148px;
        }

        .style41 {
            width: 74px;
            height: 23px;
        }


        .style42 {
            width: 93px;
        }

        .style43 {
            width: 51px;
        }

        .style45 {
            width: 99px;
        }

        .style46 {
            width: 3px;
        }

        .style47 {
            width: 398px;
        }

        .style48 {
            width: 135px;
        }


        .style49 {
            width: 208px;
        }


        .style50 {
            width: 202px;
        }

        .grvHeader th {
            font-weight: normal;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {

            var gvmaplnmoplnexe = $('#<%=this.gvmplanvaexe.ClientID %>');


            gvmaplnmoplnexe.Scrollable();
        }
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class=" form-group">
                                <div class="col-md-12 pading5px">
                                    <asp:Label ID="lblUser1" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                    <asp:TextBox ID="lblPrjName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="220px"></asp:TextBox>

                                    <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to">Date</asp:Label>
                                    <asp:TextBox ID="lblFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to">To</asp:Label>
                                    <asp:TextBox ID="lblTDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:Label ID="lblflrlistAll" runat="server" Text="Floor :" CssClass="smLbl_to" Visible="False"></asp:Label>
                                    <asp:Label ID="lblFlDesc" runat="server" CssClass="smLbl_to" Visible="False"></asp:Label>

                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName" Style="font-size: 16px;">Taka in Lac </asp:Label>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class=" form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblflrlist" runat="server" CssClass="lblTxt lblName">Flooe</asp:Label>
                                    <asp:DropDownList ID="ddlFloorListRpt" runat="server" CssClass="ddlPage">
                                    </asp:DropDownList>

                                    <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to" Text="Group :"></asp:Label>
                                    <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                <div class="clearfix"></div>
                            </div>


                        </fieldset>



                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="MaPlanVsPlanVsEx" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvmplanvaexe" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="831px" PageSize="20"
                                        OnPageIndexChanging="gvmplanvaexe_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvflrdesc10" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Description">
                                                <FooterTemplate>
                                                    <table style="width: 16%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Text="Total Amt."></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Text="Execution % Based On Master Plan" Width="190px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcWDesc10" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="230px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit10" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bgd.Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate10" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="M. Budget Qty." Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrptqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="System Generated </Br> Target Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmplanqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mapqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Target Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--<table style="width:2%; height: 19px;">
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                              ForeColor="#000"  style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                              ForeColor="#000"  Height="20px" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>--%>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Execution Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvexqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Master Budget Amt" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrptamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFrptamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>




                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="System Generated </Br> Target Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmpamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mpamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFmpamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Monthly Target Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmonthamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFmonthamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>




                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Execution. Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvexeamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>


                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFexeamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Complited %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "experc")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "experc")))>0?"%":"") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>


                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFexepercentage" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <hr class="hrline" />
                            <asp:View ID="ViewAllWork" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvAllWork" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="831px" PageSize="20"
                                        OnPageIndexChanging="gvAllWork_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvflrdesc10" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Description">
                                                <FooterTemplate>
                                                    <table style="width: 16%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Text="Total Amt."></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Text="Execution % Based On Budget" Width="190px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcWDesc10" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="230px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit10" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bgd.Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate10" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budget Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmBgdqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="M. Plan Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmplanqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mapqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--<table style="width:2%; height: 19px;">
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                              ForeColor="#000"  style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                              ForeColor="#000"  Height="20px" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>--%>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Execution Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvexqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBgdamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="M.Plan Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmpamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mpamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFmpamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Plan Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmonthamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFmonthamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>




                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Execution. Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvexeamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>


                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFexeamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Percent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "experc")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "experc")))>0?"%":"") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>


                                                    <table style="width: 10%; height: 33px;">
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFexepercentage" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></td>
                                                        </tr>
                                                    </table>

                                                </FooterTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>


                                        </Columns>

                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ProjectGraphChart" runat="server">

                                <asp:Panel ID="pnlTarVsAchievement" runat="server" Visible="False" BackColor="#F5F5F5">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class=" form-group">

                                            <div class="col-md-12 pading5px">
                                                <asp:Label ID="lbl01" runat="server" CssClass="lblTxt lblName" Text="Project Start Date:"
                                                    Width="110px"></asp:Label>

                                                <asp:Label ID="lblStartDate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                <asp:Label ID="lbl2" runat="server" CssClass="smLbl_to" Text="Project End Date:"> </asp:Label>
                                                <asp:Label ID="lblEndDate" runat="server" CssClass=" inputtextbox"></asp:Label>




                                                <asp:Label ID="lbltxtDuration" runat="server" CssClass="smLbl_to" Text="Duration"></asp:Label>

                                                <asp:Label ID="lblDuration" runat="server" CssClass=" inputtextbox" Style="width: 26px;"></asp:Label>


                                                <asp:Label ID="lblProgressInPer" runat="server" CssClass=" smLbl_to"></asp:Label>


                                                <asp:CheckBox ID="chkGraph" runat="server" BackColor="Blue" BorderColor="White"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Yellow" Text="Graph" />

                                            </div>
                                        </div>
                                </asp:Panel>

                                <div class="row">

                                    <div class="col-sm-5">

                                        <asp:GridView ID="gvtarvsachivement" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Style="margin-right: 0px" Width="323px" OnRowDataBound="gvtarvsachivement_RowDataBound">
                                            <PagerSettings Position="Top" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Duration">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lgvduration" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monyear")) %>'
                                                            Width="60px"></asp:Label>


                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Target Amt.">
                                                    <ItemTemplate>



                                                        <asp:HyperLink ID="hlnkgvtaramt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                            Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px">
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtaramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Amt.">
                                                    <ItemTemplate>



                                                        <asp:HyperLink ID="hlnkgvacamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                            Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px">
                                                        </asp:HyperLink>



                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFacamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcompleted" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper"))>0?"%":"") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cumulative Target">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcomtarget" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cumulative Actual">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcomactual" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="row">


                                            <asp:Chart ID="Chart1" runat="server" Height="400px" Width="740px" Style="margin-left: -35px;">
                                                <Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                                        MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                                    </asp:Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                                        MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">

                                                        <AxisX MaximumAutoSize="100" Interval="1">
                                                            <ScaleBreakStyle BreakLineStyle="None" />
                                                            <MajorGrid Enabled="false" />

                                                        </AxisX>


                                                    </asp:ChartArea>

                                                </ChartAreas>
                                                <Titles>
                                                    <asp:Title Font="Time New Romans, 16px" Name="Title1"
                                                        Text="Project Target Vs. Achievement">
                                                    </asp:Title>
                                                </Titles>

                                                <Legends>
                                                    <asp:Legend Docking="Top" Alignment="Center"></asp:Legend>
                                                </Legends>

                                            </asp:Chart>


                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblExe" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    <asp:Label ID="lblExAmt" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                </div>


                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

