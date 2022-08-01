<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BudgetForm.aspx.cs" Inherits="RealERPWEB.F_30_Facility.BudgetForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
            font-size: 12px;
        }
         p {
            width: 100%;
            text-align: center;
            border-bottom: 1px solid rgba(34, 34, 48, .1);
            line-height: 0.1em;
            margin: 10px 0 20px;
            color: black;
            font-weight:300;
            font-size:14px;
        }

            p span {
                background: #fff;
                padding: 0 10px;
            }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">

                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lbldate" class="form-label">Budget Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblDgNo" class="form-label" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                                <asp:DropDownList ID="ddlDgNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span> OK
                                </asp:LinkButton>
                            </div>
                        </div>
                       
                    </div>

                    <asp:Panel runat="server" ID="pnlComplain">
                        <div class="row mt-1">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                                    <asp:Label runat="server" ID="lblProjectText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label2" class="form-label">Site Visited</asp:Label>
                                    <asp:Label ID="lblSiteVisisted" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCustomer" class="form-label">Customer</asp:Label>
                                    <asp:Label runat="server" ID="lblCustomerText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblUnitlabel" class="form-label">Unit</asp:Label>
                                    <asp:Label runat="server" ID="lblUnitText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label3" class="form-label">Warranty</asp:Label>
                                    <asp:Label runat="server" ID="lblWarranty" class="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblWarrantyCode" class="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                </div>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="row w-100">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <p><span>Engineer Diagnosis</span></p>
                                        <div class="row  table-responsive m-1">
                                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid" runat="server"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complain Code" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvconcatcode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainId")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Problem Details">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvconcatdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainDesc")) %>'
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                                Width="300px" Font-Size="12px" ForeColor="Black"></asp:Label>

                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle ForeColor="Black" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle CssClass="gvPagination" />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <p><span>Complain Form</span></p>
                                        <div class="row  table-responsive m-1">
                                            <asp:GridView ID="gvComplainForm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid" runat="server"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complain Code" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvconcatcode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainId")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Problem Details">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvconcatdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainDesc")) %>'
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                                Width="300px" Font-Size="12px" ForeColor="Black"></asp:Label>

                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle ForeColor="Black" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>


                                                <FooterStyle CssClass="gvPagination" />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>


                            </div>


                        </div>
                        <p><span>Budget Form</span></p>
                        <div class="row mt-2">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label1" class="form-label">Material Category</asp:Label>
                                    <asp:DropDownList ID="ddlMatCategory" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMatCategory_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label4" class="form-label">Material</asp:Label>
                                    <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label runat="server" ID="lblUnit" class="form-label" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblsirval" class="form-label" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <br />
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-success" OnClick="btnAdd_Click">
                                            <span class="fa fa-plus-circle " style="color:white;" aria-hidden="true"  ></span>
                                            Add
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <div class="row ">

                            <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea ml-2"
                                ShowFooter="True" OnRowDataBound="gvMaterials_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="LnkbtnDelete"
                                                OnClick="LnkbtnDelete_Click" ToolTip="Delete Item">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconcatcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "materialId")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconcatdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "materialdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-sm btn-primary">Total</asp:LinkButton>

                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "unit").ToString() %>'
                                                Width="70px" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvPercnt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQuantity" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px"
                                                Width="140px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>


                                <FooterStyle CssClass="gvPagination" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>

                        <asp:Panel runat="server" ID="pnlApproval" Visible="false">
                            <div class="row">
                                <div class="col-md-5 mt-1">
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="Label6" class="form-label mt-2">Remarks</asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Notes" TextMode="MultiLine" Rows="5"></asp:TextBox>

                                    </div>

                                </div>
                            </div>
                        </asp:Panel>



                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkRefresh_Click" Width="100px">
                                <span class="fa fa-redo " style="color:black;" aria-hidden="true"></span> Refresh</asp:LinkButton>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkProceed" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2" OnClick="lnkProceed_Click" Width="150px">
                                <span class="fa fa-arrow-circle-right " style="color:white;" aria-hidden="true"></span>
                                Proceed to Next Step
                            </asp:LinkButton>
                        </div>




                    </asp:Panel>




                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
     <script type="text/javascript">              
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

</asp:Content>
