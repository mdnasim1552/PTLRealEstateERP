<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMyAttendenceSheet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.RptMyAttendenceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .headPart {
            font-weight: bold;
            width: 70%;
        }

        .lblStyle {
            /*width: 70%;*/
        }

        .lblStyle2 {
            width: 73px;
            background: #fdfdfd none repeat scroll 0 0;
            border: 1px solid #ccc;
            border-radius: 2px;
            color: #000;
        }

        .lblName2 {
            width: 105px;
        }

        table {
            width: 100%;
        }

        .card-body {
            padding: 10px !important;
        }
    </style>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function openModalAbs() {
            $('#absmodal').modal('toggle');
        }


        function CloseModalAbs() {
            $('#absmodal').modal('hide');
        }

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });

        }




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
            <div class="card card-fluid container-data mb-1 mt-5">
                <div class="card-header" id="mgtCard" runat="server">

                    <div class="row">

                        <div class="col-md-2 pl-0">
                            <!-- .form-group -->
                            <div class="form-group">
                                <label for="sel1" id="frmdate" runat="server">From Date <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtgvenjoydt1" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                            </div>
                            <!-- /.form-group -->
                        </div>

                        <div class="col-md-2 pr-3" id="divBTWDay" runat="server">
                            <div class="form-group">
                                <label for="sel1" id="todate" runat="server">To Date <span class="text-danger">*</span></label>

                                <asp:TextBox ID="txtgvenjoydt2" runat="server" AutoPostBack="true" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group" id="empMgt" runat="server">
                                <label for="Employee">
                                    Employee <span class="text-danger">*</span>
                                </label>
                                <asp:DropDownList ID="ddlEmpName" runat="server"
                                    CssClass="chzn-select form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group" id="Div1" runat="server">
                                <label for="Employee">
                                </label>
                                <asp:LinkButton ID="lnkbtnRefresh" runat="server" CssClass="btn btn-info btn-md" Style="margin-top: 30px;" OnClick="lnkbtnRefresh_Click">
                                    Refresh 
                                </asp:LinkButton>
                            </div>
                        </div>



                    </div>
                </div>

                <div class="card-body">


                    <p><strong>Basic Information</strong></p>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-row">
                                <label for="input04" class="col-md-3 mb-0">Card No:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblcard" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Emp Name:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblname" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Desgnation:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lbldesg" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Confirm Date:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblconfirmdate" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Join Date:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lbljoindate" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row" hidden="hidden">
                                <label for="input04" class="col-md-3  mb-0">Department:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lbldpt" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="form-row" runat="server" id="sysid">
                                <label for="input04" class="col-md-3  mb-0">System ID:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblsysid" runat="server" CssClass="control-label"> </asp:Label>
                                </div>
                            </div>

                             <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Department</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="tbldept" runat="server" CssClass="control-label"> </asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Att. Type</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblattype" runat="server" CssClass="control-label"> </asp:Label>
                                </div>
                            </div>


                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">In Time</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblIntime" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Out Time</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblout" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>



                        </div>

                        <div class="col-md-5">
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Working Day:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblwork" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Late Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblLate" runat="server" CssClass="control-label badge bg-warning text-white"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Leave Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblLeave" runat="server" CssClass="control-label badge bg-green text-white"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Absent Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblAbsent" runat="server" CssClass="control-label badge bg-danger text-white"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Holiday Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblHoliday" runat="server" CssClass="control-label badge bg-info text-white"> Card</asp:Label>
                                </div>
                            </div>
                            <asp:HiddenField ID="empdeptid" runat="server" />

                        </div>
                    </div>

                </div>

            </div>

            <div class="card card-fluid container-data5 ">
                <div class="card-body">
                    <header class="card-header p-0 m-0">Details Information</header>

                    <asp:Repeater ID="RptMyAttenView" runat="server" OnItemDataBound="RptMyAttenView_ItemDataBound">

                        <HeaderTemplate>
                            <table class="table-striped table-hover table-bordered">
                                <tr>
                                    <th style="text-align: center; width: 20px;">SL#</th>
                                    <th style="text-align: center; width: 94px;">Date</th>
                                    <th style="text-align: center; width: 72px;">In Time</th>
                                    <th style="text-align: center; width: 72px;">Out Time</th>
                                    <th style="text-align: center; width: 100px;">Status</th>
                                    <th style="text-align: center; width: 72px;">Penalty</th>
                                    <th style="text-align: center; width: 72px;">Official Hour</th>
                                    <th style="text-align: center; width: 100px;">Remarks</th>
                                    <th style="text-align: center; width: 100px;">Notes</th>
                                    <th style="text-align: center; width: 100px;">Request Status</th>
                                    <th style="text-align: center; width: 50px;">Request Type</th>

                                    <th style="text-align: center; width: 100px;"></th>
                                    <th style="text-align: center; width: 100px;"></th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="Laberowid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).ToString() %>'></asp:Label>

                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblRequid" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rqid")).ToString() %>'></asp:Label>
                                    <asp:Label ID="lblEmpid" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).ToString() %>'></asp:Label>
                                    <asp:Label ID="lblIntime" Visible="false" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    <asp:Label ID="lblOuttime" Visible="false" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    <asp:Label ID="lblacintime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'></asp:Label>

                                </td>
                                <td style="text-align: center">
                                    <%--  <asp:Label ID="lblactualin" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>--%>
                                    <asp:Label ID="lblactualin" runat="server" Text='<%# 
                                   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt")==	"12:00 AM" ? "" : 
                                   Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt")) %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <%--<asp:Label ID="lblactualout" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>--%>
                                    <asp:Label ID="lblactualout" runat="server" Text='<%# 
                                   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt")==	"12:00 AM" ? "" : 
                                   Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt")) %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).ToString() %>'></asp:Label>

                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedtimePenal1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lbldtimehour" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actTimehour")).ToString() %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblisremarks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isremarks")).ToString() %>'></asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblapremarks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apremarks")).ToString() %>'></asp:Label>
                                </td>


                                <td style="text-align: center">

                                    <asp:Label ID="lblreqstatus" runat="server" CssClass="control-label badge bg-green text-white" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqstatus")).ToString() %>'></asp:Label>

                                </td>

                                <td style="text-align: center">
                                    <asp:Label ID="lblreqtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")).ToString() %>'></asp:Label>
                                </td>

                                <td style="text-align: center">
                                    <asp:CheckBox ID="chkvmrno" runat="server" Enabled="False" Visible="false"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateapp"))=="True"||Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp"))=="True" %>'
                                        Width="20px" />
                                    <asp:LinkButton ID="lnkRequstApply" Visible="false" ToolTip="For Approval Request" runat="server" OnClick="lnkRequstApply_Click" CssClass="btn btn-sm btn-primary">Apply Request</asp:LinkButton>

                                </td>
                                <td style="text-align: center">
                                    <asp:HyperLink ID="hyplnkApplyLv" Target="_blank" Visible="false" runat="server" CssClass="btn btn-sm btn-success">Apply Leave</asp:HyperLink>

                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lbltotal" runat="server" Style="font-weight: bold;">Total</asp:Label></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblTotalHour" runat="server" Style="font-weight: bold;">40:00</asp:Label>
                                </td>

                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>

                            </tr>

                            </table>
                        </FooterTemplate>
                    </asp:Repeater>


                </div>

            </div>


            <div id="absmodal" class="modal animated zoomIn" role="dialog">
                <div class="modal-dialog   modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>

                        </div>
                        <div class="modal-body">
                            <div class="card-body m-0 p-0 text-center">
                                <h4 class="card-title badge badge-lg badge-info" id="lblhead" runat="server">Request Form</h4>
                            </div>
                            <div class="card-body">
                                <!-- form .needs-validation -->
                                <asp:HiddenField ID="ReqID" runat="server" />
                                <!-- .form-row -->
                                <div class="form-row">
                                    <!-- form grid -->
                                    <div class="col-md-6 mb-3">
                                        <label for="validationTooltip01">
                                            Request Type
                           
                                            <abbr title="Required">*</abbr>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlReqType" class="form-control" required="">
                                        </asp:DropDownList>

                                    </div>
                                    <!-- /form grid -->
                                    <!-- form grid -->
                                    <div class="col-md-3 mb-3">
                                        <label for="validationTooltip02">
                                            Date 
                                        </label>
                                        <asp:Label ID="lbldadte" runat="server" class="form-control"></asp:Label>

                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="validationTooltip02">
                                            Time  
                                        </label>
                                        <asp:Label ID="lbldadteOuttime" Visible="false" runat="server" class="form-control"></asp:Label>
                                        <asp:Label ID="lbldadteIntime" Visible="false" runat="server" class="form-control"></asp:Label>
                                        <asp:Label ID="lbldadteTime" runat="server" class="form-control"></asp:Label>

                                    </div>
                                    <!-- /form grid -->
                                    <!-- form grid -->
                                    <div class="col-md-12 mb-3">
                                        <label for="validationTooltipUsername">
                                            Remarks/Reason                           
                                            <abbr title="Required">*</abbr>
                                        </label>

                                        <asp:TextBox ID="txtAreaReson" class="form-control" runat="server" ClientIDMode="Static" onkeypress="RestrictSpaceSpecial();" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValidationError" ErrorMessage="*Field Is Required" Display="Dynamic" ControlToValidate="txtAreaReson" ForeColor="Red" ValidationGroup="ResonSubmit"></asp:RequiredFieldValidator>

                                    </div>
                                    <!-- /form grid -->
                                    <br />
                                    <span style="font-size: 14px; color: red" id="InfoApply" runat="server" visible="false"><i class="fa fa-info-circle"></i>If you think this day enjoyed leave please don't apply from here, Go to <a href="../../F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User">Apply Leave</a>  </span>
                                </div>



                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbntnAbsentApproval" OnClick="lbntnAbsentApproval_Click" OnClientClick="CloseModalAbs();"
                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Submit Request</asp:LinkButton>
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

