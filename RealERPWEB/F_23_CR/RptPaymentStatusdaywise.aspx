<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPaymentStatusdaywise.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptPaymentStatusdaywise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });


         
            var gvpaystatus = $('#<%=this.gvpaystatus.ClientID %>');
            gvpaystatus.Scrollable();

            
        }

    </script>
    <style type="text/css">
        .chzn-single{
                border-radius: 3px!important;
                height: 29px!important;
            }
    </style>

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



            <div class="card card-fluid mt-2">
                <div class="card-body">
                    <div class="row">
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <label ID="lblDate"  CssClass="form-label">From Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    <div class="col-md-1" >
                            <div class="form-group">
                                <label ID="lbltoDate"  CssClass="form-label" >To Date</label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>

                        </div>

                              <div class="col-md-2 mt-2">
                            <div class="from-group">
                                <asp:Label ID="prjName" CssClass="form-label">Project Name </asp:Label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlPrjName_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                       <div class="col-md-2 ml-2 mt-2">
                           <div class="form-group">
                              <%--  <asp:Label ID="Label6" runat="server" CssClass="control-label"></asp:Label>--%>
                                <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="form-control form-control-sm" TabIndex="14" visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="" OnClick="imgbtnFindCustomer_Click" ><span style="font-size:14px;">Customer Name&nbsp;<i class="fas fa-search"></i></span></asp:LinkButton>
                                <asp:DropDownList ID="ddlCustName" runat="server" CssClass="form-control form-control-sm chzn-select"  AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                           </div>
                    
                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="Label1" runat="server" class="control-label" visible="false">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"  visible="false"                                  
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    <div class="col-md-1" style="margin-top: 23px; margin-left:-65px;">
                            
                     <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                           
                        </div>
                        

                    </div>
                </div>
            </div>

           <%-- <div class="card card-fluid">
                <div class="row">
                    
                </div>


            </div>--%>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
                    
                        <asp:GridView ID="gvpaystatus" runat="server" AutoGenerateColumns="false" 
                                ShowFooter="True"  AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNopay" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchequedat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFchequedat"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Clearance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcleardat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndate"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFcleardat"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchequeno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno"))%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFchequeno"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBankName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFBankName"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MR No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmrno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFmrno"> Total :</asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                  
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPaidamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFPaidamt"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        

                        

                  

                </div>
            </div>




            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
