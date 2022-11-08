<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptFrameAndFinishingCost.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptFrameAndFinishingCost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });


        }

    </script>
    <style type="text/css">
        .table th, .table td {
            padding: 2px;
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
                    <asp:Panel ID="panelHead" runat="server">
                        <div class="row mb-4">

                            <div class=" col-md-3  pading5px asitCol3 d-none">

                                <asp:Label ID="lblProjectList" CssClass="lblTxt lblName " runat="server" Text="Project Name:"></asp:Label>
                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                            </div>


                            <div class="col-md-3">
                                <asp:Label ID="Label1" CssClass="lblTxt lblName " runat="server" Text="Project Name:"></asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>


                            <div class="col-md-1 pading5px ml-2" style="margin-top: 22px">
                                <asp:LinkButton ID="lbtOk" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" OnClick="lbtOk_Click">Ok</asp:LinkButton>

                            </div>


                            <div class="col-md-1 pading5px">
                                <asp:CheckBox ID="chkSum" runat="server" TabIndex="10" Text="Summary" CssClass="btn btn-primary checkBox" Visible="false" Checked="true" />

                            </div>



                        </div>
                    </asp:Panel>


                    <div class="form-group">
                        <div class=" col-md-3 pading5px  asitCol3">
                            <asp:Label ID="lblConAreagrwisedet" CssClass="smLbl_to" runat="server"></asp:Label>
                            <asp:Label ID="lblSalAreagrwisedet" CssClass=" smLbl_to" runat="server"></asp:Label>

                        </div>

                        <div class=" clearfix"></div>
                    </div>
                </div>
            </div>
            <div class="card" style="min-height: 480px">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvbgdgrwisedet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbgdgrwisedet_RowDataBound" PageSize="20"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" font="12px"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="group">
                                    <ItemTemplate>
                                        <asp:Label ID="lglrp" font="12px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp1")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="gvActDescgrwisedet" font="12px" runat="server" ForeColor="Black" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                            Width="400px" OnClick="lnkgvActDescgrwisedet_Click"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitgrwisedet" font="12px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbudgetamt" font="12px" runat="server" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostgrwisedet" font="12px" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostgrwisedet" runat="server" font="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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

