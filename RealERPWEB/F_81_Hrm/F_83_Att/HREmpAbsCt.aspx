﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HREmpAbsCt.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HREmpAbsCt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   


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
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                     <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" Width="336px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyNameAgg" runat="server" Style="border: none; line-height: 1.5" CssClass="form-control dataLblview" Height="22" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbldeptnameagg" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                            <asp:TextBox ID="txtsrchdeptagg" runat="server" CssClass="inputTxt inputName inpPixedWidth" ></asp:TextBox>
                                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtndeptagg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt"  AutoPostBack="true" Width="336px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblvaldeptagg" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>

                                         <div class="col-md-2">
                                        <asp:Label ID="lmsg11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>



                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2" Width="336px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3 pull-right">
                                        </div>

                                    </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblpreAdv" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                         <asp:DropDownList ID="ddlEmpName" runat="server" Width="336" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged"  AutoPostBack="true" >
                                        <%--<asp:DropDownList ID="ddlEmpName" runat="server"  CssClass="chzn-select form-control inputTxt pull-left" TabIndex="2" Width="336px">--%>
                                        </asp:DropDownList>
                                        
                                    </div>
                                 <%--   <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>--%>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>

                                    </div>
                                </div>


                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <asp:CheckBoxList ID="chkDate" runat="server" Font-Bold="True"  CssClass="chkBoxControl"
                            ForeColor="#000" RepeatDirection="Horizontal" Width="900px"
                            RepeatColumns="7">
                        </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
    
    

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Emp.  Name</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" Width="233" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>                                      
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                          <asp:Label ID="lblCompany" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Section</asp:Label>                                      
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                          <asp:Label ID="lblSection" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>                                      
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                          <asp:Label ID="lblDesignation" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                      <div class="col-md-2">
                                        <asp:Label ID="lmsg11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <asp:CheckBoxList ID="chkDate" runat="server" Font-Bold="True"  CssClass="chkBoxControl"
                            ForeColor="#000" RepeatDirection="Horizontal" Width="900px"
                            RepeatColumns="7">
                        </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

