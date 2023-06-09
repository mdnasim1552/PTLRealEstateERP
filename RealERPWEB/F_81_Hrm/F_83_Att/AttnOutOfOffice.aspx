﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AttnOutOfOffice.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.AttnOutOfOffice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });



           <%-- var gvSummary = $('#<%=this.gvSaleFunnel.ClientID %>');
            gvSummary.Scrollable();--%>


        };

    </script>

    <style>
        .chzn-container-single {
            width: 200px !important;
            height: 34px !important;
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            height: 34px !important;
        }
        .emplist .chzn-container-single {
            width: 80% !important;
            height: 34px !important;
        }



    </style>

    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-body">
            <div class="row" id="topPanle" runat="server" visible="false">

                <div class="col-md-2 p-0">

                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary " type="button">Date</button>
                        </div>

                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control" AutoComplete="off"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy hh:mm:ss tt"
                            Enabled="True" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                    </div>
                </div>
                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Company</button>
                        </div>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Department</button>
                        </div>
                        <asp:DropDownList ID="ddlDpt" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlDpt_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Section</button>
                        </div>
                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>

                    </div>
                </div>

            </div>

            <div class="row mt-1">
                <div class="col-md-6 p-0 emplist" id="ShowEmp" runat="server">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Employee </button>
                        </div>
                        <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-4">
                               <asp:Label ID="lblCurrentDate" class="badge bg-purple text-white" Font-Size="Large" runat="server"></asp:Label>

                </div>

                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Button ID="btnSaveAttn" runat="server" OnClick="btnSaveAttn_Click" Text="Save"  CssClass="btn btn-md btn-success" />
                    </div>
                </div>
            </div>
            <div class=" clearfix">
                <br />
            </div>

            <div class="row mt-1">
                <div class="col-md-3" id="ReasonType" runat="server">
                    <div class="form-group">
                        <label for="txtNote">Reason</label>
                        <asp:DropDownList ID="ddlReson" CssClass="form-control" runat="server">
                            <asp:ListItem Value="P.A">Project Attendance (P.A)</asp:ListItem>
                            <asp:ListItem Value="F.V">Factory Visit (F.V)</asp:ListItem>
                            <asp:ListItem Value="P.V">Project Visit (P.V)</asp:ListItem>
                            <asp:ListItem Value="C.M">Client Metting (C.M)</asp:ListItem>
                            <asp:ListItem Value="O.W">Office Work (O.W)</asp:ListItem>
                            <asp:ListItem Value="VAT.V">VAT Office Visit  (VAT.V)</asp:ListItem>
                            <asp:ListItem Value="DEDO.V">DEDO Office Visit(DEDO.V)</asp:ListItem>
                            <asp:ListItem Value="Tax.V">Tax Office Visit(Tax.V)</asp:ListItem>
                            <asp:ListItem Value="CTG.V">CTG Port Visit(CTG.V)</asp:ListItem>
                            <asp:ListItem Value="Beana.V">Beanpole Port Visit(Beana.V)</asp:ListItem>
                            <asp:ListItem Value="A.L">Annual Leave (A.L)</asp:ListItem>
                            <asp:ListItem Value="C.L">Casual Leave(C.L) </asp:ListItem>
                            <asp:ListItem Value="M.L">Medical Leave(M.L) </asp:ListItem>
                            <asp:ListItem Value="I.V">India Visit(I.V) </asp:ListItem>
                            <asp:ListItem Value="Oth">Other (Oth)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">


                    <div class="form-group" id="WorkComments" runat="server">
                        <label for="txtNote">Out of Work</label>
                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>

                    </div>


                </div>
            </div>



        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
