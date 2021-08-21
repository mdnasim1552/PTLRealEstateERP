<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkAccFinalReports.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkAccFinalReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>

        .name {
        
        color:maroon
        
        }

    </style>

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
                                    <div class="col-md-4 pading5px asitCol4" >
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Visible="false"  Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" Visible="false" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy" 
                                            TargetControlID="txtDatefrom" Enabled="true"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblrptlbl" runat="server" CssClass="smLbl_to" Text="Report Level" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="DDListLevels" runat="server" CssClass="smDropDown inputTxt" Visible="false"
                                            TabIndex="6">
                                            <asp:ListItem Value="1">Level-1</asp:ListItem>
                                            <asp:ListItem Value="2">Level-2</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="3">Level-3</asp:ListItem>
                                            <asp:ListItem  Value="4">Level-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Visible="false">Ok</asp:LinkButton>
                                        <asp:CheckBox ID="ChkTopHead" runat="server" Text="Print top heads" CssClass="btn btn-primary checkBox" Visible="false" />
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                                <ProgressTemplate>
                                                    <asp:Label ID="lblProgressbar" runat="server" CssClass="lblProgressBar" Text="Please Wait.........."></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblOpeningDate" runat="server" CssClass="lblTxt lblName" Text="Opening Date"
                                            Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"
                                            Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtOpeningDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ISView" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="btn btn-success primaryBtn"
                                    Visible="False"></asp:Label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="row">
                                 <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">
                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">From</asp:Label>

                                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                                <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">To</asp:Label>
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                              

                                                 <asp:LinkButton ID="lnkbtnISOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnISOk_OnClick" TabIndex="4">Ok</asp:LinkButton>


                                            </div>
                                        

                                        </div>

                                    </div>
                                </fieldset>
                           </div>


                            <asp:GridView ID="dgvIS" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgvIS_RowDataBound" Width="647px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode6" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetails" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvISDesc" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Current Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Previous Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentcu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>


                        </asp:View>
                        <asp:View ID="BSView" runat="server">


                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="btn btn-success primaryBtn"
                                    Visible="False">Balance Sheet</asp:Label>
                                <div class="clearfix"></div>
                            </div>
                         <asp:GridView ID="dgvBS" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgvBS_RowDataBound" Width="640px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label3122" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetailsbs" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvBSDesc" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                        </asp:View>
                        <asp:View ID="PSView" runat="server">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblProjectname" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProj" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProj_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlAccProject" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>



                                    <div class="clearfix"></div>

                                </div>
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server" Text="Resource Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcRes" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindRes_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlResHead" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="clearfix"></div>

                                </div>
                            </div>
                            <asp:GridView ID="dgvPS" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="1010px">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total. &lt;br&gt; Net." FooterStyle-Font-Bold="true"
                                        FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="14px"
                                        HeaderText="         Resource  Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc4").ToString() %>'
                                                Width="320px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                        HeaderText="Op.Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvopqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                        HeaderText="Op.Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfopamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3121" runat="server" CssClass="GridLebel">-</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpnamt1" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                        HeaderText="Cu.Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCuq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cu.Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfcuamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1345" runat="server" CssClass="GridLebel">-</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCuam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Dr Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfclDrAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label112" runat="server" CssClass="GridLebel">-</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClrDrAmt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl. Cr Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfclCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblfclBalAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClCram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
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
                        <asp:View ID="BEView" runat="server">




                            <asp:Panel ID="Panel2" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="Label6" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtSearchp" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindProjI" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProjI_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlHAccProject" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>



                                        <div class="clearfix"></div>

                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblRptGroup0" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                            <asp:DropDownList ID="ddlRptGroupbve" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Selected="True">Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem>Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                        <div class="clearfix"></div>

                                    </div>
                                </div>


                            </asp:Panel>

                            <asp:GridView ID="dgvBE" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="850px" OnRowDataBound="dgvBE_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode2" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode4").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                        HeaderStyle-Font-Size="12px" HeaderText="Description of Account">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescryptionbe" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="300px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnitbe" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgdqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftbgdqty" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bgd. Rate" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblfbgdam" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbgdam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Qty" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvToqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftToQty" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Rate" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Amt" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblftoamt" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClam0" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail. Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftavqty" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail.Rate" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvavrat" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Available.Amt" HeaderStyle-Font-Size="12px">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAamt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblftAvAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPer" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
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
                        <asp:View ID="SpcCode" runat="server">

                            <asp:Panel ID="Panel1" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="Label7" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                            <asp:TextBox ID="txtSearchpSpc" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindProjSpc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProjSpc_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>



                                        <div class="clearfix"></div>

                                    </div>

                                </div>


                            </asp:Panel>


                            <asp:GridView ID="dgvSPC" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                                Width="753px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcode5" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description Of Resourec">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specifition">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspcdesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfamount" runat="server" CssClass="GridLebel" Font-Bold="True"
                                                Font-Size="11px" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvamount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
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
                        <asp:View ID="InComeInd" runat="server">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label21" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProjind" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindProjind_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true" Width="350px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" CssClass="rbtnList1" RepeatColumns="6" RepeatDirection="Horizontal" Width="190px"  BackColor="#3863B7" ForeColor="White">
                                            <asp:ListItem>Details</asp:ListItem>
                                            <asp:ListItem>Summery</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                            </div>

                         
                                <%--<tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style87">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style98">
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" Style="border-style: solid; border-width: 1px"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style63">
                                        <asp:ImageButton ID="ImgbtnFindProjind" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnFindProjind_Click" Width="21px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" AutoPostBack="True" Font-Size="12px"
                                            Width="400px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectInd_ListSearchExtender" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlProjectInd">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td></td>
                                    <td class="style87"></td>
                                    <td class="style98"></td>
                                    <td class="style63"></td>
                                    <td>
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" BorderColor="#FFCC00"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="14px" Height="14px"
                                            RepeatColumns="6" RepeatDirection="Horizontal" Width="190px">
                                            <asp:ListItem>Details</asp:ListItem>
                                            <asp:ListItem>Summery</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>--%>
                               
                                        <asp:GridView ID="gvIncome" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvIncome_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="758px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                            Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    
                                        <asp:GridView ID="gvInfast" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="758px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                            Width="170px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFTfAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                            Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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


                        <asp:View ID="SPBalSheet" runat="server">

                              <div class="form-horizontal">
                                <div class="form-group">
                                   <div class="col-md-3">
                                        <asp:Label ID="Label22" runat="server" Text="Balance Sheet"   CssClass="lblName lblTxt" Visible="False"></asp:Label>
                                   </div>

                                    <div class="clearfix"></div>
                                </div>
                            </div>


                           
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="Balance Sheet" Visible="False"
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>--%>
                         
                                        <asp:GridView ID="dgvSpBS" runat="server" AutoGenerateColumns="False" BackColor="#FFECEC" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="3px" OnRowDataBound="dgvSpBS_RowDataBound"
                                            Width="640px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description Of accounts">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gendesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")).Trim(): "") 
                                                                          %>'>

                                                            Width="250px" CssClass="GridLebelL"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvclosamt" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                 
                        </asp:View>
                        <asp:View ID="ViewShEquity" runat="server">
                             <asp:GridView ID="gvsequ" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                Width="519px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description">
                                        <FooterTemplate>
                                            <asp:Label ID="lblftotalse" runat="server" Text="Total Equity" Font-Bold="True" Font-Size="12px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgcActDescse" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))
                                                                        
                                                                         
                                                                    %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopnamse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFopnamse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Increased">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcramtse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcramtse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Decreased">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdramtse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdramtse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclosamse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFclosamse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>
                        </asp:View>
                        </asp:View>


                        <asp:View ID="ViewLandStatus" runat="server">

                              <div class="form-horizontal">
                                  <asp:Panel ID="Panel4" runat="server">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtAccHead" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgBtnAccHead" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgBtnAccHead_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlAcHead" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true" Width="400px">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <div class=" col-md-6  pading5px asitCol6">
                                       <asp:Label ID="Label2" CssClass="lblTxt lblName" runat="server" Text="Group:"></asp:Label>
                                         <asp:DropDownList ID="ddlGrpAcc" runat="server"  CssClass="ddlPage">
                                                            <asp:ListItem>Main</asp:ListItem>
                                                            <asp:ListItem>Sub-1</asp:ListItem>
                                                            <asp:ListItem>Sub-2</asp:ListItem>
                                                            <asp:ListItem>Sub-3</asp:ListItem>
                                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                                        </asp:DropDownList>

                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                </asp:Panel>
                            </div>
                                            
                                                <%--<tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td class="style91">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style92">
                                                        <asp:TextBox ID="txtAccHead" runat="server" Style="border-style: solid; border-width: 1px"
                                                            Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td class="style93">
                                                        <asp:ImageButton ID="imgBtnAccHead" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                            OnClick="imgBtnAccHead_Click" Width="21px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAcHead" runat="server" AutoPostBack="True" Font-Size="12px"
                                                            Width="400px">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                                                            TargetControlID="ddlHAccProject">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td class="style91">
                                                        <asp:Label ID="Label2" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                                            Style="font-weight: 700; text-align: right" Text="Group :" Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style92">
                                                        <asp:DropDownList ID="ddlGrpAcc" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Height="21px" Style="text-transform: capitalize" Width="80px">
                                                            <asp:ListItem>Main</asp:ListItem>
                                                            <asp:ListItem>Sub-1</asp:ListItem>
                                                            <asp:ListItem>Sub-2</asp:ListItem>
                                                            <asp:ListItem>Sub-3</asp:ListItem>
                                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style93">&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>--%>
                                       
                                        <asp:GridView ID="gvlandSt" runat="server" AutoGenerateColumns="False" BackColor="#DDFFEE" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="850px" OnRowDataBound="gvlandSt_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcode2" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode4").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Font-Size="12px" HeaderText="Description of Account">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdescryptionbe" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                            Width="300px" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnitbe" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Qty" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbgdqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftbgdqty" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bgd. Rate" HeaderStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBgRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Amt" ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfbgdam" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbgdam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Qty" HeaderStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvToqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftToQty" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Rate" HeaderStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Amt" ItemStyle-HorizontalAlign="Right">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftoamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvClam0" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail. Qty" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvavqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftavqty" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail.Rate" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvavrat" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Available.Amt" HeaderStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAamt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftAvAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPer" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
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



                             <div class="form-horizontal">
                                  <asp:Panel ID="PanelNote" runat="server">
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                      <asp:Label ID="lblBankstatus" runat="server"  Text="Notes:" CssClass="lblName lblTxt"></asp:Label>

                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                               
                                
                                       
                                                   
                                                        <%--<asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" BorderColor="#000"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Notes:"
                                                            Width="120px"></asp:Label>--%>
                                          
                                                  
                                                        <asp:GridView ID="gvNote" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">



                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSlNcf" runat="server" Font-Bold="True" Style="text-align: right"
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "ndesc"))
                                                                        
                                                                         
                                                                    %>'
                                                                            Width="220px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Budgeted">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgbalambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Actual">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            Width="80px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                  </asp:Panel>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

