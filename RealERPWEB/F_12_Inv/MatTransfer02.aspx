<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MatTransfer02.aspx.cs" Inherits="RealERPWEB.F_12_Inv.MatTransfer02" %>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Trans Date.:"></asp:Label>

                                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                             <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label9" runat="server" CssClass=" smLbl_to" Text="Trans No.:"></asp:Label>

                                            <asp:Label ID="lblCurTransNo1" runat="server" CssClass="inputtextbox"></asp:Label>

                                             <asp:TextBox ID="txtCurTransNo2" runat="server" CssClass="inputtextbox">00001</asp:TextBox>

                                            <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Text="Ref. No:"></asp:Label>

                                             <asp:TextBox ID="txtrefno" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPreViousList" runat="server" CssClass=" lblName lblTxt" Text="Previous:"></asp:Label>

                                            <asp:TextBox ID="txtSrchPrevious" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPrevious_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True" Width="350" CssClass="ddlPage"></asp:DropDownList>

                                        </div>
                                    </div>
                                </asp:Panel>
                                 <asp:Panel ID="Panel2" runat="server" Visible="False">
                                     <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label10" runat="server" CssClass=" lblName lblTxt" Text="Req  No.:"></asp:Label>

                                            <asp:TextBox ID="txtReqSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnReq" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnReq_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlReqList" runat="server"  Width="350" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>

                                              <asp:Label ID="lblmsg1" runat="server"  CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                 </asp:Panel>

                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="grvacc_RowDeleting" ShowFooter="True" Width="501px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />



                                <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatCode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfrmDesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtodesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resource Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server"
                                            Style="font-size: 11px; text-align: center;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" OnClick="lnktotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                            Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server"
                                            Style="font-size: 11px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="white"
                                        HorizontalAlign="right" VerticalAlign="Middle" />

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
                                    <td class="style17">
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Trans Date.:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtCurTransDate" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style23">&nbsp;</td>
                                    <td class="style21">
                                        <asp:Label ID="Label9" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Trans No.:" Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCurTransNo1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="border: 1px solid #000000; padding: 1px 4px; text-align: left; background-color: #FFFFFF;"
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                            Width="45px">00001</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Ref. No:" Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtrefno" runat="server" BorderStyle="None" Width="105px"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" OnClick="lbtnOk_Click"
                                            Style="text-align: center" Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style30">&nbsp;</td>
                                    <td class="style31">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="style17">
                                        <asp:Label ID="lblPreViousList" runat="server" CssClass="style16"
                                            Font-Bold="True" Font-Size="12px" Style="text-align: left" Text="Previous:"
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtSrchPrevious" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        <asp:ImageButton ID="ImgbtnFindPrevious" runat="server" Height="19px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPrevious_Click"
                                            TabIndex="6" Width="21px" />
                                    </td>
                                    <td class="style21" colspan="8">
                                        <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True"
                                            Font-Size="12px" Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="7" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                          
                                <%--<tr>
                                    <td class="style26">
                                        <asp:Label ID="Label10" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Req  No.:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtReqSearch" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style28">
                                        <asp:ImageButton ID="imgbtnReq" runat="server" Height="19px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnReq_Click" TabIndex="6"
                                            Width="21px" />
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlReqList" runat="server" Font-Size="12px"
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="7" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" OnClick="lbtnSelect_Click"
                                            Style="text-align: center" Width="52px">Select</asp:LinkButton>
                                    </td>
                                    <td class="style32">
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                        
  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

