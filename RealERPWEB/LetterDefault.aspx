<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="LetterDefault.aspx.cs" Inherits="RealERPWEB.LetterDefault1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="tinymce/tinymcc.css" rel="stylesheet" />

    <%--<script src="https://cdn.tiny.cloud/1/no-api-key/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>--%>
    <%--<script src="tinymce/tinymce.min.js"></script>--%>
    <script src="tinymce/tinymce.min.js"></script>
    <%--<script>tinymce.init({ selector: 'textarea' });</script>--%>

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
                relative_urls: false,
                remove_script_host: false,
                document_base_url: false,

                content_css: [
                    '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
                    '//www.tinymce.com/css/codepen.min.css',
                    'content/tinymcc.css'

                ]

            });

            // $('.chzn-select').chosen({ search_contains: true });

        }

        
    </script>
    <style>
        body{
            font-family:"Calibri, sans-serif";
            font-size:11px;
        }

        @media print {
    .pagebreak { page-break-before: always; } /* page-break-after works, as well */
}
 
table, th, td {
  border: 1px solid black!important;
  border-collapse: collapse!important;
  padding:5px!important;
  border-style: solid!important;
}
        ol.ollist{
     margin: 0 !important;
}
        ol 
        {
            margin:0 !important;
          counter-reset: item;
        }

        ol li 
         {
           display: block
         }

        ol li:before {
                    content: counters(item, ".") ". ";
                    counter-increment: item;
           }

         ol ol[style*="list-style-type: lower-alpha;"] li:before {
                content: counters(item, ".", lower-alpha) ". ";
                counter-increment: item;
            }

        label {
            display: inline-block;
            font-weight: 400;
            margin-left: 3px;
            margin-top: -63px;
            max-width: 100%;
        }
/*
        body {
            font-family: Tahoma !important;
        }*/

        @page {
            size: A4;
            margin: 11mm 17mm 17mm 17mm;
        }

        @media print {
            footer {
                position: fixed;
                bottom: 0;
            }
             ol 
        {
          margin: 0 !important;
        }
ol.ollist{
     margin: 0 !important;
}
            .content-block, p {
                page-break-inside: avoid;
                margin: 0 !important;
            }

            html, body {
                width: 210mm;
                height: 297mm;
            }
        }
    </style>
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
                    <div class="row d-flex">

               
                            <div class="form-group mr-auto p-2">
                                <asp:Label ID="lblmsg1" runat="server" CssClass=" btn  btn-success primaryBtn pull-right" Visible="false"></asp:Label>
                            </div>
                  

                      <asp:Panel runat="server" ID="pnl2" Visible="false">
                        <div class=" ml-auto p-2">
                           <asp:Button ID="btnSendLetter" runat="server" CssClass="btn btn-success btn-sm" Text="Submit Letter" OnClick="btnSendLetter_Click" />
                        </div>
                    </asp:Panel>

                    </div>
                </div>

                <div class="card-body" runat="server" id="dnone">


                    <asp:Panel runat="server" ID="panl1">

                        <div class="row">

                            <div class="col-md-2">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="btn btn-secondary btn-sm">Date</asp:Label>
                                    </div>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-3" id="divCompany" runat="server" visible="false">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="Label2" runat="server" CssClass="btn btn-secondary btn-sm">Company</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3" id="dptDiv" runat="server">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="Label1" runat="server" CssClass="btn btn-secondary btn-sm">Department</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-3" id="sectDiv" runat="server">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="lblsec" runat="server" CssClass="btn btn-secondary btn-sm">Section</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <asp:Label ID="lblep" runat="server" CssClass="btn btn-secondary btn-sm">Employee</asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm  pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>

                            <div class="col-sm-3 col-md-3 col-lg-3 d-none">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                    </div>
                                    <div class="input-group-prepend ">
                                        <asp:CheckBox ID="chkpre" Text="Previous" runat="server" CssClass="smLbl_to" AutoPostBack="true" />
                                    </div>
                                    <asp:DropDownList ID="ddlPrevious" runat="server" Width="100" AutoPostBack="true" CssClass="form-control" TabIndex="2"></asp:DropDownList>

                                </div>




                            </div>


                        </div>

                    </asp:Panel>
           


                    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Visible="false" ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
                    <asp:Button ID="btnapprv" runat="server" Visible="false" Text="Approve" ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
                    <%-- <asp:TextBox ID="Txtnor" runat="server"></asp:TextBox>--%>
                </div>

                <div class="printHeader">
                    <div class="comlogoprint"></div>
                    <div class="compDet">
                        <div class="compName"></div>
                        <div class="compAdd"></div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtml" runat="server" TextMode="MultiLine" Style="margin-left: 30px" CssClass="from-control tinyMCE"></asp:TextBox>

</asp:Content>
