<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccVouher02.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccVouher02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   
            
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
                            ShowFooter="True" Width="337px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                             
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
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc2" runat="server" 
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
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                            TabIndex="76" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                              
                                <asp:TemplateField HeaderText="Cheque No">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvChequeno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black" 
                                            TabIndex="76" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lgvChequeDate" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px" 
                                            ForeColor="Black" TabIndex="77" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>' 
                                            Width="80px"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" style="text-align:right"
                                            CssClass="GridTextbox" Font-Size="12px" ForeColor="Black" TabIndex="78" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Black" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="Black" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="79" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            
                                <asp:TemplateField HeaderText="Payto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPayto" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="80" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                            Width="140px"></asp:Label>
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
                                   <asp:TemplateField HeaderText="Bill Ref No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillRefno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billrefno")) %>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instade Of Issue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinsissueno" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>' 
                                            Width="80px"></asp:Label>
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
                        
                        <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <%--<div class="form-group">
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
                                                                    
                                  

                                   </div>--%>
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

