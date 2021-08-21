﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LpSCodeBook.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LpSCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                            <asp:TextBox ID="txtFilter" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnSrch" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrch_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblStep" runat="server" CssClass="smLbl_to" Text="Step :"></asp:Label>
                                            <asp:DropDownList ID="ddlGroupList" runat="server" CssClass=" ddlPage">

                                                <asp:ListItem>Main Group</asp:ListItem>
                                                <asp:ListItem>Sub Group</asp:ListItem>
                                                <asp:ListItem>Sub-2 Group</asp:ListItem>
                                                <asp:ListItem>Sub-3 Group</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnShowData" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnShowData_Click">Show Data</asp:LinkButton>
                                        </div>



                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>

                    <asp:GridView ID="gvCodeBook" runat="server"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvCodeBook_PageIndexChanging"
                        OnRowCancelingEdit="gvCodeBook_RowCancelingEdit" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowEditing="gvCodeBook_RowEditing" OnRowUpdating="gvCodeBook_RowUpdating"
                        Width="16px" AllowPaging="True" PageSize="30">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CompCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvComCod" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GroupCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfGrp" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infgrp")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfCod" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True">
                                <ItemStyle Font-Bold="True" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfCod1" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="16"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                        Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfCod1" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                        Width="100px" Style="text-align: left; font-weight: 700"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfDesc" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfDes2" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                        Width="177px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfDes2" runat="server" Height="20px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvUnitFPS" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                        Width="55px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvUnitFPS" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                        Width="58px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Rate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvStdQtyF" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("###0.0000;(###0.00); ") %>'
                                        Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStdQtyF" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                        Width="100px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Const. Area" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvcarea" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("###0.00;(###0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcarea" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saleable Area" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvsarea" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("###0.00;(###0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgvsarea" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Project Name" >
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                
                                                <td>
                                                    <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProName" runat="server" CssClass="ddlistPull"  TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProNames" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="250px"></asp:Label>
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


            <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                           BorderWidth="1px">
            <table style="width: 98%;">

                <tr>
                    <td class="style18">
                        <asp:TextBox ID="txtFilter" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Height="18px" Width="86px"></asp:TextBox>
                    </td>
                    <td class="style25">
                        <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" TabIndex="1" 
                            Width="16px" />
                    </td>
                    <td class="style4">
                        <asp:Label ID="lblStep" runat="server" Font-Size="14px" Font-Underline="False" 
                            style="font-weight: 700; text-align:right; color: #FFFFFF;" Text="Step :" 
                            Width="62px"></asp:Label>
                    </td>
                    <td class="style24">
                        <asp:DropDownList ID="ddlGroupList" runat="server" Font-Bold="True" 
                            Font-Size="12px" Height="21px" style="text-transform: capitalize" TabIndex="2" 
                            Width="120px">
                            <asp:ListItem>Main Group</asp:ListItem>
                            <asp:ListItem>Sub Group</asp:ListItem>
                            <asp:ListItem>Sub-2 Group</asp:ListItem>
                            <asp:ListItem>Sub-3 Group</asp:ListItem>
                            <asp:ListItem Selected="True">Details</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style26">
                        <asp:LinkButton ID="lbtnShowData" runat="server" Font-Bold="True" 
                            Font-Size="12px" onclick="lbtnShowData_Click" 
                            style="color: #FFFFFF; " TabIndex="3" Width="60px">Show Data</asp:LinkButton>
                    </td>
                    <td class="style27">
                        <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" 
                            Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
                </asp:Panel>--%>

            <table style="width: 98%;">
                <tr>
                    <td class="style13" colspan="10"></td>
                </tr>
                <tr>
                    <td class="style13">&nbsp;</td>
                    <td class="style18">&nbsp;</td>
                    <td class="style19">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style20">&nbsp;</td>
                    <td class="style14">&nbsp;</td>
                    <td class="style21">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




