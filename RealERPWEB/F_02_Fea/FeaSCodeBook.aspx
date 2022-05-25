<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FeaSCodeBook.aspx.cs" Inherits="RealERPWEB.F_02_Fea.FeaSCodeBook" %>

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

                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvCodeBook" runat="server"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvCodeBook_PageIndexChanging"
                            OnRowCancelingEdit="gvCodeBook_RowCancelingEdit"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowEditing="gvCodeBook_RowEditing" OnRowUpdating="gvCodeBook_RowUpdating"
                            Width="16px" AllowPaging="True" PageSize="20">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
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
                                <asp:CommandField ShowEditButton="True" CancelText="Can" UpdateText="Up">
                                    <ItemStyle Font-Bold="True" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfCod1" runat="server" CssClass="form-control inputTxt"
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
                                        <asp:TextBox ID="txtgvInfDesc" runat="server" CssClass="form-control inputTxt"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfDes2" runat="server" CssClass="form-control inputTxt"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                            Width="150px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDes2" runat="server" Height="20px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnitFPS" runat="server" CssClass="form-control inputTxt"
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
                                        <asp:TextBox ID="txtgvStdQtyF" runat="server" CssClass="form-control inputTxt"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("###0.0000;(###0.0000); ") %>'
                                            Width="55px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvStdQtyF" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="58px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Const. Area" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvcarea" runat="server" CssClass="form-control inputTxt"
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
                                        <asp:TextBox ID="txtgvsarea" runat="server" CssClass="form-control inputTxt"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("###0.00;(###0.00); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgvsarea" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSerachProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="ddlistPull" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Team" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Panel ID="pnlTeam" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSearchteam" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchteam" runat="server" OnClick="ibtnSrchteam_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlteam" runat="server" CssClass="ddlPage" Width="130px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcatdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc")) %>'
                                            Width="200px"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



