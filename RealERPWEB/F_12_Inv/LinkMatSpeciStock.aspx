
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMatSpeciStock.aspx.cs" Inherits="RealERPWEB.F_12_Inv.LinkMatSpeciStock" %>

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
                                        <asp:Label ID="lblprojectname" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblvalprojectname" runat="server" CssClass="inputlblVal" Style="width:280px;"></asp:Label>
                                       
                                    </div>
                                </div>

                                
                                <div class="form-group">

                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lblmaterial" runat="server" CssClass="lblTxt lblName" Text="Material Name:"></asp:Label>
                                        <asp:Label ID="lblvalmaterial" runat="server" CssClass="inputlblVal" Style="width:280px;"></asp:Label>
                                       
                                    </div>
                                </div>

                                 <div class="form-group">

                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lbldaterange" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:Label ID="lblvaldaterange" runat="server" CssClass=" inputlblVal" Style="width:280px;"></asp:Label>
                                       
                                    </div>
                                </div>


                                  <div class="form-group">
                                   
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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




                                </div>

                                

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvMatStock_PageIndexChanging">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                       
                              <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lgvspecification" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFopening" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvOp" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRecqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lgvFRecqty" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer In">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtraninqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                    <asp:Label ID="lgvFtraninqty" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transfer Out">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtranoutqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                    <asp:Label ID="lgvFtranoutqty" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Damage/Lost">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDamage" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                    <asp:Label ID="lgvFDamage" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                 <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvtreceived" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFtreceived" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvisuqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lgvFisuqty" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lgvacstock" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0.00;(#,##0.00); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFacstock" runat="server" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
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



