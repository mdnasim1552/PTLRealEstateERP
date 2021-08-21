
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalSummaryDetails.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptSalSummaryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>


                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="yyyyMM" TargetControlID="txtfromdate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="yyyyMM" TargetControlID="txttodate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName ">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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



                                    <div class="col-md-3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                        <asp:GridView ID="gvSalSumDet" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvSalSumDet_PageIndexChanging" ShowFooter="True"
                            Width="500px" AllowPaging="True">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name" FooterText="Total:">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Basic">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBasic" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbSal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="House Rent">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvhrent" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hrent")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFhrent" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Convence">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCon" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cven")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCon" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Medical">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMedical" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mallow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmallow" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arrear">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvarsal" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arsal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFarier" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvoth" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oth")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFoth" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allowance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtallow" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tallow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtallow" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvGsal" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFgssal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Adjust">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdedday" runat="server" BackColor="Transparent"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary Payble">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgspay" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFgspay" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Provident Fund">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpdent" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpfund" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Income Tax">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvitax" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFitax" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAdvance" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adv")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFadv" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Abs ded.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvabsded" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absded")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFabsded" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other ded.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvothded" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othded")).ToString("#,##0;(#,##0); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFothded" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Deduction">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtded" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdeduc")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtded" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetsal" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetSal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
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





                    <table style="width: 100%;">
                        <%--<tr>
                    <td colspan="10">
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style16">&nbsp;
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="Company:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnCompany_Click" Width="16px" />
                                    </td>
                                    <td colspan="7" align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Height="16px" OnClick="lnkbtnShow_Click" Style="text-align: center;" Width="50px">Ok</asp:LinkButton>
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
                                </tr>


                                <tr>
                                    <td class="style16">&nbsp;
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style44">
                                        <asp:TextBox ID="txtfromdate" runat="server" Width="100px" CssClass="txtboxformat"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txttodate" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style38" align="left">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="color: #FFFFFF; text-align: right;" Text="Page Size:"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style41">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Style="margin-left: 0px" Width="100px">
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
                                    </td>
                                    <td class="style41">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000"></asp:Label>
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
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                        <tr>
                            <td colspan="10"></td>
                        </tr>

                    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
