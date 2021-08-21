
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMyAttendenceSheet.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.RptMyAttendenceSheet" %>

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
        .lblStyle2{
            width:73px;
            background: #fdfdfd none repeat scroll 0 0;
    border: 1px solid #ccc;
    border-radius: 2px;
    color: #000;
        }
        .lblName2 {
            width: 105px;
        }
    </style>

    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <%-- <ContentTemplate>--%>
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">

            <asp:Panel ID="Panel1" runat="server">

                <div>
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">

                            <div class="row">
                                <div class="col-md-4">
                                    <fieldset>
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">A. EMPLOYEE INFORMATION</asp:Label>

                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="form-group">
                                        <div class="row">
                                            <asp:Label ID="Label11" runat="server" CssClass=" lblTxt lblName"> Card No</asp:Label>
                                            <asp:Label ID="lblcard" runat="server" CssClass=" smLbl_to lblStyle"> Card</asp:Label>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="Label5" runat="server" CssClass=" lblTxt lblName"> Emp Name</asp:Label>
                                            <asp:Label ID="lblname" runat="server" CssClass=" smLbl_to lblStyle"> Name</asp:Label>
                                        </div>

                                        <div class="row">
                                            <asp:Label ID="Label6" runat="server" CssClass=" lblTxt lblName">Desgnation</asp:Label>
                                            <asp:Label ID="lbldesg" runat="server" CssClass=" smLbl_to lblStyle"> Desgnation name</asp:Label>
                                        </div>
                                        <div class="row">
                                            <asp:Label ID="Label8" runat="server" CssClass=" lblTxt lblName">Department</asp:Label>
                                            <asp:Label ID="lbldpt" runat="server" CssClass=" smLbl_to lblStyle"> Department name</asp:Label>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <fieldset>
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">B.  OFFICE TIME</asp:Label>

                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label12" runat="server" CssClass=" lblTxt lblName lblName2"> In Time</asp:Label>
                                                <asp:Label ID="lblIntime" runat="server" CssClass=" smLbl_to lblStyle2"> Date</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblDate" runat="server" CssClass=" lblTxt lblName lblName2"> Out Time</asp:Label>
                                                <asp:Label ID="lblout" runat="server" CssClass=" smLbl_to lblStyle2"> Date</asp:Label>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label13" runat="server" CssClass=" lblTxt lblName lblName2">Total Working Day</asp:Label>
                                                <asp:Label ID="lblwork" runat="server" CssClass=" smLbl_to lblStyle2"> Working</asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="Label15" runat="server" CssClass=" lblTxt lblName lblName2">Total Late Day</asp:Label>
                                                <asp:Label ID="lblLate" runat="server" CssClass=" smLbl_to lblStyle2"> Late</asp:Label>
                                            </div>


                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label17" runat="server" CssClass=" lblTxt lblName lblName2">Total Leave Day</asp:Label>
                                                <asp:Label ID="lblLeave" runat="server" CssClass=" smLbl_to lblStyle2"> Leave</asp:Label>
                                            </div>
                                            <div class="col-md-6">

                                                <asp:Label ID="Label19" runat="server" CssClass=" lblTxt lblName lblName2">Total Absent Day</asp:Label>
                                                <asp:Label ID="lblAbsent" runat="server" CssClass=" smLbl_to lblStyle2"> Absent</asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label21" runat="server" CssClass=" lblTxt lblName lblName2">Total Holiday Day</asp:Label>
                                                <asp:Label ID="lblHoliday" runat="server" CssClass=" smLbl_to lblStyle2"> Holiday</asp:Label>
                                            </div>
                                            <div class="col-md-6"></div>


                                        </div>
                                        <div class="row" style="display: none">
                                            <asp:Label ID="Label2" runat="server" CssClass=" lblTxt lblName"> Company Name</asp:Label>
                                            <asp:Label ID="lblcompname" runat="server" CssClass=" smLbl_to lblStyle2"> Company name</asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>



                        </div>
                    </fieldset>
                </div>


            </asp:Panel>
            <fieldset>
                <div class="form-horizontal">

                    <div class="form-group">
                        <asp:Label ID="lblServHead" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">C. DETAILS INFORMATION</asp:Label>

                    </div>
                </div>
            </fieldset>

            <asp:Repeater ID="RptMyAttenView" runat="server" OnItemDataBound="RptMyAttenView_ItemDataBound">

                <HeaderTemplate>
                    <table class="table-striped table-hover table-bordered grvContentarea grvHeader grvFooter" style="width: 40%;">
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
                            <asp:Label ID="lbltotal" runat="server" style="font-weight:bold;">Total</asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblTotalHour" runat="server" style="font-weight:bold;">40:00</asp:Label>
                        </td>
                         <td></td>

                    </tr>

                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <%--</ContentTemplate>--%>
    <%-- </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
</asp:Content>

