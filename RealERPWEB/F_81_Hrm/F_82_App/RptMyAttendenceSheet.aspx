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
                <div class="card-body">

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
                                <label for="input04" class="col-md-3  mb-0">Department:</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lbldpt" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-3">
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





                        </div>
                    </div>

                </div>

            </div>

            <div class="card card-fluid container-data5 ">
                <div class="card-body">
                    <header class="card-header p-0 m-0">Details Information</header>

                    <asp:Repeater ID="RptMyAttenView" runat="server" OnItemDataBound="RptMyAttenView_ItemDataBound">

                        <HeaderTemplate>
                            <table class="table-striped table-hover table-bordered" style="width: 40%;">
                                <tr>
                                    <th>Date</th>
                                    <th>In Time</th>
                                    <th>Out Time</th>
                                    <th>Status</th>
                                    <th>Penalty</th>
                                    <th>Official Hour</th>
                                    <th>Approval Status</th>

                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <asp:Label ID="lblIntime" Visible="false" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    <asp:Label ID="lblOuttime" Visible="false" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy hh:mm:ss tt") %>'></asp:Label>
                                    <asp:Label ID="lblacintime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                  
                                </td>
                                <td>
                                    <%--  <asp:Label ID="lblactualin" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>--%>
                                    <asp:Label ID="lblactualin" runat="server" Text='<%# 
                                   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt")==	"12:00 AM" ? "" : 
                                   Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt")) %>'></asp:Label>
                                </td>
                                <td>
                                    <%--<asp:Label ID="lblactualout" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>--%>
                                    <asp:Label ID="lblactualout" runat="server" Text='<%# 
                                   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt")==	"12:00 AM" ? "" : 
                                   Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt")) %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).ToString() %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedtimePenal1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lbldtimehour" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actTimehour")).ToString() %>'></asp:Label>

                                </td>

                                <td>
                                    <asp:CheckBox ID="chkvmrno" runat="server" Enabled="False" Visible="false"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateapp"))=="True"||Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp"))=="True" %>'
                                        Width="20px" />
                                    <asp:LinkButton ID="lnkRequstApply" Visible="false" ToolTip="For Approval Request" runat="server" OnClick="lnkRequstApply_Click" CssClass="btn btn-sm btn-primary">Apply Request</asp:LinkButton>
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
                                <td>
                                    <asp:Label ID="lblTotalHour" runat="server" Style="font-weight: bold;">40:00</asp:Label>
                                </td>
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

                                <!-- .form-row -->
                                <div class="form-row">
                                    <!-- form grid -->
                                    <div class="col-md-6 mb-3">
                                        <label for="validationTooltip01">
                                            Request Type
                           
                                            <abbr title="Required">*</abbr>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlReqType" class="custom-select d-block w-100" required="">                                            
                                            <asp:ListItem Value="LP">Late Present Approval Request</asp:ListItem>
                                            <asp:ListItem Value="TC">Time Correction Approval Request</asp:ListItem>
                                            <asp:ListItem Value="AB">Absent Approval Request</asp:ListItem>
                                            <asp:ListItem Value="LA">Late Approval Request</asp:ListItem>
                                            
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

                                        <asp:TextBox ID="txtAreaReson" class="form-control" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </div>
                                    <!-- /form grid -->
                                </div>



                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lbntnAbsentApproval" OnClientClick="CloseModalAbs();" OnClick="lbntnAbsentApproval_Click"
                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Submit Request</asp:LinkButton>
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>






                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

