
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptWorkSchedule.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptWorkSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
   
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            var gv = $('#<%=this.gvWorkSchd.ClientID %>');
            gv.Scrollable();
        }
    </script>
     <style type="text/css">
        .table th,.table td{
            padding:2px;
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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvWorkSchd" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" ShowFooter="True" Width="623px" PageSize="20"
                OnPageIndexChanging="gvConPro_PageIndexChanging"
                OnRowDataBound="gvWorkSchd_RowDataBound">

                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Font-Size="10px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvCode" runat="server" Height="16px" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblgvItem" runat="server" Height="16px" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'
                                Width="90px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:HyperLink ID="HLgvDesc" runat="server" Height="16px" Target="_blank" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                Width="400px" Font-Underline="False" ForeColor="Black"></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lgvUnit" runat="server" Style="text-align: Left" Font-Size="12px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std. Qty">
                        <ItemTemplate>
                            <asp:Label ID="lgvStdQty" runat="server" Style="text-align: right" Font-Size="12px"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

