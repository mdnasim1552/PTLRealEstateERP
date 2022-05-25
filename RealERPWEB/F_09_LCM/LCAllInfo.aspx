
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LCAllInfo.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LCAllInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
           var gridview = $('#<%=this.gvTransaction.ClientID %>');
            gridview.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });
        };

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
        <div class="contentPartSmall">
            <div class="row">
            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="View1" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-horizontal">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>


                                                <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                        <asp:Label ID="lblPrePBNo" runat="server" CssClass="lblTxt lblName" Style="margin-left: -25px">L/C Type</asp:Label>
                                          <asp:DropDownList ID="ddllctypelist" runat="server" CssClass="ddlPage inputTxt" Width="220">
                                        </asp:DropDownList>                                                   

                                            </div>
                                                <div class="col-md-1">
                                                     <div class="colMdbtn ">
                                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                                </div>
                                                </div>
                                            <div class="col-md-3 pull-right " style="margin-top: -24px">

                                                <asp:CheckBox ID="checkAllData" runat="server" Text="  All print" />
                                            </div>


                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>
                                <asp:GridView ID="gvTransaction" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowCommand="gvTransaction_RowCommand">
                                    <RowStyle />
                                    <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="L/C Type">
                                              <HeaderTemplate>
                                                  <asp:Label ID="lblLcType" runat="server" style="text-align:left;">L/C Type</asp:Label>
                                               <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                             </HeaderTemplate>
                                                <ItemTemplate>
                                                    

                                                      <asp:Label ID="lgvactdesc1" runat="server" Visible="true" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                                        Width="207px"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="L/C No">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="lnkgvLcno" onmouseover="style.color='blue'" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="120px" Font-Bold="true" OnClick="lnkgvLcno_Click"></asp:LinkButton>

                                                      <asp:Label ID="lgvactcode" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                        Width="60px"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Opening Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOpDate" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lcdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Conversion Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cnvrsion")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                   <FooterTemplate>
                                                    <asp:Label ID="lblTotal" runat="server">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Value USD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfamtusd" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value BDT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFAmountbdt" runat="server" Font-Size="12px" ForeColor="#000" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipment Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpDate" runat="server" Style="text-align: Left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ship Arival Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvShipardate" runat="server" Style="text-align: Left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipardate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDelDate" runat="server" Style="text-align: Left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expired Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvExpdate" runat="server" Style="text-align: Left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        
                                            <%--<asp:TemplateField HeaderText="Received Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRcvdate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Currency ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvLcrtatus" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvLcstatus" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cspldesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            

                                        </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />


                        <%--<div class="row">


                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-12 formBtn ">
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnLedger" runat="server" CssClass="btngl pull-left" Style="margin: 0 5px;" OnClick="lnkbtnLedger_Click"><span class="flaticon-book40"></span> Ledger</asp:LinkButton>
                                            
                                        </div>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btnsave " Style="margin: 0 5px;" OnClick="lnkbtnAdd_Click1"><span class="flaticon-add118"></span> Add</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btnnew " Style="margin: 0 5px;" OnClick="lnkbtnEdit_Click"><span class="flaticon-edit26"></span> Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CssClass="btnclose btnc " Style="margin: 0 5px;" OnClick="lnkbtnDelete_Click"><span class="flaticon-deleted1 text-danger "></span>Delete</asp:LinkButton>
                                            <asp:LinkButton ID="btnClose" runat="server" CssClass="btnclose btnc " Style="margin: 0 5px;" OnClick="btnClose_Click"><span class="flaticon-delete47 text-danger "></span>Close</asp:LinkButton>

                                           
                                        </div>

                                    </div>





                                </div>



                            </div>
                        </fieldset>

                        </div>--%>
                    </div>

                    </div>


                </asp:View>

            </asp:MultiView>


           
                </div>
        </div>
        <!-- End of contentpart-->
    </div>
    <!-- End of Container-->
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


