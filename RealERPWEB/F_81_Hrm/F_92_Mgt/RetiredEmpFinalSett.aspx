<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RetiredEmpFinalSett.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RetiredEmpFinalSett" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px;
        }

        div#ContentPlaceHolder1_ddlEmpName_chzn {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
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
                    <asp:Panel ID="panelHead" runat="server">

                        <div class="row">

                            <div class="col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblCurdate" runat="server">Date</asp:Label>
                                    <asp:TextBox ID="txtCurdate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurdate"></cc1:CalendarExtender>
                                </div>
                            </div>


                            <div class="col-lg-3 col-md-3 col-sm-4">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server">Employee Name</asp:Label>
                                    <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-6">
                                <div class="form-group mt20">
                                    <asp:Label ID="lblsetNo" runat="server">Emp Sett. No :</asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm float-right" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>

                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server">
                                        <asp:LinkButton ID="lbtnPrevsetNo" runat="server" OnClick="lbtnPrevsetNo_OnClick">Prev. Sett. No:</asp:LinkButton></asp:Label>

                                    <asp:DropDownList ID="ddlPrevsetNo" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>



                            <%--                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class="form-group">

                                <div class="col-md-3">
                                    <asp:Label ID="lblsetNo" runat="server" CssClass="lblTxt lblName">Emp Sett. No :</asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                </div>
                                <div class="col-md-3 pading5px">
                                    <asp:Label ID="lblCurdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                    <asp:TextBox ID="txtCurdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurdate"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lbtnPrevsetNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevsetNo_OnClick">Prev. Sett. No:</asp:LinkButton>
                                    <asp:DropDownList ID="ddlPrevsetNo" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class=" col-md-3  pading5px asitCol3">

                                    <asp:Label ID="lblEmpList" CssClass="lblTxt lblName " runat="server" Text="Employee Name:"></asp:Label>
                                    <asp:TextBox ID="txtSrcEmp" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                    <asp:LinkButton ID="imgbtnFindEmp" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindEmp_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                </div>


                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>


                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </fieldset>--%>
                        </div>

                    </asp:Panel>


                </div>
                <div class="card-body">
                    <asp:Panel ID="PanelEmpinfo" runat="server" CssClass="mb-2 mt-2" Visible="False">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-12">
                                <asp:Label ID="lblempname" runat="server" Text="Name Of Employee:"></asp:Label>
                                <asp:Label ID="lblname" runat="server"></asp:Label>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-12">
                                <asp:Label ID="lblempdesig" runat="server" Text="Designation:"></asp:Label>
                                <asp:Label ID="lbldesig" runat="server"></asp:Label>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-12">
                                <asp:Label ID="lblwrk" runat="server" Text="Work Station:"></asp:Label>
                                <asp:Label ID="lblwrkst" runat="server"></asp:Label>
                            </div>


                        </div>
                        <div class="row">

                            <div class="col-lg-4 col-md-4 col-sm-12">
                                <asp:Label ID="lbljoind" runat="server" Text="Joining Date:"></asp:Label>
                                <asp:Label ID="lbljdate" runat="server"></asp:Label>
                            </div>

                            <div class="col-lg-3 col-md-4 col-sm-12">

                                <asp:Label ID="lblrd" runat="server" Text="Date Of Release:"></asp:Label>
                                <asp:Label ID="lblrsd" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-12">

                                <asp:Label ID="lblsl" runat="server" Text="Service Length:"></asp:Label>
                                <asp:Label ID="lblslen" runat="server"></asp:Label>
                            </div>
                        </div>

                        <%-- <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblempname" CssClass="lblTxt " runat="server" Text="Name Of Employee:"></asp:Label>
                                            <asp:Label ID="lblname" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblempdesig" CssClass="lblTxt " runat="server" Text="Designation:"></asp:Label>
                                            <asp:Label ID="lbldesig" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblwrk" CssClass="lblTxt" runat="server" Text="Work Station:"></asp:Label>
                                            <asp:Label ID="lblwrkst" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lbljoind" CssClass="lblTxt" runat="server" Text="Joining Date:"></asp:Label>
                                            <asp:Label ID="lbljdate" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblrd" CssClass="lblTxt" runat="server" Text="Date Of Release:"></asp:Label>
                                            <asp:Label ID="lblrsd" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblsl" CssClass="lblTxt" runat="server" Text="Service Length:"></asp:Label>
                                            <asp:Label ID="lblslen" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>--%>
                    </asp:Panel>

                    <asp:GridView ID="gvRtrSalSett" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered mb-2"
                        AutoGenerateColumns="False"
                        ShowFooter="True">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-success btn-sm" OnClick="lnkTotal_OnClick">Total</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_OnClick" CssClass="btn  btn-primary btn-sm">Update</asp:LinkButton>
                                    <asp:Label ID="lgvfTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Text="Net Payable Amount"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvgdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary">
                                <ItemTemplate>
                                    <asp:TextBox ID="lgvnetamtcash" runat="server" Style="text-align: right" BorderStyle="None" BackColor="Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTNetmtcash" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="right" />
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






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

