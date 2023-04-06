<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptReconcilationLetter.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptReconcilationLetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>


    <!-- Include the plugin's CSS and JS: -->
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <style>
        .multiselect {
            width: 300px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        #ContentPlaceHolder1_divgrp {
            width: 395px !important;
        }

        .form-control {
            height: 34px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

     <script language="javascript" type="text/javascript">

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

         function OpenDedModal() {
             $('#DeductinModal').modal('toggle');
         };

         function CloseDedModal() {
             $('#DeductinModal').modal('hide');
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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectList" runat="server" Text="Project Name"></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox d-none"></asp:TextBox>
                                <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="3"  OnSelectedIndexChanged="ddlprjlist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                        </div>
                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnFindSubConName" runat="server" CssClass="form-label">Contractor List</asp:LinkButton>
                                <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlcontractorlist" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <%--<div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblCatagory" runat="server" CssClass="smLbl_to" Text="Catagory"></asp:Label>
                                <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="chzn-select form-control  form-control-sm" TabIndex="12" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                           <div class="col-md-3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                        <div class="col-md-2 ml-5" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnok" runat="server" OnClick="lbtnok_Click" CssClass="btn btn-sm btn-primary">ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvflrwisbill" runat="server" AllowPaging="true" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvflrwisbill_PageIndexChanging"
                            ShowFooter="True" Width="650px">
                            <PagerSettings Position="Bottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPrjName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPrjcustName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPrjUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Paid Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPaidAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ")%>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Send Mail">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lgvPrjName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                            Width="180px"></asp:Label>--%>
                                        <asp:LinkButton ID="lbtnSendMail" runat="server" CssClass="btn btn-sm btn-primary m-1" OnClick="lbtnSendMail_Click">Send</asp:LinkButton>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lgvPrjName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                            Width="180px"></asp:Label>--%>
                                        <%--<asp:Button OnClientClick="target ='_blank';" ID="MyButton" runat="server" CssClass="btn btn-outline-primary" Text="View" />--%>
                                        <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-sm btn-primary m-1" OnClick="lbtnView_Click">View</asp:LinkButton>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnPrintMail" runat="server" CssClass="btn btn-sm  btn-success m-1" OnClick="lbtnPrintMail_Click">Print</asp:LinkButton>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                               

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%-- Deduction Modal --%>
            <div id="DeductinModal" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Reconsilation Letter</h4>
                            <button type="button" class="close btn btn-xs" data-dismiss="modal" title="Close"><i class="fa fa-times-circle" aria-hidden="true"></i></button>
                        </div>
                        <div class="modal-body form-horizontal" style="min-height: 200px;">
                            <div class="row-fluid">
                                <div class="row">
                                    <p>Dear Sir/Madam,<br /><br />
                                       Assalamu Alaikum.<br /><br />
                                       We are happy to inform you that we have received Tk. <asp:Label ID="TkLabel" runat="server" Text="Label"></asp:Label> (In Word: <asp:Label ID="TkLabelWord" runat="server" Text="Label"></asp:Label>)<br />
                                       as of <asp:Label ID="todayDateFormattedLabel" runat="server" Text="Label"></asp:Label> against Apt / Shop <asp:Label ID="unitLabel" runat="server" Text="Label"></asp:Label> of Finlay <asp:Label ID="projectLabel" runat="server" Text="Label"></asp:Label>.<br /><br />
                                       Please reply us within 07 (seven) days if any mismatch found. Otherwise, We will treat this<br />
                                       statement as correct.<br /><br />
                                    
                                       Best regards,<br /><br />
                                    
                                       Customer Management<br />
                                       Finlay Properties Limited</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             </div>
        </ContentTemplate>
         
    </asp:UpdatePanel>

</asp:Content>
