<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktSalsPaymentPro.aspx.cs" Inherits="RealERPWEB.F_21_MKT.MktSalsPaymentPro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-5 pading5px">

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                        <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select inputTxt" Width="350px" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                                        <asp:Label ID="lmsg" runat="server" Visible="False" Width="350px" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>

                                    </div>

                                    <div class=" col-md-7  pading5px">
                                        <asp:Label ID="lblSearch" CssClass=" smLbl_to" runat="server" Text="Unit Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchunit" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnsrchunit" runat="server" CssClass="btn btn-primary srearchBtn" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                </div>
                        </fieldset>

                        <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                            OnClick="lbtnusize_Click" Style="text-align: right; height: 14px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Booking Money">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcparking" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mgt Booking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>


                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">

                                <br />
                                <div class="form-horizontal">

                                    <div class="form-group">

                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lperInfo" runat="server" CssClass="btn btn-info primaryBtn" Text="Personal Information"></asp:Label>
                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>


                                        </div>

                                        <div class=" col-md-8  pading5px">

                                            <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-success primaryBtn"
                                                    OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" Width="510px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Height="20px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <br />
                                <br />

                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-info primaryBtn" Text="Revenue Information"></asp:Label>
                                        <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvGcod" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="40px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTotalCost" runat="server" CssClass="btn btn-primary primaryBtn"
                                                            OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Item">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lFinalUpdateCost" runat="server"
                                                            OnClick="lFinalUpdateCost_Click" CssClass="btn btn-success primaryBtn"> Update Cost</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="170px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                            Width="40px" Font-Size="11px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Size">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px" Font-Size="11px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRate" runat="server" ForeColor="Black"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvuamt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;-#,##0; ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Remarks">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Height="18px" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                            Width="80px" Font-Size="11px"></asp:TextBox>
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
                                        <br />
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="ldT" runat="server" CssClass="lblTxt lblName"
                                                        Text="Discount (TK)"></asp:Label>
                                                    <asp:Label ID="ldiscountt" runat="server" CssClass="lblTxt"></asp:Label>
                                                </div>


                                                <div class="col-md-4">
                                                    <asp:Label ID="ldp" runat="server" CssClass="lblTxt lblName"
                                                        Text="Discount (%)"></asp:Label>
                                                    <asp:Label ID="ldiscountp" runat="server" CssClass="lblTxt"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:Panel ID="Panel2" runat="server">


                                                <div class="col-md-12 pading5px">

                                                    <div class="form-group">

                                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Sales Team</asp:Label>


                                                        <asp:LinkButton ID="ibtnFindSalesteam" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                        <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="chzn-select ddlPage margin5px" TabIndex="12" Style="width: 280px; float: left">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-12 pading5px">

                                                    <div class="form-group">
                                                        <asp:Label ID="lblCollection" runat="server" CssClass=" smLbl_to">Collection Team</asp:Label>
                                                        <asp:LinkButton ID="ibtnFindCollectionteam" CssClass="btn btn-primary srearchBtn" Visible="false" runat="server" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        <asp:DropDownList ID="ddlCollectionTeam" runat="server" CssClass="chzn-select ddlPage margin5px" TabIndex="12" Style="width: 280px;" >
                                                        </asp:DropDownList>

                                                    </div>

                                                </div>


                                                <div class="col-md-12 pading5px">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Aggrement Date</asp:Label>
                                                        <asp:TextBox ID="txtAggrementdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtAggrementdate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAggrementdate"></cc1:CalendarExtender>

                                                        <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Handover Date</asp:Label>
                                                        <asp:TextBox ID="txthandoverdate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txthandoverdate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txthandoverdate"></cc1:CalendarExtender>


                                                    </div>

                                                    <div class="clearfix"></div>
                                                </div>

                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <div class="col-md-5"></div>
                                                        <asp:LinkButton ID="lbtnUpdateCAST" runat="server" CssClass="btn  btn-danger primaryBtn" OnClick="lbtnUpdateCAST_OnClick" >Update</asp:LinkButton>

                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <div class="clearfix"></div>
                                        </div>
                                        
                                    </div>
                                </div>

                                <br />
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <asp:Label ID="lPays" runat="server" CssClass="btn btn-info primaryBtn" Text="Payment Shedule"></asp:Label>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>


                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="223px" OnRowDeleting="gvPayment_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="450px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatpayment" runat="server"
                                                    OnClick="lUpdatpayment_Click" CssClass="btn btn-success primaryBtn">Update Payment Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No Of Installment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvinsnum" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insnum")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvduration" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lTotalPayment" runat="server" CssClass="btn btn-primary primaryBtn"
                                                    OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="black" Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtoAmt" runat="server"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px" Font-Size="11px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lftoAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="black" Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        </asp:MultiView>

                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                        </cc1:ListSearchExtender>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




