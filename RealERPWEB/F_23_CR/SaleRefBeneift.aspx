<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SaleRefBeneift.aspx.cs" Inherits="RealERPWEB.F_23_CR.SaleRefBeneift" %>

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



       }

   </script>
                
    
 
                
                


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

           <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                         <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                     <%--    <div class="col-md-6 pading5px asitCol6">--%>
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Project Name:"  ></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                       <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="ddlPage" AutoPostBack="True"   onselectedindexchanged="ddlProjectName_SelectedIndexChanged" Width="280px" ></asp:DropDownList>

                                     <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    <%--</div>--%>
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
                                          <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"    Format="dd-MMM-yyyy" TargetControlID="txtDate"> </cc1:CalendarExtender>

                                         <asp:Label ID="lblbenefit" runat="server" CssClass=" smLbl_to" Text="Benifit Rate:"  ></asp:Label>

                                        <asp:TextBox ID="txtinrate" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                  
                                    </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </fieldset>
                            </div>
                             <div class="table table-responsive">
                                   <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="419px">
                                               <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvfTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" 
                                                            Style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Received Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrcvhamt" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lFrcvAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Received Date ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrecondatDate" runat="server" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpaydate" runat="server" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "upindate")).ToString("dd-MMM-yyyy") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgveday" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eday")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Benifit Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvinrate" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Benifit Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFinamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvinamt" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inam")).ToString("#,##0;(#,##0); ") %>' 
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
               

                                <%--<tr>
                                    <td class="style35">
                                    </td>
                                    <td class="style21">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;" Text="Project Name:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style36">
                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindProject_Click" 
                                            Width="16px" TabIndex="1" />
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="287px" Height="24px" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddlProjectName_SelectedIndexChanged" TabIndex="2">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td class="style90">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" onclick="lbtnOk_Click" 
                                            style="text-align: center; height: 17px;" Width="50px" TabIndex="6">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                    <td class="style23">
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="style19" style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="lblCustName" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;" Text="Customer Name:" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px" TabIndex="3"></asp:TextBox>
                                    </td>
                                    <td class="style36">
                                        <asp:ImageButton ID="imgbtnFindCustomer" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindCustomer_Click" 
                                            Width="16px" TabIndex="4" />
                                    </td>
                                    <td class="style29" align="left">
                                        <asp:DropDownList ID="ddlCustName" runat="server" 
                                            Font-Bold="True" Font-Size="12px" Width="287px" TabIndex="5">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style90">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="style19" style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: right; color: #FFFFFF;" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="txtboxformat" TabIndex="8" 
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"    Format="dd-MMM-yyyy" TargetControlID="txtDate"> </cc1:CalendarExtender>
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="lblbenefit" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="text-align: left; color: #FFFFFF;" Text="Benifit Rate:"></asp:Label>
                                    </td>
                                    <td align="left" class="style29">
                                        <asp:TextBox ID="txtinrate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" style="text-align: right" TabIndex="3" text="18%" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style90">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                    <td class="style23">
                                        &nbsp;</td>
                                </tr>--%>
                     
                       
                                 
                  
                </ContentTemplate>
    </asp:UpdatePanel>
  </asp:Content>
