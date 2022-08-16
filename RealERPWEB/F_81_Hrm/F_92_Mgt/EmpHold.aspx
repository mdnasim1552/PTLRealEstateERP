<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpHold.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpHold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        select#ContentPlaceHolder1_ddlEmployee{
              width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlDepartment_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlSection_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlCompanyName_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }
        div#ContentPlaceHolder1_ddlCompanyName_chzn{
            width: 100% !important;
        }
        .pd4{
            padding:4px!important;
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
                            <asp:Label ID="Label2" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>

                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="7">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-lg-2 col-md-1 col-sm-6">
                            <asp:Label ID="Label4" runat="server">Month</asp:Label>

                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm mt20">Ok</asp:LinkButton>

                        </div>
                    </div>


                    <asp:Panel ID="PnlSub" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-6">
                               
                                <asp:Label ID="lblEmployee" runat="server">Employee 

                                     <asp:LinkButton ID="imgbtnEmployee" runat="server" OnClick="imgbtnEmployee_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" Width="233" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-1 col-md-1 col-sm-6">
                                <asp:Label ID="lblfrmDate" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>


                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-6">
                                <asp:Label ID="lbltoDate" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control  form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>
                            <div class="col-lg-1 col-md-3 col-sm-6">
                                <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_Click" CssClass="btn btn-success btn-sm mt20"><i class="fa fa-plus"></i> Add</asp:LinkButton>
                            </div>

                        </div>
                    </asp:Panel>

                </div>
                <div class="card-body">
                    <asp:GridView ID="gvemphold" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True"   OnRowDeleting="gvemphold_RowDeleting">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.">
                        <ItemTemplate>
                            <asp:Label ID="SLNO" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="True" />

                    <asp:TemplateField HeaderText="Card">
                        <ItemTemplate>
                            <asp:Label ID="lgvidcardno" runat="server" Font-Size="11PX"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvempname" runat="server" Font-Size="11PX"
                                Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                 ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-success btn-sm primaryBtn" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                        </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                 ></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <ItemTemplate>
                            <asp:TextBox ID="lblgvfrmdate" runat="server" Font-Size="11PX"
                                Style="text-align: left"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "frmdate")).ToString("dd-MMM-yyyy") %>'
                                Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                            <cc1:CalendarExtender ID="lblgvfrmdate_CalendarExtender" runat="server"
                                Enabled="True" TargetControlID="lblgvfrmdate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="To">
                        <ItemTemplate>
                            <asp:TextBox ID="lblgvtodate" runat="server" Font-Size="11PX"
                                Style="text-align: left"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todate")).ToString("dd-MMM-yyyy") %>'
                                Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                            <cc1:CalendarExtender ID="lblgvtodate_CalendarExtender" runat="server"
                                Enabled="True" TargetControlID="lblgvtodate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
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

            <%--              <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="dataLblview pull-left" Visible="False" Width="233"></asp:Label>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="233" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblDeptDesc" runat="server" CssClass="dataLblview" Visible="False" Width="233"></asp:Label>
                                    </div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSecion" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSecion" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblSectionDesc" runat="server" CssClass="dataLblview" Visible="False" Width="233"></asp:Label>
                                    </div>


                                </div>


                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </fieldset>--%>

     
                <%--       <asp:Panel ID="PnlSub" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblEmployee" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                            <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol4">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                            </asp:DropDownList>

                                            <div class="pull-left">
                                                <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_Click" CssClass="btn btn-primary okBtn">Add</asp:LinkButton>
                                            </div>
                                        </div>


                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="lblfrmDate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                            <asp:TextBox ID="txtfrmDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                            <asp:Label ID="lbltoDate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                            <asp:TextBox ID="txttoDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </fieldset>



                        </asp:Panel>--%>
  


            






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


