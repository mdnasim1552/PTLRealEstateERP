<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandOwnerPaymentSch.aspx.cs" Inherits="RealERPWEB.F_14_Pro.LandOwnerPaymentSch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Accounts Head:"></asp:Label>
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt" TabIndex="3" Width="287">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" Width="287px" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>
                                    </div>
                                    <div style="margin-left: -73px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div style="margin-bottom: 35px;">
                            <asp:GridView ID="gvSpaymentland" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvSpaymentland_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <%-- <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />--%>

                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="hypcustomer" runat="server" Width="350px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>


                                            <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize" Style="text-align: right;" OnClick="lbtnusize_OnClick" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Height="18px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vou. Date">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvoudat" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher Num">

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:HyperLink Target="_blank" ID="lgvvounum" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="90px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminsbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>--%>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" Visible="false" style="margin-left: -70px;" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>
                        </div>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View runat="server" ID="gvLndPayment">
                                <fieldset class="scheduler-border fieldset_B" runat="server" visible="false">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                           
                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                            <%--<asp:LinkButton ID="lbtnBack" runat="server"
                                            OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>--%>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>
                                
                                <div class="row">
                                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="223px" OnRowDeleting="gvPayment_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode3" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatpayment" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatpayment_Click">Update Payment Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lTotalPayment" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                </div>
                                
                            </asp:View>
                        </asp:MultiView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

