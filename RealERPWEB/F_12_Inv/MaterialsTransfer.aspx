<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MaterialsTransfer.aspx.cs" Inherits="RealERPWEB.F_12_Inv.MaterialsTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .fontbold {
            font-weight: bold !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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

                var gvisu = $('#<%=this.grvacc.ClientID %>');
                $.keynavigation(gvisu);
                //gvisu.Scrollable();

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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCurNo" runat="server" CssClass="smLbl_to" Text="ID"></asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="lblCurTransNo1" runat="server" CssClass="form-control form-control-sm" Text="TRN00-" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtCurTransNo2" runat="server" CssClass="form-control form-control-sm disabled" ReadOnly="True">00000</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblSMCR" runat="server" CssClass="control-label" Text="Ref No"></asp:Label>
                            <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblpage" runat="server" CssClass="control-label" Text="Page"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                         <div class="col-md-2" style="margin-top: 22px;">

                            <asp:CheckBox ID="chkGatePass" runat="server" TabIndex="10" Text="From Gate Pass" CssClass="" AutoPostBack="true"  OnCheckedChanged="chkGatePass_CheckedChanged" />

                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="ImgbtnPreList" runat="server" CssClass="text-primary" OnClick="ImgbtnPreList_Click">
                                    <i class="fa fa-search"></i> Previous List
                            </asp:LinkButton>
                            <asp:TextBox ID="txtRefNo1" runat="server" TabIndex="7" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectFromList" runat="server" CssClass="control-label" Text="From"></asp:Label>
                                <asp:TextBox ID="txtSrcNotify" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindNotify" runat="server"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control dataLblview" Height="30" Style="line-height: 1.5" Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="To"></asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="LinkButton3" runat="server"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                                <asp:Label ID="lblddlProjectTo" runat="server" CssClass="form-control dataLblview" Height="30" Style="line-height: 1.5" Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblGatePassNo" runat="server" CssClass="control-label" Text="Gate Pass No"></asp:Label>
                                <asp:TextBox ID="txtsrchGatePass" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="lbtnGatePassNo" runat="server" OnClick="lbtnGatePassNo_Click"><i class="fa fa-search" > </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlGatePass" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div class="row" id="pnlgrd" runat="server" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server" CssClass="control-label" Text="Resource List"></asp:Label>
                                <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindRes" runat="server" OnClick="lbtnGatePassNo_Click"><i class="fa fa-search" > </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlreslist" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSpecification" runat="server" CssClass="control-label" Text="Specification"></asp:Label>
                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkselect0" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                <asp:Label ID="lblVoucherNo" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

                            </div>
                        </div>
                    </div>

                    <div class="row" id="pnlGatePass" runat="server" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResListgp" runat="server" CssClass="control-label" Text="Resource List"></asp:Label>
                                <asp:TextBox ID="txtSearchResgp" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="lbtnFindResgp" runat="server" OnClick="lbtnFindResgp_Click"><i class="fa fa-search" > </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlreslistgp" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlreslistgp_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSpecificationgp" runat="server" CssClass="control-label" Text="Specification"></asp:Label>
                                <asp:DropDownList ID="ddlResSpcfgp" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkselectgp" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkselectgp_Click">Select</asp:LinkButton>
                                <asp:LinkButton ID="lnkselectgpAll" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkselectgpAll_Click">Select All</asp:LinkButton>
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"></asp:Label>

                            </div>
                        </div>

                    </div>
                      </div>
            </div>
                    <div class="card card-fluid">
                <div class="card-body">
                    <div class="col-md-12 col-sm-12 col-lg-12">

                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                        OnPageIndexChanging="grvacc_PageIndexChanging" OnRowDataBound="grvacc_RowDataBound"
                        OnRowDeleting="grvacc_RowDeleting" PageSize="15">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>
                                                                <asp:CommandField ControlStyle-Width="20px" ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText='<span class="fa fa-sm fa-trash fa" aria-hidden="true" ></span>&nbsp;' />

                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="MTRF">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmtrref" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gate Pass">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgatepref" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <%--<FooterTemplate>
                                    <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                        CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>

                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />--%>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MTRF  Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmtrfqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Balance Qty">

                                <ItemTemplate>

                                    <asp:Label ID="lblBalqty" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                    VerticalAlign="Middle" Font-Size="12px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText=" Receiving Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                </ItemTemplate>
                               <%-- <FooterTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True"
                                        CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>

                                </FooterTemplate>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                        Width="100px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                    VerticalAlign="Middle" Font-Size="12px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="spcfcode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgspcfcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
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

                     <div class="col-md-6">
                            <asp:Label ID="lblReqNarr" runat="server" Visible="false" CssClass="control-label" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtNarr" runat="server" Visible="false" CssClass="form-control form-control-sm" Rows="3" TextMode="MultiLine"></asp:TextBox>

                        </div>

                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



