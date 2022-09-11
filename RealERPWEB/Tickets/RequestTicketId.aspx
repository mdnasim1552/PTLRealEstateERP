<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RequestTicketId.aspx.cs" Inherits="RealERPWEB.Tickets.RequestTicketId" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
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
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-4"></div>
                <div class="col-lg-4 col-md-4 col-sm-4 mt-3">
                   
                        <div class="form-group">
                              <asp:Label ID="Label1" runat="server" Font-Bold="True">User ID</asp:Label>
                            <asp:TextBox ID="txtUserid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                     <div class="form-group">
                              <asp:Label ID="Label2" runat="server" Font-Bold="True">UserName</asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    <div class="form-group">
                              <asp:Label ID="Label3" runat="server" Font-Bold="True">Designation</asp:Label>
                            <asp:TextBox ID="txtDesig" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                     
                    <div class="form-group">
                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" CssClass=" form-control btn btn-primary btn-sm " OnClick="lnkbtnSubmit_Click">Submit</asp:LinkButton>
                    </div>
                  
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4"></div>
            </div>
              </div>

        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
