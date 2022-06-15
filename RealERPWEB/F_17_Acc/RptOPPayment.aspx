<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptOPPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptOPPayment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    

    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  
  <%-- <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvtbOpPay.ClientID %>');
           gv.Scrollable();
       }
       </script>--%>
    

   
                
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
                              <asp:Panel ID="Panel1" runat="server" >
                                 <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                         <asp:RadioButtonList ID="rbtnGroup" runat="server" BackColor="#155273" ForeColor="White" CssClass="btn btn-primary checkBox"
                                        RepeatColumns="6" RepeatDirection="Horizontal" Width="235px">
                                       
                                                        <asp:ListItem  Selected="True">Payment</asp:ListItem>
                                                        <asp:ListItem>Deposit</asp:ListItem>
                                                       
                                        </asp:RadioButtonList>
                                    </div>
                                    </div>
                                 <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                              <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>


                                            <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                           <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                        <%--<div class="col-md-1">
                                            <div class="colMdbtn">
                                                

                                            </div>

                                        </div>--%>
                                       </div>

                      
                            <div class="form-group">
                                  <div class="col-md-3 pading5px asitCol2">
                             <%--    <asp:Label ID="lblreport" runat="server" CssClass="lblName lblTxt"  Text="Value" ></asp:Label>--%>
                                <asp:DropDownList ID="ddlVaule" runat="server"  CssClass="ddlPage" Width="100px">
                                    <asp:ListItem Value="HonourBasis">Honour Basis</asp:ListItem>
                                    <asp:ListItem Value="PostDated">Post Dated</asp:ListItem>
                                    <asp:ListItem Value="All">ALL</asp:ListItem>                                  
                                </asp:DropDownList>
                                   </div>

                              </div>
                                 <div class="form-group">
                                        <div class="col-md-2 pading5px asitCol2">
                                          <asp:Label ID="lblPage" runat="server" CssClass="lblName lblTxt"  Text="Size:" Visible="False"></asp:Label>

                                             <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False"  CssClass="ddlPage">
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

                                </asp:Panel>
                            <div class="form-horizontal">
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                          <asp:Panel ID="PanelVou" runat="server">
                               <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="View1" runat="server">
                                             <asp:GridView ID="gvtbOpPay" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                                AutoGenerateColumns="False" onpageindexchanging="gvtbOpPay_PageIndexChanging" 
                                                onrowdatabound="gvtbOpPay_RowDataBound" ShowFooter="True" PageSize="50">
                                              
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <HeaderTemplate>
                                                           <table style="width:47%;">
                                                               <tr>
                                                                   <td class="style58">
                                                                       <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                                           Text="Particulars" Width="180px"></asp:Label>
                                                                   </td>
                                                                   <td class="style60">
                                                                       &nbsp;</td>
                                                                   <td>
                                                                       <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" 
                                                                           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                           ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcType" runat="server" 
                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")).Trim(): "")  %>' 
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Up to 5000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUp" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam1")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFUp" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="5001-50,000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbtween" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam2")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFbtween" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="50,001-100000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAv" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam3")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAv" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="100001-Above">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAv3" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam4")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAv3" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Adjustment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAv4" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam5")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAv4" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtAmt" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpay")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvtFAmt" runat="server" Font-Size="11px" Height="16px" 
                                                                style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            <FooterStyle CssClass="grvFooter"/>
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </asp:View>

                                    <asp:View ID="ViewDep" runat="server">
                                         <asp:GridView ID="gvtbOpDep" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                                AutoGenerateColumns="False" onpageindexchanging="gvtbOpDep_PageIndexChanging" 
                                                onrowdatabound="gvtbOpDep_RowDataBound" ShowFooter="True" PageSize="50">
                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo02" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Particulars">
                                                        <HeaderTemplate>
                                                           <table style="width:47%;">
                                                               <tr>
                                                                   <td class="style58">
                                                                       <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                                           Text="Particulars" Width="180px"></asp:Label>
                                                                   </td>
                                                                   <td class="style60">
                                                                       &nbsp;</td>
                                                                   <td>
                                                                       <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" 
                                                                           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                           ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                   </td>
                                                               </tr>
                                                           </table>
                                                       </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvdeptdesc" runat="server" 
                                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))    %>' 
                                                                      
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Up to 10000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUpcoll" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam1")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="10001-50000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbtweencoll" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam2")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Above 50,000">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAvcoll" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam3")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtAmtcoll" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdepam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            <FooterStyle CssClass="grvFooter"/>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                    </asp:View>
                                   </asp:MultiView>
                          </asp:Panel>

                    </div>
                </div>
            </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>




