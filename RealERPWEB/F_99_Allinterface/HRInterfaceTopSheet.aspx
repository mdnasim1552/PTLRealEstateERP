<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HRInterfaceTopSheet.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.HRInterfaceTopSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .flowMenu ul {
            margin: 0;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
                /*border-bottom: 1px solid #e9e9e9;*/
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }



        ul.sidebarMenu li {
            display: block;
            list-style: none;
            border: 1px solid #00444C;
            padding: 0;
            /* border-bottom: 0; */
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                cursor: pointer;
                /* background: #32CD32; */
                background: #046971;
                border-radius: 5px;
                color: #fff;
                text-align: left;
                padding: 0 5px;
                border-bottom: 1px;
                line-height: 30px;
                color: #fff;
                font-size: 13px;
                font-weight: normal;
                text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            }

                ul.sidebarMenu li a:hover {
                    background: #43b643;
                    color: #fff;
                }

        .AllGraph .nav-tabs {
            border-bottom: 0;
        }

        .sidebarMenu li h5 {
            background: #43b643;
            color: #fff;
            font-size: 15px;
            margin: 0;
            padding: 0;
            line-height: 35px;
            text-align: center;
        }






        #demo {
            margin-top: 30px;
            position: absolute;
            z-index: 200;
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {


            $("input, select")
                .bind("keydown",
                    function (event) {
                        var k1 = new KeyPress();
                        k1.textBoxHandler(event);

                    });
            $('.chzn-select').chosen({ search_contains: true });


            //    $('#<%=this.gvEmpOverTime.ClientID%>').tblScrollable();
            $('#<%=this.gvarrear.ClientID%>').tblScrollable();
            $('#<%=this.gvEmpMbill.ClientID%>').tblScrollable();
            $('#<%=this.gvothearn.ClientID%>').tblScrollable();
            $('#<%=this.gvEmpOtherded.ClientID%>').tblScrollable();


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
            <div class="container moduleItemWrpper">
                <div class="contentPart">





                    <%--<div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border">

                                <div class="form-horizontal">


                                    <div class="form-group">

                                        <div class="col-md-10 pading5px">
                                            
                                            <asp:HyperLink ID="HpblnkNew" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=Overtime" CssClass="btn btn-xs btn-success"> Over Time</asp:HyperLink>
                                            <asp:HyperLink ID="hlnkLedger" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn" CssClass="btn btn-xs btn-warning">Launch/Other Earn</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_85_Lon/EmpLoanInfo" CssClass="btn btn-xs btn-success">Advance/Loan</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLinkTriBal" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=arrear"  CssClass="btn btn-xs btn-warning">Arrear</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=Mobile" CssClass="btn btn-xs btn-success">Mobile Bill</asp:HyperLink>
                                            <asp:HyperLink ID="hlnkabsnt" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_83_Att/HREmpAbsCt"   CssClass="btn btn-xs btn-warning">Absent Count</asp:HyperLink>
                                            <asp:HyperLink ID="hlnkempResign" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_92_Mgt/RetiredEmployee" CssClass="btn btn-xs btn-success">Employee Resign</asp:HyperLink>
                                             <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction"   CssClass="btn btn-xs btn-warning">Deduction</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Payroll" CssClass="btn btn-xs btn-success">Actual salary Sheet</asp:HyperLink>
                                            
                                        </div>


                                    </div>

                                </div>
                            </fieldset>
                        </asp:Panel>

                    </div>--%>

                    <div class="row">
                        <div class="col-md-2 pading5px">

                            <div class="flowMenu">
                                <ul class="dashCir block sidebarMenu">


                                    <li>
                                        <h5>Salary Sheet</h5>
                                    </li>

                                    <%--<li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=Overtime")%>" target="_blank">Over Time</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn")%>" target="_blank">Launch/Other Earn</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanInfo")%>" target="_blank">Advance/Loan</a></li>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=arrear")%>" target="_blank">Arrear</a></li>--%>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=Mobile")%>" target="_blank">Mobile Bill</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MLateAppDay")%>" target="_blank">Monthly Late Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval?Type=MabsentApp")%>" target="_blank">Monthly Absent Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HREmpAbsCt")%>" target="_blank">Absent Count</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RetiredEmployee")%>" target="_blank">Employee Resign</a></li>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction")%>" target="_blank">Deduction</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Payroll")%>" target="_blank">Actual salary Sheet</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll?Type=Salary&Entry=Mgt")%>" target="_blank">Salary Sheet Lock</a></li>


                                    <li>
                                        <h5>Entry</h5>
                                    </li>

                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntryForm")%>" target="_blank">New Employee Code</a></li>


                                </ul>
                            </div>



                        </div>
                        <div class="col-md-10 pading5px">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <div>
                                                <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="true">
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlType" runat="server" Width="233" AutoPostBack="True" CssClass="chzn-select form-control inputTxt pull-left">
                                                <asp:ListItem Value="Overtime">Over Time</asp:ListItem>
                                                <asp:ListItem Value="arrear">Arrear</asp:ListItem>
                                                <asp:ListItem Value="Mobile">Mobile Bill</asp:ListItem>
                                                <asp:ListItem Value="otherearn">Other Earning</asp:ListItem>
                                                <asp:ListItem Value="OtherDeduction"> Other Deduction</asp:ListItem>


                                                <%--<asp:ListItem>Actual Salary Sheet</asp:ListItem>
                                            <asp:ListItem>Salary Sheet Lock</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblType" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindDepartment" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindDepartment_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-5 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>



                                            <div class="pull-left">
                                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click" Text="Ok"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <%-- <div class="col-md-4 pading5px asitCol5 pull-left">
                                        
                                    </div>--%>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                            <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True" TabIndex="7">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False" Width="233"></asp:Label>
                                        </div>

                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSecSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" Width="233">
                                            </asp:DropDownList>


                                        </div>

                                    </div>



                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label3" runat="server" CssClass=" smLbl">Code</asp:Label>
                                            <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSearchEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>




                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>

                                        <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass="rbtnList1 margin5px"
                                            Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal"
                                            Width="380px" Visible="false">
                                            <asp:ListItem Selected="True">Management</asp:ListItem>
                                            <asp:ListItem>Non Management</asp:ListItem>
                                            <asp:ListItem>Both</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                            </fieldset>




                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewOvertime" runat="server">
                                    <asp:GridView ID="gvEmpOverTime" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvEmpOverTime_PageIndexChanging" ShowFooter="True"
                                        Width="831px" OnRowDeleting="gvEmpOverTime_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" DeleteText="<span style='color:red;' class='glyphicon glyphicon-trash'></span>" />

                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="120px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lTotal" runat="server" OnClick="lTotal_Click" CssClass="btn btn-primary primaryBtn">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text=' <%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdate" runat="server" OnClick="lUpdate_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fixed Hour">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvFixed" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixhour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hourly">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvhourly" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourly")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceiling(7PM-10PM)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvc1" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceiling(10:1PM-2AM)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvc2" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceiling(2AM-6PM)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvc3" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fixed Rate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFixedrate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Hourly(Rate)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvhourlyrate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvc1rate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvc2rate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvc3rate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFhour" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>

                                <asp:View ID="BankPayment" runat="server">
                                    <asp:GridView ID="gvBankPay" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvBankPay_PageIndexChanging"
                                        ShowFooter="True" Width="931px">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgProName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgIdCard" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkFiUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpName" runat="server"
                                                        Text='<%#"<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))  %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bank Serial No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbgvBankSno" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankseno")) %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank AC No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvBACNo" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankacno")) %>'
                                                        Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBamt" runat="server" ForeColor="White"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvAmt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvRemarks" runat="server" BackColor="Transparent" Font-Size="11px"
                                                        BorderColor="#660033" BorderWidth="0px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>


                                <asp:View ID="ViewHolidays" runat="server">
                                    <asp:GridView ID="gvEmpHoliday" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvEmpHoliday_PageIndexChanging" ShowFooter="True"
                                        Width="474px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ChkAllEmp" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="ChkAllEmp_CheckedChanged" Text="ALL " Width="50px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkHoliday" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hstatus"))=="True" %>'
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectionholiday" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="180px" Font-Bold="True"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateHoliday" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" OnClick="lUpdateHoliday_Click"
                                                        Style="text-decaration: none;">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:View>

                                <asp:View ID="ViewMobileBill" runat="server">

                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">



                                            <asp:Panel ID="pnlCopy" runat="server" Visible="false" Style="border: 1px solid blue;">

                                                <div class="form-group">
                                                    <div class="col-md-5 pading5px ">
                                                        <asp:Label ID="lblPrevious" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                                        <asp:DropDownList ID="ddlpreyearmon" runat="server" AutoPostBack="True"
                                                            TabIndex="11" CssClass=" ddlPage">
                                                        </asp:DropDownList>




                                                        <div class="colMdbtn pading5px">
                                                            <asp:LinkButton ID="lbtnCopy" runat="server" Text="Copy" OnClick="lbtnCopy_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>

                                            <div class="form-group">
                                                <div class="col-md-5 pading5px ">
                                                    <asp:CheckBox ID="chkcopy" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" />

                                                </div>


                                                <div class="col-md-2 pading5px asitCol3">
                                                </div>
                                            </div>



                                        </div>
                                    </fieldset>




                                    <asp:GridView ID="gvEmpMbill" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpMbill_PageIndexChanging"
                                        ShowFooter="True" Width="498px" OnRowDeleting="gvEmpMbill_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" DeleteText="<span style='color:red;' class='glyphicon glyphicon-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectionmb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalmBill" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalmBill_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b >"+"<br />"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateMbill" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateMbill_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile Number">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvphoneno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Limit<br> Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMbilllimit" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbilllimit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFMbilllimit" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Actual </br> Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMbill" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFMbillamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Internet Bill">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtintbill" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intbill")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFMbalance" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
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
                                </asp:View>
                                <asp:View ID="VieweNCAHSLEAVE" runat="server">
                                    <asp:GridView ID="gvEmpELeave" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpMbill_PageIndexChanging"
                                        ShowFooter="True" Width="572px" OnRowDeleting="gvEmpELeave_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" DeleteText="<span style='color:red;' class='glyphicon glyphicon-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectionLvEncash" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="200px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalEnLeave" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalEnLeave_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno0" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName0" runat="server"
                                                        Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateEnLeave" runat="server" OnClick="lbntUpdateEnLeave_Click"
                                                        CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance Leave">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvElave" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "eleave")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave Benifit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvEnCleave" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ecleave")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>

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
                                </asp:View>

                                <asp:View ID="ViewOtherDeduction" runat="server">


                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">



                                            <asp:Panel ID="Pnlother" runat="server" Visible="false" Style="border: 1px solid blue;">

                                                <div class="form-group">
                                                    <div class="col-md-5 pading5px ">
                                                        <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                                        <asp:DropDownList ID="ddlpreyearmonoth" runat="server" AutoPostBack="True"
                                                            TabIndex="11" CssClass=" ddlPage">
                                                        </asp:DropDownList>




                                                        <div class="colMdbtn pading5px">
                                                            <asp:LinkButton ID="lblbtncopyoth" runat="server" Text="Copy" OnClick="lblbtncopyoth_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>

                                            <div class="form-group">
                                                <div class="col-md-5 pading5px ">
                                                    <asp:CheckBox ID="Chkother" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="Chkother_CheckedChanged" />

                                                </div>


                                                <div class="col-md-2 pading5px asitCol3">
                                                </div>
                                            </div>



                                        </div>
                                    </fieldset>

                                    <asp:GridView ID="gvEmpOtherded" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpOtherded_PageIndexChanging"
                                        ShowFooter="True" Width="685px" OnRowDeleting="gvEmpOtherded_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" DeleteText="<span style='color:red;' class='glyphicon glyphicon-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSection" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalOtherDed" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalOtherDed_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateOtherDed_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Leave Deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvleaveded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFleaveded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Arrear Deduccion">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvarairded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTarrearded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Advanced deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsaladv" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saladv")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFSaladv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lunch">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtfallow" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fallded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFoterded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile bill">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtmbill" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotermbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transport">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvTransDed" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "transded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFoterTransDed" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvotherded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otherded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotherded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amt.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
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

                                </asp:View>
                                <asp:View ID="loandeduction" runat="server">
                                    <asp:GridView ID="gvEmploan" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmploan_PageIndexChanging"
                                        ShowFooter="True" Width="732px" Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionlondded" runat="server" Font-Bold="true" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalLoan" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalLoan_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno1" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName1" runat="server" Height="16px"
                                                        Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig"))%>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateLoan" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateLoan_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company Loan">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcloan" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cloan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PF Loan">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvpfloan" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfloan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amt.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFLToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvltoamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
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
                                </asp:View>
                                <asp:View ID="Viewarersalary" runat="server">
                                    <asp:GridView ID="gvarrear" runat="server" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvarrear_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDeleting="gvarrear_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" DeleteText="<span style='color:red;' class='glyphicon glyphicon-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>


                                                    <table style="width: 150px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Section" Width="110px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>

                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>

                                                            </td>
                                                        </tr>
                                                    </table>

                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnCalArrear" runat="server" CssClass="btn btn-primary primarygrdBtn"
                                                        Font-Size="12px" OnClick="lbtnCalArrear_Click">Calculation</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionarrear" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalArrear" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalArrear_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateArrear" runat="server" OnClick="lbntUpdateArrear_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear Salary">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtarrear" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFarrearamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PF.Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPFAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPFTotal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tapfamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvAPFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PF" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPercent" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
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
                                </asp:View>

                                <asp:View ID="ViewOtherEarn" runat="server">

                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">



                                            <asp:Panel ID="PnlEarn" runat="server" Visible="false" Style="border: 1px solid blue;">

                                                <div class="form-group">
                                                    <div class="col-md-5 pading5px ">
                                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                                        <asp:DropDownList ID="ddlPremEarn" runat="server" AutoPostBack="True"
                                                            TabIndex="11" CssClass=" ddlPage">
                                                        </asp:DropDownList>




                                                        <div class="colMdbtn pading5px">
                                                            <asp:LinkButton ID="btnCopyEarn" runat="server" Text="Copy" OnClick="btnCopyEarn_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>

                                            <div class="form-group">
                                                <div class="col-md-5 pading5px ">
                                                    <asp:CheckBox ID="ChkEarn" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="ChkEarn_CheckedChanged" />

                                                </div>


                                                <div class="col-md-2 pading5px asitCol3">
                                                </div>
                                            </div>



                                        </div>
                                    </fieldset>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvothearn" runat="server"
                                            AutoGenerateColumns="False"
                                            ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvothearn_PageIndexChanging"
                                            OnRowDeleting="gvothearn_RowDeleting">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpIdearn" runat="server" Font-Bold="True" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSectionearn" runat="server" Font-Bold="true"
                                                            Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalOthEarn" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalOthEarn_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardnoearn" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name &amp; Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpNameearn" runat="server" Height="16px"
                                                            Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbntUpdateOthEarn" runat="server" OnClick="lbntUpdateOthEarn_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TPT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvtpallow" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tptallow")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtpallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KPI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvkpi" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "kpi")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFkpi" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Per. Bonus">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txgvperbon" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFperbon" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hair Cutt.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txgvhaircut" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "haircutal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFhaircut" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fooding">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txgvfood" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "foodal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFfood" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Food Taken <br>Day" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txgvftakenday" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "factualday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtakenday" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Night Fooding">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txgvnightfood" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nfoodal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFnightfood" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Others">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvotherearn" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othearn")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFotherearn" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtotal" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
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
                                </asp:View>

                                <asp:View ID="ViewAdjustment" runat="server">
                                    <asp:GridView ID="grvAdjDay" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="grvAdjDay_PageIndexChanging"
                                        OnRowDeleting="grvAdjDay_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionearn" runat="server" Font-Bold="true"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                        CssClass="btn btn-primary primaryBtn">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearn" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name &amp; Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpNameearn" runat="server" Height="16px"
                                                        Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                        CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Late Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDelday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Stylef="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dalday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Approved Days">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnCalCulationSadj" runat="server" OnClick="lbtnCalCulationSadj_Click">Calculation</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtaprday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Adjust Day">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAdj" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAdj" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
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
                                </asp:View>

                            </asp:MultiView>
                        </div>
                    </div>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


