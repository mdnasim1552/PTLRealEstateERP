<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPaymentAndProvidentinf.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.RptPaymentAndProvidentinf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>

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

//           var gvpaysal = $('#<%=this.gvpayroll.ClientID %>');
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
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="s" runat="server" CssClass="btn btn-info primaryBtn " Text="Please wait . . . . . . ."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>

                                    <div class="col-md-2 pull-right">
                                        <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Next</a>


                                        <%--<asp:HyperLink ID="hlnextEntry" Visible="false" class="btn btn-info primaryBtn margin5px" runat="server" NavigateUrl="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Entry")%>">Next</asp:HyperLink>--%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSecSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSection">
                                        </cc1:ListSearchExtender>
                                        <%--                                        <asp:Label ID="lblSectionDesc" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>--%>
                                    </div>

                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to ">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <div class="col-md-2">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                               
                                <asp:GridView ID="gvprovident" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False"  OnPageIndexChanging="gvprovident_PageIndexChanging"
                                    ShowFooter="True" Width="831px">
                                    <PagerSettings Position="Top" />
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
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="ID CARD">
                                            <ItemTemplate>
                                                <asp:Label ID="lgIdCard" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempname" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                          
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDesig" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                          
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvjoining" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFhrent" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCon" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPsal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                  
                                        <asp:TemplateField HeaderText="P.F Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvarsal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
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
                </div>
            </div>



            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

