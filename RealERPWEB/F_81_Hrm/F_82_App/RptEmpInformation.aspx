﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpInformation.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.RptEmpInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
     <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            try {

                var gridViewScroll = new GridViewScroll({
                    elementID: "gvempDyInfo",
                    width: 1400,
                    height: 500,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });
                 
                gridViewScroll.enhance();
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }
        }

         

    </script>


    <style>
        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 58px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
    </style>



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
                <div class="contentPart">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewServices" runat="server">

                            <asp:Panel ID="Panel1" runat="server">
                                <div class="card-header">
                                    <div class="row mt-2">
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">

                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee List 
                                                    <asp:LinkButton ID="ibtnEmpList" runat="server" OnClick="ibtnEmpList_Click"><span class="fas fa-search"> </span></asp:LinkButton></asp:Label>
                                                <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="form-control  d-done" Visible="false"></asp:TextBox>
                                                <%--//<asp:LinkButton ID="" runat="server"  OnClick="ibtnEmpList_Click"><span class="fas fa-search"> </span></asp:LinkButton>--%>
                                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control chzn-select" TabIndex="2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" runat="server" CssClass="d-block"> Date</asp:Label>
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-1 col-md-2 col-sm-6 mt-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="card-body">
                                <div class="row table-responsive">

                                    <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="678px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Increment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
                        </asp:View>

                        <asp:View ID="ViewEmpInformation" runat="server">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="row">

                                    <%--                <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompanyAgg" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindCompanyAgg" runat="server" CssClass="btn btn-primary btn-sm" OnClick="ibtnFindCompanyAgg_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                        </div>
                                    </div>--%>

                                    <div class="col-lg-3">

                                        <asp:Label ID="Label6" runat="server">Company

                                        <asp:LinkButton ID="ibtnFindCompanyAgg" runat="server" CssClass="" OnClick="ibtnFindCompanyAgg_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                        </asp:Label>

                                        <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select float-left" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>





                                    </div>

                                    <div class="col-lg-3" runat="server" id="comlist" visible="False">
                                        <div class="form-group">
                                            <asp:Label runat="server">Companies</asp:Label>
                                            <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" CssClass="chzn-select" OnSelectedIndexChanged="ddlComName_OnSelectedIndexChanged" AutoPostBack="True" runat="server" TabIndex="2" Width="224">
                                            </asp:DropDownList>

                                        </div>
                                    </div>

                                    <%--                  <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbldeptnameagg" runat="server">Department</asp:Label>
                                            <asp:TextBox ID="txtsrchdeptagg" runat="server" CssClass="form-control form-control-sm" OnTextChanged="txtsrchdeptagg_TextChanged"></asp:TextBox>
                                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtndeptagg_Click"><i class="fa fa-search"> </i><</asp:LinkButton>

                                        </div>
                                    </div>--%>

                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbldeptnameagg" runat="server">Department
                                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="" OnClick="lbtndeptagg_Click"><i class="fa fa-search"> </i></asp:LinkButton>

                                            </asp:Label>

                                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass=" chzn-select float-left" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblvaldeptagg" runat="server" CssClass="form-control form-control-sm" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>

                                        </div>
                                    </div>

                                    <%--                <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblsection" runat="server">Section</asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary btn-sm" OnClick="ibtnFindProject_Click"><i class="fa fa-search"> </i>< </span></asp:LinkButton>

                                        </div>
                                    </div>--%>


                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblsection" runat="server">Section
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="" OnClick="ibtnFindProject_Click"><i class="fa fa-search"> </i> </span></asp:LinkButton>

                                            </asp:Label>

                                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt chzn-select" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server">Employee List
                                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="" OnClick="ibtnEmpListAllinfo_Click"><i class="fa fa-search"> </i></asp:LinkButton>

                                            </asp:Label>
                                            <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="form-control form-control-sm d-none"></asp:TextBox>
                                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control  chzn-select" TabIndex="2" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>

                        </asp:View>
                        <asp:View ID="EmpDynamicInfo" runat="server">

                            <div class="row">
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:Panel ID="Panel6" runat="server">
                                        <fieldset class="scheduler-border fieldset_A">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company
                                                        <asp:LinkButton ID="imgbtnCompany" runat="server" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </asp:Label>
                                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>


                                                </div>

                                            </div>
                                        </fieldset>


                                    </asp:Panel>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Label ID="Label12" runat="server" CssClass="btn btn-success primaryBtn" Style="margin-right: 20px;" Text="Field Information:"></asp:Label>
                                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px" OnCheckedChanged="chkall_CheckedChanged" Text="Check All" />

                                        </div>
                                        <div class="col-md-12">
                                            <asp:CheckBoxList ID="cblEmployee" runat="server" AutoPostBack="True"
                                                CellPadding="2" CssClass="rbtnList1 chkBoxControl margin5px"
                                                Width="100%"
                                                ForeColor="#000" Height="12px"
                                                OnSelectedIndexChanged="cblEmployee_SelectedIndexChanged" RepeatColumns="10"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>aa</asp:ListItem>
                                                <asp:ListItem>bb</asp:ListItem>
                                                <asp:ListItem>bb</asp:ListItem>
                                                <asp:ListItem>bb</asp:ListItem>
                                                <asp:ListItem>cc</asp:ListItem>
                                                <asp:ListItem>dd</asp:ListItem>
                                                <asp:ListItem>ee</asp:ListItem>
                                                <asp:ListItem>ff</asp:ListItem>
                                                <asp:ListItem>gg</asp:ListItem>
                                                <asp:ListItem>hh</asp:ListItem>

                                            </asp:CheckBoxList>

                                        </div>

                                    </div>


                                </asp:Panel>
                            </div>



                            <div class="row">
                                <asp:Label ID="lblSearchlist" runat="server" CssClass="d-block" Text="Search List"></asp:Label>

                                <div class="col-md-3 col-lg-3 col-sm-12">
                                    <div class="from-group mb-1">
                                        <asp:DropDownList ID="ddlFieldList1" runat="server" CssClass="form-control  chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="from-group  mb-1">
                                        <asp:DropDownList ID="ddlFieldList2" runat="server" OnSelectedIndexChanged="ddlFieldList2_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="from-group  mb-1">
                                        <asp:DropDownList ID="ddlFieldList3" runat="server" OnSelectedIndexChanged="ddlFieldList3_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-lg-2 col-sm-12">
                                    <div class="from-group  mb-1">

                                        <asp:DropDownList ID="ddlSrch1" runat="server" CssClass="form-control  chzn-select">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="from-group  mb-1">

                                        <asp:DropDownList ID="ddlSrch2" runat="server" OnSelectedIndexChanged="ddlSrch2_SelectedIndexChanged" Width="90px" CssClass="form-control  chzn-select">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="from-group  mb-1">
                                        <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSrch3_SelectedIndexChanged" CssClass="form-control  chzn-select">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12">

                                    <asp:Label ID="lbland1" runat="server" CssClass="d-block mb-1" Text="And" Visible="False"
                                        Width="25px"></asp:Label>


                                    <asp:TextBox ID="txttoSearch1" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>

                                    <asp:DropDownList ID="ddltodesig1" runat="server" CssClass="form-control  chzn-select mb-1">
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="form-control  chzn-select mb-1">
                                        <asp:ListItem Value="and">And</asp:ListItem>
                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                    </asp:DropDownList>


                                    <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>
                                    <asp:DropDownList ID="ddldesig01" runat="server" CssClass="form-control  chzn-select mb-1">
                                        <asp:ListItem Value="and">And</asp:ListItem>
                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="clearfix"></div>

                                    <asp:TextBox ID="txtSearch2" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>
                                    <asp:DropDownList ID="ddldesig02" runat="server" CssClass="form-control  chzn-select mb-1">
                                    </asp:DropDownList>

                                    <asp:Label ID="lbland2" runat="server" Text="And" Visible="False"
                                        CssClass="lblTxt lblName mb-1"></asp:Label>

                                    <asp:TextBox ID="txttoSearch2" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>

                                    <asp:DropDownList ID="ddltodesig2" runat="server" CssClass="form-control  chzn-select mb-1">
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="form-control  chzn-select mb-1">
                                        <asp:ListItem Value="and">And</asp:ListItem>
                                        <asp:ListItem Value="or">Or</asp:ListItem>
                                    </asp:DropDownList>



                                    <asp:Label ID="lbland3" runat="server" Text="And" Visible="False"
                                        CssClass="lblTxt lblName"></asp:Label>

                                    <asp:TextBox ID="txttoSearch3" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>

                                    <asp:DropDownList ID="ddltodesig3" runat="server" CssClass="form-control  chzn-select">
                                    </asp:DropDownList>
                                     

                                    <asp:TextBox ID="txtSearch3" runat="server" CssClass="form-control form-control-sm mb-1"></asp:TextBox>
                                    <asp:DropDownList ID="ddldesig03" runat="server" CssClass="form-control  chzn-select mb-1">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-md-2 col-lg-2 col-sm-12">
                                    <asp:Label ID="lblOrderList" runat="server" CssClass=" d-block"
                                        Text=""></asp:Label>
                                    <table CssClass="table-striped table-hover table-bordered">
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="form-control  chzn-select">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                    CssClass="form-control  chzn-select">
                                                    <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                    <asp:ListItem Value="desc">Des</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="form-control  chzn-select">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                    CssClass="form-control  chzn-select">
                                                    <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                    <asp:ListItem Value="desc">Des</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="style115">
                                                <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="form-control  chzn-select">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style116">
                                                <asp:DropDownList ID="ddlOrderad3" runat="server" CssClass="form-control  chzn-select">
                                                    <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                    <asp:ListItem Value="desc">Des</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>


                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-1 col-lg-1 col-sm-12">
                                    <asp:LinkButton ID="lbtnEmpDyInfo" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnEmpDyInfo_Click">Ok</asp:LinkButton>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label3U" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                                <div class="col-md-2 col-lg-2 col-sm-12">

                                    <asp:Label ID="lblPage" runat="server" CssClass="d-block"
                                        Text="Page size:" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control" Visible="False">
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
                            </div>



                            <asp:GridView ID="gvempDyInfo" runat="server" PageSize="15"
                                ShowFooter="True" ClientIDMode="Static" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvComCode" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBrnCode" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brcode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Company Name">
                                        <HeaderTemplate>
                                            <table style="width: 47%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Company Name"
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCBdataExel" runat="server" CssClass="btn btn-success btn-sm primaryBtn">X</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvComName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdesc")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBrnName" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSecID" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSec" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Card">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCard" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Emp Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvEname" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="110px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDesID" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigid")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDes" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Joining Date">

                                        <ItemTemplate>
                                            <asp:Label ID="lgvjDate" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindate1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Birth Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBDate" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "birthdate")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Blood Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBlg" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bloodgroup")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsal" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salpay")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsal2" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grspay")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsal3" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salary")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdistrict" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "district"))%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spouse Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvspouse" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spouse"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgrade" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grade"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Present Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaddress" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress"))%>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="National ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvnidno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nidno"))%>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No of children">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvnochil" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nochild"))%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmobile" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobileno"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Pabx">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmobile" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pabx"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmobile" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

