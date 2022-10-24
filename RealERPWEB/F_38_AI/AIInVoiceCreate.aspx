﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIInVoiceCreate.aspx.cs" Inherits="RealERPWEB.F_38_AI.AIInVoiceCreate" %>

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
            <div class="section">
                <div class="card mt-4">
                    <div class="card-header p-1">
                        <h4>New Invoice</h4>
                    </div>
                    <div class="card-body">
                        <div class="well">
                            <div class="row mb-1">
                                <asp:Label ID="Label4" CssClass="col-lg-1 col-form-label" runat="server">InVoice Date</asp:Label>
                                <div class="col-lg-1 col-md-2 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtdate" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row mb-1">

                                <asp:Label ID="Label7" CssClass="col-lg-1 col-form-label" runat="server">Invoice No
                                    <span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="vldtxtInvoice" runat="server" ForeColor="Red" ControlToValidate="txtInvoiceno" ValidationGroup="NewInvoice"
                                            ErrorMessage="Please Enter Invoice No" /></span>

                                </asp:Label>
                                <div class="col-lg-1 col-md-2 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtInvoiceno" placeholder="AI-INV" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 col-md-2 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtInvoiceno2" placeholder="000001" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">

                                <asp:Label ID="Label12" CssClass="col-lg-1 col-form-label" runat="server">Due Date</asp:Label>
                                <div class="col-lg-3">
                                    <asp:TextBox runat="server" ID="txtduedate" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtduedate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <asp:Label ID="Label3" CssClass="col-lg-1 col-form-label" runat="server">Clients</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddlsuplier" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlsuplier_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label1" CssClass="col-lg-1 col-form-label" runat="server">Project Name</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddlprojname" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlprojname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label2" CssClass="col-lg-1 col-form-label" runat="server">&nbsp; Batch Name</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddlbatchname" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlbatchname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <asp:Label ID="Label5" CssClass="col-lg-1 col-form-label" runat="server">Data Set</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddldataset" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label9" CssClass="col-lg-1 col-form-label" runat="server">&nbsp; Quantity</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtdoneqty" min="0" TextMode="Number" placeholder="00" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>
                                <asp:Label ID="Label8" CssClass="col-lg-1 col-form-label" runat="server">&nbsp;Rate
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtrate" ValidationGroup="NewInvoice"
                                            ErrorMessage="Please Enter Rate No" /></span>
                                </asp:Label>
                                <div class="col-lg-2 col-md-3 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtrate" min="0" TextMode="Number" placeholder="0.00" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddlcurency" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <asp:Label ID="Label6" CssClass="col-lg-1 col-form-label" runat="server">Subject</asp:Label>
                                <div class="col-lg-6 col-md-3 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtsubjects" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="mt-1">
                                    <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20"><i class="fa fa-plus-circle"></i>&nbsp;Add</asp:LinkButton></li>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="gv_AIInvoice" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" >
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
                                    <asp:TemplateField HeaderText="Member" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InvoiceDate" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoicedate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invoicedate")).ToString("dd-MMM-yyyy") %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustomer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customer")) %>' Width="200px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinvoiceno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invoiceno")) + Convert.ToString(DataBinder.Eval(Container.DataItem, "invoiceno1")) %>' Width="90px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblduedate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "duedate")).ToString("dd-MMM-yyyy") %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprjname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjname")) %>' Width="250px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbatchname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>' Width="140px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Set">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldataset" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dataset")) %>' Width="80px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblquantity" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrate" runat="server" Text='<%#  Convert.ToString( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity"))).ToString() +"-"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "  currency"))  %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subjects" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsubjects" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subjects")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
