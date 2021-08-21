<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptRatioAnalisiys.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptRatioAnalisiys" %>

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
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From Date:"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="  smLbl_to" Text="To:" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="1" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>


                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                        <asp:GridView ID="gvIncomeMon" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="616px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrpdesc" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))  %>'
                                            Width="100px" Style="font-size: 11px; color: Black;"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Particulers">
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesc"))  %>'
                                            Width="150px" Style="font-size: 11px; color: Black;"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Formula">
                                    <ItemTemplate>

                                        <asp:Label ID="Label1" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rfourm"))  %>'
                                            Width="290px" Style="font-size: 11px; color: Black;"></asp:Label>


                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "formuladec1"))  %>'
                                            Width="290px" Style="font-size: 11px; color: Black;"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Ratio">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtmpaysum" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ratio"))%>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoamtmpaysum" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Standard">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFamtmpaysum1" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamtmpaysum1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="%" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvt" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ratioprcen"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Interpretaion">
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvPart" runat="server"
                                            Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inter"))  %>'
                                            Width="90px" Style="font-size: 11px"></asp:Label>
                                    </ItemTemplate>

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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

