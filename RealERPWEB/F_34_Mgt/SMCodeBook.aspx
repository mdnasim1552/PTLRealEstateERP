﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SMCodeBook.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.SMCodeBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        }


    </script>


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

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 850px;">

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Select Module</label>
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Template</label>
                                <asp:DropDownList ID="ddlCatcode" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCatcode_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">Segment</label>
                                <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="custom-select chzn-select">
                                    <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-2</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="6">Details Code</asp:ListItem>
                                </asp:DropDownList>





                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn-danger btn-sm btn" Visible="false"></asp:Label>


                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select chzn-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>





                            </div>
                        </div>


                    </div>





                    <asp:GridView ID="grvacc" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" AllowPaging="True"
                        OnPageIndexChanging="grvacc_PageIndexChanging"
                        CellPadding="4" Font-Bold="False" Font-Size="12px"
                        OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                        OnRowUpdating="grvacc_RowUpdating" Width="572px" ShowFooter="True" OnRowDataBound="grvacc_RowDataBound">
                        <%-- <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                            Visible="True" />
                        <FooterStyle />--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True">
                                <HeaderStyle Font-Size="16px" />
                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                            </asp:CommandField>

                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                        Width="20px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="5"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                        Width="40px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="200px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS Template">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvsmscont" runat="server" Font-Size="12px" MaxLength="4000" TextMode="MultiLine"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smscont")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblsmscont" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smscont")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvactstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actstatus")) %>'
                                        Width="60px" Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlactStat" CssClass="form-control  chzn-select" runat="server">
                                        <asp:ListItem Value="True" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="False">In-Active</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mail Template">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvmailcont" runat="server" Font-Size="12px" MaxLength="4000" TextMode="MultiLine"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailcont")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblmailcont" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailcont")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvactMstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actmstatus")) %>'
                                        Width="60px" Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlactMStat" CssClass="form-control  chzn-select" runat="server">
                                        <asp:ListItem Value="True" Selected="True">Active</asp:ListItem>
                                        <asp:ListItem Value="False">In-Active</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:Panel ID="Paneldata" runat="server">
                                        <table style="width: 100%;">
                                            <tr>

                                                <td>
                                                    <div class="col-md-4 pading5px">
                                                        <asp:DropDownList ID="ddlData" runat="server" CssClass="form-control inputTxt chzn-select" Width="250px">
                                                        </asp:DropDownList>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcodedesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <RowStyle CssClass="grvRows" />
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


