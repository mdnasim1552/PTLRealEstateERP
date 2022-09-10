<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPeriodicSalesWithCollection.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptPeriodicSalesWithCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
    });

    function pageLoaded() {
        $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
        }

</script>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">

                                <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"
                                    Font-Bold="True"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" CssClass=" ddlPage chzn-select" runat="server" Font-Bold="True"
                                    Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>

                                
                            </div>
                            <div class="col-md-1 pading5px pull-left">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>



                        </div>
                        


                    </div>

                    <div class="row">

                        <div class="form-group">
                            <div class="col-md-12">

                                <asp:Label ID="lblPage0" runat="server" CssClass="lblTxt lblName" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
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


                              
                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="From Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>


                                  <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="To Date:"> </asp:Label>
                                        <asp:TextBox ID="txttodat" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodat_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodat"></cc1:CalendarExtender>

                            
                                                                      

                            </div>

                            
                        </div>

                    </div>

                </fieldset>
                <div class="table table-responsive">
                    <asp:GridView ID="gvsaleswithcoll" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvsaleswithcoll_PageIndexChanging" OnRowCreated="gvsaleswithcoll_RowCreated"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
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

                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Name">

                                             <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Customer Name" Width="150px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvcustname" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                              <%--<asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgacuname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                           ForeColor="#000" Style="text-align: right" Width="200px"></asp:Label>
                               </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>



                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgudesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                       <asp:TemplateField HeaderText="Unit Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsftsize" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Sold Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsaldate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saldat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                                <asp:Label ID="lgvFsalesdate" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"  Text="Total :"></asp:Label>
                                     </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                                 <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Budgeted Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBudgetamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFBudgetamt" runat="server" Font-Bold="True" Font-Size="12px"
                                           ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                               </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                 <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sales Value">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSalesval" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFSalesval" runat="server" Font-Bold="True" Font-Size="12px"
                                           ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                               </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                 <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                               <asp:TemplateField HeaderText="Booking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbookdues" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookdues")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFbookdues" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Installment ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvInsdues" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insdues")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFInsdues" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="Booking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbookam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                     <asp:Label ID="lgvFbookam" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                 </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Installment">
                                <ItemTemplate>
                                    <asp:Label ID="lgvinstallamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFinstallamt" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                       

                            
                       <%--     <asp:TemplateField HeaderText="Total Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtdues" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tduesamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFtdues" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>--%>
                       
                       
                            <asp:TemplateField HeaderText="Total Collection">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtocoll" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFtocall" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Booking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetbooking" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbookam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFnetbooking" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Installment">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetIns" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netinsdues")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                   <asp:Label ID="lgvFnetIns" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                       
                       
                           
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbalance" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>

                                 <FooterTemplate>
                                   <asp:Label ID="lgvFbalance" runat="server" Font-Bold="True" Font-Size="12px"
                                         ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
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
    </div>

</asp:Content>
