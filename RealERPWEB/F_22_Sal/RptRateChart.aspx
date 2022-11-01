<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptRateChart.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptRateChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-body">
            <div class="row">
                

                 
                       
                            <div class="col-md-3 asitCol2  pading5px">

                                <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName"
                                    Text="Date:"></asp:Label>

                                <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox" BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtDate"></cc1:CalendarExtender>

                            </div>

                       
                       
                            <div class="clol-md-3 asitCol3 pading5px">


                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName"
                                    Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox" BorderStyle="None"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                            
                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" Width="250px">
                                </asp:DropDownList>

                                <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click"
                                    CssClass="btn btn-primary primaryBtn"
                                    Font-Underline="False">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">

                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                    Text="Size:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Visible="False" Width="71px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"
                                    Text="Group:"></asp:Label>

                                <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" Font-Bold="True"
                                    CssClass="ddlPage" OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChanged"
                                    Width="71px">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                 
                </div>
            </div>
        </div>
                <div class="table table-responsive">
                    <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvSpayment_PageIndexChanging"
                        OnRowDataBound="gvStock_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px" Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item">
                                <ItemTemplate>
                                    <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                        Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size/sft ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUsize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Facing">
                                <ItemTemplate>
                                    <asp:Label ID="lgvFacing" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvView" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Present Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apt. Value">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFUAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFPamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utility">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUtAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFUtAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Co-operative">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCoAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFCoAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usuamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDisAmt" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>'
                                        Width="80px" Style="text-align: right"></asp:Label>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>


