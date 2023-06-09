﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryProjectFesibility05.aspx.cs" Inherits="RealERPWEB.F_02_Fea.EntryProjectFesibility05" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
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



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">                       
                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <asp:Label ID="Label1" runat="server" CssClass="btn btn-secondary">Search</asp:Label>
                                </div>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="input-group-prepend ">
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnok_Click" >Ok</asp:LinkButton></li>
                                </div>
                            </div>
                        </div>
                        

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="792px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnfUpdateCost" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Black" onclick="lbtnfUpdateCost_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpproject" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mpactdesc"))%>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Project Name">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvflat" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                          <%--       <asp:TemplateField HeaderText="Stored">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvstored" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stored"))%>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    
                                </asp:TemplateField>--%>

                             <%--    <asp:TemplateField HeaderText="Face">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvface" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "face"))%>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                 
                                </asp:TemplateField>--%>
                              
                                <asp:TemplateField HeaderText="Unit Desc">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvfloor" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>' 
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                   
                                </asp:TemplateField>

                              

                                    
                                <asp:TemplateField HeaderText="Unit Size">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvusize" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Black" onclick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                    </FooterTemplate>

                                        <%-- <FooterTemplate>
                                        <asp:Label ID="lgvFusize" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>--%>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>   
                                
                                 <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrate" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="lgvFgvrate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>  


                                  
                                 <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvamout" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="txtFgvamout" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField> 


                                
                                        <asp:TemplateField HeaderText="Parking">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPamt" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carparking")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvPAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Utility">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUtility" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvPUtility" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTamt" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                
                                

                                <asp:TemplateField HeaderText="Purchase Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpuramt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purvalue")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="lgvFpuramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>     
                                
                                  <asp:TemplateField HeaderText="Purchase Date">

                               <ItemTemplate>
                           <asp:TextBox ID="txtgvpurDate" runat="server" Font-Size="11px" 
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdate")).ToString("dd-MMM-yyyy")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>                   
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtgvpurDate"></cc1:CalendarExtender>
                        </ItemTemplate>

                                   <%-- <ItemTemplate>
                                        <asp:TextBox ID="txtgvbdamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purvalue")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>--%>

                                     <FooterTemplate>
                                        <asp:Label ID="lgvFbdamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right"></asp:Label>
                                    </FooterTemplate>


                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                              

                                
                              <asp:TemplateField HeaderText="Commited Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcommision" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "commitedval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                         <FooterTemplate>
                                        <asp:Label ID="lgvFcommision" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                           <asp:TemplateField HeaderText="Actual Sales </br> Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvactualsal" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acsalvalue")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                         <FooterTemplate>
                                        <asp:Label ID="lgvFcommision22" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                           <asp:TemplateField HeaderText="Ageing Date">

                            <ItemTemplate>
                           <asp:TextBox ID="txtgvAgeingDay" runat="server" Font-Size="11px" 
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "AgeingDay")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "AgeingDay")).ToString("dd-MMM-yyyy")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>                   
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtgvAgeingDay"></cc1:CalendarExtender>
                        </ItemTemplate>

                            <HeaderStyle
                                HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
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
            </div>

<%--            <table style="width: 100%;">
                <tr>
                    <td colspan="14">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px" Width="995px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style7">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Search:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style51">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" OnClick="lnkbtnSerOk_Click"
                                            Style="color: #FFFFFF; height: 17px;" TabIndex="3">Ok</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style7">
                                        <asp:CheckBox ID="chkAllRes" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#660033"
                                            OnCheckedChanged="chkAllSInf_CheckedChanged"
                                            Style="color: #FFFFFF; text-align: left;" TabIndex="4" Text="Show All"
                                            Width="100px" />
                                    </td>
                                    <td class="style51">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="14"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
