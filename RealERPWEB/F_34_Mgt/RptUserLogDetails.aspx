<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptUserLogDetails.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.RptUserLogDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
        function loadModal() {
            $('#overtimeinfo').modal('toggle');
        }

        function CloseModal() {
            $('#overtimeinfo').modal('hide');
        }
    </script>


    <style>
        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 29px !important;
            border-radius: 5px !important;
        }

        .colorfont {
            color: black;
        }
        
    </style>
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From</label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" ></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">User Name</label>
                                <asp:DropDownList ID="ddlUserName" runat="server" CssClass="custom-select chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="lblPage" runat="server" visible="false" class="control-label" for="ddlUserName">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">Type</label>
                                <asp:DropDownList ID="ddltype" runat="server" CssClass="custom-select chzn-select">
                                    <asp:ListItem Value="0">---------All---------</asp:ListItem>
                                    <asp:ListItem Value="01">Post Dated Voucher</asp:ListItem>
                                    <asp:ListItem Value="02">Current Voucher</asp:ListItem>
                                    <asp:ListItem Value="03">Money Receipts</asp:ListItem>
                                    <asp:ListItem Value="04">Material Requisition</asp:ListItem>
                                    <asp:ListItem Value="05">Material Received</asp:ListItem>
                                    <asp:ListItem Value="06">Material Issue</asp:ListItem>
                                    <asp:ListItem Value="07">Material Transfer</asp:ListItem>
                                    <asp:ListItem Value="08">Purchase Program</asp:ListItem>
                                    <asp:ListItem Value="09">Purchase Order</asp:ListItem>
                                    <asp:ListItem Value="10">Purchase Bill</asp:ListItem>
                                    <asp:ListItem Value="11">Payment Schedule</asp:ListItem>
                                    <asp:ListItem Value="12">Work Execution</asp:ListItem>
                                    <asp:ListItem Value="13">Contractor Bill Entry</asp:ListItem>
                                    <asp:ListItem Value="14">Contractor Bill Final</asp:ListItem>
                                    <asp:ListItem Value="15">Cheque Deposit</asp:ListItem>
                                    <asp:ListItem Value="16">Bank Reconcilation</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 250px;">
                <!-- .card-header -->
                <div class="card-header">
                    <!-- .nav-tabs -->
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item">
                            <a class="nav-link active " data-toggle="tab" href="#home">Log Details</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#profile">Summary</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#home2">Log Details HRM</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link" data-toggle="dropdown" href="#" role="button" aria-expanded="false">Relevent Report <span class="caret"></span></a>
                            <div class="dropdown-menu" style="">
                                <div class="dropdown-arrow"></div>
                                <a class="dropdown-item" href="F_81_Hrm/F_92_Mgt/RptUserLogDetails.aspx">HR Log Report</a> <a class="dropdown-item" href="#">Another action</a> <a class="dropdown-item" href="#">Something else here</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="F_34_Mgt/RptUserLogStatus.aspx">User Log Information</a>
                            </div>
                        </li>
                    </ul>
                    <!-- /.nav-tabs -->
                </div>
                <!-- /.card-header -->
                <!-- .card-body -->
                <div class="card-body">
                    <!-- .tab-content -->
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade active show" id="home">
                            <div class="col-md-12 table-responsive">
                                <asp:GridView ID="gvLogType" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvLogType_PageIndexChanging" OnRowDataBound="gvLogType_RowDataBound" Height="310px" Width="1210px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <HeaderTemplate>
                                                <div class="pull-left">Number </div>
                                                <div class="pull-right">
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvType" runat="server" Target="_blank" CssClass="colorfont"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "number").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "number")).Trim(): "")  %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryDatA" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdat")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry<br> User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryuser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryuser")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry <br>Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryTime" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedtime")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry IP <br>Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryIP" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postrmid")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/App.<br> User">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lgvEdituser" runat="server" ForeColor="Red" OnClick="lbtngvEditUser_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "edituser")) %>'
                                                    Width="80px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/App.<br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEditDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "editdat")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete<br> User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdeleteuser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deluser")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete<br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdeleteDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deldat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deleted <br>Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDelTime" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deletedtime")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="profile">
                            <asp:GridView ID="gvstatus" runat="server" Width="100" AutoGenerateColumns="false"
                                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLSlNo" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgrentryuser" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryuser")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Task">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgrpdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Count">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCount" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tcount")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>
                        <div class="tab-pane fade" id="home2">
                            <div class="col-md-12 table-responsive">
                                <asp:GridView ID="gvLogType2" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvLogType2_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <HeaderTemplate>
                                                <div class="pull-left">Number </div>
                                                <div class="pull-right">
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcType" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "number").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "number")).Trim(): "")  %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value <br>Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdat")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry<br> User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryuser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryuser")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry <br>Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryTime" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedtime")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry IP <br>Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryIP" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postrmid")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/App.<br> User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEdituser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "edituser")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/App.<br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEditDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "editdat")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete<br> User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdeleteuser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deluser")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete<br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdeleteDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deldat")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deleted <br>Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDelTime" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deletedtime")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <!-- /.tab-content -->
                </div>
                <!-- /.card-body -->
            </div>

            <div id="overtimeinfo" class="modal col-md-8 col-md-offset-2 animated zoomIn" role="dialog">
                <div class="modal-dialog   modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header bg-primary">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="glyphicon glyphicon-hand-right"></span>
                                <asp:Label ID="lbmodalheading" runat="server"></asp:Label>
                            </h4>
                        </div>

                        <div class="modal-body">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="16px">Voucher Number : </asp:Label>

                                <asp:Label ID="LabelVounum" runat="server" Font-Bold="True" Height="16px"></asp:Label>

                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px">Voucher Date : </asp:Label>
                                <asp:Label ID="LabelVoudat" runat="server" Font-Bold="True" Height="16px"></asp:Label>
                            </div>
                            <div class="col-md-8 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Height="16px">Bank/Cash : </asp:Label>

                                <asp:Label ID="LabelControl" runat="server" Font-Bold="True" Height="16px"></asp:Label>
                            </div>
                            <div class="row-fluid form-horizontal forgotform" id="">
                            </div>
                            <div class="row">
                                <asp:GridView ID="mgvbreakdown" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="430px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvStat" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vstatus")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateday1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat1")).ToString("dd-MMM-yyyy HH:mm tt") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat1")).ToString("dd-MMM-yyyy HH:mm tt")!="01-Jan-1900" %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accounts Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActDesc" runat="server"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Substring(13)%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc"))=="")?"": Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Substring(13)+" ["+(DataBinder.Eval(Container.DataItem, "spcldesc"))+"]"%>'
                                                    Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc"))!=""%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Specification">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpecDesc" runat="server" 
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc"))%>'
                                                    Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc"))!="NONE"%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                         </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="Dr Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvDrAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram"))!=0.00%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cr Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvCrAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram"))!=0.00%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="User">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvgrp" runat="server"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <%--<FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />--%>

                                        </asp:TemplateField>








                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>


                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Height="16px" Width="100px">Ref No: </asp:Label>

                            <asp:Label ID="LabelRefNo" runat="server" Font-Bold="True" Height="16px"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Height="16px" Width="100px">Narration: </asp:Label>

                            <asp:Label ID="LabelNar" runat="server" Font-Bold="True" Height="16px"></asp:Label>

                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



