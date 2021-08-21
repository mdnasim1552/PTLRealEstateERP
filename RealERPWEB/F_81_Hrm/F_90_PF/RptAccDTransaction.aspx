<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccDTransaction.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.RptAccDTransaction" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                         <asp:Panel ID="Panel1" runat="server">
                                                 <div class="form-group">
                                             <div class="col-md-11 pading5px asitCol11">

                                                  <asp:RadioButtonList ID="rbtnList1" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" BackColor="#155273" ForeColor="White" CssClass="btn btn-primary checkBox"  Width="475px">
                                                        <asp:ListItem Selected="True">Cash Book</asp:ListItem>
                                                        <asp:ListItem>Daily Transaction</asp:ListItem>
                                                        <asp:ListItem>Receipt &amp; Payments</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                  
                                                    <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" Text="From:" ></asp:Label>

                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                   <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"  Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">   </cc1:CalendarExtender>

                                                    <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                     <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"   Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""> </cc1:CalendarExtender>

                                                    <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click">Show Report</asp:LinkButton>

                                               
                                                </div>
                                              </div>
                                             </asp:Panel>
                                        </div>
                                    </fieldset>
                                </div>
                            <div class="table table-responsive">
                                <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="VCashBook" runat="server">

                                             <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Receipts" ></asp:Label>
                                                </div>
                                              </div>

                                            <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                            ShowFooter="True" Width="931px">
                                                           
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDate" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' 
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Voucher #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvvnum" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvActDesc" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvActDesc3" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Control Head">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvCActDesc" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Detail Head">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRDesc" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cash">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvCashAmt" runat="server" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px" style="text-align: right"></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                    
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    
                                                                    
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None"  style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                <FooterStyle CssClass="grvFooter"/>
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />

                                                        </asp:GridView>

                                              <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Payment"></asp:Label>
                                                </div>
                                              </div>

                                             <asp:GridView ID="gvcashbookp" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                            ShowFooter="True" Width="931px">
                                                           
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDate0" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' 
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Voucher #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvvnum0" runat="server"  
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvActDesc0" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvActDesc1" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Control Head">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvCActDesc0" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Detail Head">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRDesc0" runat="server"  
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                                            Width="150px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cash">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvCashAmt0" runat="server" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    
                                                                   
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bank">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Height="18px" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                       <FooterStyle CssClass="grvFooter"/>
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                        </asp:GridView>

                                             <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Details of Cash &amp; Bank Balance"  ></asp:Label>
                                                </div>
                                              </div>

                                             <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                            ShowFooter="True" Width="973px">
                                                          
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Accounts Head">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvActDesc2" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                            Width="500px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Opening">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvOpening" runat="server" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Receipt">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvrecam" runat="server" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payment">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvpayam" runat="server" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="100px" style="text-align: right"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Closing">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Height="18px" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                 <FooterStyle CssClass="grvFooter"/>
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />

                                                        </asp:GridView>
                                         </asp:View>

                                    <asp:View ID="VDailytransaction" runat="server">
                                          <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Transaction Listing" ></asp:Label>
                                                </div>
                                              </div>

                                         <asp:GridView ID="gvtranlsit" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                            ShowFooter="True" Width="931px" onrowdatabound="gvtranlsit_RowDataBound">
                                                          
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDate1" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' 
                                                                            Width="65px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Voucher #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvvnum1" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvAcRsCode" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>' 
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvAcRsDesc" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>' 
                                                                            Width="300px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Res. Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvInAmt" runat="server" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Debit ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvDram" runat="server" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lgvFDram" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Credit">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtgvCram" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" style="text-align: right" 
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' 
                                                                            Width="100px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="txtgvFCram" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRefnum" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                       <FooterStyle CssClass="grvFooter"/>
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                        </asp:GridView>
                                    </asp:View>

                                    <asp:View ID="VRecAndPayment" runat="server">
                                         <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Receipts &amp; Payments"  ></asp:Label>
                                                </div>
                                              </div>

                                         <asp:GridView ID="gvrecandpay" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                         ShowFooter="True" Width="973px">
                                                       
                                                         <Columns>
                                                             <asp:TemplateField HeaderText="Sl.No.">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" 
                                                                         style="text-align: right" 
                                                                         Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Head Accounts Head">
                                                                 <FooterTemplate>
                                                                     <table style="width: 12%;">
                                                                         <tr>
                                                                             <td align="left">
                                                                                 <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Total Receipts:" 
                                                                                     Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Opening Balance:" 
                                                                                     Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Total:" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvActDesc4" runat="server" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>' 
                                                                         Width="300px"></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Receipt Amt.">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvrecpam" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="100px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <table style="width: 12%;">
                                                                         <tr>
                                                                             <td align="left">
                                                                                 <asp:Label ID="lblgvFrecpam" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="lgvFObal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="lgvFTRAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                 </FooterTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Head of Accounts">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvPayDesc" runat="server" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                                                         Width="300px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <table style="width: 12%;">
                                                                         <tr>
                                                                             <td align="left">
                                                                                 <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Total Payments:" 
                                                                                     Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Closing Balance:" 
                                                                                     Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Text="Total:" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                 </FooterTemplate>
                                                                 <HeaderStyle HorizontalAlign="Center" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Payment Amt.">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lgvpayam1" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="100px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <table style="width: 12%;">
                                                                         <tr>
                                                                             <td align="left">
                                                                                 <asp:Label ID="lgvFpayam1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="lgvFCbal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                         <tr>
                                                                             <td>
                                                                                 <asp:Label ID="lgvFTPAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                     ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                             </td>
                                                                         </tr>
                                                                     </table>
                                                                 </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Right" />
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                         </Columns>
                                                      <FooterStyle CssClass="grvFooter"/>
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />

                                                     </asp:GridView>

                                         <div class="form-group">
                                             <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Details of Balance"  ></asp:Label>
                                                </div>
                                              </div>

                                        <asp:GridView ID="gvrecapaybal" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"


                                                         ShowFooter="True" Width="973px">
                                                        
                                                         <Columns>
                                                             <asp:TemplateField HeaderText="Sl.No.">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" 
                                                                         style="text-align: right" 
                                                                         Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterText="Total:" HeaderText="Description Of Bank/Cash">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvActDesc5" runat="server" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>' 
                                                                         Width="500px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Opening Balance">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lgvOpening1" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="100px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lgvFOpening1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                 </FooterTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Deposit">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvdepam1" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="100px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblgvdepam1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                 </FooterTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Withdrawn">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lgvwitam1" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "witham")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="100px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lgvFwitam1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Right" />
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Closing Balance">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lgvClAmt1" runat="server" BackColor="Transparent" 
                                                                         BorderStyle="None" Height="18px" style="text-align: right" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="90px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lgvFClAmt1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="#000" style="text-align: right" Width="100px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                             </asp:TemplateField>
                                                         </Columns>
                                                       <FooterStyle CssClass="grvFooter"/>
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                     </asp:GridView>

                                    </asp:View>



                             </asp:MultiView>

                            </div>
                            </div>
                    </div>




                                            <%--<tr>
                                                <td class="style31">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style36">
                                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" 
                                                        BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="14px" RepeatColumns="6" RepeatDirection="Horizontal" 
                                                        style="text-align: center" Width="475px">
                                                        <asp:ListItem Selected="True">Cash Book</asp:ListItem>
                                                        <asp:ListItem>Daily Transaction</asp:ListItem>
                                                        <asp:ListItem>Receipt &amp; Payments</asp:ListItem>
                                                        <asp:ListItem>Other</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td align="left" class="style33">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                                                        style="text-align: right" Text="From.:" Width="60px" CssClass="style40"></asp:Label>
                                                </td>
                                                <td class="style34">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" 
                                                        Font-Bold="True" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style32">
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" 
                                                        style="text-align: right" Text="To:" CssClass="style40"></asp:Label>
                                                </td>
                                                <td class="style39">
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" 
                                                        Font-Bold="True" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnShow" runat="server" Font-Bold="True" Font-Size="16px" 
                                                        onclick="lbtnShow_Click" Width="100px" CssClass="style40" 
                                                        BackColor="#003366" BorderColor="#000" BorderStyle="Solid" BorderWidth="1px">Show Report</asp:LinkButton>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>--%>
                                         
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="16px" 
                                                            ForeColor="Yellow" Text="Receipts" Width="669px"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="16px" 
                                                            ForeColor="#660033" Text="Payment" Width="669px" style="color: #FFFF99"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                       
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="16px" 
                                                            ForeColor="#660033" Text="Details of Cash &amp; Bank Balance" 
                                                            Width="669px" Height="16px" style="color: #FFFF99"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                            
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="16px" 
                                                            ForeColor="#660033" Height="16px" Text="Transaction Listing" Width="669px" 
                                                            style="color: #FFFF99"></asp:Label>
                                                    </td>
                                                </tr>--%>

                                             <tr>
                                                 <%--<td>
                                                     <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="16px" 
                                                         ForeColor="#660033" Height="16px" Text="Receipts &amp; Payments" 
                                                         Width="669px" style="color: #FFFF99"></asp:Label>
                                                 </td>--%>
                                             </tr>
                                       
                                             <%--<tr>
                                                 <td>
                                                     <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="16px" 
                                                         ForeColor="#660033" Height="16px" Text="Details of Balance" Width="669px" 
                                                         style="color: #FFFF99"></asp:Label>
                                                 </td>
                                             </tr>--%>
                                           
                          
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>

