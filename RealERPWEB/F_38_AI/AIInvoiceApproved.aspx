<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIInvoiceApproved.aspx.cs" Inherits="RealERPWEB.F_38_AI.AIInvoiceApproved" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
            height: 28px !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
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

            <div class="card mt-3">
                <div class="card-header pt-2 pb-2">
                    <div class="row">
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server">Voucher Date</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label7" runat="server">Voucher No
                                 <asp:LinkButton ID="ibtnvounu" runat="server" OnClick="ibtnvounu_Click" TabIndex="12"><span class="fa fa-search"> </span></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtvounum1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 p-1 mt-3">
                            <asp:TextBox ID="txtvounum2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="Label2" runat="server">Invoice No</asp:Label>
                            <asp:TextBox ID="tblinvo" runat="server" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>

                        <div class="col-lg-1 col-md-1 p-1 mt-3">
                            <div class="form-group-">
                                <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="table-responsive mt-2">
                            <asp:GridView ID="gvInvoApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea"
                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL # ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/C Description">
                                        <ItemTemplate>

                                            <asp:Label ID="lblactdesc" runat="server" Width="400px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" Font-Size="16px"  />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldramount" runat="server" Width="200px"
                                                Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "dramount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                    <asp:Label ID="tbldrsum" runat="server"   Font-Bold="True" Font-Size="16px"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" Font-Size="16px"  />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcramount" runat="server" Width="200px"
                                                Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "cramount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                    <asp:Label ID="tblcrsum" runat="server"   Font-Bold="True" Font-Size="16px"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="true" Font-Size="16px"  />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#007c69" ForeColor="#ffffff" />
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="pnlupdate" runat="server" Visible="false">
                        <div class="row p-1">
                            <div class="col-lg-2 col-md-2 col-sm-6">
                                <asp:Label ID="Label4" runat="server">Ref./Cheq No/Slip No</asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-6">
                                <asp:Label ID="Label5" runat="server">Other ref.(if any)</asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="col-lg-1 col-md-1 mt-3 p-1">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass=" btn btn-primary btn-sm mt20">Update</asp:LinkButton></li>
                                </div>
                            </div>
                        </div>
                        <div class="row  p-2">
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <asp:Label ID="Label6" runat="server">Naration</asp:Label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
