<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurMktSurvey.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurMktSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

        });

    </script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


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
            <div class="card">
                <div class="card-header">
                    <div class="row">


                        <div class="col-md-3 d-none">

                            <asp:TextBox ID="txtInfo" runat="server" CssClass="form-control form-control-sm" TabIndex="4"></asp:TextBox>

                            <div class="colMdbtn">
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblInformation" runat="server" CssClass="form-label" Text="Information for"></asp:Label>
                            <asp:DropDownList ID="ddlSurveyType" runat="server" AutoPostBack="True" CssClass="chzn-select form-control inputTxt" TabIndex="6" OnSelectedIndexChanged="ddlSurveyType_SelectedIndexChanged" Style="width: 336px;">
                                <asp:ListItem Value="1">Market Survey Report</asp:ListItem>
                                <asp:ListItem Value="2">Material Wise Suppliers List</asp:ListItem>
                                <asp:ListItem Value="3">Supplier Wise Materials List</asp:ListItem>
                            </asp:DropDownList>

                        </div>




                        <div class="col-md-3 pull-right">
                            <div class="msgHandSt">
                                <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger " Visible="false"></asp:Label>
                            </div>
                        </div>


                    </div>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <div class="row mt-4">


                                <div class="col-md-1">
                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="form-label" Text="Survey No."></asp:Label>
                                    <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="form-control form-control-sm" Text="MSR00-"></asp:Label>

                                </div>
                                <div class="col-md-1" style="margin-top: 22px;">
                                    <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="form-control form-control-sm">00000</asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lbl211" runat="server" CssClass="form-label" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd.MM.yyyy " TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>


                                </div>
                                <div class="col-md-2" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass="btn  btn-sm btn-primary" OnClick="lbtnMSROk_Click" TabIndex="2">Ok</asp:LinkButton>
                                </div>

                                <div class="col-md-2  pull-right">
                                    <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="form-label" OnClick="ImgbtnFindPreMR_Click"
                                        TabIndex="3">Pre. MSR. List</asp:LinkButton>

                                    <asp:DropDownList ID="ddlPrevMSRList" runat="server" Width="180px" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>

                                </div>


                            </div>
                            <asp:Panel ID="Panel1" runat="server" Visible="False">

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel4" runat="server">

                                        <div class="row mt-2">
                                            <div class="col-md-3 d-none">

                                                <asp:TextBox ID="txtMSRResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindMat" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindMat_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Materials List"></asp:Label>
                                                <asp:DropDownList ID="ddlMSRRes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMSRRes_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                                </asp:DropDownList>
                                            </div>




                                            <div class="col-md-3 d-none">

                                                <asp:TextBox ID="txtsrchSpecification3" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindSpecificationms" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecificationms_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-3 pading5px  asitCol4">
                                                <asp:Label ID="lblspecificationms" runat="server" CssClass="lblTxt lblName" Text="Specification"></asp:Label>
                                                <asp:DropDownList ID="ddlSpecificationms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecificationms_SelectedIndexChanged" CssClass=" chzn-select  form-control inputTxt" TabIndex="6">
                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-md-3 d-none">

                                                <asp:TextBox ID="txtMSRSupSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindSup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSup_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Supplier List"></asp:Label>
                                                <asp:DropDownList ID="ddlMSRSupl" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-1" style="margin-top: 22px;">
                                                <asp:LinkButton ID="lbtnMSRSelect" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnMSRSelect_Click" TabIndex="2">Select</asp:LinkButton>

                                            </div>

                                            <div class="col-xs-1 col-md-1 col-lg-1" style="margin-top: 13px;">
                                                <a class="btn btn-sm btn-primary" href='<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=SurveyLink")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">Survey Link</a>

                                            </div>
                                        </div>




                                    </asp:Panel>
                                </div>
                                <div class="row mt-4">
                                    <asp:GridView ID="gvMSRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="274px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Materials Description ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="120px">
                                                              
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlMSRPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                        Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlMSRPageNo_SelectedIndexChanged"
                                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                        Width="120px">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier Name">

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnMSRTotal" runat="server" CssClass="btn btn-sm  btn-primary  primarygrdBtn" OnClick="lbtnMSRTotal_Click">Total</asp:LinkButton>

                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Specification">

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnMSRUpdate" runat="server" CssClass="btn btn-sm btn-danger" OnClick="lbtnMSRUpdate_Click">Final Update</asp:LinkButton>

                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvspcfdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="90px">
                                                              
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Purchase Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Credit Period (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Period (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRDel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delivery")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Concern  Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRCperson" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRPhone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRMobile" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                                <div class="row mt-2">


                                    <div class="col-md-3 ">
                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="form-label" Text="Approved By:" Visible="False"></asp:Label>
                                        <asp:Label ID="lblApprovalDate" runat="server" CssClass="form-label" Text="Approv.Date" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-md-4">

                                        <asp:Label ID="lblPreparedBy" runat="server" CssClass="form-label" Text="Prepared By" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                    </div>

                                </div>
                                <div class="row mt-2">

                                    <div class="col-md-6 mt-2">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblReqNarr" runat="server" CssClass="form-label" Text="Vendor Justification:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtMSRNarr" runat="server" class="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtApprovedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtApprovalDate" runat="server" class="form-control" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtPreparedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                    </div>
                                    </td>
                                         
                                        
                                </div>





                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblResList1" runat="server" CssClass="lblTxt lblName" Text="Materials"></asp:Label>
                                                <asp:TextBox ID="txtResSearch1" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindRes1" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes1_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-4 pading5px  asitCol4">
                                                <asp:DropDownList ID="ddlResList1" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="6" OnSelectedIndexChanged="ddlResList1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnSelectRes1" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lbtnSelectRes1_Click" TabIndex="2">Ok</asp:LinkButton>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Specification"></asp:Label>
                                                <asp:TextBox ID="txtsrchSpecification" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindSpecification" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecification_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                                </asp:DropDownList>
                                            </div>


                                        </div>

                                    </div>
                                </fieldset>

                            </div>
                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblSuplList1" runat="server" CssClass="lblTxt lblName" Text="Supplier"></asp:Label>
                                                    <asp:TextBox ID="txtSuplSearch1" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ImgbtnFindSupl1" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSupl1_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:DropDownList ID="ddlSupl1" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="6" Width="500">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 pading5px">
                                                    <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lbtnSelectSupl1_Click" TabIndex="2">Select</asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnSelectSupAll" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lbtnSelectSupAll_Click" TabIndex="2">Select all</asp:LinkButton>

                                                </div>

                                            </div>

                                        </div>
                                    </fieldset>

                                    <asp:GridView ID="gvSuplInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="16px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDeleting="gvSuplInfo_RowDeleting">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Res Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCodsup" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplCodsup" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlSuplPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                        Font-Bold="True" Font-Size="14px" Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                        Width="150px" OnSelectedIndexChanged="ddlSuplPageNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplDesc1" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                        Width="250px"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnSuplUpdate" runat="server" CssClass="btn btn-sm btn-danger primaryBtn" OnClick="lbtnSuplUpdate_Click">Final Update</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "rmrks").ToString() %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <div class="row mt-2">

                                <div class="col-md-3 d-none">
                                    <asp:TextBox ID="txtSuplSearch2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="ImgbtnFindSupl2" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSupl2_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSuplList2" runat="server" CssClass="lblTxt lblName" Text="Supplier"></asp:Label>

                                    <asp:DropDownList ID="ddlSupl2" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1" style="margin-top: 22px;">
                                    <asp:LinkButton ID="lbtnSelectSupl2" runat="server" CssClass="btn btn-sm btn-primary " OnClick="lbtnSelectSupl2_Click" TabIndex="2" Style="margin-left: 140px;">Ok</asp:LinkButton>

                                </div>



                            </div>
                            <div class="col-md-12">
                                <div class="row">
                                    <asp:Panel ID="Panel3" runat="server" Visible="False">
                                        <div class="row mt-2">

                                            <div class="col-md-3 d-none">
                                                <asp:TextBox ID="txtResSearch2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindRes2" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes2_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lblResList2" runat="server" CssClass="form-label" Text="Material"></asp:Label>

                                                <asp:DropDownList ID="ddlResList2" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlResList2_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 ml-5" style="margin-top:21px;">
                                                <asp:LinkButton ID="lbtnSelectRes2" runat="server" CssClass="btn btn-sm btn-primary   primarygrdBtn" OnClick="lbtnSelectRes2_Click" TabIndex="2">Select</asp:LinkButton>

                                            </div>
                                            <div class="col-md-2" style="margin-top:21px;">
                                                <asp:LinkButton ID="lbtnSelectResAll" runat="server" CssClass="btn btn-sm btn-primary   primarygrdBtn" OnClick="lbtnSelectResAll_Click" TabIndex="2">Select all(Spec)</asp:LinkButton>

                                            </div>
                                            <div class="col-md-2" style="margin-top:21px;">
                                                <asp:LinkButton ID="lbtnSelectReaSpesAll" runat="server" CssClass="btn btn-sm btn-primary   primarygrdBtn" OnClick="lbtnSelectReaSpesAll_Click" TabIndex="2">Select all(Mat)</asp:LinkButton>

                                            </div>



                                            <div class="col-md-3 pading5px  asitCol3 d-none">
                                                <asp:TextBox ID="txtsrchSpecification2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindSpecification2" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecification2_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-4" >
                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Specification"></asp:Label>

                                                <asp:DropDownList ID="ddlSpecification02" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                                </asp:DropDownList>
                                            </div>

                                            </div>
                                        <div class="swal2-grow-row mt-2">
                                            <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="189px" OnRowDeleting="gvResInfo_RowDeleting">
                                                <PagerSettings Visible="False" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSuplCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                    <asp:TemplateField HeaderText="Res Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Materials">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResDesc1" runat="server"
                                                                Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                                Width="250px">
                                                                    
                                                                    
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlResPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                                Font-Bold="True" Font-Size="14px" Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                                Width="150px" OnSelectedIndexChanged="ddlResPageNo_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrsirunit" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Rate">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-sm btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>

                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="90px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvResRemarks1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "rmrks").ToString() %>' Width="150px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

