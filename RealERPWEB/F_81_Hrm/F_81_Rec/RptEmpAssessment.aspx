<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpAssessment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RptEmpAssessment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindDepartment" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindDepartment_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_OnSelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>
                                <div class="pull-left">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_OnClick" Text="Ok"></asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" TabIndex="7">
                                </asp:DropDownList>
                                <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False" Width="233"></asp:Label>
                            </div>

                        </div>

                        <div class="form-group">

                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                <asp:TextBox ID="txtfrmDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-md-3 asitCol2 pading5px">
                                <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>

                        </div>
                    </div>
                </fieldset>
            </div>

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


