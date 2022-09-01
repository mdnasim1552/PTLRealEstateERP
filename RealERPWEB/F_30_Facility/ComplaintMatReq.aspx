<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ComplaintMatReq.aspx.cs" Inherits="RealERPWEB.F_30_Facility.ComplaintMatReq" %>

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
            font-weight: 300;
            font-size: 14px;
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
                                <asp:Label runat="server" ID="Label3" class="form-label">Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                                <asp:DropDownList ID="ddlDgNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlMatReq" Visible="false">
                        <div class="row mt-1">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label4" class="form-label">Req No</asp:Label>
                                    <asp:Label runat="server" ID="lblReqNo" class="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblReqNoFull" class="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                                    <asp:HiddenField runat="server" ID="lblpactcode" />
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
                                    <asp:Label runat="server" ID="Label1" class="form-label">Warranty</asp:Label>
                                    <asp:Label runat="server" ID="lblWarranty" class="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblWarrantyCode" class="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <p><span>Budget Form</span></p>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label6" CssClass="font-weight-bold">Budget Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblBgdDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label11" CssClass="font-weight-bold">Approved Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblApprDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label9" CssClass="font-weight-bold">Remarks:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblRemarksAppr">No Comments</asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="row table-responsive">

                                    <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea ml-2"
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
                                                        Width="300px"></asp:Label>
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
                                                    <asp:Label ID="lblgvQuantity" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
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
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4 mt-1">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label7" class="form-label mt-2">Narration</asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Notes" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-lg-8 mt-1">
                                <p><span>Description</span></p>
                                <div class="table-responsive ml-1">
                                    <asp:GridView ID="gvDescrip" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea inptNoneBorder ml-1">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>' BorderStyle="None" BackColor="Transparent"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColon" runat="server" Text=" : "></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#F5F5F5" ForeColor="#000" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>

                        </div>

                    </asp:Panel>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
