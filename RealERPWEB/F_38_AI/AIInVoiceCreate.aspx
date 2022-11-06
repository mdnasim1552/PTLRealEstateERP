<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIInVoiceCreate.aspx.cs" Inherits="RealERPWEB.F_38_AI.AIInVoiceCreate" %>

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
                        <h4>Create Invoice</h4>
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
                                </asp:Label>
                                <div class="col-lg-1">
                                    <asp:TextBox runat="server" ID="txtInvoiceno" readonly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 ">
                                    <asp:TextBox runat="server" ID="txtInvoiceno2" readonly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row ">
                                <asp:Label ID="Label12" CssClass="col-lg-1 col-form-label" runat="server">Due Date</asp:Label>
                                <div class="col-lg-3">
                                    <asp:TextBox runat="server" ID="txtduedate" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtduedate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <asp:Label ID="Label3" CssClass="col-lg-1 col-form-label" runat="server">Clients</asp:Label>
                                <div class="col-lg-3 col-md-3 col-sm-6">
                                    <asp:DropDownList ID="ddlsuplier" runat="server" readonly="true" enabled="False" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlsuplier_SelectedIndexChanged">
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
                                <asp:Label ID="Label8" CssClass="col-lg-1 col-form-label" runat="server">&nbsp;&nbsp;Rate
                                   
                                </asp:Label>
                                <div class="col-lg-2 col-md-3 col-sm-6">
                                    <asp:TextBox runat="server" ID="txtrate" min="0" TextMode="Number" placeholder="0.00" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 col-md-3 col-sm-6">
                                    <%--<asp:TextBox ID="ddlcurency" readonly="true" runat="server"  CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                    <asp:TextBox ReadOnly="true" runat="server" ID="txtcurrency" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    
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
                            <div class="col-md-12">
                            <div class="card" id="virtualList" runat="server" visible="false" >
                                <div class="card-header bg-light p-1">
                                    <h4 class="text-center display-none"><span class="  text-muted">Invoice List</span></h4>
                                </div>
                                <div class="card-body">
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
                                    
                                    <asp:TemplateField HeaderText="InvoiceDate" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbatchid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
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
                                            <asp:TextBox ID="lblquantity" runat="server" BackColor="Transparent" OnTextChanged="lblquantity_TextChanged"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")) %>' Width="80px" ForeColor="Black" Font-Size="12px"> </asp:TextBox>                                           
                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblrate" runat="server" 
                                                BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) %>' Width="80px" ForeColor="Black" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalamount" runat="server" Text='<%#  Convert.ToString( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity"))).ToString() +"-"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "  currency"))  %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
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
                                     <asp:TemplateField HeaderText="Action" >
                                        <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btninvoicedelete" OnClick="btninvoicedelete_Click" OnClientClick="return confirm('Are You Sure?')" CssClass="text-danger"><i class="fa fa-trash"></i></asp:LinkButton>
                                            
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
                          <div class="row">
                            <div class="col-md-5 mt-1">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label10" class="form-label mt-2">Remarks</asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Notes" TextMode="MultiLine" Rows="12"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
