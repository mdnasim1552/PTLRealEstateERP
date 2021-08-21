<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptOrderVsReceive.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptOrderVsReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Project Name" Font-Size="11px" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindSubConName0" CssClass="btn btn-primary srearchBtn" runat="server"  TabIndex="2" OnClick="ibtnFindSubConName0_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="336px" AutoPostBack="True" CssClass="chzn-select ddlistPull">
                                        </asp:DropDownList>
                                        </div>
                                    <div class="col-md-1 asitCol1">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server"
                               CssClass="btn btn-primary primaryBtn" Style="margin-left:-13px;" OnClick="lnkbtnOk_Click" >Ok</asp:LinkButton>
                                    </div>

                                </div>
                          <%--      <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label1" runat="server"
                                            Text="Supplier Name" Font-Size="11px" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcSupplier" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindSupplier" CssClass="btn btn-primary srearchBtn" runat="server"  TabIndex="2" OnClick="imgbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlSupplierName" runat="server"
                                            Width="336px" AutoPostBack="True" CssClass="chzn-select ddlistPull">
                                        </asp:DropDownList>
                                       
                                    </div>

                                </div>--%>

                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                            Text="To:" CssClass="smLbl_to" Visible="true"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                            Text="Size:"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblPo" runat="server" CssClass="smLbl_to"
                                            Text="PO :"></asp:Label>

                                        <asp:TextBox ID="txtSrcRefno" runat="server" CssClass="inputtextbox" Width="120px"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindRefno" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="imgbtnFindRefno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="table-responsive">

                    <asp:GridView ID="gvOrder" runat="server" AllowPaging="True"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        ShowFooter="True" Width="650px" OnPageIndexChanging="gvOrder_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">

                                <FooterTemplate>
                                    <asp:Label ID="torlal" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvreqno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Req Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvreqdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="MRF No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrf" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MRR No" >
                                <ItemTemplate>
                                    <asp:Label ID="lgvMrrno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                                <asp:TemplateField HeaderText="MRR Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lgvMrrdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            
                                <asp:TemplateField HeaderText="MRR ref" >
                                <ItemTemplate>
                                    <asp:Label ID="lgvMrrref" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Order No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Order No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            


                            <asp:TemplateField HeaderText="Order Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderdate" runat="server"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Order ref">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderref" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                     <FooterTemplate>
                                    <asp:Label ID="lgvFOrderref" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"> Total:</asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Materials Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmatname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>

                                     <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Order Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderqty" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFOrderAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MRR Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFmrrqty" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrrrate" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                              <asp:TemplateField HeaderText=" MRR Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrramt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" ></asp:Label>
                                </ItemTemplate>

                                  <FooterTemplate>
                                    <asp:Label ID="lgvFmrrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
