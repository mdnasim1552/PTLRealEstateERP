﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpAccount.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewBankConfirmation" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:Label ID="lblfrmdate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>



                                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:Label ID="lbltodate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>


                                            </div>
                                            <div class="col-md-3 pull-right">
                                            </div>

                                        </div>

                                    </div>
                                </fieldset>
                                <asp:GridView ID="gvCABankBal" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCABankBal_RowDataBound"
                                    ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">




                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid4" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True"
                                                                Text="Description of Accounts"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbankcb" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="250px">
                                                                      
                                                                      
                                                                      
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Change"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetbalcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Opening"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing "
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclosamcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>










                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="ViewSales" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:Label ID="sfrDate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>



                                                <asp:Label ID="Label13" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:Label ID="stDate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>

                                                <asp:Label ID="Label10" runat="server" CssClass="smLbl_to" Text="Project Name"></asp:Label>
                                                <asp:Label ID="lblPrijDesc" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>





                                            </div>
                                            <div class="col-md-3 pull-right">
                                            </div>

                                        </div>

                                    </div>
                                </fieldset>
                                <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvDayWSale_PageIndexChanging"
                                    ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDcuname" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                                    Font-Size="12px" ForeColor="White" Style="text-align: right" Width="150px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnit" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUSize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price per SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUpsft" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Team">
                                            <ItemTemplate>
                                                <asp:Label ID="lgDCper" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDTAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDSchdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDCandate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cudate")) %>'
                                                    Width="65px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDDisAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                 </FooterTemplate>
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDvDisPer" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="ViewSalDet" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="Label18" runat="server" CssClass="lblTxt lblName" Text="Net Sales Details"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:Label ID="lblSFrmDate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>



                                                <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:Label ID="lblSTrmDate" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>

                                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size:"></asp:Label>
                                                <div class="col-md-1 pading5px">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>150</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>300</asp:ListItem>
                                                        <asp:ListItem>600</asp:ListItem>
                                                        <asp:ListItem>900</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>




                                            </div>
                                            <div class="col-md-3 pull-right">
                                            </div>

                                        </div>

                                    </div>
                                </fieldset>
                                <asp:GridView ID="grvSalDet" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grvSalDet_PageIndexChanging"
                                    ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDcuname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agency Commsion">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvaAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "agamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFaAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvNAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewDetailsBal" runat="server">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDetails_RowDataBound"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcoded" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>'
                                                Width="120px">
                                                                          
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Description">
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                                    </td>
                                                    <td class="style59">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescriptiond" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>'
                                                Width="350px">
                                                             
                                                             
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfopDes" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount"
                                        ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfcloamtd" runat="server" CssClass="GridLebel"
                                                Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvclobald" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                        ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfopnamtd" runat="server" CssClass="GridLebel"
                                                Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvopnamtd" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                        HeaderText="Changes During the Period" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel" Font-Size="10px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>




                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewDetailsofInSt" runat="server">
                            <asp:GridView ID="gvInDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInDetails_RowDataBound"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid5" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcoded0" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>'
                                                Width="120px">
                                                                          
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Description">
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                                    </td>
                                                    <td class="style59">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCdataExel0" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescriptionind" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>'
                                                Width="350px">
                                                             
                                                             
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblfopDes0" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount"
                                        ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfcloamtind" runat="server" CssClass="GridLebel"
                                                Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvclobalind" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                        ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfopnamtind" runat="server" CssClass="GridLebel"
                                                Font-Bold="True"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvopnamtind" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                        FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcuamtind" runat="server" CssClass="GridLebel"
                                                Font-Size="10px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewCashFlow" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                                <asp:Label ID="lblcflowfrm" runat="server" CssClass="inputtextbox"></asp:Label>



                                                <asp:Label ID="Label5" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                <asp:Label ID="lblcflowto" runat="server" CssClass="inputtextbox"></asp:Label>


                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblrptlbl" runat="server" CssClass="smLbl_to" Text="Report Level"></asp:Label>
                                                <asp:DropDownList ID="DDListLevels" runat="server" CssClass="smDropDown inputTxt"
                                                    TabIndex="6">
                                                    <asp:ListItem Value="1">Level-1</asp:ListItem>
                                                    <asp:ListItem Value="2">Level-2</asp:ListItem>
                                                    <asp:ListItem Value="3">Level-3</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="lblOpeningDate" runat="server" CssClass="lblTxt lblName" Text="Opening Date"></asp:Label>
                                                <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtOpeningDate" Enabled="true">
                                                </cc1:CalendarExtender>

                                            </div>
                                        </div>

                                    </div>
                                </fieldset>
                            </div>

                            <div class="row">
                                <asp:GridView ID="grvCashFlow" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowDataBound="grvCashFlow_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="326px">
                                    <RowStyle Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnactDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="280px" Font-Underline="False" Style="color: Black"
                                                    OnClick="lbtnactDesc_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvcuamtcf" runat="server" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamcf" runat="server" ext='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclosamcf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                                <asp:Panel ID="PanelNote" runat="server" Visible="False">
                                    <asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Bank Status:"
                                        Width="120px"></asp:Label>
                                </asp:Panel>
                                <asp:GridView ID="gvbankbal" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="258px" OnRowDataBound="gvbankbal_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNcf" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Change">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbalambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvclosambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
