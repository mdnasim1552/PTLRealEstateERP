<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBillTracking.aspx.cs" Inherits="RealERPWEB.F_20_BillMod.RptBillTracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                        <div class="form-group">
                                    <div class="col-md-10 pading5px">

                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtfrmDate" runat="server"    CssClass="inputtextbox"></asp:TextBox>
                                       <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtfrmDate">
                                        </cc1:CalendarExtender>

                                         <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                         <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="1">Ok</asp:LinkButton>

                                    </div>
                                  
                                 </div> 
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                     <div class="table table-responsive">
                           <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="margin-top: 0px" Width="831px">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate"))%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvdeptname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc"))%>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Nature">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvref" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submitted Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvsubamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Approved  Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFappamt" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvappamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreceived" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "received")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvtrndate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trndate"))%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Transfer Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvdtndeptname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdeptdesc"))%>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvnarraion" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narration"))%>' 
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                      
                                    </Columns>
                               <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:GridView ID="gvApprStatus" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Style="margin-top: 0px" Width="831px">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvApprStatusSL" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStvdate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate1"))%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                         
                                        <asp:TemplateField HeaderText="Bill Nature">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStbillnature" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStctdesc" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStpartyname" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStref" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvApStbillamt" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfappamtapp" runat="server" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvappamtapp" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: right; background-color: Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" 
                                                VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsStreceived" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "received")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Approved  Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApsSttrndate" runat="server" BorderColor="#99CCFF" 
                                                    BorderStyle="Solid" BorderWidth="0px" 
                                                    Style="text-align: Left; background-color: Transparent" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat1"))%>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
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
                     </div>
                </div>
            </div>


                        <%--<asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style27">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="style16" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="text-align: left" Text="From:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style26">
                                        <asp:TextBox ID="txtfrmDate" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtfrmDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbltodate" runat="server" CssClass="style16" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: left" 
                                            Text="To:"></asp:Label>
</td>
                                    <td class="style28">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                       <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnOk_Click" 
                                            Style="text-align: center;" Width="52px">Ok</asp:LinkButton>
</td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
              
                           
                      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
