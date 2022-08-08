<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="VehicleMgt.aspx.cs" Inherits="RealERPWEB.F_36_Vehcl.VehicleMgt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .custom-file .custom-file-label,{
            height:1.94rem!important;
        }
        .mt20 {
            margin-top: 20px;
        }
        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script>

        $(document).ready(function () {
            $(document).on("click", ".extra-fields-customer", function () {
                $('.customer_records').clone().appendTo('.customer_records_dynamic');
                $('.customer_records_dynamic .customer_records').addClass('single remove ');
                $('.customer_records_dynamic .customer_records .rmdv').remove();
                $('.single .extra-fields-customer').remove();
                $('.single').append('<a href="#" class="btn btn-sm btn-danger remove-field btn-remove-customer mt20"><i class="fa fa-trash"></i> Remove</a>');
                $('.customer_records_dynamic > .single').attr("class", "remove row");

                $('.customer_records_dynamic input').each(function () {
                    var count = 0;
                    var fieldname = $(this).attr("name");
                    $(this).attr('name', fieldname + count);
                    count++;
                });

            });

            $(document).on('click', '.remove-field', function (e) {

                event.target.parentElement.remove();
                $('.customer_records_dynamic .customer_records ').remove();
                e.preventDefault();
            });


        });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
            }
            catch (e) {
                alert(e);
            }

        };
        $('.chzn-select').chosen({ search_contains: true });

        function OpenVehcl() {

            $('#addVchlModal').modal('toggle');
        }
        function CloseVehcl() {

            $('#addVchlModal').modal('toggle');
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
            <div class="card mt-5">
                <div class="card-header d-flex ">
 
  <div class="p-2 mr-auto">
      <h6 class="card-title" style="margin:0;">Vehicles Management</h6>
  </div>
  <div class="ml-auto p-2"> 
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-sm ml-auto" OnClick="AddvchlModal_click"><i class="fa fa-plus"></i> Add Vehicles</asp:LinkButton>

  </div>
                </div>
                <div class="card-body">
                    <table class="table table-bordered col-10 mx-auto">
                        <thead class="bg-light">
                            <tr>

                                <th>SL#</th>
                                <th scope="col">ID</th>
                                <th scope="col">Vehicle Model </th>
                                <th scope="col">Reg. NO</th>
                                <th scope="col">Reg.Docs</th>
                                <th scope="col">Start Time</th>
                                <th scope="col">End Date</th>
                                <th scope="col">Expiry Date</th>
                                <th scope="col">Type</th>
                                <th scope="col">Assign Driver Date</th>
                                <th scope="col">Status</th>
                                <th scope="col">Action</th>

                            </tr>
                        </thead>
                        <tbody>
                            <tr>

                                <td>1</td>
                                <td>v-12</td>
                                <td>dsfsd</td>
                                <td>sfsdf</td>
                                <td>Probox</td>
                                <td>14-5558</td>
                                <td>Docs-(POP UP)</td>
                                <td>07:00 AM</td>
                                <td>09:00 PM</td>
                                <td>Rent </td>

                                <td>pending</td>

                                <td>
                                    <asp:HyperLink runat="server" ID="lnkView"><i class="fa fa-eye"></i></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>

                                <td>1</td>
                                <td>v-12</td>
                                <td>dsfsd</td>
                                <td>sfsdf</td>
                                <td>Probox</td>
                                <td>14-5558</td>
                                <td>Docs-(POP UP)</td>
                                <td>07:00 AM</td>
                                <td>09:00 PM</td>
                                <td>Rent </td>

                                <td>pending</td>

                                <td>
                                    <asp:HyperLink runat="server" ID="HyperLink1"><i class="fa fa-eye"></i></asp:HyperLink>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </div>

            <div class="modal" id="addVchlModal" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Add Vehicles</h6>
                            <asp:LinkButton ID="CloseVehcl" runat="server" CssClass="close close_btn" OnClientClick="CloseVehcl();" data-dismiss="modal"> &times; </asp:LinkButton>
                        </div>
                        <div class="modal-body mt-3">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblLoanId" runat="server">Vehicle Id</asp:Label>
                                        <asp:TextBox ID="txtVid" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server">Registration No</asp:Label>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server">Model</asp:Label>
                                        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server">Type</asp:Label>
                                        <asp:TextBox ID="txtType" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">OP</asp:Label>
                                        <asp:TextBox ID="txtop" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server">Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server">Start Time</asp:Label>
                                        <asp:TextBox ID="txtstartdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server">End Time</asp:Label>
                                        <asp:TextBox ID="txtenddat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row customer_records">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server">Doc Type</asp:Label>
                                        <asp:TextBox ID="txtdtype" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server">Attachment</asp:Label>
                           <asp:FileUpload ID="imgFileUpload" CssClass="form-control form-control-sm" runat="server" AllowMultiple="true"/>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server">Expiry</asp:Label>
                                        <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <%-- <a class="Addfields btn btn-success btn-sm ml-auto" id="AddField" href="#">Add More</a>--%>
                                <a class="extra-fields-customer btn btn-success btn-sm  mt20" href="#"><i class="fa fa-plus"></i>Add More </a>



                            </div>



                            <div class="customer_records_dynamic"></div>

                            <div class="doctbl row">
                                <table class="table table-bordered table-striped table-hover table-bordered grvContentarea col-8 mx-auto">
                                    <thead class="bg-light">
                                        <tr>

                                            <th scope="col">Type</th>
                                            <th scope="col">Expiry Date</th>
                                            <th scope="col">Docs</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>

                                            <td>Application for vehicle</td>
                                            <td>2022-10-10</td>
                                            <td>@URL</td>
                                        </tr>
                                        <tr>

                                            <td>Application for vehicle</td>
                                            <td>2022-10-10</td>
                                            <td>@URL</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>




                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <%--        <Triggers>
            <asp:PostBackTrigger ControlID="lnk_save" />
        </Triggers>--%>
    </asp:UpdatePanel>



</asp:Content>




