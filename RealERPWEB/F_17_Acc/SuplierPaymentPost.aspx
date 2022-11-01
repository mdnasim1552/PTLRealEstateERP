<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SuplierPaymentPost.aspx.cs" Inherits="RealERPWEB.F_17_Acc.SuplierPaymentPost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.txtScrchConCode.ClientID %>').focus();

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
          <%-- var gridview = $('#<%=this.dgv1.ClientID %>');
           $.keynavigation(gridview);--%>

            $('.chzn-select').chosen({
                search_contains: true,
                enable_escape_special_char: false
            });

        };




        function PrintRptCheque(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">

                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary" OnClick="lnkPrivVou_Click">Previous</asp:LinkButton>

                                        </div>
                                        <asp:TextBox ID="txtScrchPre" runat="server" CssClass="inputtextbox" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="colMdbtn pading5px pull-right">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Code"></asp:Label>
                                        <asp:TextBox ID="txtScrchConCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="chzn-select form-control " OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-5 pading5px ">

                                        <%-- <asp:Label ID="Label2" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>

                                        <asp:CheckBox ID="chkPrint" runat="server" TabIndex="10" Text="Cheque Print" CssClass="btn btn-primary checkBox" />--%>

                                        <asp:Panel ID="PanelChk" runat="server" Visible="False">
                                            <table>
                                                <tr>
                                                    <td class="style25">
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True"
                                                            CssClass="btn btn-primary primaryBtn chkBoxControl" OnCheckedChanged="chkPrint_CheckedChanged"
                                                            Text="Cheque Print" />
                                                    </td>
                                                    <td class="style29">
                                                        <asp:DropDownList ID="ddlChqList" runat="server" CssClass="form-control inputTxt"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>
                                            </table>
                                        </asp:Panel>


                                    </div>




                                </div>
                            </div>
                        </fieldset>

                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Supplier</asp:Label>
                                            <asp:TextBox ID="txtserchReCode" runat="server" CssClass=" inputTxt inputName inpPixedWidth" TabIndex="14"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkRescode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkRescode_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlresuorcecode" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="17" AutoPostBack="true" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-5 pading5px">

                                             <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkAdd" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkAdd_Click">Add Bill</asp:LinkButton>


                                            </div>

                                            <div class="msgHandSt pull-right">
                                                <asp:Label ID="lblmsg" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>
                                            </div>


                                        </div>




                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName">Bill No</asp:Label>
                                            <asp:TextBox ID="txtserchBill" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>



                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkBillNo0" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkBillNo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                       
                                 <%--           <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>--%>

                                               <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass="chzn-select form-control"  MaxDropDownHeight="200"
                                                        TabIndex="8" TransitionalMode="True" Width="400px">
                                                    </cc1:DropCheck>
                                               

                                       

                                      

                                    </div>


                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblChequeNo" runat="server" CssClass="  lblTxt lblName">Cheque No.</asp:Label>

                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass=" inputtextbox"></asp:TextBox>



                                        </div>

                                        <div class="col-md-9 pading5px">

                                            <asp:Label ID="lblChequeDate" runat="server" CssClass=" smLbl_to">Date</asp:Label>

                                            <asp:TextBox ID="txtChequeDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server"
                                                Enabled="True" TargetControlID="txtChequeDate" PopupButtonID="Image3"
                                                Format="dd.MM.yyyy"></cc1:CalendarExtender>

                                            <asp:Label ID="lblremarks" runat="server" CssClass="smLbl_to">Remarks</asp:Label>

                                            <asp:TextBox ID="txtremarks" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lbltaxamt" runat="server" CssClass="smLbl_to" Style="width:66px;">Tax</asp:Label>

                                            <asp:TextBox ID="txttaxamt" runat="server" CssClass=" inputtextbox" Style="text-align:right;"></asp:TextBox>

                                            <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                        </div>

                                    </div>


                                </div>

                            </fieldset>
                        </asp:Panel>




                    </div>
                    <div>
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="337px"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Top" />
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:CommandField ShowEditButton="True" />
                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server"
                                            Font-Size="11px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "          " + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "") %>'
                                            Width="250px" TabIndex="75"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue No">

                                    <ItemTemplate>
                                        <asp:Label ID="lblisuno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                            Width="60px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Cheque No">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChequeno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="100px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChequeDate" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="77"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvChequeDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvChequeDate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Bill Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalance" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTbalance" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="80px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="80px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Tax">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtaxamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxam")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="70px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtaxamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="70px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>




                                 <asp:TemplateField HeaderText="Net Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnetamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextbox"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netam")).ToString("#,##0;(#,##0.); ") %>'
                                            Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="78" Style="text-align: right;"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnetamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px"
                                            Width="80px" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="80px" ForeColor="Black" TabIndex="79"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payto">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="140px" ForeColor="Black" TabIndex="80"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acvounum" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvacvounm" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>'
                                            Width="50px" ForeColor="Black" TabIndex="80"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBillno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="100px" ForeColor="Black" TabIndex="99"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Instade Of Issue">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvinsissueno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>'
                                            Width="80px" ForeColor="Black" TabIndex="99"></asp:TextBox>
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


                    <div>
                        <asp:Panel ID="pnlNarration" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">


                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>
                                            <a class=" btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=17")%>' style="margin: 0 0 0 5px;">Next</a>

                                        </div>


                                        <div class="col-md-2 pading5px">

                                            <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
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


