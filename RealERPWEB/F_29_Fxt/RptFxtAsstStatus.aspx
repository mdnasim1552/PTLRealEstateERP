<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptFxtAsstStatus.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFxtAsstStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .mt22 {
            margin-top: 22px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

            <div class="card card-fluid mb-1">
                <div class="card-body mb-0">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="row mt-2">
                                    <div class="col-md-2.5" style="text-align: center;">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#DAEAD3"
                                                RepeatColumns="6" RepeatDirection="Horizontal" ForeColor="Black" CssClass="rbtnList1">
                                                <asp:ListItem>With Details</asp:ListItem>
                                                <asp:ListItem>Without Details</asp:ListItem>
                                                <asp:ListItem>With Value</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 mt-1">
                                        <div class="form-group">
                                            <asp:CheckBox ID="ChkBalance" runat="server" CssClass="checkBox " Text="Without Zero Balance" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtSrcProject" Visible="false" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to" Text="Group"></asp:Label>
                                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class=" table-responsive">
                                <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    Style="text-align: left" Width="810px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid0" runat="server" Style="text-align: center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgpactdesc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receive Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReceiveDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receive Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQty" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTrnsDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnsdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTqty" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRentPerday" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFoterAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />

                                </asp:GridView>
                            </div>

                        </asp:View>


                        <asp:View ID="Depreciation" runat="server">
                            <asp:Panel ID="Panel3" runat="server">
                                <div class="row">
                                    <div class="col-md-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-1 ">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnShow_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-8  pading5px  asitCol8">
                                        <asp:Label ID="txtDays" Visible="false" runat="server" CssClass="btn btn-danger btn-sm"></asp:Label>
                                    </div>
                                </div>

                                <asp:GridView ID="grDep" runat="server" AllowPaging="True" CssClass=" table-striped table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                                    ShowFooter="True" Style="text-align: left" Width="810px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl..">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid1" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <HeaderTemplate>                                                
                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                &nbsp;
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>                                                    
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="13px"
                                                    Style="text-align: left" Text="Total :" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance as on ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpening" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                                     Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Addition During The year">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAddition" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTAddition" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Disposal During the year">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalesdec" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalesdec" runat="server" Font-Bold="True" Font-Size="12px"
                                                     Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Revaluation During The Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdisposal" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDisposal" runat="server" Font-Bold="True" Font-Size="12px"
                                                     Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Balance as on">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTotal" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate Of Dep. %">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDepPar" runat="server"
                                                    Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Depreciation as on">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDepOpen" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDepOpen" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Additon During The year ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDepCur" runat="server"
                                                    Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDepCur" runat="server" Font-Bold="True" Font-Size="12px"
                                                     Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Disposal during the year ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvadjment" runat="server"
                                                    Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFadjment" runat="server" Font-Bold="True" Font-Size="12px"
                                                     Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance as at">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDepTotal" runat="server"
                                                    Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTDepTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="W.D Values as on ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCBal" runat="server"
                                                    Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTCBal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:Panel>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>




            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                    </div>


                    <%--<fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel4" runat="server">

                            </asp:Panel>
                        </div>
                    </fieldset>--%>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
