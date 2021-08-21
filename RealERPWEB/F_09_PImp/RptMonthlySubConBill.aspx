
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthlySubConBill.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptMonthlySubConBill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


            var gv = $('#<%=this.gvSubConBill.ClientID %>');
            gv.Scrollable();

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-2">
                                         <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>




                                    </div>
                                    






                                </div>
                            </div>
                        </fieldset>
                       </div>
                      </div>
                    
             
                           <div class="table table-responsive">
                                <asp:GridView ID="gvSubConBill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnPageIndexChanging="gvSubConBill_PageIndexChanging" AllowPaging="true">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                      <%--  <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>' Width="95px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Sub Contractor Name">
                                            <ItemTemplate>
                                                <asp:Label ID="HLgvDesc" runat="server" 
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="180px"></asp:Label>
                                                </ItemTemplate>
                                            <%--<ItemTemplate>
                                                <asp:Label ID="lgvPrjName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                         
                                        
                                 
                                    
                                        <asp:TemplateField HeaderText="Bill Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Security Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt1" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSecurityAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deduction">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdedAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Penalty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpenaltyamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPenaltyAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Total net</br> amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNetAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpayment" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPayment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Payable">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                             </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNetpayableAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                           </div>
                   <%-- </asp:View>
                   --%>
           
           
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
