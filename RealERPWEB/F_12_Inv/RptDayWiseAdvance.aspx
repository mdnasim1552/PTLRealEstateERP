<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDayWiseAdvance.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptDayWiseAdvance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvOrderAdvanced.ClientID %>');
            gv1.Scrollable();
          <%--  var gv2 = $('#<%=this.gvDetails.ClientID %>');
            var gv3 = $('#<%=this.gvBankPosition.ClientID %>');
            var gvtbcon = $('#<%=this.gvtbcon.ClientID %>');

            gv1.Scrollable();
            
            gv2.Scrollable();
            gv3.Scrollable();
            gvtbcon.Scrollable();
           --%>
            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="lblDaterange" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDatefrom" Enabled="true"></cc1:CalendarExtender>


                                                <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                            </div>
                                            
                                    </div>
                                     
                                        
                                     <div class="form-group">

                                    <div class="col-md-6 ">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Font-Size="11px " Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPRro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                         <asp:DropDownList ID="ddlProName" runat="server" Width="350px"
                                            TabIndex="2" AutoPostBack="True" CssClass="chzn-select">
                                        </asp:DropDownList>


                                       <%-- <cc1:ListSearchExtender ID="ddlProName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProName">--%>
                                        <%--</cc1:ListSearchExtender>--%>
                                    </div>

                                  

                                     <div class="col-md-1">

                                          <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="6">Ok</asp:LinkButton>
                                     </div>


                                </div>

                                    </div>
                                </fieldset>
                                 <div class="table table-responsive">
                                <asp:GridView ID="gvOrderAdvanced" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" >
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

                                        <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="180px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" _
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
                                        
                                 
                                    
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvorderno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")).ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Order ref">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvorderref" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")).ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdedamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                          
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requisition No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")).ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="MRF no">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrfno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")).ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Advanced</br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpayment" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Naration">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrfno" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordnar")).ToString() %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
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

