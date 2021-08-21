<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpSysGenTarget.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpSysGenTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {


            var gvTvsImp = $('#<%=this.gvTvsImp.ClientID %>')


           gvTvsImp.Scrollable();
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
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Date"></asp:Label>
                                        <asp:TextBox ID="lblFDate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="To"></asp:Label>
                                        <asp:TextBox ID="lblTDate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <div class="colMdbtn">
                                            

                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                             Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="70px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="400">400</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblflrlist" runat="server" CssClass="smLbl_to" Text="Floor: "></asp:Label>

                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" CssClass=" smDropDown" Width="110px">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to" Text="Group"></asp:Label>

                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass=" smDropDown" Width="80px">
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
                                    <asp:TemplateField HeaderText="Total Work Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Completed Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvterqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
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
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                    </table>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


