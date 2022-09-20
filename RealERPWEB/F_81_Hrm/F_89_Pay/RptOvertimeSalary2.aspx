<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptOvertimeSalary2.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptOvertimeSalary2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .grvContentarea {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });



        }


        function loadModal() {
            $('#overtimeinfo').modal('toggle');
        }

        function CloseModal() {
            $('#overtimeinfo').modal('hide');


        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                    <div class="row mt-1">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Company</asp:Label>
                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control d-none"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnCompany" runat="server" OnClick="imgbtnCompany_Click"><i class="fas fa-search"></i></asp:LinkButton>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  inputTxt pull-left chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>

                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                <ProgressTemplate>
                                    <asp:Label ID="s" runat="server" CssClass="btn  primaryBtn " Text="Please wait . . . . . . ."></asp:Label>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6" id="divBracnhLsit" runat="server">
                            <asp:Label ID="Label9" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="lblDept" runat="server" CssClass="form-label">Department</asp:Label>
                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control d-none"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnProSrch" runat="server" OnClick="imgbtnProSrch_Click"><i class="fas fa-search"></i> </span></asp:LinkButton>
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                            </cc1:ListSearchExtender>
                            <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label3" runat="server" CssClass="form-label">Section</asp:Label>
                            <asp:TextBox ID="txtSrcSec" runat="server" CssClass="form-control d-none"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnSecSrch" runat="server" OnClick="imgbtnSecSrch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>

                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div>
                                <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                            </div>

                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6 mr-3">

                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblPage" runat="server" CssClass="form-label">Page Size</asp:Label>

                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left mt-4" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>


                        <%--                                    <div class="col-md-2 pull-right mt-3">
                                        <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Next</a>


                                        <%--<asp:HyperLink ID="hlnextEntry" Visible="false" class="btn btn-info primaryBtn margin5px" runat="server" NavigateUrl="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Entry")%>">Next</asp:HyperLink>
                                    </div>--%>
                    </div>




                    <div class="row mt-4 ">

                        <asp:GridView ID="gvovertime" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvovertime_PageIndexChanging"
                            ShowFooter="True" Width="831px">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CARD #">

                                    <%--     <FooterTemplate>
                                                <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-primary primarygrdBtn" OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>

                                    <ItemTemplate>
                                        <asp:Label ID="lgIdCard" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Section">


                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name &amp; Designation">


                                    <ItemTemplate>
                                        <asp:Label ID="lgvndesig" runat="server"
                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdesig" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right">Total :</asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvGsal" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFgssal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Basic">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBasic" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbSal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="House Rent">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvhrent" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hrent")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFhrent" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Daily Allowance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdailyAllow" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otall")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdailyAllow" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Convence">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCon" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cven")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCon" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Medical">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMedical" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mallow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmallow" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Overtime </br>Hour">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvSPday" runat="server" Style="text-align: right" OnClick="lbtngvSPday_Click"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovthour")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:LinkButton>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOvertime" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Overtime Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvovrate" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovtrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Overtime Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSPday" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovtamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOvtamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>


                                    <FooterStyle HorizontalAlign="Right" />
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


            <div id="overtimeinfo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">


                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="glyphicon glyphicon-hand-right"></span>
                                <asp:Label ID="lbmodalheading" runat="server" CssClass="justify-content-start"></asp:Label>
                            </h4>

                        </div>
                        <div class="modal-body">

                            <div class="col-md-12 col-sm-12 col-lg-12">
                                <asp:GridView ID="mgvbreakdown" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvgrp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Day">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateday1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accual In time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Out time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Hour">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ovhour1"))%>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="mlgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                        <div class="modal-footer">

                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>











        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


