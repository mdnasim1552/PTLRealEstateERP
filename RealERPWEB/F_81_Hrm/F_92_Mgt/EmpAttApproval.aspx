<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpAttApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpAttApproval" %>

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
                    <div class="row">
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
