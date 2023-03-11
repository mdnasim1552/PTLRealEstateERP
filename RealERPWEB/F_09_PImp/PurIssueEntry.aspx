<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurIssueEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurIssueEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlPrevVOUList_chzn {
            margin-top: 20px !important;
        }

        div#ContentPlaceHolder1_ddlitemlist_chzn {
            margin-top: 20px !important;
        }

        th {
            padding: 0.1rem !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                            <div class="loading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card mt-4">
                <div class="card-header border-bottom-0 pb-0">
                    <div class="row ">
                        <div class="col-lg-12 p-0 m-0">
                            <div class="form-group row">
                                <div class="col-lg-3">
                                    <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>
                                    <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select ddlistPull " AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                    <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass=" ddlistPull inputTxt"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt smLbl_to" Text="Ref. No."></asp:Label>
                                    <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control form-control-sm" TabIndex="3"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblIssuno" runat="server" CssClass="lblTxt lblName" Text="Issue No:"></asp:Label>
                                    <div class="row">
                                        <asp:Label ID="lblCurISSNo1" runat="server" CssClass="form-control form-control-sm col-lg-6">WEN</asp:Label>
                                        <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm col-lg-6" TabIndex="3">000</asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-lg-1">
                                    <asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>

                                </div>
                                <div class="col-lg-2">





                                    <div class="input-group input-group-sm mb-3 mt20">
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="form-control form-control-sm" OnClick="lbtnPrevISSList_Click">Previous Issue</asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-1">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>


                        </div>

                    </div>
                </div>



                <asp:Panel ID="pnlgrd" runat="server" Visible="False">

                    <asp:Panel ID="Panel3" runat="server">

                        <div class="card-header border-top-0 pt-0">
                            <div class="row">

                                <div class="col-lg-12 p-0 m-0">
                                    <div class="form-group row">

                                        <div class="col-lg-2">
                                            <asp:Label ID="lblfloorno" runat="server" Text="Floor No"></asp:Label>
                                            <asp:DropDownList ID="ddlfloorno" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="lblitemList" runat="server" Text="Item List"></asp:Label>
                                            <div class="input-group input-group-sm mb-3">
                                                <asp:TextBox ID="txtsrchItemName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <asp:LinkButton ID="imgbtnSearchItemList" CssClass="btn btn-primary btn-sm" runat="server" OnClick="imgbtnSearchItem_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlitemlist" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                           
                                        <div class="col-lg-2">
                                                           <div class="btn-group">
                                            <asp:LinkButton ID="lbtnAllLab" runat="server" CssClass="btn btn-primary btn-sm mt20 rounded" OnClick="lbtnAllLab_Click">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnAllLaball" runat="server" CssClass="btn btn-primary btn-sm mt20 ml-2 rounded" OnClick="lbtnAllLaball_Click">Select ALL</asp:LinkButton>
                                          

                                            </div>
                                        </div>
                                   

                                        <div class="col-lg-1">
                                            <asp:Label ID="lblPage" runat="server" CssClass="" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>


                                    </div>

                                </div>

                            </div>
                        </div>


                    </asp:Panel>


                    <div class="card-body">
                        <div class="row mt-2 ">
                            <asp:GridView ID="grvissue" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="599px" OnRowDataBound="grvissue_RowDataBound"
                                OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fl" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrDes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workitem")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary  btn-sm"
                                                OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>

                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Bal.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger btn-sm"
                                                OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Wrk.Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtwrkqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Style="text-align: right" CssClass="form-control form-control-sm" Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </div>

                        <div class="row">
                            <asp:Panel ID="Panel2" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:Label ID="lblReqNarr" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Narration:" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style34">
                                            <asp:TextBox ID="txtISSNarr" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" TextMode="MultiLine" CssClass="form-control form-control-sm"
                                                Width="416px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="style35">&nbsp;</td>
                                        <td class="style36">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:Label ID="lblPreparedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Prepared By:" Visible="False"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="style34">
                                            <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApprovedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                                Height="16px" Style="text-align: right" Text="Approved By:" Visible="False"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="style35">
                                            <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td class="style36">
                                            <asp:Label ID="lblApprovalDate" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="16px" Style="text-align: right" Text="Approv.Date:"
                                                Visible="False" Width="66px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                                Visible="False" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>

                </asp:Panel>

            </div>
      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


