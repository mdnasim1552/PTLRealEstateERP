<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="userprivileges.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.userprivileges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <style>
        .clm0 {
            font-size: 16px !important;
            color: #a93c1c;
            font-weight: bold;
        }

        .cl0 {
            font-size: 15px !important;
            color: #f6350c;
            font-weight: bold;
            padding-left: 5px;
        }

        .cl1 {
            font-size: 14px !important;
            color: #ff7900;
            font-weight: bold;
            padding-left: 15px;
        }

        .cl2 {
            font-size: 13px !important;
            color: #006fa2;
            padding-left: 25px;
            font-weight: bold;
        }

        .cl3 {
            font-size: 12px !important;
            color: #000;
            font-weight: bold;
            padding-left: 35px;
        }
    </style>

    <div class="page-inner">
        <!-- .page-title-bar -->
        <header class="page-title-bar">
            <!-- .breadcrumb -->
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item active">
                        <a href="../F_34_Mgt/UserLoginfrmNew">
                            <i class="breadcrumb-icon fa fa-angle-left mr-2"></i>User List</a>
                    </li>
                </ol>
            </nav>
            <!-- /.breadcrumb -->
            <!-- floating action -->
            <button type="button" class="btn btn-success btn-floated">
                <span class="fa fa-plus"></span>
            </button>
            <!-- /floating action -->
            <!-- title and toolbar -->
            <div class="d-md-flex align-items-md-start">
                <h1 class="page-title mr-sm-auto">User Privileges </h1>
                <!-- .btn-toolbar -->

                <!-- /.btn-toolbar -->
            </div>
            <!-- /title and toolbar -->
        </header>
        <!-- /.page-title-bar -->
        <!-- .page-section -->
        <div class="page-section">
            <!-- .card -->
            <section class="card card-fluid pb-6">
                <!-- .card-header -->
                <header class="card-header">
                    <!-- .nav-tabs -->

                    <ul class="nav nav-tabs card-header-tabs">

                        <li class="nav-item show active">
                            <a class="nav-link" data-toggle="tab" href="#tab1">Menu</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab2">Project</a>
                        </li>
                        <li class="nav-item ">
                            <a class="nav-link" data-toggle="tab" href="#tab3">Cash Bank</a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab4">Chart of Accounts</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab5">Work Code</a>
                        </li>

                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab5">HR Department</a>
                        </li>

                    </ul>
                    <!-- /.nav-tabs -->
                </header>

                <div class="card-body">
                    <div id="myTabContent" class="tab-content">



                        <div class="tab-pane fade show active" id="tab1">
                            <div class="row">
                                <div class="card-body">


                                    <div class="row">


                                        <div class="col-md-2">
                                            <asp:Label ID="Label2" runat="server" CssClass="mr-2" Text=""></asp:Label>
                                            <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True" Visible="false"
                                                Font-Bold="True" Checked="true"
                                                OnCheckedChanged="chkShowall_CheckedChanged" Text=" All Items" CssClass="checkBox btn btn-warning" />

                                            


                                            <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="btn btn-secondary fileinput-button xchzn-select d-none">
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn btn-primary fileinput-button d-none ">Show</asp:LinkButton>

                                        </div>
                                        <div class="col-md-2">
                                             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">All </asp:ListItem>
                                                   
                                                     <asp:ListItem Value="2">Component</asp:ListItem>
                                                    <asp:ListItem Value="4">Widget</asp:ListItem>
                                                    <asp:ListItem Value="3">Graph </asp:ListItem>
                                                    <asp:ListItem Value="9">Menu </asp:ListItem>
                                                    

                                                </asp:DropDownList>
                                        </div>

                                        <div class="col-md-1 text-right">
                                            <button class="btn btn-primary">Page Size</button>
                                        </div>
                                        <div class="col-md-1">

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" form-control smDropDown"
                                                Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                                <asp:ListItem Value="800">800</asp:ListItem>
                                                <asp:ListItem Value="1200">1200</asp:ListItem>
                                                <asp:ListItem Value="1600">1600</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>


                                    </div>



                                    <asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea mt-5"
                                        OnPageIndexChanging="gvPermission_PageIndexChanging" OnRowDataBound="gvPermission_RowDataBound"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Form id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="menutype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "menutype")) %>'></asp:Label>
                                                    <asp:Label ID="lgvufrmid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="menuparentid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvufrmidmenuparentid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "menuparentid2")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Form Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvufrmnamex" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                        Width="120px"></asp:Label>
                                                    <asp:Label ID="lgvsidebar" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sidebar")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Query Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvQrytype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" CssClass="clm0" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "step")) %>'></asp:Label>
                                                    <asp:Label ID="lgvufrmname" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPer_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderTemplate>
                                                    Description
                                                     <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                         OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                </HeaderTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Permission">
                                                <HeaderTemplate>

                                                    <asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label>
                                                    <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkallView_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPermit" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                        Width="50px" />


                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry or Edit " Visible="false">
                                                <HeaderTemplate>

                                                    <asp:Label ID="Label4" runat="server" Text="Entry or Edit"></asp:Label>
                                                    <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkallEntry_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEntry" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>'
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View & Print" Visible="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text="View & Print"></asp:Label>

                                                    <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkallPrint_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPrint" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Delete" Visible="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblgvdelete" runat="server" Text="Delete"></asp:Label>


                                                    <asp:CheckBox ID="chkallDelete" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkallDelete_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delete"))=="True" %>'
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Check All" Visible="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkall_CheckedChanged" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                    </asp:GridView>
                                    <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>





                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab2">
                            <div class="row">
                                <div class="col-md-12">
                                    Project
                                    
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab3">
                            <div class="row">
                                <div class="col-md-12">
                                    Cash bank 
                                    
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab4">
                            <div class="row">
                                <div class="col-md-12">
                                    Chart of Accountrs 
                                    
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab5">
                            <div class="row">
                                <div class="col-md-12">
                                    work Code
                                    
                                </div>

                            </div>
                        </div>

                    </div>

                </div>

            </section>
            <!-- /.card -->
        </div>
        <!-- /.page-section -->
    </div>
</asp:Content>

