<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EarlyPayment.aspx.cs" Inherits="RealERPWEB.F_23_CR.EarlyPayment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
  
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                
  <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
 <script language="javascript" type="text/javascript"  src="../Scripts/KeyPress.js"></script>

  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


       });

       function pageLoaded() {
           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);

           });

           $('.chzn-select').chosen({ search_contains: true });

       }
   </script>
                

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
                                         <div class="col-md-5 pading5px asitCol6">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Project Name:"  ></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                       <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="chzn-select ddlPage form-control" AutoPostBack="True"   onselectedindexchanged="ddlProjectName_SelectedIndexChanged" Width="280px" ></asp:DropDownList>
                                    <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                        <div>
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                  </div>

                                     <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="lblCustName" runat="server" CssClass="lblTxt lblName" Text="Customer Name:"   ></asp:Label>

                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                       <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindCustomer_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlCustName" runat="server"  CssClass="ddlPage"  Width="280px"></asp:DropDownList>

                                  
                                    </div>
                                        </div>


                                      <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date:"   ></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"   Format="dd-MMM-yyyy" TargetControlID="txtDate"> </cc1:CalendarExtender>

                                    </div>
                                        </div>
                                    </asp:Panel>
                                <asp:Panel ID="PanelPayment" runat="server" Visible="False" >

                                    <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="lbltxtBalirmation" runat="server" CssClass="lblTxt lblName" Text="Client Ledger:"   ></asp:Label>
                                    </div>
                                        </div>

                                      <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">

                                            <asp:Label ID="lbltxttotalval" runat="server" CssClass="lblTxt lblName" Text="Total Value"   ></asp:Label>

                                              <asp:Label ID="lbltotalval" runat="server" CssClass=" inputtextbox"></asp:Label>

                                              <asp:Label ID="lbltxtPrincipalamt0" runat="server" CssClass="lblTxt lblName"  Text="Principal Amount"  ></asp:Label>

                                              <asp:Label ID="lblPrincipalamt" runat="server" CssClass="inputtextbox" ></asp:Label>
                                          </div>
                                     </div>

                                     <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">

                                            <asp:Label ID="lbltxtPaid" runat="server" CssClass="lblTxt lblName" Text="Paid"   ></asp:Label>

                                              <asp:Label ID="lblPaid" runat="server" CssClass=" inputtextbox"></asp:Label>

                                              <asp:Label ID="lbltxtPrincipalamt2" runat="server" CssClass="lblTxt lblName"  Text="Benifit"  ></asp:Label>

                                              <asp:Label ID="lblbenifit" runat="server" CssClass="inputtextbox" ></asp:Label>
                                          </div>
                                     </div>

                                     <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">

                                            <asp:Label ID="lbltxtBalance" runat="server" CssClass="lblTxt lblName" Text="Balance"   ></asp:Label>

                                              <asp:Label ID="lblBalance" runat="server" CssClass=" inputtextbox"></asp:Label>

                                              <asp:Label ID="lbltxtnetpayment" runat="server" CssClass="lblTxt lblName"   Text="Net Payment"   ></asp:Label>

                                              <asp:Label ID="lblnetpayment" runat="server" CssClass="inputtextbox" ></asp:Label>

                                             <asp:Label ID="Label12" runat="server" Visible="False"  CssClass="inputtextbox" Width="500px"></asp:Label>
                                          </div>
                                     </div>

                                      <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="lbltxtProposal" runat="server" CssClass="lblTxt lblName" Text="Proposal Information:"  ></asp:Label>
                                    </div>
                                        </div>


                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="form-group">
                                         <div class="col-md-6 pading5px asitCol6">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Benifit Rate:"  ></asp:Label>

                                        <asp:TextBox ID="txtinrate" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                       <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Payment:"  ></asp:Label>

                                        <asp:TextBox ID="txtPayment" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lbtnUpdate0" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>

                                       
                                    <asp:Label ID="Label11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                  </div>

                                    </asp:Panel>


                                </asp:Panel>




                                </div>
                            </fieldset>
                            </div>
                             <div class="table table-responsive">
                                  <asp:GridView ID="gvPayment" runat="server"   CssClass=" table-striped table-hover table-bordered grvContentarea"

                            AutoGenerateColumns="False" ShowFooter="True"
                                                Width="513px">
                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Description of Item" FooterText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="12px" 
                                                            ForeColor="#000" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Schedule Date ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Schedule Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvschamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lFschAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                  

                                                    <asp:TemplateField HeaderText="Pay Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpaydate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paiddate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Benifit Rate">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvinrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inrate")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Early Day">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgveday" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eday")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Benifit Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFinamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvinamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
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
                             </div>
                        
                        </div>
                </div>


                <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
  </asp:Content>