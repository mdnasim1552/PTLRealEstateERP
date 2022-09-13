<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="RealERPWEB.F_38_AI.CustomerList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript" language="javascript">
         $(document).ready(function () {
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


         });
         function pageLoaded() {

             $("input, select").bind("keydown", function (event) {
                 var k1 = new KeyPress();
                 k1.textBoxHandler(event);
             });

             $('.chzn-select').chosen({ search_contains: true });
         }
         function checkCustomerAdd() {
             OpenAddCustomer();
         }
         function OpenAddCustomer() {

             $('#AddCustomerModal').modal('toggle');
         }

     </script>

    <asp:UpdatePanel  ID="UpdatePanel1" runat="server">
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

             <div class="section">
                 <div class="card mt-5">
                     <div class="row">
                          <asp:LinkButton ID="tblAddCustomerModal" runat="server" CssClass="btn btn-primary ml-auto  btn-sm mt20 mr-1" OnClick="AddCustomerModal_Click"><i class="fa fa-plus"></i>Add Customer</asp:LinkButton>
                     </div>

                       <div class="row">
                            <table class="table table-hover table-bordered table-responsive-md" id="tblDefault">
                                <thead>
                                    <tr style="background-color: #ECF3ED;">
                                        <th>Sl #</th>
                                        <th>First Name</th>
                                        <th>Last Name</th>
                                        <th>Country</th>
                                        <th>Email</th>
                                        <th>Phone Number</th>
                                        <th>Present Address</th>
                                        <th>Permanent Address</th>                                      
                                       
                                    </tr>
                                </thead>
                                <tbody class="text-center">
                                    <tr>
                                        <td>1</td>
                                        <td>MD. HARUN-UR RASHID (FCMA)</td>
                                        <td>HARUN-UR RASHID (FCMA)</td>
                                        <td>Bangladesh</td>
                                        <td>abc@gmail.com</td>
                                        <td>1258795</td>
                                        <td>Famget,Dhaka-1215</td>
                                        <td>Tejkunipara, Farmgate, Dhaka-1215, Bangladesh</td>
                                       
                                       
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>MD KAMRUL</td>
                                        <td> HASSAN</td>
                                        <td>Bangladesh</td>
                                        <td>abc@gmail.com</td>
                                        <td>124658795</td>
                                        <td>Famget,Dhaka-1215</td>
                                        <td>Tejkunipara, Farmgate, Dhaka-1215, Bangladesh</td>
                                        
                                    </tr>
                                </tbody>
                            </table>
                        </div>





                     <%-- Customer list Create  --%>
                      <div id="AddCustomerModal" class="modal " role="dialog" data-keyboard="false" data-backdrop="static">
                            <div class="modal-dialog modal-xl">
                                <div class="modal-content">
                                    <div class="modal-header bg-light">
                                        <h6 class="modal-title">New Customer Add</h6>
                                        <%--<asp:LinkButton ID="ModalLoanClose" runat="server" CssClass="close close_btn"  data-dismiss="modal"> &times; </asp:LinkButton>--%>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-lg-3 ">
                                                <asp:Label ID="ltbProject" runat="server">First Name</asp:Label>
                                                <asp:TextBox ID="txtfName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <%-- <cc1:CalendarExtender  runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>--%>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lbtCustName" runat="server">Last Name</asp:Label>
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="Label9" runat="server">Country</asp:Label>
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="Label17" runat="server">Email</asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="row mt-3">

                                            <div class="col-lg-3">
                                                <asp:Label ID="Label18" runat="server">Phone Number</asp:Label>
                                                <asp:TextBox ID="txtphoneNumber" runat="server" CssClass="form-control form-control-sm" TextMode="Number"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="Label19" runat="server">Present Address</asp:Label>
                                                <asp:TextBox ID="tblEndDate" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="Label20" runat="server">Permanent Address</asp:Label>
                                                <asp:TextBox ID="txtperAddress" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer row">
                                         <div class="d-flex justify-content-center float-right ">

                                                <%-- <button type="button" class="btn btn-danger  ml-auto  btn-md mt20 mr-1" data-bs-dismiss="modal">Close</button>--%>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary ml-auto  btn-md mt20 mr-1" >Customer Save</asp:LinkButton>

                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                 </div>

             </div>

         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
