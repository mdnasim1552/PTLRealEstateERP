<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkDuesColl.aspx.cs" Inherits="RealERPWEB.F_22_Sal.LinkDuesColl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
                <div class="contentPart">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-6  pading5px">

                                        <asp:Label ID="Label4" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblProjectName" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblinterest" CssClass="lblTxt lblName" runat="server" Text="Interest Per Month(%)"></asp:Label>
                                        <asp:Label ID="lblInterPar" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>



                                    </div>
                                    <div class="col-md-1">
                                         <div class="col-md-4 pading5px">

                                        <asp:CheckBox ID="chkConsolidate" runat="server" TabIndex="10" Text="Consolidate" Visible="false" CssClass="btn btn-primary checkBox" />
                                    </div>

                                    </div>



                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class=" col-md-6  pading5px">

                                        <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server" Text="Customer name"></asp:Label>
                                        <asp:Label ID="LblCutName" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="Label9" CssClass="lblTxt lblName" runat="server" Text="Date"></asp:Label>
                                        <asp:Label ID="lblDate" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>



                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class=" form-group">
                        <asp:Label ID="lblDelayCharge" runat="server" CssClass="btn btn-success pre-scrollable"
                            Text="Delay Charge:"
                            Visible="False"></asp:Label>
                    </div>
                    <asp:GridView ID="gvInterest" runat="server" AutoGenerateColumns="False"
                        CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="665px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ins.Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinsdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cinsdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"
                                        Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cinsdat")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ins.Amt">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFinsamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvinsamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque. Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvacdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                        Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bank Clearing">
                                <ItemTemplate>
                                    <asp:Label ID="lgvacdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndat")).ToString("dd-MMM-yyyy") %>'
                                        Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndat")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Paid.Amt">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamount")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cummulative Due">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcummulativedue" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumdue")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Cummulative Paid">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcummulativepaid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumpaid")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cummulative Bal.">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFcumbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumbalance")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Days">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdays" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "day")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delay Charge">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFinamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvbalamt0" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "interest")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cummulative">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFcuminamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvcuminamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cuminterest")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dues Amt">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFdueamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvdueamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>


                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />

                    </asp:GridView>

                    <div class=" form-group">
                        <asp:Label ID="lblchqnotyetCleared" runat="server" CssClass="btn btn-success primaryBtn"
                            Text="Cheque not yet Cleared:" Visible="False"></asp:Label>
                    </div>

                    <asp:GridView ID="gvChqnoclin" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left" Width="467px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="MR No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrno0" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                        Width="80px">
                                            
                                          
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchqno0" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                        Width="80px">
                                            
                                          
                                            
                                            
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgrecdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cinsdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Cheque Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgpaydate0" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFPayamtbuncr" runat="server" Font-Bold="True"
                                        Font-Size="12px"  ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvpayamtbuncr" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamount")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                        </Columns>


                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />

                    </asp:GridView>
                    <div class=" form-group">
                        <asp:Label ID="lblchqdishonour" runat="server" CssClass="btn btn-success primaryBtn"
                            Text="Cheque Dishonour:" Visible="False"></asp:Label>
                        <div class="clearfix"></div>
                    </div>
                    <asp:GridView ID="gvCDHonour" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="616px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mr. No">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvMrrno" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvChequeNo" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDueDate" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cinsdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvamount" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamount")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Dishour Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdishDate" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dishounour <br /> Charge">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFdischarge" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvcharge" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                        </Columns>


                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

             




            
        </asp:View>
         <asp:View ID="ViewClientLedger" runat="server">
            <asp:Panel ID="Panel2" runat="server">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class=" col-md-6  pading5px">

                                    <asp:Label ID="Label111" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                    <asp:Label ID="LblPrjDesc" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>
                                    <asp:Label ID="Label11" CssClass="lblTxt lblName" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:Label ID="lblCustName" CssClass=" smLbl_to" runat="server" Text="Project Name:"></asp:Label>
                                    <asp:Label ID="Label7" CssClass="smLbl_to" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="lblDate1" CssClass=" smLbl_to" runat="server" Text="Lavel"></asp:Label>



                                </div>
                                <div class="clearfix"></div>
                            </div>

                        </div>
                    </fieldset>
                </div>

            </asp:Panel>


        </asp:View>
        <asp:View ID="View2" runat="server">
            <asp:Panel ID="Panel3" runat="server">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class=" col-md-6  pading5px">

                                    <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                    <asp:Label ID="lblInprjDesc" CssClass=" smLbl_to" runat="server" Text=""></asp:Label>
                                    
                                    <asp:Label ID="Label113" CssClass="smLbl_to" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="lblInDate" CssClass=" smLbl_to" runat="server" Text=""></asp:Label>



                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class=" col-md-6  pading5px">

                                    <asp:Label ID="Label10" CssClass="lblTxt lblName" runat="server" Text="Customer name"></asp:Label>
                                    <asp:Label ID="lblInCustDesc" CssClass=" smLbl_to" runat="server" Text=""></asp:Label>
                                    
                                    <asp:Label ID="Label12" CssClass="smLbl_to" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="Label13" CssClass=" smLbl_to" runat="server" Text=""></asp:Label>



                                </div>
                                <div class="clearfix"></div>
                            </div>

                        </div>
                    </fieldset>
                </div>



                
            </asp:Panel>
            <asp:GridView ID="gvCustInvoice" runat="server" AutoGenerateColumns="False"
                ShowFooter="True" Style="text-align: left" Width="654px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                OnRowDataBound="gvCustInvoice_RowDataBound">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Description" FooterText="Total">

                        <ItemTemplate>
                            <asp:Label ID="lgvdesc" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                Width="150px">
                                            
                                             
                                            
                                            
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Due Date">
                        <ItemTemplate>
                            <asp:Label ID="lgdate" runat="server"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Previous Due">
                        <FooterTemplate>
                            <asp:Label ID="lgvFPreDue" runat="server" Font-Bold="True" Font-Size="12px"
                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvPreDue" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "predue")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Current Due">
                        <FooterTemplate>
                            <asp:Label ID="lgvFCurDue" runat="server" Font-Bold="True" Font-Size="12px"
                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvCurDue" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdue")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Charge">
                        <FooterTemplate>
                            <asp:Label ID="lgvFDelayCh" runat="server" Font-Bold="True" Font-Size="12px"
                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>

                        <ItemTemplate>
                            <asp:HyperLink ID="HLgDelamt" runat="server" Target="_blank" BackColor="Transparent"
                                BorderStyle="None" Font-Size="11px" Style="font-size: 12px; color: Black; text-decoration: none; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delayamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:HyperLink>
                        </ItemTemplate>



                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Toal Payment">
                        <FooterTemplate>
                            <asp:Label ID="lgvFtopayment" runat="server" Font-Bold="True" Font-Size="12px"
                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvtopayment" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todue")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>





                </Columns>


                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>
            <div class=" form-group">
                <asp:Label ID="lblchequenotyetcl" runat="server" CssClass="btn btn-success primaryBtn" Text="Cheque not yet Cleared:" Visible="False"></asp:Label>
                <div class="clearfix"></div>
            </div>

            <asp:GridView ID="gvChqnocl" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Style="text-align: left" Width="408px">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField FooterText="Total" HeaderText="MR No">
                        <ItemTemplate>
                            <asp:Label ID="lgvmrno" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                Width="80px">
                                            
                                          
                                            
                                            
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lgvchqno" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                Width="80px">
                                            
                                          
                                            
                                            
                            </asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px"  ForeColor="#000" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lgpaydate" runat="server"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <FooterTemplate>
                            <asp:Label ID="lgvFPayamt" runat="server" Font-Bold="True" Font-Size="12px"
                                 ForeColor="#000" Style="text-align: right"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "predue")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>


                </Columns>


                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />

            </asp:GridView>

         
        </asp:View>
           
       
    </asp:MultiView>
                    </div>
            </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

