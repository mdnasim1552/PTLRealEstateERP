<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktEntryUnit.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktEntryUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            var gv = $('#<%=this.gvUnit.ClientID %>');
            gv.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            $('.chzn-select').chosen({ search_contains: true });
            var gridview = $('#<%=this.gvUnit.ClientID %>');
            $.keynavigation(gridview);
        }
    </script>

    <style>
        #ContentPlaceHolder1_ddlProjectName_chzn {
            width:289 px!important;
        }
    </style>







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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblPrjName" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:LinkButton ID="ibtnFindProject" CssClass="srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2">Project Name</asp:LinkButton>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm" style="width:290px;" TabIndex="3">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectdesc" runat="server" Visible="false" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:LinkButton ID="btnType" CssClass="srearchBtn" OnClick="btnType_Click" runat="server" TabIndex="2">Type Name</asp:LinkButton>
                                <asp:TextBox ID="txtType" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlUnitType" runat="server" CssClass="form-control form-control-sm chzn-select" style="width:185px;" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblMaterials" runat="server" CssClass="lblTxt lblName" Text=""></asp:Label>
                                <asp:TextBox ID="txtSrcMat" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="ibtnGroup" CssClass="srearchBtn" runat="server" OnClick="ibtnGroup_Click" TabIndex="2">Group Name</asp:LinkButton>
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control form-control-sm  chzn-select" style="width:185px;" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" TabIndex="4">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Category Name"></asp:Label>
                                <asp:TextBox ID="txtsrchfloor" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFloor" CssClass="srearchBtn" runat="server" OnClick="ibtnFloor_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control form-control-sm chzn-select" style="width:180px;" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="70px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top:20px;">
                            <div class="form-group">
                                <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnCheckedChanged="chkAllSInf_CheckedChanged" Text="Show All" CssClass="btn btn-primary btn-sm checkBox" TabIndex="7" />

                                <asp:Label ID="lbldateto" runat="server" CssClass="control-label" Text="Total No" Style="display: none;"></asp:Label>
                                <asp:TextBox ID="txttotalno" runat="server" CssClass="control-label" Width="100px" Style="display: none;"></asp:TextBox>
                                <asp:LinkButton ID="ibtnTotal" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnTotal_Click" TabIndex="2" Style="display: none;"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top:20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelGroup" runat="server" Visible="False">
            </asp:Panel>
            <div class="card card-fluid">
                <div class="card-body" style="min-height:400px;">
                    <div class=" row table-responsive">
                        <asp:GridView ID="gvUnit" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvUnit_PageIndexChanging"
                            OnRowDataBound="gvUnit_RowDataBound" OnRowDeleting="gvUnit_RowDeleting"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="26px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Code">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary checkBox">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCod" runat="server" Font-Size="11px" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Id">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfcode" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcode")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdate" runat="server" OnClick="lFinalUpdate_Click" CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemdesc" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>







                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                            Width="40px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lFUsize" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvuamt" runat="server" Font-Size="11px" BackColor="Transparent"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Parking">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvPamt" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvPAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Utility">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvUtility" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvPUtility" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Co-operative">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvPCooprative" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvPCooprative" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTamt" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apt.Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvUqty" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvfAptqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parking Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvPqty" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvfParkingqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parking Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbstat" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bstat")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Booking Money">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBookingMoney" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="D.Payment %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBookingper" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookingper")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="EMI" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvemi" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noofinstall")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Handover Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvhoverdate" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "handovdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "handovdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvhoverdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvhoverdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Handover %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvhoverper" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "handovper")).ToString("#,##0;(#,##0); ")%>'
                                            Width="60px"></asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Facing">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvUFacing" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvUView" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mgt. Booked">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook"))=="True" %>'
                                            Width="50px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Item BN">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemdescbn" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udescbn")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


