<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MonthsWiseSale.aspx.cs" Inherits="RealERPWEB.F_21_MKT.MonthsWiseSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $('.chzn-container').css('width', '250px');
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row mb-2" id="divFilter">
                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtfromdate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" id="lblToDate" runat="server" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="Cal3" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            </div>
                        </div>


                        <div class="col-md-3  ">
                            <div class="input-group input-group-alt profession-slect srDiv">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Lead Status</button>
                                </div>
                                <asp:DropDownList ID="ddlleadstatus" data-placeholder="Choose Lead Status.." runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 pading5px">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page</button>
                                </div>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">

                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem Selected="True">400</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvMonhtlySales" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="531px" OnRowCreated="gvMonhtlySales_RowCreated" AllowPaging="True" OnPageIndexChanging="gvMonhtlySales_PageIndexChanging" PageSize="15">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <%--<asp:LinkButton ID="lbYearbgdTotal" runat="server" OnClick="lbYearbgdTotal_Click" CssClass="btn btn-primary primaryBtn"> Total </asp:LinkButton>--%>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                         <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text=" Name" Width="160px"></asp:Label>

                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                            </asp:HyperLink>
                                        </HeaderTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblttl" Font-Bold="true" runat="server">Total</asp:Label>
                                          
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvempname" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="200px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Jan">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty1" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty1" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
 
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Feb">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty2" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty2" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>

                                             
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mar">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty3" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty3" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Apr">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty4" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblgvqty4" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="May">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty5" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblgvqty5" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jun">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty6" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty6" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Jul">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty7" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty7" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Aug">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty8" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblgvqty8" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty8")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sep">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty9" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblgvqty9" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty9")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Oct">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty10" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblgvqty10" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty10")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nov">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty11" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblgvqty11" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty11")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dec">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFqty12" runat="server" Width="40px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty12" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty12")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Total Qty">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtqty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtqty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                            VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
