<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurLabRequisition02.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurLabRequisition02" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>


    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <style>
        .multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        #ContentPlaceHolder1_divgrp {
            width: 395px !important;
        }



        .mt20 {
            margin-top: 20px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            var gvisu = $('#<%=this.grvissue.ClientID %>');
            $.keynavigation(gvisu);
            // gvisu.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });

            $(function () {
                //$('[id*=lstfloor]').multiselect({
                //    includeSelectAllOption: true,
                //    enableCaseInsensitiveFiltering: true,
                //});

            });

            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });


            //gvisu.gridviewScroll({
            //           width: 1165,
            //           height: 420,
            //           arrowsize: 30,
            //           railsize: 16,
            //           barsize: 8,
            //           headerrowcount: 2,
            //           varrowtopimg: "../Image/arrowvt.png",
            //           varrowbottomimg: "../Image/arrowvb.png",
            //           harrowleftimg: "../Image/arrowhl.png",
            //           harrowrightimg: "../Image/arrowhr.png",
            //           freezesize: 5


            //       });

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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblCurNo1" runat="server" Text="Req No"></asp:Label>
                                <asp:Label ID="lblCurISSNo1" runat="server" CssClass="form-control form-control-sm" Text="LRQ00-"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm" TabIndex="3">000</asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Label ID="Label7" runat="server" Text="Ref No"></asp:Label>

                                <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Text="Page"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Style="margin-left: 6px;" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click" Visible="true">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lbtnPrevISSList" runat="server">Prev. List</asp:Label>

                               
                                <asp:TextBox ID="txtSrcPreBill" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnPreBillList" runat="server" OnClick="ibtnPreBillList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="chzn-select form-control  form-control-sm"></asp:DropDownList>

                            </div>
                        </div>





                    </div>
                </div>
            </div>


            <asp:Panel ID="PnlRes" runat="server" Visible="False">
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
                        <asp:Panel ID="Panel3" runat="server">


                            <div class="row">



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCatagory" runat="server" Text="Catagory"></asp:Label>
                                        <asp:DropDownList ID="ddlcatagory" runat="server" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblLabour" runat="server" Text="Labour"></asp:Label>
                                        <asp:DropDownList ID="ddlWorkList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblfloorno" runat="server" Text="Floor No"></asp:Label>
                                        <asp:ListBox ID="lstfloor" runat="server" CssClass="form-control form-control-sm select2" SelectionMode="Multiple"></asp:ListBox>
                                    </div>

                                </div>




                                <div class="col-md-2" style="margin-top: 22px;">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-sm btn-primary">Select</asp:LinkButton>
                                    </div>
                                </div>
                            </div>




                        </asp:Panel>

                    </div>
                </div>
                <div class="card card-fluid mb-1" >
                    <div class="card-body">
                <div class="row">
                    <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                        CssClass=" table-striped  table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="669px" PageSize="20" OnRowDataBound="grvissue_RowDataBound"
                        OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl #">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lbtnDelItem" OnClick="lbtnDelItem_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>


                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Item Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fl" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvflrCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                        Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnDeleteBill" runat="server" Font-Bold="True" Visible="false"
                                        Font-Size="13px" ForeColor="#000" OnClick="lbtnDeleteBill_Click"
                                        Style="text-align: center;" Width="90px" CssClass="btn btn-primary primarygrdBtn">Delete All</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFloordes" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' Width="90px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">


                                <ItemTemplate>
                                    <asp:Label ID="lbllabdesc" runat="server"
                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   %>'
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvTotal" runat="server" Font-Bold="True"
                                        >Total</asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                            <%--   <asp:TemplateField HeaderText="Pre. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpreqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>--%>


                            <%--  <asp:TemplateField HeaderText="MB Book">

                                        <FooterTemplate>
                                            
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmbbook" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbbook")) %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: left"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>--%>



                            <asp:TemplateField HeaderText="Budgeted Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbgdqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="60px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Budgeted Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblschrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>




                            <%--      <asp:TemplateField HeaderText="Work Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField--%>







                            <asp:TemplateField HeaderText="Bal.Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblbalqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <%--  <asp:TemplateField HeaderText="Issued % on Budget">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperobgd" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>--%>


                            <%--<asp:TemplateField HeaderText="Work Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtwrkqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px"  BackColor="Transparent"
                                                BorderStyle="None"  Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>--%>


                            <asp:TemplateField HeaderText="Balance Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblbalamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px" BackColor="Transparent"
                                        BorderStyle="None" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>






                            <%--<asp:TemplateField HeaderText="%">
                                        
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtpercentge" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>--%>





                            <asp:TemplateField HeaderText="Qty">
                                <%--<FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>--%>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtisuqty" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                        Width="70px" BackColor="Transparent"
                                        BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtlabrate" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="60px" BackColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                        Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contractor" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="LblGvContractor" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>'> </asp:Label>
                                    <asp:DropDownList ID="DdlContractor" runat="server" CssClass="form-control" Width="130px" OnSelectedIndexChanged="DdlContractor_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAllapproved" runat="server" TabIndex="10" Text="Approved" CssClass="btn checkBox" AutoPostBack="True" OnCheckedChanged="chkAllapproved_CheckedChanged" Width="20px" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkapproved" runat="server" Width="80px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="" />
                        <RowStyle CssClass="grvRowsNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
                </div>
                        </div>
                </div>
                <div class="card card-fluid mb-1" style="min">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Panel ID="PnlNarration" runat="server">

                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblNaration" runat="server" Text="Narration:"></asp:Label>
                                                <asp:TextBox ID="txtISSNarr" runat="server" CssClass="form-control from-control-sm" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblTrade" runat="server" Text="Trade"></asp:Label>
                                                <asp:DropDownList ID="ddltrade" runat="server" CssClass="form-control from-control-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>

                                                <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>

                                                <asp:Label ID="lblApprovalDate" runat="server" Text="Approv.Date:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" Visible="False" CssClass="form-control from-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>








                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True"
                        OnRowDataBound="gvMSRInfo2_RowDataBound" OnRowCreated="gvMSRInfo2_RowCreated">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRSlNo2" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="rsircode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrsircode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvspcfcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Materials Description ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRResDesc" runat="server"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="150px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRResUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRFlrcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRFlrdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Requirement">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="55px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BOQ Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvMSRbgdrat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate1" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">


                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt1" runat="server" Width="70"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate2" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt2" runat="server" Width="70"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>



                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate3" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt3" runat="server" Width="70"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>



                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate4" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt4" runat="server" Width="70"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="RIght" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate5" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">

                                <FooterTemplate>
                                    <asp:Label ID="lgvFamt5" runat="server" Width="70"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmount5" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>



                                <FooterStyle HorizontalAlign="RIght" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Previous App.rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblaprovrate" runat="server"
                                        Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))=="0.00")?"" :Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))  %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>





                    <asp:GridView ID="gvterm" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRSlNo3" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Suppliercode">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvssircode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Supplier">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMSRResUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Discount (Amt)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvDiscount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Carring Charge (Amt)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvccharge" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "ccharge").ToString() %>'
                                        Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment Term">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvpayterm" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "payterm").ToString() %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Quotation Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCurQuTDate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: center; background-color: Transparent"
                                        Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).Year==1900?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).ToString("dd-MMM-yyyy")) %>'>
                                                Width="80px">
                                    </asp:TextBox>

                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Working Time">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtworkline" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "worktime").ToString() %>'
                                        Style="text-align: left; background-color: Transparent"
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Notes">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNotes" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "notes").ToString() %>'
                                        Style="text-align: left; background-color: Transparent"
                                        Width="180px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
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
            <asp:Label ID="lblBillno" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

