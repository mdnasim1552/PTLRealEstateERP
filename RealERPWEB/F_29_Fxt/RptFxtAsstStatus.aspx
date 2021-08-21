<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFxtAsstStatus.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFxtAsstStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="View1" runat="server">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#DAEAD3"
                                                RepeatColumns="6" RepeatDirection="Horizontal" Width="350px" ForeColor="Black" CssClass="rbtnList1" Style="margin: 0px 0px 5px 175px;">
                                                <asp:ListItem>With Details</asp:ListItem>
                                                <asp:ListItem>Without Details</asp:ListItem>
                                                <asp:ListItem>With Value</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label7" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="350" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date:"></asp:Label>

                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                            <asp:CheckBox ID="ChkBalance" runat="server" CssClass="checkBox" Text="Without Zero Balance" />

                                            <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to" Text="Group :"></asp:Label>

                                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="table table-responsive">

                                    <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Style="text-align: left" Width="810px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgpactdesc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReceiveDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvQty" runat="server" Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transfer Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTrnsDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnsdate")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transfer Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTqty" runat="server" Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRentPerday" runat="server" Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="80px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRate" runat="server" Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="80px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt" runat="server" Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="center" Width="80px" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFoterAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
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

                            <asp:View ID="Depreciation" runat="server">
                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8   pading5px  asitCol8">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="From:"></asp:Label>


                                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>

                                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8  pading5px  asitCol8">
                                            <asp:Label ID="txtDays" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>

                                    <asp:GridView ID="grDep" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                                        ShowFooter="True" Style="text-align: left" Width="810px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid1" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Particulars">
                                                <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label></td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: left" Text="Total :" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance as on ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOpening" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Addition During The year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAddition" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTAddition" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Disposal During the year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsalesdec" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFsalesdec" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Revaluation During The Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdisposal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDisposal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Balance as on">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate Of Dep. %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepPar" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depreciation as on">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepOpen" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDepOpen" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Additon During The year ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepCur" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDepCur" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Disposal during the year ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvadjment" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFadjment" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance as at">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepTotal" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todep")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDepTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="W.D Values as on ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCBal" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTCBal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </asp:Panel>
                            </asp:View>

                        </asp:MultiView>
                    </div>


                    <%--<fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel4" runat="server">

                            </asp:Panel>
                        </div>
                    </fieldset>--%>
                </div>
            </div>



            <%--<tr>
                                            <td class="style19">
                                                &nbsp;
                                            </td>
                                            <td class="style74">
                                                &nbsp;</td>
                                            <td class="style70">
                                                &nbsp;</td>
                                            <td class="style75">
                                                <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" BorderColor="#FFCC00"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="14px"
                                                    RepeatColumns="6" RepeatDirection="Horizontal" Width="350px" ForeColor="Black">
                                                    <asp:ListItem>With Details</asp:ListItem>
                                                    <asp:ListItem>Without Details</asp:ListItem>
                                                    <asp:ListItem>With Value</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="style19">
                                                &nbsp;
                                            </td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                        </tr>--%>
            <%--<tr>
                                            <td class="style19">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text="Project Name:" Width="90px" CssClass="style23" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="style74">
                                                <asp:TextBox ID="txtSrcProject" runat="server" BorderStyle="None" 
                                                    CssClass="txtboxformat" Font-Bold="True" Width="90px"></asp:TextBox>
                                            </td>
                                            <td class="style70">
                                                <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ibtnFindProject_Click" Style="width: 18px" />
                                            </td>
                                            <td class="style75">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Width="350px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server" QueryPattern="Contains"
                                                    TargetControlID="ddlProjectName"></cc1:ListSearchExtender>
                                            </td>
                                            <td class="style19">
                                                <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="#000"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                                    Style="color: #FFFFFF">Ok</asp:LinkButton>
                                            </td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                        </tr>--%>

            <%--<tr>
                                            <td class="style21">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text="Date:" Width="90px" CssClass="style23" Font-Size="12px" 
                                                    BorderStyle="None"></asp:Label>
                                            </td>
                                            <td class="style67">
                                                <asp:TextBox ID="txtdate" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                                    Width="90px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                            </td>
                                            <td class="style22">
                                                <asp:CheckBox ID="ChkBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: left" Text="Without Zero Balance" Width="140px" />
                                            </td>
                                            <td class="style28">
                                                <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                                    Style="font-weight: 700; text-align: right" Text="Group :" Width="55px"></asp:Label>
                                            </td>
                                            <td class="style29">
                                                <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="21px" Style="text-transform: capitalize" Width="80px">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>--%>


            <%--<tr>
                                            <td class="style63">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style64">
                                                <asp:TextBox ID="txtFromdate" runat="server" CssClass="txtboxformat"
                                                    Font-Bold="True" Width="85px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>
                                            </td>
                                            <td class="style69">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Text="To:" Width="25px"></asp:Label>
                                            </td>
                                            <td class="style68">
                                                <asp:TextBox ID="txtTodate" runat="server" CssClass="txtboxformat"
                                                    Font-Bold="True" Width="85px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366"
                                                    BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    Font-Size="12px" OnClick="lbtnShow_Click"
                                                    Style="color: #FFFFFF; text-align: center; height: 17px;" Width="80px">Show</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
            <%--<tr>
                                            <td class="style63">&nbsp;</td>
                                            <td class="style64">&nbsp;</td>
                                            <td class="style69">&nbsp;</td>
                                            <td class="style68">
                                                <asp:Label ID="txtDays" runat="server" BackColor="#000" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
