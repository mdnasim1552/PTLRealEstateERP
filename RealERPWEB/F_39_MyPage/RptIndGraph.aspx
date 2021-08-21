<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptIndGraph.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptIndGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Employee List"></asp:Label>
                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnemployee" runat="server" TabIndex="4" CssClass="btn btn-primary srearchBtn" OnClick="lbtnemployee_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>


                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlemplist" runat="server" CssClass=" form-control inputTxt"
                                            TabIndex="5">
                                        </asp:DropDownList>


                                    </div>
                                    <div class=" col-md-1 smLbl_to">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmonth" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>
                                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass=" ddlPage">
                                        </asp:DropDownList>
                                    </div>



                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Graph Type:"></asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                            TabIndex="11" CssClass="ddlPage125 inputTxt">
                                            <asp:ListItem Text="Monthly" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Yearly(Bar Graph)"></asp:ListItem>
                                            <asp:ListItem Text="Yearly(Line Graph)"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Viewbargraph" runat="server">
                            <div class="row">

                                <asp:Chart ID="Chart1" runat="server" Width="1191px" Height="400px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#2fd1f9" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="Pink" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4" YValuesPerPoint="6">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                                <MajorGrid Enabled="False" />

                                            </AxisX>
                                            <AxisY>
                                                <MajorGrid Enabled="False" />
                                            </AxisY>

                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend></asp:Legend>
                                    </Legends>
                                </asp:Chart>
                            </div>
                        </asp:View>

                        <asp:View ID="Viewmonbardetailssales" runat="server">

                            <asp:Chart ID="Chartmonsales" runat="server" Width="1191px" Height="400px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#2fd1f9" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="Pink" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100" Interval="1">
                                            <MajorGrid Enabled="False" />

                                        </AxisX>
                                        <AxisY>
                                            <MajorGrid Enabled="False" />
                                        </AxisY>

                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                            </asp:Chart>

                            <asp:GridView ID="gvEmpEval" runat="server" Width="200px"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvEmpEval_RowCreated">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Month">


                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFTotal" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="Total"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblgvFper" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="%"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmon" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"yearmon")) %>'
                                                Width="60px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt1" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt2" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt2")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Offer">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt3" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt4" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Call">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt5" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltamt6" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtamt6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtamt6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt1" runat="server"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt1")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt2" runat="server"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt2")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Offer">
                                        <ItemTemplate>

                                            <asp:Label ID="lblamt3" runat="server"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt3")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Visit">
                                        <ItemTemplate>

                                            <asp:Label ID="lblamt4" runat="server"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt4")).ToString("#,##0;(#,##0); ")  %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Call">
                                        <ItemTemplate>

                                            <asp:Label ID="lblamt5" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Others">
                                        <ItemTemplate>

                                            <asp:Label ID="lblamt6" runat="server"
                                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt6")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                                Width="50px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                        </ItemTemplate>



                                        <FooterTemplate>
                                            <asp:Label ID="lblFamt6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFpertamt6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Percent">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltpar" runat="server"
                                                Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tper")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                                Width="55px"
                                                Style="text-align: right" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="gpa">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltpar" runat="server"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))%>'
                                                Width="120px"
                                                Style="text-align: left" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>








                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="Viewmonbardetailscollection" runat="server">
                        </asp:View>

                        <asp:View ID="Viewmonbardetailsother" runat="server">
                            <asp:Chart ID="Chartmonoth" runat="server" Width="1191px" Height="400px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#2fd1f9" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="Pink" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100" Interval="1">
                                            <MajorGrid Enabled="False" />

                                        </AxisX>
                                        <AxisY>
                                            <MajorGrid Enabled="False" />
                                        </AxisY>

                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                            </asp:Chart>

                            <asp:GridView ID="gvempevagen" runat="server" Width="200px"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Month">


                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFTotalgen" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="Total"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblgvFper" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="%"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmongen" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"yearmon")) %>'
                                                Width="60px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtarget" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvftarget" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtarget" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvactual" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"acmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfactual" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFperontar" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="gpa">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltpargen" runat="server"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))%>'
                                                Width="120px"
                                                Style="text-align: left" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>








                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="Viewmonempevaavgsales" runat="server">
                            <div class="row">

                                <asp:Chart ID="chrtlinegraphsales" runat="server" Width="1191px" Height="400px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#0033cc" BorderWidth="3" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#ff0066" BorderWidth="3" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#00cc66" BorderWidth="3" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="Circle" MarkerBorderWidth="30" Name="Series3" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                                <MajorGrid Enabled="False" />

                                            </AxisX>
                                            <AxisY>
                                                <MajorGrid Enabled="False" />
                                            </AxisY>

                                        </asp:ChartArea>
                                    </ChartAreas>
                                    <Legends>
                                        <asp:Legend></asp:Legend>
                                    </Legends>
                                    <BorderSkin BorderWidth="3" />
                                </asp:Chart>


                                <asp:GridView ID="gvEmpEvalavg" runat="server" Width="200px"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvEmpEvalavg_RowCreated">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoavg" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="5px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Month">


                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalavg" runat="server"
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="Total"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblgvFperavg" runat="server"
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="%"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmonavg" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"yearmon")) %>'
                                                    Width="60px"
                                                    Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg1" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamtavg1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg2" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamt2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Offer">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg3" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamtavg3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg4" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamt4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Call">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg5" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamtavg5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Others">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltamtavg6" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tamt6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtamtavg6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFgtamtavg6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamtavg1" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt1")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg1" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamtavg2" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt2")).ToString("#,##0;(#,##0); ")%>'
                                                    Width="80px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg2" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Offer">
                                            <ItemTemplate>

                                                <asp:Label ID="lblamtavg3" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt3")).ToString("#,##0;(#,##0); ")  %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg3" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit">
                                            <ItemTemplate>

                                                <asp:Label ID="lblamtavg4" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt4")).ToString("#,##0;(#,##0); ")  %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>



                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg4" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Call">
                                            <ItemTemplate>

                                                <asp:Label ID="lblamtavg5" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>



                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg5" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Others">
                                            <ItemTemplate>

                                                <asp:Label ID="lblamtavg6" runat="server"
                                                    Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem,"amt6")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                                    Width="50px"
                                                    Style="text-align: right" BackColor="Transparent" BorderStyle="None" Font-Size="13px"></asp:Label>
                                            </ItemTemplate>



                                            <FooterTemplate>
                                                <asp:Label ID="lblFamtavg6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFpertamtavg6" runat="server" Style="text-align: right; color: black;" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Percent">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltparavg" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tper")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                                    Width="55px"
                                                    Style="text-align: right" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Average">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltavgsales" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem,"avgmark")).ToString("#,##0.00;(#,##0.00); ")+"</B>" %>'
                                                    Width="55px"
                                                    Style="text-align: right" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="gpa">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgpaavg" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))%>'
                                                    Width="120px"
                                                    Style="text-align: left" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <asp:View ID="ViewmonempevaavgColl" runat="server">
                        </asp:View>
                        <asp:View ID="Viewmonempevaavgoth" runat="server">
                            <asp:Chart ID="chrtlinegraphoth" runat="server" Width="1191px" Height="400px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#0033cc" BorderWidth="3" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#ff0066" BorderWidth="3" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#00cc66" BorderWidth="3" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="Circle" MarkerBorderWidth="30" Name="Series3" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100" Interval="1">
                                            <MajorGrid Enabled="False" />

                                        </AxisX>
                                        <AxisY>
                                            <MajorGrid Enabled="False" />
                                        </AxisY>

                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                                <BorderSkin BorderWidth="3" />
                            </asp:Chart>
                            <asp:GridView ID="gvempevagenavg" runat="server" Width="200px"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                CssClass="table table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Month">


                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFTotalavggen" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="Total"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblgvFper" runat="server"
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None" Text="%"></asp:Label>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmonavggen" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"yearmon")) %>'
                                                Width="60px"
                                                Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>






                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtargetavg" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"tmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvftargetavg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFgtargetavg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvactualavg" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"acmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfactualavg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFperontaravg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Average">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvavrageavg" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"avgmark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"
                                                Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfaverageavg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblFperonavg" runat="server" Style="text-align: right; color: black;" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="gpa">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltpargenavg" runat="server"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem,"gpa"))%>'
                                                Width="120px"
                                                Style="text-align: left" BackColor="Transparent" Font-Size="13px" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>








                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>
                    </asp:MultiView>




                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-16 formBtn ">

                                            <div class="pull-right">

                                                <asp:HyperLink ID="lnkbtnClose" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="~/F_05_MyPage/EmpKpiEntry04.aspx?Type=Mgt"><span class="flaticon-delete47 text-danger "></span>Close</asp:HyperLink>


                                            </div>
                                        </div>





                                    </div>



                                </div>
                            </fieldset>
                        </div>
                    </div>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

