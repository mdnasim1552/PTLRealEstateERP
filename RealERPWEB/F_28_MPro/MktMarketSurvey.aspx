<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMarketSurvey.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktMarketSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lblHead {
            color: deeppink;
            font-size: 14px !important;
            font-weight: bold;
        }

        .table {
            margin-bottom: 0;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

             $('.chzn-select').chosen({ search_contains: true });

        }

        function openServyModal() {
            $('#ServyModal').modal('toggle');
        };

        function closeServyModal() {
            $('#ServyModal').modal('hide');
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
            <div class="card card-fluid mb-2">
                <div class="card-body">
                    <asp:Panel ID="Panel1" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblCSNo" runat="server" class="control-label" Text="CS No.:"></asp:Label>
                                    <asp:Label ID="lblCurMSRNo1" runat="server" class="control-label" Text="MSR00-"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblSurDate" runat="server" class="control-label" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender_txtCurMSRDate" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>

                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblReqList" runat="server" class="control-label" Text="Req List:"></asp:Label>
                                    <asp:TextBox ID="txtReqSearch" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="lnkReqList" runat="server" Visible="false" OnClick="lnkReqList_Click"></asp:LinkButton>
                                    <asp:DropDownList ID="ddlReqList" runat="server" ValidationGroup="g1" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1 ml-2">
                                <asp:LinkButton ID="lbtnMSROk" runat="server" Text="Ok" OnClick="lbtnMSROk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:LinkButton ID="lnkbtnNewReq" runat="server" OnClick="lnkbtnNewReq_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;" ToolTip="Add New Work Details"><i class="fas fa-plus"></i>&nbsp;Add Work</asp:LinkButton>
                            </div>

                        </div>

                        <div class="form-group">
                            <div class="col-md-4  pading5px pull-right  asitCol10" style="display: none">
                                <asp:Label ID="lblPreMrList" runat="server" CssClass=" lblName lblTxt" Text="Previous MSR:"></asp:Label>
                                <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="btn btn-primary primaryBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevMSRList" runat="server" Width="160px" CssClass="ddlPage"></asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlAddWorkDet" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPRType" runat="server" class="control-label" Text="Pur. Req. Type"></asp:Label>
                                    <asp:DropDownList ID="ddlPRType" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPRType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblActType" runat="server" class="control-label" Text="Activity Type"></asp:Label>
                                    <asp:DropDownList ID="ddlActType" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblMatType" runat="server" class="control-label" Text="Marketing Type"></asp:Label>
                                    <asp:DropDownList ID="ddlMarkType" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;">Select</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="card card-fluid mb-2">
                        <div class="card-header">
                            <asp:Label ID="lbltitel2" runat="server" CssClass="lblHead" Visible="false"><h4> B. Best Selection</h4> </asp:Label>
                        </div>
                        <div class="card-body" style="min-height: 200px;">
                            <div class="table-responsive">
                                <asp:GridView ID="gvBestSelect" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="table-striped table-bordered grvContentarea"
                                    ShowFooter="True" Width="1009px">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplBSel" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1bs" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvacttypeBSel1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                    Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials">
                                            <InsertItemTemplate>
                                                <asp:Label ID="lblgrmet1BSel" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </InsertItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnBSFTotal" runat="server" Font-Bold="True" OnClick="lbtnBSFTotal_Click"
                                                    CssClass="btn btn-warning btn-sm form-control">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) + "</B>"%>'
                                                    Width="500px"></asp:Label>
                                                <asp:TextBox ID="txtgvRSirDetDesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="12px" Style="background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdetdesc")) %>'
                                                    Width="500px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" Font-Size="14px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrUnitBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock </br>Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="lblFstkqty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="70px" ForeColor="#000"></asp:Label>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstkqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFQtybs" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="70px" ForeColor="#000"></asp:Label>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpropqtyBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved</br> Qty" Visible="false" HeaderStyle-BackColor="Yellow" ItemStyle-BackColor="Yellow">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFareqty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="50px" ForeColor="#000"></asp:Label>


                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvareqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS </br> Qty" HeaderStyle-BackColor="Violet" ItemStyle-BackColor="Violet">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFcsreqqty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>


                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcsreqqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csreqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRateBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFAmountbs" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamounteBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="lblFbdtamt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbdtamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Rate">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlBSpurate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lstpurate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Qty">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlBSpurqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrlpurdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lpurdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrssup1BSel" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment </br>Type" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpaytype" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: left; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype"))%>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance </br>Amount" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvadvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit</br> Period</br> (Day)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgPeriodbs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment")) %>'
                                                    Style="text-align: left; background-color: Transparent" Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Concern </br>Person" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgConcernbs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mobile" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMobilebs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lead</br> Time" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleadtime" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                    <div class="card card-fluid mb-2">
                        <div class="card-header">
                            <asp:Label ID="lbltitel1" runat="server" CssClass="lblHead" Visible="false"><h4> A. Comparative Statement</h4> </asp:Label>
                        </div>
                        <div class="card-body" style="min-height: 200px;">
                            <div class="table-responsive">
                                <asp:GridView ID="gvResInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-bordered grvContentarea"
                                    ShowFooter="True" Width="1009px" OnRowDataBound="gvResInfo_RowDataBound">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblrsircode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                    Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials">
                                            <ItemTemplate>
                                                <asp:LinkButton OnClick="lblgrsirdescs1_Click" ID="lblgrsirdescs1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' ToolTip="Add Supplier Information"
                                                    Width="130px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>

                                            <ItemStyle />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Used For">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgreqnote" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirunit" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFQty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>


                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpropqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvpropqty_01" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Visible="false"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty1")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CS </br> Qty" HeaderStyle-BackColor="Violet" ItemStyle-BackColor="Violet">
                                            <FooterTemplate>

                                                <asp:Label ID="lblFcsreqqty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>


                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcsreqqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csreqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>


                                                <asp:Label ID="lblAmount" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="lblFbdtamt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    Font-Bold="True" Font-Size="10px"
                                                    Width="80px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbdtamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrsirdesc1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">

                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkboxgv" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" %>' />



                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Rate">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlstpurate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lstpurate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Qty">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlSpurqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last </br>Purchase</br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrlpurdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lpurdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment </br>Type" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpaytypeC" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: left; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype"))%>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance </br>Amount" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvadvamtC" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Period (Day)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgPeriod" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment")) %>' Style="text-align: left; background-color: Transparent"
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TextRemarks" Style="border: none;" TextMode="MultiLine" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrrmrk")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Concern Person" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgConcern" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mobile" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMobile" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtTotal1" runat="server" Visible="false" CssClass="btn btn-primary primaryBtn" OnClick="lbtTotal1_Click">Total</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal--->
            <div id="ServyModal" class="modal  animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <i class="fas fa-address-card"></i>
                                <asp:Label ID="Label5" runat="server"> Purchase Supplier Information Add </asp:Label>
                            </h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <asp:Label ID="lblResList2" runat="server" CssClass="lblTxt lblName" Font-Bold="true" Text="Material"></asp:Label>
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlSupl2" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 mt-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Rate</span>
                                        </div>
                                        <asp:TextBox ID="TextRate" runat="server" CssClass="inputTxt inpPixedWidth form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-success" OnClick="UpdateData_Click"><i class="fas fa-save"></i>&nbsp;&nbsp;Save</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
