<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CustMaintenanceWork.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustMaintenanceWork" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
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
                    <asp:Panel ID="pnlDelChrgDet" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblType" runat="server" class="control-label  lblmargin-top9px" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control chzn-select form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPreList" runat="server" class="control-label  lblmargin-top9px" Text="Project Name"></asp:Label>
                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblUnitName" runat="server" class="control-label  lblmargin-top9px" Text="Unit Name"></asp:Label>
                                    <asp:DropDownList ID="ddlUnitName" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="ibtnPreAdNo" runat="server" Text="Previous" Font-Underline="false" OnClick="ibtnPreAdNo_Click"></asp:LinkButton>
                                    <asp:DropDownList ID="ddlPrevADNumber" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblAddWrkDate" runat="server" class="control-label  lblmargin-top9px" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtCurTransDate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblAddWorkNo" runat="server" class="control-label  lblmargin-top9px" Text="Add. No."></asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" class="control-label  lblmargin-top9px"></asp:Label>
                                    <asp:TextBox ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>

                            <div class="col-2">
                                <asp:Label ID="lblSchCode" runat="server" Visible="false"></asp:Label>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-header">
                    <asp:Panel ID="PanelItem" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblItem" runat="server" class="control-label  lblmargin-top9px" Text="Resource List"></asp:Label>
                                    <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnAddWork" runat="server" OnClick="lbtnAddWork_Click" CssClass="btn btn-primary btn-sm  lblmargin-top20px" Style="margin-top: 20px;">Add</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group">
                                    <asp:Label ID="lblInsmnt" runat="server" class="control-label  lblmargin-top9px" Text="Installment" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control form-control-sm chzn-select" Visible="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvAddWork" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdWrkdelete" runat="server" OnClick="lbtnAdWrkdelete_Click" CssClass="btn btn-xs"> <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGcodAdd" runat="server" ForeColor="Black" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalAddWork" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnTotalAddWork_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlocateion" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description of <br/> Item">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdateAdWork" runat="server" CssClass="btn btn-success btn-sm" OnClick="lFinalUpdateAdWork_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgdescw" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Client Choice">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdesclchoice" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")) %>'
                                            Width="150px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvunit" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Company Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Company Standard  M.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcommRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Company Standard L.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomlRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comlrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Company Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcomAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Choice  M.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvclmRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Client Choice  L.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcllRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cllrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Client Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldiffgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Refund">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnrefund" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nrefund")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnrefund" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Demand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblndemand" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ndemand")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFndemand" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Installment" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInstallment" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delschdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdiscount" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdisAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnetamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgbID" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>

                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </div>

                    <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                        <div class="col-sm-6 col-md-6 col-lg-6 mt-3" id="dNarr" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNarr" runat="server" CssClass="control-label  lblmargin-top9px" Font-Bold="true" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


