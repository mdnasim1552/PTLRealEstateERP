﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LetterDefaultOld.aspx.cs" Inherits="MFGWEB.LetterDefault" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

 
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.keynavigation.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
   <script language="javascript" type="text/javascript" src="../../tinymce/tinymce.min.js"></script> --%>

    <link href="Content/tinymcc.css" rel="stylesheet" />


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            tinymce.init({
                selector: 'textarea',
                height: 500,
                plugins: [
                      "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
         "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
         "save table contextmenu directionality template paste textcolor"
                ],
                fontsize_formats: "8pt 9pt 10pt 11pt 12pt 26pt 36pt",
                toolbar: "insertfile undo redo | fontsizeselect | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor",
                imagetools_cors_hosts: ['www.tinymce.com', 'codepen.io'],
                forced_root_block: false,
                convert_urls: false,
                relative_urls : false,
                remove_script_host : false,
                document_base_url: false,

                content_css: [
                  '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
                  '//www.tinymce.com/css/codepen.min.css',
                  'content/tinymcc.css'
                
                ]

            });

            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>

    <style>
        label {
            display: inline-block;
            font-weight: 400;
            margin-left: 3px;
            margin-top: -63px;
            max-width: 100%;
        }
    </style>


    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                                <asp:Label ID="lblmsg1" runat="server" CssClass=" btn  btn-success primaryBtn pull-right" Visible="false"></asp:Label>
                            </div></div></fieldset>
        <div class="contentPart">
            <asp:Panel runat="server" ID="panl1">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-8 col-md-8 col-lg-8 form-inline">
                                <asp:Label ID="lbltodate" runat="server" CssClass=" lblName lblTxt">Date</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                <asp:Label ID="lblDept" runat="server" CssClass=" smLbl_to">Department</asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="100" CssClass="form-control inputTxt pull-left" TabIndex="6">
                                </asp:DropDownList>

                                <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>

                                <asp:DropDownList ID="ddlSection" runat="server" Width="100" CssClass="form-control inputTxt pull-left" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Label ID="lblpreAdv" runat="server" CssClass=" smLbl_to">Employee</asp:Label>
                                <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display: none;"></asp:TextBox>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control inputTxt pull-left" Width="200" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" TabIndex="2"></asp:DropDownList>

                                
                            </div>
                             <div class="col-sm-1 col-md-1 col-lg-1 pading5px">
                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                              </div>
                            <div class="col-sm-3 col-md-3 col-lg-3 pading5px" style="float:right">


                                <asp:Label ID="lblcat" runat="server" CssClass=" smLbl_to">Category :</asp:Label>
                                <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="true" CssClass="chzn-select  ddlPage inputTxt" Width="180" TabIndex="2">
                                    <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                    <asp:ListItem Value="Sales" Text="Sales"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkpre" Text="Previous" runat="server" CssClass="smLbl_to" AutoPostBack="true" OnCheckedChanged="chkpre_CheckedChanged" />
                                <asp:DropDownList ID="ddlPrevious" runat="server" Width="100" OnSelectedIndexChanged="ddlPrevious_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" TabIndex="2"></asp:DropDownList>

                                

                            </div>


                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:TextBox ID="txtml" runat="server" TextMode="MultiLine"></asp:TextBox>

            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Visible="false" ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
            <asp:Button ID="btnapprv" runat="server" OnClick="btnapprv_Click" Visible="false" Text="Approve" ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
            <%-- <asp:TextBox ID="Txtnor" runat="server"></asp:TextBox>--%>
        </div>
    </div>

    <div class="printHeader">
        <div class="comlogoprint"></div>
        <div class="compDet">
            <div class="compName"></div>
            <div class="compAdd"></div>
        </div>
    </div>


    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



