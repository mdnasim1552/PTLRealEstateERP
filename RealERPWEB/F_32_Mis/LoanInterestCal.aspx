<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LoanInterestCal.aspx.cs" Inherits="RealERPWEB.F_32_Mis.LoanInterestCal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-9 pading5px">
                                            <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox" TabIndex="10"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" CssClass=" ddlPage" TabIndex="12" Width="300px"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="lblDate0" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>

                                            <asp:TextBox ID="txtfDat" runat="server" CssClass="inputtextbox" TabIndex="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfDat_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfDat"></cc1:CalendarExtender>

                                            <asp:Label ID="lblDate1" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>

                                            <asp:TextBox ID="txttDat" runat="server" CssClass="inputtextbox" TabIndex="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttDat_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttDat"></cc1:CalendarExtender>

                                            <asp:Label ID="LblIntrstRate" runat="server" CssClass=" smLbl_to" Text="Interest Rate(Per Month):"></asp:Label>

                                            <asp:TextBox ID="TxtIntrstRate" runat="server" CssClass="inputtextbox" TabIndex="10"></asp:TextBox>


                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                    </div>


                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvInstCal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="696px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDate" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="White" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvProc" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" Font-Size="12pt" ForeColor="White" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRec" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance" FooterText="Total: ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbalAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="White" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvday" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daydif")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDays" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") + "%" %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Interest">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvIns" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intamt")).ToString("#,##0;(#,##0); ")%>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFInst" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
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


