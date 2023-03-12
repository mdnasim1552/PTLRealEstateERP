<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPFIndvSal.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.RptPFIndvSal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
<%--            var gvswfsum = $('#<%=this.gvswfsum.ClientID %>');
            gvswfsum.Scrollable();--%>

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card mt-5">
                <div class="card-header">

                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server">Company</asp:Label>

                                <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lbldeptnameagg" runat="server">Department</asp:Label>

                                <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblsection" runat="server">Section</asp:Label>

                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select  " AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Employee List:</asp:Label>
                                <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="2" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-1">
                            <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>

                        </div>
                    </div>

                    <div class="from-group row">
                        <div class="col-md-1">
                            <label class="control-label" for="From Date">From Date</label>
                            <asp:TextBox ID="txtfrmdat" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdat"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="ToDate">To Date</label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>


                    </div>

                </div>
                <div class="card-body">





                    <asp:GridView ID="gvsalsum" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoswf" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Month ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmonth" runat="server" Font-Size="11PX"
                                        Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gross Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvswfamt" runat="server" Font-Size="11PX"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="gvfgssal" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cash Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvswfamt" runat="server" Font-Size="11PX"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="gvFgvcashamt" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bank Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvpfswf" runat="server" Font-Size="11PX"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFgvbankamt" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                                  <asp:TemplateField HeaderText="PF Fund">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvpfswf" runat="server" Font-Size="11PX"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFgvpfamt" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtotalamt" runat="server" Font-Size="11PX"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#, ##0.00;(#, ##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="gvFtotamat" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
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








        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



