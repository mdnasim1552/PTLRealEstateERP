<%@ Page Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProductCost.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptProductCost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="true" Format="dd-MMM-yyyy" TargetControlID="txtfromdate" />




                        </div>
                    
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label14" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6" style="margin-top:20px;" >
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnShow_Click" >Ok</asp:LinkButton>
                        </div>



                    </div>



                </div>

                <div class="card-body">
                     <div class="table-responsive">
                
                     <asp:GridView ID="gvProCostAna" runat="server" ClientIDMode="Static" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True"  AllowPaging="True"   OnPageIndexChanging="gvProCostAna_PageIndexChanging"  OnRowDataBound="gvProCostAna_RowDataBound">
                     
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project" >
                                                  <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Width="180px"
                                                        Text="Project Name">
                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgProName" runat="server" Width="180px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Flat/Plot" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgflatplot" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Size(Sft)" >

                                                <FooterTemplate>
                                                   <asp:Label ID="lfTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Text="Total" ></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgsize" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Purchase Value" >
                                                <FooterTemplate>
                                                   <asp:Label ID="lFpurcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>



                                                <ItemTemplate>
                                                    <asp:Label ID="lpurcost" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purvalue")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Purchase Incentive" >
                                                 <FooterTemplate>
                                                   <asp:Label ID="lFpurincentive" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lpurincentive" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purinstive")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>





                                             <asp:TemplateField HeaderText="Other Cost" >
                                                 <FooterTemplate>
                                                   <asp:Label ID="lFothcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lothcost" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text=" - "></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Center" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>

                                            
                                               <asp:TemplateField HeaderText="Total Purchase Cost" >
                                                <FooterTemplate>
                                                   <asp:Label ID="lFtpurcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="ltpurcost" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpurcost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                   <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                            
                                           <asp:TemplateField HeaderText="Fixed Cost" >
                                                <FooterTemplate>
                                                   <asp:Label ID="lFfixedcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lfixedcost" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fxtcost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Variable Cost" >

                                                <FooterTemplate>
                                                   <asp:Label ID="lFvarcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lvarcost" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othmktexp")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                  <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>
                                             
       
                                           <asp:TemplateField HeaderText="Total Cost" >
                                                 <FooterTemplate>
                                                   <asp:Label ID="lFtotalcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="ltotalcost" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>


                                            
                                              <asp:TemplateField HeaderText="Committed Sale Value" >
                                                 <FooterTemplate>
                                                   <asp:Label ID="lFcommitedsaleval" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lcommitedsaleval" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "commitedval")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                  <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Margin / (Loss)" >
                                                <FooterTemplate>
                                                   <asp:Label ID="lFmarloss" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lmarloss" runat="server" Width="100px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "margin")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Actual Cost <br> incurred till <br> date" >

                                                 <FooterTemplate>
                                                   <asp:Label ID="lFactcostintill" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lactcostintill" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accosttill")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Total Days" >
                                                <ItemTemplate>
                                                    <asp:Label ID="ltdays" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ageing")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                  <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rem. Days" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lremdays" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainday")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle  HorizontalAlign="Right" Width="80px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
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