<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" AutoEventWireup="true" CodeBehind="RptUtilityAndOtherCollection.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptUtilityAndOtherCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });


            <%--var gv = $('#<%=this.gvMatStock.ClientID %>');
            gv.Scrollable();
            var gvqbasis = $('#<%=this.gvQtyBasis.ClientID %>');
            gvqbasis.Scrollable();
            var gvAmtPeriodic = $('#<%=this.gvAmtPeriodic.ClientID %>');
            gvAmtPeriodic.Scrollable();
            var gvdvQtyBasisPeriodic = $('#<%=this.dvQtyBasisPeriodic.ClientID %>');
            gvdvQtyBasisPeriodic.Scrollable();--%>
        }

    </script>
    <style>
        #ContentPlaceHolder1_ddlProjectName_chzn {
            width: 289 px !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 29px !important;
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



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">    
                        <div class="col-md-1.5">
                            <div class="form-group">
                                 <asp:Label ID="lbldate" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>                               
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                           <div class="form-group">
                                <asp:Label ID="Label2" CssClass="lblTxt lblName" runat="server" Text="Project Name"></asp:Label>                                                                     
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm chzn-select" Width="290px"  TabIndex="13" AutoPostBack="true">
                                </asp:DropDownList>                                  
                               
                           </div>
                        </div>                
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:label ID="lblreport" runat="server" CssClass="lblTxt lblName">Reports</asp:label>
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control form-control-sm chzn-select">
                                     <asp:ListItem Value="AllOtherColl">All </asp:ListItem>
                                    <asp:ListItem Value="UtilityCharge">Utility Charge</asp:ListItem>                                 
                                    <asp:ListItem Value="Registration">Registration</asp:ListItem>
                                    <asp:ListItem Value="Additional">Additional</asp:ListItem>
                                    <asp:ListItem Value="Mutation">Mutation</asp:ListItem>
                                    <asp:ListItem Value="Association">Association Fee </asp:ListItem>
                                    <asp:ListItem Value="ServiceCharge">Service Charge</asp:ListItem>
                                    <asp:ListItem Value="SocietyFee">Society Fee</asp:ListItem>
                                    <asp:ListItem Value="Optional">Optional </asp:ListItem>
                                    <asp:ListItem Value="DelayCharge">Delay Charge </asp:ListItem>                                  
                                </asp:DropDownList>
                            </div>
                        </div>                        
                        <div class="col-md-1" style="margin-top:20px">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary ml-2" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage1" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <div class="row responsive"> 
                        <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewOtherCharge" runat="server">
                            <asp:GridView ID="gvotherColl" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" OnPageIndexChanging="gvotherColl_PageIndexChanging" AllowPaging="false" OnRowDataBound="gvotherColl_RowDataBound" CssClass=" table-striped table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Font-Names="Century Gothic" />
                                        <ItemStyle HorizontalAlign="left"  Font-Names="Century Gothic" />

                                    </asp:TemplateField>

                                 

                                   <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="100px"></asp:Label>
                                     <asp:HyperLink ID="hlbtntbCdataExcel1" runat="server"
                                      CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span>
                                 
                                    </asp:HyperLink>
                                </HeaderTemplate>

                                <FooterTemplate>
                                   
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvactDesc1" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))+"</b>" %>'
                                        Width="150px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"  Font-Names="Century Gothic"/>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" Font-Names="Century Gothic" />
                            </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Customer code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCustcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode"))%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>                                          
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCustName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnitName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>                                        

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left"  Font-Names="Century Gothic"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpayment" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpayment" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalance" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>
                                   
                                </Columns>

                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <RowStyle CssClass="grvRowsNew" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="AllOthercoll" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvallothrcoll" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" OnPageIndexChanging="gvallothrcoll_PageIndexChanging" AllowPaging="false" OnRowCreated="gvallothrcoll_RowCreated" OnRowDataBound="gvallothrcoll_RowDataBound"
                                     CssClass=" table-striped table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                         <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                       

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="100px"></asp:Label>
                                     <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                      CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span>
                                 
                                    </asp:HyperLink>
                                </HeaderTemplate>

                                <FooterTemplate>
                                   
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblgvactDesc" runat="server"
                                        Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))+"</b>" %>'
                                        Width="150px" Font-Size="11px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic"  />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Names="Century Gothic"  />
                            </asp:TemplateField>
                                   

                                    <asp:TemplateField HeaderText="Customer code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCustcodeall" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode"))%>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCustNameall" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnitNameeall" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                     
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptreg" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptreg")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtreg" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentreg" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidregamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentreg" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalancereg" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "regisbal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptadw" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptadw")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtadw" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"  Font-Names="Century Gothic" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentadw" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidadw")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentadw" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalanceadw" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adwbal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptasscia" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptassocia")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtasscia" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentasscia" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidassocia")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentasscia" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalanceasscia" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "associabal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalanceasscia" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptutility" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptutility")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtutility" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentutility" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidutility")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentutility" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalanceutility" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utilityabal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalanceutility" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptsociety" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptsociety")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtsociety" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentsociety" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidsociety")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentsociety" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalancesociety" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "societybal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalancesociety" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptservice" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptservice")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtservice" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentservice" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidservice")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentservice" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalanceservice" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "servicebal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalanceservice" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptmutation" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptmutation")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtmutation" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentmutation" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidmutation")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentmutation" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalancemutation" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mutationbal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalancemutation" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptoptional" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptoptional")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtoptional" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right"  Font-Names="Century Gothic" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentoptional" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidoptional")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentoptional" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalanceoptional" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "optionalbal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalanceoptional" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Receipt Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreceiptdelay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reciptdelay")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFreceiptamtdelay" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpaymentdelay" runat="server"
                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paiddelay")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lgvFpaymentdelay" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBalancedelay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delaybal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                       <FooterTemplate>
                                        <asp:Label ID="lgvFBalancedelay" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                          </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" Font-Names="Century Gothic"  />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Names="Century Gothic"  />
                                    </asp:TemplateField>
                                   
                                </Columns>

                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <RowStyle CssClass="grvRowsNew" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>
                            </div>

                            

                        </asp:View>

                        

                        

                        

                    </asp:MultiView>
                    </div>
                    

                </div>
            </div>




            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
