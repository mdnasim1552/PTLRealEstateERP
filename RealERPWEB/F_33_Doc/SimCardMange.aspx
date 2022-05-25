<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SimCardMange.aspx.cs" Inherits="RealERPWEB.F_33_Doc.SimCardMange" %>

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
                            <form>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Mobile Number:">
                                            </asp:Label>
                                            <asp:DropDownList ID="ddlMobile" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlMobile_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label20" runat="server" Text="Employee:">
                                                <asp:CheckBox ID="chckguest" runat="server" Text="Is guest?" OnCheckedChanged="chckguest_CheckedChanged"/>
                                            </asp:Label>
                                            <asp:Panel ID="pnlemp" runat="server">
                                                <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlguest" runat="server" Visible="false">
                                                <asp:TextBox ID="txtguest" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </asp:Panel>
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
                            </form>
                        </div>

                        <div class="col-lg-8">
                            <div class="card">
                                <div class="card-header">
                                    <div>
                                        <asp:RadioButtonList ID="simtype" runat="server" AutoPostBack="True"
                                            CssClass="custom-control custom-control-inline custom-checkbox rbt p-0"
                                            Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            OnSelectedIndexChanged="simtype_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Issued</asp:ListItem>
                                            <asp:ListItem>Returned</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="table table-sm table-responsive">
                                        <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvdoc" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvdoc_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>

                                                        <asp:Label ID="lblempname" runat="server" Text='<%#Eval("empname")%>' Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobileno")%>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("issudat")%>' Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkreturn" runat="server" CssClass="btn btn-warning btn-sm" OnClick="lnkreturn_Click"> Return</i> 
                                                        </asp:LinkButton>


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
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="lnk_save" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>




