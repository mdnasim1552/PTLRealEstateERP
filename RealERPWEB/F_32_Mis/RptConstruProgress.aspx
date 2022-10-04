<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptConstruProgress.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptConstruProgress" %>

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
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-3 pading5px asitCol3 d-none">
                            <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                            <asp:TextBox ID="txtSrcProject" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                            <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Project Name"></asp:Label>

                            <asp:DropDownList ID="ddlProjectName" runat="server"  AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                >
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1  ml-3" style="margin-top:23px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary"  ></asp:LinkButton>


                        </div>


                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" TabIndex="3" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page size"></asp:Label>

                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                </div>
            </div>
            <div class="card" style="min-height:480px;">
                <div class="card-body">
                      <div class="row">
                <asp:GridView ID="gvConPro" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" ShowFooter="True" Width="378px" PageSize="20"
                    OnPageIndexChanging="gvConPro_PageIndexChanging"
                    OnRowDataBound="gvConPro_RowDataBound">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvflrCode" runat="server" Height="16px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor Description">
                            <FooterTemplate>

                                <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="50px" Text="Total"></asp:Label>



                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Bold="true" Font-Underline="false" Target="_blank" ForeColor="Black"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                    Width="150px"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work % ">
                            <ItemTemplate>
                                <asp:Label ID="lgvWorkP" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perwork")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFWorkP" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Budget Amt">
                            <ItemTemplate>
                                <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Master Plan Amt.">
                            <ItemTemplate>
                                <asp:Label ID="lgvMasPlan" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFMasPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Master Plan As Of Today">
                            <ItemTemplate>
                                <asp:Label ID="lgvMPlanastoday" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFMPlanastoday" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Execution Amt.">
                            <ItemTemplate>
                                <asp:Label ID="lgvExAmt" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Less Execution">
                            <ItemTemplate>
                                <asp:Label ID="lgvlessExAmt" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:HyperLink ID="hlnkgvFlessexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="Blue" Target="_blank" Style="text-align: right" Width="70px"></asp:HyperLink>
                            </FooterTemplate>


                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Execution % on M.P As Of Today">
                            <ItemTemplate>
                                <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ")%>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>

                                <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>


                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
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
            <div class="card">
                <div class="card-body">
            <div class="row">
                <asp:Panel ID="Pnlnote" runat="server" Visible="False">

                    <asp:Label ID="Label15" runat="server" Width="300px" CssClass="btn btn-success primaryBtn">Note</asp:Label>
                    <div class="clearfix"></div>
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Budgeted Execution in %" CssClass=" smLbl_to"></asp:Label>

                        <asp:Label ID="lPercentonbgd" runat="server" CssClass=" smLbl_to"></asp:Label>
                        <div class=" clearfix"></div>

                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Actual Execution in %" CssClass=" smLbl_to"></asp:Label>

                        <asp:Label ID="lPercentonbgdexe" runat="server" CssClass=" smLbl_to"></asp:Label>
                        <div class=" clearfix"></div>

                    </div>

                </asp:Panel>
            </div>
            </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


