<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMRREntry.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktMRREntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            var gridview = $('#<%=this.gvMRRInfo.ClientID %>');
            $.keynavigation(gridview);
            $('.chzn-select').chosen({ search_contains: true });



            $('[id*=listGroup]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });



        }

        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
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
                <div class="card-header">
                    <asp:Panel ID="pnlMRRDettails" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblMRRDate" runat="server" class="control-label  lblmargin-top9px" Text="MRR Date"></asp:Label>
                                    <asp:TextBox ID="txtCurMRRDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNo" runat="server" class="control-label  lblmargin-top9px" Text="Ref No."></asp:Label>
                                    <asp:Label ID="lblCurMRRNo1" runat="server" class="control-label  lblmargin-top9px" Text="MRR00-"></asp:Label>
                                    <asp:TextBox ID="txtCurMRRNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblMRRNo" runat="server" class="control-label  lblmargin-top9px" Text="MRR No."></asp:Label>
                                    <asp:TextBox ID="txtMRRRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-2">
                                <asp:LinkButton ID="ImgbtnPreMRR" runat="server" CssClass="lblTxt lblName" OnClick="ImgbtnPreMRR_Click">Previous MRR</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevMRRList" runat="server" Width="180px" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row  mb-1">
                            <div class="col-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon2">Project List</span>
                                    </div>
                                    <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Project" OnClick="ImgbtnFindProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-1">
                                <asp:CheckBox ID="chkdupMRR" runat="server" Text="Dup.MRR" CssClass="btn btn-primary checkBox" />
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon3">Supplier List</span>
                                    </div>
                                    <asp:TextBox ID="txtSupSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="ImgbtnFindSup" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Supplier" OnClick="ImgbtnFindSup_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-4">
                                <asp:DropDownList ID="ddlSupList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSupList_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-1 ml-3 pull-right">
                                <asp:Label ID="lblPO" runat="server" class="control-label  lblmargin-top9px" Text="Order"></asp:Label>
                            </div>
                            <div class="col-2">
                                <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                            </div>
                            <div class="col-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlResDetails" runat="server" Visible="false">
                        <div class="row mt-1">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" class="control-label  lblmargin-top9px" Text="Chalan No"></asp:Label>
                                    <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" class="control-label  lblmargin-top9px" Text="QC No"></asp:Label>
                                    <asp:TextBox ID="txtQc" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblChaDate" runat="server" class="control-label  lblmargin-top9px" Text="Challan Date"></asp:Label>
                                    <asp:TextBox ID="txtChaDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtChaDate" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtChaDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon4">Resource List</span>
                                    </div>
                                    <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Supplier" OnClick="ImgbtnFindRes_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:ListBox ID="listGroup" runat="server" CssClass="form-control" Style="min-width: 360px !important;" SelectionMode="Multiple"></asp:ListBox>

                            </div>
                            <div class="col-1">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Select" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                            <div class="col-1">
                                <asp:LinkButton ID="lbtnSelectResAll" runat="server" Text="Select All" OnClick="lbtnSelectResAll_Click" Visible="false" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <asp:GridView ID="gvMRRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnSelectedIndexChanged="gvMRRInfo_SelectedIndexChanged">
                        <PagerSettings Visible="False" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Item Serial RowID add for Manama--%>
                            <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Req No." Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvReqnomain" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtngvDelete" runat="server" Font-Bold="True" CssClass=" btn btn-xs" OnClick="lbtngvDelete_Click"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Req No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvReqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Materials">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                        Width="150px">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResDesc" runat="server"
                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                        Width="150px">
           
                                                    
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="25px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOrderQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrecuptodate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Qty.">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnDelMRR" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnDelMRR_Click">Delete</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOrderBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderbal")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="This MRR">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnResFooterTotal" runat="server"
                                        OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-primary primarygrdBtn btn-sm">Total :</asp:LinkButton>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvMRRQty" runat="server" BackColor="White" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" BackColor="#69AEE7" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdateMRR" runat="server" OnClientClick="return Confirmation()" OnClick="lbtnUpdateMRR_Click" CssClass="btn btn-danger primarygrdBtn btn-sm">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMRRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMRRAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="80px" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Black"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chalan Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvChlnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvMRRNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "mrrnote").ToString() %>' Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <asp:Panel ID="PnlNarration" runat="server" Visible="false">
                        <div class="row mt-2">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblreqnaration" runat="server" class="control-label  lblmargin-top9px" Text="Req Narration:" Font-Bold="true" Style="text-align: left"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-8">
                                <div class="form-group">
                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Font-Bold="true" Text="Narration"></asp:Label>
                                    <asp:TextBox ID="txtMRRNarr" CssClass="form-control" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td class="style15">
                                <asp:Label ID="lblPreparedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                    Height="16px" Style="text-align: right" Text="Prepared By:" Visible="False"
                                    Width="99px"></asp:Label>
                            </td>
                            <td class="style20">
                                <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                    Width="100px"></asp:TextBox>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblApprovedBy" runat="server" Font-Bold="True" Font-Size="12px"
                                    Height="16px" Style="text-align: right" Text="Approved By:" Visible="False"
                                    Width="80px"></asp:Label>
                            </td>
                            <td class="style71">
                                <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False"
                                    Width="120px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblApprovalDate" runat="server" Font-Bold="True"
                                    Font-Size="12px" Height="16px" Style="text-align: right" Text="Approv.Date:"
                                    Visible="False" Width="66px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)"
                                    Visible="False" Width="100px"></asp:TextBox>
                            </td>
                            <td class="style69">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td class="style60">&nbsp;
                            </td>
                            <td class="style53">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
