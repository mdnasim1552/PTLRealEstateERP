<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNew.Master" AutoEventWireup="true" CodeBehind="RptReceivedList04.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptReceivedList04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>        
        .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 28px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });


        });
        function pageLoaded() {







            var gvmoncollsch = $('#<%=this.gvmoncollsch.ClientID %>');
            var gvmoncoll = $('#<%=this.gvmoncoll.ClientID %>');
            var gvmoncolschandac = $('#<%=this.gvmoncolschandac.ClientID %>');


            //gvmoncollsch.gridviewScroll({

            //    width: 1260,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    varrowtopimg: "../Image/arrowvt.png",
            //    varrowbottomimg: "../Image/arrowvb.png",
            //    harrowleftimg: "../Image/arrowhl.png",
            //    harrowrightimg: "../Image/arrowhr.png",
            //    freezesize: 4
            //});


            //gvmoncoll.gridviewScroll({

            //    width: 1260,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    varrowtopimg: "../Image/arrowvt.png",
            //    varrowbottomimg: "../Image/arrowvb.png",
            //    harrowleftimg: "../Image/arrowhl.png",
            //    harrowrightimg: "../Image/arrowhr.png",
            //    freezesize: 7
            //});

            gvmoncolschandac.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });

            var gridViewScroll = new GridViewScroll({
                elementID: "gvmoncollsch",
                width: 1400,
                height: 500,
                freezeColumn: true,
                freezeFooter: true,
                freezeColumnCssClass: "GridViewScrollItemFreeze",
                freezeFooterCssClass: "GridViewScrollFooterFreeze",
                freezeHeaderRowCount: 1,
                freezeColumnCount: 8,

            });






        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

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
            
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                    CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2"  ID="divAmount" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblAmount0" runat="server" Text="Amount" CssClass=" lblTxt lblName"></asp:Label>
                                <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" >
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                    <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                    <asp:ListItem Value="between">Between</asp:ListItem>
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-2 col-lg-1" style="margin-top: 20px;"  ID="divAmountC1" runat="server" visible="false">
                            <div class="form-group">
                                <asp:TextBox ID="txtAmountC1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                               
                            </div>
                        </div>
                         <div class="col-md-1 col-sm-2 col-lg-1" ID="divAmountC2" runat="server" visible="false">
                            <div class="form-group">
                                 <asp:Label ID="lblToCash" runat="server" CssClass="lblTxt lblName" Text="To" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtAmountC2" runat="server" Visible="false" CssClass="form-control form-control-sm" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" ID="divlanguage" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lbllanguage" runat="server" CssClass="lblTxt lblName" Text="Languages"></asp:Label>
                                <asp:DropDownList ID="ddllang" Visible="false" CssClass="form-control form-control-sm chzn-select" TabIndex="13" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddllang_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">EN</asp:ListItem>
                                    <asp:ListItem>BN</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>                        
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm"  OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" CssClass="form-control form-control-sm chzn-select" TabIndex="13" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


             <div class="card card-fluid">
                <div class="card-body mb-2" style="min-height: 400px;">
                    <div class="row table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewallProDues" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        CssClass=" table-striped table-bordered grvContentarea"
                                        OnPageIndexChanging="dgvAccRec03_PageIndexChanging"
                                        Width="654px" OnRowDataBound="dgvAccRec03_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Project Name"></asp:Label>

                                                                <asp:HyperLink ID="hlbtntbCdataExel1" CssClass="btn btn-success btn-xs " runat="server"><i class="fas fa-file-excel"></i></asp:HyperLink>

                                                                <%--  <asp:HyperLink ID="hlbtntbCdataExel1" runat="server" 
                                                              CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="Solid" Text="Export to Excel"></asp:HyperLink>--%>
                                                          
                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                                        ForeColor="Black" Target="_blank" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                        Width="180px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <Label runat="server" Font-Bold="True" Font-Size="13px"></Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value Of Stock">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtstkamal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtstkamal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tstkam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Sales Value">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtocostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtocsotal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Received">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoreceivedal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtotreceivedal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Receivable">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFatoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtatoduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtcurdues" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtcurdues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Over Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtpredues" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtpredues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Dues & Over Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Delay Charge">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdelchargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                       Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>' Style="text-align: right"
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle />
                                                <FooterStyle  HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Cheque">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdischargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                      Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdischargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnettoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnettoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="85px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle CssClass="grvFooterNew" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <RowStyle CssClass="grvRowsNew" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeaderNew" />

                                    </asp:GridView>
                                </div>

                                <asp:Panel ID="pnlIndPro" runat="server" Visible="False">
                                    <div class="row mt-1">
                                        <div class="col-md-12">
                                            <asp:Label runat="server" CssClass="GrpHeader" Font-Bold="True" Font-Size="12px"
                                                Width="300px">Note</asp:Label>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="col-md-12">
                                            <asp:GridView CssClass=" table-striped table-bordered grvContentarea" ID="gvinpro" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="200px">
                                                <PagerSettings Position="Top" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoid0" runat="server"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle  HorizontalAlign="center"/>
                                                        <HeaderStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dues Upto </Br> Dec 2014">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cureent Due </Br> Dec 2014">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcurduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvtoduesnote" runat="server" BackColor="Transparent"
                                                                BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                                                TabIndex="76"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>




                                                </Columns>
                                                <FooterStyle CssClass="grvFooterNew" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeaderNew" />

                                            </asp:GridView>
                                        </div>


                                    </div>
                                </asp:Panel>


                            </asp:View>

                            <asp:View ID="ViewmonthlycollSchedule" runat="server">

                                <asp:GridView ID="gvmoncollsch" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmoncollsch_RowDataBound" CssClass=" table-striped table-bordered grvContentarea" AllowPaging="true"
                                    PageSize="10" OnPageIndexChanging="gvmoncollsch_PageIndexChanging" 
                                    ShowFooter="True">
                                    <RowStyle />
                                    <PagerSettings Mode="NumericFirstLast"/>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlmoncoll" runat="server" Font-Bold="True" Height="16px" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate1")) 
                                                                         %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer Name">

                                            <HeaderTemplate>

                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                    Text="Customer Name"></asp:Label>


                                                <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                    CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvcustname" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="13px"
                                                    Style="text-align: right" Text="Total"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--      <asp:TemplateField HeaderText=" Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcustname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) 
                                                                         %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) 
                                                                         %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvschdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'  width="120px" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" width="120px"/>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="P1">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P21">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp21" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P22">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp22" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P23">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp23" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P24">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp24" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P25">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp25" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P26">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp26" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P27">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp27" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P28">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp28" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P29">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp29" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P30">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp30" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P31">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp31" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P32">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp32" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P33">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp33" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P34">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp34" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P35">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp35" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P36">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp36" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P37">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp37" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P38">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp38" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P39">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp39" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P40">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp40" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P41">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp41" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p41")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P42">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp42" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p42")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P43">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp43" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p43")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P44">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp44" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p44")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P45">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp45" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p45")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P46">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp47" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p47")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P48">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp48" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p48")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P49">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp49" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p49")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="P50">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvp50" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p50")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewMonthyCollection" runat="server">
                                <asp:GridView ID="gvmoncoll" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvmoncoll_RowDataBound" CssClass=" table-striped table-bordered grvContentarea" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvmoncoll_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"/>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText=" Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpronamemcoll" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) 
                                                                         %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcustnamemcoll" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) 
                                                                         %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvudescmcoll" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) 
                                                                         %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Mr #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) 
                                                                         %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) 
                                                                         %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheqeu No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchequeno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) 
                                                                         %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="R1">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R2">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R3">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R4">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R5">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R6">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R7">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R8">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R9">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R10">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R11">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R12">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R13">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="R14">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r14")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="R15">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="R16">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="R17">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="R18">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="R19">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="R20">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvr20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalmcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocoll")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="85px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewMonCollSchSummary" runat="server">
                                <asp:GridView ID="gvmoncollschsum" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmoncollschsum_RowDataBound" CssClass=" table-striped table-bordered grvContentarea" AllowPaging="True" PageSize="15"
                                    OnPageIndexChanging="gvmoncollschsum_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlmoncollsum" runat="server" Font-Bold="True" Height="16px" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" Width="80px" Text="Total" Font-Bold="true" Font-Size="13px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactdesccschsum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) 
                                                                         %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalAmount" runat="server" Width="90px" Font-Bold="true" Font-Size="12px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcollschsumamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />
                                </asp:GridView>
                                <asp:GridView ID="gvmoncollschsumBN" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmoncollschsumBN_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlmoncollsum" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="প্রকল্পের নাম">

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" Width="80px" Text="Total" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvpactdesccschsum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdescbn")) 
                                                                         %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="টাকা">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalAmountbn" runat="server" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcollschsumamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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

                            <asp:View ID="ViewMonCollSchDet" runat="server">
                                <div class="row">

                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-group">
                                            <div class="col-md-6 padding5px">
                                                <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Catagory"></asp:Label>

                                                <asp:DropDownList ID="ddlCatatory" runat="server" CssClass="chzn-select form-control form-control-sm" Width="160px" TabIndex="6">
                                                </asp:DropDownList>


                                            </div>
                                    </fieldset>
                                    <asp:GridView ID="gvmoncolschandac" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True"
                                        CssClass=" table-striped table-bordered grvContentarea"
                                        OnRowDataBound="gvmoncolschandac_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNocdueaac" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid1"))%>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>                                                    
                                                        <asp:Label ID="lblheader" runat="server" Text="Project Name"></asp:Label>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvproname" runat="server" Font-Size="11PX" Font-Underline="false" Font-Bold="true"
                                                        ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcustname" runat="server" Font-Size="11PX" Font-Underline="false"
                                                        ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvunitname" runat="server" Font-Size="11PX" Font-Underline="false"
                                                        ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Sales Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsalesdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salesdate"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Size">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sales Value">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvapamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Car Parking">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcarparking" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Utility & Association">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvutilityaoth" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utlaassoamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Additional & Deduction">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmodifiaction" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modiamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Others">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothers" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Total Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotalschandac" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Received Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrcvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Balance">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbalamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Current Dues">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtcurduesschandac" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Dues Date">

                                                <ItemTemplate>

                                                    <asp:Label ID="lgvcurduesdate" runat="server"
                                                        Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "curduesdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "curduesdate")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="65px"></asp:Label>
                                                    <%--<asp:Label ID="lgvcurduesdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curduesdate"))%>'
                                                        Width="65px"></asp:Label>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                        </Columns>

                                        <FooterStyle CssClass="grvFooterNew" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeaderNew" />

                                    </asp:GridView>

                                </div>



                            </asp:View>

                            <asp:View ID="ViewMonduesover" runat="server">



                                <asp:GridView ID="gvDuesOverdues" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvDuesOverdues_PageIndexChanging" AllowPaging="true" PageSize="10"
                                    ShowFooter="True"
                                    CssClass="table-striped table-bordered grvContentarea">
                                    <RowStyle />
                                    <PagerSettings Mode="NumericFirstLast"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSloverdues" runat="server" Font-Bold="True" Height="16px" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">

                                            <HeaderTemplate>
                                                <asp:Label ID="lblheader" runat="server" Text="Project Name"></asp:Label>
                                                <asp:HyperLink ID="hlbtntbCdataduesExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvproname" runat="server" Font-Size="11PX" Font-Underline="false" Font-Bold="true"
                                                    ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustname" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Unit">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFunitname" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right">Total :</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunitname" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInstallment" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                    Width="310px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mr No #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrNo" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="current Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcduesamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvcDuesamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Over Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDuesamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvDuesamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "overdues")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFReceived" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReceived" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBalamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBalamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Current Dues Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurduesdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Collection Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvColldate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recdate"))%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>







                                    </Columns>

                                    <FooterStyle CssClass="grvFooterNew" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeaderNew" />

                                </asp:GridView>





                            </asp:View>

                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

