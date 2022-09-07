<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktWorkOrderEntry.aspx.cs" Inherits="RealERPWEB.F_28_MPro.MktWorkOrderEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .grvContentarea {
            margin-right: 0px;
        }
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

            <div class="card card-fluid mb-2">
                <div class="card-body">
                    <asp:Panel ID="pnlPurOrderDet" CssClass="mt-2" runat="server">
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblOrderDate" runat="server" class="control-label" Text="Order Date"></asp:Label>
                                    <asp:TextBox ID="txtCurOrderDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurOrderDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd.MM.yyyy" TargetControlID="txtCurOrderDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblOrderNo" runat="server" class="control-label" Text="Order No"></asp:Label>
                                    <asp:Label ID="lblCurOrderNo1" runat="server" class="control-label" Text="POR00- "></asp:Label>
                                    <asp:TextBox ID="txtCurOrderNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNo" runat="server" class="control-label" Text="Ref. No"></asp:Label>
                                    <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lbltxtissueno" runat="server" class="control-label" Text="P.O #"></asp:Label>
                                    <asp:TextBox ID="lblissueno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-1 col-md-1 col-lg-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>

                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:LinkButton ID="lbtnPrevOrderList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevOrderList_Click">Previous PO</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevOrderList" runat="server" Width="180px" CssClass="form-control chzn-select" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-header">
                    <asp:Panel ID="pnlSupplier" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="basic-addon2">Supplier:</span>
                                    </div>
                                    <asp:TextBox ID="txtsrchSupplier" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <asp:LinkButton ID="imgSearchOrderno" runat="server" CssClass="btn btn-primary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchOrderno_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4 mb-3">
                                <asp:DropDownList ID="ddlSuplierList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSuplierList_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="card-body" style="min-height: 400px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="PurApp" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAprovInfo" runat="server"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="482px" CssClass="table-striped table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkitem" runat="server" Style="text-align: center"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="1" %>'
                                                    Width="30px" />
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

                                        <asp:TemplateField HeaderText="PR Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtype")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Marketing Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMktTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mkttype")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSupCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
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

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplDesc" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc1").ToString() %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnSelectedOrdr" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnSelectedOrdr_Click">Select Order</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProjDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pur. Req. Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpfDesc0" runat="server" Font-Bold="False" 
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")) + "</B>"%>'
                                                    Width="350px"></asp:Label>
                                                <asp:TextBox ID="txtgvRsirdetDesc" runat="server" Font-Bold="False"  TextMode="MultiLine" Rows="5" ReadOnly="true" Width="350px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdetdesc"))%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Marketing Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mkttypedesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo3" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRefno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approv. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprvQty" runat="server" Style="text-align: right;" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprvqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRate" runat="server" Style="text-align: right;" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderAmt" runat="server" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="WorkOrdr" runat="server">
                            <div class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <div class="input-group input-group-sm input-group-alt">
                                            <div class="input-group-prepend ">
                                                <span class="input-group-text">Subject:</span>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" class="form-control inputTxt"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6">
                                    <div class="form-group">
                                        <div class="input-group input-group-sm ">
                                            <span class="input-group-prepend">
                                                <span class="input-group-text">Dear Sir,</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6 mb-2">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-prepend">
                                                <span class="input-group-text">:</span>
                                            </span>
                                            <asp:TextBox ID="txtLETDES" runat="server" class="form-control inputTxt"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging" CssClass="btn btn-primary checkBox btn-sm" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="PnlCharging" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div class="input-group input-group-sm">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon4">Project</span>
                                            </div>
                                            <asp:TextBox ID="txtSrchProjectName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchProject_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-lg-4 mb-3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary okBtn btn-sm" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div class="input-group input-group-sm">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text" id="basic-addon5">Charge:</span>
                                            </div>
                                            <asp:TextBox ID="txtSrchCharge" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <div class="input-group-prepend">
                                                <asp:LinkButton ID="imgSearchCharge" runat="server" CssClass="btn btn-primary btn-sm" ToolTip="Find Supplier" OnClick="imgSearchCharge_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-lg-4 mb-3">
                                        <asp:DropDownList ID="ddlCharge" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                    </div>

                                </div>
                            </asp:Panel>


                            <div class="table-responsive mb-2">
                                <asp:GridView ID="gvOrderInfo" runat="server"
                                    AutoGenerateColumns="False" ShowFooter="true"
                                    CssClass="table-striped table-bordered grvContentarea" OnRowDataBound="gvOrderInfo_RowDataBound">

                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrjCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PR Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prtype")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Marketing Type Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMktTypeCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mkttype")) %>'
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

                                        <asp:TemplateField HeaderText="Description of Products">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server" CssClass="d-block"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "acttypedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "prtypedesc")).Trim().Length>0 ?  "<br>" : "")+"&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "acttypedesc")).Trim()+"</B>": "")
                                                                         
                                                                    %>'></asp:Label>
                                                <asp:TextBox ID="txtgvRsirdetDesc1" runat="server" Font-Bold="False" CssClass="from-control" Width="450px" TextMode="MultiLine" Rows="5"  
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdetdesc"))%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-warning btn-sm form-control" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ref No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefno1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdatePurOrder" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return Confirmation();" OnClick="lbtnUpdatePurOrder_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pur. Appr. <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPurApprvQty" runat="server" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprvqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order <br> Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOrderQty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:CheckBox ID="lblfchkbox" Text=" Forward" runat="server" Width="70px"></asp:CheckBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px"  Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True"  HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvOrderAmt" runat="server" BackColor="Transparent" BorderStyle="none"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"  Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTOrderAmt" runat="server" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True"  HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-4">
                                    <asp:LinkButton ID="btnSendmail" CssClass="btn btn-info primaryBtn btn-sm" runat="server" OnClick="btnSendmail_Click">Send Email</asp:LinkButton>
                                </div>

                                <div class="col-md-2">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend ">
                                            <span class="input-group-text">Advanced</span>

                                        </div>
                                        <asp:TextBox ID="txtadvAmt" runat="server" class="form-control" Style="text-align: right"></asp:TextBox>


                                    </div>



                                </div>
                            </div>

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
                                        <div class="form-group" id="divterms" runat="server">
                                            <div class="form-group" style="margin-top: 10px;">
                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Type:" Visible="false"></asp:Label>
                                                <div class="col-md-2 pading5px">
                                                    <asp:DropDownList ID="ddltypecod" CssClass="form-control inputTxt" runat="server" Visible="false">
                                                        <asp:ListItem Value="001">Service Terms
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="002">Products Terms</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:LinkButton ID="lnkselect" runat="server" Visible="false" CssClass="btn btn-primary primarygrdBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2 pading5px">
                                                    <div class="form-group">
                                                        <div class="input-group input-group-alt input-group-sm">
                                                            <div class="input-group-prepend ">
                                                                <span class="input-group-text">Terms & Condition</span>
                                                            </div>
                                                            <asp:LinkButton ID="lnkAddTerms" runat="server" CssClass=" btn btn-sm btn-primary" OnClick="lnkAddTerms_Click" ToolTip="Add New Terms and Conditions"> <i class=" fa  fa-plus" aria-hidden="true"></i> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="lssircode" runat="server" Visible="False"></asp:Label></td>
                                                </div>
                                            </div>
                                            <asp:GridView ID="gvOrderTerms" runat="server"
                                                AutoGenerateColumns="False" PageSize="30" ShowFooter="true"
                                                CssClass="table table-striped table-bordered grvContentarea" Width="500px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndelterm" runat="server" ToolTip="Delete Terms & Condition" OnClientClick="javascript:return FunConfirm();" OnClick="lbtndelterm_Click"> <i class="fa fa-trash" style="color:red;"  aria-hidden="true"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTermsID" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: left; background-color: Transparent"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>'
                                                                Width="150px"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" 
                                                                Text=" : "></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: left; background-color: Transparent"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>'
                                                                Width="250px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: left; background-color: Transparent"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>'
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle CssClass="grvFooterNew" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeaderNew" />
                                            </asp:GridView>
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
