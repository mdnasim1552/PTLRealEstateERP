<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandOwnerPayment.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.LandOwnerPayment" %>

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
                                <div class=" form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Total Amount</asp:Label>
                                        <asp:TextBox ID="txtToamt" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Ins. Amount</asp:Label>
                                        <asp:TextBox ID="txtinsamt" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to">Start Date</asp:Label>
                                        <asp:TextBox ID="txtstrdate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>
                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Duration</asp:Label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddlPage" AppendDataBoundItems="True"
                                            Width="100px">
                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                            <asp:ListItem Value="7">7  Month</asp:ListItem>
                                            <asp:ListItem Value="8">8  Month</asp:ListItem>
                                            <asp:ListItem Value="9">9  Month</asp:ListItem>
                                            <asp:ListItem Value="10">10  Month</asp:ListItem>
                                            <asp:ListItem Value="11">11  Month</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnGenerate_OnClick">Generate</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-right" >
                                <asp:LinkButton ID="lbtnAddInstallment" runat="server"
                                    Visible="False" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAddInstallment_OnClick">Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <asp:GridView ID="gvOwnerPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." FooterText="Total ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Installment Date.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvinstdate" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "insdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvinstdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvinstdate">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_OnClick">Update</asp:LinkButton>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Installment Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />

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

