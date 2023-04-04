<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ImplementPlanWithMaterials.aspx.cs" Inherits="RealERPWEB.F_09_PImp.ImplementPlanWithMaterials" %>

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
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                                    <div class="col-lg-2">


                                        <asp:Label ID="lblPreList" runat="server">Prev. List
                                                                    <asp:LinkButton ID="lbtnPrevVOUList" runat="server" OnClick="lbtnPrevVOUList_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlPrevVOUList" runat="server" CssClass="chzn-select form-control form-control-sm " AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                     <div class="col-lg-1">
                                        <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn btn-warning btn-sm mt20" OnClick="lbtnBack_Click">Back</asp:LinkButton>


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
                                            <asp:DropDownList ID="ddlitemlist" runat="server" CssClass=" chzn-select form-control form-control-sm " AutoPostBack="True" OnSelectedIndexChanged="ddlitemlist_SelectedIndexChanged"></asp:DropDownList>
                                        </div>


                                        <div class="col-lg-1">
                                            <asp:LinkButton ID="lbtnAllLab" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnAllLab_Click">Select</asp:LinkButton>

                                        </div>
                                         <div class="col-lg-1 offset-lg-6 d-flex justify-content-end">
                                            <asp:LinkButton ID="lbtngenerate" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="btnGenerateIssue_Click">Generate</asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>

                </asp:Panel>
                <div class="card-body">
                    <asp:Panel runat="server" ID="WorkPanel">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="847px" AllowPaging="True"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                                OnRowDeleting="gvRptResBasis_RowDeleting">
                                <PagerSettings PageButtonCount="20" Position="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                                    <asp:CommandField ControlStyle-Width="20px" ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText='<span class="fa fa-sm fa-trash fa" aria-hidden="true" ></span>&nbsp;' />


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
                                                   <%-- <td>
                                                        <asp:LinkButton ID="lnkfinalup" runat="server" OnClick="lnkfinalup_Click" CssClass="btn btn-danger btn-sm">Final Update</asp:LinkButton>
                                                    </td>--%>
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
                                                Width="100px"></asp:Label>
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
                    </asp:Panel>
                    <asp:Panel runat="server" ID="MaterialPanel" Visible="false">
                        <div class="table table-responsive">
                            <asp:GridView ID="DataGridTwo" runat="server" AutoGenerateColumns="False"                                
                                CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblisircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                Width="150px"></asp:Label>
                                            <asp:Label ID="lblrsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="150px"></asp:Label>
                                            <asp:Label ID="lblflrcod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblflrdesc" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "flrdesc").ToString() %>'
                                                Width="100px" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work">
                                        <ItemTemplate>
                                            <asp:Label ID="lblisirdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "WrkUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkqty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "WrkQty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right; padding-right: 2px;"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnktotalRate" runat="server" OnClick="lnktotalRate_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblrsirdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="200px" Style="padding-left: 2px;"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrsirunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Actual Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAnaQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isustdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAnaQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Rate">
                                       
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAnaRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Site Supply">
                                       
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSiteSupply" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitesupplydate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                             <cc1:CalendarExtender ID="txtSiteSupply_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txtSiteSupply"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>


                                <FooterStyle CssClass="gvPagination" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

