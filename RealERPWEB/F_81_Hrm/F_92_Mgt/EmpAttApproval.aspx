<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpAttApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpAttApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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


            <div class="card-fluid container-data  mt-2">
                <div class="row" id="warning" runat="server" visible="false">
                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                The Department: <b><span id="dptNameset" runat="server"></span></b>Approval user  did not set.
                                <br />
                                Please Contact you HR Department 
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <div class="card card-fluid container-data  mt-2">
                <div class="card-body">
                    <div class="row" id="LateApp" runat="server">
                        <div class="col-3">
                            <div class="card card-fluid">
                                  <header class="card-header text-center">EMPLOYEE INFORMATION</header>
                                <div class="card-body">
                                    <div class="text-center">
                                        <h2 class="h4 mt-2 mb-0" id="UserName" runat="server">Beni Arisandi </h2>
                                           <p class="text-muted mb-0" id="idcard" runat="server">ID Card- </p>
                                        <p class="text-muted  mb-0" id="UDesignation" runat="server">Project Manager @CreativeDivision </p>
                                        <p class="text-muted" id="UDptment" runat="server">Project Manager @UDptment </p>
                                     
                                      
                                    </div>

                                </div>
                            </div>
                            <div class="card card-fluid" hidden="hidden">
                                  <header class="card-header text-center">APPROVE INFORMATION</header>
                                <div class="card-body">
                                    <div class="text-center">
                                       

                                        <h2 class="h4 mt-2 mb-0" id="H1" runat="server">Mr.Rahim</h2>

                                        <p class="text-muted  mb-0" id="P1" runat="server">Project Manager HOD </p>
                                        <p class="text-muted" id="P2" runat="server">Project Manager @UDptment </p>
                                       
                                    </div>

                                </div>
                            </div>

                        </div>
                    
                        <div class="col-9">
                            <!-- form grid -->
                            <div class="card card-fluid pb-2">
                                 <header class="card-header">REQUEST APPROVAL
                                      <p class="text-muted  mb-0 fa-pull-right" id="Reqst" runat="server"> </p>
                                 </header>
                                <div class="card-body">
                                     <div class="form-row">
                                    <div class="col-md-6 mb-3">

                                        <label for="validationTooltip01">
                                            Request Type
                           
                                        <abbr title="Required">*</abbr>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlReqType" class="custom-select d-block w-100" required="" OnSelectedIndexChanged="ddlReqType_SelectedIndexChanged" AutoPostBack="true">
                                           <%-- <asp:ListItem Value="LA">Late Approval Request (if Finger 9:04:59 to 9:59:59)</asp:ListItem>
                                            <asp:ListItem Value="LP">Late Present Approval Request (if Finger 10:00 to 5:30)</asp:ListItem>
                                            <asp:ListItem Value="TC">Time Correction Approval Request (Project Visit, Customer visit, etc )</asp:ListItem>
                                            <asp:ListItem Value="AB">Absent Approval Request (IF Finger missed but present)</asp:ListItem> 
                                            <asp:ListItem Value="TLV">Time of Leave</asp:ListItem>   --%>                                                                   
                                            
                                        </asp:DropDownList>

                                    </div>
                                
                                        <div class="col-md-2 mb-3">
                                        <label for="validationTooltip02">
                                            Date 
                                        </label>
                                            <asp:TextBox ID="lbldadte" runat="server" CssClass="form-control"></asp:TextBox>

                                            <%--<asp:TextBox ID="lbldadte" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <%--<asp:Label ID="lbldadte" runat="server" class="form-control"></asp:Label>--%>

                                    </div>
                                       <div class="col-md-2 mb-3">
                                        <label for="validationTooltip02">
                                            Time  
                                        </label>
                                        <asp:Label ID="lbldadteOuttime" Visible="false" runat="server" class="form-control"></asp:Label>
                                        <asp:Label ID="lbldadteIntime" Visible="false" runat="server" class="form-control"></asp:Label>
                                        <%--<asp:Label ID="lbldadteTime" runat="server" class="form-control"></asp:Label>--%>
                                            <asp:TextBox ID="lbldadteTime" runat="server" CssClass="form-control" ></asp:TextBox>

                                    </div>

                                                         <div class="col-md-2">
                                            <label >
                                           Out Time  
                                        </label>
                                        <asp:TextBox ID="lblouttime" class="form-control" runat="server"  ></asp:TextBox>

                                    </div>
                                    

                                    <div class="col-md-12 mb-3">
                                        <label for="validationTooltipUsername">
                                            Employee Remarks/Reason                           
                                 <abbr title="Required">*</abbr>
                                        </label>

                                        <asp:TextBox ID="txtAreaReson" class="form-control" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </div>
                                    <!-- /form grid -->

                                   
                                    <div class="col-md-12 mb-3">
                                        <label for="ApprovedBy">Remaks</label>
                                        <asp:TextBox ID="txtremarks" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="col-12 mb-2 ">
                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClientClick="return confirm('Are you sure to cancel this item?');" CssClass="btn btn-danger btn-sm ApprovedBtn fa-pull-right" OnClick="lnkCancel_Click" BorderStyle="None">Cancel</asp:LinkButton>

                                           <asp:LinkButton ID="lnkApproved" runat="server" CssClass="btn btn-info  btn-sm ApprovedBtn fa-pull-right  mr-4" OnClick="lnkApproved_Click"  BorderStyle="None">Approved</asp:LinkButton>

                                    </div>
                                    <div class="col-2 mb-2 fa-pull-right">

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
