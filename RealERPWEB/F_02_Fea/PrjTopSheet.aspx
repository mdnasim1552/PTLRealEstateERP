<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PrjTopSheet.aspx.cs" Inherits="RealERPWEB.F_02_Fea.PrjTopSheet" %>

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
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row mb-3">



                        <div class="col-md-2 mt-2">
                            <asp:Label ID="lbldate" runat="server" CssClass="form-label" Text="From"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-2 mt-2">
                            
                            <asp:Label ID="lbltodate" runat="server" CssClass="form-label" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2 mt-2">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Category :"></asp:Label>
                            <asp:DropDownList ID="ddlcatag" runat="server" CssClass=" chzn-select form-control form-control-sm" AutoPostBack="true" ></asp:DropDownList>

                            <%--<asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Wrkschedule" runat="server" CssClass="btn btn-sm btn-success pad2px"> <span class="glyphicon glyphicon-pencil"></span> Add New Work</asp:HyperLink>--%>
                        </div>
                        <div class="col-md-2" style="margin-top:25px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_OnClick"  >ok</asp:LinkButton>

                        </div>




                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvComCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvComCost_OnRowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField FooterText="Total" HeaderText=" Category">
                                        <%--<HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                                Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCdataExel" runat="server" CssClass="btn btn-warning primaryBtn">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>--%>

                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkVoucherEdit" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                Width="200px">
                                            </asp:HyperLink>
                                            <%--<asp:Label ID="lgvActDescCost" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                    Width="200px"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtopamtcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tolamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P1">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l1")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l2")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P7">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l7")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P8">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l8")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P9">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l9")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P10">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l10")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P11">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l11")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P12">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l12")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P13">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l13")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P14">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l14")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P15">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l15")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P16">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l16")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P17">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l17")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P18">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l18")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P19">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l19")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P20">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l20")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P21">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc21" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l21")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC21" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P22">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvp22" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l22")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC22" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P23">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc23" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l23")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC23" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P24">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc24" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l24")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC24" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P25">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc25" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l25")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC25" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P26">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc26" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l26")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC26" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P27">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc27" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l27")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC27" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P28">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc28" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l28")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC28" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P29">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc29" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l29")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC29" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P30">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc30" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l30")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC30" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P31">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc31" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l31")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC31" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P32">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc32" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l32")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC32" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P33">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc33" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l33")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC33" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P34">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc34" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l34")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC34" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P35">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc35" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l35")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC35" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P36">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc36" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l36")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC36" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P37">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc37" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l37")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC37" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P38">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc38" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l38")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC38" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P39">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc39" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l39")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC39" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P40">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc40" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l40")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC40" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P41">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc41" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l41")).ToString("#,##0;(#,##0); ")%>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC41" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P42">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc42" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l42")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC42" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P43">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc43" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l43")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC43" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P44">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc44" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l44")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC44" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P45">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc45" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l45")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC45" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P46">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc46" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l46")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC46" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P47">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc47" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l47")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC47" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P48">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc48" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l48")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC48" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P49">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc49" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l49")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC49" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="l50">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc50" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "l50")).ToString("#,##0;(#,##0); ") %>'
                                                Width="55px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPC50" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="55px"></asp:Label>
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
                </div>










            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

