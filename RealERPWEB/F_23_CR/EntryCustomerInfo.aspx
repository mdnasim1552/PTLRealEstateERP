<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryCustomerInfo.aspx.cs" Inherits="RealERPWEB.F_23_CR.EntryCustomerInfo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
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
    
 
                
                
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
               <div class="container moduleItemWrpper">
                     <div class="contentPart">
                             <div class="row">  
                                     <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <asp:panel ID="Panel1" runat="server">  
                                                 <div class="form-group">
                                                     <div class="col-md-6 pading5px asitCol6">

                                                      

                                                          <asp:Label ID="lblProjectmDesc" runat="server" CssClass=" inputtextbox" Visible="False" Width="350px"></asp:Label>

                                                          <asp:Label ID="lmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>

                                                      </div>
                                                 </div>
                                                 <div class="form-group">
                                                        <div class="col-md-3 pading5px asitCol6">
                                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                                                              <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                                           <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                            <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="ddlPage chzn-select"  Width="280px" ></asp:DropDownList>

                                                            </div>
                                                    <div class="col-md-3 pading5px asitCol6">

                                                         <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary  primaryBtn" onclick="lbtnOk_Click">Ok</asp:LinkButton>

                                                       <asp:Label ID="lblProjectdesc" runat="server" Visible="False"  Width="350px" CssClass="inputtextbox"></asp:Label>

                                                        </div>
                                                      </div>
                                            </asp:panel>
                                        </div>
                                    </fieldset>
                                 </div>
                          <div class="table table-responsive">
                                <div class="form-group">
                                      <div class="col-md-6 pading5px asitCol6">
                                            <asp:Label ID="lblCode" runat="server" Visible="False"  CssClass="inputtextbox"></asp:Label>
                                           

                                     </div>
                                </div>

                                <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="831px" 
                                        onrowcancelingedit="gvSpayment_RowCancelingEdit" 
                                        onrowediting="gvSpayment_RowEditing" onrowupdating="gvSpayment_RowUpdating">
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                           
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCod" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize" 
                                                        onclick="lbtnusize_Click" style="text-align: right; " 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px"></asp:Label>
                                                    
                                                
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                
                                           <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvamt" runat="server" style="text-align: right" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="Min Booking Money">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvminbmoney" runat="server"  style="text-align: right" BackColor="Transparent" 
                                                        BorderStyle="None" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                           
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' 
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Car Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcparking" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mgt Booking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                             <asp:CommandField ShowEditButton="True" />

                                             <asp:TemplateField HeaderText="Client Name">
                                                 <EditItemTemplate>
                                                     <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                                         BorderWidth="1px">
                                                         <table style="width: 100%;">
                                                             <tr>
                                                                 <td class="style64">
                                                                     <asp:TextBox ID="txtSerachClient" runat="server" BorderStyle="Solid" 
                                                                         BorderWidth="1px" Height="18px" TabIndex="4" Width="86px"></asp:TextBox>
                                                                 </td>
                                                                 <td class="style65">
                                                                     <asp:ImageButton ID="ibtnSrchClient" runat="server" Height="16px" 
                                                                         ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrchClient_Click" TabIndex="5" 
                                                                         Width="16px" />
                                                                 </td>
                                                                 <td>
                                                                     <asp:DropDownList ID="ddlClientName" runat="server" TabIndex="6" Width="150px">
                                                                     </asp:DropDownList>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </asp:Panel>
                                                 </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcclientname" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>' 
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>

                                  <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                     <%--   <FooterStyle BackColor="#333333" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />--%>
                                    </asp:GridView>

                               <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="ViewPersonal" runat="server">
                                             <div class="form-group">
                                                     <div class="col-md-6 pading5px asitCol6">

                                                        <asp:Label ID="lperInfo" runat="server" CssClass="lblTxt lblName" Text="Personal Information"></asp:Label>

                                                          <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnBack_Click">Ok</asp:LinkButton>

                                                      </div>
                                                 </div>
                                         </asp:View>
                                         <asp:View ID="VLoanInfo" runat="server">
                                              <div class="form-group">
                                                     <div class="col-md-6 pading5px asitCol6">
                                                          <asp:LinkButton ID="lbtnBackCost" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnBack_Click">Back</asp:LinkButton>

                                                      </div>
                                                 </div>

                                              <asp:GridView ID="gvLoanInformation" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                            ShowFooter="True" Width="831px">
                                                          
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" ForeColor="Black" 
                                                                            Height="16px" style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCodeLoan" runat="server" ForeColor="Black" Height="16px" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                            Width="49px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc3" runat="server" Font-Size="11px" ForeColor="Black" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                                            Width="200px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalloan" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="lUpdateLoanInfo" runat="server" Font-Bold="True" 
                                                                            Font-Size="12px" ForeColor="White" onclick="lUpdateLoanInfo_Click" 
                                                                            style="text-decaration:none;">Update Loan Info</asp:LinkButton>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvValloan" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="11px" 
                                                                            Height="20px" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                            Width="510px"></asp:TextBox>
                                                                    </ItemTemplate>
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

                                           <asp:View ID="ViewResStatus" runat="server">
                                                 <div class="form-group">
                                                     <div class="col-md-6 pading5px asitCol6">
                                                          <asp:LinkButton ID="lbtnBackResStatus" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnBack_Click">Back</asp:LinkButton>

                                                      </div>
                                                 </div>
                                                 <asp:GridView ID="gvRegStatus" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                             AutoGenerateColumns="False" ShowFooter="True" Width="831px" 
                                                             style="margin-right: 0px">
                                                           
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" ForeColor="Black" 
                                                                             Height="16px" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Code">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvItmCodeReg" runat="server" ForeColor="Black" 
                                                                             Height="16px" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                             Width="49px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Description">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lgcResDescReg" runat="server" Font-Size="11px" ForeColor="Black" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                                             Width="200px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Type" Visible="False">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lgvgvalReg" runat="server" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="">
                                                                     <FooterTemplate>
                                                                         <asp:LinkButton ID="lUpdateRegis" runat="server" Font-Bold="True" 
                                                                             Font-Size="12px" ForeColor="White" onclick="lUpdateRegis_Click" 
                                                                             style="text-decaration:none;">Update Registration</asp:LinkButton>
                                                                     </FooterTemplate>
                                                                     <HeaderTemplate>
                                                                         <table style="width:13%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:Label ID="lblLgDept" runat="server" 
                                                                                         style="text-align: center" Text="Received from Legal" Width="106px" 
                                                                                         Height="16px"></asp:Label>
                                                                                 </td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:Label ID="lblLgDept0" runat="server" Height="16px" 
                                                                                         style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                                                 </td>
                                                                                 
                                                                             </tr>
                                                                         </table>
                                                                     </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtgvValRecleg" runat="server" BackColor="Transparent" 
                                                                             BorderStyle="None" Font-Size="11px" Height="20px" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                             Width="200px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                 </asp:TemplateField>




                                                                 <asp:TemplateField HeaderText="">
                                                                    
                                                                     <HeaderTemplate>
                                                                         <table style="width:13%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:Label ID="lblLgDept" runat="server" 
                                                                                         style="text-align: center" Text="Provided to Client" Width="106px" 
                                                                                         Height="16px"></asp:Label>
                                                                                 </td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     <asp:Label ID="lblLgDept0" runat="server" Height="16px" 
                                                                                         style="text-align: center" Text="Status &amp; Date" Width="106px"></asp:Label>
                                                                                 </td>
                                                                                 
                                                                             </tr>
                                                                         </table>
                                                                     </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtgvValprotoclient" runat="server" BackColor="Transparent" 
                                                                             BorderStyle="None" Font-Size="11px" Height="20px" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>' 
                                                                             Width="200px"></asp:TextBox>
                                                                     </ItemTemplate>
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
                               <div class="form-group">
                                      <div class="col-md-6 pading5px asitCol6">
                                           <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server"   QueryPattern="Contains" TargetControlID="ddlProjectName"> </cc1:ListSearchExtender>
                                     </div>
                                </div>
                               
                        
                                            
                                            
                                           <%-- <table style="width:100%;">--%>
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="lperInfo" runat="server" Font-Bold="True" Font-Size="18px" 
                                                            ForeColor="#660066" style="text-align: left; color: #FFFFFF;" Text="Personal Information" 
                                                            Width="539px"></asp:Label>
                                                        <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnBack" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            onclick="lbtnBack_Click" style="color: #FFFFFF" BackColor="#003366" 
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px">Back</asp:LinkButton>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <%--<td class="style6" colspan="2">--%>
                                                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                            ShowFooter="True" Width="831px">
                                                          
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px" 
                                                                            ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                                            Width="49px" ForeColor="Black"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc1" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                                            Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgval" runat="server" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="lUpdatPerInfo" runat="server" Font-Bold="True" 
                                                                           style="text-decaration:none;" Font-Size="12px" ForeColor="red" onclick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" 
                                                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="510px" 
                                                                            
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                                            Height="20px" Font-Size="11px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                           <FooterStyle CssClass="grvFooter"/>
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />

                                                        </asp:GridView>
                                                    <%--</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>--%>
                                                
                                              <%--  <tr>
                                                    <td colspan="12">
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style57">
                                                        &nbsp;</td>
                                                    <td class="style56">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <table style="width:100%;">
                                                 
                                                 <tr>
                                                     <td colspan="12">
                                                       
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td class="style57">
                                                         &nbsp;</td>
                                                     <td class="style56">
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         &nbsp;</td>
                                                 </tr>
                                             </table>
                                   --%>

                             </div>

                         </div>
                   </div>

                        
                    </ContentTemplate>
                </asp:UpdatePanel>
           
</asp:Content>



