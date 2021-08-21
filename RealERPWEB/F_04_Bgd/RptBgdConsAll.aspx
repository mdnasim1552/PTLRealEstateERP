<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBgdConsAll.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptBgdConsAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                   
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-1" style="margin-right:20px;">
                                            <asp:Label ID="lblProjectList" CssClass="lblTxt lblName " runat="server" Text="Project Name:"></asp:Label>
                                        </div>
                                        <div class=" col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblprject" CssClass="lblTxt lblName" style="width:150px;text-align:left;margin-left:25px;" runat="server"></asp:Label>
                                        </div>
                                       
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3" >
                                             <asp:Label ID="Label1" CssClass="lblTxt lblName " runat="server" Text="Date"></asp:Label>
                                             <asp:Label ID="lbldate" CssClass="lblTxt lblName" runat="server"></asp:Label>
                                        </div>
                                         <div class=" col-md-3 pading5px asitCol3" style="margin-left:-30px;">
                                           <asp:Label ID="lblreport" CssClass="lblTxt  lblName " runat="server" Text="Reports" style="margin-left:-20px;" ></asp:Label>
                                          <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage" width="90" OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChange" AutoPostBack="True">
                                            <asp:ListItem>Work Basis</asp:ListItem>
                                            <asp:ListItem>Resource Basis</asp:ListItem>
                                            <asp:ListItem>Work VS Resources</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                         <div class=" col-md-3  pading5px asitCol3" runat="server" visible="false" id="floorwise">
                                        <asp:LinkButton ID="lbtnFloorList" runat="server" CssClass="lblTxt lblName " OnClick="lbtnFloorList_Click">Floor</asp:LinkButton>

                                        <asp:DropDownList ID="ddlFloorList" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                        <div class="col-md-1">
                                             <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" style="margin-left:-26px;"
                                            CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                                        </div>
                                         
                                         <div class=" col-md-3  pading5px asitCol3" runat="server" id="paging" visible="false">

                                        <asp:Label ID="lblPagesp0" CssClass="lblTxt lblName" runat="server">Page Size</asp:Label>

                                        <asp:DropDownList ID="ddlpagesizewrkvres" runat="server" CssClass="ddlPage" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesizewrkvres_SelectedIndexChanged">
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
                        <asp:MultiView runat="server" ID="Multiview1"> 
                           <asp:View runat="server" ID="vwResBasis">
                               <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="847px" ShowFooter="True" OnRowDataBound="gvRptResBasis_RowDataBound" DataKeyNames="rptdesc1" ViewStateMode="Enabled" AllowSorting="true" >   <%--OnSorting="gvRptResBasis_Sorting"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catagory" SortExpression="flrdes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description" SortExpression="rptdesc1">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Construction Area:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Cost Per SFT:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblgvRptRes1" Target="_blank" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="300px"></asp:HyperLink>
                                                <asp:Label ID="hlnkresdes" runat="server" Font-Bold="False" Style="display: none;" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" SortExpression="rptunit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" SortExpression="rptqty">
                                            <FooterTemplate>
                                                <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd. Rate" SortExpression="rptrat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" SortExpression="rptamt">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                           </asp:View> 
                            
                            <asp:View runat="server" ID="vwWrkVsRes">
                                <asp:GridView ID="gvWrkVsRes" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvWrkVsRes_PageIndexChanging" ShowFooter="True" Width="640px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvFloor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description" FooterText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvItemDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))     %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvItemUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvItemQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvResDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))     %>'
                                                    Width="230px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvresqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvresamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


