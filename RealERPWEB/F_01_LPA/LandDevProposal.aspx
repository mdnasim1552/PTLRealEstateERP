<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandDevProposal.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LandDevProposal" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server"
                                            Visible="False" CssClass="form-control inputTxt"></asp:Label>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="PanelSelName" runat="server" Visible="False">
                            <asp:Panel ID="PnlCopyProject" runat="server" Visible="False">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="From Project"></asp:Label>
                                            <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ibtnCopyFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnCopyFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-5 pading5px ">
                                            <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                            </asp:DropDownList>


                                        </div>

                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnCopyData" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnCopyData_Click" TabIndex="4">Ok</asp:LinkButton>

                                        </div>


                                    </div>



                                </div>



                            </asp:Panel>
                            <div class="form-group">
                                <div class="col-sm-1">
                                    <asp:CheckBox ID="chkAllRes" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllSInf_CheckedChanged" CssClass="btn btn-primary primaryBtn margin5px chkBoxControl"
                                        Text="Show All" />
                                </div>
                                <div class="col-sm-5 pading5px">
                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#0B88C5" ForeColor="White" AutoPostBack="True" CssClass="btn rbtnList1 margin5px  primaryBtn " OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                        RepeatColumns="6" RepeatDirection="Horizontal">
                                        <asp:ListItem>Project Information</asp:ListItem>
                                        <asp:ListItem>Sales Revenue</asp:ListItem>
                                        <asp:ListItem>Cost</asp:ListItem>
                                       
                                      <%--  <asp:ListItem>Reports</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                                 <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to" Text="Creation Date:"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>
                                <div class="col-sm-3 pading5px">
                                    <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy" 
                                        Visible="False" />
                                    <asp:Label ID="lblFeaProLock" runat="server" Visible="False"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                   

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewProjectInfo" runat="server">
                                <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="772px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkProjectLock" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Text="Project Lock" Width="90px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>' Width="4px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatProInfo" runat="server" CssClass="btn btn-danger primaryBtn"
                                                    ForeColor="Black" OnClick="lUpdatProInfo_Click">Update Information</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="20px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                                    Width="510px"></asp:TextBox>
                                            </ItemTemplate>
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
                            <asp:View ID="ViewSalesRevenue" runat="server">
                                <asp:Label ID="lblSaleableAreaa" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="Black" Text="Saleable Area (Total):" Width="206px"></asp:Label>
                                <asp:GridView ID="gvFeaPrjFCS" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="821px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodfcs" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalCostFCS" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" OnClick="lbtnTotalCostFCS_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnfUpdateCostFCS" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCostFCS_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvItemdescsalar" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                    Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnitnum3" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlandsizes" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sft.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvconktosfts" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sft. Per Floor">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsftperfs" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MGC">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpercntges" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sft./ Floor">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlsizess" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No of Floor">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvstonums" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Sft.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtssfts" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtssfts" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brochure Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBQTY" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvBDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc"))%>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                <hr />
                                <asp:Label ID="lblSaleableAreaDistributot" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="Black" Text="Saleable Area Distribution:" Width="206px"></asp:Label>

                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lblLownertext" runat="server" CssClass=" smLbl_to" Text="Land Owner Share:"></asp:Label>
                                        <asp:Label ID="lblLownerval" runat="server" CssClass="inputTxt inpPixedWidth"></asp:Label>
                                        <asp:Label ID="lblCompanytext" runat="server" CssClass="smLbl_to" Text="Company Share:"></asp:Label>
                                        <asp:Label ID="lblCompanyval" runat="server" CssClass="inputTxt inpPixedWidth"></asp:Label>
                                        <asp:Label ID="lblMsg0" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                        <div class=" clearfix"></div>
                                        </div>
                                </asp:Panel>

                                <asp:GridView ID="gvlpsaldis" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="651px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodsd" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalsd" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" OnClick="lbtnTotalsd_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnFUpdateSalessd" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFUpdateSalessd_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvItemdescsd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                    Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnitnumsd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSizesd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalshsd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Owner">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlownersd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlownershsd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcompanysd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "company")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcompanyshsd" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase From Land Owner">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpurfrmlanownersd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pflowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpfrmlownershsd" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjustment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvadjsd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjmnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFadjmntsd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalcompanysd" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalcom")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtcompanyshsd" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
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
                                </asp:GridView>
                                <hr />
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                    Text="Sales Revenue:" Width="206px"></asp:Label>
                                <asp:Label ID="lblMsg1" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvFeaPrj" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvFeaPrj_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="651px">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnFUpdateSales" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFUpdateSales_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvItemdesc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgUnitnum" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvnum" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrno")) %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total SFT / Quantity">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTsize" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgtsize" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Department Rate/SFT">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSratepsft01" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsalrate01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate01")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Department Rate/SFT">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSratepsft02" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsalrate02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate02")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Management Rate/SFT">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFSratepsft03" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsalrate03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate03")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Department Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt01" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt01" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Department Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt02" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Management Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt03" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt03" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
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
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewCost" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvFeaPrjFC" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="700px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCodfc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalCostFC" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" OnClick="lbtnTotalCostFC_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnfUpdateCostFC" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCostFC_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvItemdescc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                        Width="200px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgUnitnum2" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlandsize" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sft.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvconktosft" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sft Per Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsftperf" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FAR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvfar" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "far")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MGC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpercntge" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sft./Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlsizes" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Floor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvstonum" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Sft.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtcsft" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtcsft" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total (%)">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercent" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0.00;(#,##00.00); ")+" %" %>'
                                                        Width="60px"></asp:Label>
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
                                    </asp:GridView>
                                    <asp:Label ID="lblMsg2" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                </div>
                                <hr class="hrline" />
                                <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvFeaPrjC_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="792px" OnRowDataBound="gvFeaPrjC_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnPSameVal" runat="server" Font-Bold="True" Style="text-align: left"
                                                                Font-Size="12px" ForeColor="Black" Width="100px" OnClick="btnPSameVal_Click">Put Same Value</asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnfUpdateCost" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCost_Click">Final Update</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvItemdesc0" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnitnum0" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total SFT / Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTSizec" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgtsizec" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Department Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalrate01" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratec01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate01")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="sales Department Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalrate02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratec02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate02")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Management Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalrate03" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratec03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate03")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Department Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvAmtc01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtc01" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Department Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvAmtc02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtc02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Management Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvAmtc03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtc03" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Per SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSFT" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvFSFT" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "persft")).ToString("#,##0;(#,##0); ") %>'
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
                                </asp:GridView>

                            </asp:View>

                            <asp:View ID="ViewLandOwener" runat="server">
                                <hr />
                                <asp:GridView ID="gvFeaLOwner" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowDeleting="gvFeaLOwner_RowDeleting"
                                    ShowFooter="True" Width="334px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodl" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalLOwner" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" OnClick="lbtnTotalLOwner_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnfUpdateLOwner" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateLOwner_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvItemdesc1" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgUnitnum1" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSizel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvnuml" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total SFT / Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTsizel" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgtsizel" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Owner Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalratel01" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratel01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate01")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Department Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalratel02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratel02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate02")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Management Rate/SFT">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsalratel03" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgsalratel03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate03")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Owner Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmtl01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtl01" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Department Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmtl02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtl02" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Management Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmtl03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtl03" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
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
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ViewReport" runat="server">
                                <hr />
                                <div class=" table-responsive">
                                    <asp:GridView ID="gvFeaPrjRep" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True"
                                        Width="785px" OnRowDataBound="gvFeaPrjRep_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                                        Width="220px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvItemdescRep" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgUnitnumRep" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgSizerep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgnumrep" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total SFT / Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgtsizecRep" runat="server" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Owner Rate/SFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgsalraterep01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate01")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Department Rate/SFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgsalraterep02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate02")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Management Rate/SFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgsalraterep03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate03")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Owner Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmtrep01" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmtc001" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Department Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmtrep02" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmtc002" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Management Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmtrep03" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmtc003" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
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
                                    </asp:GridView>
                                </div>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
