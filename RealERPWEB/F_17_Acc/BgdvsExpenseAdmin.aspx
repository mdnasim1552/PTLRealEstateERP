<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdvsExpenseAdmin.aspx.cs" Inherits="RealERPWEB.F_17_Acc.BgdvsExpenseAdmin" %>

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

                            <div class="form-group">
                                <div class="col-md-3 pading5px">
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>


                                    <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                    <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>


                                </div>
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lnkshow" OnClick="btnShow_Click" runat="server" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>

                        </fieldset>
                    </div>



                    <div class="table-responsive">
                        <asp:GridView ID="gvBgdExpense" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvBgdExpense_OnRowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                               
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblactdesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                              
                               
                                <asp:TemplateField HeaderText="Bgd. Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbgdex" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Act. Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lblactex" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actcost")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Prv. Year Act. Exp">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprvyex" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preyrcost")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


