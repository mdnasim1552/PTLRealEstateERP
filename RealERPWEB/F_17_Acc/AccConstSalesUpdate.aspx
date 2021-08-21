<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccConstSalesUpdate.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccConstSalesUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
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
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Voucher Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">                                 

                                    <div class="col-md-3 pading5px asitCol3">

                                        <div class="colMdbtn">
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">


                                    </div>





                                </div>

                                <div class="form-group">

                                       <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblBillList" runat="server" CssClass=" lblTxt lblName" Text="Bill List"></asp:Label>
                                        <asp:TextBox ID="txtBillno" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <asp:LinkButton ID="imgSearchBillno" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgSearchBillno_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                      <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddBillList" runat="server"  Font-Size="12px" 
                                            TabIndex="9" CssClass="chzn-select form-control  inputTxt">
                                        </asp:DropDownList>

                                      </div>
                                   
                                        <asp:LinkButton ID="lbtnoK" runat="server" CssClass="btn btn-primary  btn-xs"     OnClick="lbtnoK_Click" TabIndex="8" Text="Ok"></asp:LinkButton>

                                  


                                </div>
                            </div>
                    </div>
                </div>
                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" BackColor="White"
                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                    BorderStyle="Solid" BorderWidth="2px" Height="16px" ShowFooter="True"
                    Width="689px">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn"
                                    OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                    Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                            <FooterTemplate>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL" Font-Names="Verdana"
                                    Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                    Width="400px"></asp:Label>
                                <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                    Visible="False" Width="200px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Size="11px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details Description" ItemStyle-Font-Size="9px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                    Width="250px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="11px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification" ItemStyle-Font-Size="9px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                    Text="Total:"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Font-Size="9px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                            ItemStyle-Font-Size="11px" Visible="false">
                            <FooterTemplate>
                                <asp:TextBox ID="txtTgvQty" runat="server" ReadOnly="true"
                                    Visible="False" Width="70px"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvQty" runat="server" ReadOnly="True" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="12px" />
                            <ItemStyle Font-Size="11px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" Visible="false">

                            <ItemTemplate>
                                <asp:TextBox ID="txtgvRate" runat="server" ReadOnly="True" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle ForeColor="White" />
                            <ItemStyle Font-Size="11px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                    Font-Size="12px" ForeColor="#000" Height="22px" Width="103px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" BorderWidth="1px" ForeColor="Black" Style="text-align: right;"
                                    Height="22px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="103px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                    Font-Size="12px" ForeColor="#000" Height="22px" Width="103px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                    BorderStyle="None" BorderWidth="1px" ForeColor="Black" Style="text-align: right;"
                                    Height="22px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="103px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill. No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBillno" runat="server" CssClass="GridLebel" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>

                <asp:Panel ID="Panel1" runat="server" Visible="False">

                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class=" form-group">
                                <div class="col-md-12 pading5px">
                                    <asp:Label ID="lblRefNum" runat="server" CssClass="smLbl_to" Text="Ref./Cheq No/Slip No."></asp:Label>
                                    <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                    <asp:Label ID="lblSrInfo" runat="server" CssClass="smLbl_to" Text="Narration"></asp:Label>

                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


                                              <asp:Label ID="IblAdvance" runat="server" CssClass="smLbl_to" Text="Advanced"></asp:Label>

                                    <asp:TextBox ID="txtAdvanced" runat="server" CssClass="inputTxt inpPixedWidth text-right" AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>
                                    <asp:HyperLink ID="lbtnBalance" runat="server" Target="_blank" Style="margin-left: 10px; color: blue; font-weight: bold; font-size: 14px;"></asp:HyperLink>
                                    <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-1 pading5px">
                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblName lblTxt" Text="Narration"></asp:Label>
                                    <asp:Label ID="lbltoamt" runat="server" Visible="False"></asp:Label>

                                </div>

                                <div class="col-md-7 pading5px">
                                    <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                        CssClass="form-control" Rows="5" Height="150" TextMode="MultiLine"
                                        TabIndex="21"></asp:TextBox>
                                </div>
                                <div class=" col-md-4 pading5px">

                                    <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click" Width="100px">Final Update</asp:LinkButton>

                                </div>

                                <%-- <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                OnClick="lnkFinalUpdate_Click"
                                                TabIndex="22">Final Update</asp:LinkButton>
                                        </div>--%>
                            </div>
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>
            </div>





            
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

