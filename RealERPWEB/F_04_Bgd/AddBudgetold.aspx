<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AddBudgetold.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.AddBudgetold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    `  


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="AB Date:"></asp:Label>

                                            <asp:TextBox ID="txtABDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtABDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtABDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="AB No."></asp:Label>

                                            <asp:Label ID="lblCurABNo1" runat="server" CssClass="inputtextbox" Text="AB00-"></asp:Label>

                                            <asp:TextBox ID="txtCurABNo2" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label9" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSearchPrj" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindPrj" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindPrj_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="300" CssClass="ddlPage"></asp:DropDownList>

                                             <asp:Label ID="lblProjectName" runat="server"  CssClass="inputtextbox" Visible="False" Width="300px"></asp:Label>

                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPre" runat="server" CssClass=" lblName lblTxt" Text="Pre. AB No."></asp:Label>

                                            <asp:TextBox ID="txtSearchPreAb" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindPreAb" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindPreAb_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPrevABList" runat="server" Width="300" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Page Size:" Visible="False"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5" CssClass="ddlPage"   Visible="False">
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

                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="dgvAddBgd" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Style="text-align: left" AllowPaging="True"
                OnPageIndexChanging="dgvAddBgd_PageIndexChanging">

                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNor" runat="server" Font-Bold="True"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Isircode" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lgcUcodedep" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lgcResDesc" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                Width="220px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnUpdateAdBgd" runat="server" OnClick="lbtnUpdateAdBgd_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lgvUnit" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit"))%>'
                                Width="30px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="40"
                                OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bgd Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblBgdQty" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bgd Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblBgdRat" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bgd Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblBgdAmt" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbgvFBgdAmt" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="70"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actual Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblActQty" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actqty")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblActRat" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actrat")).ToString("#,##0;(#,##0); ") %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actual Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblActAmt" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbgvFActamt" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="70"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avail. Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblAvaQty" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avaqty")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avail. Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblAvaRate" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avarat")).ToString("#,##0;(#,##0); ") %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Avail. Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAvaAmt" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avaamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="gvlbFAvaamt" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="70"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre. Bgd Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblPreBgdAmt" runat="server"
                                Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preproamt")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbgvFPreBgdAmt" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="70"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre. Bgd Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblPreBgdQty" runat="server"
                                Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preproqty")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre. Bgd Rate" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPreBgdRat" runat="server"
                                Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preprorat")).ToString("#,##0;(#,##0); ") +"</B>" %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Proposed Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAprAmt" runat="server" Style="text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lbgvFApramt" runat="server" Font-Bold="True"
                                Font-Size="13px" ForeColor="#000"
                                Style="text-align: right; height: 15px;" Width="70"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposed Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtProQty" runat="server" Style="text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposed Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblProRat" runat="server"
                                Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prorat")).ToString("#,##0;(#,##0); ")  %>'
                                Width="50px" Style="text-align: right;"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



