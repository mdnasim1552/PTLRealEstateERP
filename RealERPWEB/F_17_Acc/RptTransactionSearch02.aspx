<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptTransactionSearch02.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptTransactionSearch02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#<%=this.txtvoudate.ClientID %>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtTransSearch(event);



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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="col-md-6">
                                    <div class="form-group">

                                        <asp:Label ID="lblVouDate" runat="server" CssClass="lblTxt lblName" Text="Vou Date:"></asp:Label>
                                        <asp:TextBox ID="txtvoudate" runat="server" OnTextChanged="txtvoudate_TextChanged" TabIndex="1" CssClass="inputtextbox" AutoPostBack="true"></asp:TextBox>

                                        <asp:Label ID="lblConAccount" runat="server" CssClass="lblTxt lblName" Text="Bank"></asp:Label>
                                        <asp:TextBox ID="txtBankDesc" TabIndex="2" runat="server" CssClass="  inputtextbox"></asp:TextBox>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">


                                        <asp:Label ID="lblacchead" runat="server" CssClass="lblTxt lblName" Text="Account Head"></asp:Label>
                                        <asp:TextBox ID="txtAccountHead" runat="server" TabIndex="3" CssClass="  inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblDetailsHead" runat="server" CssClass="lblTxt lblName" Text="Details Head"></asp:Label>
                                        <asp:TextBox ID="txtDetailsHead" runat="server" TabIndex="4" CssClass="  inputtextbox"></asp:TextBox>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">


                                        <asp:Label ID="lblamount0" runat="server" CssClass="lblTxt lblName" Text="Amount"></asp:Label>
                                        <asp:TextBox ID="txtamount" runat="server" TabIndex="5" CssClass="  inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblchequeno" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtchequeno" runat="server" TabIndex="6" CssClass="  inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lnkOk" runat="server" TabIndex="11" CssClass="btn btn-primary primaryBtn" OnClick="lnkOk_Click" Text="Ok"></asp:LinkButton>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">


                                        <asp:Label ID="lblIssueNo" runat="server" CssClass="lblTxt lblName" Text="Issue No"></asp:Label>
                                        <asp:TextBox ID="txtissueno" runat="server" TabIndex="7" CssClass="  inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblChequedate" runat="server" CssClass="lblTxt lblName" Text="Cheque Date"></asp:Label>
                                        <asp:TextBox ID="txtchequedate" runat="server" TabIndex="8" CssClass="  inputtextbox" AutoPostBack="True"
                                            OnTextChanged="txtchequedate_TextChanged"></asp:TextBox>


                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">


                                        <asp:Label ID="lblPayto0" runat="server" CssClass="lblTxt lblName" Text="Pay To:"></asp:Label>
                                        <asp:TextBox ID="txtpayto" runat="server" TabIndex="9" CssClass="  inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblNerration" runat="server" CssClass="lblTxt lblName" Text="Nerration"></asp:Label>
                                        <asp:TextBox ID="txtnarration" runat="server" TabIndex="10" CssClass="  inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lnkRefresh" TabIndex="99"  runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkRefresh_Click" Text="Refresh"></asp:LinkButton>

                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <asp:Panel ID="Panel2" runat="server">

                                        <div class="form-group">
                                            <asp:ListBox ID="lstVouname" runat="server" AutoPostBack="True"
                                                                    BackColor="Aqua" Font-Bold="True" Font-Size="12px" Height="120px"
                                                                    OnSelectedIndexChanged="lstVouname_SelectedIndexChanged"
                                                                    SelectionMode="Multiple" CssClass="form-control" ></asp:ListBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px"                                                        
                                                      TabIndex="55"
                                                        Text="Cheque Print"
                                                        />

                                            <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="chkBoxControl margin5px" />

                                           
                                        </div>

                                        
                                    </asp:Panel>
                                </div>


                            </div>
                        </fieldset>
                        <asp:Panel ID="Pnlmain" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="lblVoucherNo" runat="server" CssClass="lblTxt lblName" Text="Voucher No."></asp:Label>
                                            <asp:TextBox ID="lblvalVoucherNo" runat="server" CssClass="  inputtextbox"></asp:TextBox>

                                            <asp:Label ID="lblVouDate0" runat="server" CssClass="lblTxt lblName" Text="Voucher Date"></asp:Label>
                                            <asp:TextBox ID="lblvalVoucherDate" runat="server" CssClass="  inputtextbox"></asp:TextBox>

                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">


                                            <asp:Label ID="lblBankDescription" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                            <asp:TextBox ID="lblValBankDescription" runat="server" CssClass="  inputtextbox" Width="240px"></asp:TextBox>



                                            <div class="clearfix"></div>
                                        </div>

                                    </div>
                                </div>
                            </fieldset>


                        </asp:Panel>

                           <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">

                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Text="Total"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server" Font-Names="Verdana"
                                            Font-Size="11px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                            Width="400px"></asp:Label>
                                        <asp:Label ID="lblAccdesc" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Visible="False" Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Quantity">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" TabIndex="79"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" ReadOnly="True" Style="text-align: right" TabIndex="80"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="82"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtFgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" TabIndex="83"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                         <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                         <asp:TextBox ID="lblvalRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>
                                    <div class="col-md-6 pading5px">

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="lblvalSirinfo" runat="server" CssClass="inputtextbox" ></asp:TextBox>
                                       
                                            <asp:Label ID="lblPayto" runat="server" CssClass="smLbl" Text="Pay To"></asp:Label>
                                        <asp:TextBox ID="lblvalpayto" runat="server" CssClass="inputtextbox" Width="320px" ></asp:TextBox>
                                      

                                    </div>
                                                                    
                                  

                                   </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="lblvalNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                         
                                         
                                        
                                       
                                          <div class="col-md-2 pading5px">

                                          <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                    </div>

                                        

                                    
                                    </div>
                                    </div>

                            </fieldset>
                    </div>
                    
                     
               
                      
                </div>

                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

