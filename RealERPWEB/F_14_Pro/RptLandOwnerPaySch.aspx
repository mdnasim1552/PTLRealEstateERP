<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptLandOwnerPaySch.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptLandOwnerPaySch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            <%--var gv1 = $('#<%=this.gvSpaymentland.ClientID %>');
            gv1.Scrollable();--%>
       
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                     <div class="col-md-2 pading5px ">
                                         <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
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
                                CssClass=" table-striped table-hover table-bordered grvContentarea">
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
                                            <asp:HyperLink ID="hypcustomer" runat="server" Width="350px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:HyperLink>
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
                                    ShowFooter="True" OnRowDataBound="gvPayment_RowDataBound">
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
                                      <%--  <asp:CommandField ShowDeleteButton="True" />--%>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvDate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                    Width="80px" Font-Size="11px"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="80px">Total</asp:Label>
                                            </FooterTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaydate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paiddate"))%>'
                                                    Width="80px" Font-Size="11px"></asp:Label>
                                               
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Voucher Num. ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                   Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'                                                   
                                                    Width="80px" Font-Size="11px"></asp:Label>
                                               
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MR Num. ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmrnum" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="80px" Font-Size="11px"></asp:Label>
                                               
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Num. ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchqnum" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="100px" Font-Size="11px"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px">Total Payment</asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr style="background:green;">
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFFF" Style="text-align: right" Width="100px">Payment Due</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px">Total Due</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                             <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtpayAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lpayfAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr style="background:green;">
                                                        <td>
                                                            <asp:Label ID="lpayfdue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFFF" Style="text-align: right" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="ltotaldue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                            </FooterTemplate>
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

