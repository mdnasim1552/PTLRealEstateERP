<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EngrCheck.aspx.cs" Inherits="RealERPWEB.F_30_Facility.EngrCheck" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">       
        function loadModalComplain() {
            $('#modalEditComplain').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }
        function CloseModalComplain() {
            $('#modalEditComplain').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">

                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <div class="col-2 d-flex align-items-center">
                            <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:DropDownList ID="ddlComplain" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-1 d-flex align-items-center">
                            <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-primary align-self-end" OnClick="btnOKClick_Click">OK</asp:LinkButton>
                        </div>

                    </div>

                    <asp:Panel runat="server" ID="pnlComplain">


                        <div class="row mt-1">
                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="lbldate" class="form-label">Date</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblProjectText" class="form-control"></asp:Label>
                            </div>

                        </div>
                        <div class="row mt-1">
                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblCustomer" class="form-label">Customer</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblCustomerText" class="form-control"></asp:Label>
                            </div>
                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblUnitlabel" class="form-label">Unit</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:Label runat="server" ID="lblUnitText" class="form-control"></asp:Label>
                            </div>

                        </div>
                        <hr />


                        <div class="row mt-1">

                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="Label3" class="form-label">Site Visited</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:TextBox ID="txtSiteVisisted" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtSiteVisisted"></cc1:CalendarExtender>
                            </div>
                            <div class="col-2 d-flex align-items-center">
                                <asp:Label runat="server" ID="Label4" class="form-label">Estimated WD Time</asp:Label>
                            </div>
                            <div class="col-3 d-flex align-items-center">
                                <asp:TextBox ID="txtwdtime" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtwdtime"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <hr />
                        <div id="divComplainList">


                            <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" id="pills-home-tab" data-toggle="pill" href="#pills-home" role="tab" aria-controls="pills-home" aria-selected="true">Problem List-User</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="pills-profile-tab" data-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="false">Diagnosis</a>
                                </li>
                               
                            </ul>
                            <div class="tab-content" id="pills-tabContent">
                                <div class="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">

                                    <div class="row  table-responsive mt-1">

                                        <asp:GridView ID="dgvUser" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Problem Details">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconcatdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainDesc")) %>'
                                                            Width="170px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                            Width="280px" Font-Size="12px" ForeColor="Black"></asp:Label>

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
                                <div class="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                    <div class="row">
                                        <div class="col-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtComplainDesc" placeholder="Write Problem"></asp:TextBox>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtComplainRemarks" placeholder="Write Remarks" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                        </div>
                                        <div class="col-2">
                                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-sm btn-info" OnClick="btnAdd_Click">Add</asp:LinkButton>
                                        </div>
                                    </div>



                                    <div class="row table-responsive mt-1">

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
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkbtnDelete"
                                                            OnClick="LnkbtnDelete_Click" ToolTip="Delete Item" Width="25px">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LnkbtnEdit" OnClick="LnkbtnEdit_Click"
                                                            ToolTip="Edit Item">
                                                <span class="fas fa-edit fa-sm" style="color:blue;" aria-hidden="true" Width="25px" ></span>&nbsp;
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
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


                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkRefresh_Click" Width="100px">Refresh</asp:LinkButton>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" Width="100px">Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkProceed" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2" OnClick="lnkProceed_Click" Width="150px">Proceed to Next Step</asp:LinkButton>
                        </div>


                    </asp:Panel>




                </div>
            </div>

            <div id="modalEditComplain" class="modal animated slideInLeft" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="display: block;">

                            <button type="button" class="close btn btn-xs bg-danger" data-dismiss="modal">
                                <span class="fa fa-close"></span>

                            </button>
                            <h4 class="modal-title">
                                <span class="fa fa-sm fa-table pr-2" runat="server" id="txtheader">Edit Complain</span></h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">

                                <div class="form-group" runat="server">
                                    <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbltype" class="col-md-4">Problem Details</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtProblemDetails" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="catdet">
                                    <asp:Label runat="server" ID="Label2" class="col-md-4">Remarks</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtmodalRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                    </div>
                                </div>

                            </div>



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkUpdateComplain" runat="server" CssClass="btn btn-sm btn-success"
                                OnClientClick="CloseModalComplain();" OnClick="lnkUpdateComplain_Click"><span class="glyphicon glyphicon-save"></span>Update</asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
