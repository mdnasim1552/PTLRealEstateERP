<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UploadDocumentation.aspx.cs" Inherits="RealERPWEB.F_33_Doc.UploadDocumentation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
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

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script type="text/javascript">


        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {

                $('.chzn-select').chosen({ search_contains: true });

            }
            catch (e) {
                alert(e);
            }

        };
        $('.chzn-select').chosen({ search_contains: true });
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

                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" Text="Type:"></asp:Label>
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>





                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbltitle" runat="server" Text="Title :"></asp:Label>

                                        <asp:Panel runat="server" ID="pnlTxt">
                                            <asp:TextBox ID="txtsName" runat="server"  CssClass="form-control form-control-sm"></asp:TextBox>
                                        </asp:Panel>


                                        <asp:Panel ID="pnlMonth" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlDept" runat="server" Visible="false">
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:Panel>

                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" Text="Details:"></asp:Label>

                                        <asp:TextBox ID="txtDetails1" runat="server" Rows="5" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Documents:"></asp:Label>
                                        <asp:FileUpload ID="imgFileUpload" runat="server" AllowMultiple="true" />


                                    </div>

                                </div>


                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <p class="text-right">                   
                                            <asp:LinkButton ID="lnk_save" CssClass="btn btn-success btn-sm mt20" runat="server" OnClick="lnk_save_Click">Save</asp:LinkButton>
                                        </p>

                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-8">
                            <div class="table table-sm table-responsive">
                                <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvdoc" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltitle" runat="server" Text='<%#Eval("title")%>' Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" runat="server" Text='<%#Eval("remarks")%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File">
                                            <ItemTemplate>
                                                <asp:Label ID="lblimgpath" runat="server" Text='<%#Eval("imgpath")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblid" runat="server" Text='<%#Eval("id")%>' Visible="false"></asp:Label>


                                                <asp:HyperLink runat="server" CssClass="btn btn-primary btn-sm" NavigateUrl='<%#Eval("imgpath")%>' Target="_blank">View</asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btn_remove_Click"> <i class="fa fa-trash"></i> 

                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="lnk_save" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>




