<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalInterest02.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSalInterest02" %>

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
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="270px" CssClass="ddlistPull form-control"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server"
                                            OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="6">Ok</asp:LinkButton>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblCustName" runat="server" CssClass="lblTxt lblName" Text="Customer Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="inputtextbox"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindCustomer" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindCustomer_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:DropDownList ID="ddlCustName" runat="server"
                                            CssClass="ddlistPull" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox" TabIndex="8"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbltoDate" runat="server" Font-Bold="True" CssClass="smLbl_to"
                                            Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttoDate" runat="server" CssClass="inputtextbox" TabIndex="8"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                    </div>

                                   

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:CheckBox ID="chkInvoicePrint" runat="server"
                                            CssClass="checkbox"
                                            Text="Invoice Print" Visible="False" />

                                        <asp:CheckBox ID="chkPayment" runat="server" CssClass="checkbox"
                                            Text="Payment" Visible="False" />

                                        <asp:Label ID="lmsg" runat="server" CssClass="lblmsg"></asp:Label>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lblinterest" runat="server" CssClass="smLbl_to"
                                            Text="Interest Per Month(%)"></asp:Label>

                                        <asp:TextBox ID="txtinpermonth" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>


                                </div>
                            </div>
                        </fieldset>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewInterest" runat="server">

                                <asp:Label ID="lblDelayCharge" runat="server"
                                    CssClass="lblTxt lblName" Text="Delay Charge:"
                                    Visible="False"></asp:Label>
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvInterest" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                </div>
                                <asp:Label ID="lblchqnotyetCleared" runat="server"
                                    CssClass="lblTxt lblName"
                                    Text="Cheque not yet Cleared:" Visible="False"></asp:Label>

                                <div class="table table-responsive">
                                    <asp:GridView ID="gvChqnocl" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
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
                                                    <asp:Label ID="lgvmrno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                        Width="80px">
                                            
                                          
                                            
                                            
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvchqno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                        Width="80px">
                                            
                                          
                                            
                                            
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
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
                                                    <asp:Label ID="lgpaydate" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFPayamtbuncr" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
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
                                </div>
                                <asp:Label ID="lblchqdishonour" runat="server" BorderStyle="None" CssClass="lblTxt lblName"
                                    Text="Cheque Dishonour:" Visible="False"></asp:Label>

                                <div class="table table-responsive">

                                    <asp:GridView ID="gvCDHonour" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
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


                                            <asp:TemplateField HeaderText="Dishonour Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdishDate" runat="server" ForeColor="Black" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dishonour <br /> Charge">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdischarge" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
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
                                </div>
                            </asp:View>
                            <asp:View ID="ViewRegistration" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvRegis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvRegis_RowDataBound" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrp" runat="server" Font-Bold="true" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rescode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrescode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnCalculation" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" OnClick="lbtnCalculation_Click">Calculation</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsize" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmount" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:TextBox>
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
                                    <asp:Panel ID="PanelNarration" runat="server" BorderColor="Maroon"
                                        BorderStyle="Solid" BorderWidth="1px" Visible="False">

                                        <div class="form-group">
                                            <div class="col-md-3 asitCol3 pading5px">
                                                <asp:Label ID="lblinterest0" runat="server" CssClass="lblName lblTxt" Text="Remarks:"
                                                    Width="80px"></asp:Label>
                                                <asp:TextBox ID="txtregRemarks" runat="server" CssClass="inputtextbox"
                                                    TabIndex="3" TextMode="MultiLine"></asp:TextBox>

                                            </div>
                                        </div>



                                    </asp:Panel>
                                </div>

                            </asp:View>
                            <asp:View ID="ViewDuesCollAll" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvDueCollAll" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">

                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false" ForeColor="Black" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usirdesc")) %>'
                                                        Width="260px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rescode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrescode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description" FooterText="Total:">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle HorizontalAlign="Right" ForeColor="White" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Flat Size">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsize" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Apartment Cost">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFaptcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvaptcost" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Car Parking">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFcpcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcpcost" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Utility ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFutilitycost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvutilitycost" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Others">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothescost" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTVal" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTVal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cleared/Received Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvClrAmt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFClrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uncleared/ Received Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUClrAmt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ucamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFUClrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Received Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTRecAmt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTRecAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Installment Dues">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvinsdues" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumbal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFInDues" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delay Charge">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDlChg" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumintr")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFDlChg" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque Dishonour Charge">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvChqDis" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFChqDis" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Dues">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTDues" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTDues" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Dues">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGDues" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFGDues" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
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
                                </div>


                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

