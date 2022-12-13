<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EmpOvertime.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_86_All.EmpOvertime" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            var gvOvertime = $('#<%=this.gvEmpOverTime.ClientID %>');
            var gvBankPay = $('#<%=this.gvBankPay.ClientID %>');
            var gvEmpHoliday = $('#<%=this.gvEmpHoliday.ClientID %>');
            var gvEmpMbill = $('#<%=this.gvEmpMbill.ClientID %>');
            var gvEmpELeave = $('#<%=this.gvEmpELeave.ClientID %>');
            var gvEmpOtherded = $('#<%=this.gvEmpOtherded.ClientID %>');
            var gvEmploan = $('#<%=this.gvEmploan.ClientID %>');
            var gvarrear = $('#<%=this.gvarrear.ClientID %>');
            var gvothearn = $('#<%=this.gvothearn.ClientID %>');


            gvarrear.Scrollable();

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


                // $.keynavigation(gvOvertime);
                $.keynavigation(gvBankPay);
                $.keynavigation(gvEmpHoliday);
                $.keynavigation(gvEmpMbill);
                $.keynavigation(gvEmpELeave);
                $.keynavigation(gvEmpOtherded);
                $.keynavigation(gvEmploan);
                $.keynavigation(gvarrear);
                $.keynavigation(gvothearn);

            });


            gvothearn.Scrollable();
            gvOvertime.Scrollable();
            gvEmpOtherded.Scrollable();
            //  gvEmpMbill.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });
        }


        function otdetails() {
            $('#otdetails').modal('toggle');
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
                <div class="contentPart">
                    <div class="card-header">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-lg-1 col-md-2 col-sm-3">
                                        <div class="form-group">

                                            <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                            <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="form-control inputTxt chzn-select">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview">Company Name</asp:Label>
                                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control inputTxt chzn-select pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server">Department</asp:Label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="7" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-lg-2 col-md-4 col-sm-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                            </asp:DropDownList>


                                        </div>

                                    </div>
                                    <div class="col-lg-2 col-md-4 col-sm-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem Selected="True">300</asp:ListItem>
                                                <asp:ListItem>600</asp:ListItem>
                                                <asp:ListItem>1000</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-lg-1 col-md-4 col-sm-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass=" smLbl">Code
                                          <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" OnClick="imgbtnSearchEmployee_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                            </asp:Label>
                                            <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control inpPixedWidth"></asp:TextBox>


                                        </div>
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="btnUploadovrtime" runat="server" Text="UPload Data" OnClick="btnUploadovrtime_Click" CssClass="btn btn-primary btn-sm" TabIndex="9" Visible="false"></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-lg-2 col-md-4 col-sm-4 mt-4">
                                        <div class="form-group">
                                            <div class="pull-left">
                                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click" Text="Ok"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <asp:Panel ID="pnlDedEarnExcel" runat="server" class="row" Visible="false">
                                    <div class="col-lg-3 col-md-5 col-sm-6">
                                        <div class="form-group">

                                            <div class="input-group">
                                                <asp:FileUpload ID="fileuploadExcel" runat="server" class="form-control btn" onchange="submitform();" />
                                                <span class="input-group-addon bg-primary" style="background: #ff6a00 !important" id="basic-addons1">
                                                    <asp:LinkButton ID="lbtnDedorOtherEernExcelAdjust" runat="server" CssClass="btn" ForeColor="White" ToolTip="Adjust Deduction" OnClick="lbtnDedorOtherEernExcelAdjust_Click">
                                            <i class="fas fa-file-excel"></i>&nbsp;&nbsp;Adjust</asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                            <div class="row">
                                <div class="col-lg-1 col-md-3 col-sm-4">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Visible="false">Company
                                          <asp:LinkButton ID="ibtnFindDepartment" runat="server" OnClick="ibtnFindDepartment_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                        </asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control inpPixedWidth" Visible="false"></asp:TextBox>

                                    </div>
                                </div>


                                <div class="col-lg-2 col-md-3 col-sm-4">
                                    <div class="form-group">

                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Visible="false">Department
                                         <asp:LinkButton ID="imgbtnDeptSrch" runat="server" OnClick="imgbtnDeptSrch_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                        </asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="form-control inpPixedWidth" Visible="false"></asp:TextBox>


                                    </div>
                                </div>

                                <div class="col-lg-3 col-md-4 col-sm-4 mt-4 ">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnlistsaltype" Visible="false" runat="server" CssClass="rbtnList1 margin5px"
                                            Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Management</asp:ListItem>
                                            <asp:ListItem>Non Management</asp:ListItem>
                                            <asp:ListItem>Both</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                                <div class="col-lg-1 col-md-3 col-sm-4 ">
                                    <div class="form-group">

                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Visible="false">Section
                                     <asp:LinkButton ID="imgbtnSecSrch" runat="server" OnClick="imgbtnSecSrch_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                                        </asp:Label>
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="form-control inpPixedWidth" Visible="false"></asp:TextBox>

                                    </div>
                                </div>

                            </div>
                    </div>
                    </fieldset>
                    </div>

                    <div class="card-body">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewOvertime" runat="server">

                                <asp:GridView ID="gvEmpOverTime" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvEmpOverTime_PageIndexChanging" ShowFooter="True" OnRowCommand="gvEmpOverTime_RowCommand"
                                    OnRowDeleting="gvEmpOverTime_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                        <asp:TemplateField HeaderText="Section">
                                            <HeaderTemplate>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                    ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSection" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="55px"></asp:Label>
                                                                       <asp:Label ID="lblempid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lTotal" runat="server" OnClick="lTotal_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name & Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server"
                                                    Text=' <%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdate" runat="server" OnClick="lUpdate_Click" CssClass="btn btn-primary btn-sm">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fixed Hour">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvFixed" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixhour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
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
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling<br/>(7PM-10PM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling<br/>(10:1PM-2AM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling<br/>(2AM-6PM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
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

                                       


                                        <asp:TemplateField HeaderText="Total Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
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

                                         <asp:TemplateField HeaderText="Fixed Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvfixamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>

                                                
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                                      
                                         <asp:TemplateField HeaderText="System Hour" >
                                            <ItemTemplate>
     <%--                                           <asp:Label ID="lblsyshour" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "syshour")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                               <asp:LinkButton runat="server" ID="lnksyshour" OnClick="lnksyshour_Click"></asp:LinkButton>--%>

                                          <asp:Button runat="server" ID="lblsyshour" Width="40px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "syshour")).ToString("#,##0.0;(#,##0.0); ") %>' CommandArgument="H" />
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                               
                                         <asp:TemplateField HeaderText="System Day" >
                                            <ItemTemplate>
               <%--                                 <asp:Label ID="lblsysday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daycount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                               <asp:LinkButton runat="server" ID="lnksysdaycount" OnClick="lnksysdaycount_Click"></asp:LinkButton>--%>

                                          <asp:Button runat="server" ID="lblsysday" Width="40px"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daycount")).ToString("#,##0;(#,##0); ") %>' CommandArgument="D" />
                                      
                                            </ItemTemplate>

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
                                    <div class="row">

                                        <asp:Panel ID="pnlCopy" runat="server" Visible="false">

                                            <div class="col-md-4">
                                                <div class="form-group">

                                                    <asp:Label ID="lblPrevious" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>

                                                    <asp:DropDownList ID="ddlpreyearmon" runat="server" AutoPostBack="True"
                                                        TabIndex="11" CssClass=" form-control chzn-select ddlPage">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="lbtnCopy" runat="server" Text="Copy" OnClick="lbtnCopy_Click" CssClass="btn btn-primary btn-sm" TabIndex="9"></asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-4  ">
                                            <div class="form-group">

                                                <asp:CheckBox ID="chkcopy" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkcopy_CheckedChanged" />

                                            </div>



                                        </div>



                                    </div>
                                </fieldset>
                                <br />
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

                                        <asp:CommandField ShowDeleteButton="True" />
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

                                        <asp:CommandField ShowDeleteButton="True" />
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

                                         <asp:TemplateField HeaderText="Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave Enjoyed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                            <hr />

                            <asp:View ID="ViewOtherDeduction" runat="server">

                                <div class="row">
                                    <div class="col-lg-1 col-md-2">
                                        <div class="form-group">

                                            <asp:CheckBox ID="Chkother" runat="server" TabIndex="10" Text="Copy From" CssClass="btn btn-primary  mt-4 btn-sm checkBox" AutoPostBack="True" OnCheckedChanged="Chkother_CheckedChanged" />

                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 ">
                                        <asp:Panel ID="Pnlother" runat="server" Visible="false">

                                            <div class="form-group row">
                                                <div class="col-lg-4 col-md-4 col-sm-5 ">
                                                    <asp:Label ID="Label5" runat="server" CssClass="d-block" Text="Month:"></asp:Label>
                                                    <asp:DropDownList ID="ddlpreyearmonoth" runat="server"
                                                        TabIndex="11" CssClass="form-control chzn-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12 ">

                                                    <asp:Label ID="lbltype" runat="server" CssClass="d-block" Text="Field : "></asp:Label>
                                                    <asp:CheckBoxList ID="chkfield" runat="server" BackColor="#0B88C5" ForeColor="White" CssClass="btn rbtnList1 margin5px btn-sm primaryBtn "
                                                        RepeatColumns="7" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="000">&nbsp; All</asp:ListItem>
                                                        <asp:ListItem Value="001">&nbsp;Mobile Bill</asp:ListItem>
                                                        <asp:ListItem Value="005">&nbsp;Others</asp:ListItem>

                                                    </asp:CheckBoxList>
                                                </div>
                                                <div class="col-lg-1 col-md-1 col-sm-12 ">
                                                    <asp:LinkButton ID="lblbtncopyoth" runat="server" Text="Copy" OnClick="lblbtncopyoth_Click" CssClass="btn btn-primary btn-sm okBtn mt-4" TabIndex="9"></asp:LinkButton>
                                                </div>

                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <asp:GridView ID="gvEmpOtherded" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging=" gvEmpOtherded_PageIndexChanging"
                                        ShowFooter="True" Width="685px" OnRowDeleting="gvEmpOtherded_RowDeleting" OnRowDataBound="gvEmpOtherded_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" DeleteText="" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSection" runat="server" Font-Bold="true" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <asp:HyperLink ID="hlbtntbCdataExeldeduct" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectiondectuc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="160px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalOtherDed" runat="server" CssClass="btn btn-info btn-sm" OnClick="lbtnTotalOtherDed_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbntUpdateOtherDed_Click">Update</asp:LinkButton>
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

                                            <asp:TemplateField HeaderText=" Advanced deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsaladv" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saladv")).ToString("#,##0;(#,##0); ")%>'
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

                                            <asp:TemplateField HeaderText="Mobile bill ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtmbill" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotermbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotherded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fine Deduc. Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvfineDeduction" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fine")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvfinededuction" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fine Deduc. Days">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvfineDeducdays" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "finedays")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvfinededucdays" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cash Deduc.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvCashDeduc" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cashded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbFlgvCashDeduc" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amt.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Status">
                                                <FooterTemplate>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <%--    <asp:TextBox ID="txtgvpaystusus" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right" BorderColor="#660033" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paystatus")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>--%>

                                                    <asp:DropDownList ID="ddlPaystatus" runat="server" CssClass="chzn-select form-control" Width="90px">
                                                        <asp:ListItem Value="0"> Bank </asp:ListItem>
                                                        <asp:ListItem Value="1"> Cash </asp:ListItem>
                                                        <asp:ListItem Value="2">Both</asp:ListItem>



                                                    </asp:DropDownList>

                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaystatus" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right" BorderColor="#660033" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paystatus")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpaystusus" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <%--                                     <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkCash" runat="server"
                                                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkcash"))=="True" %>'
                                                                                        Width="60px" />
                                                                                </ItemTemplate>
                                            
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True" 
                                                                                        OnCheckedChanged="chkAllfrm_CheckedChanged" Text="Cash Salary" Width="60px" />
                                                                                </HeaderTemplate>
                                                                            </asp:TemplateField>--%>



                                            <asp:TemplateField HeaderText="Gross Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgssal" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right" BorderColor="#660033" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px" Font-Size="11px"></asp:Label>
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

                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="row">
                                        <div class="col-lg-1 col-md-2 col-sm-2 mt-2">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkarrearcopy" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkarrearcopy_CheckedChanged" />


                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-3 col-sm-3 ">
                                            <div class="form-group">
                                                <asp:Panel ID="Pnlarrer" runat="server" Visible="false">


                                                    <asp:Label ID="Label8" runat="server" CssClass=" smLbl_to" Text="Month:"></asp:Label>
                                                    <asp:DropDownList ID="ddlprearrear" runat="server" CssClass="form-control chzn-select " AutoPostBack="True"
                                                        TabIndex="11">
                                                    </asp:DropDownList>
                                                    <div class="colMdbtn pading5px mt-2">
                                                        <asp:LinkButton ID="lnkarrearcopy" runat="server" Text="Copy" OnClick="lnkarrearcopy_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>
                                                </asp:Panel>
                                            </div>
                                        </div>


                                    </div>
                                </fieldset>
                                <hr />
                                <div class="row">
                                    <asp:GridView ID="gvarrear" runat="server" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvarrear_PageIndexChanging" ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                            <asp:CommandField ShowDeleteButton="True" />
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


                                                    <table style="width: 300px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Section" Width="180px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center">Export Excel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnCalArrear" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" OnClick="lbtnCalArrear_Click"
                                                        Style="text-decaration: none;">Calculation</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionarrear" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalArrear" runat="server" CssClass="btn btn-info btn-sm" OnClick="lbtnTotalArrear_Click">Total</asp:LinkButton>
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
                                                    <asp:LinkButton ID="lbntUpdateArrear" runat="server" OnClick="lbntUpdateArrear_Click" CssClass="btn btn-primary btn-sm">Update</asp:LinkButton>
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

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCasharrear" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkcash"))=="True" %>'
                                                        Width="60px" />
                                                </ItemTemplate>

                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllArrearfrm" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkAllArrearfrm_CheckedChanged" Text="Cash Salary" Width="60px" />
                                                </HeaderTemplate>
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

                            <asp:View ID="ViewOtherEarn" runat="server">

                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="row">
                                        <div class=" col-lg-1 col-md-2 col-sm-2">
                                            <div class="form-group">

                                                <asp:CheckBox ID="ChkEarn" runat="server" TabIndex="10" Text="Copy " CssClass="btn btn-primary checkBox  btn-sm mt-4" AutoPostBack="True" OnCheckedChanged="ChkEarn_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-9 col-md-9 col-sm-9  ">
                                            <asp:Panel ID="PnlEarn" runat="server" Visible="false">

                                                <div class="form-group row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12  ">

                                                        <asp:Label ID="Label6" runat="server" CssClass=" d-block" Text="Month:"></asp:Label>
                                                        <asp:DropDownList ID="ddlPremEarn" runat="server" AutoPostBack="True"
                                                            TabIndex="11" CssClass=" form-control chzn-select ddlPage">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-12  ">

                                                        <asp:Label ID="Label9" runat="server" CssClass="d-block" Text="Field"></asp:Label>
                                                        <asp:CheckBoxList ID="chkotherearn" runat="server" BackColor="#0B88C5" ForeColor="White" CssClass="checkbox "
                                                            RepeatColumns="10">
                                                            <asp:ListItem Value="000">&nbsp;All</asp:ListItem>
                                                            <asp:ListItem Value="001">&nbsp;Earned Leave</asp:ListItem>
                                                            <asp:ListItem Value="003">&nbsp;Arear Salary</asp:ListItem>
                                                            <asp:ListItem Value="005">&nbsp;Project Visit</asp:ListItem>
                                                            <%--          <asp:ListItem Value="007">Car Allowance</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="008">Fooding</asp:ListItem>--%>
                                                            <asp:ListItem Value="009">&nbsp;Refund</asp:ListItem>
                                                            <asp:ListItem Value="012">&nbsp;Others</asp:ListItem>
                                                            <%--<asp:ListItem Value="015">Dress Bill</asp:ListItem>--%>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div class="col-lg-1 col-md-1 col-sm-12  ">


                                                        <asp:LinkButton ID="btnCopyEarn" runat="server" Text="Copy" OnClick="btnCopyEarn_Click" CssClass="btn btn-primary btn-sm mt-4 okBtn" TabIndex="9"></asp:LinkButton>

                                                    </div>

                                                </div>
                                            </asp:Panel>

                                        </div>

                                    </div>
                                </fieldset>
                                <hr />
                                <div class="row">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvothearn" runat="server" AllowPaging="True"
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
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblSection" runat="server" Font-Bold="True"
                                                            Text="Section" Width="80px"></asp:Label>

                                                        <asp:HyperLink ID="hlbtntOtherEarnExcel" runat="server"
                                                            CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>X</asp:HyperLink>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSectionearn" runat="server" Font-Bold="true"
                                                            Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Card #">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol0" runat="server" Text="Card #" />
                                                        <asp:CheckBox ID="chkCol0" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol1" runat="server" Text="Employee Name &amp; Designation" />
                                                        <asp:CheckBox ID="chkCol1" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol2" runat="server" Text="Fuel" />
                                                        <asp:CheckBox ID="chkCol2" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol3" runat="server" Text="KPI" />
                                                        <asp:CheckBox ID="chkCol3" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol4" runat="server" Text="Per. Bonus" />
                                                        <asp:CheckBox ID="chkCol4" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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
                                                <asp:TemplateField HeaderText="Hair Cut.">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol5" runat="server" Text="Hair Cut." />
                                                        <asp:CheckBox ID="chkCol5" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol6" runat="server" Text="Fooding" />
                                                        <asp:CheckBox ID="chkCol6" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol7" runat="server" Text="Food Taken <br>Day" />
                                                        <asp:CheckBox ID="chkCol7" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol8" runat="server" Text="Night Fooding" />
                                                        <asp:CheckBox ID="chkCol8" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol9" runat="server" Text="Others" />
                                                        <asp:CheckBox ID="chkCol9" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
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

                                                <asp:TemplateField HeaderText="HardShip Allowance">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol10" runat="server" Text="HardShip Allowance" />
                                                        <asp:CheckBox ID="chkCol10" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvhardship" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hardship")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFhardship" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol11" runat="server" Text="Trip days" />
                                                        <asp:CheckBox ID="chkCol11" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvtripday" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tripday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtripday" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCol12" runat="server" Text="Trip Amt" />
                                                        <asp:CheckBox ID="chkCol12" runat="server" Checked="true" Class="hidden" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvtripamt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tripal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtripamt" runat="server" Font-Bold="True" Font-Size="12px"
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

                                                <asp:TemplateField HeaderText="Gross Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblgssals" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                        <%--  <asp:Button ID="btnExportOtherEarnExcel" runat="server" Text="Export To Excel" OnClick="btnExportOtherEarnExcel_Click" CssClass="btn btn-success btn-xs" />--%>
                                    </div>
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
                                                    BorderStyle="None" Font-Size="11px"
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

                            <asp:View ID="ViewSalaryDeduction" runat="server">
                                <asp:GridView ID="gvsalreduction" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvsalreduction_PageIndexChanging" ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Height="200px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNosred" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgProNamesred" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="140px" Font-Bold="True"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <asp:LinkButton ID="lbtnTotalsred" runat="server" OnClick="lbtnTotalsred_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>



                                                <asp:LinkButton ID="lbtnRound" runat="server" OnClick="lbtnRound_Click" Style="float: right;" CssClass="btn btn-primary primarygrdBtn">Round</asp:LinkButton>

                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnPutSameValue" runat="server" Font-Bold="True" Font-Underline="true"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lbtnPutSameValue_Click" CssClass="btn btn-primary primarygrdBtn">Put Same Value</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSectionsred" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name &amp; Designation">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkFiUpdatesred" runat="server" OnClick="lnkFiUpdatesred_Click" CssClass="btn  btn-danger primarygrdBtn">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvndesigsred" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCardNosred" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpreamt" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpresal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reduction %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvredcpercnt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                    BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "redpercnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reduction Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvredamt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                    BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "redamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFredamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
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
                            </asp:View>

                            <asp:View ID="ViewAddiBonus" runat="server">


                                <asp:GridView ID="GvAddiBonus" runat="server" AutoGenerateColumns="False"
                                    OnPageIndexChanging="GvAddiBonus_PageIndexChanging" ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDeleting="GvAddiBonus_RowDeleting" OnRowDataBound="GvAddiBonus_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpIdadd" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <HeaderTemplate>


                                                <table style="width: 300px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Section" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExeladd" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center">Export Excel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSectionAddition" runat="server" Font-Bold="true" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Card #">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalAddi" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalAddi_Click">Total</asp:LinkButton>
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
                                                <asp:LinkButton ID="lbntUpdateAddition" runat="server" OnClick="lbntUpdateAddition_Click" CssClass="btn btn-success primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtaddbonus" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAddibonus" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaystatusaddi" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right" BorderColor="#660033" BorderWidth="1px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chkcash")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpaystusus" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Status">
                                            <FooterTemplate>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <%--    <asp:TextBox ID="txtgvpaystusus" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right" BorderColor="#660033" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paystatus")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" Font-Size="11px"></asp:TextBox>--%>

                                                <asp:DropDownList ID="ddlPaystatusaddi" runat="server" CssClass="chzn-select form-control" Width="90px">
                                                    <asp:ListItem Value="0"> Bank </asp:ListItem>
                                                    <asp:ListItem Value="1"> Cash </asp:ListItem>
                                                </asp:DropDownList>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
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

                        </asp:MultiView>
                    </div>
                </div>
            </div>


        <div class="modal fade" id="otdetails" tabindex="-1" role="dialog" aria-labelledby="NoticeModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document" >
                <div class="modal-content">
                    <div class="modal-header order-bottom">
                        <h6 class="modal-title font-weight-bold" id="">Overtime Details</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">
                            <div class="card-header bg-info ">
                                <h6 class="font-weight-bold text-white" id="modalNoticeTitle" runat="server"></h6>
                            </div>
                            <div class="card-body bg-light">
                                <div class="card-body bg-light">
                                <div class="table-responsive pb-3">
                                    <asp:GridView ID="gvotDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Font-Bold="True"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID Card" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lblempid"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lbldate"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="In Time">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lblintime"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Out Time">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lblintime"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            
                                                 <asp:TemplateField HeaderText="Total Hour">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lblttlhour"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttlhour")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>

                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

