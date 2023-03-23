<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurMTReqGatePass.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurMTReqGatePass" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            var gridview = $('#<%=this.gvAprovInfo.ClientID %>');
            $.keynavigation(gridview);

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lCurAppdate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurAprovDate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurAprovDate_CalendarExtender" runat="server"
                                Format="dd-MM-yyyy" TargetControlID="txtCurAprovDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lcurGatePassNo" runat="server" CssClass="smLbl_to" Text="Gate Pass No"></asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="lblGatePassNo1" runat="server" CssClass="form-control form-control-sm" Text="GPN00-" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtGatePassNo2" runat="server" CssClass="form-control form-control-sm disabled" ReadOnly="True">00000</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblgatepmanualno" runat="server" CssClass="control-label" Text="Gate Pass(Manual)"></asp:Label>
                            <asp:TextBox ID="txtGatemPassNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>

                        <div class="col-md-3" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="ImgbtnFinGatePass" runat="server" CssClass="text-primary" OnClick="ImgbtnFinGatePass_Click">
                                    <i class="fa fa-search"></i> Previous List
                            </asp:LinkButton>
                            <asp:TextBox ID="txtGatePassNo" runat="server" TabIndex="7" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" id="pnlproj" runat="server" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectFromList" runat="server" CssClass="control-label" Text="From"></asp:Label>
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="To"></asp:Label>
                                <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrject" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnPrject_Click">Show</asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <div class="row" id="Panel1" runat="server" visible="false">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server" CssClass="control-label" Text="Requisitions"></asp:Label>
                                <asp:DropDownList ID="ddlResList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList2" runat="server" CssClass="control-label" Text="Resources"></asp:Label>
                                <asp:DropDownList ID="ddlResourcelist" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlResourcelist_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSpecification" runat="server" CssClass="control-label" Text="Specification"></asp:Label>
                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectRes_Click">Select</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                            </div>
                        </div>

                        <div class="col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvAprovInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="16px" OnRowDeleting="gvAprovInfo_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reqno" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtfpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvttpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Description of Materials">




                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))   %>'
                                                Width="100px">
                                            </asp:Label>



                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspecification" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlspecification" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="45px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqNo1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn"
                                                OnClick="lbtnResFooterTotal_Click">Total :</asp:LinkButton>

                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRF No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmrfno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvacstockqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockbal")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Req. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmtrfqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdatePurAprov" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdatePurAprov_Click">Final Update</asp:LinkButton>

                                        </FooterTemplate>--%>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Bal. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Approved Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvaprovedQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "getpqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approved </br> " Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:CommandField ControlStyle-Width="20px" ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText='<span class="fa fa-sm fa-trash fa" aria-hidden="true" ></span>&nbsp;' />


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>

                        <div class="col-md-6">
                            <asp:Label ID="lblReqNarr0" runat="server" CssClass="control-label" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtgetpNarr" runat="server" CssClass="form-control form-control-sm" Rows="3" TextMode="MultiLine"></asp:TextBox>

                        </div>

                    </div>

                </div>
            </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
