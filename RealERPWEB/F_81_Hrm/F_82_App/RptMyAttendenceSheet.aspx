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
                                    <asp:Label ID="lblLate" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Leave Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblLeave" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Absent Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblAbsent" runat="server" CssClass="control-label"> Card</asp:Label>
                                </div>
                            </div>

                            <div class="form-row">
                                <label for="input04" class="col-md-3  mb-0">Total Holiday Day</label>
                                <div class="col-md-9 ">
                                    <asp:Label ID="lblHoliday" runat="server" CssClass="control-label"> Card</asp:Label>
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
                                    <asp:Label ID="lblacintime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblactualin" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblactualout" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>

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
                                    <asp:CheckBox ID="chkvmrno" runat="server" Enabled="False"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateapp"))=="True"||Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp"))=="True" %>'
                                        Width="20px" />

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




        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

