
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccRentCollUpdate.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccRentCollUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName" Text="Current Voucher"></asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="true" ReadOnly="True" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="true" ReadOnly="True" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Voucher Date"></asp:Label>

                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-3 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        <asp:Label ID="lblmsg" CssClass=" btn btn-danger  primaryBtn" runat="server" Visible="false"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>

                <asp:Panel ID="pnlBill" runat="server" Visible="False">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">


                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblMRList" runat="server" CssClass="lblTxt lblName" Text="MR List"></asp:Label>
                                        <asp:TextBox ID="txtsrchMRno" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchMRno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgSearchMRno_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlMRList" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lbtnSelectMR" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnSelectMR_Click">Select MR</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" Width="937px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ConActcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgccactcde" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrks" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="qty" Visible="False">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBaName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay/Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Control Act Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdramt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Other ref.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvsirialno" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirialno")) %>'
                                            Width="70px"></asp:TextBox>
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
                    </div>
                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class=" form-group">
                                <div class="col-md-12 pading5px">
                                    <asp:Label ID="lblRefNum" runat="server" CssClass="smLbl_to" Text="Ref./Cheq No/Slip No."></asp:Label>
                                    <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                    <asp:Label ID="lblSrInfo" runat="server" CssClass="smLbl_to" Text="Narration"></asp:Label>

                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1 pading5px">
                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblName lblTxt" Text="Narration"></asp:Label>


                                </div>

                                <div class="col-md-7 pading5px">
                                    <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                        CssClass="form-control" Rows="5" Height="150" TextMode="MultiLine"
                                        TabIndex="21"></asp:TextBox>
                                </div>
                                <div class="col-md-2 pading5px">
                                    <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                        OnClick="lnkFinalUpdate_Click"
                                        TabIndex="22">Final Update</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </fieldset>



                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

