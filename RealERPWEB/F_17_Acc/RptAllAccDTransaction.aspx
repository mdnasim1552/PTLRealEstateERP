<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAllAccDTransaction.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAllAccDTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/waypoints.min.js"></script>  
     <script src="../Scripts/jquery.counterup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            

        });

       

        function pageLoaded() {

            try {

                $('.counter').counterUp({
                    delay: 10,
                    time: 1000
                });


                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });
          
                $('#<%=this.gvcashbook.ClientID%>').tblScrollable();
                $('#<%=this.gvcashbookp.ClientID%>').tblScrollable();
                $('#<%=this.gvcashbookDB.ClientID%>').tblScrollable();
                
            }

            catch(e)
            {
            
                alert(e);

            }


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
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true">
                                            </cc1:CalendarExtender>


                                            <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true">
                                            </cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                            </div>

                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblVoucherCash" runat="server" CssClass="lblTxt lblName">Voucher Type</asp:Label>

                                            <asp:DropDownList ID="ddlVoucharCash" runat="server" CssClass=" ddlistPull inputTxt">
                                                <asp:ListItem Value="C">Cash Voucher</asp:ListItem>
                                                <asp:ListItem Value="B">Bank Voucher</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="ALL Voucher">ALL Voucher</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:TextBox ID="lblGroup" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5 pading5px">

                                            <asp:RadioButtonList ID="rbtnGroup" runat="server"
                                                RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="235px">

                                                <asp:ListItem>Receipt</asp:ListItem>
                                                <asp:ListItem>Payment</asp:ListItem>
                                                <asp:ListItem Selected="True">Both</asp:ListItem>

                                            </asp:RadioButtonList>



                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblAmount0" runat="server" CssClass="lblTxt lblName">Amount</asp:Label>

                                            <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass=" ddlistPull inputTxt"
                                                OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                <asp:ListItem Value="between">Between</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:TextBox ID="txtAmountC1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:Label ID="lblToCash" runat="server" Visible="False" CssClass=" smLbl_to">To</asp:Label>


                                            <asp:Label ID="txtAmountC2" runat="server" Visible="False" CssClass="lblTxt lblName"></asp:Label>
                                            <asp:LinkButton ID="imgbtnSearchVoucherCash" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false" OnClick="imgbtnSearchVoucherCash_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>








                                        </div>


                                    </div>

                                </div>
                            </fieldset>
                        </asp:Panel>

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="lblReceiptCash" runat="server"
                                        Text="Receipts" Font-Bold="true" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row table-responsive">

                        <asp:GridView ID="gvcashbook" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True"
                            Width="931px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                        <asp:Label ID="lblPaymentCash" runat="server"  Font-Bold="true"
                            Text="Payment" Visible="False"></asp:Label>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbookp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="931px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                        <asp:Label ID="lblDetailsCash" runat="server" 
                            Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px"
                            Visible="False"></asp:Label>
                    </div>

                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        Width="973px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accounts Head">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="500px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receipt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                    </div>



                </div>
            </div>



            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
