
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpImpExeStatus.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpImpExeStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblPrjName" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Width="380px"></asp:Label>



                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:Label ID="lblFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:Label ID="lblTDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                        <asp:Label ID="lblflrlistAll" runat="server" CssClass="smLbl_to" Text="Floor" Visible="false"></asp:Label>
                                        <asp:Label ID="lblFlDesc" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Visible="false"></asp:Label>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            TabIndex="2" CssClass="smDropDown inputTxt" Width="70px">
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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblflrlist" runat="server" CssClass="smLbl_to" Text="Floor"></asp:Label>
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True"
                                            TabIndex="2" CssClass="smDropDown inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to" Text="Group"></asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="smDropDown inputTxt"
                                            TabIndex="6">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="MaPlanVsPlanVsEx" runat="server">

                            <asp:GridView ID="gvmplanvaexe" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                                            ForeColor="Black" Text="Total Amt."></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Text="Execution % Based On Master Plan" Width="190px"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Master Plan Qty (Upto Date)">
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
                                                                            ForeColor="Black" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="Black" Height="20px" style="text-align: right" Width="70px"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Master Plan Amt (Upto Date)">
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
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label></td>
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
                        </asp:View>
                        <asp:View ID="ViewAllWork" runat="server">

                            <asp:GridView ID="gvAllWork" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                                            ForeColor="Black" Text="Total Amt."></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Text="Execution % Based On Budget" Width="190px"></asp:Label>
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
                                                                            ForeColor="Black" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="Black" Height="20px" style="text-align: right" Width="70px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
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
                                                            ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label></td>
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
                        </asp:View>
                        <asp:View ID="ViewTvsPlan" runat="server">

                            <asp:GridView ID="gvTvsImp" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvTvsImp_PageIndexChanging"
                                PageSize="20" ShowFooter="True" Width="104px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo12" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Work Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcWDesctvp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor Desc.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrdesctvp" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnittvp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvratetvp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="System Generated Work Target Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtrqtytvp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" System Generated Work Target Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtramttvp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtramttvp" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Target Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvImpqtytvp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "impqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Target Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvimpAmttvp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "impamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFimpAmttvp" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

