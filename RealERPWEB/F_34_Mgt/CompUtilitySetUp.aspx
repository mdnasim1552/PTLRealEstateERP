﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CompUtilitySetUp.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CompUtilitySetUp" %>
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

            <div class="card mt-4 pb-4" style="min-width:480px;" id="divBody" runat="server">
                <div class="card-body">
                    <div class="row">
                         <div class="col-md-3">
                          
                            <label class="control-label">ID Card Length</label>
                              <asp:TextBox ID="txtIdcard" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>

                        
                        <div class="col-md-1">

                            <label class="control-label">Start Date</label>
                          <asp:TextBox ID="txtstrtdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-6" style="margin-top:34px;">
                             <asp:CheckBox ID="chkCRMddata" runat="server" ></asp:CheckBox>
                            <label class="control-label">CRM Back Date Data</label>
                             <asp:CheckBox ID="chkBag" runat="server" style="margin-left:12px;" ></asp:CheckBox>
                             <label class="control-label" >Bangla</label>
                            
                            <asp:CheckBox ID="chkLvapp" runat="server" style="margin-left:12px;" ></asp:CheckBox>
                            <label class="control-label" >Leave App Supervisor</label>
                             <asp:CheckBox ID="chkPay" runat="server" style="margin-left:12px;"></asp:CheckBox>
                            <label class="control-label">Payslip</label>
                             
                      
                        </div>
                        
                       <div class="col-md-1" style="margin-top:30px;">
                   <asp:LinkButton ID="lnkUpdate"  runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkUpdate_Click" AutoPostBack="True">Update</asp:LinkButton>
                       </div>
                        
                       
                        </div>
                    
                   
                </div>
            </div>

           

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
