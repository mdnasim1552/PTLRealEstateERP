<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RetiredEmployee.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RetiredEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
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

            <div class="card card-fluid container-data mt-5" style="min-height: 1000px;">
                <div class="card-header">
                    <div class="row mb-2">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="d-block" OnClick="ibtnFindCompany_Click">Company</asp:LinkButton>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 ml-1">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnDeptSrch" runat="server" OnClick="imgbtnDeptSrch_Click">Department</asp:LinkButton>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 ml-1">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnSection" runat="server" OnClick="imgbtnSection_Click">Section</asp:LinkButton>
                                <asp:DropDownList ID="ddlSection1" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlSection1_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-2">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" OnClick="imgbtnEmployee_Click">Employee</asp:LinkButton>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 ml-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkBtnSep" runat="server">Separation Type</asp:LinkButton>
                                <asp:DropDownList ID="ddlSepType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkBtnDate" runat="server">Date</asp:LinkButton>
                                <asp:TextBox ID="txtSepDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtSepDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSepDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnAdd_Click">Add</asp:LinkButton>
                            <%--<asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-sm btn-danger primaryBtn pull-left" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>--%>
                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <asp:GridView ID="gvEmpResign" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" Width="450px" OnRowDataBound="gvEmpResign_RowDataBound">
                        <PagerSettings Visible="False" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Id Card">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpId" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Emp Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Resign Type">
                                <ItemTemplate>
                                     <asp:DropDownList ID="ddlgvSepType" runat="server" CssClass="chzn-select form-control" Width="180px"></asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Separation Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSepDate" runat="server" BackColor="Transparent" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sepdate")).ToString("dd-MMM-yyyy") %>' Width="150px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtSepDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtSepDate" Enabled="true"></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDeleteEmp" runat="server" Font-Bold="True" CssClass=" btn btn-xs" OnClick="lbtnDeleteEmp_Click"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


