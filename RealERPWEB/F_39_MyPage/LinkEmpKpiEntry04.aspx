<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkEmpKpiEntry04.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkEmpKpiEntry04" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            font-weight: bold;
        }

        .auto-style2 {
            font-weight: bold;
        }

        .auto-style3 {
            text-align: left;
            width: 110px;
        }

        span.maincode ::first-line {
            color: red !important;
        }

        .last_bold {
            font-size: 14px !important;
            color: green !important;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function loadModal7() {
            $('#exampleModal7').modal('show');
        }
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gridview = $('#<%=this.gvEmpWlist.ClientID %>');
            gridview.Scrollable();
        };

    </script>
   
    <%--<button type="button" class="btn btn-primary">Save changes</button>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_B">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-7 pading5px">
                                <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>

                                <asp:Label ID="lbldate" runat="server" CssClass=" inputtextbox"></asp:Label>
                                

                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Employee List"></asp:Label>
                                <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox"></asp:TextBox>

                                <asp:LinkButton ID="imgSearchSalesTeam" runat="server" TabIndex="4" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchSalesTeam_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                <asp:DropDownList ID="ddlemplist" runat="server" CssClass=" form-control inputTxt" Width="37%"
                                    TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlemplist_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                            <div class="smLbl_to">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" CssClass="btn btn-primary  okBtn pull-right"  TabIndex="9" OnClick="lnkok_Click"></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px">
                            </div>






                            <div class="col-md-3 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled " Visible="false"></asp:Label>
                                </div>

                            </div>
                        </div>



                    </div>
                </fieldset>
            </div>
            <div class="row">

                <div class="col-md-8">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvEmpWlist" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="668px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvEmpWlist_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActcode" runat="server"
                                                ForeColor="Black" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                   </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Work List">

                                        <ItemTemplate>
                                            <asp:Label ID="lgvActivi" runat="server"
                                                Text='<%#  "<span class=last_bold>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) +"</span>"+ 
                                            
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         //"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                Width="500px">
                                            </asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Work List">

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFresh" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnFresh_Click">Fresh</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>

                                            <asp:HyperLink ID="hlnkgvwrkdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank"
                                       
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                
                                                 Width="400px">
                                    </asp:HyperLink>


                                            

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Marks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marks")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="70px"  Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFwmarks" runat="server"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right"  Font-Bold="true"/>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target of Work">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvwqty" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wqty")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="80px"  Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFwqty" runat="server"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Work">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFacqty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvacqty" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acqty")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="80px"  Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblppercent" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent"))==0.00)?Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent")).ToString("#,##0.00;(#,##0.00); "):Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppercent")).ToString("#,##0.00;(#,##0.00); ")+" %"%>'
                                                Width="50px"  Style="text-align: right"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Marks">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFacmarks" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvacmarks" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acmarks")).ToString("#,##0.00;-#,##0.00; ")%>'
                                                Width="80px"  Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-3 pull-right">

                    <div>

                        <asp:Label ID="lblEmpinf" runat="server" Width="100%" Font-Bold="true" Font-Size="14px" CssClass="text-right" Text=""></asp:Label>

                        <div class=" clearfix"></div>
                    </div>
                    <hr />
                    <div>

                        <table class="table-striped table-hover table-bordered table-responsive grvContentarea last_bold" style="width: 83%; float: right">
                            <tr>
                                <td class="auto-style3">Target :</td>

                                <td class="auto-style1">
                                    <asp:Label ID="lbltrvalue" runat="server" Text="100"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Achieved :</td>

                                <td class="text-right">
                                    <asp:Label ID="lblAch" runat="server" Text="43.50" CssClass="auto-style2"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Achieved in % :</td>

                                <td class="auto-style1">
                                    <asp:Label ID="lblPer" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Grade :</td>
                                <td class="auto-style1">
                                    <asp:Label ID="lblGrade" runat="server" Text=""></asp:Label></td>
                            </tr>

                        </table>
                        <div class=" clearfix"></div>
                    </div>

                    <div class="pull-right">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grvGpa" runat="server" AutoGenerateColumns="False"
                                ShowFooter="False" Width="150px" CssClass="table-striped table-hover table-bordered grvContentarea last_bold" >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgdesc" runat="server"
                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "mrange"))  %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mark Range">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgrade" runat="server"
                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "mdescrip"))  %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   
                                   
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <fieldset class="scheduler-border fieldset_A">

                        <div class="form-horizontal">
                            <div class="form-group">

                                <div class="col-md-12 formBtn ">
                                    <div class="pull-left">
                                        <asp:LinkButton ID="lblPerformance" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px; padding: 2px 5px" OnClick="lblPerformance_Click" ><span class="glyphicon glyphicon-tasks"></span> Performance</asp:LinkButton>
                                        <asp:LinkButton ID="lblGrp" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px; padding: 2px 5px" OnClick="lblGrp_Click" ><span class="glyphicon glyphicon-signal"></span> Graph</asp:LinkButton>
                                         <asp:LinkButton ID="lnkbtnmonsummary" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px; padding: 2px 5px" OnClick="lnkbtnmonsummary_Click" ><span class="glyphicon glyphicon-signal"></span> Summary</asp:LinkButton>
                                            
                                    
                                        



                                    </div>
                                    <div class="pull-right">

                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="~/DeafultMenu.aspx?Type=5004"><span class="flaticon-delete47 text-danger "></span>Close</asp:HyperLink>


                                    </div>
                                </div>




                            </div>



                        </div>
                    </fieldset>
                </div>
            </div>



            <div class="modal fade AsitModal" id="exampleModal7" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog row">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                <h3 class="modal-title">Employee's KPI Performance Graph</h3>


                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">

                                    <ContentTemplate>

                                        <div class="table-responsive">
                                            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="370px" BorderColor="Black" Visible="false">
                                                <asp:Chart ID="Chart1" runat="server" Height="364px" Width="1100px" BackColor="WhiteSmoke">
                                                    <Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue" IsValueShownAsLabel="true" BorderDashStyle="DashDot" LegendText="Target"
                                                            MarkerColor="black" MarkerStyle="Square" Name="Series1" MarkerSize="5">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="red" IsValueShownAsLabel="true" LegendText="Actual"
                                                            MarkerColor="yellow" MarkerStyle="Circle" Name="Series2" MarkerSize="5">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="green" IsValueShownAsLabel="true" LegendText="Average"
                                                            MarkerColor="red" MarkerStyle="Circle" Name="Series3" MarkerSize="5">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1">

                                                            <AxisX MaximumAutoSize="100" Interval="1" TitleFont="Sans Serif">
                                                            </AxisX>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                    <Titles>
                                                        <asp:Title Font="Cambria, 20px" ForeColor="Black" Name="Title1"
                                                            Text="Monthly KPI Target, Achievement Vs. Average">
                                                        </asp:Title>
                                                    </Titles>
                                                    <Legends>
                                                        <asp:Legend ForeColor="Black" Title=""></asp:Legend>
                                                        
                                                    </Legends>
                                                    
                                                </asp:Chart>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
                                <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

        </div>
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

