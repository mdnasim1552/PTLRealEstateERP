<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="RealERPWEB.F_38_AI.CreateTask" %>

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
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-12">
                            <div class="form-group row">
                                <div class="p-0 col-lg-6 col-md-6 col-sm-12">                                     
                                        <asp:Label ID="Label14" runat="server">Date</asp:Label>
                                       <asp:TextBox ID="Txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                     <cc1:CalendarExtender  runat="server" Format="dd-MMM-yyyy" TargetControlID="Txtdate"></cc1:CalendarExtender>
                                </div>
                                   
                                <div class=" col-lg-6 col-md-6 col-sm-12">
                                    
                                        <asp:Label ID="Label3" runat="server">Order Type</asp:Label>
                                        <asp:DropDownList ID="ddlordertype" runat="server" CssClass="form-control chzn-select">
                                        </asp:DropDownList> 
                                
                            </div>
                                </div>
                            <div class="form-group row">
                                <asp:Label ID="Label4" runat="server">Batch</asp:Label>
                                <asp:DropDownList ID="ddlbatch" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="Label11" runat="server">Task Title</asp:Label>
                                <asp:TextBox ID="txttasktitle" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label8" runat="server">Task Type</asp:Label>
                                <asp:DropDownList ID="ddltasktype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4  col-md-4 col-sm-12">
                            <div class="form-group row">
                                <asp:Label ID="Label1" runat="server">Customer</asp:Label>
                                <asp:DropDownList ID="ddlcustomer" runat="server" CssClass="form-control chzn-select" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="form-group row">
                                <asp:Label ID="Label5" runat="server">Project Type</asp:Label>
                                <asp:DropDownList ID="ddlprotype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label6" runat="server">Work Type</asp:Label>
                                <asp:DropDownList ID="ddlworktype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <div class="p-0 col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label9" runat="server"> Task Quantity</asp:Label>
                                    <asp:TextBox ID="txtquantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class=" col-lg-6 col-md-6 col-sm-12">
                                    <asp:Label ID="Label10" runat="server">Work Hour</asp:Label>
                                    <asp:TextBox ID="txtworkhour" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class=" col-lg-4  col-md-4 col-sm-12">
                            <div class="form-group row">
                                <asp:Label ID="Label2" runat="server">Project</asp:Label>
                                <asp:DropDownList ID="ddlproject" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <asp:Label ID="Label7" runat="server">DataSet</asp:Label>
                                <asp:DropDownList ID="ddldataset" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                             <div class=" form-group row">
                                    <asp:Label ID="Label15" runat="server">Work Quantity</asp:Label>
                                    <asp:TextBox ID="txtworkquantity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <div class="form-group row">
                                <asp:Label ID="Label12" runat="server">Task Description</asp:Label>
                                <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-12">

                            <div class="form-group row ">
                                <asp:Label ID="Label13" runat="server">Remakrs</asp:Label>
                                <asp:TextBox ID="txtremaks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <div class="col-lg-12 col-md-12 col-sm-22 mt-4">
                            <div class="form-group text-center">
                                <asp:LinkButton ID="btntaskcreate" runat="server" OnClick="btntaskcreate_Click" CssClass=" btn btn-primary btn-sm mt20"><i class="fas fa-plus">&nbsp;CreateTask</i></asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
