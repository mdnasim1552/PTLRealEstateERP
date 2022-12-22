<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SalesAllReports.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesAllReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });


            var gv = $('#<%=this.gvprjcoll.ClientID %>');
            gv.Scrollable();
            var gvqbasis = $('#<%=this.gvQtyBasis.ClientID %>');
            gvqbasis.Scrollable();
            var gvAmtPeriodic = $('#<%=this.gvAmtPeriodic.ClientID %>');
            gvAmtPeriodic.Scrollable();
            var gvdvQtyBasisPeriodic = $('#<%=this.dvQtyBasisPeriodic.ClientID %>');
            gvdvQtyBasisPeriodic.Scrollable();
        }

    </script>

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



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">Reports</label>
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="custom-select  chzn-select" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="amtbasis">-------------Select-----------</asp:ListItem>
                                    <asp:ListItem Value="PrjCollect">Project Wise Colletion</asp:ListItem>

                                    <asp:ListItem Value="PrjCollDateWise">Project Wise Colletion</asp:ListItem>
                                    <asp:ListItem Value="PaymentStatus">Payment Status</asp:ListItem>
                      <%--              <asp:ListItem Value="qtybasisp">Inventory Report-Quantity Basis(Periodic)</asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="Label1" runat="server" class="control-label" for="ddlUserName">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select"
                                   
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
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
                </div>
            </div>

            <div class="card card-fluid">
                <div class="row">
                    <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate" id="lblDate" runat="server">From</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To</label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>

                        </div>

                              <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label">Project Name</label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                    <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                </div>


            </div>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Viewprjcoll" runat="server">

                            <asp:GridView ID="gvprjcoll" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True"  AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprjName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvprjNameF"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtamount" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFtamount"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>                                                                                                   
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="View1ConBIll" runat="server">
                            <asp:GridView ID="gvQtyBasis" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" OnPageIndexChanging="gvQtyBasis_PageIndexChanging" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNoqty" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>


                                            <asp:HyperLink ID="hlnkgcResDesqtcqty" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="150px">
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRecamtqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer In">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTraninqtybas" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTranoutqtybas" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Damage/Lost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDamageqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTamtqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netrcvamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvIsuamtqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted <br/> Consumption">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdconuamtqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdconamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAcSqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstock")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budgeted Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdstkqty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdstock")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Variance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdstkamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ViewAmtPeriodic" runat="server">
                            <asp:GridView ID="gvAmtPeriodic" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvAmtPeriodic_PageIndexChanging"
                                ShowFooter="True" OnRowDataBound="gvAmtPeriodic_RowDataBound" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNop" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>


                                            <asp:HyperLink ID="hlnkgcResDescp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="170px">
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblTotal">Total</asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvOpnamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblopnf"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRecamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblrcvfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer In">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTraninamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lbltinfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTranoutamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lbltoutfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Damage/Lost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDamagep" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lbllstfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netrcvamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblnetrcvfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvIsuamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblisufp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAcSamtp" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstock")).ToString("#,##0;(#,##0); ")%>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblactstktfp"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">

                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkNetBasis" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false" Target="_blank" Width="50px">Net Inv.                                                
                                            </asp:HyperLink>
                                        </ItemTemplate>


                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="View1" runat="server">
                            <asp:GridView ID="dvQtyBasisPeriodic" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="dvQtyBasisPeriodic_PageIndexChanging"
                                ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNopqty" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>


                                            <asp:HyperLink ID="hlnkgcResDescq" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="170px">
                                            </asp:HyperLink>

                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvOpnamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRecamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer In">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTraninamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Transfer Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTranoutamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Damage/Lost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDamageq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvTamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netrcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvIsuamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAcSamtq" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actstock")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>




            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
