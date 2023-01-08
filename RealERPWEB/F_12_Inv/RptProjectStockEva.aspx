<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProjectStockEva.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptProjectStockEva" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }


        #ContentPlaceHolder1_gvStocjEvaluation_lblActualStock_0 {
            font-weight: bold;
        }

        .totalcount {
            font-weight: bold;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            //$('.chzn-select').chosen({ search_contains: true });

            $('[id*=chkResourcelist]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
            });

        });


        function pageLoaded() {


            $(function () {
                $('[id*=chkResourcelist]').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,
                    //$('.chzn-select').chosen({ search_contains: true });
                });
            });
            //$(function () {
            //    $('[id*=listGroup]').multiselect({
            //        includeSelectAllOption: true
            //    })

            //    $('[id*=lstProject]').multiselect({
            //        includeSelectAllOption: true
            //    })
            //});




            //var name = (1/2" PVC pipe);
            //name = name.replace("''", "").toLowerCase();
            //alert(name);



<%--            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvMatStock.ClientID %>');
            gv.Scrollable();

            var gv1 = $('#<%=this.gvMatStockSpec.ClientID%>');
            gv1.Scrollable();--%>


        }

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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2">Project Name&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlProName" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblresName" runat="server" CssClass="control-label" Text=""></asp:Label>
                                <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="lbtnresource" runat="server" OnClick="lbtnresource_Click" TabIndex="2">Material&nbsp;<i class="fas fa-search"></i></asp:LinkButton>
                                <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblGroup" runat="server" CssClass="control-label" Text="Stock Valuation"></asp:Label>
                                <asp:DropDownList ID="group" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Group Wise</asp:ListItem>
                                    <asp:ListItem Value="2">Material Wise</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="control-label" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>6000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm okBtn" OnClick="lbtnOk_Click" TabIndex="4" Style="margin-top: 20px;">ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvStocjEvaluation" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvStocjEvaluation_PageIndexChanging" OnRowDataBound="gvStocjEvaluation_RowDataBound" CssClass="table-striped table-hover table-bordered"
                                ShowFooter="True">

                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtPactdesc" runat="server" Style="text-align: left" Width="170px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtMrdesc" runat="server" Style="text-align: left" Width="90px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtResource" runat="server" Style="text-align: left" Width="110px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Ammount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActualStock" runat="server" Style="text-align: right" Width="85px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstock")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lgvttaccrecvbale" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPercentage" runat="server" Style="text-align: right" Width="40px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("0.00") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:Label ID="lgvttlsolamt" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter text-right" Font-Size="13px" Font-Bold="false" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
