
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccProjectReports.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccProjectReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



   
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
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName"> Project Name</asp:Label>
                                        
                                        <asp:TextBox ID="lblActDesc" runat="server" CssClass="inputlblDateRange inputTxt" ReadOnly="True"></asp:TextBox>

                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label3123" runat="server" CssClass="lblTxt lblName"> Date</asp:Label>                                        
                                        <asp:TextBox ID="lblDate" runat="server" CssClass="inputlblDateRange inputTxt" ReadOnly="True"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="form-group">
                                     <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlReportLevelDetails" runat="server" CssClass="ddlPage">
                                            <asp:ListItem Value="2">Main</asp:ListItem>
                                            <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                          <asp:GridView ID="dgvPS" runat="server" AutoGenerateColumns="False" 
                                     onrowdatabound="dgvPS_RowDataBound" ShowFooter="True" 
                                     CssClass="table-striped table-hover table-bordered grvContentarea ">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" 
                                            Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                            FooterStyle-HorizontalAlign="Right" FooterText="Total. &lt;br&gt; Net." 
                                            HeaderStyle-Font-Size="14px" >
                                             

                                             <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text=" Resource  Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                                    CssClass="btn btn-primary primarygrdBtn">Export Excel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            
                                            
                                            
                                            <ItemTemplate>
                                       <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                        Width="300px"></asp:HyperLink>
                                          </ItemTemplate>


                                            <%--<ItemTemplate>
                                                <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "subdesc4").ToString() %>' 
                                                    Width="320px"></asp:Label>
                                            </ItemTemplate>--%>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>' 
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" 
                                            HeaderStyle-Font-Size="12px" HeaderText="Op.Qty" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopqty" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" 
                                            HeaderStyle-Font-Size="12px" HeaderText="Op.Amt" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfopamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3121" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpnamt1" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" 
                                            HeaderStyle-Font-Size="12px" HeaderText="Cu.Qty" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCuq" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Dr.Amt" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDr" runat="server" CssClass="GridLebel"  Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                                <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfdramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvfdramt" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>

                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                              <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cr.Amt" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCr" runat="server" CssClass="GridLebel" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>

                                              <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfcramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvfcramt" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                              <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Qty" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClq" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Dr Amt" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclDrAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label112" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClrDrAmt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl. Cr Amt" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclBalAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClCram" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                  <FooterStyle BackColor="#F5F5F5" />
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

