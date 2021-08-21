<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProImgUpload.aspx.cs" Inherits="RealERPWEB.F_33_Doc.ProImgUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
                                 <asp:Label ID="Label2" runat="server" CssClass="lblTxt col-md-1 lblName">Select Project</asp:Label>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                    <asp:Label ID="lbldate" runat="server" CssClass="lblTxt col-md-1 lblName">Date</asp:Label>
                                    <div class="col-md-3 ">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server">

                            <div class="col-md-3">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">


                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName text-warning">Project Image</asp:Label>
                                            <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />
                                            <div>

                                                <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                    onchange="submitform();" ToolTip="Employee Image" Width="216px" />
                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                    

                </asp:Panel>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:Panel ID="Panel3" runat="server">



                        <div class="col-md-5 pading5px col-md-offset-2">
                            <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-danger primaryBtn margin5px ">Update</asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-info primaryBtn">Delete</asp:LinkButton>



                        </div>
                    </asp:Panel>
                </div>
            </div>
            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>