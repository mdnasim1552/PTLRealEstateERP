<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptReceivedList02.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptReceivedList02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>



    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <style>
        
        
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }
        
        
        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript">



        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            try {


              
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>

                <%--   var dgvAccRec02 = $('#<%=this.dgvAccRec02.ClientID %>');

            dgvAccRec02.gridviewScroll({
                width: 1160,
                height: 420,             
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });--%>

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gv_YCollectionDetails",
                //    width: 1600,
                //    height: 300,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 5,
                //});

                //gridViewScroll.enhance();


                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('.chzn-select').chosen({ search_contains: true });

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

            }
            catch (e)
            {
                alert(e.message);

            }
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
            <div class="card mt-3">
                <div class="card-header  pt-2 pb-2">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control-sm form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                    CssClass=" form-control-sm form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="prjname" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName">Project Name
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox runat="server" ID="DropCheck1" CssClass="form-control select2 " SelectionMode="Multiple" AutoPostBack="true"></asp:ListBox>
                            </div>
                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblAmount0" runat="server" Text="Amount:" CssClass="  lblTxt lblName"></asp:Label>
                                <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass="form-control chzn-select form-control-sm  " TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" Width="209px">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                    <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                    <asp:ListItem Value="between">Between</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lbl1" runat="server" CssClass=" smLbl_to blName lblTxt">Balance</asp:Label>
                                <asp:TextBox ID="txtAmountC1" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblToCash" runat="server" CssClass=" smLbl_to blName lblTxt" Text="To:" Visible="false"></asp:Label>

                                <asp:TextBox ID="txtAmountC2" runat="server" Visible="false" CssClass="form-control-sm  form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 mt-3">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm " OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to " Text="Size :"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" CssClass="form-control form-control-sm  chzn-select " runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>4000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                    <asp:ListItem>7000</asp:ListItem>
                                    <asp:ListItem>8000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" runat="server" id="salestatus">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Sales Status" CssClass="  lblTxt lblName"></asp:Label>
                                <asp:DropDownList ID="ddlsalestatus" runat="server" CssClass="form-control chzn-select form-control-sm  " TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" Width="209px">

                                    <asp:ListItem Value="Current">Current Sales</asp:ListItem>
                                    <asp:ListItem Value="Previous">Previous Sales</asp:ListItem>
                                    <asp:ListItem Value="Total" Selected="True">Total</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-6 mt-4 d-none" runat="server" id="Paydate">
                            <div class="form-group">
                                <asp:CheckBox ID="chkPayDateWise" runat="server" TabIndex="10" Text="Pay Date Wise" CssClass="btn btn-primary btn-sm checkBox" />

                            </div>
                        </div>

                    </div>


                </div>
                <div class="card-body" style="min-height:400px;">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="dgvAccRec" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="dgvAccRec_PageIndexChanging" ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    Width="654px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pr.Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAccCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgacuname" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgudesc0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "salsdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person">
                                            <FooterTemplate>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgCper" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvAcAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvRecAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvBalAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTIns" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toinstall")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To be Paid Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTBPaid" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPaid" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidins")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcurinsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurinsamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "procolam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
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
                        <asp:View ID="ViewRecList02" runat="server">

                            <div class="table-responsive">
                                <asp:GridView ID="dgvAccRec02" OnRowCreated="dgvAccRec02_RowCreated" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="dgvAccRec02_PageIndexChanging" ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    Width="654px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Project Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdesc02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Project Name">

                                            <HeaderTemplate>
                                                <table style="width: 150px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Project Name" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="80px">Export Excel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgactdesc02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgacuname02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvphone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile"))
                                                                       %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Unit Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgudesc01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flat Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFunitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunitsize" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFavgrate" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apartment Price">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFaptcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvaptcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Parking">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcpcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcpcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Utility ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFutilitycost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvutilitycost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Others">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFothcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvothescost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtocost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtocsot" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Received </br> Up to Last Month">
                                            <FooterTemplate>
                                                <asp:HyperLink ID="hlnkgvFtoreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Target="_blank" Style="text-align: right" Width="100px"></asp:HyperLink>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotreceived" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFatodues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvatodues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dues Upto Dec">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotaldues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotaldues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues Balance">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtodues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtodues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Booking">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpbooking" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtodues0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Installment">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpinstallment" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtodues1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpretodues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtodues2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Booking ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCbooking" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCbooking" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Installment ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCinstallment" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCinstallment" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Current Dues ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoCInstalment" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCoCInstalment" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dues & Overdues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFvtodues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvtodues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delay Charge">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdelcharge" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelcharge" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Cheque">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdischarge" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdischarge" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnettodues" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnettodues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Encash">
                                            <FooterTemplate>
                                                <asp:Label ID="lgFEncash" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEncash" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Returned">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Today's">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Post Dated">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total Received ">

                                            <FooterTemplate>
                                                <asp:Label ID="gvFtoreceived02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="gvtoreceived02" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
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

                            <asp:Panel ID="pnlIndPro" runat="server" Visible="False">
                                <div class="row">


                                    <div class="col-md-12">
                                        <asp:Label runat="server" CssClass="GrpHeader" Font-Bold="True" Font-Size="12px"
                                            Width="300px">Note</asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>

                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvinpro" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="337px" CssClass=" table-striped  table-bordered grvContentarea">
                                                <PagerSettings Position="Top" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid0" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Decription">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtounit" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                            <itemstyle horizontalalign="Right" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Unit Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtounsize" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtourate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtouamt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Precentate">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtoupercent" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
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
                            </asp:Panel>

                        </asp:View>
                        <asp:View ID="ViewallProDues" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="dgvAccRec03_PageIndexChanging" ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    Width="654px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Target="_blank" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                    Width="250px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flat Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Avg. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrateal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apartment Price">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFaptcostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvaptcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car Parking">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcpcostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcpcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Utility ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFutilitycostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvutilitycostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Others">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFothcostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvothescostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtocostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtocsotal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Encash">
                                            <FooterTemplate>
                                                <asp:Label ID="lgFEncashal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEncashal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Returned">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtretamtal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtretamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Today's">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtframtal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtframtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Post Dated">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtpdamtal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtpdamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoreceivedal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotreceivedal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFatoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtatoduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues Upto Dec">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues Balance">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Booking">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpbookingal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpbduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Installment">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpinsduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpretoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoduesal0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Booking ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCbookingal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCbookingal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Installment ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCinstallmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total ">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoCInstalmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCoCInstalmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Due">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFvtoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delay Charge">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdelchargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Cheque">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdischargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdischargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnettoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnettoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
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
                            <asp:Panel ID="pnlIndProal" runat="server" Visible="False">

                                <div class="row">


                                    <div class="col-md-12">
                                        <asp:Label runat="server" CssClass="GrpHeader" Font-Bold="True" Font-Size="12px"
                                            Width="300px">Note</asp:Label>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvinproal" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="337px" CssClass=" table-striped  table-bordered grvContentarea">

                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid1" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="10px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Decription">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesinproal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtounital" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                            <itemstyle horizontalalign="Right" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Unit Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtounsizeal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtourateal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtouamtal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Precentate">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtoupercental" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
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


                            </asp:Panel>

                        </asp:View>
                        <asp:View ID="ViewYearLyCollection" runat="server">
                            <div class="  table-responsive">
                                <asp:GridView ID="gvyCollection" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvyCollection_PageIndexChanging" ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    Width="654px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">

                                            <FooterTemplate>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>


                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Project Name" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>

                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdescyc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Project Name">
                                            <FooterTemplate>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdescyc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoBgdCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtobgdcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtsoldcalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtosoldvalue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoreceivedyc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotreceivedyc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpduesyc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpredueayc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues1">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues2">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues3">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues4">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam4" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues5">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam5" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam5" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues6">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam6" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam6" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues7">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam7" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues8">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam8" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues9">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam9" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues10">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam10" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues11">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam11" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam11" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues12">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam12" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam12" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtdueam" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgtdueam" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtodueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
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

                        <asp:View ID="ViewProStatus" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvProClientst" runat="server" AllowPaging="True" CssClass=" table-striped  table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    Width="654px" OnPageIndexChanging="gvProClientst_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgpactdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unsold Unit No">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotalcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right">Total</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgununitname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uunitname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unsold Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFununitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvununitsize" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unusize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Sold Unit No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgsoldunitname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sunitname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sold Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsoldunitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsoldunitsize" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Unit Size">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtounitsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtounitsize" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgacunamecst" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtocostcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtocsotcst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Encash">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFEncashcst" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEncashcst" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Due Balance">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFatoduescst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvatoduescst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delay Charge">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdelchargecst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelchargecst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Net Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnettoduescst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnettoduescst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Registration Exp">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFregiscst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvregiscst" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Registration Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvregistration" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "register")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalteam" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salteam")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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

                        <asp:View ID="ViewYCollDetails" runat="server">
                            <div class="  table-responsive">
                                <asp:GridView ID="gv_YCollectionDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gv_YCollectionDetails_PageIndexChanging"
                                    ShowFooter="True" CssClass=" table-striped  table-bordered grvContentarea" PageSize="15"
                                    Width="654px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="Project Name">

                                            <FooterTemplate>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderTemplate>

                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Project Name" ></asp:Label>
                                                        </td>
                                                        <td class="">&nbsp;</td>
                                                        <td>

                                                            

                                                            <asp:HyperLink ID="hlbtntbdeCdataExel" runat="server" 
                                                                BorderColor="White"  Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px"><button class="btn btn-sm btn-success"><i class='far fa-file-excel'></i></button>
                                                                </asp:HyperLink>


                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdescyc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' Width="100px"> </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' Width="90px"> </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installment Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "installdate")) %>' Width="100px"> </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Person Nmae">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "saleperson")) %>' Width="140px"> </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgacustname" runat="server" CssClass='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "salestatus")) =="Current" ? "text-primary":"text-danger " %>'
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFtoBgdCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdetobgdcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFtsoldcalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdetosoldvalue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFtoreceivedyc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdetotreceivedyc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFpduesyc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdepredueayc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues1">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam1")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues2">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam2")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues3">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam3")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues4">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam4" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam4")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues5">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam5" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam5" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam5")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues6">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam6" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam6" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam6")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues7">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam7" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam7")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues8">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam8" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam8")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues9">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam9" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam9")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues10">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam10" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam10")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues11">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam11" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam11" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam11")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues12">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFdueam12" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedueam12" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam12")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFtdueam" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdetdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todueam")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvdeFgtdueam" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdegtdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtodueam")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooterNew" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <RowStyle CssClass="grvRowsNew" />

                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>

                </div>

            </div>
            <!-- End of card-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

