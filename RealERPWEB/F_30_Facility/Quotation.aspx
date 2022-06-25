﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="RealERPWEB.F_30_Facility.Quotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">

                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:DropDownList ID="ddlDgNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-1 d-flex align-items-center">
                            <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-primary align-self-end" OnClick="btnOKClick_Click">OK</asp:LinkButton>
                        </div>

                    </div>

                    <asp:Panel runat="server" ID="pnlComplain">


                        <div class="row mt-1">
                            <div class="col-1 d-flex align-items-center">
                                <asp:Label runat="server" ID="lbldate" class="form-label">Date</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-1 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblProjectText" class="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-1 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblCustomer" class="form-label">Customer</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblCustomerText" class="form-control"></asp:Label>
                            </div>
                            <div class="col-1 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblUnitlabel" class="form-label">Unit</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblUnitText" class="form-control"></asp:Label>
                            </div>

                        </div>
                        <hr />         
                        <div class="row">

                            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
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
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                     
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
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Enabled="false"
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
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Notes" TextMode="MultiLine" Rows="5"></asp:TextBox>

                                </div>
                            </div>
                        </asp:Panel>



                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkRefresh_Click" Width="100px">Refresh</asp:LinkButton>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" Width="100px" OnClick="lnkSave_Click">Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkProceed" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2" OnClick="lnkProceed_Click" Width="150px">Proceed to Next Step</asp:LinkButton>
                        </div>




                    </asp:Panel>




                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
