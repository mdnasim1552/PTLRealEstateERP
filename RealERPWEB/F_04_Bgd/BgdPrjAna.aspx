﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BgdPrjAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdPrjAna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
    <style type="text/css">
        .auto-style1 {
            width: 141px;
        }

        .auto-style2 {
            width: 94px;
        }

        .grditem {
            color: maroon;
            font-size: 12px;
            font-weight: bold;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
                    
        }

        input[type=radio] {
            box-sizing: border-box;
            padding: 0;
            margin-right: 5px;
            margin-left: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function Search_Gridview(strKey, cellNr, gvname) {

            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvname) {

                case 'gvAnalysis':
                    tblData = document.getElementById("<%=this.gvAnalysis.ClientID %>");
                    console.log('Ok');
                    break;
                case 'gvMultiSelectItem':
                    tblData = document.getElementById("<%=this.gvMultiSelectItem.ClientID %>");
                    console.log('Ok');
                    break;
            }
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function openNotesModal() {
            $('#noteDetails').modal('toggle');
        }
        function closeNotesModal() {
            $('#noteDetails').modal('hide');
        }



        function OpenMulitSelectModal() {
            $('#MulitSelectModal').modal('show');
        }

        function CloseMulitSelectModal() {
            $('#MulitSelectModal').modal('hide');
        }


        function pageLoaded() {

            try {
                $('.chzn-select').chosen({ search_contains: true });

                $('#<%=this.gvRptResBasis02.ClientID%>').tblScrollable();

                $('#<%=this.gvRptResBasis.ClientID%>').tblScrollable();


            }


            catch (e) {

                alert(e);
            }
        }


        function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvMultiSelectItem.ClientID %>");
            console.log("Executed.");
            var i = 0
            $('#<%=gvMultiSelectItem.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
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
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>



            <div class="card card-fluid">
                <div class="card-body mb-3">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblItem" runat="server" CssClass="control-label" Text="Project Name"></asp:Label>
                                <asp:LinkButton ID="ImgbtnFindProject" CssClass="srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectDesc" runat="server" Visible="false" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 text-center" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk1_Click">Ok</asp:LinkButton>
                            <asp:CheckBox ID="chkShorting" runat="server" AutoPostBack="true" OnCheckedChanged="chkShorting_CheckedChanged" Text="Alphabet" />
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <asp:CheckBox ID="ChkCopyProject" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopyProject_CheckedChanged" Text="Copy Budget"
                                Visible="False" />

                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:CheckBox ID="ChkCopyTender" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopyTender_CheckedChanged" Text="Copy Tender"
                                Visible="False" CssClass="btn btn-sm btn-primary chkBoxControl primaryBtn" />
                        </div>
                        <div class="col-md-3 pull-right pading5px">
                            <asp:Label ID="lblprocopy" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblProjectLock" runat="server" Visible="False"></asp:Label>

                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-9 col-sm-9 col-lg-9">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" Visible="False" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                RepeatColumns="7" RepeatDirection="Horizontal">
                                <asp:ListItem>Catagory Selection</asp:ListItem>
                                <asp:ListItem>Item Selection(All Cat.)</asp:ListItem>
                                <asp:ListItem>Item Selction(Ind.Cat)</asp:ListItem>
                                <asp:ListItem>Rate Input</asp:ListItem>
                                <asp:ListItem>Reports</asp:ListItem>
                                <asp:ListItem>Special Report</asp:ListItem>
                                <asp:ListItem>Special Report(02)</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>



                    <div class="row" runat="server" id="PnlCopyProject" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="control-label" Text="From Project"></asp:Label>
                                <asp:LinkButton ID="ibtnCopyFindProject" CssClass="srearchBtn" runat="server" OnClick="ibtnCopyFindProject_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <asp:LinkButton ID="lbtnCopyProject" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" AutoPostBack="True" OnClick="lbtnCopyProject_Click">Copy</asp:LinkButton>
                        </div>

                        <div class="col-md-2" style="margin-top: 20px;">
                            <asp:CheckBox ID="chkwithoutqty" runat="server" Text="Without Qty" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="PnlCopyTender" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="From Tender"></asp:Label>
                                <asp:LinkButton ID="ibtnCopyFindTender" CssClass="srearchBtn" runat="server" OnClick="ibtnCopyFindTender_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtSrcCopyTen" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlCopyTenderName" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <asp:LinkButton ID="lbtnCopyTender" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" AutoPostBack="True" OnClick="lbtnCopyTender_Click">Copy Data P/U</asp:LinkButton>
                        </div>

                        <div class="col-md-1" style="margin-top: 20px;">
                            <asp:CheckBox ID="Chknoqty" runat="server" Text="Without Qty" CssClass="btn btn-sm btn-primary primaryBtn chkBoxControl" />
                        </div>
                    </div>


                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1Floor" runat="server">
                            <div class="row">


                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <h3 class="card-title mr-auto" runat="server" id="lblFPhaseTitle">Catagory Selection</h3>
                                    <%-- <asp:Label ID="lblFPhaseTitle" runat="server" CssClass="btn btn-success primaryBtn"
                                        Text="Catagory Selection"></asp:Label>--%>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <asp:Label ID="lblcreationdate" runat="server" CssClass="lblTxt lblName" Visible="False" Text="Creation Date:"></asp:Label>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="inputDateBox" Visible="False"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    <asp:LinkButton ID="lbtnUpdatePCDate" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnUpdatePCDate_Click">Update</asp:LinkButton>

                                </div>

                                <div class="col-md-6" style="margin-top: 20px;">
                                    <asp:CheckBox ID="chkFlrShowSelected" runat="server" AutoPostBack="True"
                                        CssClass="chkBoxControl primaryBtn"
                                        OnCheckedChanged="chkFlrShowSelected_CheckedChanged"
                                        Text="Show selected Catagory(s) only" />

                                    <asp:LinkButton ID="lbtnShowSelectedFloor" runat="server" CssClass="btn  btn-primary primaryBtn" OnClick="lbtnShowSelectedFloor_Click">Show Selected</asp:LinkButton>

                                </div>

                            </div>
                            <div class="row">
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

                            </div>
                        </asp:View>
                        <asp:View ID="View2Item" runat="server">

                            <div class="row">




                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlgroupwrk" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="dddlgroupwrk_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="Desc. of Items"></asp:Label>
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="srearchBtn" runat="server" OnClick="ImgbtnFindItem_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtItemSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectItem" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnSelectItem_Click" TabIndex="4">Select Item</asp:LinkButton>

                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;" runat="server" id="divlbtnSelectMultiItem">
                                    <asp:LinkButton ID="lbtnSelectMultiItem" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectMultiItem_Click">Multi Item</asp:LinkButton>

                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblFloor1" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesizeen" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesizeen_SelectedIndexChanged">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="800">800</asp:ListItem>
                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                            <asp:ListItem Value="1200">1200</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 25px;">
                                    <%--<h3 class="card-title mr-auto">Item Selection (All Catagory)</h3>--%>
                                    <asp:HyperLink ID="lnkbtnAnalysis" runat="server" Target="_blank" NavigateUrl="~/F_04_Bgd/BgdStdAna.aspx" CssClass="btn btn-xs btn-success" ToolTip="Analysis" BackColor="transparent"><span class="fa fa-plus" aria-hidden="true" style="color:blue"></span></asp:HyperLink>

                                </div>

                            </div>



                            <hr class=" hrline" />
                            <div id="divGridView">
                                <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                                    OnRowCancelingEdit="gvAnalysis_RowCancelingEdit" CssClass=" table-striped  table-bordered grvContentarea"
                                    OnRowDataBound="gvAnalysis_RowDataBound" OnRowEditing="gvAnalysis_RowEditing"
                                    OnRowUpdating="gvAnalysis_RowUpdating"
                                    DataKeyNames="isirdesc1" ViewStateMode="Enabled" AllowSorting="true" OnSorting="gvAnalysis_Sorting"
                                    ShowFooter="True" OnRowDeleting="gvAnalysis_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-7 text-center">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="SL"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-center">
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" ToolTip="Export Excel"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowDeleteButton="True" />--%> <%--<span class="fa fa-file-excel-o"></span>--%>
                                        <asp:TemplateField ShowHeader="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelWork" runat="server" OnClick="lbtnDelWork_Click" ToolTip="Delete Work" Font-Size="X-Small" OnClientClick="javascript:return FunConfirm();"><i class="fa fa-trash" style="color:red"> </i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item" SortExpression="isirdesc">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchItmDesc" BackColor="Transparent" CssClass="form-control form-control-sm" BorderStyle="None" runat="server" placeholder="Description of Item" onkeyup="Search_Gridview(this,2, 'gvAnalysis')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmDesc" runat="server" Font-Size="X-Small"
                                                    Text='<%# "<span class=grditem>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "misirdesc")) + "</span>"+
                                                                         (DataBinder.Eval(Container.DataItem, "isirdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "misirdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")).Trim(): "")   %>'
                                                    Width="350px"></asp:Label>




                                                <%--<asp:Label ID="Label14" runat="server" Font-Bold="True" Style="display: none;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblgvItmDesc_e" runat="server" Font-Bold="True" Font-Size="X-Small"
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
                                                                    <asp:TemplateField HeaderText="Catatgory Desc">
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
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <div class="input-group input-group-alt">
                                                            <asp:TextBox ID="txtIgrdtemSearch" runat="server" Font-Size="X-Small" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <asp:LinkButton ID="ImgbtngrdFindItem" runat="server" CssClass="input-group-text" OnClick="ImgbtngrdFindItem_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <td class="auto-style2"></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmUnit" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" Font-Size="X-Small" OnClick="lbtnDelete_Click" ToolTip="Budget Qty Zero" OnClientClick="javascript:return FunConfirm();"><i class="fa fa-trash"> </i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Budgeted Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQty" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="100px" CssClass="style101"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEdited" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "edited")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DateTime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpostdat" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdat")).Length==0?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "postdat")).ToString("dd-MMM-yyyy hh:mm tt")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpostid" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAddNotes" runat="server" OnClick="lbtnAddNotes_Click" ToolTip="Add Notes"><i class="fas fa-comment-alt-plus"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNotes" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
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
                        <asp:View ID="View3Item" runat="server">
                            <div class="col-md-2">
                                <h3 class="card-title mr-auto">Item Selection (Individual Catagory)</h3>

                            </div>

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
                                    <asp:DropDownList ID="ddlFloorListToCopy" runat="server" CssClass="form-control inputTxt chzn-select " TabIndex="3">
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
                                    <asp:DropDownList ID="ddlItem2" runat="server" CssClass="form-control  inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">

                                    <asp:LinkButton ID="lbtnSelectItem2" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectItem2_Click" TabIndex="4" Style="margin-left: -50px;">Select Item </asp:LinkButton>

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
                                            <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmUnit" runat="server" Font-Size="X-Small"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server" BorderColor="#99CCFF" Font-Size="X-Small"
                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
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

                            <div class="row mt-2">

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblTitle2" runat="server" CssClass="control-label" Text="Res. Rate Input"></asp:Label>
                                        <asp:TextBox ID="txtSearchItem" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                            Font-Bold="True" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged"
                                            Width="70px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnSelectFloorRes_Click">Show Item</asp:LinkButton>
                                </div>


                            </div>
                            <div class="row">

                                <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvResInfo_PageIndexChanging" DataKeyNames="rsirdesc" ViewStateMode="Enabled" AllowSorting="true" OnSorting="gvResInfo_Sorting"
                                    PageSize="20" Width="16px" ShowFooter="True" OnRowDataBound="gvResInfo_RowDataBound" OnRowUpdating="gvResInfo_RowUpdating">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
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

                                        <asp:TemplateField HeaderText="Floor" SortExpression="flrdes">
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkProjectLock" runat="server"
                                                    Text="Project Lock" Width="90px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfloordes" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group">
                                            <ItemTemplate>
                                                <%--lblgvsirdescrinput--%>
                                                <asp:Label ID="lblgvsirdescrinput" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Resource" SortExpression="rsirdesc">
                                            <FooterTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style173">
                                                            <asp:CheckBox ID="chklkrate" runat="server"
                                                                Text="Lock Rate" />
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnUpdateResRate" runat="server"
                                                                Font-Size="14px" OnClick="lbtnUpdateResRate_Click" Style="text-align: center; height: 17px;"
                                                                Width="80px">Update Rate</asp:LinkButton>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                    Width="300px"></asp:Label>
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Style="display: none;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="300px"></asp:Label>



                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" SortExpression="rsirunit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Est.Qty" SortExpression="tresqty">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnSameValue" runat="server" Font-Size="12px" OnClick="lbtnSameValue_Click" CssClass="btn btn-primary btn-sm okBtn ">Put Same Val</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res. Rate" SortExpression="bgdrat">
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
                                        <asp:TemplateField HeaderText="Amount" SortExpression="tresamt">
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


                                        <asp:TemplateField HeaderText="Last Purchase Rate" SortExpression="tresamt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTReslstRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lprate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Last Purchase Date" SortExpression="tresamt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTReslstdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lpdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="View5Report" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="Analysis Reports"></asp:Label>
                                        <asp:DropDownList ID="ddlReports" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                                            <asp:ListItem>Resource Basis</asp:ListItem>
                                            <asp:ListItem>Work Basis</asp:ListItem>
                                            <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                            <asp:ListItem>Individual Work Basis</asp:ListItem>
                                            <asp:ListItem>Analysis Sheet</asp:ListItem>
                                            <asp:ListItem>Project Information</asp:ListItem>


                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3" runat="server" id="PnlRptItmList" visible="false">
                                    <div class="form-group">
                                        <asp:Label ID="lblItem9" runat="server" CssClass="control-label" Text="Item"></asp:Label>
                                        <asp:LinkButton ID="ImgbtnRptFindItem" CssClass="srearchBtn" runat="server" OnClick="ImgbtnRptFindItem_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtRptItemSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlRptItem" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3" runat="server" id="PnlRptResList" visible="false">
                                    <div class="form-group">
                                        <asp:Label ID="lblItem12" runat="server" CssClass="control-label" Text="Resource"></asp:Label>
                                        <asp:LinkButton ID="ImgbtnRptFindRes" CssClass="srearchBtn" runat="server" OnClick="ImgbtnRptFindRes_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtRptResSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlRptRes" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" TabIndex="3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="control-label" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblFloor" runat="server" CssClass="control-label" Text="Catagory"></asp:Label>
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnShowReport" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnShowReport_Click">Show Report</asp:LinkButton>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblRptMainGroup" runat="server" CssClass="control-label" Text="Main Group" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlRptMainGroup" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" Visible="false">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
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


                            <asp:Panel runat="server" ID="pnlAllRpt" Visible="false">
                                <div class="row">
                                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="847px" ShowFooter="True" OnRowDataBound="gvRptResBasis_RowDataBound" DataKeyNames="rptdesc1" ViewStateMode="Enabled" AllowSorting="true" OnSorting="gvRptResBasis_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                        Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Catagory" SortExpression="flrdes">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                        Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsirdesc1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" SortExpression="rptdesc1">
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
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Style="display: none;" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" SortExpression="rptunit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" SortExpression="rptqty">
                                                <FooterTemplate>
                                                    <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bgd. Rate" SortExpression="rptrat">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" SortExpression="rptamt">
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
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
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

                                            <asp:TemplateField HeaderText="Mat. Rate" SortExpression="matrat" Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lbfttmat" runat="server" Font-Size="Small"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptmatrat" runat="server" Font-Bold="False" Font-Size="12px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lab. Rate" SortExpression="labrat" Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lbfttlabrat" runat="server" Font-Size="Small"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptlabrat" runat="server" Font-Bold="False" Font-Size="12px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "labrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
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
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlPrjInfo" Visible="false">
                                <div class="row">
                                    <asp:GridView ID="gvprjInf" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="640px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                        Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "") %>'
                                                        Width="270px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvval" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        </asp:View>
                        <asp:View ID="SpReport" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <h3 class="card-title mr-auto">Special Reports</h3>

                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" CssClass="control-label" Text="Floor"></asp:Label>
                                        <asp:DropDownList ID="ddlFlrlstspr" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="control-label" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroupspr" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" CssClass="control-label" Text="Work"></asp:Label>
                                        <asp:DropDownList ID="ddlRptWork" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                            <asp:ListItem Selected="True">All Work</asp:ListItem>
                                            <asp:ListItem>Civil</asp:ListItem>
                                            <asp:ListItem>Sanitary</asp:ListItem>
                                            <asp:ListItem>Electrical</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm custom-select"
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
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnShowSpRpt" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnShowSpRpt_Click">Show Report</asp:LinkButton>
                                </div>
                            </div>

                            <div class="row">

                                <asp:GridView ID="gvSpRpt" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvSpRpt_PageIndexChanging" ShowFooter="True" Width="847px">
                                    <PagerSettings Position="Top" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Catagory">
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
                            </div>

                        </asp:View>


                        <asp:View ID="Viewbgdcostres02" runat="server">
                            <div class="row" runat="server" id="details" visible="false">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="control-label" Text=""></asp:Label>
                                        <asp:DropDownList ID="ddldetails" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select">
                                            <asp:ListItem Value="Main" Text="Main"> </asp:ListItem>
                                            <asp:ListItem Value="Sub" Text="Sub"></asp:ListItem>
                                            <asp:ListItem Value="Details" Text="Details" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>



                                <div class="col-md-1">
                                    <asp:LinkButton ID="lbtnshow" runat="server" CssClass="btn btn-success primaryBtn" OnClick="lbtnshow_OnClick">Show</asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvRptResBasis02" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="633px" ShowFooter="True" OnRowDataBound="gvRptResBasis02_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resource Description">

                                            <HeaderTemplate>
                                                <table style="width: 300px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Resource Description" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRes02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                    Width="300px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lbftTqty02" runat="server" Font-Size="Small"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptQty02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRat02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>

                                                <asp:Label ID="lblgvFTotalCost02" runat="server" Font-Bold="True"
                                                    Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>



                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptAmt02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percentage">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFPercent02" runat="server" Font-Bold="True" Font-Size="12px"
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
                                                <asp:Label ID="lblgvPer02" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" />
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


                    </asp:MultiView>
                </div>
            </div>

            <div class="modal fade " id="MulitSelectModal" role="dialog">
                <div class="modal-dialog  modal-lg " style="width: 500px;">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h3><i class="fa fa-table"></i>Select Multiple Items</h3>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive col-12">
                                <div class="row" style="max-height: 350px">
                                    <asp:GridView ID="gvMultiSelectItem" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsiSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsiItemCode" runat="server" Font-Size="X-Small"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Description">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchItmDesc" BackColor="Transparent" CssClass="form-control form-control-sm" BorderStyle="None" runat="server" placeholder="Item Description" onkeyup="Search_Gridview(this,1,'gvMultiSelectItem')"></asp:TextBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsiItemDesc" runat="server" Font-Size="X-Small"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCol" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
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
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbtnUpdateMultiItem" runat="server" class="btn btn-success" aria-hidden="true" OnClick="lbtnUpdateMultiItem_Click" OnClientClick="CloseMulitSelectModal();">Update</asp:LinkButton>
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <%--Notes Modal Start--%>
            <div class="modal fade " id="noteDetails" role="dialog">
                <div class="modal-dialog  modal-lg ">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h3 class="modal-title text-center">Note Details</h3>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label class="control-label" style="font-size: large;">Details:</label>
                                <asp:TextBox ID="txtpactcode" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtisircode" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtNoteDetails" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbtnUpdateNotes" runat="server" class="btn btn-success" aria-hidden="true" OnClick="lbtnUpdateNotes_Click" OnClientClick="closeNotesModal();">Update</asp:LinkButton>
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <%--Notes Modal End--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

