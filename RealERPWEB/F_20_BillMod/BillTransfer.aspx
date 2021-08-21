<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BillTransfer.aspx.cs" Inherits="RealERPWEB.F_20_BillMod.BillTransfer" %>

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
                                <asp:Panel ID="Panel2" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                                            <asp:Label ID="lblmsg1" runat="server" CssClass=" lblmsg"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblProjectFromList0" runat="server" CssClass="lblTxt lblName" Text="From:"></asp:Label>
                                            <asp:TextBox ID="txtProjectSearchF" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlprjlistfrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged" Width="300px" CssClass="form-control inputTxt" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control inputTxt" Visible="False" Width="300px"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px ">
                                          <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="1"  Text="Ok"  Style="margin-left:8px;"></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="To:"></asp:Label>
                                            <asp:TextBox ID="txtProjectSearchT" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindProject0" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject0_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-3pading5px">
                                            <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="form-control inputTxt" TabIndex="2" Width="300px">
                                            </asp:DropDownList>
                                             <asp:Label ID="lblddlProjectTo" runat="server" CssClass="form-control inputTxt" Visible="False" Width="295px"></asp:Label>

                                        </div>
                                        <div class="col-md-5 pading5px ">
                                           
                                        </div>


                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Style="margin-top: 0px" Width="831px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Nature">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server"  OnClick="lbtnUpdate_Click" CssClass="btn btn-danger  primaryBtn" >Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subbmited Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgFsubamt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Approved Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFapprovedamt" runat="server" ></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvaprovedamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreceived" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "received")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkrcv" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkrcv"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
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
                                    <td class="style27">
                                        <asp:Label ID="Label14" runat="server" CssClass="style16" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="text-align: left" Text="Date:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style26">
                                        <asp:TextBox ID="txtCurTransDate" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style24">
                                        &nbsp;
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Italic="False" Font-Size="12px"
                                            ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td class="style17">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style78" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style27">
                                        <asp:Label ID="lblProjectFromList0" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Height="16px" Style="text-align: left" Text="From:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style26">
                                        <asp:TextBox ID="txtProjectSearchF" runat="server" BorderStyle="None" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnFindProject_Click" Width="16px" />
                                    </td>
                                    <td class="style80" colspan="38">
                                        <asp:DropDownList ID="ddlprjlistfrom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged"
                                            Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectFrom" runat="server" __designer:wfdid="w4" BackColor="White"
                                            Font-Bold="True" Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px;
                                            text-align: left" Visible="False" Width="295px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                            Style="text-align: center;" Width="52px" BackColor="#000066" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" ForeColor="White">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style84">
                                        &nbsp;
                                    </td>
                                    <td class="style83">
                                        &nbsp;
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style27">
                                        <asp:Label ID="Label13" runat="server" CssClass="style16" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: left" Text="To:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style26">
                                        <asp:TextBox ID="txtProjectSearchT" runat="server" BorderStyle="None" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="ImgbtnFindProject0" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnFindProject0_Click" Width="16px" />
                                    </td>
                                    <td class="style80" colspan="38">
                                        <asp:DropDownList ID="ddlprjlistto" runat="server" Style="margin-left: 0px" Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectTo" runat="server" __designer:wfdid="w4" BackColor="White"
                                            Font-Bold="True" Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px;
                                            text-align: left" Visible="False" Width="295px"></asp:Label>
                                    </td>
                                    <td class="style84">
                                        &nbsp;
                                    </td>
                                    <td class="style83">
                                        &nbsp;
                                    </td>
                                </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
