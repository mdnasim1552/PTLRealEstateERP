<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccTrialBalance.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccTrialBalance" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
    
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                
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
                                             <div class="col-md-11 pading5px asitCol11">
                                                  
                                                    <asp:Label ID="lblDaterange" runat="server" CssClass="lblTxt lblName" Text="Date Range" ></asp:Label>

                                                    <asp:TextBox ID="txtDatefrom" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"> </cc1:CalendarExtender>

                                                    <asp:Label ID="lblDateto" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                                    <asp:TextBox ID="txtDateto" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"   Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"> </cc1:CalendarExtender>

                                                    <asp:Label ID="lblreportlevel" runat="server" CssClass="lblTxt lblName" Text="Report Level"></asp:Label>

                                                  <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass=" ddlPage" >
                                                            <asp:ListItem Value="1">Level-1</asp:ListItem>
                                                            <asp:ListItem Value="2">Level-2</asp:ListItem>
                                                            <asp:ListItem Value="3">Level-3</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                                        </asp:DropDownList>

                                                    <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                                  <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Please wait......"></asp:Label>
                                                    </ProgressTemplate>
                                                 </asp:UpdateProgress>

                                                </div>
                                              </div>
                                        </div>
                                    </fieldset>
                                </div>
                            <div class="table table-responsive">
                                 <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        BackColor="#FFECEC" BorderColor="#66CCFF" BorderStyle="Solid" 
                                        BorderWidth="3px" ShowFooter="True" 
                                        Width="909px" onrowdatabound="dgv1_RowDataBound">
                                        <Columns>
                                         <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                FooterStyle-HorizontalAlign="Right" FooterText="Total Dr. &lt;br&gt; Total Cr." 
                                                HeaderText="Description of Accounts">
                                                <ItemTemplate>                                                                                                       
                                                    <asp:HyperLink id="HLgvDesc" runat="server" Width="400px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' 
                                                        Font-Underline="False" __designer:wfdid="w38" Target="_blank" 
                                                        Font-Size="12px" CssClass="GridLebelL" ></asp:HyperLink> 
                                                </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Left" />
                                                 <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                FooterStyle-HorizontalAlign="Right"  
                                                HeaderText="Opening Amt" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                FooterStyle-HorizontalAlign="Right" 
                                                HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                FooterStyle-HorizontalAlign="Right" 
                                                HeaderText="Closing Amount" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                                <td>
                                    </td>
                                <td>
                                    </td>
                                <td align="right">
                                    <asp:Label ID="lblDaterange" runat="server" CssClass="label2" Text="Date Range" 
                                        Width="100px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDatefrom" runat="server" CssClass="ddl"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"> </cc1:CalendarExtender>
                                </td>
                                <td class="style20">
                                    <asp:Label ID="lblDateto" runat="server" CssClass="label2" Text="To"></asp:Label>
                                </td>
                                <td class="style22">
                                    <asp:TextBox ID="txtDateto" runat="server" CssClass="ddl"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"   Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"> </cc1:CalendarExtender>
                                </td>
                                <td class="style18">
                                    </td>
                                <td class="style50">
                                    <asp:Label ID="lblreportlevel" runat="server" CssClass="label2" 
                                        Text="Report Level" Width="100px"></asp:Label>
                                </td>
                                <td class="style17">
                                    <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass="ddl" 
                                        Width="107px">
                                        <asp:ListItem Value="1">Level-1</asp:ListItem>
                                        <asp:ListItem Value="2">Level-2</asp:ListItem>
                                        <asp:ListItem Value="3">Level-3</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    </td>
                                <td class="style51">
                                    <asp:LinkButton ID="lnkok" runat="server" CssClass="button" 
                                        onclick="lnkok_Click" Width="71px">Ok</asp:LinkButton>
                                </td>
                                <td class="style19">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label2" runat="server" Text="Please wait......" 
                                                ForeColor="#FF3300" Width="144px" style="color: #FFFF66"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>--%>
                           
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>

