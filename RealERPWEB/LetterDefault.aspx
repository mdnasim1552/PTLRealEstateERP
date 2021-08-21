<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LetterDefault.aspx.cs" Inherits="RealERPWEB.LetterDefault" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

 
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.keynavigation.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>--%>
   <script language="javascript" type="text/javascript" src="../../tinymce/tinymce.min.js"></script> 

    <link href="Content/tinymcc.css" rel="stylesheet" />


    <script type="text/javascript" language="javascript">
        $(document).ready(function ()
        {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded()
        {
            tinymce.init
                ({
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


              
               
            //plugins: "print",
            //menubar: "file",
            //toolbar: "print"
      


                //height: 500,
                //menubar: false,
                //plugins: [
                //  'advlist autolink lists link image charmap print preview anchor',
                //  'searchreplace visualblocks code fullscreen',
                //  'insertdatetime media table contextmenu paste code'
                //],
                //toolbar: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
                //content_css: '//www.tinymce.com/css/codepen.min.css'
            });

            //$('.chzn-select').chosen({ search_contains: true });

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
                            <div class="col-sm-5 col-md-5 col-lg-5 pading5px">
                                <asp:Label ID="lbltodate" runat="server" CssClass=" lblName lblTxt">Date</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                <asp:Label ID="lblpreAdv" runat="server" CssClass=" smLbl_to">Employee</asp:Label>
                                <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display: none;"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control inputTxt pull-left" Width="200" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" TabIndex="2"></asp:DropDownList>

                                </div>
                             <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblrefno" runat="server" CssClass=" lblName lblTxt">Ref No.</asp:Label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1 pading5px">
                                
                                 <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                            <div class="col-sm-3 col-md-2 col-lg-3 pading5px">
                                <asp:LinkButton ID="lbtnprevious" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnprevious_OnClick">Previous</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevious" runat="server" Width="150" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" TabIndex="2"></asp:DropDownList>
                               
                            </div>
                   
                        </div>
                        <div class="form-group">
                                    
                            <div class="col-sm-3 col-md-3 col-lg-3 pading5px">


                                <asp:Label ID="lblcat" runat="server" CssClass=" smLbl_to">Category :</asp:Label>
                                <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="true" CssClass="chzn-select  ddlPage inputTxt" Width="180" TabIndex="2">
                                    <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                    <asp:ListItem Value="Sales" Text="Sales"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            

                            
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>

            <asp:TextBox ID="txtml" runat="server" TextMode="MultiLine"></asp:TextBox>

            <asp:Button ID="btnsave" CssClass="btn btn-success" runat="server" OnClick="btnsave_Click" Visible="false" Text="Save"  ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
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



