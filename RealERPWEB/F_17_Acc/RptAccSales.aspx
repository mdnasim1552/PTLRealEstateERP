<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccSales.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        $(document).ready(function () {
            alert('Pro');
            $('#<%=this.txtdate01.ClientID%>').datepicker({ dateFormat: 'dd-M-yy' });
            

        });
        </script>--%>


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

                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName ">Date.</asp:Label>
                                        <%-- <asp:TextBox ID="txtdate01" runat="server" CssClass="inputtextbox "></asp:TextBox>--%>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                    
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Size:"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"                                            
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn margin5px" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                   


                                </div>


                            </div>
                        </fieldset>

                    </div>



                    <asp:GridView ID="gvSalesDet" runat="server" PageSize="15"
                        AutoGenerateColumns="False" ShowFooter="True" AllowPaging="True"
                        OnPageIndexChanging="gvSalesDet_PageIndexChanging" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvActDesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))
                                                                          %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="True" Font-Size="12pt" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advanced Sales">
                                <ItemTemplate>
                                    <asp:Label ID="lgvadsale" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advsale")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFadsale" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Sales">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsale" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sale")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFsale" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvreceived" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "received")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Receivable">
                                <ItemTemplate>
                                    <asp:Label ID="lgvreceivable" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFreceivable" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFcost" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Margin">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmargin" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "margin")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lgvFmargin" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: right" Width="75px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>






                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>

                </div>
                <!-- End of Content Part-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

