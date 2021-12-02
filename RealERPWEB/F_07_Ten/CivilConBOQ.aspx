<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CivilConBOQ.aspx.cs" Inherits="RealERPWEB.F_07_Ten.CivilConBOQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
          //  $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });
        }


    </script>
    <style>
        .chzn-container-single {
            width: 365px !important;           
        }
         .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
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
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row mb-2" id="divFilter">

                        <div class="col-md-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Projects</button>
                                </div>
                                <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="chzn-select  form-control  pl-0 pr-0">
                                </asp:DropDownList>

                            </div>
                        </div>
                         <div class="col-md-2">
                                <asp:LinkButton ID="lnkbtnOK" runat="server" OnClick="lnkbtnOK_Click" CssClass="btn btn-primary btn-sm primaryBtn">Ok</asp:LinkButton>

                        </div>
                    </div>

                    <div class="row mb-2">
                         <div class="col-md-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Work Group</button>
                                </div>
                                <asp:DropDownList ID="ddlWorkGroup" OnSelectedIndexChanged="ddlWorkGroup_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass=" chzn-select form-control  pl-0 pr-0">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Work List</button>
                                </div>
                                <asp:DropDownList ID="ddlWorkList" runat="server" AutoPostBack="True" CssClass=" chzn-select form-control  pl-0 pr-0">
                                </asp:DropDownList>

                            </div>
                        </div>
                          <div class="col-md-1">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page</button>
                                </div>                            

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="12px"  CssClass="form-control  pl-0 pr-0"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"  >
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="150">150</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                    </asp:DropDownList>

                            </div>
                        </div>
                         <div class="col-md-2">
                                <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_Click" CssClass="btn btn-primary btn-sm primaryBtn">Add</asp:LinkButton>

                        </div>
                    </div>

                    <div class="row mb-2">
                         
                          
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
