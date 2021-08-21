<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TASSurvRate.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TASSurvRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gvpf = $('#<%=this.gvSubRate.ClientID %>');


            //gvpf.gridviewScroll({
            //    width: 1160,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 8,
            //    varrowtopimg: "../../Image/arrowvt.png",
            //    varrowbottomimg: "../../Image/arrowvb.png",
            //    harrowleftimg: "../../Image/arrowhl.png",
            //    harrowrightimg: "../../Image/arrowhr.png",
            //    freezesize: 5
            //});

            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });

        }

      

            

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">

                            <div class="form-group">

                                <div class="col-md-6 pading5px">
                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name:"></asp:Label>

                                    <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                    <asp:LinkButton ID="ImgbtnFindImpNo" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindImpNo_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                    <asp:DropDownList ID="ddlProjectName" runat="server" Style="width: 300px" CssClass="chzn-select  form-control inputTxt" TabIndex="3"></asp:DropDownList>
                                     <div>
                                    <asp:Label ID="lblProjectName" runat="server" BackColor="White" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Blue" CssClass="form-control inputTxt" Visible="False" Width="300px"></asp:Label>

                                </div>

                                </div>
                               
                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" TabIndex="4" Style="margin-left: -86px;">Ok</asp:LinkButton>
                                    <%--<asp:CheckBox ID="chkShorting" runat="server" AutoPostBack="true" Text="Alphabet" />--%>
                                    <%-- OnCheckedChanged="chkShorting_CheckedChanged"--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                    Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Visible="False" Width="80px">
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

                </div>
                <div class="row">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="table-responsive">
                            <asp:GridView ID="gvSubRate" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                 OnPageIndexChanging="gvSubRate_PageIndexChanging" ShowFooter="True"
                                Style="text-align: left" Width="650px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItemDes" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFlrDes" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="B0Q.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lnkbtnTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BoQ.Rate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdate" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lnkbtnUpdate_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtgvSRate" runat="server" BorderStyle="None" 
                                                                style="text-align: right; background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="75px"></asp:TextBox>--%>
                                            <asp:Label ID="lblgvSRate" Width="75px" Style="text-align: right; font-size: 12px;"
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="75px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BoQ.Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlblBoqAmt" runat="server" Text='' Style="text-align: right; font-size: 12px;"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%--     <asp:TextBox ID="txtgvSAmt" runat="server" BorderStyle="None" 
                                                                style="text-align: right; background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="90px"></asp:TextBox>--%>
                                            <asp:Label Style="text-align: right; font-size: 12px;" Width="90px" ID="lblgvSAmt"
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Qty.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSurvQty" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sarqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Rate.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSurvRete" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sarrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlblSurveyAmt" runat="server" Style="text-align: right; font-size: 12px;"
                                                Text=''></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSurvAmt" Font-Size="12px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="100px" Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Differ. Rate.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdiffrat" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Benifit Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlbbillamtAmt" runat="server" Style="text-align: right; font-size: 12px;"
                                                Text=''></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillamtAmt" Font-Size="12px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="100px" Font-Size="11px" />
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
                        <asp:View ID="View2" runat="server">

                            <asp:CheckBox ID="chkUllock" runat="server" AutoPostBack="True" Font-Bold="True"
                                Font-Size="12px" ForeColor="White" OnCheckedChanged="chkUllock_CheckedChanged"
                                Text="Unlock" Visible="False" />

                            <asp:GridView ID="gvConLevel" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                 OnPageIndexChanging="gvConLevel_PageIndexChanging" ShowFooter="True"
                                Style="text-align: left" Width="644px"  CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFlrDes0" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItemDes0" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bgd.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdatelevel" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lnkbtnUpdatelevel_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlQty" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllfrm_CheckedChanged"
                                                Text="ALL " />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkItem" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "markitem"))=="True" %>'
                                                Width="30px" />
                                        </ItemTemplate>
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



               <%-- <asp:Panel ID="Panel1" runat="server" Width="940px">

                    <asp:Label ID="lbldate1" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                        Style="text-align: right; font-weight: 700; color: #FFFFFF;" Text="Project Name:"
                        Width="80px"></asp:Label>

                    <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Height="18px" Width="80px"></asp:TextBox>


                    <asp:Label ID="lgvPage" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#993300"
                        Style="text-align: right; color: #FFFFFF;" Text="Page Size" Visible="False" Width="80px"></asp:Label>

                    <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                        ForeColor="White"></asp:Label>

                </asp:Panel>--%>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
