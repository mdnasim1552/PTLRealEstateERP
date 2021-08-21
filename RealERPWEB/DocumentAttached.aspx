<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DocumentAttached.aspx.cs" Inherits="RealERPWEB.DocumentAttached" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Content/chosen.css" rel="stylesheet" />
    <link href="Content/styles.imageuploader.css" rel="stylesheet" type="text/css" />



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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-5 pading5px">

                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Type</asp:Label>

                                <asp:DropDownList ID="ddlDocType" runat="server" Width="233" ClientIDMode="Static" CssClass="form-control" TabIndex="1">
                                    <asp:ListItem Value="">------Select Type------</asp:ListItem>
                                    <asp:ListItem Value="REQ">Requestion</asp:ListItem>
                                    <asp:ListItem Value="QUO">Quotation</asp:ListItem>
                                    <asp:ListItem Value="WkO">Work Order</asp:ListItem>
                                    <asp:ListItem Value="MRR">MRR</asp:ListItem>
                                    <asp:ListItem Value="BILL">Bill</asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4"></div>
                            <div class="col-md-4">


                                <asp:Label runat="server" ID="lblmsg"></asp:Label>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-4 pading5px">

                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Ref NO</asp:Label>



                                <asp:DropDownList ID="ddlrefno" runat="server" ClientIDMode="Static"  Width="233" CssClass="form-control">
                                </asp:DropDownList>
                                <input type="text" class="form-control hidden" id="refNodata" />
                                <asp:Label runat="server" ID="lblrefcode" class=" hidden"></asp:Label>

                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs hidden" Text="Show Attached" OnClick="btnShowimg_OnClick" />

                            </div>
                            <div class="col-sm-2 col-xs-2 reply-emojis">

                                <a href="#ImageUpload" data-toggle="modal"><i class="fa fa-image fa-2x"></i></a>

                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="row hidden">
                <div class="col-md-6 col-sm-6 col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon-picture"></span>Attached Documents
       
                                            <div class="pull-right">
                                                <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>

                                            </div>
                        </div>
                        <div class="panel-body ">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                <div id="EmpAll" runat="server">

                                                    <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                    <asp:Label ID="billno" Visible="False" runat="server" Text='<%# Eval("refno") %>'></asp:Label>
                                                    <asp:Label ID="attachedid" Visible="False" runat="server" Text='<%# Eval("id") %>'></asp:Label>

                                                    <a href="../../Upload/Purchase/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                        <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                    </a>
                                                    <div class="checkboxcls">
                                                        <asp:CheckBox ID="ChDel" runat="server" />
                                                    </div>


                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

            <!-------------------image upload model--------------------------->
            <div class="modal fade" id="ImageUpload" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header modal-header-success">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h6><i class="glyphicon glyphicon-thumbs-up"></i>Upload Images</h6>
                        </div>
                        <div class="modal-body">
                            <section role="main" class="l-main">

                                <div class="uploader__box js-uploader__box l-center-box">
                                    <form action="your/nonjs/fallback/" method="POST">
                                        <div class="uploader__contents">
                                            <label class="button button--secondary" for="fileinput">Select Files</label>
                                            <input id="fileinput" class="uploader__file-input" type="file" multiple value="Select Files">
                                        </div>
                                        <input class="button button--big-bottom" type="submit" value="Upload Selected Files">
                                    </form>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <script src="Scripts/jquery.imageuploader.js"></script>

    <script>
        $(document).ready(function () {
           // $("#ddlDocType").change();

        });
        $("#ddlDocType").change(function () {
            var doctype = $("#ddlDocType option:selected").val();
            $.ajax({
                type: "POST",
                url: "Service/UserService.asmx/GetdoctypeList",
                data: '{doctype: ' + JSON.stringify(doctype) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlrefno = $("[id*=ddlrefno]");
                    ddlrefno.empty().append('<option selected="selected" value="00000000000000">Please select</option>');
                    $.each(r.d, function () {
                        ddlrefno.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
                 

            });


        });

        $("#ddlrefno").change(function () {
            var refNodatad = $("#ddlrefno option:selected").val();
             
            $("#refNodata").val(refNodatad);
            $('#<%=lblrefcode.ClientID%>').text=refNodatad; 

        });

        //$("#btnShowimg").click(function () {
        //    var refNodatad = $("#ddlrefno option:selected").val();
        //     $.ajax({
        //        type: "POST",
        //        url: "Service/UserService.asmx/GetAttachedDocs",
        //        data: '{refNodatad: ' + JSON.stringify(refNodatad) + '}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (r) {
        //            var ddlrefno = $("[id*=ddlrefno]");
        //            ddlrefno.empty().append('<option selected="selected" value="00000000000000">Please select</option>');
        //            $.each(r.d, function () {
        //                ddlrefno.append($("<option></option>").val(this['Value']).html(this['Text']));
        //            });
        //        }
        //    });

        //});
        (function () {

            var options = {};
            $('.js-uploader__box').uploader(options);
        }());


    </script>



       </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

