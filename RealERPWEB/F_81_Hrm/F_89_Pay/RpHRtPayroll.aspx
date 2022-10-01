<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RpHRtPayroll.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RpHRtPayroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .margin5px {
        }
        /*
        .dropdown-toggle:after {
            display: inline-block;
            margin-left: .255em;
            vertical-align: .255em;
            content: "";
            border-top: .3em solid;
            border-right: .3em solid transparent;
            border-bottom: 0;
            border-left: .3em solid transparent;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            try {

                var gridViewScroll = new GridViewScroll({
                    elementID: "gvpayroll",
                    width: 1400,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvBonus",
                //    width: 1000,
                //    height: 500,
                //    freezeColumn: true,
                //    freezeFooter: true,
                //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //    freezeHeaderRowCount: 1,
                //    freezeColumnCount: 8,

                //});
                gridViewScroll.enhance();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e)
            {
                alert(e);
            }
        }




        function OpenDedModal() {
            $('#DeductinModal').modal('toggle');
        };

        function CloseDedModal() {
            $('#DeductinModal').modal('hide');
        }



    </script>


    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 58px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label8" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="divBracnhLsit" runat="server">
                            <asp:Label ID="Label9" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label10" runat="server">Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                            </cc1:ListSearchExtender>
                            <asp:Label ID="lblComBonLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                            <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="Label11" runat="server">Section</asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6" id="divEMplist" runat="server" visible="false">
                            <asp:Label ID="lblemp" runat="server">Employee List                                  
                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" OnClick="ibtnEmpListAllinfo_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                            </asp:Label>
                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>





                    </div>
                    <div class="row mt-1">
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control" AutoComplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label14" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6" id="SeachDivForGrid" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server">Search </asp:Label>
                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True"
                                OnTextChanged="txtSearch_TextChanged" CssClass="form-control" placeholder="Search here..."></asp:TextBox>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-6" id="rbtnPayTypeDiv" runat="server" visible="false">
                            <asp:RadioButtonList ID="rbtnPayType" RepeatDirection="Horizontal" CssClass="rbtnList1" Visible="false" runat="server">
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Bank</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem Selected="True">All</asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2" id="gndDiv" visible="false" runat="server">
                            <asp:Label ID="Label15" CssClass="d-block" runat="server">Print Grand Total</asp:Label>
                            <asp:CheckBox ID="chkgrndt" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2" id="lblBanglaDiv" runat="server" visible="false">
                            <asp:Label ID="lblBangla" runat="server" Visible="false">Bangla Print</asp:Label>
                            <asp:CheckBox ID="chkBangla" runat="server" Visible="false" />
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left mt-4" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>

                        <panel id="pnlsalops" runat="server" visible="true" class="col-lg-2 col-md-2 col-sm-6 float-right">
                            <div class="btn-group mt-4">
                                <button type="button" class="btn btn-success">MoreLink</button>
                                <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown">
                                </button>
                                <ul class="dropdown-menu" role="menu">
                                    <li>
                                        <asp:HyperLink ID="hylnkarrear" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=arrear"> Arrear Salary</asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink ID="hylnkloan" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_85_Lon/EmpDeducOther">Loan Installment</asp:HyperLink></li>
                                    </li>
                                              <li>
                                                  <asp:HyperLink ID="hylnkOtherdeduction" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction">Other Deduction </asp:HyperLink>
                                              </li>
                                    <li>
                                        <asp:HyperLink ID="hylnkOtherearn" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn">Other Earning </asp:HyperLink>
                                    </li>
                                    <li>
                                        <asp:HyperLink ID="hylnkOvetime" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=Overtime">Employee Overtime </asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink ID="hylnkAbscount" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_83_Att/HREmpAbsCt">Absent Count </asp:HyperLink>

                                    </li>

                                    <li>
                                        <asp:HyperLink ID="hylnkempTrans" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_87_Tra/HREmpTransfer">Employee Transfer </asp:HyperLink>

                                    </li>


                                    <li>
                                        <asp:HyperLink ID="hylnkSalReduction" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=SalaryReduction">Salary Reduction </asp:HyperLink>

                                    </li>

                                    <li>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EntryDeduction02.aspx">Deduction </asp:HyperLink>

                                    </li>
                                </ul>
                            </div>
                        </panel>

                    </div>

                </div>

                <div class="card-body pt-0">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Salary" runat="server">
                            <div class="row">
                                <asp:Panel ID="Panel6" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:RadioButtonList ID="rbtSalSheet" runat="server" CssClass="rbtnList1 margin5px"
                                                Font-Size="14px" Height="16px" RepeatColumns="10" RepeatDirection="Horizontal"
                                                Width="1728px">
                                                <asp:ListItem>NDE</asp:ListItem>
                                                <asp:ListItem>Foster</asp:ListItem>
                                                <asp:ListItem>Sanmar</asp:ListItem>
                                                <asp:ListItem>Multiplan</asp:ListItem>
                                                <asp:ListItem>Rupayan</asp:ListItem>
                                                <asp:ListItem>Asian TV</asp:ListItem>
                                                <asp:ListItem>GLG</asp:ListItem>
                                                <asp:ListItem>Assure</asp:ListItem>
                                                <asp:ListItem>Leisure</asp:ListItem>
                                                <asp:ListItem>Bridge</asp:ListItem>
                                                <asp:ListItem>InnStar</asp:ListItem>
                                                <asp:ListItem>Alliance</asp:ListItem>
                                                <asp:ListItem>ACME</asp:ListItem>
                                                <asp:ListItem>Suvastu</asp:ListItem>
                                                <asp:ListItem>Tropical</asp:ListItem>
                                                <asp:ListItem>Terranova</asp:ListItem>
                                                <asp:ListItem>PEB</asp:ListItem>
                                                <asp:ListItem>GreenWood</asp:ListItem>
                                                <asp:ListItem>Manama</asp:ListItem>
                                                <asp:ListItem>ERL</asp:ListItem>
                                                <asp:ListItem>Entrust</asp:ListItem>
                                                <asp:ListItem>BTI</asp:ListItem>
                                                <asp:ListItem>JBS</asp:ListItem>
                                                <asp:ListItem>Lanco</asp:ListItem>
                                                <asp:ListItem>Finlay</asp:ListItem>
                                                <asp:ListItem>Epic</asp:ListItem>
                                                <asp:ListItem>Acme AI</asp:ListItem>


                                            </asp:RadioButtonList>


                                            <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass="rbtnList1 margin5px"
                                                Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal"
                                                Width="520px" Visible="false">
                                            </asp:RadioButtonList>

                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">

                     <%--           <div class="table-responsive">--%>
                                    <asp:GridView ID="gvpayroll" runat="server" ClientIDMode="Static" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True">

                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgProName" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID #">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgIdCard" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSection" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:CheckBox ID="chkSalaryLock" runat="server"  CssClass="btn btn-primary btn-sm checkBox" Text="Lock"  />
                                                  
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvndesig" runat="server" Width="150px" CssClass="WrpTxt"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvndesigs" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDateJoin" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Working Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvwday" runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wd")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Office <br> Duty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoffdu" runat="server" Style="text-align:center" Width="45px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netwday")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Leave Enjoyed">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvwlday" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wld")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Absday <br> (Oth.)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lFgvoffdu" runat="server" Style="text-align:center" Width="45px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tabday")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjust">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdedday" runat="server"
                                                         Width="45px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Basic">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBasic" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbSal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="House Rent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvhrent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hrent")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFhrent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Convence">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCon" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cven")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCon" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Medical">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMedical" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mallow")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFmallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvarsal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arsal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFarier" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoth" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oth")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoth" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allowance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtallow" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tallow")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGsal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFgssal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary per Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSPday" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salpday")).ToString("#,##0.0000;(#,##0.0000); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Absent Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbsent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absded")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFabsent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Income Tax">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvitax" runat="server" Style="text-align: right" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                                        BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFitax" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pro. Fund">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpdent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpfund" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gratuity Loan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGratloan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "genloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFGratloan" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Car Loan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcarloan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCarlon" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adv. <br> /Others">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAdvance" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adv")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFadv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transport">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrans" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "transded")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTransp" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobile" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillded")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFMobile" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other <br> ded.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothded" runat="server" Style="text-align: right"
                                                        Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded"))+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fine"))).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total <br> Deduction">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="gvlnkbtnTotalDed" runat="server" Style="text-align: right" OnClick="gvlnkbtnTotalDed_Click" ToolTip="Deduction Details" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdeduc")).ToString("#,##0;(#,##0); ") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%-- Only BTI --%>
                                            <asp:TemplateField HeaderText="Payble">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpayable" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payables")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpayable" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%-- for Others Company --%>
                                            <asp:TemplateField HeaderText="Salary  <br> Payble" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgspay" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFgspay" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Foods &  <br> Others">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFoodsoth" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "foodal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFFoods" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Arrea &  <br> Others">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAreaOth" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othearn")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAreasOth" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Car  <br> Allowance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCarAllown" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "haircutal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCarallo" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Net Payable  <br> Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetpaysal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspay1")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFNetPaySal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Earn <br> Leave">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEranLeav" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "elencash")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFEranLeav" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net <br> Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnetsal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnetSal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank <br> Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbankAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bank2 <br> Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankAmt2" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbankAmt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash <br> Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCash" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCashAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvrmrks" runat="server"
                                                        Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                        BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Cash Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvrmrks2" runat="server"
                                                        Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks2")) %>'
                                                        BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <%--  <asp:TemplateField HeaderText="SCB Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank1" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank1")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DBL Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank2" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank2")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="OBL Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank3" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank3")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="OBL CTG CASH">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank4" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank4")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="DBL/Sicol">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank5" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank5")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="DBL/GPL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbank6" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bank6")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFbank6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                          <%--  </div>--%>



                        </asp:View>
                        <asp:View ID="FestivalBonus" runat="server">
                            <div class="row">
                                <panel id="Pnlmantype" runet="server">
                                    <div class="form-group">

                                        <asp:RadioButtonList ID="rbtnMantype" RepeatDirection="Horizontal" CssClass="rbtnList1" Visible="false" runat="server">
                                            <asp:ListItem>Management</asp:ListItem>
                                            <asp:ListItem> Acting Management</asp:ListItem>
                                            <asp:ListItem>General Employee</asp:ListItem>
                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                        </asp:RadioButtonList>


                                    </div>
                                </panel>
                            </div>
                            <div class="row">
                                <div class="col-lg-1 col-md-1 col-sm-6">
                                    <asp:Label ID="Label1" runat="server">Bonus </asp:Label>
                                    <asp:CheckBox ID="chkBonus" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnCheckedChanged="chkBonus_CheckedChanged" />
                                </div>
                                <asp:Panel ID="PnlBonus" runat="server" CssClass="col-lg-1 col-md-1 col-sm-6" Visible="False">
                                    <asp:Label ID="Label3" runat="server">Bonus(%)
                                                    <asp:LinkButton ID="lnkbtnGenBonus"  runat="server" 
                                                        CssClass="badge bg-danger text-white" OnClick="lnkbtnGenBonus_Click">Calculate</asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtBonusPer" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                </asp:Panel>

                                <asp:Panel ID="Panel5" runat="server" CssClass="col-lg-8 col-md-8 col-sm-12">

                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-12">
                                            <asp:Label ID="lblfrmdate0" runat="server">After Days</asp:Label>
                                            <asp:TextBox ID="txtafterdays" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6">
                                            <asp:Label ID="Label12" runat="server">EID UL ADHA </asp:Label>
                                            <asp:CheckBox ID="chkBonustype" runat="server" CssClass=" form-control" />
                                        </div>

                                        <div class="col-lg-2 col-md-2 col-sm-6">
                                            <asp:Label ID="Label16" runat="server">Signature Sheet</asp:Label>
                                            <asp:CheckBox ID="chkSignatureSheet" runat="server" CssClass="form-control" />
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-12 d-none">
                                            <asp:Label ID="Label7" runat="server">Company </asp:Label>
                                            <asp:RadioButtonList ID="rbtlBonSheet" runat="server" BackColor="#DBEBD4" CssClass="rbtnList1"
                                                RepeatColumns="8" RepeatDirection="Horizontal">
                                                <asp:ListItem>NDE</asp:ListItem>
                                                <asp:ListItem>Foster</asp:ListItem>
                                                <asp:ListItem>Sanmar</asp:ListItem>
                                                <asp:ListItem>Multiplan</asp:ListItem>
                                                <asp:ListItem>Leisure</asp:ListItem>
                                                <asp:ListItem>Assure</asp:ListItem>
                                                <asp:ListItem>Bridge</asp:ListItem>
                                                <asp:ListItem>Alliance</asp:ListItem>
                                                <asp:ListItem>ACME</asp:ListItem>
                                                <asp:ListItem>Suvastu</asp:ListItem>
                                                <asp:ListItem>Tropical</asp:ListItem>
                                                <asp:ListItem>PEB</asp:ListItem>
                                                <asp:ListItem>GreenWood</asp:ListItem>
                                                <asp:ListItem>BTI</asp:ListItem>
                                                <asp:ListItem>Edison</asp:ListItem>
                                                <asp:ListItem>Finlay</asp:ListItem>
                                                <asp:ListItem>Lanco</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>


                                    </div>


                                </asp:Panel>


                            </div>

                            <div class="row mt-2">
                              <div class="table-responsive">
                                <asp:GridView ID="gvBonus" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvBonus_PageIndexChanging"
                                    ShowFooter="True" OnRowDeleting="gvBonus_RowDeleting">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" />


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProNamebon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Section">

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalBonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn btn-primary btn-sm" OnClick="lbtnTotalBonus_Click">Total</asp:LinkButton>
                                            </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgvSectionbon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Id #">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvidcardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Employee Name">
                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                                                Text="Employee Name" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-sm btn-success" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvname" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkbonLock" runat="server" CssClass="btn btn-sm btn-primary checkBox" Text="Lock" Width="90px" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesig" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkUpBonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn btn-sm btn-success primarygrdBtn" OnClick="lnkUpBonus_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBasicb" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbSalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGsalb" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgssalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgJoinDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration(Month)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDuration" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus(%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgPerBonus" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0.000000;(#,##0.000000);") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus Amt.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBonusAmt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBonusAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Special Bonus Amt.">
                                            <ItemTemplate>
                                                <asp:label ID="txtgvSpbonusamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spbonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSpBonusAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:label ID="txtgvtotalamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtbamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Bank Amt.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBankAmtbon" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbankAmtbon" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bank Amt(2).">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBankAmt2bon" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbankAmt2bon" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cash Amt">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcashAmtbon" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcashAmtbon" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBonusRemarks" runat="server" Style="text-align: left" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
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
                        </asp:View>


                        <asp:View ID="Payslip" runat="server">


                            <div class="col-lg-12 well text-center">
                                <img class="rounded mr-2" src='<%=this.ResolveUrl("~/image/human_avatar.png")%>' alt="" width="100">

                                <h5 id="H1" runat="server">Employee Infromation</h5>

                                <h4 id="eMpname" runat="server"></h4>
                                <h5 id="eMpDPt" runat="server"></h5>

                            </div>

                        </asp:View>

                        <asp:View ID="EmpSignature" runat="server">
                        </asp:View>


                        <asp:View ID="CashPay" runat="server">
                            <asp:GridView ID="gvcashpay" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvcashpay_PageIndexChanging"
                                ShowFooter="True">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lgProName0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID CARD">

                                        <ItemTemplate>
                                            <asp:Label ID="lgIdCard0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSection0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name &amp; Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvndesig0" runat="server"
                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Working <br> Day">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvwday0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wd")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Cash Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvothded0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded")).ToString("#,##0;(#,##0); ") %>'
                                                Width="45px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFToCahamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ViewOvertime" runat="server">
                            <asp:GridView ID="gvOvertime" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvOvertime_PageIndexChanging"
                                ShowFooter="True" Width="420px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcompanyandemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))    %>'
                                                Width="220px">   </asp:Label>


                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignation" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeptname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Salary Per Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalpermonth" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Salary Per Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalperhour" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salphour")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Day's">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtodays" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "today")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total hour">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtohour" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Dated">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdated" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ovrday"))%>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payable Amount">

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFoallows" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvoallow" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oallow")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>





                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="viewspecialbonus" runat="server">
                            <div class="row">
                                <div class="col-lg-1 col-md-1 col-sm-6">
                                    <asp:Label ID="Label17" runat="server">Bonus </asp:Label>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-control" AutoPostBack="True"
                                        OnCheckedChanged="chkBonus_CheckedChanged" />
                                </div>
                            </div>

                            <asp:Panel ID="Panelspbonus" CssClass="row mt-2" runat="server">
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <asp:Label ID="Label18" runat="server">Bonus(%) 
                                        <asp:LinkButton ID="lnkbtnGenspBonus" runat="server" OnClick="lnkbtnGenspBonus_Click"><i class="fa fa-random"> </i></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtspecialbonus" runat="server" BorderColor="Red" CssClass="form-control" placeholder="Type Bonus Percentage %"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <asp:Label ID="lblfrmd" runat="server">Form </asp:Label>
                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-6">
                                    <asp:Label ID="Label5" runat="server">To </asp:Label>
                                    <asp:DropDownList ID="ddlToDesig" runat="server" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>

                            <div class="row mt-2">
                                <div class="table-responsive">
 
                                    <asp:GridView ID="gvsbonus" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvsbonus_PageIndexChanging"
                                    ShowFooter="True" Width="766px" OnRowDeleting="gvsbonus_RowDeleting">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNosbonus" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" />


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProNamesbonus" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Section">

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalsbonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn btn-primary btn-sm" OnClick="lbtnTotalsbonus_Click">Total</asp:LinkButton>
                                            </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgvSectionsbonus" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <HeaderTemplate>
                                                 <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                                                Text="Employee Name" Width="120px"></asp:Label>

                                                            <asp:HyperLink ID="hlbtntbCdataExelSP" runat="server" CssClass="btn btn-sm btn-success" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvnamesbonus" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesigsbonus" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkUpSPBonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn  btn-success btn-sm" OnClick="lnkUpSPBonus_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBasicbsbonus" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbSalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGsalbsbonus" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgssalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgJoinDatesbonus" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration(Month)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDurationsbonus" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus(%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgPersbonus" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus Amt.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsbonusAmt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsbonusAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsbonusRemarks" runat="server" Style="text-align: left" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
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
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>


            <%-- Deduction Modal --%>
            <div id="DeductinModal" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                             <h4 class="modal-title">Deduction Details</h4>
                            <button type="button" class="close btn btn-xs" data-dismiss="modal" title="Close"><i class="fa fa-times-circle" aria-hidden="true"></i></button>
                           
                        </div>
                        <div class="modal-body form-horizontal" style="min-height: 200px;">
                            <div class="row-fluid">
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDedDetails" runat="server" AllowPaging="false" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True">
                                            <PagerSettings Position="Top" Mode="NumericFirstLast" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNom1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Absent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmAbsent" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absded")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                              <%--  <asp:TemplateField HeaderText="Leave Deduction">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvleaveded" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lwided")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>

                                          
                                                <asp:TemplateField HeaderText="Income Tax">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmitax" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="45px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="W.F Fund">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmpfund" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gratuity Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmgratloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "genloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Car Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmcarloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Personal Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmperloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Moto Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmmotoloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "motolon")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dress Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmdressloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dresslon")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile Set Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmmsetloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msetloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Msc. Loan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmmscloan" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mscloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Advance <br> Others">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmadvance" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adv")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Transport">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmtrans" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "transded")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Mobile <br> Ded. ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmmbillded" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillded")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Other ded.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmothded" runat="server" Style="text-align: right"
                                                            Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded"))+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fine"))).ToString("#,##0;(#,##0); ") %>'
                                                            Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Deduction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmtotded" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdeduc")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row" style="text-align: center; margin-top: 10px;">
                                    <h4><span style="font-weight: bold;">Total Deduction : </span>
                                        <asp:Label ID="lblmTotDed" runat="server" Font-Bold="true" ForeColor="#ff9900"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
