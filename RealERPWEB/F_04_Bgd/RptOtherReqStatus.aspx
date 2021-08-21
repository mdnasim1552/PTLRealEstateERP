<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptOtherReqStatus.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptOtherReqStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

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
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label7" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="300" CssClass="ddlPage"></asp:DropDownList>


                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date:"></asp:Label>

                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label6" runat="server" CssClass=" lblName lblTxt" Text="To:"></asp:Label>

                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Page Size:" Visible="False"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass="ddlPage">
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

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Req:"></asp:Label>

                                            <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindRequiSition" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindRequiSition_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>


                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvOtherReqStatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" Width="926px"
                OnPageIndexChanging="gvReqStatus_PageIndexChanging"
                ShowFooter="True">
                <PagerSettings Position="Top" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvProjDesc" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                Width="130px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Req. No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvReqNo1" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRF No">
                        <ItemTemplate>
                            <asp:Label ID="lblgvMrfNo" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                Width="60px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblgvDate" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description of Materials">

                        <ItemTemplate>
                            <asp:Label ID="lblgvResDesc" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                Width="250px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="Total" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="140px" Text="Total :"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="white" HorizontalAlign="right" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Requisition. Amt">
                        <ItemTemplate>
                            <asp:Label ID="lblgvReqAmt" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFReqAmt" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="white" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved Amt.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvAppamt" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFAppamt" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="white" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
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




            <%--<tr>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style45">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style46">
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style36">
                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnFindProject_Click"
                                            Width="16px" />
                                    </td>
                                    <td class="style55" colspan="8">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#99FFCC" Height="20px"
                                            OnClick="lnkbtnOk_Click" Style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style39">&nbsp;</td>
                                    <td class="style40">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>

            <%--<tr>
                                    <td class="style56"></td>
                                    <td class="style57">
                                        <asp:Label ID="Label5" runat="server" CssClass="style42" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: right" Text="Date:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label6" runat="server" CssClass="style42" Font-Bold="True"
                                            Style="text-align: right" Text="To:" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style60" colspan="8">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style61"></td>
                                    <td class="style62"></td>
                                    <td class="style63"></td>
                                    <td class="style63"></td>
                                </tr>--%>

            <%--<tr>
                                    <td class="style47">&nbsp;</td>
                                    <td class="style45">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False"
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style46">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                            Width="80px">
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
                                    </td>
                                    <td class="style36">
                                        <asp:Label ID="Label8" runat="server" CssClass="style42" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: right" Text="Req:"></asp:Label>
                                    </td>
                                    <td class="style67">
                                        <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style69">
                                        <asp:ImageButton ID="imgbtnFindRequiSition" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnFindRequiSition_Click"
                                            Width="16px" />
                                    </td>
                                    <td class="style70">&nbsp;</td>
                                    <td class="style68">&nbsp;</td>
                                    <td class="style67">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style65">&nbsp;</td>
                                    <td class="style55">&nbsp;</td>
                                    <td class="style39">&nbsp;</td>
                                    <td class="style40">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>

          
        </ContentTemplate>
    </asp:UpdatePanel>
  
</asp:Content>



