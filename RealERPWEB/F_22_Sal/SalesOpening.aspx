<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SalesOpening.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/jquery.datepick.css" rel="stylesheet" />
    <script src="../Scripts/jquery.plugin.min.js"></script>
    <script src="../Scripts/jquery.datepick.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.13.1/themes/base/jquery-ui.css">
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>
    <%--    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $('.DateTimePicker').datepick({ dateFormat: 'dd.mm.yyyy' });
        }
        $(function () {
            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });

    </script>



    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblInformation" runat="server" CssClass="lblTxt lblName" Text="Receive No"></asp:Label>
                                <asp:TextBox ID="lblReceiveNo" runat="server" CssClass="inputTxt inpPixedWidth" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lbl211" runat="server" CssClass="smLbl_to" Text="Opening Date:"></asp:Label>
                                <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtOpeningDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-md-4 pull-right">
                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblproject" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                <asp:TextBox ID="txtSearchPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>


                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>




                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Unit Name"></asp:Label>
                                <asp:TextBox ID="txtSearchUnit" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>


                                <asp:LinkButton ID="imgbtnFindUnit0" CssClass="btn btn-primary okBtn" runat="server" OnClick="imgbtnFindUnit_Click" TabIndex="5">Ok</asp:LinkButton>

                            </div>

                        </div>
                    </div>
            </div>
            <asp:GridView ID="gvSOpening" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                Width="831px" AllowPaging="True"
                CssClass=" table-striped table-hover table-bordered grvContentarea"
                OnPageIndexChanging="gvSOpening_PageIndexChanging" OnRowDataBound="gvSOpening_RowDataBound">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pactcode" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvPactcode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UserCode" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvUsircode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name">
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvProjectName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Item">
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                Width="200px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit ">
                        <ItemTemplate>
                            <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Size">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblgvusize" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px">
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:Label ID="txtgvCustName" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                Width="120px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Schedule  Amt">
                        <ItemTemplate>
                            <asp:Label ID="lblgvschamt" runat="server" BorderColor="#99CCFF"
                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                Style="text-align: right; background-color: Transparent"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                Width="65px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:Label ID="lgvFchamtAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Opening Amt">
                        <ItemTemplate>
                            <asp:TextBox ID="txtopnamt" runat="server" BorderColor="#99CCFF"
                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                Style="text-align: right; background-color: Transparent"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;-#,##0; ") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:Label ID="lgvFToAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Opening Amt(Consolidate)">
                        <ItemTemplate>
                            <asp:Label ID="txtopnamt02" runat="server" BorderColor="#99CCFF"
                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                Style="text-align: right; background-color: Transparent"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt1")).ToString("#,##0;-#,##0; ") %>'
                                Width="65px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:Label ID="lgvFToAmt02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>





                    <asp:TemplateField HeaderText="Opening Amt. (Details)">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label ID="lgvFToAmtdet" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:LinkButton ID="lbtopnamtdet" runat="server" CommandArgument="lbtopnamtdet"
                                OnClick="lbtopnamtdet_Click" Style="text-align: right;"
                                Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndet")).ToString("#,##0;-#,##0;") %>'
                                Width="70px"></asp:LinkButton>
                        </ItemTemplate>

                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>

                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>
            <hr class=" hrline" />
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="row">
                        <asp:Panel ID="pnlgenMrr" runat="server" Visible="False">
                            <div class=" form-group">
                                <div class="col-md-3">
                                    <asp:Label ID="Label8" runat="server" CssClass="lblName lblTxt" Text="No Of MR."></asp:Label>
                                    <asp:TextBox ID="txtnofomrr" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblPactCodeAUsircode" runat="server" Visible="False"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>



                        </asp:Panel>

                        <asp:Panel ID="panelexcel" runat="server" Visible="False">
                            <div class=" form-group">
                                <div class="col-sm-3 col-md-3 col-lg-3">
                                    <asp:Panel ID="pnlxcel" runat="server">
                                        <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exele :"></asp:Label>
                                        <div class="uploadFile">
                                            <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                        </div>

                                    </asp:Panel>
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <asp:LinkButton ID="btnexcuplosd" runat="server" OnClick="btnexcuplosd_OnClick" CssClass=" btn btn-danger primarygrdBtn" Text="Upload Exel"></asp:LinkButton>
                                </div>

                                <div class="clearfix"></div>
                            </div>



                        </asp:Panel>



                        <div class="form-group">
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px"
                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. MR" />
                            </div>

                            <div class="col-md-3">
                                <asp:CheckBox ID="chkexcel" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px"
                                    OnCheckedChanged="chkexcel_OnCheckedChanged" Text="Excel Generate" />
                            </div>


                            <div class="col-md-1 pull-right">
                                <asp:LinkButton ID="lbtnBack0" runat="server" CssClass="btn btn-info primaryBtn" OnClick="lbtnBack_Click" Style="color: #000">Back</asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvMoney" runat="server" AutoGenerateColumns="False"
                            OnRowDeleting="gvMoney_RowDeleting" ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowCancelingEdit="gvMoney_RowCancelingEdit" OnRowEditing="gvMoney_RowEditing"
                            OnRowUpdating="gvMoney_RowUpdating">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="MR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvmrno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MR.(Manual)">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrefid" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChequeno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbankna" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbTotal_Click"> Total </asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBrance" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay Date">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdateMoney" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateMoney_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaydate" runat="server" BorderColor="#99CCFF" autocomplete="off"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent" CssClass="datepicker"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="70px"></asp:TextBox>

                                        <%--<cc1:CalendarExtender ID="txtgvpaydate_CalendarExtender" runat="server" BehaviorID="txtgvpaydate_CalendarExtender" Format="dd-MMM-yyyy" TargetControlID="txtgvpaydate" />--%>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Paid Amt." Visible="True">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFPaidamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaidamount" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Replace Chq No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrRpChq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "repchqno")) %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Recon Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrecndate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" autocomplete="off"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt"))%>'
                                            Width="70px" CssClass="datepicker"></asp:TextBox>
                                        <%--<cc1:CalendarExtender ID="txtgvrecndate_CalendarExtender" runat="server" BehaviorID="txtgvrecndate_CalendarExtender" Format="dd-MMM-yyyy" TargetControlID="txtgvrecndate" />--%>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" CancelText="Can" UpdateText="Up" />

                                <asp:TemplateField HeaderText="Reconcillation Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreconbankdesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvreconbank" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Pay Tpe">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaytypedesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvpaytype" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Installment  Type">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvinsType" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinsTypedesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Cllection From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColldesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrmd")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvColl" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Receive  Type">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvRecType" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRecTypedesc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recTyped")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill No">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlgvBillNo" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Reconcillation Bank" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreconbank" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Pay type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpaytype" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytypecod")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Cllection From" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColl" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive  Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRecType" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recType")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Installment  Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinsType" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "instype")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill No" Visible="false">
                                    <%--<EditItemTemplate>
                                                <asp:DropDownList ID="ddlgvBillNo" runat="server" Width="100px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillnomain" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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


    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
