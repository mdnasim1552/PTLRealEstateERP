
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProBillStatus.aspx.cs" Inherits="RealERPWEB.F_41_GAcc.RptProBillStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server" Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" CssClass="ddlistPull"
                                            AutoPostBack="True">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lblPage" Visible="False" runat="server" Text="Page:" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="12px"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                </div>
                            </div>
                        </fieldset>

                        <asp:GridView ID="gvProBillSt" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True"
                            OnRowDataBound="gvProBillSt_RowDataBound"
                            OnPageIndexChanging="gvProBillSt_PageIndexChanging" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActdesc" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="150px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>

                                         <asp:HyperLink ID="hlnkgvvounum" runat="server" 
                                                  Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="70px"></asp:HyperLink>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>


                                        <asp:HyperLink ID="hlnkgvbillno" runat="server" 
                                                  Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="70px"></asp:HyperLink>


                                      
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill Amount">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvbillamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Advanced">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvadvanced" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                              
                                <asp:TemplateField HeaderText="Tax">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtaxAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vat">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvVatAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tax">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSdAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                               
                               <asp:TemplateField HeaderText="Net Bill">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvNbillAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbillam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                               

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerSettings Visible="False" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

