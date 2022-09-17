<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SuppLimitCodeBook.aspx.cs" Inherits="RealERPWEB.F_14_Pro.SuppLimitCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
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
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('#<%=this.grvacc.ClientID%>').tblScrollable();

            $(function () {
                $('[id*=chkCategoryName]').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });


        }

        function openSupModal() {
            $('#modalSupSatergy').modal('toggle');
        }

        function closeSupModal() {
            $('#modalSupSatergy').modal('hide');
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <asp:Panel ID="PanelHeader" runat="server">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="form-group">
                                            <div class="col-md-9 pading5px asitCol9">
                                                <asp:Label ID="LblBookName2" runat="server" CssClass="lblTxt lblName" Text="Search Option:"></asp:Label>
                                                <asp:TextBox ID="txtsrch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                <asp:LinkButton ID="ibtnSrch" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ibtnSrch_Click" Text="Ok"></asp:LinkButton>

                                            </div>

                                            <div class="msgHandSt">

                                                <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn disabled" runat="server" Visible="false"></asp:Label>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </fieldset>

                        <asp:GridView ID="grvacc" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" CellPadding="4" Font-Size="12px" PageSize="15" Width="650px"
                            OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" OnRowDataBound="grvacc_RowDataBound">

                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="#000" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Code">

                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn  btn-primary  btn-xs">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">


                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="220"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateMat" runat="server" OnClick="lbtnUpdateMat_Click" CssClass="btn btn-danger btn-xs">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Limit">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlimit" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "limit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSameValue" runat="server" OnClick="lbtnSameValue_Click" CssClass="btn  btn-primary  btn-xs">Same Value</asp:LinkButton>
                                    </FooterTemplate>

                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Period">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvperiod" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "period")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sup. Category">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Supplier Category" BackColor="Transparent" OnClick="lbtnAdd_Click" Width="100px"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="100px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
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



            <div id="modalSupSatergy" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-sm-width">
                    <div class="modal-content modal-content-mid-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Supplier Information </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                            <asp:Label ID="lblmSupCode" runat="server" CssClass="form-control" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="modal-body " style="min-height: 400px">
                            <div class="container">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="form-group">
                                            <h4 class="modal-title"><span id="spanSupName" runat="server"></span></h4>
                                            <%--<asp:Label ID="lblmSupName" runat="server" CssClass="form-control" Text=""></asp:Label>
                                            </div>--%>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Category" Font-Size="Medium"></asp:Label>
                                            <asp:ListBox ID="chkCategoryName" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSaveCategory" runat="server" OnClick="btnSaveCategory_Click" OnClientClick="closeSupModal();" CssClass="btn btn-primary">Save</asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

