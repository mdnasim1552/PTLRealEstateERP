<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" AutoEventWireup="true" CodeBehind="RptInvoice02.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptInvoice02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
        }

    </script>
     <style>        
        .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 28px !important;
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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Date"> </asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server" Text="Project Name"></asp:Label>                                
                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="srearchBtn" OnClick="imgbtnFindProject_Click" TabIndex="12"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" form-control form-control-sm" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm" Width="310px" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Customer Name</asp:Label>
                                <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="srearchBtn" OnClick="imgbtnFindCustomer_Click" TabIndex="15"><i class="fas fa-search"></i></asp:LinkButton>
                                <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="form-control form-control-sm" Visible="false" TabIndex="14"></asp:TextBox>
                                <asp:DropDownList ID="ddlCustName" runat="server" CssClass="chzn-select form-control form-control-sm" Width="310px" TabIndex="13" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 520px;">
                    <div class="row">
                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Subject:" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt" Visible="false"> </asp:TextBox>
                    </div>
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Dear Sir," Visible="false"></asp:Label>
                         <asp:TextBox ID="txthead" runat="server" TextMode="MultiLine" class="form-control inputTxt" Visible="false"></asp:TextBox>
                    </div>
                    <div class="row  table-responsive mt-2">
                        <asp:GridView ID="gvCustInvoice" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea " OnRowDataBound="gvCustInvoice_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Items">
                                        <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdateInvoice" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return Confirmation();" OnClick="lbtnUpdateInvoice_Click"> Update</asp:LinkButton>

                                        </FooterTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc2" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvschamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
