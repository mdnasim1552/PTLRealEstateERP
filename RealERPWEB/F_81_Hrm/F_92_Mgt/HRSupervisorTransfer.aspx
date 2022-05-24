<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRSupervisorTransfer.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.HRSupervisorTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });
        };

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data">
                <div class="card-header mt-3 mb-0 pb-0">
                    <h4>Employee Transfer from Supervisor to Supervisor</h4>
                </div>

                <div class="card-body mb-0 pb-0" style="min-height: 450px;">
                    <div class="row mb-0 pb-0">
                        <div class="col-md-8">
                            <div class="row">
                                <asp:Label ID="lbl" runat="server" CssClass="col-2 col-form-label">From Supervisor</asp:Label>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server"
                                        CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvSupEmpDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" OnPageIndexChanging="gvSupEmpDetails_PageIndexChanging"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        HeaderStyle-Font-Size="14px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL." HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: center"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblgvHDept" runat="server" Font-Bold="True"
                                                        Text="Department" Width="120px"></asp:Label>
                                                    <asp:HyperLink ID="hlnkbtnSupEmp" runat="server"
                                                        CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDept" runat="server"
                                                        Font-Size="12px" Font-Underline="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>                                            

                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSection" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                        Width="180px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDesig" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ID Card">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvIdcard" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                        Width="60px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server" Font-Size="12px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvEmpId" runat="server" Visible="false"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllfrm" OnCheckedChanged="chkAllfrm_CheckedChanged" runat="server" AutoPostBack="True" Text=" All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chckTrnsfer" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                        Width="30px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="row mt-0 pb-0" >
                                <asp:Label ID="Label1" runat="server" CssClass="col-4 col-form-label" Visible="false">Page</asp:Label>
                                <div class="col-5">
                                    <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control select2" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="row mt-5 pb-0">
                                <asp:Label ID="Label2" runat="server" CssClass="col-4 col-form-label">To Supervisor </asp:Label>
                                <div class="col-8">
                                    <asp:DropDownList ID="ddlEmpNameTo" data-placeholder="To Employee.." runat="server"
                                        CssClass="select2" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-12 text-center">
                                    <asp:LinkButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btn btn-sm   btn-primary">Transfer</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
