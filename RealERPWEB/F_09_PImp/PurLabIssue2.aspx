<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurLabIssue2.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurLabIssue2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .multiselect {
            width: 150px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 400px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.keynavigation.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

                var gvisu = $('#<%=this.grvissue.ClientID %>');
                $.keynavigation(gvisu);
                //gvisu.Scrollable();



            });

            $(function () {
                //$('[id*=DropCheck1]').multiselect({
                //    includeSelectAllOption: true
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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="from-group">
                                <asp:Label ID="lblconList" runat="server" Text="Contractor List"></asp:Label>

                                <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputTxt inpPixedWidth d-none" TabIndex="1"></asp:TextBox>


                                <asp:LinkButton ID="ibtnFindSubConName" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlcontractorlist" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="lblSubContractor" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>


                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblCurNo1" runat="server" Text="Bill ID:"></asp:Label>
                                <asp:Label ID="lblCurISSNo1" runat="server" CssClass="form-control form-control-sm" Text="LIS00-"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-0" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm" TabIndex="3">000</asp:TextBox>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" Text="R/A No:"></asp:Label>
                                <asp:DropDownList ID="ddlRA" runat="server" AutoPostBack="True" CssClass="chzn-select form-control  form-control-sm" OnSelectedIndexChanged="ddlRA_SelectedIndexChanged" TabIndex="18">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="from-group">
                                <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                            </div>

                        </div>
                        <div class="col-md-0" style="margin-top: 22px;">


                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                            </div>


                        </div>
                        <div class="col-md-1">
                            <div class="from-group">
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevISSList" runat="server">Prev. List:</asp:LinkButton>
                                <asp:TextBox ID="txtSrcPreBill" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:LinkButton ID="ibtnPreBillList" runat="server" OnClick="ibtnPreBillList_Click" TabIndex="2"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"></asp:DropDownList>

                            </div>

                        </div>



                    </div>
                </div>
            </div>
            <asp:Panel ID="PnlRes" runat="server" Visible="False">
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
                        <asp:Panel ID="Pnlgrp" runat="server">



                            <div class="row">


                                <div class="col-md-2">
                                    <div class="from-group">
                                        <asp:Label ID="lblgrp" runat="server" Text="Group" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlgroup" runat="server" CssClass="chzn-select form-control  form-control-sm" Visible="false">
                                        </asp:DropDownList>
                                    </div>


                                </div>



                                <div class="col-md-2">

                                    <asp:Label ID="lblCatagory" runat="server" Text="Catagory"></asp:Label>
                                    <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="chzn-select form-control  form-control-sm" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" TabIndex="12" AutoPostBack="True">
                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblLabour" runat="server" CssClass=" smLbl_to" Text="Labour"></asp:Label>
                                    <asp:TextBox ID="txtSearchLabour" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnSearchMaterisl" runat="server" OnClick="ibtnSearchMaterisl_Click" TabIndex="2"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                                    <asp:DropDownList ID="ddlWorkList" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlWorkList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-4">
                                    <asp:Label ID="lblfloorno" runat="server" Text="Floor No"></asp:Label>
                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control form-control-sm select2" SelectionMode="Multiple"></asp:ListBox>

                                </div>
                                <div class="col-md-1" style="margin-top: 22px">
                                    <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-sm btn-primary"
                                        TabIndex="17">Select</asp:LinkButton>
                                </div>

                                <%--<cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" "
                                                MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="150px">
                                            </cc1:DropCheck>--%>


                                <%--  <div class="col-md-3 pading5px asitCol3" style="margin: -20px 0 0 250px; float: left !important;">
                                                <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt  smLbl_to" Text="Page"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Style="margin-left: 6px;" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>--%>
                            </div>






                        </asp:Panel>
                    </div>
                </div>
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
                        <div class="row">
                            <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="649px" PageSize="20" OnRowDataBound="grvissue_RowDataBound"
                                OnRowDeleting="grvissue_RowDeleting" OnPageIndexChanging="grvissue_PageIndexChanging">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
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

                                    <asp:TemplateField HeaderText="Floor Desc.">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnDeleteBill" runat="server" Font-Bold="True"
                                                Font-Size="13px" ForeColor="#000" OnClick="lbtnDeleteBill_Click"
                                                Style="text-align: center;" Width="80px" CssClass="btn btn-primary primarygrdBtn">Delete All</asp:LinkButton>
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
                                                Width="240px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" Font-Size="14px"
                                                ForeColor="#000" OnClick="lnkTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="MB Book">

                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtmbbook" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbbook")) %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: left"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budgeted Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblschrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Work Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblwrkrate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>







                                    <asp:TemplateField HeaderText="Bal.Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issued % on Budget">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperobgd" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Work Qty">


                                        <ItemTemplate>
                                            <asp:TextBox ID="txtwrkqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="%">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtpercentge" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Previous Rate" Visible="False">


                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprerate" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prevrat")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pre. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpreqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtisuqty" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Qty" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtoisuqty" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                Width="70px" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Rate">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkApproved" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkApproved_Click" Visible="false">Approved</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtlabrate" runat="server" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px" BackColor="Transparent"
                                                BorderStyle="None" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="70px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                                Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Above" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvabove" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "above")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFissueamt" runat="server" Style="text-align: right"
                                                Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtissueamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                Width="70px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="right" />
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
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Panel runat="server" ID="pnlsecurity">

                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblsecurity" runat="server" Text="Security Deposit:"></asp:Label>
                                                <asp:TextBox ID="txtpercentage" runat="server" CssClass="form-control form-control-sm"  Text=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lbtnDepost" runat="server" OnClick="lbtnDepost_Click">Amt</asp:LinkButton>
                                                <asp:TextBox ID="txtSDAmount" runat="server" CssClass="form-control form-control-sm" Style="text-align: right;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">

                                                <asp:Label ID="lbldeduction" runat="server"
                                                    Text="Deduction" CssClass=" smLbl_to"></asp:Label>
                                                <asp:TextBox ID="txtDedAmount" runat="server" CssClass="form-control form-control-sm" Style="text-align: right;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">

                                                <asp:Label ID="lblpenalty" runat="server"
                                                    Text="Penalty" CssClass=" smLbl_to"></asp:Label>
                                                <asp:TextBox ID="txtPenaltyAmount" runat="server" CssClass="form-control form-control-sm" Style="width: 60px; text-align: right;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">

                                                <asp:Label ID="lblAdvanced" runat="server"
                                                    Text="Advanced" CssClass=" smLbl_to"></asp:Label>
                                                <asp:TextBox ID="txtAdvanced" runat="server" CssClass="form-control form-control-sm" Style="width: 60px; text-align: right;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">

                                                <asp:Label ID="lblReward" runat="server"
                                                    Text="Reward" CssClass=" smLbl_to"></asp:Label>
                                                <asp:TextBox ID="txtreward" runat="server" CssClass="form-control form-control-sm" Style="width: 60px; text-align: right;"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">

                                                <asp:Label ID="lblnettotal" runat="server"
                                                    Text="Net Total:" ></asp:Label>

                                                <asp:Label ID="lblvalnettotal" runat="server" CssClass="form-control form-control-sm" Style="text-align: right; color: blue;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:HyperLink ID="lbtnBalance" runat="server" Target="_blank" Style="margin-left: 10px; color: blue; font-weight: bold; font-size: 14px;"></asp:HyperLink>

                                            </div>

                                        </div>


                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="card card-fluid mb-1 mt-1">
                    <div class="card-body">
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
            </asp:Panel>
            <asp:Label ID="lblBillno" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

