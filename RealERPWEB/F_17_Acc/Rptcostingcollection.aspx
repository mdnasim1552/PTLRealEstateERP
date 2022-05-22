<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Rptcostingcollection.aspx.cs" Inherits="RealERPWEB.F_17_Acc.Rptcostingcollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
       <%-- function pageLoaded() {

            var gv = $('#<%=this.gvIncomeMon.ClientID %>');

            gv.Scrollable();



        }--%>


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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblFdate" runat="server" CssClass="lblName lblTxt"> Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server"
                                            Enabled="True" Format="dd-MM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                          <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn" TabIndex="5">Ok</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <%--  a.comcod, a.actcode, a.lcost, a.comcost, a.dcost, a.bgdcons, a.conscost, a.remcons, a.saleamt, a.collamt, a.remcollamt, actdesc=b.acttdesc --%>

                    <div class="row">
                        <asp:GridView ID="gvRptcodtColle" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="616px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCode" runat="server" __designer:wfdid="w38" CssClass="GridLebelL"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))  %>'
                                            Width="90px" Style="font-size: 11px; color: Black;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Projects Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvactdesc" runat="server" 
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="150px" Style="font-size: 11px; color: Black;"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lglbffgvlCost" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Land Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lglbfgvlCost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Commercial Conversion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComCon" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblfgvComCon" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Design Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesiCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvDesiCost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Const. Budgeted">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlConsCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcons")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfConsCost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Const. Actual">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlCostDone" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conscost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfCostDone" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Remaining Construction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlRemCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remcons")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfRemCost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Collection">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lglblgvlCost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Collection Done">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlCollDone" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfCollDone" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remaining Collection">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlremainColl" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remcollamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfremainColl" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
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
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
