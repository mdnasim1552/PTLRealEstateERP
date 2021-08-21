<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAdvanceVsPayment.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptAdvanceVsPayment" %>

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
                                <div class="form-group">
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

                                </div>

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

                                        <asp:TextBox ID="txtSrcRefno" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindRefno" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="imgbtnFindRefno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="table-responsive">

                    <asp:GridView ID="gvSubBill" runat="server" AllowPaging="True"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        ShowFooter="True" Width="650px" OnPageIndexChanging="gvSubBill_PageIndexChanging">
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
                                          ForeColor="#000">Total</asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Supplier Name">


                                <ItemTemplate>
                                    <asp:Label ID="lgvSupplier" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="P/O No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvisueno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno2")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            


                            <asp:TemplateField HeaderText="P/O Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdate" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P/O Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvissueamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderamt")).ToString("#,##0.00;(#,##0.00); ") %>'
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

                            <asp:TemplateField HeaderText="Advanced </br> Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvadvamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFOAdvAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Voucher</br> No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbillno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                              <asp:TemplateField HeaderText="Voucher </br> Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvvouno" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vouamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" ></asp:Label>
                                </ItemTemplate>

                                  <FooterTemplate>
                                    <asp:Label ID="lgvFVouAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                          ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                 <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcbillrefno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Bank Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbank" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bank")) %>'
                                        Width="220px"></asp:Label>
                                </ItemTemplate>
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


