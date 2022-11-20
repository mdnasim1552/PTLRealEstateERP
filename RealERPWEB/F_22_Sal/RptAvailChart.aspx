<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAvailChart.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptAvailChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            //$(".pop").on("click", function () {
            //    $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
            //    $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            //});
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <style type="text/css">
        .table th, .table td {
            padding: 4px;
        }
        .florHead{
            line-height:40px;
        }
    </style>

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
    <div class="card mt-4">
        <div class="card-body">
            <div class="row mb-4">



                <div class="col-md-5 pading5px asitCol5 d-none">

                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox inputTxt"></asp:TextBox>
                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label4" runat="server" CssClass="form-lable" Text="Project Name:"></asp:Label>
                    <asp:DropDownList ID="ddlProjectName" CssClass="chzn-select form-control" runat="server" Font-Bold="True">
                    </asp:DropDownList>

                </div>
                <div class="col-md-1" style="margin-top: 22px;">
                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                        OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                </div>



                <div class="col-md-1">

                    <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Size:"></asp:Label>

                    <asp:DropDownList ID="ddlpagesize" CssClass="form-control from-control-sm" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="70px">
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
               
                <div class="col-md-4" style="margin-top: 22px;">
                    <div class="metric-badge">
                        <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Flug:"></asp:Label>
                        <span class="badge badge-lg badge-danger bg-red">Sold</span>
                        <span class="badge badge-lg badge-success">Unsold</span>
                        <span class="badge badge-lg badge-primary bg-blue">Mgt Booking</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="card" style="min-height: 480px">
        <div class="card-header pt-0 pb-0">
        </div>
        <div class="card-body">

            <div class="row" id="divGVData" runat="server">
                <div class="table table-responsive">
                    <asp:GridView ID="gvAailChart" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvAailChart_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpactdesc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))
                                                  %>'
                                        Width="100px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvFlrdesc3" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc3")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "flrdesc").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc3")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim() : "")%>'
                                        Width="120px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvFlrdesc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                        Width="120px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnitdesc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="120px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCustdesc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="150px"></asp:Label>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text=""
                                        ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgUnitn" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty.">
                                <ItemTemplate>
                                    <asp:Label ID="lgQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFQty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="30px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgSize1" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFSize1" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgRate" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="%">
                                <ItemTemplate>
                                    <asp:Label ID="lgper" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parcent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Description(Land Owner)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnitdesc2" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc2")) %>'
                                        Width="150px"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty.">
                                <ItemTemplate>
                                    <asp:Label ID="lgLoqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "luqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFLoqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="30px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgSize2" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ownsize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFSize2" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Parking">
                                <ItemTemplate>
                                    <asp:Label ID="lgpamt" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utility">
                                <ItemTemplate>
                                    <asp:Label ID="lgputility" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Co-operative">
                                <ItemTemplate>
                                    <asp:Label ID="lgpCooperative" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgpfamt" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "famt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
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
            <div id="divgvChart" visible="false" runat="server">

                <div id="divUnitGraph" runat="server"></div>
                <div class="row d-none">

                    

                    <div class="col-md-2">
                        <h5 class="text-right">1st Floor</h5>
                    </div>
                    <div class="col-md-10">
                        <asp:HyperLink ID="HyperLink21" runat="server" class="bg-red btn text-white">Unit 1 </asp:HyperLink>
                    </div>
                     
                </div>
                
            </div>
        </div>


    </div>




</asp:Content>


