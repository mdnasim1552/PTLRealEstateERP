<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccVouher.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccVouher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
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
                                        <asp:Label ID="lblVoucherNo" runat="server" CssClass="lblTxt lblName" Text="Voucher No."></asp:Label>
                                        <asp:TextBox ID="lblvalVoucherNo" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblVouDate" runat="server" CssClass="smLbl_to" Text="Voucher Date "></asp:Label>
                                        <asp:TextBox ID="lblvalVoucherDate" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="lblBankDescription" runat="server" CssClass="lblTxt lblName" Text="Bank Name :"></asp:Label>
                                        <asp:TextBox ID="lblValBankDescription" runat="server" CssClass=" inputlblVal" Width="253PX"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
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
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbillno")) %>'
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
                    </div>
                    <div class="row">

                       
                        <%--<fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./Cheq No/Slip No."></asp:Label>
                                            <asp:TextBox ID="lblvalRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblSrInfo" runat="server" CssClass="smLbl_to" Text="Other ref. (if any)"></asp:Label>
                                            <asp:TextBox ID="lblvalSirinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPayto" runat="server" CssClass="smLbl_to" Text="Pay To"></asp:Label>
                                            <asp:TextBox ID="lblvalpayto" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblNarration" runat="server" CssClass="lblTxt lblName" Text="Narration"></asp:Label>
                                        <asp:TextBox ID="lblvalNarration" runat="server" CssClass="inputTxt" Text="Narration" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblisunum" runat="server" CssClass="smLbl_to" Visible="False"></asp:Label>

                                    </div>
                                  
                                </div>

                            </div>
                        </fieldset>--%>
                        <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                                       
                                   <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName" Style=" margin-left:650px; width: 600px; color: blue; text-align: left;"></asp:Label>

                                   </div>

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
            </div>


            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

