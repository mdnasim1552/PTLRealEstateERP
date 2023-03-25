<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmClientInfo02.aspx.cs" Inherits="RealERPWEB.F_21_MKT.CrmClientInfo02" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/crm-new-dashboard.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            let floatingContainer = $(".floating");
            let floatingBtn = $(".floating-btn");
            let floatingHeader = $(".floating-header");

            floatingBtn.click(() => {
                floatingContainer.addClass("active");
            });
            floatingHeader.click(() => {
                floatingContainer.removeClass("active");
            });
            //Dashboard Link
            $("#btnDashboard").click(function () {
                window.location.href = "../F_99_Allinterface/CRMDashboard03?Type=Report";
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrapper">
                <div class="page mt-4">
                    <div class="page">
                        <div class="row mb-4">
                            <div class="col-md-8">
                                <div class="row align-items-end">
                                    <div class="col-2">
                                        <div class="form-group mb-0">
                                            <label for="form-date-range" class="form-label">
                                                Date</label>
                                            <select class="form-select" id="form-date-range">
                                                <option>Today</option>
                                                <option>Yesterday</option>
                                                <option>Last 7 Days</option>
                                                <option>This Month</option>
                                                <option>Last Month</option>
                                                <option>This Year</option>
                                                <option>last Year</option>
                                                <option>Custom</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <select class="form-select">
                                                <option>Choose Employee</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <select class="form-select">
                                                <option>Choose Project</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-4">
                                        <div class="d-flex">
                                            <div class="input-group form-search mb-0 mr-3">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fas fa-search"></i>
                                                    </div>
                                                </div>
                                                <input
                                                    type="text"
                                                    class="form-control"
                                                    id="inlineFormInputGroup"
                                                    placeholder="Search Here" />
                                            </div>
                                            <button class="mmbd-btn mmbd-btn-primary">
                                                Apply
                                            </button>
                                        </div>
                                    </div>
                                    <!-- END -->
                                </div>
                            </div>
                            <div class="col-md-4 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary mr-2" id="btnaddland" runat="server">
                                        <strong><i class="fas fa-user-plus"></i>&nbsp;Add Lead</strong>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary" id="btnDashboard">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt="Dashboard"
                                            class="img-responsive" />
                                        <strong>Dashboard</strong>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- END HEAD -->

                        <div class="mb-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-body pb-0">
                                            <asp:MultiView ID="MultiView1" runat="server">
                                                <asp:View runat="server">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">

                                                                    <asp:HiddenField ID="lblnewprospect" runat="server" />
                                                                    <h4 class="panel-title"><span class="clickable small "><i class="fa fa-minus "></i></span>Basic Information
    					
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body">


                                                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                                        ShowFooter="True" OnRowDataBound="gvPersonalInfo_RowDataBound" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                                        <RowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="classhidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCodeper" ClientIDMode="Static" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgval" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>

                                                                                <ItemTemplate>

                                                                                    <asp:DropDownList ID="ddlcountryPhone" runat="server" CssClass="custom-select chzn-select" Style="float: left; padding-left: 0; padding-right: 0" Visible="false"
                                                                                        Width="120px">
                                                                                        <asp:ListItem Selected="True" Value="+88">+88</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                    <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" OnTextChanged="txtgvVal_TextChanged1" AutoPostBack="true"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">

                                                                                        <div class="form-group">
                                                                                            <asp:DropDownList ID="ddlval" runat="server" OnDataBound="ddlval_DataBound" Width="300px" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title"><span class="clickable small "><i class="fa fa-minus "></i></span>Source Information 					
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <asp:GridView ID="gvSourceInfo" runat="server" AutoGenerateColumns="False"
                                                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow" OnRowDataBound="gvSourceInfo_RowDataBound">
                                                                        <RowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDescsr" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgvalsr" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">
                                                                                        <div class="form-group mt-2">
                                                                                            <asp:DropDownList ID="ddlval" runat="server" Width="300px" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlIREmp" runat="server" Visible="false">
                                                                                        <div class="form-group mt-2">
                                                                                            <asp:DropDownList ID="ddlIREmp" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title" runat="server" id="hpref"><span class="clickable small"><i class="fa fa-plus "></i></span>
                                                                        <asp:Label ID="lblheadprospect" runat="server" Text="Prospect's Preference"></asp:Label>
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <asp:GridView ID="gvpinfo" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpinfo_RowDataBound"
                                                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                                        <RowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgvalpinf" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>

                                                                                <ItemTemplate>

                                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">



                                                                                        <div class="form-group mt-2">

                                                                                            <asp:DropDownList ID="ddlvalcom" runat="server" Width="300px" OnSelectedIndexChanged="ddlvalcom_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>


                                                                                        </div>



                                                                                        <div class="form-group mt-2">

                                                                                            <asp:DropDownList ID="ddlvalpros" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>


                                                                                        </div>


                                                                                    </asp:Panel>


                                                                                    <asp:Panel ID="pnlMullocation" runat="server" Visible="false">
                                                                                        <asp:ListBox ID="lstlocation" runat="server" SelectionMode="Multiple" Style="width: 300px !important;"
                                                                                            data-placeholder="Choose Location......" multiple="true" class="form-control chosen-select"></asp:ListBox>

                                                                                    </asp:Panel>

                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>Home Information  					
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body" style="display: none;">
                                                                    <asp:GridView ID="gvplot" runat="server" AutoGenerateColumns="False"
                                                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                                        <RowStyle />
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDescp" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgvalplot" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>

                                                                                <ItemTemplate>

                                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>




                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvalplot" runat="server" CssClass="ddlcountry chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalplot_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnldist" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvald" runat="server" CssClass=" chzn-select form-control" Width="300px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlvald_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlz" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvalz" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalz_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlp" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvalp" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalp_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnla" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvala" runat="server" CssClass=" chzn-select form-control" Width="300px">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="PanelBl" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlblock" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlblock_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>

                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>Business Address  					
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body" style="display: none;">
                                                                    <asp:GridView ID="gvbusinfo" runat="server" AutoGenerateColumns="False"
                                                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                                        <RowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgvalbuinf" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server" cssclass="ml-1 form-control"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12 pading5px">

                                                                                                <asp:DropDownList ID="ddlvalplot" runat="server" CssClass="ddlcountry chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalbusinfo_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnldist" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvald" runat="server" CssClass=" chzn-select form-control" Width="300px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlvaldbusinfo_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlz" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvalz" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalzbusinfo_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlp" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvalp" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalpbusinfo_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnla" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlvala" runat="server" CssClass=" chzn-select form-control" Width="300px">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="PanelBl" runat="server">

                                                                                        <div class="form-group mt-2">
                                                                                            <div class="col-md-12 pading5px">
                                                                                                <asp:DropDownList ID="ddlblock" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlblockbusinfo_SelectedIndexChanged">
                                                                                                </asp:DropDownList>

                                                                                            </div>
                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>More Information 					
                                                                    </h4>
                                                                </div>
                                                                <div class="panel-body" style="display: none;">
                                                                    <asp:GridView ID="gvMoreInfo" runat="server" AutoGenerateColumns="False"
                                                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                                        <RowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                                        Width="20px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                <ItemStyle Font-Bold="True" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvgvalminfo" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>

                                                                                <ItemTemplate>

                                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                                    <cc1:calendarextender id="txtgvdVal_CalendarExtender" runat="server"
                                                                                        enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtgvdVal">
                                                                                    </cc1:calendarextender>
                                                                                    <asp:Panel ID="Panegrd" runat="server">

                                                                                        <div class="form-group mt-2">

                                                                                            <asp:DropDownList ID="ddlval" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                                                            </asp:DropDownList>


                                                                                        </div>


                                                                                    </asp:Panel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row mb-2 btnsavefix">

                                                        <div class="w-100">
                                                            <%--//OnClientClick="javascript:return funDupAllMobile();"--%> <%--Req by Emdad by for new add country code 20221023--%>
                                                            <asp:LinkButton ID="lnkUpdate" runat="server"
                                                                CssClass="btn btn-primary" OnClick="lnkUpdate_Click">Save</asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </asp:View>
                                                <asp:View ID="View2" runat="server">
                                                    <asp:Panel ID="pnpRecFilter" runat="server">
                                                        <div class="row">
                                                            <label class="control-label col-md-1">Filter</label>
                                                            <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select col-md-2 mr-1 mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlDist" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPStat" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlPStat_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlPri" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlStatus" data-placeholder="Choose Status......" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlOther" runat="server" ClientIDMode="Static" CssClass="custom-select chzn-select scroll col-md-1 ml-1 ">
                                                                <asp:ListItem Value="1">Prospect Name</asp:ListItem>
                                                                <asp:ListItem Value="2">PID</asp:ListItem>
                                                                <asp:ListItem Value="3">Phone</asp:ListItem>
                                                                <asp:ListItem Value="4">Email</asp:ListItem>
                                                                <asp:ListItem Value="5">NID</asp:ListItem>
                                                                <asp:ListItem Value="6">TIN</asp:ListItem>
                                                                <asp:ListItem Value="7">Prefered Area</asp:ListItem>
                                                                <asp:ListItem Value="8">Profission</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="9">Choose One.....</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtVal" runat="server" CssClass="form-control col-md-1 ml-1" TextMode="Search"></asp:TextBox>

                                                            <div class="col-md-1">
                                                                <asp:LinkButton ID="SrchBtn" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="SrchBtn_Click">Search</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="row">
                                                        <div class="col-md-10">
                                                            <div class="row mt-1">
                                                                <asp:Label runat="server" ID="lbleads" class="control-label col-md-1">Leads</asp:Label>
                                                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control col-md-2  ml-1"></asp:TextBox>
                                                                <cc1:calendarextender id="Cal3" runat="server"
                                                                    format="dd-MMM-yyyy" targetcontrolid="txttodate">
                                                                </cc1:calendarextender>
                                                                <label class="lblmargin-top9px col-md-1 ml-1" for="todate" runat="server" id="lbltodatekpi" visible="false">To</label>

                                                                <asp:TextBox ID="txtkpitodate" runat="server" CssClass="form-control col-md-2 ml-1" Visible="false"></asp:TextBox>
                                                                <cc1:calendarextender id="txtkpitodate_CalendarExtender" runat="server"
                                                                    format="dd-MMM-yyyy" targetcontrolid="txtkpitodate">
                                                                </cc1:calendarextender>
                                                                <div class="col-md-2">
                                                                    <input type="text" id="myInput" onkeyup="Search_Gridview(this);" placeholder="Search.." title="Type" class="form-control">
                                                                </div>


                                                                <div class="col-md-1">

                                                                    <%--<label class="control-label">Page</label>--%>
                                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control custom-select"
                                                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>30</asp:ListItem>
                                                                        <asp:ListItem>40</asp:ListItem>
                                                                        <asp:ListItem Selected="true">50</asp:ListItem>
                                                                        <asp:ListItem>100</asp:ListItem>
                                                                        <asp:ListItem>150</asp:ListItem>
                                                                        <asp:ListItem>200</asp:ListItem>
                                                                        <asp:ListItem>300</asp:ListItem>
                                                                        <asp:ListItem>600</asp:ListItem>
                                                                        <asp:ListItem>900</asp:ListItem>
                                                                        <asp:ListItem>1000</asp:ListItem>
                                                                        <asp:ListItem>2000</asp:ListItem>
                                                                        <asp:ListItem>3000</asp:ListItem>
                                                                        <asp:ListItem>4000</asp:ListItem>
                                                                        <asp:ListItem>5000</asp:ListItem>
                                                                        <asp:ListItem>7000</asp:ListItem>
                                                                        <asp:ListItem>8000</asp:ListItem>
                                                                        <asp:ListItem>10000</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <%--<asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>--%>

                                                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control col-md-2 ml-1" Visible="false"></asp:TextBox>
                                                                <cc1:calendarextender id="Cal2" runat="server"
                                                                    format="dd-MMM-yyyy" targetcontrolid="txtfrmdate">
                                                                </cc1:calendarextender>
                                                                <div class="cold-md-1 ml-1">
                                                                    <asp:LinkButton ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" CssClass="btn btn-success"></asp:LinkButton>
                                                                </div>

                                                                <div class="col-md-2 text-danger" id="divPermntDel" runat="server">
                                                                    <asp:CheckBox ID="Chkpdelete" runat="server" CssClass="form-control checkbox" Text="&nbsp;P.Delete" />

                                                                </div>



                                                            </div>


                                                            <div class="table-responsive">

                                                                <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSummary_RowDataBound"
                                                                    ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea" AllowPaging="True" OnPageIndexChanging="gvSummary_PageIndexChanging">
                                                                    <RowStyle Font-Size="12px" Height="25px" Font-Names="Century Gothic" />
                                                                    <Columns>

                                                                        <%--0--%>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")  %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <%--1--%>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" ToolTip="Edit Header" OnClick="lnkgvHeader_Click"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>

                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete"
                                                                                    runat="server" Font-Bold="True" ToolTip="Delete" Style="text-align: right;" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click">

                                                        <i class=" fa fa-trash"></i></asp:LinkButton>




                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--2--%>


                                                                        <asp:TemplateField HeaderText="">

                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="ViewData" runat="server" Font-Bold="True" Height="12px" ToolTip="View" Style="text-align: right" OnClick="ViewData_Click"><span><i class="fa fa-eye" aria-hidden="true"></i></span></asp:LinkButton>



                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--3--%>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                                    CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" Font-Bold="True" Height="12px" Style="text-align: right" ToolTip="Edit Client Info" Text="Edit" OnClick="lnkEdit_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />



                                                                        </asp:TemplateField>
                                                                        <%--4--%>

                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lsircode" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                                                                <asp:Label ID="ldesig" runat="server" Width="40px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--5--%>
                                                                        <asp:TemplateField HeaderText="P-ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lsircode1" runat="server" Width="40px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--6--%>
                                                                        <asp:TemplateField HeaderText="Generated">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgenerated" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reassign Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvrassigndate" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rassigndat"))%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <%--7--%>

                                                                        <asp:TemplateField HeaderText="Prospect Details">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ldesc" runat="server" Width="130px"
                                                                                    Text='<%# 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim()
                                                                         
                                                                    %>'>

                                                             


                                                                                </asp:Label>



                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>


                                                                        <%--8--%>
                                                                        <asp:TemplateField HeaderText="Followup">
                                                                            <ItemTemplate>

                                                                                <asp:Panel ID="pnlfollowup" runat="server" Width="110px" ClientIDMode="Static">

                                                                                    <asp:Label ID="lbllfollowuplink" Width="70px" Font-Size="11px" runat="server" ToolTip="Followup" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'>
                                                                                    </asp:Label>

                                                                                    <asp:LinkButton ID="lbtnView" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="View" runat="server" OnClick="lbtnView_Click" CssClass="d-none"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                                                    <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                                                </asp:Panel>



                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>


                                                                        <%--9--%>

                                                                        <asp:TemplateField HeaderText="Lead Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lprefdesc" runat="server" Width="120px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--10--%>

                                                                        <asp:TemplateField HeaderText="Associate">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lassoc" runat="server" Width="90px"
                                                                                    Style="text-align: center"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />

                                                                        </asp:TemplateField>
                                                                        <%--11--%>

                                                                        <asp:TemplateField HeaderText="Team Leader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblbusername" runat="server" Width="90px"
                                                                                    Style="text-align: center"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />

                                                                        </asp:TemplateField>
                                                                        <%--12--%>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbllstatus" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--13--%>

                                                                        <asp:TemplateField HeaderText="Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="llTyp" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%-- <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lappdat" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                                                        <%-- <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lLSrc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                                        <%--14--%>
                                                                        <asp:TemplateField HeaderText="Active" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkAct" ClientIDMode="Static" Width="12" ToolTip="" runat="server" OnClick="lnkAct_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--15--%>



                                                                        <asp:TemplateField HeaderText="Mobile">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvphone" runat="server" Width="80px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--16--%>
                                                                        <asp:TemplateField HeaderText="Email">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvemail" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--17--%>


                                                                        <asp:TemplateField HeaderText="Occupation">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvoccupation" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--18--%>


                                                                        <asp:TemplateField HeaderText="Residence">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcaddress" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--19--%>



                                                                        <asp:TemplateField HeaderText="Interested Project">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvpactdesc" runat="server" Width="60px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--20--%>


                                                                        <asp:TemplateField HeaderText="Source">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvLSrc" runat="server" Width="100px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname"))=="" ?"": "(" + Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname")) + ")")
                                                                
                                                            %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--21--%>


                                                                        <asp:TemplateField HeaderText="Last discussion">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Width="100px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--22--%>


                                                                        <asp:TemplateField HeaderText="Notes">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--23--%>

                                                                        <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>



                                                                        <%--24--%>

                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvempid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <%--25--%>

                                                                        <asp:TemplateField HeaderText="Retreive" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retrieve Prospect" Style="text-align: right" OnClientClick="javascript:return  FunRetProsConfirm()" OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="center" />
                                                                        </asp:TemplateField>
                                                                        <%--26--%>
                                                                        <asp:TemplateField HeaderText="Next Followup" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbllfollowuplinkkpisum" Width="90px" runat="server"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>                                                               
                                                                                </asp:Label>

                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--27--%>
                                                                        <asp:TemplateField HeaderText="Project Visit<br>Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprojvisit" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projvisit")) %>'></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <%--28--%>
                                                                        <asp:TemplateField HeaderText="Entry By" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgventryby" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryby")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <FooterStyle CssClass="grvFooter" />
                                                                    <EditRowStyle />
                                                                    <AlternatingRowStyle />
                                                                    <PagerSettings Mode="NumericFirstLast" />
                                                                    <PagerStyle CssClass="gvPagination" />
                                                                    <HeaderStyle CssClass="grvHeader" />
                                                                </asp:GridView>

                                                                <asp:GridView ID="gvkpi" runat="server" AutoGenerateColumns="False"
                                                                    ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea ">
                                                                    <RowStyle Height="25px" />
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNokpi" runat="server" Font-Bold="True"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgbempid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>





                                                                        <asp:TemplateField HeaderText="Employee Name">


                                                                            <HeaderTemplate>
                                                                                <div class="row">
                                                                                    <div class="col-md-9">
                                                                                        <asp:Label ID="lblgvheadername" runat="server">Employee Name</asp:Label>

                                                                                    </div>


                                                                                    <div class="col-md-2">
                                                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                                                    </div>


                                                                                </div>


                                                                            </HeaderTemplate>



                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lowner" runat="server" Width="200px" Font-Size="10px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFtxtTotal" runat="server" Style="text-align: center"
                                                                                    Width="60px" Text="Total"></asp:Label>
                                                                            </FooterTemplate>

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Call">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpicall" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpicall_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "call")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFcallsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ext. Meeting">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpiextmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiextmeeting_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "extmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFexmeetingsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Internal. Meeting">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpiintmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiintmeeting_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFintmeetingsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />

                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="visit">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpivisit" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpivisit_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "visit")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFvisitsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>






                                                                        <asp:TemplateField HeaderText="Proposal">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpiproposal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiproposal_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proposal")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFproposalsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />

                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Leads">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpileads" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpileads_Click1"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leads")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFleadssum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />

                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Closing">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpiclosing" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiclosing_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "close")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFclosingsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <FooterStyle HorizontalAlign="Right" />

                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Others">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpiothers" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiothers_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFotherssum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>

                                                                            <ItemStyle HorizontalAlign="Right" />

                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Total">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lblgvkpitotal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpitotal_Click"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblgvFtotalsum" runat="server" Style="text-align: center"
                                                                                    Width="60px"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="center" VerticalAlign="Middle" Font-Size="12px" />

                                                                        </asp:TemplateField>






                                                                    </Columns>
                                                                    <FooterStyle CssClass="grvFooter" />
                                                                    <EditRowStyle />
                                                                    <AlternatingRowStyle />
                                                                    <PagerStyle CssClass="gvPagination" />
                                                                    <HeaderStyle CssClass="grvHeader" />
                                                                </asp:GridView>



                                                            </div>

                                                            <div class="col-md-12">

                                                                <asp:Label runat="server" ID="lblkpiDetails" Visible="false" CssClass="d-block" Text="Kpi Details"></asp:Label>

                                                            </div>


                                                            <asp:GridView ID="gvkpidet" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvkpidet_RowDataBound"
                                                                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea " PageSize="10">
                                                                <RowStyle Font-Size="11px" Height="25px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDeletekpisum" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" Text="Delete" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDeletekpisum_Click"><span class=" fa   fa-recycle"></span></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNokpisum" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lsircodekpisum" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode"))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lsircode1kpisum" runat="server" Width="50px"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1"))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Generated">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgeneratedkpisum" runat="server" Width="70px"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Followup">
                                                                        <ItemTemplate>

                                                                            <asp:Panel ID="pnlfollowupkpisum" runat="server" Width="110px" ClientIDMode="Static">



                                                                                <asp:Label ID="lbllfollowuplinkkpisum" Width="70px" runat="server"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>
                                                                                             


                                                                                </asp:Label>

                                                                                <asp:LinkButton ID="lbtnViewkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="View" runat="server" OnClick="lbtnViewkpisum_Click"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                                                <asp:LinkButton ID="lnkEditfollowupkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowupkpisum_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                                            </asp:Panel>




                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExelkpisum" runat="server"
                                                                                CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditkpisum" runat="server" Font-Bold="True" Height="16px" Style="text-align: center" ToolTip="Edit Land & Owner Info" Text="Edit" OnClientClick="javascript:return  FunConfirmEdit()" OnClick="lnkEditkpisum_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />



                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Prospect Details">
                                                                        <%-- <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproperty" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Property Details" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ldesckpisum" runat="server" Width="220px" Style="font-size: 10px;"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim() %>'>
                                                                            </asp:Label>



                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Lead Status">
                                                                        <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchstatus" SortExpression="lstatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllstatuskpisum" runat="server" Width="110px" Style="text-align: left"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta"))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />

                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderText="Associate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lassockpisum" runat="server" Width="90px"
                                                                                Style="text-align: left"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                                        </ItemTemplate>


                                                                    </asp:TemplateField>
                                                                    <%--10--%>

                                                                    <asp:TemplateField HeaderText="Team Leader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvteamdesckpi" runat="server" Width="90px"
                                                                                Style="text-align: left"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                                        </ItemTemplate>


                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Notes">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvempidkpisum" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <FooterStyle CssClass="grvFooter" />
                                                                <EditRowStyle />
                                                                <AlternatingRowStyle />
                                                                <PagerStyle CssClass="gvPagination" />
                                                                <HeaderStyle CssClass="grvHeader" />
                                                            </asp:GridView>
                                                        </div>

                                                        <div class="col-md-2 marapaddingzero">
                                                            <label class="control-label" style="font-size: 14px; font-weight: bold">Notification</label>


                                                            <div class="list-group list-group-bordered mb-3 notifsectino">
                                                                <asp:LinkButton ID="lnkbtnDws" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDws_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-primary">SW </div>
                                                                    </div>
                                                                    <div class="list-group-item-body" id="tdaswhtxt" runat="server">Schedules Work</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span1" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkbtnTODayTask" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnTODayTask_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-primary">TD </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">To Day Task</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span2" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>


                                                                <asp:LinkButton ID="lnkBtnDwr" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDwr_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-success">DWR </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">
                                                                        Daily Work Report
                                                                    </div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span3" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnKpi" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnKpi_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-info">KPI </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">
                                                                        Key Performance Indicator
                                                                    </div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span4" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>



                                                                <asp:LinkButton ID="lnkBtnCall" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnCall_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-warning">Call </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Call</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span5" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnVisit" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnVisit_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-success">Visit </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Visit</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span6" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>


                                                                <asp:LinkButton ID="lnkBtnDaypassed" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDaypassed_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-danger">DP  </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Day Passed</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span7" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lbtnpme" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnpme_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-pink">PME</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">
                                                                        Project Meeting External
                                                                    </div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span8" runat="server">0</span>
                                                                        </button>
                                                                    </div>

                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lbtnpmi" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnpmi_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-dark">PMI</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">
                                                                        Project Meeting Internal
                                                                    </div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span9" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnComments" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnComments_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-indigo">COM</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Comments</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span10" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnFreezing" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnFreezing_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-purple">FRE</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Freezing</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span11" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnDeadProspect" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDeadProspect_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-pink">DP </div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Dead Pros</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span12" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lbtnSigned" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnSigned_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-dark">Si</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Signed</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span13" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnDatablank" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDatablank_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-primary">DB</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Data Bank</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="Span14" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="lnkBtnPotentialPros" runat="server" class="list-group-item list-group-item-action" OnClick="lnkBtnPotentialPros_Click">
                                                                    <div class="list-group-item-figure">
                                                                        <div class="tile tile-circle bg-red">PP</div>
                                                                    </div>
                                                                    <div class="list-group-item-body">Potential</div>
                                                                    <div class="list-group-item-figure">
                                                                        <button class="btn btn-sm btn-light">
                                                                            <span class="badge badge-pill badge-danger" id="lblPotential" runat="server">0</span>
                                                                        </button>
                                                                    </div>
                                                                </asp:LinkButton>


                                                                <asp:HiddenField ID="hdnlblattribute" runat="server" />


                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                            <div class="table-responsive">
                                                <table
                                                    class="table table-bordered mmbd-table table-striped table-header-flash">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col" class="text-left">
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <div
                                                                        class="dropdown mmbd-dropdown-icon mr-2">
                                                                        <button
                                                                            class="btn btn-secondary dropdown-toggle"
                                                                            type="button"
                                                                            data-toggle="dropdown"
                                                                            aria-expanded="false">
                                                                            <i class="fa fa-filter"></i>
                                                                        </button>
                                                                        <div class="dropdown-menu">
                                                                            <span class="dropdown-item">Green
                                                                            </span>
                                                                            <span class="dropdown-item">Red
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <span class="header-label">P-ID </span>
                                                                </div>
                                                            </th>
                                                            <th scope="col" class="text-center">Generated Date
                                                            </th>
                                                            <th scope="col" class="text-center">Associate Name
                                                            </th>
                                                            <th scope="col" class="text-center">Project Name
                                                            </th>
                                                            <th scope="col" class="text-center">Source Name
                                                            </th>
                                                            <th scope="col" class="text-center">Followup Date
                                                            </th>
                                                            <th scope="col" class="text-center">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-center">
                                                                    <div
                                                                        class="dropdown mmbd-dropdown-icon mr-2">
                                                                        <button
                                                                            class="btn btn-secondary dropdown-toggle"
                                                                            type="button"
                                                                            data-toggle="dropdown"
                                                                            aria-expanded="false">
                                                                            <i class="fa fa-filter"></i>
                                                                        </button>
                                                                        <div class="dropdown-menu">
                                                                            <span class="dropdown-item">Query
                                                                            </span>
                                                                            <span class="dropdown-item">Lead
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <span class="header-label">Lead Status
                                                                    </span>
                                                                </div>
                                                            </th>
                                                            <th scope="col" class="text-center">Prospect Name
                                                            </th>
                                                            <th scope="col" class="text-center">Last Discussion
                                                            </th>
                                                            <th scope="col" class="text-center w-70px">ACTION
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                                            </td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div class="w-30px">
                                                                        <div
                                                                            class="form-check form-check-sm form-check-custom form-check-solid">
                                                                            <input
                                                                                class="form-check-input"
                                                                                type="checkbox" />
                                                                        </div>
                                                                    </div>
                                                                    <span class="text-success">#3066 </span>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">
                                                                <div class="d-flex flex-column">
                                                                    Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">Edison Amour</td>
                                                            <td class="text-center">Facebook</td>
                                                            <td class="text-center">Jan 6, 2022</td>
                                                            <td class="text-center">
                                                                <div class="badge badge-light">
                                                                    <i class="fas fa-check"></i>Query
                                                                </div>
                                                            </td>
                                                            <td class="text-center">Md. Ariful Islam Shanto
                                                            </td>
                                                            <td class="text-center">I have talked...</td>
                                                            <td class="text-right">
                                                                <div class="d-flex">
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fas fa-eye"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                        <i class="fa fa-edit"></i>
                                                                    </button>
                                                                    <button
                                                                        class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                        <i class="fa fa-handshake"></i>
                                                                    </button>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class="d-flex align-items-center">
                                                        <div class="length">
                                                            <select
                                                                class="form-control form-control-sm mb-0">
                                                                <option value="25">25</option>
                                                                <option value="50">50</option>
                                                                <option value="100">100</option>
                                                            </select>
                                                        </div>
                                                        <div class="info ml-2">
                                                            Showing 1 to 19 of 19 records
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <nav class="table-pagination">
                                                        <ul class="pagination">
                                                            <li class="page-item mr-3">
                                                                <a class="page-link previews" href="#"><i class="fas fa-arrow-left"></i>
                                                                    Previous</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link active" href="#">1</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">2</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">3</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link">...</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">8</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">9</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">10</a>
                                                            </li>
                                                            <li class="page-item ml-3">
                                                                <a class="page-link next" href="#">Next<i class="fas fa-arrow-right"></i></a>
                                                            </li>
                                                        </ul>
                                                    </nav>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->

                        <div class="floating">
                            <div class="floating-btn">
                                <i class="fas fa-angle-up"></i>
                                <span>To</span>
                                <span>Do</span>
                            </div>
                            <div class="floating-panel">
                                <div class="floating-header">
                                    <div class="floating-title">TO DO</div>
                                    <div class="floating-action">
                                        <i class="fas fa-angle-down down"></i>
                                        <i class="fas fa-angle-up up"></i>
                                    </div>
                                </div>
                                <div class="floating-body">
                                    <ul>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-primary">SW</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Schedules Work</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldws" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-purple">TD</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">To Day Task</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbltdt" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">DWR</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Daily Work Report</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldwr" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-dark">KPI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Key Performance Indicator</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-blue">Call</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Call</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblCall" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Visit</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Visit</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblvisit" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-red">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Day Passed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDayPass" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-warning">PME</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting External</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpme" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">PMI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting Internal</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpmi" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-indigo">COM</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Comments</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblComments" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-orange">FRE</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Freezing</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblFreez" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-cyan">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Dead Pros</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDeadProspect" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Si</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Signed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblcsigned" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-danger">DB</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Data Bank</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDatablank" runat="server">0</div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <!-- Modal -->

            <div id="mdiscussion" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width  ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Discussion </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">




                            <div class="row">

                                <div class="col-xs-7 col-sm-7 col-md-7">

                                    <p>
                                        <strong>PID: </strong><span id="lblPID" runat="server"></span>
                                        <br>
                                        <strong><span id="lblprosname" runat="server"></span></strong>
                                        <br />
                                        <strong>Contact Person: </strong><span id="lblContactPerson" runat="server"></span>
                                        <br>
                                        <strong>Primary : </strong><span id="lblprosphone" runat="server"></span>

                                        <br>
                                        <strong>Home Address: </strong><span id="lblprosaddress" runat="server"></span>
                                        <br>

                                        <strong>Notes: </strong><span id="lblnotes" runat="server"></span>
                                        <br>
                                    </p>

                                    <p>

                                        <strong>Prefered Area: </strong><span id="lblpreferloc" runat="server"></span>
                                        <br>
                                        <strong>Appartment Size: </strong><span id="lblaptsize" runat="server"></span>
                                        <br>
                                        <strong>Profession: </strong><span id="lblProfession" runat="server"></span>
                                        <br>
                                        <strong>Source: </strong><span id="lblSource" runat="server"></span>

                                        <asp:HiddenField ID="lblproscod" runat="server" />
                                        <asp:HiddenField ID="lbleditempid" runat="server" />
                                        <asp:HiddenField ID="lblgeneratedate" runat="server" />
                                    </p>
                                </div>

                                <div class="col-xs-2 col-sm-2 col-md-2 ">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary ml-1" type="button">Rate</button>
                                        </div>

                                        <asp:DropDownList ID="ddlRating" runat="server" OnSelectedIndexChanged="ddlRating_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0.00">0</asp:ListItem>
                                            <asp:ListItem Value="5.00">5</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>


                                </div>

                                <div class="col-xs-3 col-sm-3 col-md-3 ">

                                    <%--<asp:LinkButton ID="lbtntfollowupcs" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtntfollowup" data-toggle="collapse" data-target="#followup"><i class="fa fa-handshake"></i>Followup</button>
                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnStatus"><i class="fa  fa-star-and-crescent"></i><span id="lbllaststatus" runat="server">Status</span></button>

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnprestatus" runat="server"><i class="fa  fa-star-and-crescent"></i><span id="lblprelaststatus" runat="server">Previous</span></button>
                                    <asp:HiddenField ID="hiddenLedStatus" runat="server" />
                                    <asp:HiddenField ID="hdlpreleadst" runat="server" />
                                    <asp:HiddenField ID="hdncompany" ClientIDMode="Static" runat="server" />

                                    <%--<asp:LinkButton ID="lbtntfollowup" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>
                                    <%-- <asp:LinkButton ID="lbtnStatus" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnStatus_Click"> <i  class="fa  fa-star-and-crescent"></i> Status</asp:LinkButton>
                                 <asp>:LinkButton ID="lbtnMap" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa   fa-map"></i> Map</asp:LinkButton>--%>
                                </div>

                            </div>


                            <div id="Status" class="col-md-12 collapse">
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px">Status</label>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlmStatus" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <button type="button" id="btnStatus" class="btn btn-primary  btn-sm " onclick="funStatus();">Update</button>

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-12 col-lg-12">

                                    <div id="followup" class="collapse">

                                        <asp:GridView ID="gvInfo" runat="server" AllowPaging="false"
                                            AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table-condensed table-hover table-bordered grvContentarea">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNodis" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="displayhide">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCodedis" ClientIDMode="Static" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcGrpdis" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1dis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle VerticalAlign="Middle" Width="130px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgpdis" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                            Width="5px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvaldis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <FooterTemplate>

                                                        <asp:LinkButton ID="lbtnUpdateDiscussion" runat="server" OnClientClick="CloseModaldis();" OnClick="lbtnUpdateDiscussion_Click" CssClass="btn  btn-success btn-xs ">Final Update</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <ItemTemplate>



                                                        <asp:TextBox ID="txtgvValdis" runat="server" BorderWidth="0" BackColor="Transparent" Font-Size="14px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>




                                                        <asp:TextBox ID="txtgvdValdis" CssClass="disable_past_dates" runat="server" BorderWidth="0" Style="width: 80px; float: left;" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtgvdValdis_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValdis"></cc1:CalendarExtender>

                                                        <asp:Panel ID="pnlTime" runat="server" Visible="false">
                                                            <asp:DropDownList ID="ddlhour" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                <asp:ListItem Value="09" Selected="True">09</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>

                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlMmin" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                <asp:ListItem Value="31">31</asp:ListItem>
                                                                <asp:ListItem Value="32">32</asp:ListItem>
                                                                <asp:ListItem Value="33">33</asp:ListItem>
                                                                <asp:ListItem Value="34">34</asp:ListItem>
                                                                <asp:ListItem Value="35">35</asp:ListItem>
                                                                <asp:ListItem Value="36">36</asp:ListItem>
                                                                <asp:ListItem Value="37">37</asp:ListItem>
                                                                <asp:ListItem Value="38">38</asp:ListItem>
                                                                <asp:ListItem Value="39">39</asp:ListItem>
                                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                                <asp:ListItem Value="41">41</asp:ListItem>
                                                                <asp:ListItem Value="42">42</asp:ListItem>
                                                                <asp:ListItem Value="43">43</asp:ListItem>
                                                                <asp:ListItem Value="44">44</asp:ListItem>
                                                                <asp:ListItem Value="45">45</asp:ListItem>
                                                                <asp:ListItem Value="46">46</asp:ListItem>
                                                                <asp:ListItem Value="47">47</asp:ListItem>
                                                                <asp:ListItem Value="48">48</asp:ListItem>
                                                                <asp:ListItem Value="49">49</asp:ListItem>
                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                <asp:ListItem Value="51">51</asp:ListItem>
                                                                <asp:ListItem Value="52">52</asp:ListItem>
                                                                <asp:ListItem Value="53">53</asp:ListItem>
                                                                <asp:ListItem Value="54">54</asp:ListItem>
                                                                <asp:ListItem Value="55">55</asp:ListItem>
                                                                <asp:ListItem Value="56">56</asp:ListItem>
                                                                <asp:ListItem Value="57">57</asp:ListItem>
                                                                <asp:ListItem Value="58">58</asp:ListItem>
                                                                <asp:ListItem Value="59">59</asp:ListItem>


                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlslb" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                                                <asp:ListItem Value="PM">PM</asp:ListItem>




                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblschedulenumber" runat="server" BorderWidth="0" CssClass="btn btn-success btn-xs" Font-Size="14px"
                                                                Text="Schedule(0)"></asp:Label>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlStatus" runat="server" Visible="false">


                                                            <asp:CheckBoxList ID="ChkBoxLstStatus" ClientIDMode="Static" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="form-control checkbox">
                                                            </asp:CheckBoxList>

                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlParic" runat="server" Visible="false">
                                                            <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple" class="form-control chosen-select" Style="width: 300px !important;"
                                                                data-placeholder="Choose Person......" multiple="true"></asp:ListBox>

                                                        </asp:Panel>


                                                        <%-- <asp:Panel ID="Pnlcompany" runat="server">--%>
                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                            TabIndex="12">
                                                        </asp:DropDownList>
                                                        <%--</asp:Panel>--%>


                                                        <asp:Panel ID="PnlProject" runat="server">
                                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                                TabIndex="12">
                                                            </asp:DropDownList>

                                                            <%--   <asp:ListBox ID="lstProject"  SelectionMode="Multiple" runat="server"  class="form-control chosen-select" Style="width: 300px !important;"
                                                                data-placeholder="Choose Project" multiple="true"></asp:ListBox>--%>
                                                        </asp:Panel>
                                                        <asp:Panel ID="PnlUnit" runat="server">
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;"
                                                                TabIndex="12">
                                                            </asp:DropDownList>
                                                        </asp:Panel>


                                                        <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                            <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;">
                                                            </asp:DropDownList>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlFollow" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>



                                                            <asp:CheckBoxList ID="ChkBoxLstFollow" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="form-control checkbox">
                                                            </asp:CheckBoxList>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlLostResion" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>


                                                            <asp:DropDownList ID="checkboxReson" Visible="false" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;">
                                                            </asp:DropDownList>


                                                        </asp:Panel>



                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Width="700px" />
                                                </asp:TemplateField>





                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>


                                    </div>
                                </div>

                            </div>
                            <div class="row">


                                <div class="col-md-12 col-lg-12">
                                    <asp:Repeater ID="rpclientinfo" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>


                                            <div class="col-md-12  col-lg-12">
                                                <div class="well">

                                                    <div class="col-sm-12 panel">

                                                        <div class=" col-sm-12">

                                                            <p>
                                                                <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>




                                                                <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                                <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                                <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                                <br>
                                                            </p>







                                                        </div>

                                                        <%--<button type="button" class="btn  btn-primary btn-xs" id="lbtnReschdule" data-toggle="collapse" data-target='<%# "#divreschedule"+DataBinder.Eval(Container, "DataItem.rownum").ToString() %>'>Reschedule</button>--%>

                                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnreschedule" onclick="funReschedule('<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Re-Schdule</button>

                                                        <button type="button" class="btn btn-primary btn-xs" id="lbtnCancel" onclick="funCancel('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>')">Delete</button>
                                                        <asp:LinkButton ID="lbtnFollowup" CssClass="btn btn-primary btn-xs d-none" runat="server" OnClick="lbtnFollowup_Click"> Followup</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnAddition" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnAddition_Click">Addition</asp:LinkButton>
                                                        <button type="button" class="btn btn-primary btn-xs" runat="server" id="lbtnComments" data-toggle="collapse" data-target='<%# "#dcomments"+DataBinder.Eval(Container, "DataItem.rownum").ToString() %>'>Comments</button>

                                                        <div class="col-md-12 collapse dcomments" id="divreschedule<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">



                                                            <asp:TextBox ID="txtdate" runat="server" ClientIDMode="Static" CssClass=""></asp:TextBox>
                                                            <cc1:CalendarExtender ID="Cal2" runat="server"
                                                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                                            Subject:
                                                    <textarea name="lblsubjects" id="lblsubjects" style="width: 300px"></textarea>
                                                            Reason:
                                                    <textarea name="lblreason" id="lblreason" style="width: 300px"></textarea>

                                                            <%--<button type="button" class="btn  btn-success btn-xs" onclick="funReschedule('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>--%>
                                                            <button type="button" class="lbtnschedule">Post</button>

                                                            <input type="hidden" id="lblcdate" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>" />


                                                        </div>



                                                        <%--<asp:LinkButton ID="lbtnComments" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnComments_Click"    data-toggle="collapse" data-target="#dcomments">Comments</asp:LinkButton>--%>



                                                        <div class="col-md-12 collapse dcomments" id="dcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">

                                                            <textarea name="lblcomments" id="lblcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" style="width: 300px"></textarea>
                                                            <br>
                                                            <input type="text" name="txtcomdate" class="datepicker" id="txtcomdate<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("MM/dd/yyyy") %>" style="width: 300px"></input>

                                                            <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments" onclick="funPost('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>



                                                        </div>

                                                        <%--  <button type="button" class="btn btn-primary btn-xs" runat="server" id="Button1" data-toggle="collapse" data-target="#dcomments" >Comments</button>

                                    <div class="col-md-12 collapse "  id="dcomments">

                                      <input type="text"  name="lblcomments" id="lblcomments" />
                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments"  >Post</button>
                                      


                                    </div>--%>
                                                    </div>
                                                </div>



                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
