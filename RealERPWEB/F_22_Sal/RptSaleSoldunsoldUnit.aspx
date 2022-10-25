<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSaleSoldunsoldUnit.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSaleSoldunsoldUnit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/gridviewScrollHaVer.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
            }
            catch (e) {
                alert(e);
            }
            //lert("I m In");
            var gvSpayment = $('#<%=this.gvSpayment.ClientID %>');
            gvSpayment.Scrollable();

            //alert("I m In");
            //gvSpayment.gridviewScroll({
            //     width: 1160,
            //     height: 420,
            //     arrowsize: 30,
            //     railsize: 16,
            //     barsize: 8,
            //     varrowtopimg: "../Image/arrowvt.png",
            //     varrowbottomimg: "../Image/arrowvb.png",
            //     harrowleftimg: "../Image/arrowhl.png",
            //     harrowrightimg: "../Image/arrowhr.png",
            //     freezesize: 7
            // });
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                <asp:Label class="control-label" ID="Label15" runat="server">Date:</asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="lbltodate" id="lbltodate" runat="server">To:</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">Project Name:</label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select  chzn-select form-control">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2" id="salesteamdv" runat="server">
                            <div class="form-group">
                                <label class="control-label" for="lblSalesTeam" id="lblSalesTeam" runat="server">Sales Team:</label>
                                <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="custom-select  chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName" id="lblGroup" runat="server">Group</label>
                                <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="custom-select  chzn-select">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-2" id="SoldType" runat="server" visible="false">
                            <label class="control-label" for="ddlUserName" id="Label1" runat="server">Type</label>

                            <asp:RadioButtonList ID="rbtnSalType" RepeatDirection="Horizontal" CssClass=""  runat="server">
                                <asp:ListItem>Sold</asp:ListItem>
                                <asp:ListItem>UnSold</asp:ListItem>
                            
                                <asp:ListItem Selected="True">Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label d-block" for="ddlpagesize" id="lblPage" visible="false" runat="server">Page Size</label>
                           
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select form-control"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
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
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class=" table-responsive">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="SoldUnsold" runat="server">


                                    <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="False"
                                        OnPageIndexChanging="gvSpayment_PageIndexChanging" OnRowDataBound="gvSpayment_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">                                              

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="120px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Customer Name" Width="120px"></asp:Label>
                                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel"></i>
                                                    </asp:HyperLink>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCuName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Budgeted Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="35px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTqty" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>







                                            <asp:TemplateField HeaderText="Sold Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="35px" Style="text-align: right; color: red;"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSqty" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right; color: red;" Width="35px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Size(Sold)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsusize" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "susize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTsusize" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate(Sold)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsrate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sold Amt">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lgvSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>

                                                    <asp:HyperLink ID="HplgvAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvparking" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parking")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFParking" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Utility">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvutility" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFutility" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Others">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvother" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothers" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtotal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tacsalamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Sold Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSchdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                        Width="65px" Style="text-align: left"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Unsold Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUsqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="35px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFUsqty" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Size(Unsold)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusize" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTusize" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Rate(Unsold)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvurate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unsold Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUsAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usuamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDisAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDisAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvColAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFColAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Receivable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRcvAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFRcvAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDisPer" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                <asp:View ID="ParkingStatus" runat="server">

                                    <asp:GridView ID="gvparking" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                        OnPageIndexChanging="gvparking_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description of Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvtotal" Text="Total" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvftoqty" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sold">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPSAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sold")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPSAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unsold">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPUsAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsold")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPUsAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Parking Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTqty" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bstat")) %>'
                                                        Width="70px" Style="text-align: left"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTqty" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="120px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

                                <asp:View ID="RptDayWSale" runat="server">
                                    <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                                        OnPageIndexChanging="gvDayWSale_PageIndexChanging"
                                        OnRowDataBound="gvDayWSale_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDPactdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDcuname" runat="server"
                                                        Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                                        ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgUnit" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                        Width="35px"></asp:Label>


                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUSize" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="55px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price per SFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUpsft" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="55px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Team">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgDCper" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                        Width="120px"></asp:Label>


                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Budgeted Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDTAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sold Amt">
                                                <ItemTemplate>

                                                    <asp:HyperLink ID="HplgvSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>


                                                    <%-- <asp:Label ID="lgvDSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sold Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDSchdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                        Width="65px" Style="text-align: left"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDDisAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDvDisPer" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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

                                <asp:View ID="ViewUWCosting" runat="server">

                                    <asp:GridView ID="gvUnit" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" OnRowCreated="gvUnit_RowCreated">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description of Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItemdesc" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUSize" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lFUsize" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Unit Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvuamt" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPamt" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Utility">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUtility" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPUtility" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Co-operative">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPCooprative" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPCooprative" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText=" Total Unit Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtRate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTamt" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTAmt" runat="server" Font-Bold="True"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Construcction">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvccostpsft" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbgdpsft")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Overhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlcostpsft" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lbgdpsft")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtcostpsft" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tobgdpsft")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

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
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>
                                </asp:View>

                            </asp:MultiView>
                        </div>
                    </div>


                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>



