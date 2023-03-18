<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ImplementPlan.aspx.cs" Inherits="RealERPWEB.F_09_PImp.ImplementPlan" %>

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




        th {
            padding: 0.1rem !important;
        }
    </style>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <%-- <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

        </script>--%>



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
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card mt-4">
                <asp:Panel ID="Panel" runat="server">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-12 p-0">
                                <div class="form-group row mb-0">
               


                                    <div class="col-lg-3">
                                        <asp:Label ID="lblProjectList" runat="server" Text="Project Name "></asp:Label>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                                    </div>

                                    <div class="col-lg-2">

                                        <asp:Label ID="lblvounotext" runat="server"
                                            Text="Implement No"></asp:Label>

                                        <div class="input-group input-group-sm mb-3">
                                            <div class="input-group-prepend">
                                                <asp:Label ID="lblCurVOUNo1" runat="server" CssClass="input-group-text"
                                                    Text="WEP"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtCurVOUNo2" runat="server" CssClass="form-control"
                                                ReadOnly="True">000000000</asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Label ID="lbldate" runat="server" Text="Date "></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm"
                                            TabIndex="6"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>


                                    <div class="col-lg-2">


                                        <asp:Label ID="lblPreList" runat="server">Prev. List
                                                                    <asp:LinkButton ID="lbtnPrevVOUList" runat="server" OnClick="lbtnPrevVOUList_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlPrevVOUList" runat="server" CssClass="chzn-select form-control form-control-sm " AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1">
                                        <asp:Label ID="lblpage0" CssClass="lblTxt lblName" runat="server" Text="Item Search"></asp:Label>
                                        <div class="input-group input-group-sm mb-3">
                                            <asp:TextBox ID="txtSearchItem" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton ID="ImgbtnItemSearch" runat="server" CssClass="btn btn-primary" OnClick="ImgbtnItemSearch_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-lg-1">
                                        <asp:Label ID="lblpage" runat="server" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm"
                                            TabIndex="4">
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
                                    <div class="col-lg-1">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk1_Click">Ok</asp:LinkButton>


                                    </div>

                                </div>
                            </div>

                            <div class="col-lg-12 p-0">
                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <div class="form-group row mb-0">


                                        <div class="col-lg-2">
                                            <asp:Label ID="lblfloorno" runat="server" Text="Floor No"></asp:Label>
                                            <asp:DropDownList ID="ddlfloorno" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlfloorno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                      



                                        <div class="col-lg-2">
                                            <asp:Label ID="lblitemList" runat="server" Text="Item List">
                                                <asp:LinkButton ID="imgbtnSearchItemList" runat="server" OnClick="imgbtnSearchItemList_Click"><i class="fa fa-search "> </i></asp:LinkButton>
                                            </asp:Label>
                                            <asp:DropDownList ID="ddlitemlist" runat="server" CssClass=" chzn-select fform-control form-control-sm " AutoPostBack="True" OnSelectedIndexChanged="ddlitemlist_SelectedIndexChanged"></asp:DropDownList>
                                        </div>


                                        <div class="col-lg-1">
                                            <asp:LinkButton ID="lbtnAllLab" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnAllLab_Click">Select</asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>

                </asp:Panel>
                <div class="card-body">

                    <div class="table table-responsive">
                        <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="847px" AllowPaging="True"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                            OnRowDeleting="gvRptResBasis_RowDeleting">
                            <PagerSettings PageButtonCount="20" Position="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />

                                <asp:TemplateField HeaderText="Floor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFloorCode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ItemCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemCode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptcod")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Description">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkfinalup" runat="server" OnClick="lnkfinalup_Click" CssClass="btn btn-danger btn-sm">Final Update</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="lnkDelete_Click">Delete All</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completed">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComQty" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" OnClick="lnktotal_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Target (System)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvstarqty" runat="server"
                                            BorderStyle="None" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Font-Size="12px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcurqty" runat="server" CssClass=" inputtextbox"
                                            BorderStyle="none" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Font-Size="12px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
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

