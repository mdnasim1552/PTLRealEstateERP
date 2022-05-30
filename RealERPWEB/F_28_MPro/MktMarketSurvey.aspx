<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMarketSurvey.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktMarketSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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

        .middle {
            transition: .5s ease;
            opacity: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
        }

        .checkboxcls {
            opacity: 1;
            position: absolute;
            top: 80%;
            left: 10%;
        }

        .uploadedimg .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
        }

        .uploadedimg:hover .image {
            opacity: 0.3;
        }

        .uploadedimg:hover .middle {
            opacity: 1;
        }

        .text {
            background-color: #4CAF50;
            color: white;
            font-size: 16px;
            padding: 16px 32px;
        }

        .panel.with-nav-tabs .panel-heading {
            padding: 5px 5px 0 5px;
        }

        .panel.with-nav-tabs .nav-tabs {
            border-bottom: none;
        }

        .panel.with-nav-tabs .nav-justified {
            margin-bottom: -1px;
        }
        /*** PANEL PRIMARY ***/
        a:hover {
            background-color: #D8E7D1;
            text-decoration: none;
        }

        .nav-link {
            display: block;
            padding: 0.375rem 1rem;
        }

        .with-nav-tabs.panel-primary .nav-tabs > li > a,
        .with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
        .with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
            color: blue;
        }

            .with-nav-tabs.panel-primary .nav-tabs > .open > a,
            .with-nav-tabs.panel-primary .nav-tabs > .open > a:hover,
            .with-nav-tabs.panel-primary .nav-tabs > .open > a:focus,
            .with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
            .with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
                color: blue;
                border-color: transparent;
            }

        .with-nav-tabs.panel-primary .nav-tabs > li.active > a,
        .with-nav-tabs.panel-primary .nav-tabs > li.active > a:hover,
        .with-nav-tabs.panel-primary .nav-tabs > li.active > a:focus {
            color: #428bca;
            border-color: #428bca;
            background-color: #D8E7D1 !important;
        }

        .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu {
            background-color: #428bca;
            border-color: #3071a9;
        }

            .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a {
                color: #fff;
            }

                .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
                .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
                    background-color: #3071a9;
                }

            .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a,
            .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
            .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
                background-color: #4a9fe9;
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
        };

        function PrintRDLC() {
            window.open('../RDLCViewerWin.aspx?PrintOpt=PDF', '_blank');
        }
        function PrintCristal() {
            window.open('../RptViewer.aspx?PrintOpt=PDF', '_blank');
        }
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Materials', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }

        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseModal() {
            $('#myModal').modal('hide');
        }
        function openServyModal() {
            $('#ServyModal').modal('toggle');
        }
        function closeServyModal() {
            $('#ServyModal').modal('hide');
        }
        function Confirm() {
            window.onload = function () {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to replace existing file?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
                document.getElementById("<%=btnConfirm.ClientID %>").click();
            }
        }
        function addplug(plug) {
            // alert(plug);

            $('#<%=this.txtflag.ClientID %>').val(plug);


        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" EnableViewState="true" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
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
                                    <asp:Label ID="lblCSNo" runat="server" class="control-label  lblmargin-top9px" Text="CS No.:"></asp:Label>
                                    <asp:Label ID="lblCurMSRNo1" runat="server" class="control-label  lblmargin-top9px" Text="MSR00-"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblSurDate" runat="server" class="control-label  lblmargin-top9px" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblReqList" runat="server" class="control-label" Text="Req List:"></asp:Label>
                                    <asp:TextBox ID="txtReqSearch" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="lnkReqList" runat="server" Visible="false" OnClick="lnkReqList_Click"></asp:LinkButton>
                                    <asp:DropDownList ID="ddlReqList" runat="server" ValidationGroup="g1" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="-1" ID="RequiredFieldValidator1" Display="Dynamic"
                                        ValidationGroup="g1" runat="server" ControlToValidate="ddlReqList"
                                        Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>
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
                                <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPreMR_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
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
                                            <ItemTemplate>
                                                <asp:LinkButton OnClick="lblgrmet1BSel_Click" ID="lblgrmet1BSel" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="120px"></asp:LinkButton>
                                            </ItemTemplate>
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
                                                <asp:LinkButton OnClick="lblgrsirdescs1_Click" ID="lblgrsirdescs1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
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
                    <div class="card card-fluid">
                        <div class="card-header">
                            <asp:Panel ID="fotpanel" Visible="false" runat="server">
                                <div class="col-12">
                                    <div class="panel with-nav-tabs panel-primary">
                                        <div class="panel-heading">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tab2primary" data-toggle="tab"><i class="fas fa-comment-alt"></i>&nbsp;&nbsp;Justification&nbsp;&nbsp;</a></li>
                                                <li><a href="#tab3primary" data-toggle="tab"><i class="fas fa-file-upload"></i>&nbsp;&nbsp;Upload&nbsp;&nbsp;</a></li>
                                                <li><a href="#tab1primary" data-toggle="tab"><i class="fas fa-hand-holding-usd"></i>&nbsp;&nbsp;Charging</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="card-body" style="min-height: 100px;">
                                        <div class="panel-body">
                                            <div class="tab-content">
                                                <div class="tab-pane fade in " id="tab1primary">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvcharging" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                            <PagerSettings Visible="False" />
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSuplCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                            Width="80px"></asp:Label>

                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sl">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo1" runat="server" Height="16px" Style="text-align: right"
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Supplier Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSupName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                                            Width="160px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Font-Size="X-Small" Width="160px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSupamt" Style="text-align: right;" runat="server" Height="16px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "supamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tax(%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtgvTax" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tax")).ToString("#,##0.00;") %>' Height="16px" Width="50px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vat (%)">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtgvVat" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vat")).ToString("#,##0.00;") %>' Height="16px" Width="50px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="h1" Visible="false">
                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="gvText0" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c0")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle BorderStyle="None" Width="60px" HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h2" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText1" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h3" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText2" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h4" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText3" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h5" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText4" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c4")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h6" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText5" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c5")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h7" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText6" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c6")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h8" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText7" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c7")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="h9" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText8" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c8")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h10" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText9" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c9")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h11" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText10" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c10")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="h12" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText11" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c11")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="h13" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="gvText12" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c12")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblToal" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;-#,##0.00;") %>'></asp:Label>
                                                                    </ItemTemplate>
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
                                                </div>
                                                <div class="tab-pane fade active " id="tab2primary">
                                                    <div class="row">
                                                        <div class="col-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblJus" CssClass="lblHead" runat="server" Visible="false"><h4> A. Purchase Justification</h4> </asp:Label>
                                                                <asp:TextBox ID="txtMSRNarr2" placeholder="Write here............" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label3" CssClass="lblHead" runat="server"><h4> B. Audit Justification</h4> </asp:Label>
                                                                <asp:TextBox ID="txtMSRNarr" placeholder="Write here............" runat="server" class="form-control" Visible="false" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-4">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label4" CssClass="lblHead" runat="server"><h4> C. MD Justification</h4> </asp:Label>
                                                                <asp:TextBox ID="txtMSRNarr3" placeholder="Write here............" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="tab3primary">
                                                    <div class="form-group">
                                                        <div class="col-4">
                                                            <asp:Panel runat="server" ID="pnlQutatt" Visible="false">
                                                                <div class="panel panel-primary">
                                                                    <div class="panel-heading">
                                                                        <i class="fas fa-camera"></i>&nbsp;&nbsp;Qutation Image Upload
   
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <div class="row">
                                                                                    <div class="form-group">

                                                                                        <asp:Label ID="Label2" runat="server" CssClass="col-md-4" Text="Supplier Name"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlBestSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row">
                                                                                    <fieldset class="alert alert-success">

                                                                                        <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                                            OnClientUploadComplete="uploadComplete" runat="server"
                                                                                            ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                                            CompleteBackColor="White" Visible="false"
                                                                                            UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                                            OnUploadedComplete="FileUploadComplete" />
                                                                                        <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                                                        <br />
                                                                                        <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                                                    </fieldset>


                                                                                </div>


                                                                                <div style="display: none;">

                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                    <asp:Button Text="Upload" runat="server" OnClick="UploadFile" />
                                                                                    <asp:Button ID="btnConfirm" runat="server" OnClick="ConfirmReplace" Style="display: none" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="col-8">
                                                            <div class="panel panel-primary">
                                                                <div class="panel-heading">
                                                                    <i class="fas fa-images"></i>&nbsp;&nbsp;Uploaded Files       
                                                                    <div class="float-right">
                                                                        <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_Click" />
                                                                        <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>

                                                                    </div>
                                                                </div>
                                                                <div class="panel-body ">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                                                <LayoutTemplate>
                                                                                    <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <div class="col-xs-4 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                                        <div id="EmpAll" runat="server">
                                                                                            <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("filePath1") %>'></asp:Label>
                                                                                            <asp:Label ID="msrno" Visible="false" runat="server" Text='<%# Eval("msrno") %>'></asp:Label>
                                                                                            <asp:Label ID="ssircode" Visible="false" runat="server" Text='<%# Eval("ssircode") %>'></asp:Label>

                                                                                            <a href="<%# Eval("filePath1") %>" class="uploadedimg" target="_blank">
                                                                                                <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                                                <div class="middle">
                                                                                                    <span><%# Eval("supinfo") %></span>
                                                                                                </div>
                                                                                                <div class="checkboxcls">
                                                                                                    <asp:CheckBox ID="ChDel" runat="server" />
                                                                                                </div>
                                                                                            </a>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- for the breakdown--->
                <div id="myModal" class="modal  animated slideInLeft" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>
                                    <asp:Label ID="lbmodalheading" runat="server">  Material Wise Issue and Purchase stock </asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>

                                <div class="row-fluid form-horizontal forgotform" id="">
                                    <div class="form-group">

                                        <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Store Name:</label>
                                        <asp:Label ID="lblstore" CssClass="col-md-9 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                    </div>
                                    <div class="form-group">

                                        <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Material:</label>
                                        <asp:Label ID="lblmat" CssClass="col-md-3 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>
                                        <label class="col-md-2" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Specification:</label>
                                        <asp:Label ID="lblspc" CssClass="col-md-4 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                    </div>


                                </div>

                                <div class="panel with-nav-tabs panel-default">
                                    <div class="panel-heading">
                                        <ul class="nav nav-tabs">
                                            <li class="active"><a href="#tabpur" onclick="addplug('purisu');" data-toggle="tab"><span class="glyphicon glyphicon-shopping-cart"></span>Purchase And Issue</a></li>
                                            <li><a href="#tabhis" onclick="addplug('purhis');" data-toggle="tab"><span class="glyphicon glyphicon-briefcase"></span>Purchase History </a></li>

                                        </ul>
                                    </div>
                                    <div class="panel-body">
                                        <div class="tab-content">
                                            <div class="tab-pane fade in active" id="tabpur">
                                                <asp:GridView ID="gvMatHis" runat="server" HorizontalAlign="Center" OnRowDataBound="gvMatHis_RowDataBound" AutoGenerateColumns="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Supplier" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlblgvsup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="150px" Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlgvPrice" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                    Width="80px" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlblgvexedate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px" Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlblgvunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                    Width="50px" Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgenno" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno")) %>'
                                                                    Width="100px" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="lblGp" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gp")) %>'
                                                                    Width="100px" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="mlblgvrecno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                                                    Width="80px" Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="In Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlgvitmqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="80px" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Out Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlgvOutqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="80px" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stock Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mlgvbalqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stock")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="80px" Style="text-align: right"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="LbtnPrint" ToolTip="Order Print" Visible="false" Target="_blank" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                    </Columns>




                                                </asp:GridView>
                                            </div>
                                            <div class="tab-pane fade" id="tabhis">
                                                <asp:GridView ID="gvMatPurHis" runat="server"
                                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="830px">
                                                    <PagerSettings Position="Top" />
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                        <asp:TemplateField HeaderText="MRR Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvOrderDate" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="MRR No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvorderno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvreqno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MRF No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvreqrefno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvprojectdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSupName" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvSupName" runat="server" Font-Bold="True" Font-Size="12px" Text="Total: "
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Brand Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvBrName" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="MRR Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="55px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvMRRQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="55px"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="55px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                        </div>
                                    </div>
                                </div>





                            </div>
                            <div class="modal-footer ">

                                <asp:LinkButton ID="LbtnPrint" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseModal()" OnClick="LbtnModalPrint_Click"><span class="glyphicon glyphicon-print"></span> Print</asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                            </div>
                        </div>
                    </div>
                </div>

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
