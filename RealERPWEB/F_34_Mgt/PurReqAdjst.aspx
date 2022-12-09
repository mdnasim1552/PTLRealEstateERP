 <%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurReqAdjst.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.PurReqAdjst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

      <%--  function pageLoaded() {

            var gv1 = $('#<%=this.gvReqStatus.ClientID %>');
            //gv1.Scrollable();
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });




        };--%>
        function pageLoaded() {
          
           
            $('.chzn-select').chosen({ search_contains: true });
            var gv1 = $('#<%=this.gvReqStatus.ClientID %>');
            gv1.Scrollable();
        };

      

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:MultiView ID="MultiView1" runat="server">


                        <asp:View ID="ViewReqAd" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class=" col-md-3  pading5px asitCol3">

                                                <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblProjectdesc" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                            </div>


                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                            </div>
                                            <div class="clearfix"></div>

                                        </div>
                                        <div class="form-group">
                                            <div class=" col-md-5  pading5px asitCol5">

                                                <asp:Label ID="Label2" CssClass="lblTxt lblName" runat="server" Text="Adjustment No"></asp:Label>
                                                <asp:Label ID="lbladjstmentno" CssClass="smLbl_to" runat="server"></asp:Label>
                                                <asp:Label ID="Label4" CssClass=" lblTxt lblName" runat="server">Date</asp:Label>

                                                <asp:TextBox ID="txtDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>



                                            </div>

                                            <div class="clearfix"></div>

                                        </div>
                                        <div class="form-group">
                                            <div class=" col-md-3  pading5px asitCol3">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page" Visible="False"></asp:Label>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                    CssClass=" ddlPage"
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
                                            <div class=" col-md-5  pading5px asitCol5">


                                                <asp:TextBox ID="txtSrcmrfno" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                   <asp:LinkButton ID="imgbtnFindmrfno" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindmrfno_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                             


                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="row">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvReqStatus" runat="server"  CssClass=" table-striped table-hover table-bordered grvContentarea" 
                                        AutoGenerateColumns="False" Width="901px" Style="margin-right: 0px" ShowFooter="True">                                       
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrfNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description of Materials" Width="150px"></asp:Label>
                                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel"></i>
                                                    </asp:HyperLink>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvComqty" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalqty" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Adjustment">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvadjqty" runat="server" Font-Size="11px"
                                                        
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjstqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="65px" BackColor="White"
                                                        BorderStyle="None" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="65px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpfDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
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

                            

                        </asp:View>
                        <asp:View ID="ViewProfitmar" runat="server">
                            <div class="row">
                                <asp:Panel ID="Promer" runat="server">

                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class=" form-group">
                                                <div class=" col-md-3  pading5px asitCol3">
                                                    <asp:Label ID="lblfrmDate" CssClass="lblTxt lblName" runat="server" Text="As on Date"></asp:Label>
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                                    <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnShow_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    
                                </asp:Panel>
                            </div>
                            <asp:GridView ID="gvProMar" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" Style="margin-right: 0px" Width="396px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvProMar_RowDataBound">
                                            <PagerSettings Position="Top" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name <br/> A">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateProMar" runat="server" CssClass="btn btn-danger primaryBtn"  OnClick="lbtnUpdateProMar_Click">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDesc" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Advanced Sales <br/> B">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAdvSal" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advsamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFAdvSal" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Cost(Actual) <br/> C">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTCost" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actcost")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFTCost" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Budget NP% on Cost <br/> D">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPerCost" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Conservative NP % on Cost <br/> E">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvProfitmar" runat="server" CssClass="inputTxt form-control" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prmar")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Profit <br/> F=(% of C)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNetP" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpramt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFNetP" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales <br/> G=(C+F)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSales" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFSales" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Realised <br/> H">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSR" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sreamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFSR" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Unrealised <br/> I=(G-H)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSU" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sunreamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFSU" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance of advanced Sales <br/> J=(B-G) ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBalAmt" runat="server"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFBalAmt" runat="server" Font-Bold="True"
                                                            Font-Size="12px"  ForeColor="#000" ></asp:Label>
                                                    </FooterTemplate>
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


                            
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


