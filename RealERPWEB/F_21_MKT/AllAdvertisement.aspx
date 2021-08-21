<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AllAdvertisement.aspx.cs" Inherits="RealERPWEB.F_21_MKT.AllAdvertisement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppnlclint" runat="server">
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
                    <div class="row" id="topsheet" runat="server">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnok_OnClick">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-1" runat="server" id="newEntry" >
                                        <asp:HyperLink ID="lbtnNew" runat="server" CssClass="btn btn-sm btn-primary" NavigateUrl="AdvertisementEntry.aspx?Type=Entry&genno=" Target="_blank"><span class="glyphicon glyphicon-plus">New</span></asp:HyperLink>

                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <div class="row">
                            <div class="col-md-12 table table-responsive">
                                <asp:GridView ID="gvTopview" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvTopview_OnRowDataBound"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">

                                            <ItemTemplate>
                                                <asp:Label ID="slno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Ad. No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtadno"
                                                    runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                    Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Style="text-align: left; font-size: 11px;"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Media">
                                            <ItemTemplate>
                                                <asp:Label ID="txtmedia"
                                                    runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "papdesc")) %>'
                                                    Width="250px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtamt" runat="server" Style="text-align: right; font-size: 11px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hbtnEdit" runat="server" CssClass="btn btn-sm btn-primary" Target="_blank"><span class="glyphicon glyphicon-edit"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

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
                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


