<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMatIssue.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktMatIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

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
            <div class="card card-fluid mb-2">
                <div class="card-body">
                    <asp:Panel ID="MatIssDet" runat="server" CssClass="mt-2">
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblCurNo" runat="server" class="control-label  lblmargin-top9px" Text="Issue No:"></asp:Label>
                                    <asp:Label ID="lblCurISSNo1" runat="server" class="control-label  lblmargin-top9px"></asp:Label>
                                    <asp:TextBox ID="txtCurISSNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">000</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2 ml-1">
                                <div class="form-group">
                                    <asp:Label ID="lblIssueDate" runat="server" class="control-label  lblmargin-top9px" Text="Issue Date"></asp:Label>
                                    <asp:TextBox ID="txtCurISSDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurISSDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurISSDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-2 ml-1">
                                <div class="form-group">
                                    <asp:Label ID="lblSMCRNo" runat="server" class="control-label  lblmargin-top9px" Text="SMCR.No."></asp:Label>
                                    <asp:TextBox ID="txtMIsuRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2 ml-1">
                                <div class="form-group">
                                    <asp:Label ID="lblDMIRFNo" runat="server" class="control-label  lblmargin-top9px" Text="DMIRF No."></asp:Label>
                                    <asp:TextBox ID="txtsmcr" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnPrevISSList" runat="server" Text="Prev. Issue. List:" Font-Underline="false" OnClick="lbtnPrevISSList_Click"></asp:LinkButton>
                                    <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <div class="input-group input-group-alt input-group-sm">
                                        <div class="input-group-prepend ">
                                            <span class="input-group-text">Project</span>
                                        </div>
                                        <asp:TextBox ID="txtsrchproject" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnFindProject" CssClass="btn btn-secondary btn-sm" runat="server" OnClick="lbtnFindProject_Click" TabIndex="2"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4 ml-1">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlprjlist" runat="server" CssClass="chzn-select form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-2 ml-3">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-header">
                    <asp:Panel ID="PnlRes" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <div class="input-group input-group-alt input-group-sm">
                                        <div class="input-group-prepend ">
                                            <span class="input-group-text">PR Type</span>
                                        </div>
                                        <asp:TextBox ID="txtSearchMaterials" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="ibtnSearchMaterisl" CssClass="btn btn-secondary btn-sm" runat="server" OnClick="ibtnSearchMaterisl_Click" TabIndex="2"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-2 ml-1">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlPrType" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlPrType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-1 ml-1">
                                <asp:Label ID="lblActivity" runat="server" class="control-label  lblmargin-top9px" Text="Activity Type"></asp:Label>
                            </div>
                            <div class="col-2">
                                <asp:DropDownList ID="ddlActType" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSelectReaSpesAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectReaSpesAll_Click">Select All</asp:LinkButton>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <div class="input-group input-group-alt input-group-sm">
                                        <div class="input-group-prepend ">
                                            <span class="input-group-text">Page</span>
                                        </div>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <div class="table-responsive">
                        <asp:GridView ID="grvissue" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-bordered grvContentarea"
                            OnPageIndexChanging="grvissue_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtngvMatDelete" runat="server" Font-Bold="True"
                                            CssClass=" btn btn-xs" OnClick="lbtngvMatDelete_Click" ToolTip="Delete Material">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PR Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtype")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activity Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pur. Req. Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwrkdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClientClick="return Confirmation();"
                                            OnClick="lbtnDelete_Click">Delete All</asp:LinkButton>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activity Type">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-success btn-sm"
                                            OnClick="lnkupdate_Click" OnClientClick="javascript:return FunConfirmSave();">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvspecifition" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisuqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisurmk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="180px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeaderNew" />
                            <FooterStyle CssClass="grvFooterNew" />
                        </asp:GridView>
                    </div>
                    <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                        <div class="row mt-3">
                            <div class="col-1">
                                <asp:Label ID="lblNarration" runat="server" Text="Narration:" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-7">
                                <asp:TextBox ID="txtISSNarr" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
