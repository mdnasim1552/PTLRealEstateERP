<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CreateNotice.aspx.cs" Inherits="RealERPWEB.Notices.CreateNotice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-title {
            font-size: 1.30rem;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            //Comment from parvez branch
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

            $('[id*=listboxUser]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="container-fluid mt-4">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="page-title">Create Notice</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="card card-fluid" style="min-height: 550px;">
                            <div class="card-body">
                                <div class="row mb-2">
                                    <div class="col-md-1">
                                        <asp:Label ID="lblNotTitle" runat="server">Notice Title</asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtNoticeTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblStartDat" runat="server">Start Date</asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="from-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy h: mm tt" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label ID="lblDesc" runat="server">Description</asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <section class="card card-fluid">
                                            <!-- #simplemde -->
                                            <textarea class="form-control" runat="server" id="txtnoticeDesc" rows="5"></textarea>
                                            <!-- /#simplemde -->
                                        </section>
                                    </div>
                                     <div class="col-md-1">
                                        <asp:Label ID="Label1" runat="server">End Date</asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="from-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy h: mm tt" TargetControlID="txtEndDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-md-1">
                                        <asp:Label ID="lblDepratment" runat="server">Department's</asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-md-1">
                                        <asp:Label ID="lblUser" runat="server">User's</asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <%--<asp:DropDownList ID="ddlUser" runat="server" CssClass="chzn-select form-control"></asp:DropDownList>--%>
                                        <asp:ListBox ID="listboxUser" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-1">
                                        <asp:Label ID="lblAttachment" runat="server">Attachments</asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <div id="dropzone" class="fileinput-dropzone">
                                            <span>Drop files or click to upload.</span>
                                            <!-- The file input field used as target for the file upload widget -->
                                            <%--<input id="attachFile" runat="server" type="file" name="files[]" multiple>--%>
                                          
                                               <asp:FileUpload ID="attachFile" runat="server" AllowMultiple="true" accept=".pdf"/>

                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-3 mb-3">
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-8">
                                        <asp:LinkButton ID="lbtnSaveNotice" runat="server" CssClass="btn btn-success" Width="100px" Style="float: right;" OnClick="lbtnSaveNotice_Click">Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
             <Triggers>
            <asp:PostBackTrigger ControlID="lbtnSaveNotice" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
