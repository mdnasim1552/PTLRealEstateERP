<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPurchaseOrderSummary.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseOrderSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
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
            <div class="card mt-4">
                <div class="card-body pt-2 pb-2">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server">From</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label2" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label4" runat="server">Projects</asp:Label>
                            <asp:DropDownList ID="ddlprjname" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 ">
                            <asp:Label ID="Label3" runat="server">Supplier</asp:Label>
                            <asp:DropDownList ID="ddlsupplierlist" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6 mt-4 ">

                            <asp:RadioButtonList ID="radiolist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">&nbsp; Summary &nbsp; &nbsp;</asp:ListItem>
                                <asp:ListItem Value="2">&nbsp; Details</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>
                        <div class="col-md-2 mt-3">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-2">
                            <asp:Label ID="Label31" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlBatchPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatchPage_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                      </div>

                        <asp:Panel ID="Ordersummary" runat="server" Visible="false">
                            <div class="row mt-4 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderSummary" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvOrderSummary_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="14px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProjectName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpactdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsirdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrsirdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                   <FooterTemplate>
                                                    <asp:Label ID="lblsumtotal" runat="server"  Font-Bold="true" Font-Size="14px">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvorderqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvordrate" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrate")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvorderamt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="tblsummsoramt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmrrqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmrrrate" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmrramt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="tblsummamt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillrate" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billrate")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillamt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="tblsummbillamt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#007c69" ForeColor="#ffffff" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="orderdetails" runat="server" Visible="false">
                            <div class="row mt-4 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvOrderDetails_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProjectName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpactdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsirdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblorderno" runat="server" Width="110px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrsirdesc" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server"  Font-Bold="true" Font-Size="14px">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblspcfdesc" runat="server" Width="80px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRRNO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrrno" runat="server" Width="160px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbillno" runat="server" Width="160px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblordrqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblorramt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orramt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="tbldetailsoramt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrrqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrramt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="tbldetailsrecamt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbillqty" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbillamt" runat="server" Width="100px"
                                                        Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="tbldetailsbillamt" runat="server" ForeColor="Black" Font-Size="14px" ></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#007c69" ForeColor="#ffffff" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:Panel>

            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
