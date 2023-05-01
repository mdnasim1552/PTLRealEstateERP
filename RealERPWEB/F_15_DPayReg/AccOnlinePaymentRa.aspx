<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymentRa.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.AccOnlinePaymentRa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>
    <style>
        .moduleItemWrpper h3 {
            background: #bfe7e7 none repeat scroll 0 0;
            border-top-left-radius: 50px;
            border-top-right-radius: 50px;
            box-shadow: 0 0 0 4px #ddffff, 2px 1px 6px 4px rgba(10, 10, 0, 0.5);
            color: #000;
            font-size: 20px;
            font-weight: 600;
            line-height: 6px;
            margin: 22px;
            padding: 10px 0;
            text-align: center;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlMain" runat="server">

                            <div class="row">
                                 <div class="col-md-2" runat="server" id="paymentd" visible="false">
                                <div class="form-group">
                                     <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Approval Date" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtpaymentdate" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtpaymentdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="imgrecdate"
                                    TargetControlID="txtpaymentdate"></cc1:CalendarExtender>
                                </div>
                               


                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                      <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Text="Pay Date"></asp:Label>
                                <asp:TextBox ID="txtPayDate" AutoPostBack="True" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="imgrecdate"
                                    TargetControlID="txtPayDate"></cc1:CalendarExtender>
                                </div>
                              
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                     <asp:Label ID="lblSearch" runat="server" CssClass=" smLbl_to" Text="Search"></asp:Label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                               
                            </div>
                            <div class="col-md-1" style="margin-top:22px;">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnOk" CssClass="btn btn-sm btn-primary" runat="server" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                </div>
                                
                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right d-none"></asp:Label>
                            </div>
                            </div>
                           


                        </asp:Panel>
                        </div>
                        
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea"
                            Width="831px" OnRowDataBound="gvPayment_RowDataBound"
                            OnRowDeleting="gvPayment_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Cancel" />

                                <asp:TemplateField HeaderText="Payment Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum1")) %>'
                                            Width="70px"></asp:Label>

                                        <asp:Label ID="lbgvslnum1" runat="server" BorderColor="#99CCFF" Visible="false" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bill No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnumbill" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum")) %>'
                                            Width="60px"></asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate"))  %>'
                                            Width="70px"></asp:Label>
                                        <%--<cc1:CalendarExtender ID="txtgvpaymentdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvpaymentdate"></cc1:CalendarExtender>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) +
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                        "<span class=gvdesc>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")+ "</span>" %>'
                                            Width="250px">
                                                            
                                                            
                                        </asp:Label>
                                    </ItemTemplate>


                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary okBtn"
                                            OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>--%>
                                <%--         <asp:TemplateField HeaderText="Details Head">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Approved Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAppramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Balance Amt." Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFBamt" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>--%>

                                <%-- <asp:TemplateField HeaderText="Approved To-day" HeaderStyle-ForeColor="Red" Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFToday" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAppramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="No of Cheque" HeaderStyle-ForeColor="Red" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNochq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nochq")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distribution </Br> of Amount" HeaderStyle-ForeColor="Red" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Received Date">
                                   <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-sm btn-success" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>--%>

                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvValdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-sm btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Approved</asp:LinkButton>

                                    </FooterTemplate>--%>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Nature" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="" />
                        <RowStyle CssClass="grvRowsNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PanelNar" runat="server">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="lblNarration" runat="server" CssClass="lblTxt lblName" Text="Narration"></asp:Label>
                                    <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="4" TextMode="MultiLine" Width="500px" Font-Size="12px"></asp:TextBox>

                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelNote" runat="server" Visible="False">
                            <div class="row ml-5 mt-2">
                                <div class="form-group">


                                <div class="col-md-12 ">
                                    <asp:HyperLink ID="lbtnBankPos" runat="server" CssClass="btn btn-sm btn-success" Visible="False" Target="_blank"
                                        OnLoad="lbtnBankPos_Load"> Bank Position</asp:HyperLink>
                                   <asp:Label  runat="server" CssClass="btn btn-sm btn-success mb-3" >Bank Position:</asp:Label>
                                    <div class="clearfix"></div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCl" runat="server" CssClass="lblTxt lblName" Text="Bank Balance"></asp:Label>
                                        <%-- <asp:Label ID="lblClAmt" runat="server" CssClass=" smLbl_to"></asp:Label>--%>
                                        <asp:HyperLink ID="Hyplnk" runat="server" ToolTip="Click Details bank position" CssClass="btn btn-danger btn-sm" NavigateUrl="~/F_17_Acc/AccTrialBalance.aspx?Type=BankPosition&comcod=&Date1=&Date2=" Target="_blank"></asp:HyperLink>

                                        <div class="clearfix"></div>
                                    </div>

                                    <asp:Panel ID="bankpso" Visible="false" runat="server">
                                        <div class="row">
                                             <div class="form-group">
                                            <asp:Label ID="lbllssue" runat="server" CssClass="lblTxt lblName" Text="Issue Amount"></asp:Label>
                                            <asp:Label ID="lbllssueAmt" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblColl" runat="server" CssClass="lblTxt lblName" Text="Coll Amount"></asp:Label>
                                            <asp:Label ID="lblCollAmt" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label ID="lblnet" runat="server" CssClass="lblTxt lblName" Text="Net Balance"></asp:Label>
                                            <asp:Label ID="lblnetBal" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>
                                        </div>
                                       
                                    </asp:Panel>
                                </div>


                            </div>
                            </div>
                            


                        </asp:Panel>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
