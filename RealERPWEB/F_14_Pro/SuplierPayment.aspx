<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SuplierPayment.aspx.cs" Inherits="RealERPWEB.F_14_Pro.SuplierPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<script type="text/javascript" language="javascript">

              $(document).ready(function () {

                  Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

              });

              function pageLoaded() {

                  $(".chzn-select").chosen();
                  $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                  var gv = $('#<%=this.gvSupPayment.ClientID %>');
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

                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Font-Size="11px " Text="Supplier Name:"></asp:Label>

                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlSubName" runat="server" Style="width: 336px;"
                                            TabIndex="2" AutoPostBack="True" CssClass="chzn-select ddlistPul">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_OnClick" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="6">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"
                                            TabIndex="7"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:Label ID="Label17" runat="server" CssClass="lblTxt lblName"
                                            Text="Project Name:"></asp:Label>
                                    </div>
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            CssClass="chzn-select ddlistPull" Width="336px" TabIndex="5"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    
                    
                    <div class="table table-responsive">
                                <asp:GridView ID="gvSupPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
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
                                         <asp:TemplateField HeaderText="" Visible="False">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="False">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgvssircode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lgsupName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnupdate" runat="server" CssClass="btn btn-success primaryBtn"
                                            TabIndex="6" OnClick="lbtnupdate_OnClick">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrjName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                        
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPrjName" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right">Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                     
                                        
                                        <asp:TemplateField HeaderText="Payable Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBillAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                           
                                        <asp:TemplateField HeaderText="Bill On Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbillpendamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBillpendAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Total Payable Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalbill" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalpay")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                                                        
                                            <asp:TemplateField HeaderText="AIT">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblpayment" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aitamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPayment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AIT(4%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgvbillamt2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aitper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNetpayableAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgvaprvamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvaprvamtF" runat="server" Font-Bold="True"
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

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


