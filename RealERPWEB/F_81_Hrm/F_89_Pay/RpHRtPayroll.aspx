<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RpHRtPayroll.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RpHRtPayroll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .margin5px {
        }
        /*
        .dropdown-toggle:after {
            display: inline-block;
            margin-left: .255em;
            vertical-align: .255em;
            content: "";
            border-top: .3em solid;
            border-right: .3em solid transparent;
            border-bottom: 0;
            border-left: .3em solid transparent;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });



        }


    </script>



    <%--<script language="javascript" type="text/javascript">
       $(document).ready(function () {

          
           var ddlpage = $('#<%=this.ddlpagesize.ClientID %>');
           ddlpage.change(function () {
             var ddlpageno = ddlpage.val();
             alert(ddlpageno);
             var gvpaysal = $('#<%=this.gvpayroll.ClientID %>');
             gvpaysal.Scrollable();
           });
       });<a href="RpHRtPayroll.aspx">RpHRtPayroll.aspx</a>
    --%>
    <%--       function ChangePage() {

////           var ddlpage = $('#<%=this.ddlpagesize.ClientID %>').val();
//           //           alert(ddlpage);

//           var gvpaysal = $('#<%=t<a href="RpHRtPayroll.aspx">RpHRtPayroll.aspx</a>his.gvpayroll.ClientID %>');
//           gvpaysal.Scrollable();

//              
//       
//       }
//  
  </script>--%>





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
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt  chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>


                                    </div>

                                    <div class=" col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="s" runat="server" CssClass="btn btn-info primaryBtn " Text="Please wait . . . . . . ."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>

                                    <%--<div class="col-md-2">
                                        


                                    </div>--%>

                                    <div class="col-md-3 pull-right">
                                        <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Next</a>
                                        <%--<a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EntryDeduction02.aspx")%>" target="_blank">Deduction</a>--%>


                                        <%--<asp:HyperLink ID="hlnextEntry" Visible="false" class="btn btn-info primaryBtn margin5px" runat="server" NavigateUrl="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Entry")%>">Next</asp:HyperLink>--%>

                                        <panel id="pnlsalops" runat="server" visible="true">
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-success">Operations</button>
                                                <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown">
                                                    <span class="caret"></span>
                                                </button>

                                                <ul class="dropdown-menu" role="menu">
                                                    <li>
                                                        <asp:HyperLink ID="hylnkarrear" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=arrear"> Arrear Salary</asp:HyperLink>

                                                    </li>
                                                    <li>
                                                        <asp:HyperLink ID="hylnkloan" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_85_Lon/EmpDeducOther">Loan Installment</asp:HyperLink></li>
                                                    </li>
                                              <li>
                                                  <asp:HyperLink ID="hylnkOtherdeduction" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=OtherDeduction">Other Deduction </asp:HyperLink>
                                              </li>
                                                    <li>
                                                        <asp:HyperLink ID="hylnkOtherearn" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=otherearn">Other Earning </asp:HyperLink>
                                                    </li>
                                                    <li>
                                                        <asp:HyperLink ID="hylnkOvetime" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=Overtime">Employee Overtime </asp:HyperLink>

                                                    </li>
                                                    <li>
                                                        <asp:HyperLink ID="hylnkAbscount" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_83_Att/HREmpAbsCt">Absent Count </asp:HyperLink>

                                                    </li>

                                                    <li>
                                                        <asp:HyperLink ID="hylnkempTrans" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_87_Tra/HREmpTransfer">Employee Transfer </asp:HyperLink>

                                                    </li>


                                                    <li>
                                                        <asp:HyperLink ID="hylnkSalReduction" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EmpOvertime?Type=SalaryReduction">Salary Reduction </asp:HyperLink>

                                                    </li>

                                                    <li>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_81_Hrm/F_86_All/EntryDeduction02.aspx">Deduction </asp:HyperLink>

                                                    </li>



                                                </ul>
                                            </div>

                                        </panel>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblComBonLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                                        <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSecSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSection">
                                        </cc1:ListSearchExtender>
                                        <%--                                        <asp:Label ID="lblSectionDesc" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>--%>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblemp" runat="server" CssClass="lblTxt lblName" Visible="false">Employee List:</asp:Label>
                                        <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpListAllinfo_Click" Visible="false"><span class="glyphicon glyphicon-search asitGlyp" > </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="2" AutoPostBack="True" Width="335px" Visible="false">
                                        </asp:DropDownList>
                                    </div>


                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox " AutoComplete="off"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox" AutoComplete="off"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to ">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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


                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:RadioButtonList ID="rbtnPayType" RepeatDirection="Horizontal" CssClass="rbtnList1" Visible="false" runat="server">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem Selected="True">All</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                    <%--   <div class="col-md-2">
                                     <asp:CheckBox ID="chkExcluMgt" runat="server" CssClass="checkbox chkBoxControl" Text="Exclude Management" Visible="False" />

                                    </div>--%>



                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chkgrndt" runat="server" />
                                        <asp:Label ID="lblgrandt" runat="server">Print Grand Total</asp:Label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:CheckBox ID="chkBangla" runat="server" Visible="false" />
                                        <asp:Label ID="lblBangla" runat="server" Visible="false">Bangla Print</asp:Label>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="Salary" runat="server">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <asp:Panel ID="Panel6" runat="server">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <asp:RadioButtonList ID="rbtSalSheet" runat="server" CssClass="rbtnList1 margin5px"
                                                        Font-Size="14px" Height="16px" RepeatColumns="10" RepeatDirection="Horizontal"
                                                        Width="1728px">
                                                        <asp:ListItem>NDE</asp:ListItem>
                                                        <asp:ListItem>Foster</asp:ListItem>
                                                        <asp:ListItem>Sanmar</asp:ListItem>
                                                        <asp:ListItem>Multiplan</asp:ListItem>
                                                        <asp:ListItem>Rupayan</asp:ListItem>
                                                        <asp:ListItem>Asian TV</asp:ListItem>
                                                        <asp:ListItem>GLG</asp:ListItem>
                                                        <asp:ListItem>Assure</asp:ListItem>
                                                        <asp:ListItem>Leisure</asp:ListItem>
                                                        <asp:ListItem>Bridge</asp:ListItem>
                                                        <asp:ListItem>InnStar</asp:ListItem>
                                                        <asp:ListItem>Alliance</asp:ListItem>
                                                        <asp:ListItem>ACME</asp:ListItem>
                                                        <asp:ListItem>Suvastu</asp:ListItem>
                                                        <asp:ListItem>Tropical</asp:ListItem>
                                                        <asp:ListItem>Terranova</asp:ListItem>
                                                        <asp:ListItem>PEB</asp:ListItem>
                                                        <asp:ListItem>GreenWood</asp:ListItem>
                                                        <asp:ListItem>Manama</asp:ListItem>
                                                        <asp:ListItem>ERL</asp:ListItem>
                                                        <asp:ListItem>Entrust</asp:ListItem>
                                                        <asp:ListItem>BTI</asp:ListItem>
                                                        <asp:ListItem>JBS</asp:ListItem>




                                                    </asp:RadioButtonList>


                                                    <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass="rbtnList1 margin5px"
                                                        Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal"
                                                        Width="520px" Visible="false">
                                                    </asp:RadioButtonList>

                                                </div>

                                            </div>
                                        </asp:Panel>
                                    </div>
                                </fieldset>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvpayroll" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvpayroll_PageIndexChanging"
                                        ShowFooter="True" Width="831px">
                                        <PagerSettings Position="Top" Mode="NumericFirstLast" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgProName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID CARD">

                                                <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-primary primarygrdBtn" OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                                </FooterTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgIdCard" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>

                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Section" Width="70px"></asp:Label>


                                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>

                                                </HeaderTemplate>

                                                <FooterTemplate>
                                                    <asp:CheckBox ID="chkSalaryLock" runat="server" CssClass="btn btn-primary checkBox" Text="Lock" Width="90px" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                        Width="85px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name &amp; Designation">
                                                <%--<FooterTemplate>
                                                    <asp:LinkButton ID="lnkFiUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvndesig" runat="server"
                                                        Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrear">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvarsal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arsal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFarier" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoth" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oth")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoth" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allowance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtallow" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tallow")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary per Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSPday" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salpday")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Working Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvwday" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wd")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjust">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdedday" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Salary Payble">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgspay" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspay")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFgspay" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pro. Fund">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpdent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpfund" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Income Tax">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvitax" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFitax" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Advance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAdvance" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adv")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFadv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other ded.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothded" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Deduction">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtded" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdeduc")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnetsal" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnetSal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvrmrks" runat="server"
                                                        Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                        Width="180px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Cash Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lgvrmrks2" runat="server"
                                                        Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks2")) %>'
                                                        Width="120px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                            <asp:View ID="FestivalBonus" runat="server">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <panel id="Pnlmantype" runet="server">

                                            <div class="form-group">

                                                <asp:RadioButtonList ID="rbtnMantype" RepeatDirection="Horizontal" CssClass="rbtnList1" Visible="false" runat="server">
                                                    <asp:ListItem>Management</asp:ListItem>
                                                    <asp:ListItem> Acting Management</asp:ListItem>
                                                    <asp:ListItem>General Employee</asp:ListItem>
                                                    <asp:ListItem Selected="True">All</asp:ListItem>

                                                </asp:RadioButtonList>


                                            </div>

                                        </panel>

                                        <asp:Panel ID="PnlBonus" runat="server" Visible="False">
                                            <div class="form-group">

                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Bonus(%):</asp:Label>
                                                    <asp:TextBox ID="txtBonusPer" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkbtnGenBonus" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkbtnGenBonus_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                                <div class="col-md-6 pading5px">
                                                </div>


                                            </div>
                                        </asp:Panel>



                                        <asp:Panel ID="Panel5" runat="server">

                                            <div class="form-group">
                                                <div class="col-md-1 pading5px">
                                                    <asp:CheckBox ID="chkBonus" runat="server" CssClass="margin5px btn btn-primary  checkBox" AutoPostBack="True"
                                                        OnCheckedChanged="chkBonus_CheckedChanged" Text="Bonus" />
                                                </div>
                                                <div class="col-md-7 ">
                                                    <asp:RadioButtonList ID="rbtlBonSheet" runat="server" BackColor="#DBEBD4" CssClass="rbtnList1 chkBoxControl margin5px"
                                                        RepeatColumns="8" RepeatDirection="Horizontal" Width="856px">
                                                        <asp:ListItem>NDE</asp:ListItem>
                                                        <asp:ListItem>Foster</asp:ListItem>
                                                        <asp:ListItem>Sanmar</asp:ListItem>
                                                        <asp:ListItem>Multiplan</asp:ListItem>
                                                        <asp:ListItem>Leisure</asp:ListItem>
                                                        <asp:ListItem>Assure</asp:ListItem>
                                                        <asp:ListItem>Bridge</asp:ListItem>
                                                        <asp:ListItem>Alliance</asp:ListItem>
                                                        <asp:ListItem>ACME</asp:ListItem>
                                                        <asp:ListItem>Suvastu</asp:ListItem>
                                                        <asp:ListItem>Tropical</asp:ListItem>
                                                        <asp:ListItem>PEB</asp:ListItem>
                                                        <asp:ListItem>GreenWood</asp:ListItem>



                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-3 pading5px">
                                                    <asp:Label ID="lblfrmdate0" runat="server" CssClass="lblTxt lblName">After Days</asp:Label>
                                                    <asp:TextBox ID="txtafterdays" runat="server" CssClass=" inputtextbox" Style="margin-right: 10px; width: 40px; text-align: right;" Text="180"></asp:TextBox>
                                                    <asp:CheckBox ID="chkBonustype" runat="server" CssClass="  btn btn-primary  checkBox"
                                                        Text="EID UL AZHA" />
                                                </div>
                                                <div class="col-md-1 pading5px">
                                                    <asp:CheckBox ID="chkSignatureSheet" runat="server" CssClass=" btn btn-primary  checkBox"
                                                        Text="Signature Sheet" />
                                                </div>


                                            </div>

                                            <%--<table style="width: 100%;">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style36">
                                                        
                                                    </td>
                                                    <td class="style37">
                                                       
                                                    </td>
                                                    <td class="style38">
                                                        <asp:Label ID="lblfrmdate0" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Text="After Days:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style39">
                                                        <asp:TextBox ID="txtafterdays" runat="server" CssClass="txtboxformat"
                                                            Font-Bold="True" Width="40px" Text="90"></asp:TextBox>
                                                    </td>
                                                    <td class="style40">
                                                        
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style36">&nbsp;</td>
                                                    <td class="style37">
                                                       
                                                    </td>
                                                    <td class="style38">&nbsp;</td>
                                                    <td class="style39">&nbsp;</td>
                                                    <td class="style40">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>--%>
                                        </asp:Panel>

                                    </div>
                                </fieldset>

                                <asp:GridView ID="gvBonus" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvBonus_PageIndexChanging"
                                    ShowFooter="True" Width="766px" OnRowDeleting="gvBonus_RowDeleting">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" />


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProNamebon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Section">

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalBonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnTotalBonus_Click">Total</asp:LinkButton>
                                            </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgvSectionbon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Employee Name" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center" Width="110px">Export Excel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvname" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkbonLock" runat="server" CssClass="btn btn-primary checkBox" Text="Lock" Width="90px" />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesig" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkUpBonus" runat="server" Font-Bold="True"
                                                    Font-Size="12px" CssClass="btn  btn-danger primarygrdBtn" OnClick="lnkUpBonus_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBasicb" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbSalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGsalb" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgssalb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgJoinDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration(Month)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDuration" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus(%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgPerBonus" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus Amt.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBonusAmt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBonusAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBonusRemarks" runat="server" Style="text-align: left" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>




                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <%--<asp:Panel ID="PnlBonus" runat="server" BorderColor="Yellow"
                                                BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style35">&nbsp;</td>
                                                        <td class="style30">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Text="Bonus(%):" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style31">
                                                            <asp:TextBox ID="txtBonusPer" runat="server" CssClass="txtboxformat"
                                                                Font-Bold="True" Width="80px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkbtnGenBonus" runat="server" BackColor="#003366"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="#CCCCFF" OnClick="lnkbtnGenBonus_Click">Generate</asp:LinkButton>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="12"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="Payslip" runat="server">
                            </asp:View>
                            <asp:View ID="EmpSignature" runat="server">
                            </asp:View>


                            <asp:View ID="CashPay" runat="server">
                                <asp:GridView ID="gvcashpay" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvcashpay_PageIndexChanging"
                                    ShowFooter="True" Width="745px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProName0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID CARD">

                                            <ItemTemplate>
                                                <asp:Label ID="lgIdCard0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSection0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name &amp; Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvndesig0" runat="server"
                                                    Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Working Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvwday0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wd")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cash Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvothded0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFToCahamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ViewOvertime" runat="server">
                                <asp:GridView ID="gvOvertime" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvOvertime_PageIndexChanging"
                                    ShowFooter="True" Width="420px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcompanyandemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))    %>'
                                                    Width="220px">   </asp:Label>


                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Salary Per Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalpermonth" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Salary Per Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalperhour" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salphour")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtodays" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "today")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtohour" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Dated">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdated" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ovrday"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Payable Amount">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFoallows" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoallow" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oallow")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
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



            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="10">
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Text="Company:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnCompany_Click" Width="16px" />
                                    </td>
                                    <td colspan="7" align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="12px"
                                            OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Height="16px" OnClick="lnkbtnShow_Click"
                                            Style="text-align: center;" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Text="Department:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnProSrch" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnProSrch_Click" Width="16px" />
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="12px"
                                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblComSalLock" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Text="Section:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnSecSrch" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnSecSrch_Click" Width="16px" />
                                    </td>
                                    <td colspan="7">
                                        <asp:DropDownList ID="ddlSection" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSection">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style44">
                                        <asp:TextBox ID="txtfromdate" runat="server" Width="100px"
                                            CssClass="txtboxformat"></asp:TextBox>
                                        
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txttodate" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style38" align="left">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="color: #FFFFFF; text-align: right;" Text="Page Size:"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style41">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Style="margin-left: 0px" Width="100px">
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
                                    </td>
                                    <td class="style41">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="10"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
