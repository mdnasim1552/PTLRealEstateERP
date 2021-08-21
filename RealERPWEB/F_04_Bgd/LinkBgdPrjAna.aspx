<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkBgdPrjAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.LinkBgdPrjAna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 141px;
        }

        .auto-style2 {
            width: 94px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>







    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <%-- <div class=" form-group">
                                    <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project/Unit"></asp:Label>
                                    <asp:TextBox ID="lblProjectDesc2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        <asp:DropDownList ID="ddlProject" runat="server" Style="width: 300px" CssClass="chzn-select  ddlPage" TabIndex="3">   </asp:DropDownList>

                                        <asp:Label ID="lblProjectDesc" runat="server" Style="width: 300px" Visible="False" CssClass=" inputtextbox"></asp:Label>

                                        <%-- <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" TabIndex="4">Ok</asp:LinkButton>--%>
                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" TabIndex="4" Style="margin-left:-86px;">Ok</asp:LinkButton>


                                    </div>
                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lblProjectLock" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <div class="col-sm-7 pading5px">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" Visible="False" BackColor="#0B88C5" ForeColor="White" AutoPostBack="True" CssClass="btn rbtnList1 margin5px  primaryBtn " OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                            RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Floor Selection</asp:ListItem>
                                            <asp:ListItem>Item Selection(All flr)</asp:ListItem>
                                            <asp:ListItem>Item Selction(Ind.flr)</asp:ListItem>
                                            <asp:ListItem>Rate Input</asp:ListItem>
                                            <asp:ListItem>Reports</asp:ListItem>
                                            <asp:ListItem>Special Report</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-1 pading5px">
                                        <asp:CheckBox ID="ChkCopyProject" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="ChkCopyProject_CheckedChanged" Text="Copy" CssClass="btn btn-primary primaryBtn chkBoxControl"
                                            Visible="False" />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <asp:Panel ID="crDate" runat="server" Visible="false">
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblcreationdate" runat="server" CssClass="lblTxt lblName" Visible="False" Text="Creation Date:"></asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth" Visible="False"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnUpdatePCDate" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False" OnClick="lbtnUpdatePCDate_Click" TabIndex="4">Update</asp:LinkButton>

                                        </div>
                                    </asp:Panel>

                                    <div class=" clearfix"></div>
                                </div>
                            </div>
                            <asp:Panel ID="PnlCopyProject" runat="server" Visible="False">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">From Project</asp:Label>

                                        <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnCopyFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnCopyFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnCopyProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyProject_Click" TabIndex="4">Copy Data P/U</asp:LinkButton>

                                    </div>


                                </div>

                            </asp:Panel>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1Floor" runat="server">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <asp:Label ID="lblFPhaseTitle" runat="server" CssClass="btn btn-success primaryBtn"
                                        Text="Floor Selection"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkFlrShowSelected" runat="server" AutoPostBack="True"
                                        CssClass="chkBoxControl primaryBtn"
                                        OnCheckedChanged="chkFlrShowSelected_CheckedChanged"
                                        Text="Show selected floor(s) only" />

                                    <asp:LinkButton ID="lbtnShowSelectedFloor" runat="server" CssClass="btn  btn-primary primaryBtn" OnClick="lbtnShowSelectedFloor_Click">Show Selected</asp:LinkButton>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="col-md-12">
                                <hr class="hrline" />
                                <asp:CheckBoxList ID="cbListFloor" runat="server"
                                    CellPadding="2" CellSpacing="8" RepeatColumns="7"
                                    CssClass=" chkBoxControl primaryBtn " Font-Size="12px"
                                    Width="912px">
                                    <asp:ListItem>aa</asp:ListItem>
                                    <asp:ListItem>bb</asp:ListItem>
                                    <asp:ListItem>cc</asp:ListItem>
                                    <asp:ListItem>dd</asp:ListItem>
                                    <asp:ListItem>ee</asp:ListItem>
                                    <asp:ListItem>ff</asp:ListItem>
                                    <asp:ListItem>gg</asp:ListItem>
                                    <asp:ListItem>hh</asp:ListItem>
                                    <asp:ListItem>ii</asp:ListItem>
                                    <asp:ListItem>jj</asp:ListItem>
                                    <asp:ListItem>kk</asp:ListItem>
                                    <asp:ListItem>ll</asp:ListItem>
                                    <asp:ListItem>mm</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>


                        </asp:View>
                        <asp:View ID="View2Item" runat="server">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <asp:Label ID="lblTitle1" runat="server" CssClass="btn btn-success primaryBtn"
                                        Text="Item Selection (All Floor)"></asp:Label>
                                    <asp:HyperLink ID="lnkbtnAnalysis" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="~/F_04_Bgd/BgdStdAna.aspx" Target="_blank"><span class="flaticon-edit26"></span> Analysis </asp:HyperLink>
                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Desc. of Items" Style="font-size: 11px;"></asp:Label>

                                    <asp:TextBox ID="txtItemSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-5 pading5px ">
                                    <asp:DropDownList ID="ddlItem" runat="server" Style="width: 313px" CssClass="chzn-select form-control  inputTxt" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<asp:Label ID="ddlterd" runat="server"
                                            Visible="False" CssClass="form-control inputTxt"></asp:Label>--%>
                                </div>

                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnSelectItem" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectItem_Click" TabIndex="4" Style="margin-left:-155px">Select Item</asp:LinkButton>

                                </div>
                                <div class="col-md-3 pull-right pading5px">
                                    <%--   <asp:Label ID="Label7" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>--%>
                                </div>
                                <div class=" clearfix"></div>
                            </div>

                            <hr class=" hrline" />
                            <div id="divGridView">
                                <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                                    OnRowCancelingEdit="gvAnalysis_RowCancelingEdit" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvAnalysis_RowDataBound" OnRowEditing="gvAnalysis_RowEditing"
                                    OnRowUpdating="gvAnalysis_RowUpdating" PageSize="15" Width="76px"
                                    ShowFooter="True" OnRowDeleting="gvAnalysis_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Item Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="auto-style1">
                                                            <asp:Label ID="Label13" runat="server" Text="Description Of Item" Width="200px"></asp:Label>
                                                        </td>
                                                        <td class="auto-style2">
                                                            <asp:TextBox ID="txtIgrdtemSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ImgbtngrdFindItem" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtngrdFindItem_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblgvItmDesc_e" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")).Trim() %>'
                                                                Width="350px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvgvFloorAna" runat="server" AutoGenerateColumns="False"
                                                                HorizontalAlign="Center" Width="200px" BorderColor="White"
                                                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                                BorderStyle="Solid" BorderWidth="1px" ShowFooter="True" Height="16px"
                                                                Style="text-align: left">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Floor Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvgvFlrCod" runat="server" Font-Bold="False"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                                                Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Floor Desc">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvgvFlrDesc" runat="server" CssClass="StyleCheckBoxList"
                                                                                Font-Bold="False"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ref. No.">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvItmRef" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px"
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "itmrefno").ToString() %>'
                                                                                Width="80px" Font-Size="12px" Style="text-align: left"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lbtngvgvRefresh" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" OnClick="lbtngvgvRefresh_Click">Total</asp:LinkButton>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Budgeted Qty.">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvQty" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                Width="80px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvgvQtyFooter" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                Width="85px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle CssClass="grvFooter" />
                                                                <EditRowStyle />
                                                                <AlternatingRowStyle />
                                                                <PagerStyle CssClass="gvPagination" />
                                                                <HeaderStyle CssClass="grvHeader" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </EditItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="100px" CssClass="style101"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="View3Item" runat="server">

                            <asp:Label ID="lblTitle1a" runat="server" CssClass=" btn btn-success primaryBtn"> Item Selection (Individual Floor)</asp:Label>
                            <div class="clearfix"></div>
                            <div class="form-group">

                                <div class="col-md-1 pading5px asitCol1">
                                    <asp:Label ID="lblItem7" runat="server" CssClass="lblTxt lblName"> Floor</asp:Label>

                                </div>
                                <div class="col-md-4 pading5px asitCol4 ">
                                    <asp:DropDownList ID="ddlFloorList" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblFloorName" runat="server" Visible="False"
                                        CssClass="form-control inputTxt">Floor Name</asp:Label>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:LinkButton ID="lbtnSelectFloor" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectFloor_Click" TabIndex="4">Select Floor P/U</asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblItem2" runat="server" CssClass="btn btn-success primaryBtn">Description of Items</asp:Label>
                                <hr class="hrline" />
                                <div class="clearfix"></div>

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy"
                                        Visible="False" CssClass="btn btn-primary primaryBtn" />
                                </div>
                                <div class="col-md-4 pading5px asitCol4 ">
                                    <asp:DropDownList ID="ddlFloorListToCopy" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="Label12" runat="server" Visible="false"
                                        CssClass="form-control inputTxt">Floor Name</asp:Label>
                                    <asp:LinkButton ID="lbtnCopyData" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyData_Click" TabIndex="4">Copy Data </asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="dislbl" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <asp:TextBox ID="txtItemSearch2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                    <asp:LinkButton ID="ImgbtnFindItem2" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-3 pading5px asitCol3 ">
                                    <asp:DropDownList ID="ddlItem2" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">

                                    <asp:LinkButton ID="lbtnSelectItem2" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectItem2_Click" TabIndex="4" Style="margin-left:-50px;">Select Item </asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>


                            <%--<table style="width: 265px; margin-right: 0px; background-color: #999966;">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style33" colspan="3">
                                        <asp:Label ID="lblTitle1a" runat="server" BackColor="Blue" Font-Bold="True"
                                            Font-Size="22px" ForeColor="Yellow"
                                            Style="font-weight: 700; color: #000000; text-align: left"
                                            Text="Item Selection (Individual Floor)" Width="418px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblItem7" runat="server" Font-Size="14px" Font-Underline="False"
                                            Style="font-weight: 700; text-align: right" Text="Floor :" Width="62px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:DropDownList ID="ddlFloorList" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="145px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblFloorName" runat="server" BackColor="Blue" Font-Bold="True"
                                            Font-Size="18px" ForeColor="Yellow"
                                            Style="font-weight: 700; color: #000000; text-align: center; text-transform: capitalize"
                                            Text="Floor Name" Visible="False" Width="150px"></asp:Label>
                                    </td>
                                    <td class="style149">
                                        <asp:LinkButton ID="lbtnSelectFloor" runat="server" Font-Bold="True"
                                            Font-Size="12px" OnClick="lbtnSelectFloor_Click" Style="text-align: center"
                                            Width="67px">Select Floor</asp:LinkButton>
                                    </td>
                                    <td class="style129">&nbsp;</td>
                                    <td class="style128">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style33">&nbsp;</td>
                                    <td class="style34">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblItem2" runat="server" CssClass="newStyle3" Font-Size="14px"
                                            Font-Underline="True" Style="font-weight: 700" Text="Description of Items"
                                            Width="313px" Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: right"></td>
                                    <td class="style151">
                                        <asp:DropDownList ID="ddlFloorListToCopy" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize"
                                            Visible="False" Width="145px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style149">
                                        <asp:LinkButton ID="lbtnCopyData" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="19px" OnClick="lbtnCopyData_Click"
                                            Style="text-align: center;" Visible="False" Width="67px">Copy Data</asp:LinkButton>
                                    </td>
                                    <td class="style129">&nbsp;</td>
                                    <td class="style128">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style33">
                                        <asp:TextBox ID="txtItemSearch2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Height="18px" Width="66px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td class="style34">
                                        <asp:ImageButton ID="ImgbtnFindItem2" runat="server" Height="19px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindItem_Click"
                                            Width="16px" Visible="False" />
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlItem2" runat="server" CssClass="newStyle1"
                                            Font-Bold="True" Font-Size="11px" Height="21px" Width="540px"
                                            Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style149">
                                        <asp:LinkButton ID="lbtnSelectItem2" runat="server" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: center" Visible="False" Width="67px"
                                            OnClick="lbtnSelectItem2_Click">Select Item</asp:LinkButton>
                                    </td>
                                    <td class="style129">&nbsp;</td>
                                    <td class="style128">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style33" colspan="10"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style33">&nbsp;</td>
                                    <td class="style34">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style151">&nbsp;</td>
                                    <td class="style149">&nbsp;</td>
                                    <td class="style129">&nbsp;</td>
                                    <td class="style128">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                            </table>--%>
                            <asp:GridView ID="gvAnalysis2" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvAnalysis2_PageIndexChanging" PageSize="15" ShowFooter="True"
                                Width="16px" Visible="False">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItmRef" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                                Style="background-color: Transparent; text-align: left;"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "itmrefno").ToString() %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate2" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate2_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
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
                        <asp:View ID="View4Resource" runat="server">

                            <div class=" form-group">
                                <div class="col-md-6pading5px">
                                    <asp:Label ID="lblTitle2" runat="server" CssClass="lblTxt lblName " Width="170">Resource Rate Input &amp; Report</asp:Label>

                                    <asp:TextBox ID="txtSearchItem" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                    <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" OnClick="lbtnSelectFloorRes_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>


                                </div>
                                <div class="clearfix"></div>
                            </div>


                            <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvResInfo_PageIndexChanging"
                                PageSize="20" Width="16px" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Floor">
                                        <FooterTemplate>
                                            <asp:CheckBox ID="chkProjectLock" runat="server" Font-Size="12px"
                                                OnCheckedChanged="chklkrate_CheckedChanged" Text="Project Lock" Width="90px" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfloordes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Resource">
                                        <FooterTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style173">
                                                        <asp:CheckBox ID="chklkrate" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chklkrate_CheckedChanged" Text="Lock Rate" />
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnUpdateResRate" runat="server" Font-Bold="True"
                                                            Font-Size="14px" OnClick="lbtnUpdateResRate_Click" Style="text-align: center; height: 17px;"
                                                            Width="80px">Update Rate</asp:LinkButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Est.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnSameValue" runat="server" OnClick="lbtnSameValue_Click" CssClass="btn btn-danger primaryBtn">Put Same Val</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="85px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res. Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF" Font-Size="11px"
                                                BorderStyle="Solid" BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnResTotal" runat="server" Font-Bold="True"
                                                Font-Size="14px" OnClick="lbtnResTotal_Click" Style="text-align: center;"
                                                Width="50px">Total :</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTResAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTResAmtFooter" runat="server" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                        <asp:View ID="View5Report" runat="server">
                            <div class="form-group">
                                <div class="col-md-7 pading5px asitCol7">
                                    <asp:Label ID="lblTitle4" runat="server" CssClass="lblTxt lblName "  Text="Analysis Reports"></asp:Label>

                                    <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" CssClass=" ddlPage62" Width="180"
                                        OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                                        <asp:ListItem>Resource Basis</asp:ListItem>
                                        <asp:ListItem>Work Basis</asp:ListItem>
                                        <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                        <asp:ListItem>Individual Work Basis</asp:ListItem>
                                        <asp:ListItem>Analysis Sheet</asp:ListItem>
                                    </asp:DropDownList>
                                


                                

                                    <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to"
                                        Text="Group :"></asp:Label>

                                    <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblRptMainGroup" runat="server" CssClass="lblTxt lblName " Visible="false"
                                        Text="Main Group :"></asp:Label>

                                    <asp:DropDownList ID="ddlRptMainGroup" runat="server" CssClass="ddlPage" Visible="false">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Label ID="lblFloor" runat="server" CssClass=" smLbl_to"  Text="Floor"></asp:Label>

                                    <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" CssClass="chzn-select  ddlPage" Width="190px">
                                    </asp:DropDownList>


                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="ddlPage62"  Width="180px">
                                    </asp:DropDownList>--%>

                                   
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                     <asp:LinkButton ID="lbtnShowReport" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowReport_Click">Show Report</asp:LinkButton>
                                    </div>
                                <div class=" clearfix"></div>
                            </div>

                            

                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4" style="display: none">
                                    <asp:Label ID="lblRptResBreak" runat="server" CssClass="lblTxt lblName " Visible="false"
                                        Text="Res. Group"></asp:Label>

                                    <asp:DropDownList ID="ddlRptResBreak" runat="server" AutoPostBack="True" CssClass="ddlPage62" Width="180"
                                        Visible="False">
                                        <asp:ListItem>Resource Basis</asp:ListItem>
                                        <asp:ListItem>Work Basis</asp:ListItem>
                                        <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                        <asp:ListItem>Individual Work Basis</asp:ListItem>
                                        <asp:ListItem>Analysis Sheet</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6 pading5px">
                                    <asp:CheckBox ID="ChkPunchValues" runat="server" CssClass="chkBoxControl margin5px" Text="Including Punch Value" Visible="false" />
                                    <asp:CheckBox ID="ChkAdditionalCost" runat="server" Text="Incl. Additional Cost" Visible="false" />

                                    <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" CssClass="chkBoxControl margin5px" Text="Ignore Sch. Rate" Visible="False" />
                                    <asp:CheckBox ID="ChkMKSUnit" runat="server" CssClass="chkBoxControl margin5px"
                                        Text="Consider MKS Unit" Visible="False" />

                                    <asp:CheckBox ID="ChkOnSchiNo" runat="server" Text="Order on Sch. Item" Visible="False" />
                                </div>


                                <div class=" clearfix"></div>
                            </div>

                            <div class="form-group">
                                <%--<div class="col-md-3 pading5px">
                                    <asp:Label ID="lblFloor" runat="server" CssClass="lblTxt lblName "
                                        Text="Floor"></asp:Label>
                               
                                    <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" CssClass=" ddlPage62" Width="180px">
                                    </asp:DropDownList>
                                </div>--%>

                                <div class="col-md-8 pading5px">
                                    <asp:Panel ID="PnlRptItmList" runat="server" Style="background-color: #999966"
                                        Visible="False">
                                        <asp:Label ID="lblItem9" runat="server" CssClass="lblTxt lblName"
                                            Text="Item:"></asp:Label>
                                        <asp:TextBox ID="txtRptItemSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnRptFindItem" runat="server" OnClick="ImgbtnRptFindItem_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlRptItem" runat="server" CssClass=" ddlPage" Width="461px">
                                        </asp:DropDownList>



                                    </asp:Panel>
                                    <asp:Panel ID="PnlRptResList" runat="server" Style="background-color: #999966"
                                        Visible="False">
                                        <asp:Label ID="lblItem12" runat="server" CssClass="lblTxt lblName"
                                            Text="Resource:"></asp:Label>
                                        <asp:TextBox ID="txtRptResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnRptFindRes" runat="server" OnClick="ImgbtnRptFindRes_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlRptRes" runat="server" CssClass=" ddlPage" Width="461px">
                                        </asp:DropDownList>

                                    </asp:Panel>

                                </div>


                                <div class=" clearfix"></div>
                            </div>

                            <div class="row">
                                <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="847px" ShowFooter="True" OnRowDataBound="gvRptResBasis_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Construction Area:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Cost Per SFT:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <%--<table>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td class="style158" colspan="2"></td>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblRptResBreak" runat="server" Font-Size="12px"
                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                            Text="Res. Group :" Visible="False" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style158">
                                        <asp:DropDownList ID="ddlRptResBreak" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px"
                                            Style="text-transform: capitalize; margin-left: 0px;" Visible="False"
                                            Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style159">
                                        <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" Font-Bold="True"
                                            Font-Size="12px" Text="Ignore Sch. Rate" Visible="False" Width="110px" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td class="style156">
                                        <asp:Label ID="lblRptMainGroup" runat="server" Font-Size="12px"
                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                            Text="Main Group :" Width="72px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptMainGroup" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblFloor" runat="server" Font-Size="14px"
                                            Font-Underline="False" Height="16px" Style="font-weight: 700; text-align: right"
                                            Text="Floor :" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style158">
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style159" colspan="4"></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td colspan="7"></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                        </asp:View>
                        <asp:View ID="SpReport" runat="server">
                            <table style="width: 100%;">

                                <tr>
                                    <td colspan="8">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table class="table table-hover table-condensed">

                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style166">
                                                        <asp:Label ID="lblTitle5" runat="server" CssClass="btn btn-success primaryBtn"
                                                            Text="Special Reports" Width="187px"></asp:Label></td>
                                                    <td class="style171">
                                                        <asp:Label ID="Label1" runat="server" Font-Size="12px"
                                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                            Text="Floor :" Width="60px" ForeColor="#000"></asp:Label></td>
                                                    <td class="style167">
                                                        <asp:DropDownList ID="ddlFlrlstspr" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="120px">
                                                        </asp:DropDownList></td>
                                                    <td class="style169">
                                                        <asp:Label ID="Label2" runat="server" Font-Size="12px"
                                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                            Text="Group :" Width="80px" ForeColor="#000"></asp:Label></td>
                                                    <td class="style170">

                                                        <asp:DropDownList ID="ddlRptGroupspr" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px"
                                                            Style="text-transform: capitalize" Width="100px">
                                                            <asp:ListItem>Main</asp:ListItem>
                                                            <asp:ListItem>Sub-1</asp:ListItem>
                                                            <asp:ListItem>Sub-2</asp:ListItem>
                                                            <asp:ListItem>Sub-3</asp:ListItem>
                                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td>
                                                        <asp:LinkButton ID="lbtnShowSpRpt" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowSpRpt_Click">Show</asp:LinkButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style166">&nbsp;</td>
                                                    <td class="style171">
                                                        <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Underline="False"
                                                            ForeColor="#000" Style="font-weight: 700; text-align: right" Text="Work :"
                                                            Width="60px"></asp:Label>
                                                    </td>
                                                    <td class="style170">
                                                        <asp:DropDownList ID="ddlRptWork" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px"
                                                            Style="text-transform: capitalize" Width="120px">
                                                            <asp:ListItem Selected="True">All Work</asp:ListItem>
                                                            <asp:ListItem>Civil</asp:ListItem>
                                                            <asp:ListItem>Sanitary</asp:ListItem>
                                                            <asp:ListItem>Electrical</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style168">
                                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="color: #000; text-align: right;" Text=" Page Size:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style167">
                                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="100px">
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

                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:GridView ID="gvSpRpt" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvSpRpt_PageIndexChanging" ShowFooter="True" Width="847px">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Floor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptFlr2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptRes2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                            Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptQty2" runat="server" Font-Bold="true" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bgd. Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptRat2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptAmt2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percentage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPer0" runat="server" Font-Bold="true" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFPer" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>







        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

