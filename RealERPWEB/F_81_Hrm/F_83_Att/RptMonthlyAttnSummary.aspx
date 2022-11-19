<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMonthlyAttnSummary.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptMonthlyAttnSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
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
            <div class="card card-fluid mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblCompany" runat="server" CssClass="label">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divFrmDate" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divToDate" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblToDate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="viewAttnSumEmpWise" runat="server">
                            <asp:GridView ID="gvAttnSumEmpWise" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvAttnSumEmpWise_PageIndexChanging"
                                AutoGenerateColumns="False"
                                ShowFooter="True" Width="800px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCompanyname" runat="server"
                                                Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))  +"<B>"  %>' Width="120px">                                                                       
                                              
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section & Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptandemployee" runat="server"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                Width="150px"> 
                                              
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblgvempid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="80px" Visible="false"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignation" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgrade" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grade")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Join Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvjoindate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgrade" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gender")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpresent" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monthdays")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Holiday">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvweekhday" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "holiday")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Working Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvwrkday" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkday")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Present">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpresent" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Absent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvabsent" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ab")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "latecount")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>                                 
                                   
                                    <asp:TemplateField HeaderText="LWP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlwpday" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpl")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvleave" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>                                    

                                    <asp:TemplateField HeaderText="Pay Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpaydays" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paydays")).ToString("#,##0;(#,##0);") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
