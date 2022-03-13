<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktWorkOrderEntry.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktWorkOrderEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
     
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

            $(".select2").select2();

        };
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
                    <asp:Panel ID="pnlPurOrderDet" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblOrderDate" runat="server" class="control-label  lblmargin-top9px" Text="Order Date"></asp:Label>
                                    <asp:TextBox ID="txtCurOrderDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurOrderDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MMM.yyyy" TargetControlID="txtCurOrderDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblOrderNo" runat="server" class="control-label  lblmargin-top9px" Text="Order No"></asp:Label>
                                    <asp:Label ID="lblCurOrderNo1" runat="server" class="control-label  lblmargin-top9px" Text="POR00- "></asp:Label>
                                    <asp:TextBox ID="txtCurOrderNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNo" runat="server" class="control-label  lblmargin-top9px" Text="Ref. No"></asp:Label>
                                    <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lbltxtissueno" runat="server" class="control-label  lblmargin-top9px" Text="P.O #"></asp:Label>
                                    <asp:TextBox ID="lblissueno" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>

                            <div class="col-2">
                                <asp:LinkButton ID="lbtnPrevOrderList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevOrderList_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevOrderList" runat="server" Width="180px" CssClass="form-control chzn-select" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                        </div>

                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 350px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="PurApp" runat="server">
                            <div class="row">

                                <div class="col-2">
                                    <div class="input-group input-group-sm">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon2">Supplier:</span>
                                        </div>
                                        <asp:TextBox ID="txtsrchSupplier" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <asp:LinkButton ID="imgSearchOrderno" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchOrderno_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4 mb-3">
                                    <asp:DropDownList ID="ddlSuplierList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSuplierList_SelectedIndexChanged"></asp:DropDownList>
                                </div>

                                <asp:GridView ID="gvAprovInfo" runat="server"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="482px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--Item Serial RowID add for Manama--%>
                                        <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkitem" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="1" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrjCod11" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod2" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spcfcod Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspfcod02" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplDesc" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc1").ToString() %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnSelectedOrdr" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectedOrdr_Click">Selected Order</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAP. NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPAPNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo3" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRefno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprovsRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovsrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdispercnt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt")).ToString("#,##0.00;(#,##0.00); ") +(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt"))>0?"%":"")%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actual Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNewApprovRate" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Order Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNewOrderAmt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod0" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayType0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AppNo" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPAPNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="WorkOrdr" runat="server">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-6 pading5px ">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-prepend">
                                                    <asp:Label ID="Label12" runat="server" Text="Subject:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-6 pading5px">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-prepend">
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Dear Sir,"></asp:Label>
                                                </span>

                                            </div>
                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-6 pading5px mb-2">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-prepend">
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text=":"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtLETDES" runat="server" class="form-control inputTxt"></asp:TextBox>
                                                <div class="input-group-prepend">
                                                    <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging" CssClass="btn btn-primary checkBox btn-xs" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                            </fieldset>


                            <asp:Panel ID="PnlCharging" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-2">
                                        <div class="input-group input-group-sm">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon4">Project Name:</span>
                                            </div>
                                            <asp:TextBox ID="txtSrchProjectName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4 mb-3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary okBtn btn-sm" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-2">
                                        <div class="input-group input-group-sm">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon5">Charge:</span>
                                            </div>
                                            <asp:TextBox ID="txtSrchCharge" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <asp:LinkButton ID="imgSearchCharge" runat="server" CssClass="btn btn-secondary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchCharge_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4 mb-3">
                                        <asp:DropDownList ID="ddlCharge" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                    </div>

                                </div>
                            </asp:Panel>


                            <div class="table-responsive mb-2">
                                <asp:GridView ID="gvOrderInfo" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvOrderInfo_PageIndexChanging">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--Item Serial RowID add for Manama--%>
                                        <asp:TemplateField HeaderText="Item Sl" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItemSl" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"rowid")) %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrjCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aprovno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprovNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="140px">

                                                            
                                                            
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>

                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefno1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Aprov.No.">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primarygrdBtn btn-sm" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprovNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno1")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pur.Appr.Qty">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdatePurOrder" runat="server" CssClass="btn btn-success primaryBtn btn-sm" OnClientClick="return Confirmation();" OnClick="lbtnUpdatePurOrder_Click">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprovQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOrderQty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:CheckBox ID="lblfchkbox" Text=" Forward" runat="server" Width="70px"></asp:CheckBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprovsRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovsrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvdispercnt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dispercnt"))>0?"%":"") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Actual Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOrderRate" runat="server" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="75px" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOrderAmt" runat="server" BackColor="Transparent" BorderStyle="none"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Font-Size="11px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTOrderAmt" runat="server" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay.Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayType" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-9 pading5p ">
                                    <asp:LinkButton ID="btnSendmail" CssClass="btn btn-info primaryBtn btn-sm" runat="server" OnClick="btnSendmail_Click">Send Email</asp:LinkButton>
                                </div>

                                <div class="col-md-3 pading5px">
                                    <div class="input-group input-group-sm">
                                        <span class="input-group-prepend ">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Advanced Amount:"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtadvAmt" runat="server" class="form-control inputTxt"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <asp:Panel ID="pnlsch" runat="server">
                                <fieldset class="scheduler-border fieldset_Nar">
                                    <div class="form-horizontal">

                                        <asp:Panel ID="pnlschgenerate" runat="server" Visible="False">
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px ">
                                                    <div class="input-group input-group-sm">
                                                        <span class="input-group-prepend">
                                                            <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName" Text="Total Installement:"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtTInstall" runat="server" class="form-control inputTxt"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 pading5px mt-1">
                                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primarygrdBtn btn-sm" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-prepend">
                                                        <asp:Label ID="lPays1" runat="server" CssClass="lblTxt lblName" Text="Payment Shedule"></asp:Label>
                                                    </span>
                                                    <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                        CssClass="style22" Font-Bold="True" ForeColor="Black"
                                                        OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                            </asp:Panel>

                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="gvPayment_RowDeleting" ShowFooter="True" Width="223px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" ForeColor="Black"
                                                Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvschcode" runat="server" ForeColor="Black" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inscode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insdesc")) %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="20px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "insdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lTotalPayment" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>


                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIT (%)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvaitper" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Width="60px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aitper")).ToString("#,##0;-#,##0; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AIT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvait" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ait")).ToString("#,##0;-#,##0; ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfait" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnetamt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfschAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschrmrks" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschrmrks02" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks02")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                            <asp:Panel ID="PanelOther" runat="server">
                                <fieldset class="scheduler-border fieldset_Nar">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblreqnaration" runat="server" class="lblTxt lblName" Width="900px" Text="Req Narration: " Font-Bold="true" Style="text-align: left"> </asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Font-Bold="true" Text="Narration:"></asp:Label>
                                                <asp:TextBox ID="txtOrderNarr" CssClass="form-control" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group" id="divtermsp2p" runat="server" visible="false">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblReqNarrP" runat="server" CssClass="lblTxt lblName" Font-Bold="true">Terms & Conditions </asp:Label>
                                                <asp:TextBox ID="txtOrderNarrP" runat="server" CssClass="form-control" Rows="6" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group" id="divterms" runat="server" visible="true">
                                            <div class="form-group" style="margin-top: 10px;">
                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Type:"></asp:Label>
                                                <div class="col-md-2 pading5px">
                                                    <asp:DropDownList ID="ddltypecod" CssClass="form-control inputTxt" runat="server" Visible="false">
                                                        <asp:ListItem Value="001">Service Terms
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="002">Products Terms</asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                                <asp:LinkButton ID="lnkselect" runat="server" Visible="false" CssClass="btn btn-primary primarygrdBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-group row">
                                                    <div class="col-md-11 pading5px">
                                                        <div class="input-group">
                                                            <span class="input-group-prepend">
                                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Terms &amp; Conditions:"></asp:Label>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:LinkButton ID="lnkAddTerms" runat="server" CssClass="pull-right btn btn-xs btn-success" OnClick="lnkAddTerms_Click" ToolTip="Add New Terms and Conditions" Width="65px"> <span class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                                        <asp:Label ID="lssircode" runat="server" Visible="False"></asp:Label></td>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvOrderTerms" runat="server" AllowPaging="True"
                                                    AutoGenerateColumns="False" PageSize="30" ShowFooter="true"
                                                    CssClass="table table-striped table-hover table-bordered grvContentarea">
                                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                        Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                    Style="text-align: left; background-color: Transparent"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>'
                                                                    Width="150px"></asp:TextBox>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" Font-Size="16px"
                                                                    Text=" : "></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                    Style="text-align: left; background-color: Transparent"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>'
                                                                    Width="410px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                    Style="text-align: left; background-color: Transparent"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>


                                                                <asp:LinkButton ID="lbtndelterm" runat="server" ToolTip="Delete" OnClientClick="javascript:return FunConfirm();" OnClick="lbtndelterm_Click"> <span class="glyphicon glyphicon-trash"></span></asp:LinkButton>

                                                            </ItemTemplate>
                                                            <ItemStyle Width="40px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle BackColor="#F5F5F5" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-2 pading5px asitCol2 ">
                                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By:" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="inputtextbox" Visible="false">></asp:TextBox>

                                                </div>
                                                <div class="col-md-6 pading5px">

                                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="false">></asp:Label>
                                                    <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="inputtextbox" Visible="false">></asp:TextBox>

                                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approval Date: " Visible="false">></asp:Label>
                                                    <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)" Visible="false">></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtApprovalDateCalendarExtender2" runat="server"
                                                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtApprovalDate"></cc1:CalendarExtender>
                                                </div>



                                            </div>
                                        </div>




                                    </div>
                                </fieldset>
                                <fieldset>
                                    <div class="col-md-12" id="ImagePanel" runat="server">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading" runat="server" id="imgpanel">
                                                <i class="fas fa-images"></i>&nbsp;&nbsp;Image Upload
                                                    <div class="pull-right">
                                                        <div class=" file-upload">

                                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                ThrobberID="imgLoader" CssClass="form-control"
                                                                OnUploadedComplete="FileUploadComplete" />
                                                            <asp:Image ID="imgLoader" runat="server" Visible="False" ImageUrl="~/images/Wait.gif" />
                                                            <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" OnClientClick="return confirm('Really Do You want to Delete This Image?')" CssClass=" btn btn-xs btn-danger mt-2">Delete</asp:LinkButton>

                                                        </div>
                                                    </div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="form-group">
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                        </div>

                                                    </div>

                                                </div>
                                                <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <div class="col-xs-6 col-sm-2 col-md-2 listDiv" style="padding: 0 5px;">
                                                            <div id="EmpAll" runat="server">

                                                                <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("imgpath") %>'></asp:Label>
                                                                <asp:Label ID="orderno" Visible="false" runat="server" Text='<%# Eval("orderno") %>'></asp:Label>
                                                                <%--<asp:HyperLink runat="server" ID="hlnkImg" Target="_blank" NavigateUrl="<%#%>>" class="uploadedimg">
                                                         
                                                    </asp:HyperLink>--%>
                                                                <a href="<%#this.ResolveUrl( Convert.ToString(DataBinder.Eval(Container.DataItem, "imgpath")))%>" class="uploadedimg" target="_blank">
                                                                    <asp:Image ID="GetImg" runat="server" CssClass="pop image img img-responsive img-thumbnail " Height="135px"></asp:Image>

                                                                    <div class="checkboxcls">
                                                                        <asp:CheckBox ID="ChDel" Style="margin: 0px 80px; padding: 0px;" runat="server"></asp:CheckBox>
                                                                    </div>

                                                                </a>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                                    <h4 class="modal-title" id="myModalLabel">Project Image preview</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <img src="" id="imagepreview" class="img img-responsive">
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>

                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
