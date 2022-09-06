<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpAssessment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RptEmpAssessment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
            <fieldset class="scheduler-border fieldset_A">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company
                                    <asp:LinkButton ID="ibtnFindDepartment" runat="server" OnClick="ibtnFindDepartment_OnClick"><span class="fas fa-search"> </span></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 mt-4">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control chzn-select inputTxt " OnSelectedIndexChanged="ddlCompanyName_OnSelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-3  mt-4">
                        <div class="form-group">

                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary  pull-left" OnClick="lnkbtnShow_OnClick" Text="Ok"></asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-3 col-sm-3 ">
                        <div class="form-group">

                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department
                                     <asp:LinkButton ID="imgbtnDeptSrch" runat="server" OnClick="imgbtnDeptSrch_OnClick"><span class="fas fa-search"> </span></asp:LinkButton>
                            </asp:Label>
                            <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 mt-4 ">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select inputTxt" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" TabIndex="7">
                            </asp:DropDownList>
                            <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False"></asp:Label>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-3 col-sm-3 ">
                        <div class="form-group">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                            <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-3">
                        <div class="form-group">
                            <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                            <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                        </div>
                    </div>
                </div>

            </fieldset>


            <div class="row">

                <asp:GridView ID="gvEmpper" runat="server"
                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                    Width="831px">
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

                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:Label ID="lblgvEmpName" runat="server"
                                    Text=' <%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Department & Designation">
                            <ItemTemplate>
                                <asp:Label ID="lgvSectionadesig" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesig")) %>'
                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mark">
                            <ItemTemplate>
                                <asp:Label ID="lblgvmark11" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0;(#,##0); ") %>'
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
</asp:Content>


