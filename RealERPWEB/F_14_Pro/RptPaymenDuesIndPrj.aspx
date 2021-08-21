<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPaymenDuesIndPrj.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPaymenDuesIndPrj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvAcc.ClientID%>')
                    .gridviewScroll({
                        width: 1165,
                        height: 420,
                        arrowsize: 30,
                        railsize: 16,
                        barsize: 8,
                        headerrowcount: 2,
                        freezeFooter: true,
                        varrowtopimg: "../Image/arrowvt.png",
                        varrowbottomimg: "../Image/arrowvb.png",
                        harrowleftimg: "../Image/arrowhl.png",
                        harrowrightimg: "../Image/arrowhr.png",
                        freezesize: 5


                    });
            }

            catch (e) {

                alert(e.message);

            }

        }
     </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:calendarextender id="txtDatefrom_CalendarExtender" runat="server" format="dd-MMM-yyyy"
                                                              targetcontrolid="txtDatefrom" enabled="true"></cc1:calendarextender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:calendarextender id="txtDateto_CalendarExtender" runat="server" format="dd-MMM-yyyy"
                                                              targetcontrolid="txtDateto" enabled="true"></cc1:calendarextender>
                                        
                                    </div>
                                  
                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName"  Text="Accounts Head:"></asp:Label>
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt" TabIndex="3" Width="215">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="margin-left: -73px">
                                        <asp:LinkButton  ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>
                                    
                                    
                                </div>
                            </div>
                        </fieldset>
                        
                        <asp:GridView ID="gvAcc" runat="server"  AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered table-responsive grvContentarea" PageSize="15" ShowFooter="True" Width="841px" OnRowDataBound="gvAcc_OnRowDataBound">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblAccDesc" Target="_blank" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc1")) %>' Width="210"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LnkfTotal" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000">Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Unit">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdunit" runat="server"
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' Width="60"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfqty" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdqty" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdrat" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkftmat" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblbgdam" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField  HeaderText="Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfactqty" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblpurqty" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqty")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                            
                                        
                                        <asp:TemplateField  HeaderText="Rate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblpurrat" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purrat")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfacttamt" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpuramt" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puramt")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Paid" >
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpayp" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpayamt" runat="server"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Pay Due">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpaydue" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpaydue" runat="server" ForeColor="White" BackColor="Green"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paydue")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="80"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Total Due">
                                            <FooterTemplate>
                                                <asp:Label ID="Lnkfpaytmt" runat="server"  Font-Bold="True"
                                                           Font-Size="12px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblpayduet" runat="server" Target="_blank"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payduet")).ToString("#,##0.00;(#,##0.00); ") %>'  Width="90"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

