<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPerAppraisal.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.RptPerAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">




</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        <fieldset class="scheduler-border fieldset_A">
            <div class="card-header">
                <div class="row">
                    <div class=" col-lg-1 col-md-2 col-sm-3">
                        <div class="form-group">

                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company
                                     <asp:LinkButton ID="ibtnFindDepartment" runat="server" OnClick="ibtnFindDepartment_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 col-sm-3 mt-4 ">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control chzn-select pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False"></asp:Label>

                        </div>
                    </div>
                    <div class="col-lg-1 col-md-3 col-sm-6 mt-4 ">
                        <div class="form-group">
                            <div class="pull-left">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click" Text="Ok"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3">
                        <div class="form-group">

                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department
                                     <asp:LinkButton ID="imgbtnDeptSrch" runat="server" OnClick="imgbtnDeptSrch_Click"><span class="fas fa-search"> </span></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-4 col-sm-3 mt-4">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" TabIndex="7">
                            </asp:DropDownList>
                            <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False"></asp:Label>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class=" col-lg-1 col-md-3 col-sm-3 ">
                        <div class="form-group">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                            <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-3 col-sm-3 ">
                        <div class="form-group">
                            <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                            <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                        </div>
                    </div>
                    <div class="col-lg-1 col-md-2 col-sm-3 mt-4">
                        <div class="form-group">
                            <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass="rbtnList1 margin5px"
                                Font-Size="14px" Height="16px" RepeatColumns="14" RepeatDirection="Horizontal"
                                Width="380px" Visible="true">
                                <asp:ListItem Selected="True">&nbsp; Management</asp:ListItem>
                                <asp:ListItem> &nbsp;Non Management</asp:ListItem>
                                <asp:ListItem>&nbsp;Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>


        <div class="card-body">

            <asp:GridView ID="gvEmpper" runat="server"
                AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                Width="831px" OnRowDataBound="gvEmpper_RowDataBound">
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

                    <asp:TemplateField HeaderText="Employee Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpode" runat="server"
                                Text=' <%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="HLgvDesc" runat="server"
                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                Width="150px"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <%--  <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Hyperlink ID="lblgvEmpName" runat="server"
                                            Text=' <%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Hyperlink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>



                    <asp:TemplateField HeaderText="Department & Designation">
                        <ItemTemplate>
                            <asp:Label ID="lgvSectionadesig" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secadesig")) %>'
                                Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark1" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark1")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark2" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark2")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark3" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark3")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark4" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark4")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark5" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark5")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark6" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark6")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark7" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark7")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark8" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark8")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark9" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark9")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark10" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark10")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark11" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark11")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblgvmark12" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark12")).ToString("#,##0;(#,##0); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Average">
                        <ItemTemplate>
                            <asp:Label ID="lblgvaverage" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Postion">
                        <ItemTemplate>
                            <asp:Label ID="lblposition" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "position")) %>'
                                Width="55px"></asp:Label>
                        </ItemTemplate>
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

