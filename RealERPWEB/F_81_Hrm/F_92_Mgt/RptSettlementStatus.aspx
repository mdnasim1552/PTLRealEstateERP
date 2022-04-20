<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSettlementStatus.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptSettlementStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }
        div#ContentPlaceHolder1_ddlWstation_chzn{
            width: 100% !important;

        }
        div#ContentPlaceHolder1_ddlDept_chzn{
            width: 100% !important;
        }
        div#ContentPlaceHolder1_ddlSection_chzn{
                      width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }
                        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
                .card-body{
                    min-height:400px!important;
                }
                      .pd4 {
            padding: 4px !important;
        }
    </style>

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
    <script>
        function SetTarget(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
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
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Date</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                        <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server">To</asp:Label>
                                        <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                        <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-1">
                     <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary btn-sm mt20" TabIndex="4"></asp:LinkButton>

                        </div>
                    </div>
                </div>
           
                <div class="card-body">
            <asp:GridView ID="gvSettInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" OnRowDataBound="gvSettInfo_RowDataBound">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID Card">
                        <ItemTemplate>
                            <asp:Label ID="lblgvidno" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idno")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <HeaderTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblheader" runat="server" Text="Employee Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fa fa-file-excel-o"></i></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblgvempid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                Width="80px"></asp:Label>
                            <asp:Label ID="lblgvempname" runat="server" Height="16px" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                Width="150px"></asp:Label>


                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Settlement </br>Date">
                        <ItemTemplate>
                            <asp:Label ID="lblgvbilldate" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation">


                        <ItemTemplate>
                            <asp:Label ID="lblgvdesignation" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designation")) %>'
                                Width="120px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="txtgvSdupplier" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='Total'></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvdeptname" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Joining </br>Date">
                        <ItemTemplate>
                            <asp:Label ID="lblgvjoindat" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seperation</br> Date">
                        <ItemTemplate>
                            <asp:Label ID="lblgvretdat" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retdat")).ToString("dd-MMM-yyyy") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Payable </br>Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblgvttlamt" runat="server" BackColor="Transparent"
                                BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:Label ID="lblgvFttlamt" runat="server" BackColor="Transparent"
                                BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                        </FooterTemplate>

                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Service Length">
                        <ItemTemplate>
                            <asp:Label ID="lblgvservleng" runat="server" AutoCompleteType="Disabled"
                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "servleng")) %>'
                                Width="130px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>

                            <asp:Label ID="lgvLink" runat="server" BackColor="Transparent"
                                BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvstatus")).ToString() %>'></asp:Label>


                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" verticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="left" />
                        <FooterStyle HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ToolTip="Edit And Approve" ID="lnkEdit" Target="_blank" CssClass="btn btn-xs btn-success" runat="server"></asp:HyperLink>


                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Print">
                        <ItemTemplate>

                            <asp:LinkButton ID="HypRDDoPrint" runat="server" OnClick="HypRDDoPrint_Click" CssClass="btn btn-xs btn-danger"><i class="fa fa-print"></i>
                            </asp:LinkButton>

                        </ItemTemplate>
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


            <%--              <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                   
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                        <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                    </div>


                                </div>



                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                        <asp:Label ID="lblLcName" runat="server" CssClass="smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txtdateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary okBtn" TabIndex="4"></asp:LinkButton>

                                    </div>



                                </div>

                            </div>--%>



            <div class="form-group">

                <asp:Label ID="lblHeadCost" runat="server" CssClass="btn btn-primary primaryBtn"
                    Text="Cost:" Visible="False">

                             
                </asp:Label>

            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
