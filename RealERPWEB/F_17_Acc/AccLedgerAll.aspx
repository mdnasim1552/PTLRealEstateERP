<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccLedgerAll.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccLedgerAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 233px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 250px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .rbtnledger tbody tr td {
            margin: 0 5px;
        }

            .rbtnledger tbody tr td input[type=checkbox], .rbtnledger tbody tr td input[type=radio] {
                box-sizing: border-box;
                padding: 0;
                margin: 0 0 0 12px;
            }

            .rbtnledger tbody tr td label {
                margin: 0 0 0 5px;
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
            <div class="card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button cssclass="btn btn-secondary" style="border: none;">Ledger Type</button>
                                </div>
                                <asp:RadioButtonList ID="rbtnLedger" runat="server" CssClass="form-control rbtnledger" AutoPostBack="true" OnSelectedIndexChanged="rbtnLedger_SelectedIndexChanged"
                                    RepeatColumns="10" RepeatDirection="Horizontal" Style="text-align: left;">
                                    <asp:ListItem Value="Ledger">Ledger</asp:ListItem>
                                    <asp:ListItem Value="SubLedger">Subsidiary Ledger</asp:ListItem>
                                    <asp:ListItem Value="DetailLedger">Special Ledger</asp:ListItem>
                                    <asp:ListItem Value="DetailLedger02">Special Ledger(02)</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>


                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button cssclass="btn btn-secondary" type="button" style="border: none; background-color: none">Page Size</button>
                                </div>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                    Visible="true" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem Selected="True">600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="card-fluid">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="viewLedger" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">From</button>
                                        </div>
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">To</button>
                                        </div>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">Narration</button>
                                        </div>
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#155273" ForeColor="White" CssClass="form-control rbtnledger"
                                            RepeatColumns="2" RepeatDirection="Horizontal" Style="text-align: left;">
                                            <asp:ListItem>With </asp:ListItem>
                                            <asp:ListItem>Without</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <asp:CheckBox ID="chksum" runat="server" CssClass="form-control checkBox" Text="Sum" />
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkwitoutopn" runat="server" CssClass="form-control checkBox" Text="Witout Opening" />
                                </div>
                            </div>
                            <div class="row" style="margin: 5px 0px 5px 0px;">
                                <div class="col-md-1">
                                    <div class="input-group-prepend " style="float: right; padding-top: 5px">
                                        <label class="lblTxt lblName" style="border: none">Accounts Head</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group input-group-alt">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">

                                    <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkShowLedger_Click" Style="float: left; margin-top: 2px;">Show</asp:LinkButton>
                                </div>
                                <div class="col-md-5">
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="input-group-prepend " style="float: right; padding-top: 5px">
                                                    <label for="ddlLvType" class="d-block">
                                                        Resource Head    
                                    <asp:LinkButton ID="ibtnFindRes" runat="server" OnClick="ibtnFindRes_Click">  <i class="fa fa-search"> </i> </asp:LinkButton>
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">

                                                <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt chzn-select">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlConAccResHead_ListSearchExtender" runat="server" QueryPattern="Contains" TargetControlID="ddlConAccResHead">
                                                </cc1:ListSearchExtender>
                                            </div>
                                            <div class="col-md-3" style="float: left;">
                                                <asp:CheckBox ID="chkqty" runat="server" CssClass="form-control checkBox" Text="With qty" />
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="table-responsive row">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgv2_RowDataBound" AllowPaging="true" OnPageIndexChanging="dgv2_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="90px"></asp:Label>
                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No.">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                    Font-Underline="False" Target="_blank" __designer:wfdid="w1">
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque/Ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbillNo" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="85px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbillNox" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="85px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL textwrap"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).ToString()=="Narration:  "?(DataBinder.Eval(Container.DataItem, "venar1")  +" "+ DataBinder.Eval(Container.DataItem, "venar2")):"") %>'
                                                    Width="250px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgvtrnqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrate" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgvrate" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Dr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                    Width="100px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="90px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>

                        </asp:View>

                        <asp:View ID="viewSpLedger" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">From</button>
                                        </div>
                                        <asp:TextBox ID="txtDateFromSp" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFromSp" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">To</button>
                                        </div>
                                        <asp:TextBox ID="txtDatetoSp" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatetoSp" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">Narration</button>
                                        </div>
                                        <asp:RadioButtonList ID="rbtsplist" runat="server" BackColor="#155273" ForeColor="White" CssClass="form-control rbtnledger"
                                            RepeatColumns="2" RepeatDirection="Horizontal" Style="text-align: left;">
                                            <asp:ListItem Selected="True">With </asp:ListItem>
                                            <asp:ListItem>Without</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                                <div class="col-md-1">
                                    <asp:CheckBox ID="Checkdaywise" runat="server" CssClass="form-control checkBox" Text="Day Wise" />
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkwithoutopen" runat="server" CssClass="form-control checkBox" Text="Witout Opening" />
                                </div>
                                <%--<div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Resource Heads"></asp:Label>
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="" runat="server" CssClass="btn btn-primary srearchBtn" OnClick=""><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:DropDownList ID="" runat="server" CssClass="form-control inputTxt chzn-select " Width="350px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="colMdbtn">
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="input-group-prepend " style="float: right; padding-top: 5px">
                                        <label for="ddlLvType" class="d-block">
                                            Resource Head    
                                    <asp:LinkButton ID="ibtnFindResSP" runat="server" OnClick="ibtnFindResSP_Click">  <i class="fa fa-search"> </i> </asp:LinkButton>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlRescode" runat="server" CssClass="form-control inputTxt chzn-select">
                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-1" style="margin-top: 2px">
                                    <asp:LinkButton ID="lnkShowSPLedger" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkShowSPLedger_Click">Ok</asp:LinkButton>
                                </div>
                            </div>



                            <div class="table table-responsive">
                                <asp:GridView ID="gvSpledger" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvSpledger_RowDataBound1" AllowPaging="true" OnPageIndexChanging="gvSpledger_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrp" runat="server" CssClass="GridLebelL"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="90px"></asp:Label>

                                                <asp:HyperLink ID="hlbtnCBdataExelsp" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrpDescx" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <%--                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No.">

                                            <ItemTemplate>



                                                <asp:HyperLink ID="HLgvvounum" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="75px"></asp:HyperLink>




                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Opening">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOpAmount" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Dr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"> </asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnarration" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="SpecialLedger02" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">From</button>
                                        </div>
                                        <asp:TextBox ID="txtdatefrmsp02" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdatefrmsp02" Enabled="true"></cc1:CalendarExtender>
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <button class="btn btn-secondary" type="button">To</button>
                                        </div>
                                        <asp:TextBox ID="txtDatetosp02" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatetosp02" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="input-group-prepend " style="float: right; padding-top: 5px">
                                        <label for="Label5" class="d-block">
                                            Resource Head    
                                    <asp:LinkButton ID="lnkbtnRessp02" runat="server" OnClick="lnkbtnRessp02_Click">  <i class="fa fa-search"> </i> </asp:LinkButton>

                                        </label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlResoucesp02" runat="server" CssClass="form-control inputTxt chzn-select">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="margin-top: 2px">
                                    <asp:LinkButton ID="lnkShowsp02" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkShowsp02_Click">Ok</asp:LinkButton>

                                </div>

                            </div>
                            <div class="table table-responsive">
                                <asp:GridView ID="gvspleder02" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvspleder02_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvspleder02_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrpsp02" runat="server" CssClass="GridLebelL"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Labelsp02" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No.">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvvounumsp02" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="75px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">

                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="90px"></asp:Label>

                                                <asp:HyperLink ID="hlbtnCBdataExelsp02" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrpDescxsp02" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcatdesc" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>' Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnqty02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgvtrnqty02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrate02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgvrate02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmountsp02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCrAmtsp02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Paid Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmountsp02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDrAmtsp02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"> </asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Due Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClAmountsp02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFClsAmtsp02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>


                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

