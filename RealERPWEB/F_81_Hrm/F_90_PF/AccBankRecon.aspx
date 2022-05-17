<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccBankRecon.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccBankRecon" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
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
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Bank Name:"></asp:Label>


                                <asp:DropDownList ID="DDListBank" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Date Range"></asp:Label>

                                <asp:TextBox ID="TxtDate1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" Text="To:"></asp:Label>

                                <asp:TextBox ID="TxtDate2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
   

                        <div class="col-lg-2">
                             <div class="form-group mt20">
                            <asp:Label ID="LblReportTitle" runat="server" CssClass="" Text="BANK RECONCILIATION"></asp:Label>
                        </div>
                        </div>
                        <div class="col-lg-2">
                             <div class="form-group mt20">
                            <asp:Label ID="LblReportPeriod" runat="server" CssClass="mt20" Text="Reporting Period"></asp:Label>
                                                         </div>
                        </div>
                                             <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnGetData" runat="server" CssClass="btn btn-primary mt20" OnClick="lbtnGetData_Click">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" BackColor="#000" BorderColor="#000"
                            BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
                            GridLines="None" OnPageIndexChanging="gv1_PageIndexChanging"
                            OnRowCancelingEdit="gv1_RowCancelingEdit" OnRowEditing="gv1_RowEditing"
                            OnRowUpdating="gv1_RowUpdating" Width="859px">
                            <PagerSettings Position="Top" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblACTCODE" runat="server" __designer:wfdid="w1"
                                            Font-Size="11px" Style="font-size: 10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/c C.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCACTCODE" runat="server" Font-Size="11px"
                                            Style="font-size: 10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                            Style="font-size: 10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVOUNUM" runat="server" Font-Size="11px"
                                            Style="font-size: 10px; text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="87px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRECNDT" runat="server"
                                            Style="font-weight: bold; font-size: 12px; border-top-style: none; border-right-style: none; border-left-style: none; background-color: #99ffff; text-align: center; border-bottom-style: none"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                            Width="100px"></asp:TextBox>
                                        <asp:CheckBox ID="chkVoucher" runat="server" Style="font-size: 12px"
                                            Text="ThisVoucher" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRECNDT" runat="server" Font-Size="11px"
                                            Style="font-weight: bold; font-size: 12px; text-align: center"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheq./ Ref. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                            Style="font-size: 10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="107px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                            Style="font-weight: bold; font-size: 10px; text-align: center"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                            Width="66px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVOUNUM1" runat="server" Font-Size="11px"
                                            Style="font-weight: bold; font-size: 10px; text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="66px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRNAM" runat="server" Font-Size="11px" Style="font-weight: bold; font-size: 11px; text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="84px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction A/c">
                                    <HeaderTemplate>
                                        <table style="width: 44%;">
                                            <tr>
                                                <td class="style63">
                                                    <asp:Label ID="Label8" runat="server" Text="Transaction A/c" Width="160px"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="style61">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                            Style="font-size: 8px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transdes")) %>'
                                            Width="275px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Control A/c">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCTRANSDES" runat="server" Font-Size="11px"
                                            Style="font-size: 8px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ctransdes")) %>'
                                            Width="169px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVENAR" runat="server" Font-Size="11px" Style="font-size: 9px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                            Width="607px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comp Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCOMCOD" runat="server" __designer:wfdid="w3"
                                            Font-Size="11px" Style="font-size: 10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        <div class="form-group">
                            <div class="col-md-6 pading5px asitCol6">
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtDate1" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtDate2" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <%--<fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Bank Name:" ></asp:Label>

                                        <asp:TextBox ID="txtBankSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lbtnGetBankList" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnGetBankList_Click">Find</asp:LinkButton>

                                        <asp:DropDownList ID="DDListBank" runat="server"  CssClass="ddlPage" Width="250px"></asp:DropDownList>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Date Range" ></asp:Label>

                                        <asp:TextBox ID="TxtDate1" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                         <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to"  Text="To:" ></asp:Label>

                                        <asp:TextBox ID="TxtDate2" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lbtnGetData" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnGetData_Click">Ok</asp:LinkButton>

                                       <asp:Label ID="lblError" runat="server" CssClass=" btn btn-danger primaryBtn"   ></asp:Label>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="LblReportTitle" runat="server" CssClass="lblTxt lblName" Text="BANK RECONCILIATION"></asp:Label>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="LblReportPeriod" runat="server" CssClass="lblTxt lblName" Text="Reporting Period" ></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </fieldset>--%>

            <div class="table table-responsive">
                <asp:GridView ID="gv1" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" BackColor="#000" BorderColor="#000"
                    BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
                    GridLines="None" OnPageIndexChanging="gv1_PageIndexChanging"
                    OnRowCancelingEdit="gv1_RowCancelingEdit" OnRowEditing="gv1_RowEditing"
                    OnRowUpdating="gv1_RowUpdating" Width="859px">
                    <PagerSettings Position="Top" />

                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="serialnoid" runat="server"
                                    Style="text-align: right; font-size: 11px;"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="14px" />
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True"></asp:CommandField>
                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblACTCODE" runat="server" __designer:wfdid="w1"
                                    Font-Size="11px" Style="font-size: 10px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                    Width="20px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A/c C.Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblCACTCODE" runat="server" Font-Size="11px"
                                    Style="font-size: 10px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                    Width="20px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                    Style="font-size: 10px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                    Width="20px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblVOUNUM" runat="server" Font-Size="11px"
                                    Style="font-size: 10px; text-align: center"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                    Width="87px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRECNDT" runat="server"
                                    Style="font-weight: bold; font-size: 12px; border-top-style: none; border-right-style: none; border-left-style: none; background-color: #99ffff; text-align: center; border-bottom-style: none"
                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                    Width="100px"></asp:TextBox>
                                <asp:CheckBox ID="chkVoucher" runat="server" Style="font-size: 12px"
                                    Text="ThisVoucher" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRECNDT" runat="server" Font-Size="11px"
                                    Style="font-weight: bold; font-size: 12px; text-align: center"
                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cheq./ Ref. No.">
                            <ItemTemplate>
                                <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                    Style="font-size: 10px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                    Width="107px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vou.Date">
                            <ItemTemplate>
                                <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                    Style="font-weight: bold; font-size: 10px; text-align: center"
                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                    Width="66px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vou.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblVOUNUM1" runat="server" Font-Size="11px"
                                    Style="font-weight: bold; font-size: 10px; text-align: center"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                    Width="66px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblTRNAM" runat="server" Font-Size="11px" Style="font-weight: bold; font-size: 11px; text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                    Width="84px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction A/c">
                            <HeaderTemplate>
                                <table style="width: 44%;">
                                    <tr>
                                        <td class="style63">
                                            <asp:Label ID="Label8" runat="server" Text="Transaction A/c" Width="160px"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="style61">&nbsp;</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                    Style="font-size: 8px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transdes")) %>'
                                    Width="275px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Control A/c">
                            <ItemTemplate>
                                <asp:Label ID="lblCTRANSDES" runat="server" Font-Size="11px"
                                    Style="font-size: 8px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ctransdes")) %>'
                                    Width="169px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Narration" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblVENAR" runat="server" Font-Size="11px" Style="font-size: 9px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                    Width="607px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comp Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblgvCOMCOD" runat="server" __designer:wfdid="w3"
                                    Font-Size="11px" Style="font-size: 10px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                    Width="20px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
                <div class="form-group">
                    <div class="col-md-6 pading5px asitCol6">
                        <cc1:CalendarExtender ID="ClndrExtDate1" runat="server" TargetControlID="TxtDate1" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="ClndrExtDate2" runat="server" TargetControlID="TxtDate2" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </div>
                </div>
            </div>




            <%--<tr>
                        <td style="width: 5px"></td>
                        <td style="width: 3px">
                            <asp:Label Style="font-size: 16px; vertical-align: top; text-align: right"
                                ID="Label2" runat="server" Width="120px" Text="Bank Name :" Font-Bold="True"
                                ForeColor="#000"></asp:Label></td>
                        <td style="width: 6px"></td>
                        <td style="width: 19px">
                            <asp:TextBox ID="txtBankSearch" runat="server" Width="80px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                        <td>
                            <asp:LinkButton Style="font-size: 14px; color: #FFFFFF;" ID="lbtnGetBankList"
                                OnClick="lbtnGetBankList_Click" runat="server" Width="28px">Find</asp:LinkButton></td>
                        <td colspan="4">
                            <asp:DropDownList Style="font-weight: bold; font-size: 12px" ID="DDListBank" runat="server" Width="587px" Font-Bold="False">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="DDListBank_ListSearchExtender" runat="server"
                                Enabled="True" QueryPattern="Contains" TargetControlID="DDListBank">
                            </cc1:ListSearchExtender>
                        </td>
                        <td style="width: 3px"></td>
                    </tr>--%>
            <%--<tr>
                        <td style="width: 5px"></td>
                        <td style="width: 3px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                Style="font-size: 16px; vertical-align: top; text-align: right"
                                Text="Date Range :" Width="120px" ForeColor="#000"></asp:Label>
                        </td>
                        <td style="width: 6px"></td>
                        <td style="width: 19px">
                            <asp:TextBox ID="TxtDate1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                Style="font-size: 16px; vertical-align: top; text-align: right" Text="To:"
                                Width="30px" ForeColor="#000"></asp:Label>
                        </td>
                        <td style="width: 8px">
                            <asp:TextBox ID="TxtDate2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="width: 96px">
                            <asp:LinkButton ID="lbtnGetData" runat="server" Font-Bold="True"
                                Font-Size="18px" OnClick="lbtnGetData_Click"
                                Style="font-size: 14px; text-align: center; color: #FFFFFF;" Width="50px">OK</asp:LinkButton>
                        </td>
                        <td style="width: 285px">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True"
                                Width="400px" Font-Size="12px"
                                ForeColor="Yellow"></asp:Label>
                        </td>
                        <td style="width: 124px"></td>
                        <td style="width: 3px"></td>
                    </tr>--%>

            <%--<tr>
                        <td style="width: 5px"></td>
                        <td>
                            <asp:Label ID="LblReportTitle" runat="server" Font-Bold="True"
                                Font-Overline="False" Font-Size="14px" Font-Underline="True"
                                Style="font-weight: bold; font-size: 16px; text-align: center"
                                Text="BANK RECONCILIATION" Width="842px" ForeColor="#000"></asp:Label>
                        </td>
                        <td style="width: 3px"></td>
                    </tr>--%>
            <%--<tr>
                        <td style="width: 5px">&nbsp;</td>
                        <td>
                            <asp:Label ID="LblReportPeriod" runat="server" Font-Bold="True"
                                Font-Overline="False" Font-Size="12px" Font-Underline="False"
                                Style="font-weight: bold; font-size: 15px; text-align: center"
                                Text="Reporting Period" Width="842px" ForeColor="#000"></asp:Label>
                        </td>
                        <td style="width: 3px">&nbsp;</td>
                    </tr>--%>
            <%--        
            <cc1:CalendarExtender ID="ClndrExtDate1" runat="server" TargetControlID="TxtDate1" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
            <cc1:CalendarExtender ID="ClndrExtDate2" runat="server" TargetControlID="TxtDate2" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

