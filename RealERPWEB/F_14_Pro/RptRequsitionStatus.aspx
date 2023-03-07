<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptRequsitionStatus.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptRequsitionStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gvReqStatusAp = $('#<%=this.gvReqStatusAp.ClientID %>');
            gvReqStatusAp.Scrollable();


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

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#CDDBC8" CssClass="rbtnList1"
                                RepeatColumns="6" RepeatDirection="Horizontal"
                                Width="300px">
                                <asp:ListItem>Requisition  Basis </asp:ListItem>
                                <asp:ListItem>Materials Basis</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox ID="ChkBalance" runat="server" Text="Without Zero Balance" />
                        </div>
                        <div class="col-md-2">
                            <asp:CheckBox ID="CheckReqApp" runat="server" Text="Requisition Qty" Visible="false" />
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label">Project Name</asp:Label>
                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" Style="width: 90%">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Project Name</asp:Label>
                            <asp:TextBox ID="txtSrcResource" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnFindResource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control chzn-select" Style="width: 90%">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary" Style="margin-top: 20px;" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-4 mt-3 pading5px">

                            <asp:Label ID="Label13" runat="server" CssClass="form-label">Date:</asp:Label>


                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputDateBox" TabIndex="5"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                            <asp:Label ID="Label3" runat="server" CssClass="smLbl_to"
                                Text="to:"></asp:Label>


                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox" TabIndex="5"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblReq" runat="server" CssClass="form-label">Req</asp:Label>
                            <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:LinkButton ID="imgbtnFindRequiSition" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindRequiSition_Click" TabIndex="2" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                        </div>


                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="70px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
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
            </div>

            <div class="card" style="min-height: 500px">
                <div class="card-body">

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ReqStatus" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                    OnPageIndexChanging="gvReqStatus_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFspcf" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"> Total :</asp:Label>
                                            </FooterTemplate>


                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFApQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Purchase Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvordrqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrcvqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFRecqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFBalqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEntryUser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aproved Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovdat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="App. User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprvuser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ausrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site Sup. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsitesupdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sitesupdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="center" />
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


                        <asp:View ID="ClientMod" runat="server">
                            <asp:GridView ID="grvRptCliMod" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Style="text-align: left" Width="963px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="grvRptCliMod_PageIndexChanging" AllowPaging="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ADW No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAdwNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcUDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Modification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Date" FooterText="Total: " FooterStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvQty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </asp:View>


                        <asp:View ID="ViewReqStApproved" runat="server">

                            <div class="table-responsive">

                                <asp:GridView ID="gvReqStatusAp" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                    OnPageIndexChanging="gvReqStatusAp_PageIndexChanging" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <FooterTemplate>
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrfNoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDateap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnitap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprQtyap" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprrateap" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvapramt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvreqapAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEntryUserap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eusrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aproved Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovdatap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App. User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAprvuserap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ausrname")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
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


                    </asp:MultiView>

                </div>
            </div>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




