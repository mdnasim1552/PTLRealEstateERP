<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSales.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="lblTxt lblName" Text="Voucher Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy " TargetControlID="txtEntryDate"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <asp:Panel ID="PnlMonDuePriod" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-3 pull-right">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>

                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblProname" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lnkOk0" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk0_Click" TabIndex="2">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblProname0" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Cheque No"></asp:Label>
                                            <asp:TextBox ID="txtSrcChequeNo" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="7"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ibtnSearchChequeno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSearchChequeno_Click" TabIndex="8"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>


                                </asp:Panel>
                            </div>
                        </fieldset>

                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" Width="937px" OnRowDataBound="dgv1_OnRowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
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
                                            Width="100px"></asp:Label>
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
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>' Width="110px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resouce">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvResource" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>' Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="" Visible="false">


                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlChq" CssClass="ddlPage chzn-select" runat="server" Width="80px" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Pay/ </br> Received </br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Control Act Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reconcilaition Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReconDat" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recondat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recondat")).ToString("dd-MMM-yyyy")%>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvReconDat_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvReconDat"></cc1:CalendarExtender>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="70px"></asp:Label>
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkvmrno" runat="server" AutoPostBack="true" OnCheckedChanged="chkvmrno_CheckedChanged"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbok" runat="server" Width="40px" CommandArgument="lbok" OnClientClick="return Confirmation();"
                                            OnClick="lbok_Click">Update</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNewVoNum" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrks" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other ref.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvsirialno" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirialno")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
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





                    <div class="row">
                        <asp:Panel ID="PnlNarration" runat="server">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3 ">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>

                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        </div>


                                        <div class="col-md-6 pading5px">

                                            <asp:Label ID="lblPayto" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                            <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" Width="130"></asp:TextBox>




                                        </div>





                                    </div>



                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>






                                    </div>
                                </div>

                            </fieldset>
                        </asp:Panel>
                    </div>




                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


