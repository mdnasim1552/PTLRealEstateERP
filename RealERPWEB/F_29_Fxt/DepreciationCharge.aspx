<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DepreciationCharge.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.DepreciationCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">

                                    <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:GridView ID="grvDeChg" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" OnPageIndexChanging="grvDeChg_PageIndexChanging"
                ShowFooter="True" Width="604px">
                <PagerSettings Position="Top" />

                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A/C Code">
                        <ItemTemplate>
                            <asp:Label ID="lgCode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                Width="80px" Font-Bold="True"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Head Of Acounts">
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkFiUpdate" runat="server"  OnClick="lnkFiUpdate_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgHacounts" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                Width="350px" Font-Bold="True"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Charge">

                        <ItemTemplate>
                            <asp:TextBox ID="txtgvchg" runat="server" BackColor="Transparent" Font-Size="11px"
                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "charge")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

