<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ChequeSignSheet.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.ChequeSignSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        .fade-in {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            //            var gv1 = $('#<%=this.dgv1.ClientID %>');


            //            gv1.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

        }


        function PrintRpt(printype) {

            window.open('../RptViewer.aspx?PrintOpt=' + printype + '', '_blank');
        }






        function PrintRptCheque(vouanptype) {
            window.open('../F_17_Acc/AccPrint.aspx?' + vouanptype + '', '_blank');

        }




    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <div class="col-md-8 pading5px">
                            <div class="form-group">
                                <div class="col-md-8 pading5px">
                                    <asp:Label ID="lblBankCode" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                    <asp:TextBox ID="txtserchBankName" Visible="false" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                    <asp:LinkButton ID="imgbtnSrchBank" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnSrchBank_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="True" Width="390px" CssClass="inputTxt chzn-select"
                                        TabIndex="11" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn"
                                        OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                    <asp:CheckBox ID="chkCrVou" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Text="Current Voucher" CssClass="btn btn-sm checkBox" Checked="true" />
                                    <asp:CheckBox ID="chkPrint" runat="server" TabIndex="10" Text="Cheque Print" CssClass="btn btn-sm checkBox" />
                                    <asp:CheckBox ID="checkpb" runat="server" Visible="false" CssClass="btn btn-primary checkBox pull-right" Text="Pubali Bank" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                                    <asp:TextBox ID="txtProjectSearch" runat="server" Visible="false" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                    <asp:LinkButton ID="ImgbtnFindProjectName" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjectName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" Width="390px" CssClass="chzn-select inputTxt"
                                        TabIndex="11">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 pading5px">

                                    <asp:Label ID="lblDate" runat="server" CssClass=" smLbl_to" Text="Vou. Date"></asp:Label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputDateBox" Width="62px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                    <asp:Label ID="lblRef" runat="server" CssClass=" smLbl_to" Text="Ref. No"></asp:Label>
                                    <asp:TextBox ID="txtserchmrf" runat="server" Style="width: 55px" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                    <asp:CheckBox ID="withoutchqdate" runat="server" Text="Without Chq Date" Visible="false" CssClass="btn btn-primary checkBox" AutoPostBack="true" />


                                    <br />
                                    <div class="clearfix"></div>

                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    <asp:CheckBox ID="ChboxPayee" runat="server" Style="margin-left: 10px;" Text="A/C Payee" />
                                    <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                    <asp:Label ID="lblVoun" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>

                                    <asp:Label ID="lblVounum" runat="server" Visible="False"></asp:Label>
                                </div>

                                <div class="clearfix"></div>
                            </div>


                        </div>

                        <div class="col-md-4 pading5px">
                            <asp:ListBox ID="lstBillList" runat="server" AutoPostBack="True"
                                BackColor="#DFF0D8" Font-Bold="True" Font-Size="12px" Height="80px"
                                OnSelectedIndexChanged="lstBillList_SelectedIndexChanged"
                                SelectionMode="Multiple" TabIndex="12" CssClass="form-control"></asp:ListBox>
                        </div>

                    </div>




                    <div class="row">

                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgv1_RowDataBound"
                            ShowFooter="True" PageSize="100" CssClass="col-md-12 table-striped table-hover table-bordered grvContentarea fade-in "
                            OnRowCancelingEdit="dgv1_RowCancelingEdit" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating" Width="800px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="slnum" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvslNum" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actcode Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cactcode Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCactCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" OnClick="lbtnTotal_Click" CssClass="btn btn-info btn-sm" runat="server">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "<span class=gvdesc>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") + "</span>"  %>'
                                            Width="250px">
                                                            
                                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount" Visible="false">
                                    <FooterTemplate>
                                        <%--  <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" Font-Size="14px"
                                            OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-primary primaryBtn">Total :</asp:LinkButton>--%>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAppAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCactDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cactdesc").ToString() %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCactcode" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Amount">


                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFchqamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>



                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAmount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tax">


                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtax" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>



                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtax" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tax")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Net Amount">


                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnetamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>



                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnetAmount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Cheque No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChqNo" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChqdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chqdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="60px"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvChqdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvChqdate"></cc1:CalendarExtender>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Narration">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvNarr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                TextMode="MultiLine" BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narr")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <%-- <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAll_CheckedChanged" Width="20px" />

                                        </HeaderTemplate>


                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvmrno" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                --%>
                                <asp:TemplateField HeaderText="Voucher Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNewVoNum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRefno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Payto" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvPayto" runat="server" BorderWidth="0px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="100px" TextMode="MultiLine"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            </Columns>


                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </div>
                    <div class="row">
                        <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName" Style="width: 600px; color: blue; text-align: left;"></asp:Label>

                    </div>

                    <div class="row">

                        <asp:Panel ID="PnlNarration" Visible="false" runat="server">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="from-group row">                                        
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                                <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"  Width="120"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="from-group">
                                                <asp:Label ID="lblchelist" runat="server" CssClass="lblTxt lblName" Text="Cheque List"></asp:Label>
                                                <asp:DropDownList ID="ddlcheque" runat="server" CssClass="ddlPage chzn-select" Style="width: 120px; margin-left: 4px;" AutoPostBack="True" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="lblPayto" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                            <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" Width="260"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                            </cc1:AutoCompleteExtender>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Payee Type"></asp:Label>
                                            <asp:DropDownList ID="ddlpayeelist" runat="server" Visible="true" CssClass="form-control form-control-sm" AutoPostBack="true" Style="width: 260px;">
                                                </asp:DropDownList>
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
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-sm btn-danger"
                                                OnClick="btnUpdate_Click">Update</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary"
                                                OnClick="lnkOk_Click">Confirm</asp:LinkButton>
                                        </div>

                                    </div>

                                </div>


                            </fieldset>
                        </asp:Panel>
                    </div>

                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
