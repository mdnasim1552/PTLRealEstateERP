<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BillingRateEntry.aspx.cs" Inherits="RealERPWEB.F_16_Bill.BillingRateEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


            <%-- var gv = $('#<%=this.gvbillstatus.ClientID %>');
            gv.Scrollable();--%>

        }


    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-header">
                    <div class="row mt-4">
                      
                         
                                    <div class="col-md-3 d-none">
                                        <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt smLbl_to lblName" Text="Project/Unit"></asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>

                                    <div class="col-md-3">
                                        
                                           <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Project/Unit"></asp:Label>
                                        <asp:Label ID="lblProjectDesc" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 ml-2" style="margin-top:20px;">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" stylr="margin-left=-50px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk1_Click" Text="Ok"></asp:LinkButton>
                                    </div>
                            

                           
                                <div class="col-md-1">
                                    <asp:Label ID="lblpage" runat="server" CssClass="form-label" Text="Page Size:" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="12px" CssClass="form-control form-control-sm"
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
                                    <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"></asp:Label>
                                </div>
                            </div>
               
                  </div>
                <div class="card-body">

                        <asp:GridView ID="gvRptResBasis" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                            ShowFooter="True" Width="1054px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvRptResBasis_RowCreated">
                            <PagerSettings PageButtonCount="20" Mode="NumericFirstLast" />
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Work Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "sdetails").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="500px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Catagory">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-primary btn-xs" OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblbgdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkfinalup" runat="server" CssClass="btn  btn-danger btn-xs" OnClick="lnkfinalup_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbillrate" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Amount">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFordam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblordam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordam")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText=" Rate ">

                                    <ItemTemplate>
                                        <asp:Label ID="lblbgdrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbgddam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblbgddam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Gross Margin">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFmargin" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmargin" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perobgd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
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

