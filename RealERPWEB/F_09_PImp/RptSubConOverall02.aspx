<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSubConOverall02.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptSubConOverall02" %>
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


            var gv = $('#<%=this.gvSubBill.ClientID %>');
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
                                   <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                    <asp:TextBox ID="txtDat" runat="server" CssClass="inputtextbox"
                                        TabIndex="7"></asp:TextBox>
                                     <cc1:CalendarExtender ID="txtDat_CalendarExtender0" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDat"></cc1:CalendarExtender>
                                </div>  
                                    
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="6">Ok</asp:LinkButton>
                                        </div>
                       
                                </div>
                            </div>
                        </fieldset>
                       </div>
                      </div>
                    
             
                           <div class="table table-responsive">
                                <asp:GridView ID="gvSubBill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
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

                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>' Width="95px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sub-Contractor Name">
                                            
                                            <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblsuname" runat="server" Text="Sub-Contractor Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                 <asp:LinkButton ID="lnkSelected" runat="server" CssClass="btn btn-danger primaryBtn" 
                                             OnClick="lnkSelected_Click">Selected</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>

                                            <FooterTemplate>
                                       
                                    </FooterTemplate>
                                            
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="180px"></asp:HyperLink>
                                                </ItemTemplate>
                                            <%--<ItemTemplate>
                                                <asp:Label ID="lgvPrjName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        

                                          <asp:TemplateField HeaderText="Pactcode" Visible="false">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrjcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Project Name">
                                           
                                               <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Project Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"  CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrjName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Nature of Work">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnature" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billtdesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                         
                                    
                                        <asp:TemplateField HeaderText="Bill Amount" Visible="false">
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



                                        <asp:TemplateField HeaderText="Work Done </br>without Bill amt" Visible="false">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgvwrkdone" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dwithoutibll")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtbillamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTBillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                 
                                       
                                        <asp:TemplateField HeaderText="Security Amt" Visible="false">
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
                                        <asp:TemplateField HeaderText="Deduction" Visible="false">
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
                                        <asp:TemplateField HeaderText="Penalty" Visible="false">
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
                                        <%--  <asp:TemplateField HeaderText="Total net amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNetAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>

                                             <asp:TemplateField HeaderText="Paid Amount">
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

                                        <asp:TemplateField HeaderText="Project Balance">
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


                                        
                                        <asp:TemplateField HeaderText="Net  Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetcbal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ncpayable")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetcbal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkitem" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
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



