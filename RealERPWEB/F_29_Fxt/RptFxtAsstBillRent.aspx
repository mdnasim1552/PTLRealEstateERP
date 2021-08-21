
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFxtAsstBillRent.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFxtAsstBillRent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />


    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
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
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="form-group">
                                    <div class="col-md-8  pading5px  asitCol8">
                                        <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Bill No:"></asp:Label>
                                        <asp:Label ID="lblBillNo" runat="server" CssClass="inputtextbox"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px  asitCol10">
                                        <asp:Label ID="Label7" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="ddlPage" Width="300px"></asp:DropDownList>

                                        <asp:Label ID="lblProjectName" runat="server" CssClass=" inputtextbox" Text="Label"
                                            Visible="False" Width="300px"></asp:Label>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="form-group">
                                    <div class="col-md-8   pading5px  asitCol8">
                                        <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy ddd"
                                            TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy ddd"
                                            TargetControlID="txttodate"></cc1:CalendarExtender>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-8   pading5px  asitCol8">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Style="text-align: left" Width="810px">

                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lgvrescode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resource Description">
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lgvQty" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="right" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Receive Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvReceiveDat" runat="server"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy ") %>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="left" />
                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transfer Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvTrnsDat" runat="server"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trnsdate")).ToString("dd-MMM-yyyy ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Duration">
                        <ItemTemplate>
                            <asp:Label ID="lgvDuration" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rent Perday">
                        <ItemTemplate>
                            <asp:Label ID="lgvRentPerday" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                Font-Size="12px" ForeColor="White" Style="text-align: right"
                                OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="right" Width="70px" />
                        <FooterStyle HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <FooterTemplate>
                            <asp:Label ID="lgvFAmount" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lgvAmount" runat="server"
                                Style="font-size: 12px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                        <HeaderStyle HorizontalAlign="right" />
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

            <%--<tr>
                                    <td class="style19" width="100px" colspan="2">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True"
                                            Style="text-align: right; color: #FFFFFF;" Text="Bill No:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style19">
                                        <asp:Label ID="lblBillNo" runat="server" BackColor="White" Font-Bold="True"
                                            Font-Size="12px" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19">&nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style19" width="100px">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True"
                                            Style="text-align: right; color: #FFFFFF;" Text="Project Name:"
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td class="style19">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click"
                                            Style="width: 18px" />
                                    </td>
                                    <td class="style19">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lblProjectName" runat="server" BackColor="White"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#6600FF" Text="Label"
                                            Visible="False" Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12pt"
                                            OnClick="lbtnOk_Click" Style="color: #FFFFFF" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style19">&nbsp;</td>
                                    <td class="style19"></td>
                                    <td class="style19"></td>
                                    <td class="style19"></td>
                                    <td class="style19"></td>
                                    <td class="style19"></td>
                                    <td class="style19"></td>
                                </tr>--%>


            <%--<tr>
                                    <td class="style21">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                            Style="text-align: right; color: #FFFFFF;" Text="From:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy ddd"
                                            TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style22">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="To:"
                                            Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat"
                                            Font-Bold="True" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy ddd"
                                            TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style21">&nbsp;</td>
                                    <td class="style21">&nbsp;</td>
                                    <td class="style22">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="color: #FFFFFF" BackColor="Red"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>

            
         
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



