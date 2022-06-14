<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ComplainForm.aspx.cs" Inherits="RealERPWEB.F_30_Facility.ComplainForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            addNewComplainDiv();
        }
            
        function addNewComplainDiv() {

            let itemCount = $("#divComplainList .row").length;

          

            if (itemCount > 0) {
                $(".btnAdd").remove();
            }
            let innerHTML = `<div class="row my-1"> 
                        <div class="col-3">
                             <input ID="txtProblemList" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-3">
                             <input ID="txtremarks" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <button type="button" class="btn btn-danger btn-sm btnremove" onclick="removeThisItem('${itemCount}')">Remove</button>
                        </div>
                        <div class="col-1 btnAdd">
                            <button type="button" class="btn btn-info btn-sm" onclick="addNewComplainDiv()">Add</button>
                        </div></div>`;


            $("#divComplainList").append(innerHTML);
           
        }
        function removeThisItem(itemCountfromList) {
            let itemCount = $("#divComplainList .row").length;
            console.log(itemCount, itemCountfromList);
            if (("#divComplainList .row")[itemCountfromList].hasClass("btnremove")) {

            }
            else {
                $("#divComplainList .row")[itemCountfromList].remove();
            }
          
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">

                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lbldate"  class="form-label">Date</asp:Label>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputDateBox"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                        </div>
                         <div class="col-1 d-flex align-items-center">
                              <asp:Label runat="server" ID="lblissuetype"  class="form-label">Issue Type</asp:Label>                          
                        </div>
                        <div class="col-3 d-flex align-items-center">
                            <asp:DropDownList ID="ddlIssueType" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>                            
                        </div>
                       
                    </div>
                    <div class="row mt-1">
                          <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblProject"  class="form-label">Project</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                            <asp:DropDownList ID="ddlProject" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>                            
                        </div>
                        <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblCustomer"  class="form-label">Customer</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                            <asp:DropDownList ID="ddlCustomer" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>                            
                        </div>
                         <div class="col-1 d-flex align-items-center">
                            <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-primary align-self-end" OnClick="btnOKClick_Click">OK</asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <div class="row mt-1">
                          <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblUnitlabel"  class="form-label">Unit</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblUnitText"  class="form-control"></asp:Label>                   
                        </div>
                        <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblHandOverDateLabel" class="form-label">HandOver Date</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                             <asp:Label runat="server" ID="lblHandOverDateText"  class="form-control"></asp:Label>                                  
                        </div>                         
                    </div>
                     <div class="row mt-1">
                          <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblWarranty"  class="form-label">Warranty</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                             <asp:DropDownList ID="ddlWarranty" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>                        
                        </div>
                        <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblWarrantyRemain"  class="form-label">Warranty Day Remained</asp:Label>
                        </div>
                         <div class="col-3 d-flex align-items-center">
                             <asp:Label runat="server" ID="lblWarrantyRemainText"  class="form-control"></asp:Label>                                  
                        </div>
                         
                    </div>
                    <div class="row mt-1">
                         <div class="col-1 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblComType"  class="form-label">Communication Type</asp:Label>
                        </div>
                        <div class="col-3 d-flex align-items-center">
                             <asp:DropDownList ID="ddlCommunicationType" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>                        
                        </div>
                    </div>

                    <hr />

                    <div id="divComplainList">
                        
                    </div>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
