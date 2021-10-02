<%--<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryLandRegProcess.aspx.cs" Inherits="RealERPWEB.F_01_LPA.EntryLandRegProcess" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryLandRegProcess.aspx.cs" Inherits="RealERPWEB.F_01_LPA.EntryLandRegProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <style>
                .nav-item {
                    color: #000 !important;
                }

                    .nav-item .active {
                        background: #ffd800;
                        color: #000 !important;
                    }

                body {
                    font-family: "Century Gothic";
                }


                .grvHeader {
                    font-family: "Century Gothic" !important;
                }
            </style>
            <div class="page-inner">
                <!-- .page-title-bar -->
                <header class="page-title-bar">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item active"></li>
                        </ol>
                    </nav>
                </header>
                <!-- /.page-title-bar -->
                <!-- .page-section -->
                <div class="page-section">
                    <!-- .section-block -->
                    <div class="section-block">
                        <!-- .board -->
                        <div class="board board-list">
                            <!-- .tasks -->
                            <section class="tasks">
                                <!-- .task-header -->
                                <%--<header class="task-header">
                                    
                                    <select class="custom-select d-none" style="width: 160px">
                                        <option value="">Filter project </option>
                                        <option value="1">Looper Admin Theme </option>
                                        <option value="2">Smart Paper </option>
                                        <option value="3">Booking Up </option>
                                        <option value="4">Online Store </option>
                                    </select>
                                </header>--%>
                                <!-- /.task-header -->
                                <!-- .task-issue -->

                                <!-- .card -->
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblfrmdate" class="control-label lblmargin-top9px">Date</label>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled"  Font-Size="9px" 
                                                        CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblfrmdate" class="control-label lblmargin-top9px">Project</label>

                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblfrmdate" class="control-label lblmargin-top9px">Page Size</label>

                                                </div>



                                            </div>
                                            <div class="col-md-1">
                                                <div class="form-group">

                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                        CssClass=" form-control"
                                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>150</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>300</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>



                                            <div class="col-md-3">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                                <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                                    <button type="button" class="btn btn-warning">Reports</button>
                                                    <div class="btn-group" role="group">
                                                        <button id="btnGroupDrop5" type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop5" style="">
                                                            <div class="dropdown-arrow"></div>

                                                            <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/F_01_LPA/RptLandProcurement?Type=LandSt" CssClass="dropdown-item" Style="padding: 0 10px">Land Procurement Status</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_01_LPA/RptLandProcurement?Type=LandStSum" CssClass="dropdown-item" Style="padding: 0 10px">Land Procurement Status-Summary</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/RptPaymetDueAllPrj" CssClass="dropdown-item" Style="padding: 0 10px">Land Owner Payment Dues</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Land Purchase Register</asp:HyperLink>


                                                        </div>
                                                    </div>
                                                </div>
                                                <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                                    <button type="button" class="btn btn-danger">Operation</button>
                                                    <div class="btn-group" role="group">
                                                        <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                            <div class="dropdown-arrow"></div>

                                                            <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Project Information</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_51_LBgd/LandGenCode" CssClass="dropdown-item" Style="padding: 0 10px">Land Development Information Code</asp:HyperLink>

                                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="dropdown-item" Style="padding: 0 10px" OnClick="lbtnAdd_Click">Add New Land</asp:LinkButton>
                                                            <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=res" CssClass="dropdown-item" Style="padding: 0 10px">Resource Code</asp:HyperLink>

                                                            <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Budget-Sales</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink9" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Budget-Engineering</asp:HyperLink>

                                                            <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl="~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain&prjcode=" CssClass="dropdown-item" Style="padding: 0 10px">Budget-General</asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="~/F_99_Allinterface/RptEngInterface" CssClass="dropdown-item" Style="padding: 0 10px">General Bill </asp:HyperLink>
                                                            <asp:HyperLink ID="HyperLink10" runat="server" Target="_blank" NavigateUrl="~/F_15_DPayReg/BillRegInterface?Type=Report" CssClass="dropdown-item" Style="padding: 0 10px">Bill Register</asp:HyperLink>
                                                            <asp:HyperLink ID="hlnk" runat="server" Target="_blank" NavigateUrl="~/F_01_LPA/LandSearch" CssClass="dropdown-item" Style="padding: 0 10px">Search(Khotian)</asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2" id="MultCom" runat="server">
                                                <div class="form-group">
                                                    <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                                        <button type="button" class="btn btn-info">Map</button>
                                                        <div class="btn-group" role="group">
                                                            <button id="btnGroupDrop6" type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop6" style="">
                                                                <div class="dropdown-arrow"></div>

                                                                <asp:HyperLink ID="GoogleMap" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Google Map</asp:HyperLink>
                                                                <asp:HyperLink ID="combinedMap" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">CS and RS Map</asp:HyperLink>
                                                                <asp:HyperLink ID="PurchaseMap" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Purchase Map</asp:HyperLink>

                                                                <asp:HyperLink ID="SalesMap" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 10px">Sales Map</asp:HyperLink>

                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div id="dvTab">
                                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                                <li class="nav-item">

                                                    <a class="nav-link btn active" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="false">Land Purchase</a>
                                                </li>
                                                <li class="nav-item ml-1">
                                                    <a class="nav-link btn" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false">Land Sales</a>
                                                </li>
                                                <li class="nav-item ml-2">
                                                    <a class="nav-link btn" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false">Map</a>
                                                </li>

                                            </ul>
                                            <!-- Tab panes -->
                                            <div class="tab-content p-3">
                                                <div class="tab-pane fade active show" id="tab1" role="tabpanel" aria-labelledby="tab1">

                                                    <div class="table-responsive mt-1">
                                                        <asp:GridView ID="gvRegis" runat="server" AllowPaging="True"
                                                            AutoGenerateColumns="False" OnPageIndexChanging="gvRegis_PageIndexChanging"
                                                            ShowFooter="True" Style="text-align: left" Width="654px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                            OnRowCancelingEdit="gvRegis_RowCancelingEdit" OnRowEditing="gvRegis_RowEditing"
                                                            OnRowUpdating="gvRegis_RowUpdating" OnRowDataBound="gvRegis_RowDataBound">

                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                            Style="text-align: right"
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Land Description">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Text="Total"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="HLgvsircode" runat="server" Font-Underline="false" Target="_blank" ForeColor="Black"
                                                                            Style="text-align: left;" Width="250px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:HyperLink>



                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <FooterStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Doc<br> Upload">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="HLgvsircodeDoc" runat="server" Font-Underline="false" Target="_blank" ForeColor="Black"
                                                                            Style="text-align: center;"
                                                                            Text='Click' Width="80px"></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Unit" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgudesc01" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                            Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted <br> Qty">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFbudgetqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvbudgetqty" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Budgeted <br> Rate">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvbudgetrate" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Budgeted  <br> Amount">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFbudgetamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvbudgetamount" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Purchase <br> Qty">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFactualqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvactualqty" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                            Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />

                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Purchase <br> Rate">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvactualrate" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrat")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Payable <br> Amount">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFactualamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvactualamount" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Other's Cost" Visible="false">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvOtherCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvOtherCost" runat="server"
                                                                            Text='0.00'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Payment <br> Status">
                                                                    <FooterTemplate>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="HyLpaystusamount" runat="server" Target="_blank"
                                                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paystus"))=="0.000000"?false:true %>'
                                                                            Width="70px">
                                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paystus")).ToString("#,##0.00;(#,##0.00); ")%> %




                                                                        </asp:HyperLink>




                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>




                                                                <asp:CommandField ShowEditButton="True" CancelText="Can" UpdateText="Up" />

                                                                <asp:TemplateField HeaderText="Process">
                                                                    <EditItemTemplate>
                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                            <div class="form-group">
                                                                                <asp:DropDownList ID="ddlpro" runat="server" Width="180px" CssClass="custom-select ">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRegirtd" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                            Width="170px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Broker" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                                            BorderWidth="1px">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td class="style58">
                                                                                        <asp:TextBox ID="txtSerachbro" runat="server" BorderStyle="Solid"
                                                                                            BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="style59">
                                                                                        <asp:ImageButton ID="ibtnSrchbro" runat="server" Height="16px"
                                                                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchbro_Click" TabIndex="5"
                                                                                            Width="16px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlbro" runat="server" Width="120px" TabIndex="6">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRegirtdx" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brodesc")) %>'
                                                                            Width="170px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>




                                                                <asp:TemplateField HeaderText="ProCode" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvprocode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="BroCode" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvbrocode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brocode")) %>'
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>


                                                            </Columns>


                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                            <RowStyle CssClass="grvRows" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="tab2">
                                                    <div class="table-responsive" style="min-height:500px;">
                                                        <asp:GridView ID="gvBgdSales" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                            OnPageIndexChanging="gvBgdSales_PageIndexChanging"
                                                            OnRowDataBound="gvBgdSales_RowDataBound">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                                            Style="text-align: right"
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Plot Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcFlDesc" runat="server" Width="250px"
                                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "udesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")).Trim(): "")   %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcUnit" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                                            Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Plot </br>Size">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvUSize" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvURate" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvUAmt" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Parking" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvOChr" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Utility">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvutility" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Cooperative">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvcooperative" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Total </br>Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvTAmt" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Plot </br>Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvUQty" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uqty")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Min Booking </br>Money">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvminbam" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvstatis" runat="server" Style="color: red;"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sustatus")) %>'
                                                                            Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Attached </br>Documents">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="hypliSalesDocs" runat="server" Font-Underline="false" Target="_blank" ForeColor="Black"
                                                                            Style="text-align: center;"
                                                                            Text='Click Me' Width="80px">Click</asp:HyperLink>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
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

                                                <div class="tab-pane fade" id="tab3" role="tabpanel" aria-labelledby="tab3">
                                                </div>


                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <!-- .card -->


                            </section>
                            <!-- /.tasks -->
                        </div>
                        <!-- /.board -->
                    </div>
                    <!-- /.section-block -->
                </div>
                <!-- /.page-section -->
            </div>


            <asp:HiddenField ID="HiddenTab" runat="server" />

            <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Land  </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">
                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Resource Code </label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtresourcecode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Description</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtresourcehead" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                


                                <div class="form-group">
                                    <label class="col-md-4">Unit </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>




                                    <div class="col-md-3" style="display:none;">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" Checked="true" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4">Land Size </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Rate </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                



                                


                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }
        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');


        }

        function Tabs() {

            var Tab = $("#<%=HiddenTab.ClientID%>");
            var tabId = Tab.val() != "" ? Tab.val() : "tab1";

            $('#dvTab a[href="#' + tabId + '"]').tab('show');
            $("#dvTab a").click(function () {
                Tab.val($(this).attr("href").substring(1));
                //replace("#", ""));
                //substring(1));
            });
        }

        function pageLoad() {
            Tabs();
        }
        function showContent() {

            Tabs();

        };
    </script>
</asp:Content>





